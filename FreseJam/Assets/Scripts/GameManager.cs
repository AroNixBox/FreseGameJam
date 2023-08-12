using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private List<Objective> objectives = new List<Objective>();

    [SerializeField] private Transform player;
    [SerializeField] private AudioSource[] dieSounds;

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
        if (objectives.Count == 0) return null;

        int randomIndex = Random.Range(0, objectives.Count);
        return objectives[randomIndex];
    }
}