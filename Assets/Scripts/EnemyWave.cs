using UnityEngine;

[CreateAssetMenu]
public class EnemyWave : ScriptableObject
{
	public EnemySpawnSequence[] spawnSequences = {
		new EnemySpawnSequence()
	};
}