using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup[] panels;

    public void GoTo(CanvasGroup Panel) 
    {
        foreach (var p in panels) 
        {
            p.alpha = 0f;
            p.blocksRaycasts = false;
            p.interactable = false;

        }

        Panel.alpha = 1;
        Panel.blocksRaycasts = true;
        Panel.interactable = true;
    }

   

}
