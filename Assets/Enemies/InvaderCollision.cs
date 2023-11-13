using System.Collections;
using System.Collections.Generic;
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
            leGang.GoingRight = !leGang.GoingRight;
            leGang.GoDown();
        }
    }
}
