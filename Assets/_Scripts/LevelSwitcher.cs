using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    // levels in the game are pretty much just game objects - their children make up each 'level'
    // this would be more extensible if it were just one function that took two game objects but
    // hooking that up to a button seems non trivial and i just want this to be over honestly
    public GameObject startStuff;
    public GameObject levelOne;
    public GameObject levelTwo;
    public GameObject levelThree;


    GameManager gameManager;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartLevelTwo()
    {
        levelOne.SetActive(false);
        levelTwo.SetActive(true);
        gameManager.SetTimer();
    }

    public void StartLevelThree()
    {
        levelTwo.SetActive(false);
        levelThree.SetActive(true);
        gameManager.SetTimer();
    }

    public void StartAgain()
    {
       gameManager.SetTimer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }
}
