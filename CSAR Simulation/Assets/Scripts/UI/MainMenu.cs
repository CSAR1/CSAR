using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using GlobalParameters;
using UnityEngine.UI;

public class MainMenu : BasePanel
{
    GameObject RunModeButtons;

    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }

    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }

    public void OnRunButtonPressed()
    {
        gameObject.SetActive(false);
        SimulationRun.runMode = RunMode.run;
        if (RunModeButtons == null)
        {
            RunModeButtons = Object.Instantiate(Resources.Load("UI/RunModeButtons")) as GameObject;
            RunModeButtons.transform.SetParent(CanvasTransform, false);
            //RunModeButtons.GetComponent<Button>().onClick.AddListener(OnQuitReviewButtonPressed);
        }
        else
        {
            RunModeButtons.SetActive(true);
        }
    }
}
