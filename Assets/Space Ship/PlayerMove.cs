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
    [SerializeField] private sfxManager sfxManager;
    [SerializeField] private AudioSource music;
    [SerializeField] private float speed = 20;

    [SerializeField] private float timerShootMax = .8f;
    private float timerShoot;
    private bool canShoot = true;
    private bool canMove = true;

    static public int shootSound = 7;

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

        if (LeGang.invaderCount == 0)
        {
            canMove = false;
            canShoot = false;
            music.Stop();
            glitchMat.stopGlitch = true;
            sfxManager.playSound(9, 1.0f);
            LeGang.invaderCount--;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.layer == 7)
        {
            canShoot = false;
            canMove = false;
            GameObject newMask = Instantiate(endMask);
            Destroy(GetComponent<CapsuleCollider2D>());
            music.Stop();
            glitchMat.stopGlitch = true;
            sfxManager.playSound(9, 1.0f);
        }
    }

    void Shoot()
    {
        if (timerShoot > 0) return;
        timerShoot = timerShootMax;

        GameObject p = Instantiate(peanut, shootPoint);

        if (SetShader.spawnSfx) sfxManager.playSound(shootSound, 1.0f);

        if (SetShader.spawnParticles)
        {
            GameObject shootPart = Instantiate(shootParticles);
            shootPart.transform.position = shootPoint.transform.position;
            GameObject missilePart = Instantiate(missileParticles);
            missilePart.GetComponent<missileParticles>().missile = p;
        }
    }
}
