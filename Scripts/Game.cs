using UnityEngine;

public class Game : MonoBehaviour
{
    public Canvas canvas;

    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);

    [SerializeField]
    GameBoard board = default;

    [SerializeField]
    GameTileContentFactory tileContentFactory = default;

    [SerializeField]
    EnemyFactory enemyFactory = default;

    [SerializeField]
    TowerFactory towerFactory = default;

    [SerializeField]
    WarFactory warFactory = default;

    [SerializeField, Range(0.1f, 10f)]
    float spawnSpeed = 1f;
    float spawnProgress;

    Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

    void OnValidate()
    {
        // min size is 2x2
        if (boardSize.x < 2)
        {
            boardSize.x = 2;
        }
        if (boardSize.y < 2)
        {
            boardSize.y = 2;
        }
    }

    void Start()
    {
        Tower t = towerFactory.Get(canvas,GameTileContentType.RockTower,board);
        t.SetPosition(new Vector3(60,0,-50));
        Tower t1 = towerFactory.Get(canvas, GameTileContentType.RockTower, board);
        t1.SetPosition(new Vector3(70, 0, -50));

        Enemy enemy = enemyFactory.Get();
        enemy.SpawnOn(board.spawnPoint);
        enemy.Initialize(1);
        enemy.transform.position = new Vector3(70, 0, -60);

        Enable();
    }

    void Update()
    {
        // Gameplay
        //if (Input.GetMouseButtonDown(0))
        //{
        //    GameTile tile = board.GetTile(TouchRay);
        //    if (tile?.Content.Type == GameTileContentType.Wall)
        //        tile.Content = tileContentFactory.Get(GameTileContentType.RockTower);
        //}

        spawnProgress += spawnSpeed * Time.deltaTime;
        while (spawnProgress >= 1f)
        {
            spawnProgress -= 1f;
            SpawnEnemy();
        }

        Physics.SyncTransforms();
        //board.GameUpdate();
    }

  

    private void SpawnEnemy()
    {
        //GameTile spawnPoint =
        //    board.GetSpawnPoint(Random.Range(0, board.SpawnPointCount));
        //Enemy enemy = enemyFactory.Get();
        //enemy.SpawnOn(spawnPoint);
        //enemy.SetDestination(board.destination);
        Enemy enemy = enemyFactory.Get();
        enemy.SpawnOn(board.spawnPoint);
        enemy.Initialize(1);
        enemy.SetDestination(board.destination);
    }

    static Game instance;

    public static Shell SpawnShell()
    {
        Shell shell = instance.warFactory.Shell;
        return shell;
    }
    void Enable()
    {
        instance = this;
    }

    public static Explosion SpawnExplosion()
    {
        Explosion explosion = instance.warFactory.Explosion;
        return explosion;
    }
}