using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetShader : MonoBehaviour
{
    [SerializeField] private Volume postProcess;
    
    
    [Header("Blur")] 
    [SerializeField] private KeyCode blurDesactivate;
    private VolumeComponent depthOfField;
    
    [Header("Aberation Chromatique")] 
    [SerializeField] private KeyCode chromaDesactivate;
    private VolumeComponent AbeChromatique;
    
    [Header("Background")] 
    [SerializeField] private KeyCode backGroundDesactivate;
    [SerializeField] private GameObject[] backgroundObjects;
    [SerializeField] private Animator mainline;
    [SerializeField] private Animator[] secondlines = new Animator[4];

    [Header("Mask")]
    [SerializeField] private KeyCode maskDesactivate;
    static public bool spawnMask  = true;

    [Header("Particles")]
    [SerializeField] private KeyCode particlesDesactivate;
    static public bool spawnParticles = true;

    [Header("Vignette")]
    [SerializeField] private KeyCode vignetteDesactivate;
    [SerializeField] private GameObject vignette;

    [Header("Glitch")]
    [SerializeField] private KeyCode glitchDesactivate;
    static public bool glitchEnabled = true;

    [Header("sfx")]
    [SerializeField] private KeyCode sfxDesactivate;
    [SerializeField] private GameObject whiteNoise;
    [SerializeField] private GameObject deplacementSound;
    static public bool spawnSfx = true;

    [Header("music")]
    [SerializeField] private KeyCode musicDesactivate;
    [SerializeField] public AudioSource music;

    [Header("souvenirs")]
    [SerializeField] private KeyCode souvenirsDesactivate;
    static public bool spawnSouvenirs = true;

    [Header("shake")]
    [SerializeField] private KeyCode shakeDesactivate;
    [SerializeField] private Shake shake;

    [Header("anim vaisseau")]
    [SerializeField] private KeyCode animVaisseauDesactivate;
    [SerializeField] private PlayerMove vaisseau;

    private void Awake()
    {
        depthOfField = postProcess.profile.components[0];
        AbeChromatique = postProcess.profile.components[1];
        spawnParticles = true;
        spawnMask = true;
        glitchEnabled = true;
        spawnSfx = true;
        spawnSouvenirs = true;
        music.volume = 1.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(blurDesactivate))
        {
            depthOfField.active = !depthOfField.active;
        }
        
        if (Input.GetKeyDown(chromaDesactivate))
        {
            AbeChromatique.active = !AbeChromatique.active;
        }

        if (Input.GetKeyDown(backGroundDesactivate))
        {
            for (int i = 0; i < backgroundObjects.Length; i++)
            {
                backgroundObjects[i].SetActive(!backgroundObjects[i].activeInHierarchy);
            }

            if (LeGang.invaderCount <= 7 && LeGang.invaderCount > 3)
            {
                if (mainline != null)
                    mainline.SetTrigger("mvmt");
                for (int i = 0; i < secondlines.Length; i++)
                {
                    if (secondlines[i] != null)
                        secondlines[i].SetTrigger("mvmt");
                }
            }
            else if (LeGang.invaderCount <= 3)
            {
                if (mainline != null)
                    mainline.SetTrigger("brutal");
                for (int i = 0; i < secondlines.Length; i++)
                {
                    if (secondlines[i] != null)
                        secondlines[i].SetTrigger("brutal");
                }
            }
            
            if (LeGang.invaderCount <= 1)
            {
                if (mainline != null)
                    mainline.SetTrigger("break");

            }
        }

        if (Input.GetKeyDown(maskDesactivate))
        {
            spawnMask = !spawnMask;
            foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (gameObj.name == "MaskTumor(Clone)")
                {
                    Destroy(gameObj);
                }
            }
        }

        if (Input.GetKeyDown(particlesDesactivate))
        {
            spawnParticles = !spawnParticles;
            foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (gameObj.name == "missleParticles(Clone)" || gameObj.name == "ShootParticles(Clone)")
                {
                    Destroy(gameObj);
                }
            }
        }

        if (Input.GetKeyDown(vignetteDesactivate))
        {
            vignette.SetActive(!vignette.activeInHierarchy);
        }

        if (Input.GetKeyDown(glitchDesactivate))
        {
            glitchEnabled = !glitchEnabled;
        }

        if(Input.GetKeyDown(sfxDesactivate))
        {
            spawnSfx = !spawnSfx;
            whiteNoise.SetActive(!whiteNoise.activeInHierarchy);
            if (whiteNoise.activeInHierarchy)
                whiteNoise.gameObject.GetComponent<AudioSource>().Play();
            deplacementSound.SetActive(!deplacementSound.activeInHierarchy);
            if (deplacementSound.activeInHierarchy)
                deplacementSound.gameObject.GetComponent<AudioSource>().Play();

            if (!spawnSfx)
            {
                foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
                {
                    if (gameObj.name == "AudioShot(Clone)")
                    {
                        Destroy(gameObj);
                    }
                }
            }
        }

        if (Input.GetKeyDown(musicDesactivate))
        {
            if (music.volume == 1.0f)
                music.volume = 0.0f;
            else
                music.volume = 1.0f;
        }

        if (Input.GetKeyDown(souvenirsDesactivate))
        {
            spawnSouvenirs = !spawnSouvenirs;
            foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (gameObj.name == "Souvenir01(Clone)" || gameObj.name == "Souvenir02(Clone)" || gameObj.name == "Souvenir03(Clone)")
                {
                    Destroy(gameObj);
                }
            }
        }

        if (Input.GetKeyDown(shakeDesactivate))
        {
            shake.CanShake = !shake.CanShake;
        }

        if (Input.GetKeyDown(animVaisseauDesactivate))
        {
            vaisseau.UseAnimations = !vaisseau.UseAnimations;
        }
    }

   
}
