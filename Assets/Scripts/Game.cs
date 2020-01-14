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

    [SerializeField, Range(0.1f, 10f)]
    float spawnSpeed = 1f;
    float spawnProgress;

    Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

    void Awake()
    {
        //board.Initialize(boardSize, tileContentFactory);
    }

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

    void Update()
    {
        // Build board
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    HandleTouch(KeyCode.W);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    HandleTouch(KeyCode.Alpha0);
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    HandleTouch(KeyCode.D);
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    HandleTouch(KeyCode.S);
        //}
        //else if (Input.GetKeyDown(KeyCode.T))
        //{
        //    HandleTouch(KeyCode.T);
        //}

        // Gameplay
        if (Input.GetMouseButtonDown(0))
        {
            GameTile tile = board.GetTile(TouchRay);
            if (tile?.Content.Type == GameTileContentType.Wall)
                tile.Content = tileContentFactory.Get(GameTileContentType.Tower);
        }

        spawnProgress += spawnSpeed * Time.deltaTime;
        while (spawnProgress >= 1f)
        {
            spawnProgress -= 1f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameTile spawnPoint =
            board.GetSpawnPoint(Random.Range(0, board.SpawnPointCount));
        Enemy enemy = enemyFactory.Get();
        enemy.SpawnOn(spawnPoint);
        enemy.SetDestination(board.destination);
    }

    void HandleTouch(KeyCode key)
    {
        GameTile tile = board.GetTile(TouchRay);
        if (tile != null)
        {
            switch (key)
            {
                case KeyCode.W: tile.Content = tileContentFactory.Get(GameTileContentType.Wall); break;
                case KeyCode.Alpha0: tile.Content = tileContentFactory.Get(GameTileContentType.Empty); break;
                case KeyCode.D: tile.Content = tileContentFactory.Get(GameTileContentType.Destination); break;
                case KeyCode.S: tile.Content = tileContentFactory.Get(GameTileContentType.Spawn); break;
                case KeyCode.T: tile.Content = tileContentFactory.Get(GameTileContentType.Tree); break;
            }
        }
    }
}