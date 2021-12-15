using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool isGameStarted = false;// controlling is game started
    public static bool isGameEnded = false;// controlling is game ended

    public GameObject LevelCompletedPanel, LevelFailedPanel;
    public List<GameObject> Levels = new List<GameObject>();
    [SerializeField] int LevelIndex = 0;


    private void Awake()
    {
        if (instance == null)
            instance = this;

        CreateLevel();
    }

    public void CreateLevel()
    {
        LevelIndex = PlayerPrefs.GetInt("LevelNo", 0);
        if(LevelIndex> Levels.Count - 1)
        {
            LevelIndex = 0;
            PlayerPrefs.SetInt("LevelNo", 0);
        }

        Instantiate(Levels[LevelIndex]);

    }

    public void StartGame()
    {
        isGameStarted = true;
        isGameEnded = false;
        
    }

    public void EndGame()
    {
        isGameEnded = true;

    }

    /// <summary>
    /// When on level finished and completed succesfully
    /// </summary>
    public void OnLevelCompleted()
    {
        LevelCompletedPanel.SetActive(true);
    }


    /// <summary>
    /// When on level finished and failed
    /// </summary>
    public void OnLevelFailed()
    {
        LevelFailedPanel.SetActive(true);
        
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        LevelIndex++;
        PlayerPrefs.SetInt("LevelNo", LevelIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
