using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : WarEntity
{
	Vector3 launchPoint, targetPoint, launchVelocity;

	public void Initialize(
		Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity,
		float blastRadius, int damage
	)
	{
		this.launchPoint = launchPoint;
		this.targetPoint = targetPoint;
		this.launchVelocity = launchVelocity;
		this.blastRadius = blastRadius;
		this.damage = damage;
	}

	float age, blastRadius;
	int damage;

	public override bool GameUpdate()
	{
		age += Time.deltaTime;
		Vector3 p = launchPoint + launchVelocity * age;
		p.y -= 0.5f * 9.81f * age * age;
		transform.position = p;
		if(p.y < targetPoint.y)
		{
			Explosion explosion = Game.SpawnExplosion();
			explosion.Initialize(p, blastRadius, damage);
			return false;
		}
		return true;
	}

	void Update()
	{
		if (!GameUpdate()){
			OriginFactory.Reclaim(this);
			return;
		}
	}
}
