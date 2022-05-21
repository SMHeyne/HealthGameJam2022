using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCredits : MonoBehaviour
{
    public void Toggle()
    {
        if (UI_credits.SetActive(false))
        {
            UI_credits.SetActive(true);
        }else
        {
            UI_credits.SetActive(false);
        }
    }
}
