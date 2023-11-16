using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject peanut;
    [SerializeField] private GameObject shootParticles;
    [SerializeField] private GameObject missileParticles;
    [SerializeField] private GameObject endMask;
    [SerializeField] private GameObject youdied;
    [SerializeField] private GameObject youwon;
    [SerializeField] private float speed = 20;

    [SerializeField] private float timerShootMax = .8f;
    private float timerShoot;
    private bool canShoot = true;
    private bool canMove = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && canShoot) Shoot();
        if (Input.GetButtonDown("Cancel"))
            SceneManager.LoadScene("SampleScene");

        if (canMove)
        {
            float move = Input.GetAxis("Horizontal");

            rb.velocity = new Vector2(move * (speed * 100) * Time.deltaTime, 0);
        }
        else
            rb.velocity = new Vector2(0, 0);

        if (timerShoot > 0) timerShoot = Mathf.Clamp(timerShoot - Time.deltaTime, 0f, timerShootMax);

        if (LeGang.invaderCount <= 0)
        {
            canMove = false;
            canShoot = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.layer == 7)
        {
            canShoot = false;
            canMove = false;
            GameObject newMask = Instantiate(endMask, transform);
            youdied.SetActive(false);
            youwon.SetActive(true);
            Destroy(GetComponent<CapsuleCollider2D>());
        }
    }

    void Shoot()
    {
        if (timerShoot > 0) return;
        timerShoot = timerShootMax;

        GameObject p = Instantiate(peanut, shootPoint);

        if (SetShader.spawnParticles)
        {
            GameObject shootPart = Instantiate(shootParticles);
            shootPart.transform.position = shootPoint.transform.position;
            GameObject missilePart = Instantiate(missileParticles);
            missilePart.GetComponent<missileParticles>().missile = p;
        }
    }
}
