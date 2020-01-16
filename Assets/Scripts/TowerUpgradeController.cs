using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUpgradeController : MonoBehaviour
{
    Tower tower;
    TowerLevelController levelController;

    [SerializeField]
    PlayerStatsManager player;

    [SerializeField]
    TowerFactory towerFactory;

    [SerializeField]
    TowerUpgradeCanvas laser;

    [SerializeField]
    TowerUpgradeCanvas ballistic;

    public GameObject upgradePanel;
    public GameObject maxLevelPanel;

    public TextMeshProUGUI priceText;

    public void SetTower(GameTile tile)
    {
        tower = tile.Content as Tower;
        levelController = tower.GetComponent<TowerLevelController>();

        Debug.Log(tower.towerType);

        upgradePanel.SetActive(false);
        maxLevelPanel.SetActive(false);

        if (levelController.MaxLevel())
        {
            maxLevelPanel.SetActive(true);
            return;
        }
        
        upgradePanel.SetActive(true);
        priceText.text = $"{towerFactory.GetUpgradePrice(tower.towerType)} $";
        switch (tower.towerType)
        {
            case TowerType.Laser:
                {
                    laser.SetLevel(levelController.CurrentLevel + 1);
                    laser.gameObject.SetActive(true);
                    ballistic.gameObject.SetActive(false);
                    break;
                }
            case TowerType.Ballistic:
                {
                    ballistic.SetLevel(levelController.CurrentLevel + 1);
                    ballistic.gameObject.SetActive(true);
                    laser.gameObject.SetActive(false);
                    break;
                }
        }
    }

    public void Upgrade()
    {
        if (player.goldController.Buy(towerFactory.GetUpgradePrice(tower.towerType)))
        {
            levelController.UpLevel();
        }
    }

    public void Refresh()
    {
        if (levelController.MaxLevel())
        {
            upgradePanel.SetActive(false);
            maxLevelPanel.SetActive(true);
            return;
        }

        priceText.text = $"{towerFactory.GetUpgradePrice(tower.towerType)} $";
        switch (tower.towerType)
        {
            case TowerType.Laser:
                {
                    laser.SetLevel(levelController.CurrentLevel + 1);
                    laser.gameObject.SetActive(true);
                    ballistic.gameObject.SetActive(false);
                    break;
                }
            case TowerType.Ballistic:
                {
                    ballistic.SetLevel(levelController.CurrentLevel + 1);
                    ballistic.gameObject.SetActive(true);
                    laser.gameObject.SetActive(false);
                    break;
                }
        }
    }
}
