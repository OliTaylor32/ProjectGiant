using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour

{

    public Transform camera, player, centerPoint;
    public float mouseX, mouseY;
    public float mouseSpeed = 100f;
    private float moveForward, moveSide;
    public float moveSpeed = 2f;
    public float zoom;
    private float zoomSpeed = 2f;
    public float rotationSpeed = 5f;
    public int size;

    //Picking up Variables
    public GameObject pickup;
    public bool isCarrying = false;
    public GameObject carrying;

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
            #pragma warning restore CS0618 // Type or member is obsolete
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

        Vector3 movement = new Vector3(moveSide, 0, moveForward);
        movement = player.rotation * movement;
        player.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
        centerPoint.position = new Vector3(player.position.x, (player.position.y + 1), player.position.z);
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
            player.rotation = Quaternion.Slerp(player.rotation, turnAngle, Time.deltaTime * rotationSpeed);
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
            }

            else
            {
                isCarrying = false;
                //print("Drop item");
                carrying = null;
            }

        }

        if (isCarrying == true)
        {
            carrying.transform.position = pickup.transform.position;
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

}

