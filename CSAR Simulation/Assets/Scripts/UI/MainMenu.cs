using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;

public class MainMenu : BasePanel
{
    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }
}
