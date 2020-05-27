using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class Status_A10 : MonoBehaviour
{

    public float life;
    public float timePassed;
    public float maxSpeed;
    private MainMenu mainMenu;
    private RunPanel runPanel;
    public float detected;


    // Start is called before the first frame update
    void Start()
    {
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenu.OnStart += InitValueA10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            switch (SimulationRun.pilotDetectedMode)
            {
                case PilotDetectedMode.notFound:
                    // 未被发现，搜索函数
                    break;
                case PilotDetectedMode.foundByEnemy:
                    // 未被发现，搜索函数
                    break;
                case PilotDetectedMode.foundBySARTeam:
                    // 发现，掩护函数
                    break;
                case PilotDetectedMode.foundByBoth:
                    // 发现，掩护函数
                    break;
                //case PilotDetectedMode.recovered:
                    // 救援，撤离护航函数
                    //break;
            }
        }

    }

    void InitValueA10()
    {
        life = (TaskDefinition.lifeLeft - 1f) * 3600f; //剩余生命（换算成秒）
        maxSpeed = TaskDefinition.maxSpeed; //最大移动速度
        runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
    }
    
    }
