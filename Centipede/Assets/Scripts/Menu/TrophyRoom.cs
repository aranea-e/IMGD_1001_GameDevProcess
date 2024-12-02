using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrophyRoom : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] gameInfo;
    [SerializeField] Toggle[] toggles;
    void Awake() {
        toggles[0].interactable = (PlayerPrefs.GetInt("Tetris", 0) == 1);
        toggles[1].interactable = (PlayerPrefs.GetInt("Invaders", 0) == 1);
        toggles[2].interactable = (PlayerPrefs.GetInt("Pac", 0) == 1);
        toggles[3].interactable = (PlayerPrefs.GetInt("Mario", 0) == 1);
        toggles[4].interactable = (PlayerPrefs.GetInt("Dig", 0) == 1);
    }

    public void ChangeInfo(int i) {
        for (int j = 0; j < gameInfo.Length; j++)
        {
            gameInfo[j].enabled = (j == i);
        }
    }
}
