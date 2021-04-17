using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    [SerializeField]
    GameObject nextLevelButton;

    [SerializeField]
    Vector3 tileStartPosition;

    [SerializeField]
    float gap;

    [SerializeField]
    int columnCount;

    [SerializeField]
    int rowCount;

    [SerializeField]
    Tile startTile;

    List<GameObject> tiles;

    

    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<GameObject>();
        InstantiateTiles();
    }

    public void ActivateNextLevelButton()
    {
        nextLevelButton.SetActive(true);
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
                tiles.Add(newTile);

             
            }
        }
        // so horrible lol
        if (columnCount > 3)
        {
            int spacesToMove = columnCount - 3;
            transform.position = new Vector3(transform.position.x - (spacesToMove), transform.position.y, transform.position.z);
        }
        // so horrible lol
        if (rowCount > 3)
        {
            int spacesToMove = rowCount - 3;
            transform.position = new Vector3(transform.position.x, transform.position.y - (spacesToMove), transform.position.z);
        }
    }
    

    public void CheckWin()
    {
        startTile.CheckMyNeighbours();
    }

    public void ClearCheckedFlags()
    {
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<Tile>().hasBeenCheckedThisRound = false;
        }
    }
}
