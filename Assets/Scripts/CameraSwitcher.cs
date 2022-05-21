using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public const int OverviewScene = 0;

    public Camera[] Cameras;
    public GameObject[] SceneUI;
    public GameObject AllScenesUI;

    public int ActiveScene { get; private set; } = 1;
    public int LastScene => Cameras.Length - 1;

    public void Start()
    {
        if (Cameras.Length != SceneUI.Length)
        {
            throw new Exception("Camera Count and SceneUI Count missmatch!");
        }

        Reset();
    }

    public void Reset()
    {
        ActiveScene = 1;
        EnableScene(OverviewScene);

        for (int i = 1; i < Cameras.Length; i++)
        {
            DisableScene(i);
        }
    }

    public void SwitchToOverview()
    {
        DisableScene(ActiveScene);
        EnableScene(OverviewScene);
    }

    public void SwitchToActiveScene()
    {
        DisableScene(OverviewScene);
        EnableScene(ActiveScene);
    }

    public void SwitchToNextScene()
    {
        if (ActiveScene == LastScene)
        {
            return;
        }
        
        DisableScene(ActiveScene);
        ActiveScene += 1;

        DisableScene(OverviewScene);
        EnableScene(ActiveScene);
    }

    private void EnableScene(int sceneId)
    {
        SceneUI[sceneId].SetActive(true);
        Cameras[sceneId].enabled = true;
        AllScenesUI.SetActive(sceneId != OverviewScene && sceneId != LastScene);
    }
    
    private void DisableScene(int sceneId)
    {
        SceneUI[sceneId].SetActive(false);
        Cameras[sceneId].enabled = false;
    }
}
