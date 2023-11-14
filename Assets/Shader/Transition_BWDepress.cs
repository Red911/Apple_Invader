using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_BWDepress : MonoBehaviour
{
    [SerializeField] private AnimationCurve t;
    private Material mat;
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
