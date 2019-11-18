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

        moveSide = Mathf.Clamp(moveSide, -3, 3);
        moveForward = Mathf.Clamp(moveForward, -3, 5);

        if (isAttacking == false)
        {

            Vector3 movement = new Vector3(moveSide, 0, moveForward);
            movement = player.rotation * movement;
            player.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
            centerPoint.position = new Vector3(player.position.x, (player.position.y + 1), player.position.z);
            if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            {
                Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
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
            else if (moveForward > moveSide)
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
            if (tears >= 5)
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

