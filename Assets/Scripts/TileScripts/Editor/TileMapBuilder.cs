/******************************************************************
*    Author: Marissa Moser
*    Contributors: 
*    Date Created: August 31, 2024
*    Description: A custom editor window to build a map of tiles.
*******************************************************************/
using System.Collections.Generic;
using System.Drawing.Printing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TileMapBuilder : EditorWindow
{
    Vector2Int _mapSize = new(9, 9);
    GameObject _defaultTilePrefab;
    GameObject _tileParentObj;

    /// <summary>
    /// Function that allows the Tile Map Builder to have its own window. Access it
    /// by going to the menu/tools/Tile Map Builder.
    /// </summary>
    [MenuItem("Tools/Tile Map Builder")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TileMapBuilder));
    }

    /// <summary>
    /// Sets up the fields and buttons in the custom window.
    /// </summary>
    private void OnGUI()
    {
        //serialized fields
        _mapSize = EditorGUILayout.Vector2IntField("Map Size", _mapSize);

        _defaultTilePrefab = EditorGUILayout.ObjectField("Default Tile Prefab",
            _defaultTilePrefab, typeof(GameObject), false) as GameObject;

        _tileParentObj = EditorGUILayout.ObjectField("Tile Parent", _tileParentObj, typeof(GameObject), true) as GameObject;

        //buttons
        EditorGUILayout.Space();
        if (GUILayout.Button("Create New Map"))
        {
            CreateNewTileMap();
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Clear Map"))
        {
            ClearTileMap();
        }
    }

    /// <summary>
    /// Function to create a new map of tiles based on _mapSize with a border of
    /// empty "hole" tiles around it.
    /// </summary>
    private void CreateNewTileMap()
    {
        ClearTileMap();

        //make empty parents
        _tileParentObj = new GameObject("Tile Parent");
        _tileParentObj.transform.position = Vector3.zero;

        //generate tiles
        for (int i = 0; i < _mapSize.x; i++)
        {
            for (int j = 0; j < _mapSize.y; j++)
            {
                //Create tiles for map
                GameObject go = Instantiate(_defaultTilePrefab, new Vector3(i, 0, j),
                    Quaternion.identity, _tileParentObj.transform);

                //Set up tile's fields
                go.name = i + "," + j + " " + go.name;
                Tile tile = go.GetComponent<Tile>();
                tile.SetCoordinates(new Vector2(i, j));
            }
        }
    }

    /// <summary>
    /// Clears the current tile map.
    /// </summary>
    private void ClearTileMap()
    {
        DestroyImmediate(_tileParentObj);
    }
}
