using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] componentsToDisable;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
               componentsToDisable[i].enabled = false;

            }
        }
        else{
            Camera.main.gameObject.SetActive(false);
        }
    }

}
