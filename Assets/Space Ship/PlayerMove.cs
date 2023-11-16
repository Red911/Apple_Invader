using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Transform tf;
    private Animator an;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject peanut;
    [SerializeField] private GameObject shootParticles;
    [SerializeField] private GameObject missileParticles;
    [SerializeField] private GameObject endMask;
    [SerializeField] private GameObject youdied;
    [SerializeField] private GameObject youwon;
    [SerializeField] private float speed = 20;
    [SerializeField] private float rotationOffset = 2f;

    [SerializeField] private float timerShootMax = .8f;
    private float timerShoot;
    private bool canShoot = true;
    private bool canMove = true;

    void Start()
    {
        tf = transform;
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && canShoot) Shoot();
        if (Input.GetButtonDown("Cancel")) SceneManager.LoadScene("SampleScene");

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
            if (endMask != null)
            {
                GameObject newMask = Instantiate(endMask, transform);
            }

            youdied.SetActive(false);
            youwon.SetActive(true);
            Destroy(GetComponent<CapsuleCollider2D>());
        }

        if (c.gameObject.layer == 6)
        {
            if (c.transform.position.x - tf.position.x > 0)
            {
                an.SetTrigger("Wall_ShakeR");
            }
            else
            {
                an.SetTrigger("Wall_ShakeL");
            }
        }
    }

    void Shoot()
    {
        if (timerShoot > 0) return;
        timerShoot = timerShootMax;

        GameObject p = Instantiate(peanut, shootPoint);
        p.GetComponent<Peanut>().PlayerTf = tf;
        if (shootParticles != null)
        {
            GameObject shootPart = Instantiate(shootParticles);
            shootPart.transform.position = shootPoint.transform.position;
        }
        if(missileParticles != null)
        {
            GameObject missilePart = Instantiate(missileParticles);
            missilePart.GetComponent<missileParticles>().missile = p;
        }

    }

    public void GetRotated()
    {
        tf.Rotate(Vector3.forward, 90f);
    }
}
