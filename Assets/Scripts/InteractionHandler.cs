using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public const int OverviewSceneId = 0;

    private MentalHealthCounter MentalHealthCounter;

    public Camera[] Cameras;
    public GameObject[] SceneUI;
    public GameObject SharedScenesUI;

    public Sprite[] HappySprites;
    public Sprite[] NeutralSprites;
    public Sprite[] DarkSprites;

    public SpriteRenderer[] SceneRenderer;

    public GameObject NextSceneButton;

    public int ActiveSceneId { get; private set; } = 1;
    public int LastSceneId => Cameras.Length - 1;

    public void Start()
    {
        if (Cameras.Length != SceneUI.Length)
        {
            throw new Exception("Camera Count and SceneUI Count mismatch!");
        }

        Reset();
    }

    public void Reset()
    {
        ActiveSceneId = 1;
        MentalHealthCounter = new();

        EnableScene(OverviewSceneId);
        NextSceneButton.SetActive(false);

        for (int i = 1; i < Cameras.Length; i++)
        {
            DisableScene(i);
        }
    }

    public void SwitchToOverview()
    {
        DisableScene(ActiveSceneId);
        EnableScene(OverviewSceneId);
    }

    public void SwitchToActiveScene()
    {
        DisableScene(OverviewSceneId);
        EnableScene(ActiveSceneId);
    }

    public void SwitchToNextScene()
    {
        if (ActiveSceneId == LastSceneId)
        {
            return;
        }
        
        DisableScene(ActiveSceneId);
        ActiveSceneId += 1;

        NextSceneButton.SetActive(ActiveSceneId == 3);

        DisableScene(OverviewSceneId);
        EnableScene(ActiveSceneId);
    }

    private void EnableScene(int sceneId)
    {
        SceneUI[sceneId].SetActive(sceneId == OverviewSceneId || !NextSceneButton.activeSelf);
        Cameras[sceneId].enabled = true;
        SharedScenesUI.SetActive(sceneId != OverviewSceneId && sceneId != LastSceneId);
    }
    
    private void DisableScene(int sceneId)
    {
        SceneUI[sceneId].SetActive(false);
        Cameras[sceneId].enabled = false;
    }

    public void OnButtonHappyClicked()
    {
        SceneRenderer[ActiveSceneId].sprite = HappySprites[ActiveSceneId];
        ChangeUIForProgress();
    }

    public void OnButtonNeutralClicked()
    {
        SceneRenderer[ActiveSceneId].sprite = NeutralSprites[ActiveSceneId];
        MentalHealthCounter.AddOnePoint();
        ChangeUIForProgress();
    }

    public void OnButtonDarkClicked()
    {
        SceneRenderer[ActiveSceneId].sprite = DarkSprites[ActiveSceneId];
        MentalHealthCounter.AddTwoPoints();
        ChangeUIForProgress();
    }

    private void ChangeUIForProgress()
    {
        SceneUI[ActiveSceneId].SetActive(false);
        NextSceneButton.SetActive(true);
    }
}

class MentalHealthCounter
{
    public int MentalHealthPoints { get; private set; } = 0;

    public void Reset()
    {
        MentalHealthPoints = 0;
    }

    public void AddOnePoint()
    {
        MentalHealthPoints += 1;
    }

    public void AddTwoPoints()
    {
        MentalHealthPoints += 2;
    }
}

