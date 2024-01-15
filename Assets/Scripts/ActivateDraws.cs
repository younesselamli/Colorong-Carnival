using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDraws : MonoBehaviour
{
    public GameObject[] levels;
    public int currentLevelIndex;

    void Start()
    {

        currentLevelIndex = PlayerPrefs.GetInt("CurrentDraw", 0);
        ActivateLevel(currentLevelIndex);


    }

    void ActivateLevel(int levelIndex)
    {
        levels[levelIndex].SetActive(true);
    }

    void DeactivateLevel(int levelIndex)
    {
        levels[levelIndex].SetActive(false);
    }
}
