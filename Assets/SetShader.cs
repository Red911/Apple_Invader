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


    [Header("Mask")]
    [SerializeField] private KeyCode maskDesactivate;
    static public bool spawnMask  = true;

    [Header("Particles")]
    [SerializeField] private KeyCode particlesDesactivate;
    static public bool spawnParticles = true;

    [Header("Vignette")]
    [SerializeField] private KeyCode vignetteDesactivate;
    [SerializeField] private GameObject vignette;

    private void Awake()
    {
        depthOfField = postProcess.profile.components[0];
        AbeChromatique = postProcess.profile.components[1];
        spawnParticles = true;
        spawnMask = true;
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
    }

   
}
