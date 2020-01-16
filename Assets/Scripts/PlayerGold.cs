using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGold : MonoBehaviour
{
    int currentGold;
    public static int StartGold = 1000;

    [Header("Gold Events")]
    public IntEvent goldChangedEvent;
    public UnityEvent notEnoughGoldEvent;

    void Start()
    {
        currentGold = StartGold;
        if (goldChangedEvent == null)
        {
            goldChangedEvent = new IntEvent();
        }
        if (notEnoughGoldEvent == null)
        {
            notEnoughGoldEvent = new UnityEvent();
        }
    }

    public int CurrentGold => currentGold;

    public bool Buy(int price)
    {
        if (currentGold < price)
        {
            notEnoughGoldEvent.Invoke();
            return false;
        }

        currentGold -= price;
        goldChangedEvent.Invoke(currentGold);
        return true;
    }

    public void Sell(int price)
    {
        currentGold += price;
        goldChangedEvent.Invoke(currentGold);
    }
}
