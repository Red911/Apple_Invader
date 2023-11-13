using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Peanut : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10;
    [SerializeField] private float lifetime = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, (speed * 100) * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D (Collider2D c)
    {
        if (c.gameObject.layer == 7)
        {
            c.gameObject.GetComponent<InvaderCollision>().GetDestroyed();
            Destroy(gameObject);
        }
    }
}
