using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    START,
    LEVEL_ONE,
    LEVEL_TWO,
    LEVEL_THREE
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;


    // Each of these lists holds the gameobjects for each gameplay state
    public List<GameObject> startStateObjects;
    public List<GameObject> gameplayStateObjects;

    // honestly just laziness
    [SerializeField]
    GameObject levelTwoButton;

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
        if (gameState != GameState.START)
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
            gameState = GameState.LEVEL_ONE;
            foreach (GameObject obj in startStateObjects)
            {
                obj.SetActive(false);
            }

            // start level one
            gameplayStateObjects[0].SetActive(true);
            levelTwoButton.SetActive(false);


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
