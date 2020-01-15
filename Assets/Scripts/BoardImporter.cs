using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardImporter
{
    [MenuItem("Map Importer/Import Board")]
    static void ImportBoard()
    {
        string path = EditorUtility.OpenFilePanel("Choose board file", "Resources", "map");
        Selection.activeGameObject.GetComponent<GameBoard>().ReadBoardFromText(path);
    }

    [MenuItem("Map Importer/Clear Board")]
    static void ClearBoard()
    {
        Selection.activeGameObject.GetComponent<GameBoard>().ClearBoard();
    }

    [MenuItem("Map Importer/Clear Tiles")]
    static void ClearTiles()
    {
        Selection.activeGameObject.GetComponent<GameBoard>().ClearTiles();
    }

    [MenuItem("Map Importer/Export Board")]
    static void ExportBoard()
    {
        string path = EditorUtility.SaveFilePanel("Choose board file", "Resources", "exported_board", "map");
        Selection.activeGameObject.GetComponent<GameBoard>().SaveToText(path);
    }
}
