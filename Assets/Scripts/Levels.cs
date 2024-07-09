/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Levels : MonoBehaviour
{

    public Button[] levelButtons; // List of buttons for each level

    // Start is called before the first frame update
    void Start()
    {
        // Initialize level buttons
        InitializeLevelButtons();
    }

    private void InitializeLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Default to Level_1 unlocked

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < unlockedLevel)
            {
                int levelIndex = i + 2; // Assuming Level_1 has index 2, Level_2 has index 3, etc.
                levelButtons[i].interactable = true;
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    private void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void UnlockNextLevel(int completedLevelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // If the completed level is the highest unlocked level, unlock the next one
        if (completedLevelIndex >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", completedLevelIndex + 1);
            PlayerPrefs.Save();
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelMenu : MonoBehaviour
{

    public Button[] buttons;
    private void Awake()
    {




        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }


    }

    public void OpenLevel(int levelID)
    {
        string levelName = "Level_" + levelID;
        SceneManager.LoadScene(levelName);
    }


    public void MainMenu(string sceneName)
    {
        string levelName = sceneName;
        SceneManager.LoadScene(levelName);
    }
}
