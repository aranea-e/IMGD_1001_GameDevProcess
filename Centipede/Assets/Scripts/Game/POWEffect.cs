using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POWEffect : MonoBehaviour
{
    void Update()
    {
        if (!GetComponent<SpriteRenderer>().enabled)
        {
            Destroy(gameObject);
        }
    }
}
