using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	NavMeshAgent agent;
	EnemyFactory originFactory;

	public int Health { get; set; }
	public float Scale { get; set; }

	public void Initialize(float scale)
	{
		Scale = scale;
		Health = 1;
	}

	void Awake()
	{
		
		agent = transform.root.GetComponent<NavMeshAgent>();
		Debug.Assert(agent != null, "AWake - Enemy without navgent!");
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

	public void SpawnOn(GameTile tile)
	{
		agent.Warp(tile.transform.position);
	}

	public void SetDestination(GameTile tile)
	{
		Debug.Assert(agent != null, "SetDestination - Enemy without navgent!");
		agent.SetDestination(tile.transform.position);
	}

	public void ApplyDamage(int damage)
	{
		Debug.Assert(damage >= 0, "Negative damage applied.");
		Health -= damage;

		GameUpdate();
	}

	public bool GameUpdate()
	{

		if(Health <= 0)
		{
			originFactory.Reclaim(this);
			return false;
		}
		return true;

	}
}