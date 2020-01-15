using UnityEngine;

[System.Serializable]
public class EnemySpawnSequence
{

	public EnemyFactory factory = default;

	public EnemyType type = EnemyType.Medium;

	[Range(1, 100)]
	public int amount = 1;

	[Range(0.1f, 10f)]
	public float cooldown = 1f;
}