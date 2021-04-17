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

public enum SkillLevel
{
    BAD,
    GOOD,
    EXTRA
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


    // gameplay
    float timeLeft;


    // UI Labels
    public TextMeshProUGUI timeLeftLabel;
    public TextMeshProUGUI timeLeftNum;

    private bool hasGameStarted;
    public SkillLevel skillLevel;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize to the start scene
        gameState = GameState.START;


        // default if user didnt pick dropdown value
        timeLeft = 30.0f;

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


    public void SetSkillLevel(int dropdownValue)
    {
        switch (dropdownValue)
        {
            case 0:
                skillLevel = SkillLevel.BAD;
                break;
            case 1:
                skillLevel = SkillLevel.GOOD;
                break;
            case 2:
                skillLevel = SkillLevel.EXTRA;
                break;
        }
        SetTimer();
    }


    public void SetTimer()
    {
        switch(skillLevel)
        {
            case SkillLevel.BAD:
                timeLeft = 30.0f;
                break;
            case SkillLevel.GOOD:
                timeLeft = 60.0f;
                break;
            case SkillLevel.EXTRA:
                timeLeft = 1000.0f;
                break;
        }
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
            timeLeftLabel.gameObject.SetActive(true);
            timeLeftNum.gameObject.SetActive(true);


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
