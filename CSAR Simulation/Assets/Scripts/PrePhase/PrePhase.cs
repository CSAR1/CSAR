using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class PrePhase : BasePanel
{
    private float distance;
    private float speed;
    private float lifeLeft;
    private float time;
    private HideEnemyCap hideEnemyCap;
    
    private MainMenu mainMenu;
    private RunPanel runPanel;

    void Start()
    {
        distance = 650f;
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenu.OnStart += InitPrameters;
    }
    
    void FixedUpdate()
    {
        
    }

    void InitPrameters()
    {
        if (EquipmentSelection.ydyh == YDYH.A_10)
        {
            speed = 0.5f * (A_10.maxSpeed + A_10.minSpeed);
        }
        else if (EquipmentSelection.ydyh == YDYH.AC_130)
        {
            speed = 0.5f * (AC_130.maxSpeed + AC_130.minSpeed);
        }
        lifeLeft = TaskDefinition.lifeLeft;
        hideEnemyCap = TaskDefinition.hideEnemyCap;
        time = distance / speed;
        if (time >= lifeLeft)
        {
            UIManager.Instance.PushInfo("救援力量到达现场之前，待救飞行员已死亡，救援失败。");
            SimulationRun.runMode = RunMode.pause;
        }
        float captured = Random.Range(0f, 10f);
        float ydyhDamaged = Random.Range(0f, 10f);
        float sarDamaged = Random.Range(0f, 10f);
        switch (hideEnemyCap)
        {
            case HideEnemyCap.high:
                if (captured < 1f)
                {
                    UIManager.Instance.PushInfo("救援力量到达现场之前，待救飞行员被敌方间谍发现，救援失败。");
                    SimulationRun.runMode = RunMode.pause;
                }
                break;
            case HideEnemyCap.medium:
                if (captured < 2f)
                {
                    UIManager.Instance.PushInfo("救援力量到达现场之前，待救飞行员被敌方军犬发现，救援失败。");
                    SimulationRun.runMode = RunMode.pause;
                }
                break;
            case HideEnemyCap.low:
                if (captured < 3f)
                {
                    UIManager.Instance.PushInfo("救援力量到达现场之前，待救飞行员暴露行踪，被敌方抓获，救援失败。");
                    SimulationRun.runMode = RunMode.pause;
                }
                break;
        }
        if (EquipmentSelection.ydyh == YDYH.A_10)
        {
            if (ydyhDamaged < 0.5f)
            {
                UIManager.Instance.PushInfo("A-10攻击机在奔袭途中被敌方击落，救援失败。");
                ScoreValue.lossScore -= 15f;
                LossResult.aircraftLoss += 1;
                LossResult.aircraftLossRate = 1f / 3f * 100;
                LossResult.peopleLoss += 1;
                LossResult.peopleLossRate = 1f / 7f * 100;
                SimulationRun.runMode = RunMode.pause;
            }
        }
        else if (EquipmentSelection.ydyh == YDYH.AC_130)
        {
            if (ydyhDamaged < 0.7f)
            {
                UIManager.Instance.PushInfo("AC-130攻击机在奔袭途中被敌方击落，救援失败。");
                ScoreValue.lossScore -= 15f;
                LossResult.aircraftLoss += 1;
                LossResult.aircraftLossRate = 1f / 3f * 100;
                LossResult.peopleLoss += 1;
                LossResult.peopleLossRate = 1f / 7f * 100;
                SimulationRun.runMode = RunMode.pause;
            }
        }
        if (EquipmentSelection.sar == SAR.MH_53)
        {
            if (ydyhDamaged < 0.8f)
            {
                UIManager.Instance.PushInfo("MH-53直升机在奔袭途中被敌方击落，救援失败。");
                ScoreValue.lossScore -= 55f;
                LossResult.aircraftLoss += 1;
                LossResult.aircraftLossRate = 1f / 3f * 100;
                LossResult.peopleLoss += 5;
                LossResult.peopleLossRate = 5f / 7f * 100;
                SimulationRun.runMode = RunMode.pause;
            }
        }
        else if (EquipmentSelection.sar ==SAR.MH_60)
        {
            if (ydyhDamaged < 0.7f)
            {
                UIManager.Instance.PushInfo("MH-60直升机在奔袭途中被敌方击落，救援失败。");
                ScoreValue.lossScore -= 55f;
                LossResult.aircraftLoss += 1;
                LossResult.aircraftLossRate = 1f / 3f * 100;
                LossResult.peopleLoss += 5;
                LossResult.peopleLossRate = 5f / 7f * 100;
                SimulationRun.runMode = RunMode.pause;
            }
        }
        runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
        if (SimulationRun.runMode == RunMode.run)
        {
            if (EquipmentSelection.sar == SAR.MH_53)
            {
                runPanel.ShowInformation("0.36小时后：MH-53直升机已安全到达事发地附近基地降落，等待引导掩护机指示。");
            }
            else if (EquipmentSelection.sar == SAR.MH_60)
            {
                runPanel.ShowInformation("0.40小时后：MH-60直升机已安全到达事发地附近基地降落，等待引导掩护机指示。");
            }
            if (EquipmentSelection.ydyh == YDYH.A_10)
            {
                runPanel.AddInformation(time.ToString("0.00") + "小时后：A-10攻击机已安全到达事发地附近，遇险飞行员健康状况依然良好。");
                ActionResult.reachSARArea = true;
                TimeResult.reachTime = time;
            }
            else if (EquipmentSelection.ydyh == YDYH.AC_130)
            {
                runPanel.AddInformation(time.ToString("0.00") + "小时后：AC-130攻击机已安全到达事发地附近，遇险飞行员健康状况依然良好。");
                ActionResult.reachSARArea = true;

                TimeResult.reachTime = time;
            }
        }
    }
}
