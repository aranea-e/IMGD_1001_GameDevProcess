using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetronimoSegment : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    public Tetronimo t;

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
}
