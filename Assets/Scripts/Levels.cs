using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Levels are incremented in Spawn Manager after every level, UnlockedLevel variable is incremeneted in the UnlockNextLevel method in the spawn_manager
//based on the unlocked level, the buttons are getting interactable and uninteractable.
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
/*        string levelName = "Level_" + levelID;
*/
        int level =  levelID;
        SceneManager.LoadScene(level);
    }


    public void MainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}