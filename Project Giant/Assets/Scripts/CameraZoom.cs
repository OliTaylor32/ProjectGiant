using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour

{
    public GameObject giant;
    public Transform centerPoint;
    public float mouseX;
    public float mouseY;
    public float zoom;
    public float zoomSpeed;
    public float mouseSensitivity;
    // Start is called before the first frame update
    void Start()
    {
        zoom = -4f;
        zoomSpeed = 1f;
        mouseSensitivity = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        zoom = Mathf.Clamp(zoom, -6, -3);
        //transform.position = new Vector3 (transform.position.x, transform.position.y, zoom);


        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity;
        mouseY = Mathf.Clamp(mouseY, -10, 60);

        transform.position = new Vector3(0, 0, zoom);
        transform.rotation = new Quaternion (mouseX, mouseY, 0, 0);
        transform.LookAt(centerPoint);
    }
}
