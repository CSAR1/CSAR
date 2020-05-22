using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UIFramework;
using UnityEngine.UI;
using GlobalParameters;

public class TaskDefinitionPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private GameObject MainUI;
    private GameObject QuitReviewButton;
    
    public Text pilotNum; //遇险人数
    public Text lifeLeft; //剩余生命
    public Dropdown hideEnemyCap; //躲避敌方的能力
    public Text maxSpeed; //最大移动速度

    // Start is called before the first frame update
    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        hideEnemyCap.onValueChanged.AddListener(OnHideEnemyCapEndEdit);
        MainUI = GameObject.Find("Main");
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(1, .5f);
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

    public void OnPilotNumEndEdit(string s)
    {
        string content = pilotNum.text;
        //Debug.Log("Before = " + TaskDefinition.pilotNum);
        if (int.TryParse(content, out int data))
        {
            TaskDefinition.pilotNum = data;
        }
        //Debug.Log("After = " + TaskDefinition.pilotNum);
    }

    public void OnLifeLeftEndEdit(string s)
    {
        string content = lifeLeft.text;
        if (int.TryParse(content, out int data))
        {
            TaskDefinition.lifeLeft = data;
        }
    }

    public void OnHideEnemyCapEndEdit(int n)
    {
        //Debug.Log(TaskDefinition.hideEnemyCap.ToString());
        switch (n)
        {
            case 0:
                TaskDefinition.hideEnemyCap = HideEnemyCap.high;
                break;
            case 1:
                TaskDefinition.hideEnemyCap = HideEnemyCap.medium;
                break;
            case 2:
                TaskDefinition.hideEnemyCap = HideEnemyCap.low;
                break;
        }
        //Debug.Log(TaskDefinition.hideEnemyCap.ToString());
    }

    public void OnMaxSpeedEndEdit(string s)
    {
        string content = maxSpeed.text;
        if (int.TryParse(content, out int data))
        {
            TaskDefinition.maxSpeed = data;
        }
    }

    public void OnReviewButtonPressed()
    {
        MainUI.SetActive(false);
        gameObject.SetActive(false);
        //QuitReviewButton.SetActive(true);
    }
}
