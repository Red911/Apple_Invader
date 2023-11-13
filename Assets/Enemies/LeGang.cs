using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeGang : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;
    [SerializeField] private float speedX = 10;
    [SerializeField] private float movementDownDistance = 20;
    private bool goingRight;

    public bool GoingRight { get => goingRight; set => goingRight = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        GoingRight = true;
    }

    void Update()
    {
        float move = GoingRight ? 1 : -1;
        rb.velocity = new Vector2(move * speedX * 100 * Time.deltaTime, 0);
    }

    public void GoDown()
    {
        tr.Translate(new Vector3(0,- movementDownDistance, 0));
    }
}
