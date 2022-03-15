using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour

{
    public Transform camera, player, centerPoint;

    //Movement Variables.
    public float mouseX, mouseY;
    public float mouseSpeed = 100f;
    private float moveForward, moveSide;
    public float moveSpeed = 2f;
    public float zoom;
    private float zoomSpeed = 5f;
    public float rotationSpeed = 5f;
    public float maxSpeed = 5f;


    //Picking up Variables
    public GameObject pickup;
    public bool isCarrying = false;
    public GameObject carrying;
    public int size;
    private bool pickingUp = false;
    private bool dropping;
    private float timer;
    public float carryRotation;
    public float startCarryRotation;

    //Other Variables
    public bool isAttacking;
    public int stars = 0;
    public int tears = 0;
    public int starLv = 0;
    public int tearLv = 0;
    private Animator anim;
    private string[] animations;
    public AudioClip grunt;
    public AudioClip walk;
    public DayCycle daycycle;

    public GameObject growEffect;

    // Start is called before the first frame update
    void Start()
    {
        animations = new string[] { "Idle", "Walking", "LeftTurn", "RightTurn", "Attack", "WalkCarry", "IdleCarry", "PickUp", "LeftTurnCarry", "RightTurnCarry", "Backward", "BackwardCarry", "Drop" };
        zoom = -7;
        dropping = false;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (daycycle.day == false)
        {
            ResetAnimations("Idle");
            anim.SetBool("Idle", true);
            Destroy(GetComponent<PlayerControl>());
        }
        //        //FOR DEMO PURPOSES
        //        if (Input.GetKeyDown(KeyCode.R))
        //        {
        //#pragma warning disable CS0618 // Type or member is obsolete
        //            Application.LoadLevel(0);
        //#pragma warning restore CS0618
        //        }
        if (Input.GetButtonDown("Pause"))
        {
            GameObject.Find("Directional Light").GetComponent<Save>().save();
            GameObject.Find("Canvas").GetComponentInChildren<Fade>().HalfFade();
            GameObject.Find("Canvas").transform.Find("QuitText").gameObject.SetActive(true);
            StartCoroutine(WaitForQuit());
        }

        //CAMERA AND PLAYER MOVEMENT
        //Camera Zooming
        zoom += (Input.GetAxis("Mouse ScrollWheel") + Input.GetAxis("Zoom")) * zoomSpeed;
        zoom = Mathf.Clamp(zoom, -12, -5);
        camera.transform.localPosition = new Vector3(0, 0, zoom);

        //Look around pivot with the mouse
        mouseX += (Input.GetAxis("Mouse X") + Input.GetAxis("HorizontalRS")) * mouseSpeed;
        mouseY += (Input.GetAxis("Mouse Y") + Input.GetAxis("VerticalRS")) * mouseSpeed;
        mouseY = Mathf.Clamp(mouseY, -10f, 60f); // Camera can't go through floor or directly overhead.
        camera.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);

        //Get any input for movement by the player
        moveForward += Input.GetAxis("Vertical") * moveSpeed;
        moveSide += Input.GetAxis("Horizontal") * moveSpeed;

        if (moveForward < -1f)
        {
            moveSide = 0f;
        }

        //If no input, come to a stop.
        if (Input.GetAxis("Horizontal") == 0)
        {
            moveSide = 0;
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            moveForward = 0;
        }

        //make sure Player can't move faster than designated max speeds
        moveSide = Mathf.Clamp(moveSide, -maxSpeed, maxSpeed);
        moveForward = Mathf.Clamp(moveForward, -2, maxSpeed); //Player can not upgrade backpedal speed

        if (isAttacking == false && pickingUp == false && dropping == false)//Attacking locks the player into place
        {
            transform.Rotate(0, (moveSide * 10 * Time.deltaTime), 0);//Rotate
            Vector3 movement = new Vector3(0, 0, moveForward); //get value for moving the player forward and backwards
            movement = transform.rotation * movement; //get the direction for moving the player forward and backwards
            player.GetComponent<CharacterController>().Move(movement * Time.deltaTime); //Move the player
            centerPoint.position = new Vector3(player.position.x, (player.position.y + 1), player.position.z); //Re-allign centerpoint for camera
        }
        if (gameObject.GetComponent<CharacterController>().isGrounded == false) //If the giant isn't on the ground, get him grounded as soon as possible
        {
            Vector3 grav = new Vector3(0, -150, 0);
            player.GetComponent<CharacterController>().Move(grav * Time.deltaTime);
        }




        //*********
        //Animation
        //*********
        if (dropping == false)
        {
            if (pickingUp == false)
            {
                if (isAttacking == true)
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
                        ResetAnimations("Attack");
                    anim.SetBool("Attack", true);
                }
                else if (moveForward == 0 && moveSide == 0)
                {
                    if (isCarrying == true)
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleCarry") == false)
                            ResetAnimations("IdleCarry");
                        anim.SetBool("IdleCarry", true);
                    }
                    else
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") == false)
                        {
                            ResetAnimations("Idle");
                            anim.SetBool("Idle", true);
                        }
                    }
                    gameObject.GetComponent<AudioSource>().clip = null;
                    gameObject.GetComponent<AudioSource>().Stop();


                }
                else if (Input.GetAxis("Vertical") > 0.05f)
                {
                    if (isCarrying == true)
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkCarry") == false)
                            ResetAnimations("WalkCarry");
                        anim.SetBool("WalkCarry", true);
                    }
                    else
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") == false)
                            ResetAnimations("Walking");
                        anim.SetBool("Walking", true);
                    }
                    if (gameObject.GetComponent<AudioSource>().clip != walk)
                    {
                        gameObject.GetComponent<AudioSource>().loop = true;
                        gameObject.GetComponent<AudioSource>().Stop();
                        gameObject.GetComponent<AudioSource>().clip = walk;
                        gameObject.GetComponent<AudioSource>().Play();
                    }

                }
                else if (Input.GetAxis("Vertical") < -0.05f)
                {
                    if (isCarrying == true)
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BackwardCarry") == false)
                            ResetAnimations("BackwardCarry");
                        anim.SetBool("BackwardCarry", true);
                    }
                    else
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Backwards") == false)
                            ResetAnimations("Backward");
                        anim.SetBool("Backward", true);
                    }
                    if (gameObject.GetComponent<AudioSource>().clip != walk)
                    {
                        gameObject.GetComponent<AudioSource>().loop = true;
                        gameObject.GetComponent<AudioSource>().Stop();
                        gameObject.GetComponent<AudioSource>().clip = walk;
                        gameObject.GetComponent<AudioSource>().Play();
                    }
                }
                else if (Input.GetAxis("Horizontal") < -0.05f)
                {
                    if (isCarrying == true)
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("LeftTurnCarry") == false)
                            ResetAnimations("LeftTurnCarry");
                        anim.SetBool("LeftTurnCarry", true);
                    }
                    else
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("LeftTurn") == false)
                            ResetAnimations("LeftTurn");
                        anim.SetBool("LeftTurn", true);

                    }
                    if (gameObject.GetComponent<AudioSource>().clip != walk)
                    {
                        gameObject.GetComponent<AudioSource>().loop = true;
                        gameObject.GetComponent<AudioSource>().Stop();
                        gameObject.GetComponent<AudioSource>().clip = walk;
                        gameObject.GetComponent<AudioSource>().Play();
                    }

                }
                else if (Input.GetAxis("Horizontal") > 0.05f)
                {
                    if (isCarrying == true)
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("RightTurnCarry") == false)
                            ResetAnimations("RightTurnCarry");
                        anim.SetBool("RightTurnCarry", true);
                    }
                    else
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("RightTurn") == false)
                            ResetAnimations("RightTurn");
                        anim.SetBool("RightTurn", true);
                    }
                    if (gameObject.GetComponent<AudioSource>().clip != walk)
                    {
                        gameObject.GetComponent<AudioSource>().loop = true;
                        gameObject.GetComponent<AudioSource>().Stop();
                        gameObject.GetComponent<AudioSource>().clip = walk;
                        gameObject.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }



        //***********************
        //Picking up and Carrying
        //***********************

        //if (Input.GetKeyDown("z"))
        //{
        //    if (pickup.GetComponent<PickUpDetect>().pickUp.GetComponent<WorkShop>() != null )
        //    {
        //        if (pickup.GetComponent<PickUpDetect>().pickUp.GetComponent<WorkShop>().open == false)
        //        {
        //            pickup.GetComponent<PickUpDetect>().pickUp.GetComponent<WorkShop>().OpenWorkShop();
        //            isAttacking = true;
        //        }

        //    }
        //}

        if (Input.GetButtonDown("PickUp"))
        {
            if (isCarrying == false) //If the player isn't carry anything, pick up an object
            {
                pickup.SendMessage("GetPickUp", SendMessageOptions.DontRequireReceiver);
                //print("Get item");
                ResetAnimations("PickUp");
                anim.SetBool("PickUp", true);
                pickingUp = true;
                timer = Time.time;

            }

            else //The player is carrying something, so drop it
            {
                gameObject.GetComponent<AudioSource>().loop = false;
                gameObject.GetComponent<AudioSource>().Stop();
                gameObject.GetComponent<AudioSource>().clip = grunt;
                gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(Drop());
            }

        }
        if (Time.time - timer > 1.3f && Time.time - timer < 1.4f)//Used so the picking up grunt sound is played at the correct time
        {
            gameObject.GetComponent<AudioSource>().Play(0);
        }

        if (Time.time - timer > 2f)
            pickingUp = false;

        if (isCarrying == true)//Make the carried object move with the player
        {
            if (carrying.GetComponent<Object>()!= null)
            {
                if (carrying.GetComponent<Object>().item == "sHouse")
                {
                    carrying.transform.position = new Vector3(pickup.transform.position.x, pickup.transform.position.y + 1.6f, pickup.transform.position.z);
                }
                else
                    carrying.transform.position = new Vector3(pickup.transform.position.x, pickup.transform.position.y + 0.5f, pickup.transform.position.z);
            }
            else
                carrying.transform.position = new Vector3(pickup.transform.position.x, pickup.transform.position.y + 0.5f, pickup.transform.position.z);
            carrying.transform.eulerAngles = new Vector3(pickup.transform.rotation.x, carryRotation + (transform.eulerAngles.y - startCarryRotation), pickup.transform.rotation.z);

        }

        //*********
        //Attacking
        //*********
        if (isCarrying == false) //Can't attack if the player is carrying something
        {
            if (tearLv >= 1) // Can't attack until leveled up
            {
                if (Input.GetButtonDown("Attack"))
                {
                    if (isAttacking == false)//If the player isn't already attacking, attack
                    {
                        isAttacking = true;
                        pickup.SendMessage("Attack", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

        }

        //*******
        //Growing
        //*******

        //Stars give speed upgrades
        if (stars < 10)
        {
            starLv = 0;
            maxSpeed = 2f;
            rotationSpeed = 1.2f;
        }
        else if (stars >= 10 && stars < 20)
        {
            starLv = 1;
            maxSpeed = 3f;
            rotationSpeed = 1.3f;
        }
        else if (stars >= 20 && stars < 30)
        {
            starLv = 2;
            maxSpeed = 4f;
            rotationSpeed = 1.4f;
        }
        else if (stars >= 30 && stars < 40)
        {
            starLv = 3;
            maxSpeed = 5f;
            rotationSpeed = 1.5f;
        }
        //tears unlocks new abilities

        if (tears < 10)
        {
            tearLv = 0;
        }
        else if (tears >= 10 && tears < 20)
        {
            tearLv = 1;
        }
        else if (tears >= 20 && tears < 30)
        {
            tearLv = 2;
        }
        else if (tears >= 30 && tears < 40)
        {
            tearLv = 3;
        }
        //Leveling up results in a bigger giant that can lift heavier objects
        if (tearLv + starLv == 1)
        {
            if (size < 2)
            {
                size = 2;
                StartCoroutine(Grow());
            }
        }

        if (tearLv + starLv == 2)
        {
            if (size < 3)
            {
                size = 3;
                StartCoroutine(Grow());
            }
        }

        if (tearLv + starLv == 3)
        {
            if (size < 4)
            {
                size = 4;
                StartCoroutine(Grow());
            }
        }
    }

    private void ReturnPickUp(GameObject obj)//When the giant hears a reply telling it what to try to pick up
    {
        if (transform.root != transform) //Get the parent of the object if the object is a child
            carrying = obj.transform.parent.gameObject;
        else
            carrying = obj;

        obj.SendMessage("GetWeight", gameObject, SendMessageOptions.DontRequireReceiver); //Get the weight of the object
        //print("Get Weight");
    }

    private void ReturnWeight(int weight)//When the giant hears a reply telling it the objects weight
    {
        if (weight <= size) //If the giant is big enough, carry the object
        {
            isCarrying = true;
            carrying.transform.position = pickup.transform.position;
            carryRotation = carrying.transform.eulerAngles.y;
            startCarryRotation = transform.eulerAngles.y;
        }
    }

    private void AttackFinished() //Alow the player to perform other actons after attacking is over
    {
        isAttacking = false;
    }

    public void starTear(bool isStar)//Once a star or tear is collected, add it to correct total
    {
        if (isStar == true)
            stars++;
        else
            tears++;
    }

    private IEnumerator Drop() //Drop the object and allow the giant to pick up a new object
    {
        dropping = true;
        ResetAnimations("Drop");
        anim.SetBool("Drop", true);
        yield return new WaitForSeconds(1.5f);
        carrying = null;
        isCarrying = false;
        yield return new WaitForSeconds(0.5f);
        dropping = false;
    }

    private void ResetAnimations(string next)
    {
        for (int i = 0; i < animations.Length; i++)
        {
            if (animations[i] != next)
            {
                anim.SetBool(animations[i], false);
            }
        }

    }

    public void WorkShopExit()
    {
        isAttacking = false;
    }

    public void Freeze()
    {
        ResetAnimations("Idle");
        anim.Play("Idle");
        Destroy(this);
    }

    public void HoldingStolen()
    {
        StartCoroutine(Drop());
    }

    private IEnumerator Grow()
    {
        Instantiate(growEffect, transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 180f)));
        yield return new WaitForSeconds(0.5f);
        switch (size)
        {
            case 2:
                transform.localScale = new Vector3(1.2f, 3.6f, 1.2f);
                break;
            case 3:
                transform.localScale = new Vector3(1.4f, 4.2f, 1.4f);
                break;
            case 4:
                transform.localScale = new Vector3(1.6f, 4.8f, 1.6f);
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitForQuit()
    {
        yield return new WaitForSeconds(3f);
        GameObject.Find("Canvas").transform.Find("QuitText").gameObject.SetActive(false);
        if (Input.GetButton("Pause"))
        {
            GameObject.Find("Canvas").GetComponentInChildren<Fade>().StartFadeOut();
            yield return new WaitForSeconds(1f);
            Application.LoadLevel(0);

        }
        GameObject.Find("Canvas").GetComponentInChildren<Fade>().StartFadeIn();
    }
}

