using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUpgradeCanvas : MonoBehaviour
{
    public GameObject[] levelModels;

    public void SetLevel(int level)
    {
        for (int i = 0; i < levelModels.Length; i++)
        {
            levelModels[i].SetActive(i + 2 == level);
        }
    }
}
