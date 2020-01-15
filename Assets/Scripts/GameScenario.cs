using UnityEngine;

[CreateAssetMenu]
public class GameScenario : ScriptableObject
{
	public EnemyWave[] waves = { };

	[Range(0, 10)]
	public int cycles = 1;
}