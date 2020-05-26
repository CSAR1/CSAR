using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using UnityEngine.UI;
using DG.Tweening;

public class EvaluationPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private GameObject runModeButtons;
    public GameObject[] Buttons = new GameObject[5];
    public GameObject[] Panels = new GameObject[5];

    private void Start()
    {
        runModeButtons = GameObject.Find("RunModeButtons(Clone)");
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(0.8f, .5f);
        OnActionSelected();
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

    public void OnActionSelected()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 148f / 255f);
            Buttons[i].GetComponentInChildren<Text>().color = new Color(34f / 255f, 34f / 255f, 34f / 255f, 255f / 255f);
        }
        Buttons[0].GetComponent<Image>().color = new Color(10f / 255f, 7f / 255f, 7f / 255f, 144f / 255f);
        Buttons[0].GetComponentInChildren<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
        Panels[0].SetActive(true);
    }

    public void OnTimeSelected()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 148f / 255f);
            Buttons[i].GetComponentInChildren<Text>().color = new Color(34f / 255f, 34f / 255f, 34f / 255f, 255f / 255f);
        }
        Buttons[1].GetComponent<Image>().color = new Color(10f / 255f, 7f / 255f, 7f / 255f, 144f / 255f);
        Buttons[1].GetComponentInChildren<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
        Panels[1].SetActive(true);
    }

    public void OnLossSelected()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 148f / 255f);
            Buttons[i].GetComponentInChildren<Text>().color = new Color(34f / 255f, 34f / 255f, 34f / 255f, 255f / 255f);
        }
        Buttons[2].GetComponent<Image>().color = new Color(10f / 255f, 7f / 255f, 7f / 255f, 144f / 255f);
        Buttons[2].GetComponentInChildren<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
        Panels[2].SetActive(true);
    }

    public void OnAttackSelected()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 148f / 255f);
            Buttons[i].GetComponentInChildren<Text>().color = new Color(34f / 255f, 34f / 255f, 34f / 255f, 255f / 255f);
        }
        Buttons[3].GetComponent<Image>().color = new Color(10f / 255f, 7f / 255f, 7f / 255f, 144f / 255f);
        Buttons[3].GetComponentInChildren<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
        Panels[3].SetActive(true);
    }

    public void OnFuelSelected()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 148f / 255f);
            Buttons[i].GetComponentInChildren<Text>().color = new Color(34f / 255f, 34f / 255f, 34f / 255f, 255f / 255f);
        }
        Buttons[4].GetComponent<Image>().color = new Color(10f / 255f, 7f / 255f, 7f / 255f, 144f / 255f);
        Buttons[4].GetComponentInChildren<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
        Panels[4].SetActive(true);
    }
}
