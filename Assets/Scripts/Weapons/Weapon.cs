﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float knockBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Shoot() 
    {
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
    }
}
