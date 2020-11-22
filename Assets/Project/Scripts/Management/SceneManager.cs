using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject playerInfoPanel;
    public GameObject gameEndPanel;
    public GameObject menuPanel;
    public GameObject dialogueMessage;
    public Text dialogueText;
    public GameObject controlsInfo;
    public GameObject creditsInfo;

    public Image mailFiller, foodFiller, vgFiller;

    private void Start()
    {
        playerInfoPanel.gameObject.SetActive(true);
    }
    public void DialogueShow(Story story)
    {
        dialogueText.text = $"\"{story.text}\" - {story.title}.";
        Time.timeScale = 0;
        dialogueMessage.SetActive(true);

        var player = GlobalConstants.Player;
        if (player != null)
        {
            var sources = player.GetComponents<AudioSource>();
            foreach (var src in sources) src.enabled = false;
        }
    }

    public void DialogueClose()
    {
        dialogueMessage.SetActive(false);
        Time.timeScale = 1;
        var stash = GlobalConstants.Player.GetComponent<PlayerStash>();
        GlobalConstants.Player.SendMessage(nameof(stash.ContinueStory));

        var player = GlobalConstants.Player;
        if (player != null)
        {
            var sources = player.GetComponents<AudioSource>();
            foreach (var src in sources) src.enabled = true;
        }
    }

    public void PauseControl()
    {
        if(Time.timeScale == 1)
        {
            pausePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.gameObject.SetActive(false);
        }
    }

    public void GameEndControl()
    {
        if (Time.timeScale == 1)
        {
            gameEndPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            gameEndPanel.gameObject.SetActive(false);
        }
    }

    public void MenuControl()
    {
        if (Time.timeScale == 1)
        {
            menuPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            menuPanel.gameObject.SetActive(false);
        }
    }
    public void ControlsInfo()
    {
        if (controlsInfo.gameObject.activeInHierarchy == false)
        {
            controlsInfo.gameObject.SetActive(true);            
        }
        else
        {
            controlsInfo.gameObject.SetActive(false);
        }
    }
    public void CreditsInfo()
    {
        if (creditsInfo.gameObject.activeInHierarchy == false)
        {
            creditsInfo.gameObject.SetActive(true);
         }
        else
        {
           creditsInfo.gameObject.SetActive(false);
        }
    }

    public void GoToMenu()
    {
        menuPanel.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
