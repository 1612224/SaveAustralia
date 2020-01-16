using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPanel : MonoBehaviour
{
    [SerializeField]
    Vector2 offset;

    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void AlignTo(Vector3 position)
    {
        if (!rectTransform)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, position);
        rectTransform.anchoredPosition = screenPosition;
    }
}
