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
    public TextMeshProUGUI youLostLabel;
    public TextMeshProUGUI skillLevelLabel;
    public GameObject startAgainButton;

    private bool hasGameStarted;
    public SkillLevel skillLevel;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize to the start scene
        gameState = GameState.START;
        timeLeftLabel.gameObject.SetActive(false);
        timeLeftNum.gameObject.SetActive(false);
        skillLevelLabel.gameObject.SetActive(false);


        SetTimer();

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

    private void Update()
    {
        if (gameState != GameState.START)
        {
            decrementTimer();
            if (timeLeft < 0)
            {
                print("lose");
                Lose();
            }
        }
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
                skillLevelLabel.SetText("skill level: bad");
                break;
            case SkillLevel.GOOD:
                timeLeft = 60.0f;
                skillLevelLabel.SetText("skill level: good");
                break;
            case SkillLevel.EXTRA:
                timeLeft = 1000.0f;
                skillLevelLabel.SetText("skill level: xtra");
                break;
        }
    }

    private void decrementTimer()
    {
        timeLeft -= Time.deltaTime;
        timeLeftNum.SetText(Mathf.Floor(timeLeft).ToString());
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

            timeLeftLabel.gameObject.SetActive(false);
            timeLeftNum.gameObject.SetActive(false);
            skillLevelLabel.gameObject.SetActive(false);
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
            skillLevelLabel.gameObject.SetActive(true);

        }
    }

    public void RestartEverything()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Lose()
    {
        foreach (GameObject obj in gameplayStateObjects)
        {
            obj.SetActive(false);
       
        }
        youLostLabel.gameObject.SetActive(true);
        startAgainButton.SetActive(true);
        timeLeftLabel.gameObject.SetActive(false);
        timeLeftNum.gameObject.SetActive(false);
    }
 
}
