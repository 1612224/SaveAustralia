using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellTowerController : MonoBehaviour
{
    GameTile currentTile;
    int price;

    public TextMeshProUGUI priceText;

    [SerializeField]
    PlayerStatsManager player;

    [SerializeField]
    GameTileContentFactory contentFactory;

    [SerializeField]
    TowerFactory towerFactory;

    public void SetTile(GameTile tile)
    {
        currentTile = tile;
        price = towerFactory.GetPrice((tile.Content as Tower).towerType);
        priceText.text = $"{price} $";
        GetComponent<PopupPanel>().AlignTo(tile.transform.position);
    }

    public void Sell()
    {
        player.goldController.Sell(price);
        currentTile.Content = contentFactory.Get(GameTileContentType.Wall);
    }
}
