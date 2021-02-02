using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float acceleration;
    public float jumpForce;
    public float horizontalDrag;

    

    public GameObject myCamera;

    private bool isTouchingGround;

    public KeyCode moveForward = KeyCode.W, moveLeft = KeyCode.A,
        moveRight = KeyCode.D, moveBack = KeyCode.S, jump = KeyCode.Space;

    private Rigidbody rigidbody;

    RaycastHit hitGround;
    public GameObject feet;
    public float touchGroundReach;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        #region HorizontalMovement
        Vector3 currentMoveVector = Vector3.zero;
        if (Input.GetKey(moveForward)) {
            currentMoveVector += transform.forward;
        }
        if (Input.GetKey(moveLeft))
        {
            currentMoveVector += -transform.right;
        }
        if (Input.GetKey(moveBack))
        {
            currentMoveVector += -transform.forward;
        }
        if (Input.GetKey(moveRight))
        {
            currentMoveVector += transform.right;
        }
        rigidbody.AddForce(currentMoveVector.normalized * acceleration * Time.deltaTime * 200);
        Vector3 currentHorizontalSpeed = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
        rigidbody.AddForce(-currentHorizontalSpeed * horizontalDrag * Time.deltaTime * 200);
        #endregion

        #region Jump
        Physics.Raycast(feet.transform.position,Vector3.down, out hitGround, touchGroundReach);

        if (hitGround.collider != null && hitGround.collider.gameObject.tag == "Jumpable")
        {
            isTouchingGround = true;
        }
        else isTouchingGround = false;

        if (Input.GetKeyDown(jump) && isTouchingGround)
        {
            rigidbody.AddForce(transform.up * jumpForce);
        }
        #endregion

    }
}
