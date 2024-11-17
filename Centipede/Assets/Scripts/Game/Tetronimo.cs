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
        destroyTetronimo(false);
    }
    public void destroyTetronimo(bool givePoints) {
        if (givePoints)
        {
            GameManager.Instance.IncreaseScore(points);
        }
        
        foreach (TetronimoSegment segment in FindObjectsOfType<TetronimoSegment>())
        {
            GameManager.Instance.SpawnMushroom(segment.transform.position);
            Destroy(segment.gameObject);
        }

        sActive = false;
    }
}
