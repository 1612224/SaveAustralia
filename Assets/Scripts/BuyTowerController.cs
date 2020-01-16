using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyTowerController : MonoBehaviour
{
    GameTile currentTile;

    public TextMeshProUGUI laserPriceText;
    public TextMeshProUGUI ballisticPriceText;

    [SerializeField]
    PlayerStatsManager player;

    [SerializeField]
    TowerFactory towerFactory;

    void Awake()
    {
        laserPriceText.text = $"{towerFactory.GetPrice(TowerType.Laser)} $";
        ballisticPriceText.text = $"{towerFactory.GetPrice(TowerType.Ballistic)} $";
    }

    public void SetTile(GameTile tile)
    {
        currentTile = tile;
        GetComponent<PopupPanel>().AlignTo(tile.transform.position);
    }

    public void BuyLaserTower()
    {
        if (player.goldController.Buy(towerFactory.GetPrice(TowerType.Laser)))
        {
            currentTile.Content = towerFactory.Get(TowerType.Laser);
        }
    }

    public void BuyBallisticTower()
    {
        if (player.goldController.Buy(towerFactory.GetPrice(TowerType.Ballistic)))
        {
            currentTile.Content = towerFactory.Get(TowerType.Ballistic);
        }
    }
}
