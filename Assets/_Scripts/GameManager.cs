using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    START,
    GAMEPLAY
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;


    // Each of these lists holds the gameobjects for each gameplay state
    public List<GameObject> startStateObjects;
    public List<GameObject> gameplayStateObjects;

    // UI Labels
    //public TextMeshProUGUI currentModeText;
    //public TextMeshProUGUI currentPoopNumText;
    //public TextMeshProUGUI finalPoopNumText;
    //public TextMeshProUGUI currentScansLeftText;
    //public TextMeshProUGUI currentScoopsLeftText;
    //public TextMeshProUGUI noScansLeftText;
    //public TextMeshProUGUI noScoopsLeftText;

    private bool hasGameStarted;

  //  public TileManager tileManager;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize to the start scene
        gameState = GameState.START;

        // This is here so I can have whatever game objects I need be active/inactive in the editor
        // and not have to mess about before hitting play
        foreach (GameObject obj in startStateObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in gameplayStateObjects)
        {
            obj.SetActive(false);
        }

        hasGameStarted = false;
    }

    // kinda lomg
    public void ToggleGameState()
    {
        if (gameState == GameState.GAMEPLAY)
        {
            gameState = GameState.START;
            foreach (GameObject obj in startStateObjects)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in gameplayStateObjects)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            gameState = GameState.GAMEPLAY;
            foreach (GameObject obj in startStateObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in gameplayStateObjects)
            {
                obj.SetActive(true);
            }

            if (!hasGameStarted)
            {
                // initialize whatever the tilemanager needs to initialize
            }

        }
    }

    public void RestartEverything()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
 
}
