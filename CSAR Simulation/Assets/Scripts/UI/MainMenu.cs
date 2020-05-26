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
    private RunPanel runPanel;

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
        SimulationRun.inputPhase = true;
    }

    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        transform.DOScale(0, 0f).OnComplete(() => canvasGroup.alpha = 0);
        SimulationRun.inputPhase = false;
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
        EvaluationReset();
        ScoreReset();
        OnPushPanel("Run");
        runPanel = GameObject.Find("RunPanel(Clone)").GetComponent<RunPanel>();
        runPanel.ShowInformation("暂无状态更新。");
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

    public void EvaluationReset()
    {
        ActionResult.reachSARArea = false; //是否到达搜索区域
        ActionResult.findTarget = false; //是否搜索到待救目标
        ActionResult.targetAlive = false; //待救目标是否还存活
        ActionResult.targetRescued = false; //是否救起待救目标
        ActionResult.returnToBase = false; //是否返回基地
        ActionResult.missionSucceed = false; //任务是否成功

        TimeResult.reachTime = -1f; //到达搜索区域耗时
        TimeResult.searchTime = -1f; //搜索耗时
        TimeResult.reachTargetTime = -1f; //到达待救目标位置耗时
        TimeResult.targetRescued = -1f; //救起待救目标耗时
        TimeResult.returnToBase = -1f; //返回基地耗时
        TimeResult.time = -1f; //行动总耗时

        LossResult.aircraftLoss = 0; //装备损失数量
        LossResult.peopleLoss = 0; //人员损失数量
        LossResult.aircraftLossRate = 0f; //装备损失率
        LossResult.peopleLossRate = 0f; //人员损失率

        AttackResult.tankDestroied = 0; //击毁敌方装备数量
        AttackResult.peopleKilled = 0; //击杀敌方人员数量

        FuelResult.fuelConsumed = 0f; //救援直升机耗油量
    }

    public void ScoreReset()
    {
        ScoreValue.actionScore = 0f;
        ScoreValue.lossScore = 100f;
        ScoreValue.attackScore = 0f;
        ScoreValue.fuelScore = 100f;
}
}
