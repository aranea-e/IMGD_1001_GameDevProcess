using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POWInstance : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Dart")) {
            Audio.play("hit-p");
            FindObjectOfType<POW>().destroyPOW(this, 1);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyDart")) {
            Audio.play("hit-p");
            Destroy(collision.gameObject);
            FindObjectOfType<POW>().destroyPOW(this, 1);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("POW")) {
            FindObjectOfType<POW>().destroyPOW(this, 1);
        }
    }
}
