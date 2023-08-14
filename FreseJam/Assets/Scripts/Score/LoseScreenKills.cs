using TMPro;
using UnityEngine;

public class LoseScreenKills : MonoBehaviour
{
    private string playerName;
    [SerializeField] private TextMeshProUGUI killsVisual;
    private int kills = 0;

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
        Debug.Log(PlayerPrefs.GetInt("highscore"));
        if (kills > PlayerPrefs.GetInt("highscore"))
        {
            if (playerName != null)
            {
                PlayerPrefs.SetInt("highscore", kills);
                HighScores.UploadScore(playerName, kills);
            }
        }
    }
    public void ShowKills()
    {
        killsVisual.text = "x" + kills;
        if(GameManager.Instance)
            GameManager.Instance.DestroyGameManager();
    }
}
