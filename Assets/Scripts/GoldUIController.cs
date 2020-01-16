using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUIController : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    void Awake()
    {
        goldText.text = $"{PlayerGold.StartGold} $";
    }

    public void UpdateGold(int amount)
    {
        goldText.text = $"{amount} $";
    }
}
