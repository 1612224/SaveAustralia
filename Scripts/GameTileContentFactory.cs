using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameTileContentFactory : GameObjectFactory
{
	[SerializeField]
	GameTileContent destinationPrefab = default;

	[SerializeField]
	GameTileContent emptyPrefab = default;

	[SerializeField]
	GameTileContent highWallPrefab = default;

	[SerializeField]
	GameTileContent lowWallPrefab = default;

	[SerializeField]
	GameTileContent spawnPrefab = default;

	[SerializeField]
	GameTileContent treePrefab = default;

	[SerializeField]
	GameTileContent rockTowerPrefab = default;

	public GameTileContent Get(GameTileContentType type)
	{
		switch (type)
		{
			case GameTileContentType.Destination: return Get(destinationPrefab);
			case GameTileContentType.Empty: return Get(emptyPrefab);
			case GameTileContentType.Wall: return Get(highWallPrefab);
			case GameTileContentType.Spawn: return Get(spawnPrefab);
			case GameTileContentType.Tree: return Get(treePrefab);
			case GameTileContentType.RockTower: return Get(rockTowerPrefab);
		}
		Debug.Assert(false, "Unsupported type: " + type);
		return null;
	}

	public void Reclaim(GameTileContent content)
	{
		Debug.Assert(content.OriginFactory == this, "Wrong factory reclaimed!");
		CustomDestroy.SafeDestroy(content.gameObject);
	}

	GameTileContent Get(GameTileContent prefab)
	{
		GameTileContent instance = CreateGameObjectInstance(prefab);
		instance.OriginFactory = this;
		return instance;
	}
}