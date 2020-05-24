using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using GlobalParameters;
using UnityEngine.UI;

public class RunModeButtons : MonoBehaviour
{
    public Text pauseText;
    private bool switchOn = true;

    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }

    public void OnQuitPressed()
    {
        OnPushPanel("Warning");
        SimulationRun.runMode = RunMode.pause;
    }

    public void OnPausePressed()
    {
        if (switchOn)
        {
            pauseText.text = "继续仿真";
            SimulationRun.runMode = RunMode.pause;
        }
        if (!switchOn)
        {
            pauseText.text = "暂停仿真";
            SimulationRun.runMode = RunMode.run;
        }
        switchOn = !switchOn;
    }
}
