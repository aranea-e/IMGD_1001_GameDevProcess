using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] int animCount;
    [SerializeField] Animator anim;
    [SerializeField] Canvas c;
    float t;
    int tHit;
    [SerializeField] int freq;

    void Update() {
        if (SceneManager.GetActiveScene().buildIndex != 2 && SceneManager.GetActiveScene().buildIndex != 5)
        {
            c.enabled = true;
            t += Time.deltaTime;
            if (t >= 1.0f)
            {
                t = 0.0f;
                tHit++;
            }
            if (Random.Range(0, tHit) > freq)
            {
                tHit = 0;
                t = 0.0f;
                playRandomAnim();
            }
        } else {
            c.enabled = false;
        }
    }

    [ContextMenu("play")]
    void playRandomAnim() {
        int i = Random.Range(0, animCount);
        setAnimation(i);
        startAnimation();
    }

    void setAnimation(int i) {
        anim.SetInteger("Anim", i);
    }

    void startAnimation() {
        anim.SetTrigger("Start");
    }
}
