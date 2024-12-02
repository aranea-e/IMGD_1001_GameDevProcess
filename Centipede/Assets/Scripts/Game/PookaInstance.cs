using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PookaInstance : MonoBehaviour
{
    float speed;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] LayerMask invulnMask;
    bool invuln;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite[] sprites;

    void Start() {
        speed = FindObjectOfType<Centipede>().speed / 4;
    }
    void Update() {
        if (FindObjectOfType<Blaster>() != null)
        {
           float speed = this.speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, 
            FindObjectOfType<Blaster>().transform.position, 
            speed); 
        }

        if (Physics2D.OverlapBox(transform.position, Vector2.one / 2, 0f, collisionMask))
        {
            Audio.play("hit-c");
            FindObjectOfType<Pooka>().destroyPooka(0);
        }

        invuln = Physics2D.OverlapBox(transform.position, Vector2.one / 2, 0f, invulnMask);
        if (invuln)
        {
            sr.sprite = sprites[1];
        } else {
            sr.sprite = sprites[0];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Audio.play("death");
            FindObjectOfType<Centipede>().enabled = false;
            GameManager.Instance.ResetRound();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("POW"))
        {
            FindObjectOfType<Pooka>().destroyPooka(1);
        }
        
        if (invuln)
        {
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Dart"))
        {
            Audio.play("hit-c");
            FindObjectOfType<Pooka>().destroyPooka(1);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyDart") && collision.collider.enabled)
        {
            Audio.play("hit-c");
            Destroy(collision.gameObject);
            FindObjectOfType<Pooka>().destroyPooka(0);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
