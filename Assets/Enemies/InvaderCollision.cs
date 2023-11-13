using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InvaderCollision : MonoBehaviour
{
    private LeGang leGang;


    void Start()
    {
        leGang = GetComponentInParent<LeGang>();
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 6)
        {
            if (leGang.TimerCollision > 0) return;
            leGang.TimerCollision = 1f;

            leGang.GoingRight = !leGang.GoingRight;
            leGang.GoDown();
        }
    }

    public void GetDestroyed()
    {
        leGang.InvaderCount--;
        Destroy(gameObject);
    }
}
