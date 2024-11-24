using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitInstance : MonoBehaviour
{
    [SerializeField] SpriteRenderer s;
    [SerializeField] Sprite[] sprites;

    public void UpdateSprite(int i) {
        s.sprite = sprites[i];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Dart")) {
            Audio.play("hit-f");
            FindObjectOfType<Fruit>().destroyFruit(this, 1);
        }
    }
}
