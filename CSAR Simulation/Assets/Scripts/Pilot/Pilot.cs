using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class Pilot : MonoBehaviour
{
    public float life;
    private MainMenu mainMenu;

    void Start()
    {
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenu.OnStart += InitLifeValue;
    }

    void FixedUpdate()
    {
        
        if (SimulationRun.runMode == RunMode.run)
        {
            life -= 1f;
            if (life <= 0f)
            {
                SimulationRun.runMode = RunMode.pause;
                UIManager.Instance.PushInfo("待救飞行员已死亡，救援失败。");
            }
        }
    }

    void InitLifeValue()
    {
        life = (TaskDefinition.lifeLeft - 1f) * 3600f; //剩余生命（换算成秒）
    }
}
