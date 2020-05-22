using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using GlobalParameters;

public class MainMenu : BasePanel
{
    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }

    public void OnRunButtonPressed()
    {
        gameObject.SetActive(false);
        SimulationRun.runMode = RunMode.run;
    }
}
