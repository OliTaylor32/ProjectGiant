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
    private float zoomSpeed = 2f;
    public float rotationSpeed = 5f;
    public float maxSpeed = 5f;
    

    //Picking up Variables
    public GameObject pickup;
    public bool isCarrying = false;
    public GameObject carrying;
    public int size;
    private bool pickingUp = false;
    private float timer;

    //Other Variables
    public bool isAttacking;
    public int stars = 0;
    public int tears = 0;
    public int starLv = 0;
    public int tearLv = 0;

    // Start is called before the first frame update
    void Start()
    {
        zoom = -7;
    }

    // Update is called once per frame
    void Update()
    {
        //FOR DEMO PURPOSES
        if (Input.GetKeyDown(KeyCode.R))
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel(0);
            #pragma warning restore CS0618 
        }

        //CAMERA AND PLAYER MOVEMENT
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        zoom = Mathf.Clamp(zoom, -12, -5);
        camera.transform.localPosition = new Vector3(0, 0, zoom);

        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseY = Mathf.Clamp(mouseY, -10f, 60f); // Camera can't go through floor or directly overhead.
        camera.LookAt(centerPoint);

        centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
        moveForward += Input.GetAxis("Vertical") * moveSpeed;
        moveSide += Input.GetAxis("Horizontal") * moveSpeed;


        if (Input.GetAxis("Horizontal") == 0)
        {
            moveSide = 0;
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            moveForward = 0;
        }

        moveSide = Mathf.Clamp(moveSide, -maxSpeed, maxSpeed);
        moveForward = Mathf.Clamp(moveForward, -3, maxSpeed);

        if (isAttacking == false)
        {
            transform.Rotate(0, moveSide * 10 * Time.deltaTime, 0);
            Vector3 movement = new Vector3(0, 0, moveForward);
            movement = transform.rotation * movement;
            player.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
            centerPoint.position = new Vector3(player.position.x, (player.position.y + 1), player.position.z);
            if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            {
                Quaternion turnAngle = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0);
                player.rotation = Quaternion.Slerp(player.rotation, turnAngle, Time.deltaTime * rotationSpeed);
            }

        }
        if (gameObject.GetComponent<CharacterController>().isGrounded == false)
        {
            Vector3 grav = new Vector3(0, -10, 0);
            player.GetComponent<CharacterController>().Move(grav * Time.deltaTime);
        }


        //*********
        //Animation
        //*********
        if (pickingUp == false)
        {
            if (isAttacking == true)
            {
                gameObject.GetComponent<Animator>().Play("Attack");
            }
            else if (moveForward == 0 && moveSide == 0)
            {
                if (isCarrying == true)
                {
                    gameObject.GetComponent<Animator>().Play("IdleCarry");
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("Idle");
                }

            }
            else if (Input.GetKey(KeyCode.W) ==true)
            {
                if (isCarrying == true)
                {
                    gameObject.GetComponent<Animator>().Play("WalkCarry");
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("Walk");
                }

            }
            else if (Input.GetKey(KeyCode.A) == true)
            {
                if (isCarrying == true)
                {
                    gameObject.GetComponent<Animator>().Play("TurnLeftCarry");
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("TurnLeft");
                }

            }
            else if (Input.GetKey(KeyCode.D) == true)
            {
                if (isCarrying == true)
                {
                    gameObject.GetComponent<Animator>().Play("TurnRightCarry");
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("TurnRight");
                }

            }
            else if (Input.GetKey(KeyCode.S) == true)
            {
                if (isCarrying == true)
                {
                    gameObject.GetComponent<Animator>().Play("BackwardsCarry");
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("Backwards");
                }

            }
        }



        //***********************
        //Picking up and Carrying
        //***********************

        if (Input.GetKeyDown("space"))
        {
            //print("Space key pressed");
            if (isCarrying == false)
            {
                pickup.SendMessage("GetPickUp", SendMessageOptions.DontRequireReceiver);
                //print("Get item");
                gameObject.GetComponent<Animator>().Play("PickingUp");
                pickingUp = true;
                timer = Time.time;

            }

            else
            {
                isCarrying = false;
                //print("Drop item");
                carrying = null;
            }

        }

        if (Time.time - timer > 2f)
            pickingUp = false;

        if (isCarrying == true)
        {
            carrying.transform.position = new Vector3 (pickup.transform.position.x, pickup.transform.position.y + 0.5f, pickup.transform.position.z);
        }

        //*********
        //Attacking
        //*********
        if (isCarrying == false)
        {
            if (tearLv >= 1)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (isAttacking == false)
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

        if (stars < 10)
        {
            starLv = 0;
            maxSpeed = 5f;
            rotationSpeed = 1f;
        }
        else if (stars >= 10 && stars < 20)
        {
            starLv = 1;
            maxSpeed = 6f;
            rotationSpeed = 1.2f;
        }
        else if (stars >= 20 && stars < 30)
        {
            starLv = 2;
            maxSpeed = 7f;
            rotationSpeed = 1.4f;
        }
        else if (stars >= 30 && stars < 40)
        {
            starLv = 3;
            maxSpeed = 8f;
            rotationSpeed = 1.6f;
        }

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

        if (tearLv + starLv == 1)
        {
            if (size < 2)
            {
                //Growing animation
                size = 2;
                transform.localScale = new Vector3(1.2f, 3.6f, 1.2f);
            }
        }

        if (tearLv + starLv == 2)
        {
            if (size < 3)
            {
                //Growing animation
                size = 3;
                transform.localScale = new Vector3(1.4f, 4.2f, 1.4f);
            }
        }

        if (tearLv + starLv == 3)
        {
            if (size < 4)
            {
                //Growing animation
                size = 4;
                transform.localScale = new Vector3(1.6f, 4.8f, 1.4f);
            }
        }
    }

    private void ReturnPickUp(GameObject obj)
    {
        if (transform.root != transform)
            carrying = obj.transform.parent.gameObject;
        else
            carrying = obj;

        obj.SendMessage("GetWeight", gameObject, SendMessageOptions.DontRequireReceiver);
        //print("Get Weight");
    }

    private void ReturnWeight(int weight)
    {
        if (weight <= size)
        {
            isCarrying = true;
            carrying.transform.position = pickup.transform.position;
        }
    }

    private void AttackFinished()
    {
        isAttacking = false;
    }

    public void starTear(bool isStar)
    {
        if (isStar == true)
            stars++;
        else
            tears++;
    }

}

