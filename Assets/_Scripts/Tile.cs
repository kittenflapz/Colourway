using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Renderer tileRenderer;

    List<Color> colors;

    int colorIndex;
    private void Awake()
    {
        tileRenderer = GetComponent<Renderer>();
        colors = new List<Color>() 
        { 
            Color.red,
            Color.blue,
            Color.yellow,
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        colorIndex = Random.Range(0, colors.Count);
        SetTileColor(colorIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        IncrementColorIndex();
        SetTileColor(colorIndex);
        CheckMyNeighbours();
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
        //todo: loop dis

        CheckNeighbour(Direction.NORTH);
        CheckNeighbour(Direction.EAST);
        CheckNeighbour(Direction.SOUTH);
        CheckNeighbour(Direction.WEST);

    }

    public Color GetColor()
    {
        return colors[colorIndex];
    }

    private void CheckNeighbour(Direction direction)
    {
        Vector3 targetPosition = Vector3.zero;
        RaycastHit raycastHit;
        switch (direction)
        {
            case Direction.NORTH:
                targetPosition = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                break;
            case Direction.EAST:
                targetPosition = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
                break;
            case Direction.SOUTH:
                targetPosition = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
                break;
            case Direction.WEST:
                targetPosition = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
                break;
        }

        if (Physics.Linecast(transform.position, targetPosition, out raycastHit))
        {
            Tile tileHit = raycastHit.transform.gameObject.GetComponent<Tile>();
            if (tileHit.GetColor() == this.GetColor())
            {
                print("To this tile's " + direction.ToString() + " there is a tile that is the same color");
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
