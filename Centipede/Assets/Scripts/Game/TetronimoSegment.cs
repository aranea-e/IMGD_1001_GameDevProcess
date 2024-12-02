using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetronimoSegment : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    public Tetronimo t;

    public LayerMask collisionMask;

    float speed;

    [SerializeField] Sprite[] sprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        t = FindObjectOfType<Tetronimo>();
        
        switch (t.currentType)
        {
            case "z":
            spriteRenderer.sprite = sprites[0];
            break;

            case "l":
            spriteRenderer.sprite = sprites[1];
            break;

            case "o":
            spriteRenderer.sprite = sprites[2];
            break;

            case "s":
            spriteRenderer.sprite = sprites[3];
            break;

            case "i":
            spriteRenderer.sprite = sprites[4];
            break;

            case "j":
            spriteRenderer.sprite = sprites[5];
            break;

            case "t":
            spriteRenderer.sprite = sprites[6];
            break;
        }

        speed = 7f;
        for (int i = 1; i < GameManager.Instance.round; i++)
        {
            speed *= 1.1f;
        }
    }

    private void OnEnable()
    {
        boxCollider.enabled = true;
    }

    private void OnDisable()
    {
        boxCollider.enabled = false;
    }

    void Update() {
        Vector2 playerDirection = PlayerInputs.direction;
        if (playerDirection.magnitude > 1.0f)
        {
            playerDirection = playerDirection.normalized;
        }
        Vector2 targetPosition = new Vector2(transform.position.x + (((speed / 2) * playerDirection.x) * Time.deltaTime),
                                             transform.position.y - (speed * Time.deltaTime));
        if (Physics2D.OverlapBox(targetPosition, Vector2.zero, 0f, collisionMask) || targetPosition.y < -25)
        {
            if (Physics2D.OverlapBox(targetPosition, Vector2.zero, 0f, collisionMask))
            {
                Audio.play("land");
            }
            t.destroyTetronimo();
            return;
        }
        transform.position = targetPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Audio.play("death");
            t.destroyTetronimo(2);
            GameManager.Instance.ResetRound();
        }
    
        if (collision.gameObject.layer == LayerMask.NameToLayer("Dart"))
        {
            Audio.play("hit-c");
            t.destroyTetronimo(1);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("POW"))
        {
            t.destroyTetronimo(3);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyDart"))
        {
            Audio.play("hit-c");
            Destroy(collision.gameObject);
            t.destroyTetronimo(0);
        }
    }
}
