using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileParticles : MonoBehaviour
{
    public GameObject missile;

    void Update()
    {
        if (missile != null)
        {
            transform.position = missile.transform.position;
        }
        else
        {
            GetComponent<ParticleSystem>().Stop();
        }
    }
}
