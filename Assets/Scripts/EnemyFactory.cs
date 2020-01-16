using UnityEngine;

[CreateAssetMenu]
public class EnemyFactory : GameObjectFactory
{
    [System.Serializable]
    class EnemyConfig
    {
        public Enemy prefab = default;
        public int health = 100;
        public int gold = 10;
    }

    [SerializeField]
    EnemyConfig small = default, medium = default, large = default;


    EnemyConfig GetConfig(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Small: return small;
            case EnemyType.Medium: return medium;
            case EnemyType.Large: return large;
        }
        Debug.Assert(false, "Unsupported enemy type!");
        return null;
    }

    public Enemy Get(EnemyType type = EnemyType.Medium)
    {
        EnemyConfig config = GetConfig(type);
        Enemy instance = CreateGameObjectInstance(config.prefab);
        instance.OriginFactory = this;
        instance.Initialize(config.health, config.gold);
        return instance;
    }

    public void Reclaim(Enemy enemy)
    {
        Debug.Assert(enemy.OriginFactory == this, "Wrong factory reclaimed!");
        Destroy(enemy.gameObject);
    }


}