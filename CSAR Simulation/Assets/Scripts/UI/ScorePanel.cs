using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using DG.Tweening;
using UnityEngine.UI;

public class ScorePanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private GameObject runModeButtons;

    private float standardTime = 2f;

    public Text action;
    public Text loss;
    public Text attack;
    public Text fuel;
    public Text overall;

    private void Start()
    {
        runModeButtons = GameObject.Find("RunModeButtons(Clone)");
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        CalculateScore();
        SetScore();

        transform.localScale = Vector3.zero;
        transform.DOScale(0.8f, .5f);
    }

    public override void OnExit()
    {
        //canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        transform.DOScale(0, .5f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
        UIManager.Instance.PopPanel();
        runModeButtons.SetActive(false);
        UIManager.Instance.PushPanel(UIPanelType.MainMenu);
    }

    public void OnCheckDetails()
    {
        UIManager.Instance.PushPanel(UIPanelType.Evaluation);
    }

    public void CalculateScore()
    {

    }

    public void SetScore()
    {

    }
}
