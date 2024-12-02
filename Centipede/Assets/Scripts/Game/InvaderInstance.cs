using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderInstance : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask mask;
    private Vector2 targetPosition;
    private Vector2 direction = Vector2.right + Vector2.down;
    [SerializeField] Collider2D col;
    private float fireTimer;
    private float fireMinTimer;
    [SerializeField] private float fireT;
    [SerializeField] private float fireMT;
    [SerializeField] private float fireChance;

    [SerializeField] InvaderShot shotPrefab;

    public bool super = false;

    void Start()
    {
        speed = FindObjectOfType<Centipede>().speed / 4.0f;
        SetNewTarget();

        fireMT = 5.0f;
        fireT = 1.0f;
        fireChance = 0.25f;

        for (int i = 5; i < GameManager.Instance.round; i++)
        {
            fireMT *= 0.90f;
            fireT *= 0.90f;
            fireChance *= 1.1f;
        }

        if (super)
        {
            fireMT = 0.0f;
            fireT = 0.25f;
            fireChance = 1.0f;
        }
    }

    void Update() {
        Move();
        CheckFire();
    }

    void CheckFire() {
        fireMinTimer += Time.deltaTime;
        if (fireMinTimer >= fireMT)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireT)
            {
                fireTimer = 0.0f;
                if (Random.Range(0.0f, 1.0f) < fireChance)
                {
                    Fire();
                    fireMinTimer = 0.0f;
                }
            }
        }
    }

    void Fire() {
        Audio.play("shoot");
        Instantiate(shotPrefab, transform.position - (new Vector3(0.0f, 0.5f, 0.0f)), Quaternion.identity).StartWithFields(speed * 6.0f);
    }

    void Move() {
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTarget();
        }

        Vector2 currentPosition = transform.position;
        float speed = this.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed);
    }

    void SetNewTarget() {
        Vector2 gridPosition = Util.GridPosition(transform.position);
        targetPosition = gridPosition;
        targetPosition.x += direction.x;

        if (checkOverlap())
        {
            direction.x = -direction.x;
            targetPosition.x = gridPosition.x;
            targetPosition.y = gridPosition.y + direction.y;

            Bounds homeBounds = FindObjectOfType<Centipede>().homeArea.bounds;

            if ((direction.y == 1f && targetPosition.y > homeBounds.max.y) ||
                (direction.y == -1f && targetPosition.y < homeBounds.min.y))
            {
                direction.y = -direction.y;
                targetPosition.y = gridPosition.y + direction.y;
            }
        }
    }

    bool checkOverlap() {
        Collider2D[] boxes = Physics2D.OverlapBoxAll(targetPosition, Vector2.zero, 0f, mask);
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i] != col)
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Audio.play("death");
            FindObjectOfType<Centipede>().enabled = false;
            GameManager.Instance.ResetRound();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Dart"))
        {
            Audio.play("hit-c");
            FindObjectOfType<Invader>().destroyInvader(1);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("POW"))
        {
            FindObjectOfType<Invader>().destroyInvader(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetPosition, Vector3.one);
    }
}
