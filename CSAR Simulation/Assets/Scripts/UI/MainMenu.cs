using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using GlobalParameters;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : BasePanel
{
    private CanvasGroup canvasGroup;
    private GameObject RunModeButtons;

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

    public delegate void OnSimulationStart();
    public event OnSimulationStart OnStart;

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0f);
    }

    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        transform.DOScale(0, 0f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }

    public void OnRunButtonPressed()
    {
        //gameObject.SetActive(false);
        OnClosePanel();
        SimulationRun.runMode = RunMode.run;
        OnPushPanel("Run");
        OnStart.Invoke();
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
