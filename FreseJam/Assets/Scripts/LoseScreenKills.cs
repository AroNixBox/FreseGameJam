using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseScreenKills : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killsVisual;
    private int kills = 0;

    private void Awake()
    {
        kills = GameManager.Instance.GetKillAmount();
        ShowKills();
    }


    public void ShowKills()
    {
        killsVisual.text = "x" + kills;
        if(GameManager.Instance)
            GameManager.Instance.DestroyGameManager();
    }
}
