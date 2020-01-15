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
    PlayerStatsManager player;

    static Game instance;

    const float pausedTimeScale = 0f;

    [SerializeField, Range(1f, 10f)]
    float playSpeed = 1f;

    Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

    void OnEnable()
    {
        instance = this;
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
        // Time control for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale =
                Time.timeScale > pausedTimeScale ? pausedTimeScale : playSpeed;
        }
        else if (Time.timeScale > pausedTimeScale)
        {
            Time.timeScale = playSpeed;
        }

        // Gameplay
        if (Input.GetMouseButtonDown(0))
        {
            GameTile tile = board.GetTile(TouchRay);
            if (tile?.Content.Type == GameTileContentType.Wall)
                tile.Content = tileContentFactory.Get(GameTileContentType.Tower);
        }
    }
}