using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject CreditsUI;
    [SerializeField] private GameObject OtherStuff;
    public void PlayGame()
    {
        SceneManager.LoadScene("GameAnim");
    }

    public void ShowCredits()
    {
        if (CreditsUI)
        {
            CreditsUI.SetActive(true);
            OtherStuff.SetActive(false);
        }
    }
    public void DisableCredits()
    {
        if (CreditsUI)
        {
            CreditsUI.SetActive(false);
            OtherStuff.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}
