using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using UnityEngine.UI;
using DG.Tweening;
using GlobalParameters;

public class EvaluationPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private GameObject runModeButtons;
    public GameObject[] Buttons = new GameObject[5];
    public GameObject[] Panels = new GameObject[5];

    public Text reachSARArea; //是否到达搜索区域
    public Text findTarget; //是否搜索到待救目标
    public Text targetAlive; //待救目标是否还存活
    public Text targetRescued; //是否救起待救目标
    public Text returnToBase; //是否返回基地
    public Text missionSucceed; //任务是否成功

    public Text reachTime; //直升机到达搜索区域耗时
    public Text searchTime; //搜索耗时
    public Text reachTargetTime; //到达待救目标位置耗时
    public Text targetRescuedTime; //救起待救目标耗时
    public Text returnToBaseTime; //返回基地耗时
    public Text time; //行动总耗时

    public Text aircraftLoss; //装备损失数量
    public Text peopleLoss; //人员损失数量
    public Text aircraftLossRate; //装备损失率
    public Text peopleLossRate; //人员损失率

    public Text tankDestroied; //击毁敌方装备数量
    public Text peopleKilled; //击杀敌方人员数量

    public Text fuelConsumed; //救援直升机耗油量

    private void Start()
    {
        runModeButtons = GameObject.Find("RunModeButtons(Clone)");
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        UpdateValues();

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
    }

    public void UpdateValues()
    {
        reachSARArea.text = ActionResult.reachSARArea == true ? "是" : "否"; //是否到达搜索区域
        findTarget.text = ActionResult.findTarget == true ? "是" : "否"; //是否搜索到待救目标
        targetAlive.text = ActionResult.targetAlive == true ? "是" : "否"; //待救目标是否还存活
        targetRescued.text = ActionResult.targetRescued == true ? "是" : "否"; //是否救起待救目标
        returnToBase.text = ActionResult.returnToBase == true ? "是" : "否"; //是否返回基地
        missionSucceed.text = ActionResult.missionSucceed == true ? "是" : "否"; //任务是否成功

        reachTime.text = TimeResult.reachTime > 0f ? TimeResult.reachTime.ToString("0.00") + "小时" : "无数据"; //直升机到达搜索区域耗时
        searchTime.text = TimeResult.searchTime > 0f ? TimeResult.searchTime.ToString("0.00") + "小时" : "无数据"; //搜索耗时
        reachTargetTime.text = TimeResult.reachTargetTime > 0f ? TimeResult.reachTargetTime.ToString("0.00") + "小时" : "无数据"; //到达待救目标位置耗时
        targetRescuedTime.text = TimeResult.targetRescued > 0f ? TimeResult.targetRescued.ToString("0.00") + "小时" : "无数据"; //救起待救目标耗时
        returnToBaseTime.text = TimeResult.returnToBase > 0f ? TimeResult.returnToBase.ToString("0.00") + "小时" : "无数据"; //返回基地耗时
        time.text = TimeResult.time > 0f ? TimeResult.time.ToString("0.00") + "小时" : "无数据"; //行动总耗时

        aircraftLoss.text = LossResult.aircraftLoss.ToString(); //装备损失数量
        peopleLoss.text = LossResult.peopleLoss.ToString(); //人员损失数量
        aircraftLossRate.text = LossResult.aircraftLossRate.ToString("0.00") + "%"; //装备损失率
        peopleLossRate.text = LossResult.peopleLossRate.ToString("0.00") + "%"; //人员损失率

        tankDestroied.text = AttackResult.tankDestroied.ToString(); //击毁敌方装备数量
        peopleKilled.text = AttackResult.peopleKilled.ToString(); ; //击杀敌方人员数量

        fuelConsumed.text = FuelResult.fuelConsumed.ToString("0.00") + "kg"; //救援直升机耗油量
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
