using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private List<Objective> objectives = new List<Objective>();

    [SerializeField] private Transform player;
    [SerializeField] private AudioSource[] dieSounds;
    [SerializeField] private TextMeshProUGUI crewCount;
    [SerializeField] private TextMeshProUGUI killCount;
    [SerializeField] public GameObject bloodFX;
    
    private int playerKills = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var objective in GameObject.FindObjectsOfType<Objective>())
        {
            objectives.Add(objective);
        }
    }

    private void Start()
    {
        crewCount.text = "x" + objectives.Count;
        killCount.text = "x" + playerKills;
    }

    public void SpawnBloodParticleOnDeadCrewMember(Transform crewmemberPosition)
    {
        Instantiate(bloodFX, crewmemberPosition.position, crewmemberPosition.rotation);
    }

    public void IncreaseKillCount()
    {
        playerKills += 1;
        killCount.text = "x" + playerKills;
    }

    public void UpdateCrew()
    {
        crewCount.text = "x" + objectives.Count;
        if (objectives.Count <= 0)
        {
            SceneManager.LoadScene("Lose");
            Destroy(gameObject);
        }
    }

    public int GetKillAmount()
    {
        return playerKills;
    }

    public void DestroyGameManager()
    {
        Destroy(this.gameObject);
    }
    public Transform PlayersLocation()
    {
        return player;
    }

    //ObjectiveGetsDestroyed
    public void ObjectiveDestroyed(Objective destroyedObjective)
    {
        if (destroyedObjective)
        {
            objectives.Remove(destroyedObjective);
            Destroy(destroyedObjective.gameObject);
        }

    }

    public void InstantiatePeopleDieSound()
    {
        int randomIndex = Random.Range(0, dieSounds.Length);
        dieSounds[randomIndex].Play();
    }

    public Objective AssignRandomObjective()
    {
        UpdateCrew();
        if (objectives.Count == 0) return null;

        int randomIndex = Random.Range(0, objectives.Count);
        return objectives[randomIndex];
    }
}