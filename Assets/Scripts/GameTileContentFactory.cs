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
	Tower[] towerPrefabs = default;

	public GameTileContent Get(GameTileContentType type)
	{
		switch (type)
		{
			case GameTileContentType.Destination: return Get(destinationPrefab);
			case GameTileContentType.Empty: return Get(emptyPrefab);
			case GameTileContentType.Wall: return Get(highWallPrefab);
			case GameTileContentType.Spawn: return Get(spawnPrefab);
			case GameTileContentType.Tree: return Get(treePrefab);
			//case GameTileContentType.Tower: return Get(towerPrefab);
		}
		Debug.Assert(false, "Unsupported non-tower type: " + type);
		return null;
	}

    public GameTileContent Get(TowerType type)
    {
        Debug.Assert((int)type < towerPrefabs.Length, "Unsupported tower type!");
        Tower prefab = towerPrefabs[(int)type];
        Debug.Assert(type == prefab.TowerType, "Tower prefab at wrong index!");
        return Get(prefab);
    }

    T Get<T>(T prefab) where T : GameTileContent
    {
        T instance = CreateGameObjectInstance(prefab);
        instance.OriginFactory = this;
        return instance;
    }

    public void Reclaim(GameTileContent content)
	{
		Debug.Assert(content.OriginFactory == this, "Wrong factory reclaimed!");
		CustomDestroy.SafeDestroy(content.gameObject);
	}

	//GameTileContent Get(GameTileContent prefab)
	//{
	//	GameTileContent instance = CreateGameObjectInstance(prefab);
	//	instance.OriginFactory = this;
	//	return instance;
	//}
}