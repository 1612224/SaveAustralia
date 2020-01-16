using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerFactory : GameObjectFactory
{
	[System.Serializable]
	public class TowerConfig
	{
		public Tower prefab = default;
		public int price = 100;
		public int upgradePrice = 50;
	}

	public TowerConfig laser;
	public TowerConfig ballistic;

	public int GetPrice(TowerType type)
	{
		switch (type)
		{
			case TowerType.Laser: return laser.price;
			case TowerType.Ballistic: return ballistic.price;
			default: throw new System.Exception("Unsupported tower type");
		}
	}

	public int GetUpgradePrice(TowerType type)
	{
		switch (type)
		{
			case TowerType.Laser: return laser.upgradePrice;
			case TowerType.Ballistic: return ballistic.upgradePrice;
			default: throw new System.Exception("Unsupported tower type");
		}
	}

	public Tower Get(TowerType type)
	{
		switch(type)
		{
			case TowerType.Laser: return Get(laser.prefab);
			case TowerType.Ballistic: return Get(ballistic.prefab);
			default: throw new System.Exception("Unsupported tower type");
		}
	}

	Tower Get(Tower prefab)
	{
		Tower instance = CreateGameObjectInstance(prefab);
		instance.OriginFactory = this;
		return instance;
	}

	public void Reclaim(Tower tower)
	{
		Debug.Assert(tower.OriginFactory == this, "Wrong factory reclaimed!");
		Destroy(tower.gameObject);
	}
}
