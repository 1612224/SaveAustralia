using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOptionController : MonoBehaviour
{
    GameTile currentTile;

    [SerializeField]
    TowerFactory towerFactory;

    public void SetTile(GameTile tile)
    {
        currentTile = tile;
        GetComponent<PopupPanel>().AlignTo(tile.transform.position);
    }

    public void BuyLaserTower()
    {
        currentTile.Content = towerFactory.Get(TowerType.Laser);
    }

    public void BuyBallisticTower()
    {
        currentTile.Content = towerFactory.Get(TowerType.Ballistic);
    }
}
