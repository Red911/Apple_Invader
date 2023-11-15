using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject peanut;
    [SerializeField] private GameObject shootParticles;
    [SerializeField] private GameObject missileParticles;
    [SerializeField] private float speed = 20;

    [SerializeField] private float timerShootMax = .8f;
    private float timerShoot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) Shoot();

        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * (speed*100) * Time.deltaTime, 0);

        if (timerShoot > 0) timerShoot = Mathf.Clamp(timerShoot - Time.deltaTime, 0f, timerShootMax);
    }

    void Shoot()
    {
        if (timerShoot > 0) return;
        timerShoot = timerShootMax;

        GameObject p = Instantiate(peanut, shootPoint);
        GameObject shootPart = Instantiate(shootParticles);
        shootPart.transform.position = shootPoint.transform.position;
        GameObject missilePart = Instantiate(missileParticles);
        missilePart.GetComponent<missileParticles>().missile = p;
    }
}
