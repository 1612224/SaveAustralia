using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);

    [SerializeField]
    GameBoard board = default;

    [SerializeField]
    GameTileContentFactory tileContentFactory = default;

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

    void Awake()
    {
        board.Initialize(boardSize, tileContentFactory);
    }

    void Update()
    {
        // Build board
        if (Input.GetKey(KeyCode.W))
        {
            HandleTouch(KeyCode.W);
        }
        else if (Input.GetKey(KeyCode.Alpha0))
        {
            HandleTouch(KeyCode.Alpha0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            HandleTouch(KeyCode.D);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            HandleTouch(KeyCode.S);
        }
        else if (Input.GetKey(KeyCode.T))
        {
            HandleTouch(KeyCode.T);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            board.ClearBoard();
        }
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
