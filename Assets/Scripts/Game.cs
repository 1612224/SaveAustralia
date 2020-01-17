using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    TowerFactory towerFactory = default;

    [SerializeField]
    WarFactory warFactory = default;

    GameBehaviorCollection nonEnemies = new GameBehaviorCollection();

    static Game instance;

    bool waitForNextScene = false;

    const float pausedTimeScale = 0f;

    [SerializeField, Range(1f, 10f)]
    float playSpeed = 1f;

    Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

    void OnEnable()
    {
        instance = this;
        waitForNextScene = false;
    }

    public static Shell SpawnShell()
    {
        Shell shell = instance.warFactory.Shell;
        instance.nonEnemies.Add(shell);
        return shell;
    }

    public static Explosion SpawnExplosion()
    {
        Explosion explosion = instance.warFactory.Explosion;
        instance.nonEnemies.Add(explosion);
        return explosion;
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

    private bool IsRoundFinished()
    {
        GameObject[] spawnObj = GameObject.FindGameObjectsWithTag("Spawn");
        if (Enemy.instanceCount > 0)
            return false;

        //Debug.Log("COUNT SPAwN:" + spawnObj.Length);
        foreach(var o in spawnObj)
        {
            SpawnController spawn = o.GetComponent<SpawnController>();
            if (spawn)
            {
                if (!spawn.isFinished) return false;
            }
        }

        return true;
    }

    void Update()
    {
        if (IsRoundFinished() && !waitForNextScene)
        {
            int idx = SceneManager.GetActiveScene().buildIndex;
            SceneManager.UnloadSceneAsync(idx);
            SceneManager.LoadSceneAsync(idx + 1);
            waitForNextScene = true;
            return;
        }
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

        nonEnemies.GameUpdate();
        Physics.SyncTransforms();
    }
}