using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glitchMat : MonoBehaviour
{
    public Material mat;
    public AudioSource whiteNoise;
    public AudioSource alienDeplacement;
    public sfxManager sfxManager;

    private float multipier = 0.0f;
    static public bool stopGlitch = false;


    private void Start()
    {
        stopGlitch = false;
    }

    void Update()
    {
        if (LeGang.invaderCount > 10)
        {
            multipier = 0.0f;
            whiteNoise.volume = 0.0f;
            PlayerMove.shootSound = 7;
            Peanut.hitSound = 6;
            alienDeplacement.clip = sfxManager.sfx[5];
            if (!alienDeplacement.isPlaying && SetShader.spawnSfx)
                alienDeplacement.Play();
        }
        else if (LeGang.invaderCount > 8)
        {
            multipier = 0.7f;
            whiteNoise.volume = 0.3f;
            PlayerMove.shootSound = 7;
            Peanut.hitSound = 6;
            alienDeplacement.clip = sfxManager.sfx[5];
            if (!alienDeplacement.isPlaying && SetShader.spawnSfx)
                alienDeplacement.Play();
        }
        else if (LeGang.invaderCount > 4)
        {
            multipier = 2.0f;
            whiteNoise.volume = 0.6f;
            PlayerMove.shootSound = 12;
            Peanut.hitSound = 11;
            alienDeplacement.clip = sfxManager.sfx[10];
            if (!alienDeplacement.isPlaying && SetShader.spawnSfx)
                alienDeplacement.Play();
        }
        else if (LeGang.invaderCount > 0)
        {
            multipier = 7.0f;
            whiteNoise.volume = 1.0f;
            PlayerMove.shootSound = 2;
            Peanut.hitSound = 1;
            alienDeplacement.clip = sfxManager.sfx[0];
            alienDeplacement.volume = 0.6f;
            if (!alienDeplacement.isPlaying && SetShader.spawnSfx)
                alienDeplacement.Play();
        }
        else
        {
            multipier = 0.0f;
            whiteNoise.volume = 0.0f;
            alienDeplacement.volume = 0.0f;
        }

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
