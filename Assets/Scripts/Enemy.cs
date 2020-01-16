using UnityEngine;
using UnityEngine.AI;

public class Enemy : GameBehavior
{
	NavMeshAgent agent;
	EnemyFactory originFactory;
	public int damage = 10;
	public int health = 100;
	public int gold = 10;

	public PlayerStatsManager player;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	public EnemyFactory OriginFactory
	{
		get => originFactory;
		set
		{
			Debug.Assert(originFactory == null, "Redefined origin factory!");
			originFactory = value;
		}
	}

	public void Initialize(int health, int gold)
	{
		this.health = health;
		this.gold = gold;
	}


	public void SpawnOn(Transform transform)
	{
		agent.Warp(transform.position);
	}

	public void SetDestination(Transform transform)
	{
		agent.SetDestination(transform.position);
	}

	public void OnReachDestination()
	{
		player.healthController.Damage(damage);
		originFactory.Reclaim(this);
	}

	public void ApplyDamage(int damage)
	{
		Debug.Assert(damage >= 0, "Negative damage applied.");
		health -= damage;
		GameUpdate();
	}

	public override bool GameUpdate()
	{

		if(health <= 0)
		{
			originFactory.Reclaim(this);
			player.goldController.Sell(gold);
			return false;
		}
		return true;

	}
}