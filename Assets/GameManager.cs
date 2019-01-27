using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public GameObject housesMap;
    public GameObject wallsMap;

    public int placeable = 0;

    public Vector3Int previousPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            placeable = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            placeable = 2;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public int determineTileType(Tilemap map)
    {
        if (map.name == housesMap.name)
        {
            return 1;
        } else if (map.gameObject.gameObject.name == wallsMap.name)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    public TileBase determineTile(Tilemap map)
    {
        if (map.name == housesMap.name)
        {
            return Resources.Load<TileBase>("house tile");
        }
        else if (map.name == wallsMap.name)
        {
            return Resources.Load<TileBase>("wall tile");
        }
        else
        {
            return Resources.Load<TileBase>("house tile");
        }
    }

}
