using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public InvaderInstance invaderPrefab;

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
        destroyInvader(2);
        sTime = 0.0f;
        mTimeElapsed = 0.0f;
        if ((GameManager.Instance.round == 1) || 
            ((GameManager.Instance.round % 2) == 0) && (GameManager.Instance.round < 7))
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
        Instantiate(invaderPrefab, position, Quaternion.identity, transform);
        sActive = true;
        mTimeElapsed = 0.0f;
        
    }

    [ContextMenu("test super spawn")]
    void SpawnSuper() {
        Vector2 position = new Vector2(Random.Range(-15,16), 20);
        Instantiate(invaderPrefab, position, Quaternion.identity, transform).super = true;;
        sActive = true;
        mTimeElapsed = 0.0f;
        
    }

    [ContextMenu("destroy")]
    public void destroyInvader() {
        destroyInvader(0);
    }

    public void destroyInvader(int destType) {
        if (FindObjectOfType<InvaderInstance>() == null)
        {
            sActive = false;
            return;
        }
        if (destType == 1)
        {
            GameManager.Instance.IncreaseScore(points);
            PlayerPrefs.SetInt("Invaders", 1);
        }
        Destroy(FindObjectOfType<InvaderInstance>().gameObject);
        if (destType == 2)
        {
            foreach (InvaderShot shot in FindObjectsOfType<InvaderShot>())
            {
                Destroy(shot.gameObject);
            }
        }
        sActive = false;
    }

}
