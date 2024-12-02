using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Sprite[] states;
    private SpriteRenderer spriteRenderer;
    public int points = 1;
    private int health;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = states.Length;
    }

    public void Damage(int amount)
    {
        health -= amount;

        if (health > 0)
        {
            spriteRenderer.sprite = states[states.Length - health];
        }
        else
        {
            GameManager.Instance.IncreaseScore(points);
            Destroy(gameObject);
        }
    }

    public void Damage(int amount, bool score)
    {
        health -= amount;

        if (health > 0)
        {
            spriteRenderer.sprite = states[states.Length - health];
        }
        else
        {
            if (score)
            {
                GameManager.Instance.IncreaseScore(points);
            }
            
            Destroy(gameObject);
        }
    }

    public void Heal()
    {
        health = states.Length;
        spriteRenderer.sprite = states[0];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Dart")) {
            Audio.play("hit-m");
            Damage(1);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("POW")) {
            Damage(10);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyDart")) {
            Audio.play("hit-m");
            Destroy(collision.gameObject);
            Damage(1, false);
        }
    }

}
