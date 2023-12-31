using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Peanut : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Transform playerTf;
    [SerializeField] private float speed = 10;
    [SerializeField] private float lifetime = 3;
    [SerializeField] private GameObject tumorMask;
    [SerializeField] private GameObject endMask;
    [SerializeField] private GameObject souvenir01;
    [SerializeField] private GameObject souvenir02;
    [SerializeField] private GameObject souvenir03;
    public Animator camAnimator;
    public float maxMaskSize = 15.0f;
    public float minMaskSize = 10.0f;

    private sfxManager sfxManager;
    static public int hitSound = 6;

    private Animator mainLineAnimator;
    private Animator[] secondLinesAnimator = new Animator[4];

    public Transform PlayerTf { get => playerTf; set => playerTf = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
        sfxManager = GameObject.Find("AudioManager").GetComponent<sfxManager>();
        if (GameObject.Find("MainLine") != null)
            mainLineAnimator = GameObject.Find("MainLine").GetComponent<Animator>();

        int i = 0;
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "SecondLine")
            {
                secondLinesAnimator[i] = gameObj.GetComponent<Animator>();
                i++;
            }
        }

        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        Vector2 shootDir = Vector2.up;
        //rb.velocity = new Vector2(0, (speed * 100) * Time.fixedDeltaTime);
        rb.velocity = shootDir * speed * 100 * Time.deltaTime;
    }

    private void OnTriggerEnter2D (Collider2D c)
    {
        if (c.gameObject.layer == 7)
        {
            if (LeGang.invaderCount > 1)
            {
                if (SetShader.spawnMask)
                {
                    GameObject newMask = Instantiate(tumorMask);
                    newMask.transform.position = c.gameObject.transform.position;
                    float x = maxMaskSize;
                    float m = (maxMaskSize - minMaskSize) / (1.0f - 14.0f);
                    float b = 14.0f - m * minMaskSize;
                    x = m * (float)LeGang.invaderCount + b;
                    newMask.transform.localScale = new Vector3(x, x, 1);
                }
                if (LeGang.invaderCount > 9)
                    camAnimator.SetTrigger("blur1");
                else if (LeGang.invaderCount > 4)
                    camAnimator.SetTrigger("blur2");
                else
                    camAnimator.SetTrigger("blur3");
            }
            else
            {
                GameObject newMask = Instantiate(endMask);
            }

            if (SetShader.spawnSouvenirs)
            {
                if (LeGang.invaderCount == 11)
                {
                    GameObject souvenirInst = Instantiate(souvenir01);
                }
                else if (LeGang.invaderCount == 8)
                {
                    GameObject souvenirInst = Instantiate(souvenir02);
                    if (mainLineAnimator != null)
                        mainLineAnimator.SetTrigger("mvmt");
                   for (int i = 0; i < secondLinesAnimator.Length; i++)
                    {
                        if (secondLinesAnimator[i] != null)
                            secondLinesAnimator[i].SetTrigger("mvmt");
                   }
                }
                else if (LeGang.invaderCount == 4)
                {
                    GameObject souvenirInst = Instantiate(souvenir03);
                    if (mainLineAnimator != null)
                        mainLineAnimator.SetTrigger("brutal");
                    for (int i = 0; i < secondLinesAnimator.Length; i++)
                    {
                        if (secondLinesAnimator[i] != null)
                            secondLinesAnimator[i].SetTrigger("brutal");
                    }
                }
                else if (LeGang.invaderCount == 2)
                {
                    if (mainLineAnimator != null)
                        mainLineAnimator.SetTrigger("break");

                }
            }


            sfxManager.playSound(hitSound, 1.0f);
            c.gameObject.GetComponent<InvaderCollision>().GetDestroyed();
            Destroy(gameObject);
        }
    }
}
