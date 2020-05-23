using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class PrePhase : MonoBehaviour
{
    private float distance;
    private float speed;
    private float lifeLeft;
    private float time;
    
    private MainMenu mainMenu;

    void Start()
    {
        distance = 600f;
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
        time = distance / speed;
        if (time >= lifeLeft)
        {
            UIManager.Instance.PushInfo("救援力量到达现场之前，待救飞行员已死亡，救援失败");
            SimulationRun.runMode = RunMode.pause;
        }
    }
}
