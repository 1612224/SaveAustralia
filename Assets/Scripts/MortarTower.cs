using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTower : Tower
{
	[SerializeField]
	Transform turret = default;    

	void Awake()
	{
		
	}

    public override TowerType TowerType => TowerType.Ballistic;

    public override void GameUpdate()
	{
		if (TrackTarget(ref target) || AcquireTarget(out target))
		{
			Shoot();
		}
		else
		{
			
		}
	}

	protected override void Shoot()
	{
		
	}
}
