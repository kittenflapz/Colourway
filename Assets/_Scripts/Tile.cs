using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Renderer tileRenderer;
    List<Color> colors;

    TileManager tileManager;

    [SerializeField]
    bool isStartTile;

    [SerializeField]
    bool isEndTile;

    [SerializeField]
    int startColorIndex;

    public bool hasBeenCheckedThisRound = false;

    int colorIndex;
    private void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
        tileRenderer = GetComponent<Renderer>();
        colors = new List<Color>() 
        { 
            Color.red,
            Color.blue,
            Color.yellow,
            Color.green,
            Color.white,
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isStartTile && !isEndTile)
        {
            colorIndex = Random.Range(1, colors.Count);
        }
        else
        {
            colorIndex = startColorIndex;
        }

        SetTileColor(colorIndex);
    }


    private void OnMouseDown()
    {
        if (!isStartTile && !isEndTile)
        {
            tileManager.ClearCheckedFlags();
            IncrementColorIndex();
            SetTileColor(colorIndex);
            tileManager.CheckWin();
        }
    }
    
    private void SetTileColor(int colorIndex)
    {
        tileRenderer.material.SetColor("_Color", colors[colorIndex]);
    }

    // handles color index going over number of colors in list (by starting back at 0)
    private void IncrementColorIndex()
    {
        if (colorIndex < colors.Count - 1)
        {
            colorIndex++;
        }
        else
        {
            colorIndex = 0;
        }
    }

   public void CheckMyNeighbours()
    { 
        CheckNeighbour(Direction.NORTH);
        CheckNeighbour(Direction.EAST);
        CheckNeighbour(Direction.SOUTH);
        CheckNeighbour(Direction.WEST);
    }

    public Color GetColor()
    {
        return colors[colorIndex];
    }

    // sorry
    private void CheckNeighbour(Direction direction)
    {
       
        print("start of check funciton");
        hasBeenCheckedThisRound = true;
        Vector3 targetPosition = Vector3.zero;
        RaycastHit raycastHit;
        switch (direction)
        {
            case Direction.NORTH:
                targetPosition = new Vector3(transform.position.x, transform.position.y + 3, 0);
                break;
            case Direction.EAST:
                targetPosition = new Vector3(transform.position.x + 3, transform.position.y, 0);
                break;
            case Direction.SOUTH:
                targetPosition = new Vector3(transform.position.x, transform.position.y - 3, 0);
                break;
            case Direction.WEST:
                targetPosition = new Vector3(transform.position.x - 3, transform.position.y, 0);
                break;
        }
        Debug.DrawLine(transform.position, targetPosition, Color.red);
        if (Physics.Linecast(transform.position, targetPosition, out raycastHit))
        {
            print("linecast hit");
            Tile tileHit = raycastHit.transform.gameObject.GetComponent<Tile>();

            if (tileHit.hasBeenCheckedThisRound == false)
            {
                print("checking");
                if (tileHit.GetColor() == this.GetColor())
                {
                      print("i am tile at " + transform.position + " and there is a tile to my " + direction + " that is the same color as me");
                
                    if (tileHit.isEndTile)
                    {
                        print("win");
                        tileManager.ActivateNextLevelButton();
                        return;
                    }
                    tileHit.CheckMyNeighbours();
                }
            }
            
        }
    }


}


// cardinal directions because then we don't have to ask 'who's left? mine or yours?' 
// and also sounds smortor
public enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}
