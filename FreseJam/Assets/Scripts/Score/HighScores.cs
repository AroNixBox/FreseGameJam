using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HighScores : MonoBehaviour
{
    const string privateCode = "E2UYf8AX0Uymy_fmgNJkDwggbWQ4yLykOSv6jCDInjEw";
    const string publicCode = "64daa16e778d3cc2703ef0a9";
    const string webURL = "https://www.dreamlo.com/lb/";

    public PlayerScore[] scoreList;
    DisplayHighscores myDisplay;

    static HighScores instance;
    void Awake()
    {
        instance = this;
        myDisplay = GetComponent<DisplayHighscores>();
    }
    
    public static void UploadScore(string username, int score)
    {
        instance.StartCoroutine(instance.DatabaseUpload(username,score));
    }

    IEnumerator DatabaseUpload(string userame, int score)
    {
        UnityWebRequest www = UnityWebRequest.Get(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(userame) + "/" + score);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            print("Error uploading: " + www.error);
        }
        else
        {
            print("Upload Successful");
            DownloadScores();
        }
    }

    public void DownloadScores()
    {
        StartCoroutine("DatabaseDownload");
    }
    IEnumerator DatabaseDownload()
    {

        UnityWebRequest www = UnityWebRequest.Get(webURL + publicCode + "/pipe/0/10");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            print("Error downloading: " + www.error);
        }
        else
        {
            OrganizeInfo(www.downloadHandler.text);
            myDisplay.SetScoresToMenu(scoreList);
        }
    }

    void OrganizeInfo(string rawData)
    {
        string[] entries = rawData.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        scoreList = new PlayerScore[entries.Length];
        for (int i = 0; i < entries.Length; i ++)
        {
            string[] entryInfo = entries[i].Split(new char[] {'|'});
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            scoreList[i] = new PlayerScore(username,score);
            print(scoreList[i].username + ": " + scoreList[i].score);
        }
    }
}

public struct PlayerScore
{
    public string username;
    public int score;

    public PlayerScore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}