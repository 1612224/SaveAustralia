using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	NavMeshAgent agent;
	EnemyFactory originFactory;

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

	public void SpawnOn(GameTile tile)
	{
		agent.Warp(tile.transform.position);
	}

	public void SetDestination(GameTile tile)
	{
		agent.SetDestination(tile.transform.position);
	}
}