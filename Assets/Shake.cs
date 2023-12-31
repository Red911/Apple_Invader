using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private bool startShake;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration = 1f;
    private bool canShake;

    public bool CanShake { get => canShake; set => canShake = value; }

    private void Start()
    {
        canShake = true;
    }

    private void Update()
    {
        if (startShake)
        {
            startShake = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while ( elapsedTime < duration )
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/ duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPos;
    }

    public void StartShake(float d)
    {
        if (!canShake) return;
        startShake = true;
        duration = d;
    }
}
