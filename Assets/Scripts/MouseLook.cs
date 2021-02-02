using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity;
    public Transform body;
    public Transform nose;
    public Transform me;


    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationX = mouseX * sensitivity * Time.deltaTime;
        float rotationY = mouseY * sensitivity * Time.deltaTime;

        Vector3 rotationBody = body.transform.rotation.eulerAngles;
        Vector3 rotationFace = nose.rotation.eulerAngles;

        rotationFace.x -= rotationY;
        rotationFace.z = 0f;
        rotationBody.y += rotationX;

        //me.rotation = nose.rotation;
        body.rotation = Quaternion.Euler(rotationBody);
        nose.rotation = Quaternion.Euler(rotationFace);
    }
}
