using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetronimo : MonoBehaviour
{
    public TetronimoSegment tetronimoPrefab;

    [SerializeField] int points = 100;
    [SerializeField] float sChance;
    [SerializeField] float sRate;
    [SerializeField] float sTime;
    [SerializeField] float mTime;
    [SerializeField] float mTimeElapsed;
    [SerializeField] bool sActive;

    public string currentType;

    void Update()
    {
        if (!sActive)
        {
            checkSpawn();
        }
    }

    public void newRound() {
        destroyTetronimo(2);
        sTime = 0.0f;
        mTimeElapsed = 0.0f;
        if (GameManager.Instance.round == 1)
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
                sActive = true;
                sTime = 0.0f;
                mTimeElapsed = 0.0f;
                Spawn();
            }
        }
    }

    [ContextMenu("test spawn")]
    void Spawn() {
        Vector2[] spawnLocations = new Vector2[4];
        switch (Random.Range(0, 7))
        {
            case 0:
            currentType = "z";
            spawnLocations[0] = new Vector2(0, 25);
            spawnLocations[1] = new Vector2(1, 25);
            spawnLocations[2] = new Vector2(0, 26);
            spawnLocations[3] = new Vector2(-1, 26);
            break;

            case 1:
            currentType = "l";
            spawnLocations[0] = new Vector2(0, 25);
            spawnLocations[1] = new Vector2(0, 26);
            spawnLocations[2] = new Vector2(0, 27);
            spawnLocations[3] = new Vector2(1, 25);
            break;

            case 2:
            currentType = "o";
            spawnLocations[0] = new Vector2(0, 25);
            spawnLocations[1] = new Vector2(0, 26);
            spawnLocations[2] = new Vector2(1, 25);
            spawnLocations[3] = new Vector2(1, 26);
            break;

            case 3:
            currentType = "s";
            spawnLocations[0] = new Vector2(0, 25);
            spawnLocations[1] = new Vector2(-1, 25);
            spawnLocations[2] = new Vector2(0, 26);
            spawnLocations[3] = new Vector2(1, 26);
            break;

            case 4:
            currentType = "i";
            spawnLocations[0] = new Vector2(0, 25);
            spawnLocations[1] = new Vector2(0, 26);
            spawnLocations[2] = new Vector2(0, 27);
            spawnLocations[3] = new Vector2(0, 28);
            break;

            case 5:
            currentType = "j";
            spawnLocations[0] = new Vector2(0, 25);
            spawnLocations[1] = new Vector2(0, 26);
            spawnLocations[2] = new Vector2(0, 27);
            spawnLocations[3] = new Vector2(-1, 25);
            break;

            case 6:
            currentType = "t";
            spawnLocations[0] = new Vector2(0, 25);
            spawnLocations[1] = new Vector2(0, 26);
            spawnLocations[2] = new Vector2(-1, 26);
            spawnLocations[3] = new Vector2(1, 26);
            break;
        }

        for (int i = 0; i < 4; i++)
        {
            Instantiate(tetronimoPrefab, spawnLocations[i], Quaternion.identity, transform);
        }
    }

    [ContextMenu("test destroy")]
    public void destroyTetronimo() {
        destroyTetronimo(0);
    }
    public void destroyTetronimo(int destType) {
        if (destType == 1)
        {
            GameManager.Instance.IncreaseScore(points);
        }
        
        foreach (TetronimoSegment segment in FindObjectsOfType<TetronimoSegment>())
        {
            if (destType == 1)
            {
                GameManager.Instance.SpawnMushroom(segment.transform.position);
            }
            Destroy(segment.gameObject);
        }

        sActive = false;
    }
}
