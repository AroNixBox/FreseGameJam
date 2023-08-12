using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private List<Transform> objectives = new List<Transform>();

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
            objectives.Add(objective.transform);
        }
    }

    //ObjectiveGetsDestroyed
    public void ObjectiveDestroyed(Objective destroyedObjective)
    {
        objectives.Remove(destroyedObjective.transform);
    }

    public Transform AssignRandomObjective()
    {
        if (objectives.Count == 0) return null;

        int randomIndex = Random.Range(0, objectives.Count);
        return objectives[randomIndex];
    }
}