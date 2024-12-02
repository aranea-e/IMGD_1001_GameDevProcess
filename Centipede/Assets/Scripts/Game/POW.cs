using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POW : MonoBehaviour
{
    public POWInstance POWPrefab;
    public POWEffect POWEffectPrefab;

    [SerializeField] float sChance;
    [SerializeField] float sRate;
    [SerializeField] float sTime;
    [SerializeField] float mTime;
    [SerializeField] float mTimeElapsed;
    [SerializeField] bool sActive;

    void Update()
    {
        if (!sActive)
        {
            checkSpawn();
        }
    }

    public void newRound() {
        destroyPOW(2);
        if (GameManager.Instance.round <= 2)
        {
            sChance = 0.0f;
            return;
        }
        sTime = 0.0f;
        mTimeElapsed = 0.0f;
        sChance = 0.1f;
        sRate = 1.0f;
        mTime = 5.0f;
        for (int i = 4; i < GameManager.Instance.round; i++)
        {
            sChance *= 1.25f;
            sRate *= 0.90f;
            mTime *= 0.9f;
        }
    }

    void checkSpawn() 
    {
        mTimeElapsed += Time.deltaTime;
        if (mTimeElapsed < mTime)
        {
            return;
        }
        sTime += Time.deltaTime;
        if (sTime > sRate)
        {
            sTime = 0.0f;
            if (Random.Range(0.0f, 1.0f) < sChance)
            {
                Spawn();
            }
        }
    }

    [ContextMenu("test spawn")]
    void Spawn() {
        Bounds bounds = FindObjectOfType<MushroomField>().area.bounds;

        for (int i = 0; i < 100; i++)
        {
            Vector2 position = Vector2.zero;

            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

            if (!FindObjectOfType<MushroomField>().MushroomAt(position))
            {
                Instantiate(POWPrefab, position, Quaternion.identity, transform);
                sActive = true;
                mTimeElapsed = 0.0f;
                break;
            }
        }
        
    }

    [ContextMenu("destroy")]
    public void destroyPOW() {
        destroyPOW(0);
    }

    public void destroyPOW(int destType) {
        foreach (POWInstance p in FindObjectsOfType<POWInstance>())
        {
            destroyPOW(p, destType);
        }
    }

    public void destroyPOW(POWInstance p) {
        destroyPOW(p, 0);
    }

    public void destroyPOW(POWInstance p, int destType) {
        if (destType == 1)
        {
            spawnPOWEffect(p.transform.position);
        }
        Destroy(p.gameObject);
        sActive = false;
    }

    public void spawnPOWEffect(Vector2 pos) {
        Instantiate(POWEffectPrefab, pos, Quaternion.identity, transform);
    }
}
