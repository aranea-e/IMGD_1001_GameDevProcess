using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public FruitInstance fruitPrefab;

    [SerializeField] int[] points;
    [SerializeField] float sChance;
    [SerializeField] float sRate;
    [SerializeField] float sTime;
    [SerializeField] float mTime;
    [SerializeField] float mTimeElapsed;
    [SerializeField] bool sActive;
    int fruitType;

    void Update()
    {
        if (!sActive)
        {
            checkSpawn();
        }
    }

    public void newRound() {
        destroyFruit(2);
        sTime = 0.0f;
        mTimeElapsed = 0.0f;
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
        fruitType = randomFruit();
        Bounds bounds = FindObjectOfType<MushroomField>().area.bounds;

        for (int i = 0; i < 100; i++)
        {
            Vector2 position = Vector2.zero;

            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

            if (!FindObjectOfType<MushroomField>().MushroomAt(position))
            {
                Instantiate(fruitPrefab, position, Quaternion.identity, transform).UpdateSprite(fruitType);
                sActive = true;
                mTimeElapsed = 0.0f;
                break;
            }
        }
        
    }

    [ContextMenu("destroy")]
    public void destroyFruit() {
        destroyFruit(0);
    }

    public void destroyFruit(int destType) {
        foreach (FruitInstance f in FindObjectsOfType<FruitInstance>())
        {
            destroyFruit(f, destType);
        }
    }

    public void destroyFruit(FruitInstance f) {
        destroyFruit(f, 0);
    }

    public void destroyFruit(FruitInstance f, int destType) {
        if (destType == 1)
        {
            GameManager.Instance.IncreaseScore(points[fruitType]);
            PlayerPrefs.SetInt("Pac", 1);
        }
        Destroy(f.gameObject);
        sActive = false;
    }

    [ContextMenu("test rng")]
    public void debugRandomFruit() {
        int[] nums = new int[7];
        for (int i = 0; i < 100; i++)
        {
            nums[randomFruit()]++;
        }
        Debug.Log("0:" + nums[0]);
        Debug.Log("1:" + nums[1]);
        Debug.Log("2:" + nums[2]);
        Debug.Log("3:" + nums[3]);
        Debug.Log("4:" + nums[4]);
        Debug.Log("5:" + nums[5]);
        Debug.Log("6:" + nums[6]);
    }

    int randomFruit() {
        int a = Mathf.Abs(Random.Range(0, 8) - Random.Range(0, 8));
        int b = Mathf.Abs(Random.Range(0, 8) - Random.Range(0, 8));
        int i = Mathf.Abs(a - b);
        if (i == 0)
        {
            return 0;
        }
        return (i - 1);
    }
}
