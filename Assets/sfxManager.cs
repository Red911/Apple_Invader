using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    [SerializeField] public AudioClip[] sfx;

    [SerializeField] private GameObject audioShot;

    public void playSound(int clipToPlay, float volume)
    {
        if (!SetShader.spawnSfx) return;

        audioShot.gameObject.GetComponent<AudioSource>().clip = sfx[clipToPlay];
        audioShot.gameObject.GetComponent<AudioSource>().volume = volume;
        GameObject clip = Instantiate(audioShot);
    }
}
