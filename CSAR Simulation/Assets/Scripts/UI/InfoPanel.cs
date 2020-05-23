using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using DG.Tweening;
using UnityEngine.UI;

public class InfoPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private GameObject runModeButtons;
    private GameObject mainMenu;

    public Text content;

    // Start is called before the first frame update
    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        runModeButtons = GameObject.Find("RunModeButtons(Clone)");
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(1, .5f);
    }

    public void SetContent(string content)
    {
        this.content.text = content;
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
    }

    public void OnQuitConfirm()
    {
        UIManager.Instance.PopPanel();
        runModeButtons.SetActive(false);
        UIManager.Instance.PushPanel(UIPanelType.MainMenu);
    }
}
