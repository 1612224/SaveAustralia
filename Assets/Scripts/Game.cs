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
}