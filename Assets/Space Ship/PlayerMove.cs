using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject peanut;
    [SerializeField] private float speed = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) Shoot();

        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * (speed*100) * Time.deltaTime, 0);
    }

    void Shoot()
    {
        GameObject p = Instantiate(peanut, shootPoint);
    }
}
