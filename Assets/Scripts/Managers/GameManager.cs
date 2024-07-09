


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int savedLevelIndex;
    public GameObject pauseMenuUI;
/*    public GameObject LevelsUI;
*/    private bool isPaused = false; 


    // Start is called before the first frame update
    public void Start()
    {
/*        LevelsUI.SetActive(false);
*/        // Save the current level index only if it's not the Main Menu
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }

        // Load the saved level index
        savedLevelIndex = PlayerPrefs.GetInt("SavedLevel", 1); // Default to endless scene if not found

    }
    public void NextLevel()
    {
        SceneManager.LoadScene(savedLevelIndex + 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Main Menu")
            {

            }
            else
            {
                if (isPaused) { Resume(); }
                else { Pause(); }
            }
        }

    }

    public void Endless()
    {
        SceneManager.LoadScene("Endless");
    }

    public void Continue()
    {
        // Load the saved level
        savedLevelIndex = PlayerPrefs.GetInt("SavedLevel", 1); // Default to endless scene if not found
        SceneManager.LoadScene(savedLevelIndex);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level_1");
    }

   /* public void Levels() //To be Implemented
    {
        LevelsUI.SetActive(true);
    }*/

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        Debug.Log("IsPaused: in Resume()" + isPaused);

        /*        FindObjectOfType<SpawnManager>().ResumeSpawning();
        */
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
        Debug.Log("IsPaused in pause(): " + isPaused);

        /*        FindObjectOfType<SpawnManager>().PauseSpawning();
        */
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    /*public void NextLevel()
    {
        SceneManager.LoadScene(savedLevelIndex + 1);
    }
*/
    public void Restart()
    {
        SceneManager.LoadScene(savedLevelIndex);
    }

    public void LevelsGoBack()
    {
        /*   LevelsUI.SetActive(false);*/
        SceneManager.LoadScene("Main Menu");
    }
    public bool GetIsPaused()
    {
        return isPaused;
    }

}
