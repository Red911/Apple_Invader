using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeGang : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;
    [SerializeField] private float speedX = 10;
    [SerializeField] private float movementDownDistance = 20;
    [SerializeField] private PlayerMove player;
    private bool goingRight;
    private float timerCollision;
    public static int invaderCount;

    public bool GoingRight { get => goingRight; set => goingRight = value; }
    public float TimerCollision { get => timerCollision; set => timerCollision = value; }
    public int InvaderCount { get => invaderCount; set => invaderCount = value; }
    public PlayerMove Player { get => player; set => player = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        GoingRight = true;

        invaderCount = GetComponentsInChildren<InvaderCollision>().Count();
    }

    private void FixedUpdate()
    {
        if (timerCollision > 0) timerCollision = Mathf.Clamp(timerCollision - Time.deltaTime, 0f, 1f);

        float move = GoingRight ? 1 : -1;
        rb.velocity = new Vector2(move * speedX * 25 * (14 - invaderCount) * Time.deltaTime, 0);
    }

    public void GoDown()
    {
        tr.Translate(new Vector3(0,- movementDownDistance, 0));
    }
}
