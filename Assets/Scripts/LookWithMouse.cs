using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWithMouse : MonoBehaviour
{
    float rotationOnX, rotationOnZ, sensitivity;
    PlayerInteraction interaction;
    public Transform player;
    public float carryingClamp;

    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 40f;
        Cursor.lockState = CursorLockMode.Locked;

        interaction = GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {

        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;


        rotationOnX -= mouseY;


        if (interaction.isCarryingItem && !interaction.PickedUpWeapon())
        {
            rotationOnX = Mathf.Clamp(rotationOnX, -carryingClamp, carryingClamp);
        }
        else
        {
            rotationOnX = Mathf.Clamp(rotationOnX, -90f, 90f);
        }


        //Implements the rotation
        transform.localEulerAngles = new Vector3(rotationOnX, 0, 0);
        player.Rotate(Vector3.up * mouseX);

    }
}
