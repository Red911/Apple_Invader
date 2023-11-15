using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Peanut : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10;
    [SerializeField] private float lifetime = 3;
    [SerializeField] private GameObject tumorMask;
    [SerializeField] private GameObject endMask;
    public float maxMaskSize = 15.0f;
    public float minMaskSize = 10.0f;

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
            if (LeGang.invaderCount > 1)
            {
                GameObject newMask = Instantiate(tumorMask);
                newMask.transform.position = c.gameObject.transform.position;
                float x = maxMaskSize;
                float m = (maxMaskSize - minMaskSize) / (1.0f - 14.0f);
                float b = 14.0f - m * minMaskSize;
                x = m * (float)LeGang.invaderCount + b;
                Debug.Log(x);
                newMask.transform.localScale = new Vector3(x, x, 1);
            }
            else
            {
                GameObject newMask = Instantiate(endMask);
                newMask.transform.position = c.gameObject.transform.position;
            }
            c.gameObject.GetComponent<InvaderCollision>().GetDestroyed();
            Destroy(gameObject);
        }
    }
}
