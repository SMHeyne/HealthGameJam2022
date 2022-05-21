using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCredits : MonoBehaviour
{
    public GameObject UI_credits;
    public void Toggle()
    {
        UI_credits.SetActive(!UI_credits.activeSelf);
        
    }
}
