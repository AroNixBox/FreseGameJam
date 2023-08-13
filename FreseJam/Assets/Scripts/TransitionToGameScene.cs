using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToGameScene : MonoBehaviour
{
    public void TransitionScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
