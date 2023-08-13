using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cam_Switch : MonoBehaviour
{
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;

    private void Awake()
    {
        camera1.enabled = true;
        camera2.enabled = false;
    }

    public void TransitionCam()
    {
        camera1.enabled = false;
        camera2.enabled = true;
        StartCoroutine(TransitionScene());
    }

    IEnumerator TransitionScene()
    {
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene("GameScene");
    }
}
