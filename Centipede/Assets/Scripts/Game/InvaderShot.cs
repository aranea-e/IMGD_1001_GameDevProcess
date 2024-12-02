using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderShot : MonoBehaviour
{
    float speed;
    private Rigidbody2D rb;
    
    public void StartWithFields(float speed)
    {
        this.speed = speed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb.isKinematic) return;

        Vector2 position = rb.position;
        position += speed * Time.fixedDeltaTime * Vector2.down;
        rb.MovePosition(position);

        if (transform.position.y <= -25.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && FindObjectOfType<Centipede>().enabled)
        {
            Audio.play("death");
            FindObjectOfType<Centipede>().enabled = false;
            GameManager.Instance.ResetRound();
        }
    }
}
