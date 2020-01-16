using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLevelController : MonoBehaviour
{
    int currentLevel = 1;

    [SerializeField]
    GameObject[] levelModels;

    public int CurrentLevel => currentLevel;

    void Awake()
    {
        ActivateModel(0);
    }

    public void UpLevel()
    {
        if (currentLevel < levelModels.Length)
        {
            ActivateModel(currentLevel++);
            GetComponent<Tower>().UpLevel(currentLevel);
        }
    }

    public bool MaxLevel()
    {
        return currentLevel == levelModels.Length;
    }

    void ActivateModel(int index)
    {
        for (int i = 0; i < levelModels.Length; i++)
        {
            levelModels[i].SetActive(i == index);
        }
    }
}
