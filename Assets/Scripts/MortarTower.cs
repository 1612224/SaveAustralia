using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTower : Tower
{
	[SerializeField, Range(1f, 100f)]
	float damagePerSecond = 10f;

	[SerializeField]
	Transform turret = default, laserBeam = default;

	TargetPoint target;

	Vector3 laserBeamScale;

	void Awake()
	{
		laserBeamScale = laserBeam.localScale;
	}

	public override void GameUpdate()
	{
		if (TrackTarget(ref target) || AcquireTarget(out target))
		{
			Shoot();
		}
		else
		{
			laserBeam.localScale = Vector3.zero;
		}
	}

	void Shoot()
	{
		
	}
}
