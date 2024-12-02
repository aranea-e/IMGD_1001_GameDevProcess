using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooka : MonoBehaviour
{
    public PookaInstance pookaPrefab;

    [SerializeField] int points = 500;
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
        destroyPooka(2);
        sTime = 0.0f;
        mTimeElapsed = 0.0f;
        if ((GameManager.Instance.round < 4) || 
            (GameManager.Instance.round == 6) || 
            (GameManager.Instance.round == 7))
        {
            sChance = -1f;
            return;
        }
        sChance = 0.1f;
        sRate = 1.0f;
        mTime = 5.0f;
        for (int i = 2; i < GameManager.Instance.round; i++)
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
        Vector2 position = new Vector2(Random.Range(-15,16), 20);
        Instantiate(pookaPrefab, position, Quaternion.identity, transform);
        sActive = true;
        mTimeElapsed = 0.0f;
        
    }

    [ContextMenu("destroy")]
    public void destroyPooka() {
        destroyPooka(0);
    }

    public void destroyPooka(int destType) {
        if (FindObjectOfType<PookaInstance>() == null)
        {
            sActive = false;
            return;
        }
        if (destType == 1)
        {
            GameManager.Instance.IncreaseScore(points);
            PlayerPrefs.SetInt("Dig", 1);
        }
        Destroy(FindObjectOfType<PookaInstance>().gameObject);
        sActive = false;
    }
}
