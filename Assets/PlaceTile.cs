using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTile : MonoBehaviour
{
    public Tilemap map;
    public TileBase tile = null;

    private Vector3Int previous;
    private Vector3Int previousNull;

    // do late so that the player has a chance to move in update if necessary
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) {
            tile = Resources.Load<TileBase>("house tile");
        } else if (Input.GetKey(KeyCode.Alpha2)) {
            tile = Resources.Load<TileBase>("wall tile");
        }

        if (Input.GetMouseButton(0))
        {
            placeTile(tile, previous);
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
