using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UIFramework;
using UnityEngine.UI;
using GlobalParameters;

public class EnemyDefinitionPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private GameObject MainUI;
    private GameObject QuitReviewButton;

    public Text enemyNum; //火力点数量
    public Text missileRange; //对空导弹射程
    public Text missileMach; //对空导弹马赫数
    public Text maxOverload; //对空导弹最大过载
    public Text detectR; //探测范围半径

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

    // Start is called before the first frame update
    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        MainUI = GameObject.Find("Main(Clone)");
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

    public void OnEnemyNumEndEdit(string s)
    {
        string content = enemyNum.text;
        if (int.TryParse(content, out int data))
        {
            EnemyDefinition.enemyNum = data;
        }
    }

    public void OnMissileRangeEndEdit(string s)
    {
        string content = missileRange.text;
        if (int.TryParse(content, out int data))
        {
            EnemyDefinition.missileRange = data;
        }
    }

    public void OnMissileMachEndEdit(string s)
    {
        string content = missileMach.text;
        if (int.TryParse(content, out int data))
        {
            EnemyDefinition.missileMach = data;
        }
    }

    public void OnMaxOverloadEndEdit(string s)
    {
        string content = maxOverload.text;
        if (int.TryParse(content, out int data))
        {
            EnemyDefinition.maxOverload = data;
        }
    }

    public void OnDetectREndEdit(string s)
    {
        string content = detectR.text;
        if (int.TryParse(content, out int data))
        {
            EnemyDefinition.detectR = data;
        }
    }

    public void OnReviewButtonPressed()
    {
        MainUI.SetActive(false);
        gameObject.SetActive(false);
        if (QuitReviewButton == null)
        {
            QuitReviewButton = Object.Instantiate(Resources.Load("UI/QuitReviewButton")) as GameObject;
            QuitReviewButton.transform.SetParent(CanvasTransform, false);
            QuitReviewButton.GetComponent<Button>().onClick.AddListener(OnQuitReviewButtonPressed);
        }
        else
        {
            QuitReviewButton.SetActive(true);
        }
    }

    public void OnQuitReviewButtonPressed()
    {
        MainUI.SetActive(true);
        gameObject.SetActive(true);
        QuitReviewButton.SetActive(false);
    }
}
