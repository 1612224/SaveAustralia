using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    public TextMeshProUGUI warnText;

    public void SetWarningText(string msg)
    {
        warnText.text = msg;
    }
}
