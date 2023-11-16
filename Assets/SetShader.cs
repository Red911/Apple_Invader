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

    private void Awake()
    {
        depthOfField = postProcess.profile.components[0];
        AbeChromatique = postProcess.profile.components[1];
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
    }

   
}
