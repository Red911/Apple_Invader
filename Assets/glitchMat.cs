using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glitchMat : MonoBehaviour
{
    public Material mat;
    private float multipier = 0.0f;
    static public bool stopGlitch = false;

    private void Start()
    {
        stopGlitch = false;
    }

    void Update()
    {
        if (LeGang.invaderCount > 10)
            multipier = 0.0f;
        else if (LeGang.invaderCount > 7)
            multipier = 0.7f;
        else if (LeGang.invaderCount > 4)
            multipier = 2.0f;
        else
            multipier = 7.0f;

        if (SetShader.glitchEnabled && !stopGlitch)
        {
            mat.SetFloat("_GlitchStrength", multipier * (13 - LeGang.invaderCount));
            mat.SetFloat("_NoiseAmount", multipier * (13 - LeGang.invaderCount));
            mat.SetFloat("_ScanLinesStrengh", 0.07f * LeGang.invaderCount);
        }
        else
        {
            mat.SetFloat("_GlitchStrength", 0);
            mat.SetFloat("_NoiseAmount", 0);
            mat.SetFloat("_ScanLinesStrengh", 1);
        }
    }
}
