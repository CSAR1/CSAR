using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;

public class RunModeButtons : MonoBehaviour
{
    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }

    public void OnQuitPressed()
    {
        OnPushPanel("Warning");
    }
}
