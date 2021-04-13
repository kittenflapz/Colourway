﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    [SerializeField]
    Vector3 tileStartPosition;

    [SerializeField]
    float gap;

    [SerializeField]
    int columnCount;

    [SerializeField]
    int rowCount;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateTiles()
    {
        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                Vector3 newTilePosition = new Vector3(tileStartPosition.x + (i * gap), tileStartPosition.y + (j * gap), 0);
               GameObject newTile = Instantiate(tilePrefab, newTilePosition, Quaternion.identity);
                newTile.transform.Rotate(Vector3.left, 90);
                newTile.transform.parent = this.transform;
            }
        }
    }
}