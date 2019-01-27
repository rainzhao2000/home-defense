using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTile : MonoBehaviour
{
    public Tilemap map;

    private int tileType;

    private TileBase tile;

    private GameObject gameManager;
    private Vector3Int previousNull;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        tileType = gameManager.GetComponent<GameManager>().determineTileType(map);
        tile = gameManager.GetComponent<GameManager>().determineTile(map);
        Debug.Log("name:" + map.gameObject.gameObject.name + " id:" + tileType);
    }

    // do late so that the player has a chance to move in update if necessary
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (gameManager.GetComponent<GameManager>().placeable == 0)
            {
                placeTile(null, previousNull);
            } else if (gameManager.GetComponent<GameManager>().placeable == tileType)
            {
                placeTile(tile, gameManager.GetComponent<GameManager>().previousPos);
            }
        } else if (Input.GetMouseButton(1))
        {
            placeTile(null, previousNull);
        }
    }

    private void placeTile(TileBase temp, Vector3Int previous)
    {
        // get current grid location
        Vector3Int currentCell = map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // if the position has changed
        if (currentCell != previous)
        {
            // set the new tile
            map.SetTile(currentCell, temp);

            // save the new position for next frame
            previous = currentCell;
        }
    }
}
