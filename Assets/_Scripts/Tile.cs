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
}
