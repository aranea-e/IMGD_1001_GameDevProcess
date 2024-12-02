using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    private readonly List<CentipedeSegment> segments = new List<CentipedeSegment>();

    [Header("Prefabs")]
    public CentipedeSegment segmentPrefab;

    [Header("Sprites")]
    public Sprite headSprite;
    public Sprite bodySprite;

    [Header("Movement")]
    public LayerMask collisionMask = ~0;
    public Collider2D homeArea;
    public float speed = 20f;
    public int size = 12;

    [Header("Scoring")]
    public int pointsHead = 100;
    public int pointsBody = 10;

    private void OnEnable()
    {
        foreach (CentipedeSegment segment in segments) {
            segment.enabled = true;
        }
    }

    private void OnDisable()
    {
        foreach (CentipedeSegment segment in segments) {
            segment.enabled = true;
        }
    }

    public void Remove(CentipedeSegment segment)
    {
        int points = segment.isHead ? pointsHead : pointsBody;
        GameManager.Instance.IncreaseScore(points);

        GameManager.Instance.SpawnMushroom(segment.transform.position);

        if (segment.ahead != null) {
            segment.ahead.behind = null;
        }

        if (segment.behind != null)
        {
            segment.behind.ahead = null;
            segment.behind.UpdateHeadSegment();
        }

        segments.Remove(segment);
        Destroy(segment.gameObject);

        if (segments.Count == 0) {
            GameManager.Instance.NextLevel();
        }
    }

    [ContextMenu("kill")]
    public void KillCentipede() {
        foreach (CentipedeSegment seg in FindObjectsOfType<CentipedeSegment>())
        {
            Remove(seg, false);
        }
    }
    public void Remove(CentipedeSegment segment, bool score)
    {
        if (score)
        {
            int points = segment.isHead ? pointsHead : pointsBody;
            GameManager.Instance.IncreaseScore(points);
        }
        

        GameManager.Instance.SpawnMushroom(segment.transform.position);

        if (segment.ahead != null) {
            segment.ahead.behind = null;
        }

        if (segment.behind != null)
        {
            segment.behind.ahead = null;
            segment.behind.UpdateHeadSegment();
        }

        segments.Remove(segment);
        Destroy(segment.gameObject);

        if (segments.Count == 0) {
            GameManager.Instance.NextLevel();
        }
    }

    public void Respawn()
    {
        speed = 20f;
        for (int i = 1; i < GameManager.Instance.round; i++)
        {
            speed *= 1.1f;
        }

        foreach (CentipedeSegment segment in segments) {
            Destroy(segment.gameObject);
        }

        segments.Clear();

        for (int i = 0; i < size; i++)
        {
            Vector2 position = Util.GridPosition(transform.position) + (Vector2.left * i);
            CentipedeSegment segment = Instantiate(segmentPrefab, position, Quaternion.identity, transform);
            segment.spriteRenderer.sprite = i == 0 ? headSprite : bodySprite;
            segment.centipede = this;
            segment.enabled = enabled;
            segments.Add(segment);
        }

        for (int i = 0; i < segments.Count; i++)
        {
            CentipedeSegment segment = segments[i];
            segment.ahead = GetSegmentAt(i-1);
            segment.behind = GetSegmentAt(i+1);
        }

        enabled = true;
    }

    private CentipedeSegment GetSegmentAt(int index)
    {
        if (index >= 0 && index < segments.Count) {
            return segments[index];
        } else {
            return null;
        }
    }

    

}
