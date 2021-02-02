using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{

    public bool canBePickedUp;
    public bool isWeapon;
    bool isPickedUp;
    Transform followThis;

    Rigidbody rigidbody;

    Vector3 lasPos, speed;

    Transform weaponPos;


    public float angularDragWhenPickedup, angularDragWhenIndependant;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp && !isWeapon)
        {
            lasPos = rigidbody.position;
            //this.transform.position = followThis.transform.position;
            rigidbody.MovePosition(followThis.transform.position);
        }
        else if (isPickedUp && isWeapon) {
            transform.position = weaponPos.position;
            transform.rotation = weaponPos.rotation;
        }
        

        speed = (rigidbody.position - lasPos) / (Time.deltaTime * 3f);

    }

    public void PickUp(Transform whereToGo, Transform weaponPos)
    {
        if (!isWeapon)
        {
            followThis = whereToGo;
        }
        if (isWeapon)
        {
            transform.SetParent(weaponPos);
            this.weaponPos = weaponPos;
            rigidbody.detectCollisions = false;
            rigidbody.freezeRotation = true;
        }
        isPickedUp = true;
        canBePickedUp = false;
        rigidbody.useGravity = false;
        rigidbody.angularDrag = angularDragWhenPickedup;
    }

    public void Throw(Vector3 force, Vector3 playerSpeed)
    {
        //rigidbody.velocity = speed;//Makes sure it keeps the speed it's currently traveling at
        rigidbody.velocity = playerSpeed;
        rigidbody.AddForce(force);//adds the force of the throw
        isPickedUp = false;//makes sure it's not picked up anymore
        canBePickedUp = true;//makes sure it's not picked up
        rigidbody.useGravity = true;//changes it 
        followThis = null;
        rigidbody.angularDrag = angularDragWhenIndependant;
        transform.SetParent(null);
        rigidbody.detectCollisions = true;
        rigidbody.freezeRotation = false;
    }



}
