using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerFactory : GameObjectFactory
{
	// RockTower
	private string _RockTowerLevelsTag = "RockTower";
	private List<int> _RockTowerGoldCost = new List<int>(){ 0, 5, 10, 15 };
	private string _RockTowerCanvasTag = "RockTowerCanvas";


	[SerializeField]
	Tower defaultPrefab = default;
	[SerializeField]
	Tower rockPrefab = default;

	public Tower Get(Canvas canvas, GameTileContentType type, GameBoard board)
	{
		Tower instance;
		if (type == GameTileContentType.RockTower)
		{
			instance = CreateGameObjectInstance(rockPrefab);
			instance.transform.root.GetComponent<TowerController>().initial(canvas, _RockTowerLevelsTag, _RockTowerCanvasTag, _RockTowerGoldCost);
			instance.OriginFactory = this;

		}
		else
		{
			instance = CreateGameObjectInstance(defaultPrefab);
			instance.transform.root.GetComponent<TowerController>().initial(canvas, _RockTowerLevelsTag, _RockTowerCanvasTag, _RockTowerGoldCost);
			instance.OriginFactory = this;
		}

		//board.AddUpdatingContent(instance);
		return instance;
	}

	public void Reclaim(Enemy enemy)
	{
		Debug.Assert(enemy.OriginFactory == this, "Wrong factory reclaimed!");
		Destroy(enemy.gameObject);
	}
}
