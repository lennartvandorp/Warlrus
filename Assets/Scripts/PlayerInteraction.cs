using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    RaycastHit hitInfo;

    public float reach;

    public KeyCode pickUpA;//a button used to pick up items
    public KeyCode pickUpB;//another butten used for the same
    public float throwForce;

    public Transform pickUpPoint;
    public bool isCarryingItem;
    public float restrictViewWhenPickedUp;

    public Transform weaponPoint;
    public GameObject itemPickedUp;

    FirstPersonAIO firstPersonController;
    Vector3 laspos;

    Rigidbody body;

    ItemData currentItemData;

    // Start is called before the first frame update
    void Start()
    {
        firstPersonController = GetComponentInParent<FirstPersonAIO>();
        body = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        #region pick up and drop items
        //raycast for picking up items
        Physics.Raycast(transform.position, transform.forward, out hitInfo, reach);

        if (!isCarryingItem)
        {
            if (hitInfo.collider != null && hitInfo.collider.GetComponent<ItemData>() != null)
            //checks if the item selected is an item rather than something else
            {
                if (hitInfo.collider.GetComponent<ItemData>().canBePickedUp)
                {//checks if the item can be picked up
                    if (Input.GetKeyDown(pickUpA) || Input.GetKeyDown(pickUpB))
                    {
                        hitInfo.collider.GetComponent<ItemData>().PickUp(pickUpPoint, weaponPoint);
                        isCarryingItem = true;
                        firstPersonController.verticalRotationRange = restrictViewWhenPickedUp;
                        itemPickedUp = hitInfo.collider.gameObject;
                        currentItemData = itemPickedUp.GetComponent<ItemData>();
                    }
                }
            }
        }
        else{
            if(Input.GetKeyDown(pickUpA) || Input.GetKeyDown(pickUpB))
            {
                itemPickedUp.GetComponent<ItemData>().Throw(transform.forward * throwForce, body.velocity);
                isCarryingItem = false;
                firstPersonController.verticalRotationRange= 170;
                currentItemData = null;
                itemPickedUp = null;
            }
        }
        #endregion



        #region shoot

        if (itemPickedUp != null && itemPickedUp.GetComponent<ItemData>().isWeapon && Input.GetKeyDown(KeyCode.Mouse0)) {
            itemPickedUp.GetComponent<Weapon>().Shoot();
            body.AddForce(-transform.forward * itemPickedUp.GetComponent<Weapon>().knockBack);

        }

        #endregion
    }
    public bool PickedUpWeapon() {
        if (currentItemData.isWeapon)
        {
            return true;
        }
        else return false;
    }
}
