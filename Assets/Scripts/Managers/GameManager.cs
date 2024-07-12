using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int savedLevelIndex;
    public GameObject pauseMenuUI;
/*    public GameObject LevelsUI;
*/    private bool isPaused = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI totaScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI DeadMenuHighScore;
    int score = 0;
Enemy_Manager enemyManager;
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
    private void Awake()
    {
        enemyManager = GetComponent<Enemy_Manager>();
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
       
*/
    public void Restart()
    {

        SceneManager.LoadScene(savedLevelIndex);
        
        Enemy_Manager.score = 0;
        UpdateScoreText(Enemy_Manager.score);

        if (enemyManager != null)
        {
            enemyManager.ResetTotalScore();
        }


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


    public void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        UpdateTotalScoreText(score);
    }

    public void UpdateTotalScoreText(int score)
    {
        if (totaScoreText != null) 
        {
             totaScoreText.text =   "Enemies Annihilated: " + score.ToString();
        }

    }

    public void UpdateHighScoreText(int highScore)
    {
        if (highScoreText != null) 
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }
    public void HighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
            DeadMenuHighScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
    }
}
