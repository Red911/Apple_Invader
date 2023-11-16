using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumorMask : MonoBehaviour
{
    public void destroyItself()
    {
        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
        else
            Destroy(gameObject);
    }
}
