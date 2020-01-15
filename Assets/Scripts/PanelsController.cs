using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> panels;

    public bool AnyPanelActive => panels.Exists(obj => obj.activeSelf);

    void Awake()
    {
        foreach (var p in panels)
        {
            p.SetActive(false);
        }
    }

    public void ActivePanel(GameObject panel)
    {
        foreach (var p in panels)
        {
            if (Object.Equals(panel, p))
            {
                p.SetActive(true);
            }
            else
            {
                p.SetActive(false);
            }
        }
    }

    public void DeactivePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
