using System.Collections.Generic;
using UnityEngine;

public class OriginalCentipede : MonoBehaviour
{
    private readonly List<OriginalCentipedeSegment> segments = new List<OriginalCentipedeSegment>();

    [Header("Prefabs")]
    public OriginalCentipedeSegment segmentPrefab;
    public OriginalMushroom mushroomPrefab;

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
        foreach (OriginalCentipedeSegment segment in segments) {
            segment.enabled = true;
        }
    }

    private void OnDisable()
    {
        foreach (OriginalCentipedeSegment segment in segments) {
            segment.enabled = true;
        }
    }

    public void Remove(OriginalCentipedeSegment segment)
    {
        int points = segment.isHead ? pointsHead : pointsBody;
        OriginalGameManager.Instance.IncreaseScore(points);

        Vector2 position = GridPosition(segment.transform.position);
        Instantiate(mushroomPrefab, position, Quaternion.identity);

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
            OriginalGameManager.Instance.NextLevel();
        }
    }

    public void Respawn()
    {
        foreach (OriginalCentipedeSegment segment in segments) {
            Destroy(segment.gameObject);
        }

        segments.Clear();

        for (int i = 0; i < size; i++)
        {
            Vector2 position = GridPosition(transform.position) + (Vector2.left * i);
            OriginalCentipedeSegment segment = Instantiate(segmentPrefab, position, Quaternion.identity, transform);
            segment.spriteRenderer.sprite = i == 0 ? headSprite : bodySprite;
            segment.centipede = this;
            segment.enabled = enabled;
            segments.Add(segment);
        }

        for (int i = 0; i < segments.Count; i++)
        {
            OriginalCentipedeSegment segment = segments[i];
            segment.ahead = GetSegmentAt(i-1);
            segment.behind = GetSegmentAt(i+1);
        }

        enabled = true;
    }

    private OriginalCentipedeSegment GetSegmentAt(int index)
    {
        if (index >= 0 && index < segments.Count) {
            return segments[index];
        } else {
            return null;
        }
    }

    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }

}
