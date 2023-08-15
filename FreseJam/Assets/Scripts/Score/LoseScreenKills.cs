using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseScreenKills : MonoBehaviour
{
    private string playerName;
    [SerializeField] private TextMeshProUGUI killsVisual;
    private int kills = 0;
    private int maxScores = 6;
    private bool hasSubmittedAlready;

    private void Awake()
    {
        kills = GameManager.Instance.GetKillAmount();
        ShowKills();
    }
    public void ReadStringInput(string s)
    {
        playerName = s;

    }

    public void SendScore()
    {
        if (hasSubmittedAlready)
            return;
        
        int[] highscores = new int[maxScores];
        for (int i = 0; i < maxScores; i++)
        {
            highscores[i] = PlayerPrefs.GetInt("highscore" + i, 0);
            Debug.Log(highscores[i]);
        }

        List<int> scoresList = new List<int>(highscores);
        scoresList.Add(kills);
        scoresList.Sort();
        scoresList.Reverse();

        if (scoresList.Count > maxScores)
        {
            scoresList.RemoveAt(scoresList.Count - 1);
        }

        for (int i = 0; i < scoresList.Count; i++)
        {
            PlayerPrefs.SetInt("highscore" + i, scoresList[i]);
        }

        if (kills > scoresList[scoresList.Count - 1] && playerName != null)
        {
            hasSubmittedAlready = true;
            HighScores.UploadScore(playerName, kills);
        }
    }
    public void ShowKills()
    {
        killsVisual.text = "x" + kills;
        if(GameManager.Instance)
            GameManager.Instance.DestroyGameManager();
    }
}
