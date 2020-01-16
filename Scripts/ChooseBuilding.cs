using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuilding : MonoBehaviour
{
    [SerializeField]
    RectTransform prefab;

    Canvas canvas;

    RectTransform obj;
    bool isShowing = false;

    void Awake()
    {
        canvas = GetComponent<GameTileContent>().canvas;
    }

    public void Toggle()
    {
        if (isShowing)
        {
            Destroy(obj);
        }
        else
        {
            obj = Instantiate(prefab);
            isShowing = true;
        }
    }
}
