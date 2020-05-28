using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class Enemy : MonoBehaviour
{
    EnemyGenerate enemyParent;

    private int enemiesNum;
    private int missileRange;
    private int missileMach;
    private int maxOverload;
    private int detectR;

    private GameObject pilot;
    private GameObject helicopter;

    private MainMenu mainMenu;
    private RunPanel runPanel;

    public float timePassed;
    public float detected;

    void Start()
    {
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenu.OnStart += InitValue;

        enemyParent = gameObject.GetComponentInParent<EnemyGenerate>();
        pilot = GameObject.Find("Pilot");
        helicopter = GameObject.Find("Helicopter");
        timePassed = 1.3f;
    }
    
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            timePassed += 0.8f / 3600f;
            
            if (SimulationRun.pilotDetectedMode == PilotDetectedMode.notFound || SimulationRun.pilotDetectedMode == PilotDetectedMode.foundBySARTeam)
            {
                transform.position += transform.right * (-2.5f / 50000f * 2f);
            }
            if (SimulationRun.pilotDetectedMode == PilotDetectedMode.foundByEnemy || SimulationRun.pilotDetectedMode == PilotDetectedMode.foundByBoth)
            {
                transform.LookAt(transform.position + new Vector3((pilot.transform.position - transform.position).z, pilot.transform.position.y, -(pilot.transform.position - transform.position).x));
                transform.position += transform.right * (-2.5f / 50000f * 2f);
            }

            if (timePassed > 1.8f && (SimulationRun.pilotDetectedMode == PilotDetectedMode.notFound || SimulationRun.pilotDetectedMode == PilotDetectedMode.foundBySARTeam))
            {
                detected = Random.Range(0f, 10f);
                if (detected < 0.002f)
                {
                    runPanel.AddInformation(timePassed.ToString("0.00") + "小时后：待救飞行员已被敌方探测到，敌方正在驱车前往。");
                    switch (SimulationRun.pilotDetectedMode)
                    {
                        case PilotDetectedMode.notFound:
                            SimulationRun.pilotDetectedMode = PilotDetectedMode.foundByEnemy;
                            break;
                        case PilotDetectedMode.foundByEnemy:
                            SimulationRun.pilotDetectedMode = PilotDetectedMode.foundByEnemy;
                            break;
                        case PilotDetectedMode.foundBySARTeam:
                            SimulationRun.pilotDetectedMode = PilotDetectedMode.foundByBoth;
                            break;
                        case PilotDetectedMode.foundByBoth:
                            SimulationRun.pilotDetectedMode = PilotDetectedMode.foundByBoth;
                            break;
                    }
                }
            }
            if (Vector3.Magnitude(transform.position - helicopter.transform.position) <= detectR)
            {
                float helicopterHit = Random.Range(0f, 10f);
                if (helicopterHit <= 0.0003f)
                {
                    ScoreValue.lossScore -= 55f;
                    LossResult.aircraftLoss += 1;
                    LossResult.aircraftLossRate = 1f / 3f * 100;
                    LossResult.peopleLoss += 5;
                    LossResult.peopleLossRate = 5f / 7f * 100;
                    SimulationRun.runMode = RunMode.pause;
                    UIManager.Instance.PushInfo("救援直升机被敌方击落，救援失败。");
                }
            }
        }
    }

    void InitValue()
    {
        runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
        timePassed = 1.3f;

        enemiesNum = enemyParent.enemiesNum;
        missileRange = enemyParent.missileRange;
        missileMach = enemyParent.missileMach;
        maxOverload = enemyParent.maxOverload;
        detectR = enemyParent.detectR;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pilot")
        {
            SimulationRun.runMode = RunMode.pause;
            UIManager.Instance.PushInfo("待救飞行员被敌方抓获，救援失败。");
        }
    }
}
