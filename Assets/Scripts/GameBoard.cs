using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    Transform ground = default;

    Vector2Int size;

    [SerializeField]
    GameTile tilePrefab = default;

    GameTile[] tiles;

    public GameTileContentFactory contentFactory;

    public NavMeshSurface navmesh;

    // For grouping in editor only, do not use it in gameplay
    public GameObject destinationsGroupingObject;
    // For grouping in editor only, do not use it in gameplay
    public GameObject spawnPointsGroupingObject;
    // For grouping in editor only, do not use it in gameplay
    public GameObject othersGroupingObject;

    public void ReadBoardFromText(string filename)
    {
        using (var file = new StreamReader(filename))
        {
            var data = new List<List<char>>();
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                data.Add(new List<char>(ln));
            }

            InitializeFromDataList(data);
        }
    }

    public void ClearBoard()
    {
        if (tiles == null)
        {
            return;
        }
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                tiles[i].Content = contentFactory.Get(GameTileContentType.Empty);
            }
        }
    }

    public void SaveToText(string filename)
    {
        using (var file = new StreamWriter(filename))
        {
            for (int i = 0, y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++, i++)
                {
                    GameTile tile = tiles[i];
                    switch (tile.Content.Type)
                    {
                        case GameTileContentType.Empty: file.Write("0"); break;
                        case GameTileContentType.Tree: file.Write("T"); break;
                        case GameTileContentType.Destination: file.Write("D"); break;
                        case GameTileContentType.Spawn: file.Write("S"); break;
                        case GameTileContentType.Wall: file.Write("W"); break;
                        case GameTileContentType.Tower: file.Write("R"); break;
                    }
                }
                file.Write("\n");
            }
        }
    }

    public GameTile GetTile(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            int x = (int)(hit.point.x + size.x * 0.5f);
            int y = (int)(hit.point.z + size.y * 0.5f);
            if (x >= 0 && x < size.x && y >= 0 && y < size.y)
            {
                return tiles[x + y * size.x];
            }
        }
        return null;
    }

    public void Initialize(Vector2Int size, GameTileContentFactory contentFactory)
    {
        ClearBoard();
        this.size = size;
        ground.localScale = new Vector3(size.x, size.y, 1f);

        Vector2 offset = new Vector2(
            (size.x - 1) * 0.5f, (size.y - 1) * 0.5f
        );

        tiles = new GameTile[size.x * size.y];
        for (int i = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, i++)
            {
                GameTile tile = tiles[i] = Instantiate(tilePrefab);
                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(
                    x - offset.x, 0f, y - offset.y
                );

                if (x > 0)
                {
                    GameTile.MakeEastWestNeighbors(tile, tiles[i - 1]);
                }
                if (y > 0)
                {
                    GameTile.MakeNorthSouthNeighbors(tile, tiles[i - size.x]);
                }

                tile.Content = contentFactory.Get(GameTileContentType.Empty);
            }
        }
    }

    public void InitializeFromDataList(List<List<char>> data)
    {
        ClearBoard();
        Debug.Assert(data != null && data.Count > 2 && data[0].Count > 2, "Invalid Board Data - Min size is 2x2");
        this.size = new Vector2Int(data.Count, data[0].Count);
        ground.localScale = new Vector3(size.x, size.y, 1f);

        Vector2 offset = new Vector2(
            (size.x - 1) * 0.5f, (size.y - 1) * 0.5f
        );

        tiles = new GameTile[size.x * size.y];
        for (int i = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, i++)
            {
                GameTile tile = tiles[i] = Instantiate(tilePrefab);
                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(
                    x - offset.x, 0f, y - offset.y
                );

                if (x > 0)
                {
                    GameTile.MakeEastWestNeighbors(tile, tiles[i - 1]);
                }
                if (y > 0)
                {
                    GameTile.MakeNorthSouthNeighbors(tile, tiles[i - size.x]);
                }

                switch (data[y][x])
                {
                    case '0': tile.Content = contentFactory.Get(GameTileContentType.Empty); break;
                    case 'D':
                        {
                            tile.Content = contentFactory.Get(GameTileContentType.Destination);
                            tile.transform.SetParent(destinationsGroupingObject.transform);
                            break;
                        }
                    case 'S':
                        {
                            tile.Content = contentFactory.Get(GameTileContentType.Spawn);
                            tile.transform.SetParent(spawnPointsGroupingObject.transform);
                            break;
                        }
                    case 'W': tile.Content = contentFactory.Get(GameTileContentType.Wall); break;
                    case 'T': tile.Content = contentFactory.Get(GameTileContentType.Tree); break;
                    case 'R': tile.Content = contentFactory.Get(GameTileContentType.Tree); break;
                    default: Debug.LogError($"Unknown type {data[x][y]} at [{x}][{y}]"); break;
                }

                if (data[y][x] != 'D' && data[y][x] != 'S')
                {
                    tile.transform.SetParent(othersGroupingObject.transform);
                }

                tile.Content.gameObject.transform.SetParent(tile.gameObject.transform);
            }
        }

        navmesh.BuildNavMesh();
    }
}