using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class Pilot : MonoBehaviour
{
    public float life;
    public float timePassed;
    public float maxSpeed;
    private MainMenu mainMenu;
    private RunPanel runPanel;

    void Start()
    {
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenu.OnStart += InitValue;
        timePassed = 1.3f;
    }

    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            gameObject.transform.position += new Vector3(0f, 0f, 0.4f / 50000f * 2f);
            life -= 0.8f;
            timePassed += 0.8f / 3600f;
            if (life <= 0f)
            {
                SimulationRun.runMode = RunMode.pause;
                UIManager.Instance.PushInfo("待救飞行员已死亡，救援失败。");
            }
        }
    }

    void InitValue()
    {
        transform.position = new Vector3(1f, 0.073f, 1f);
        life = (TaskDefinition.lifeLeft - 1f) * 3600f; //剩余生命（换算成秒）
        maxSpeed = TaskDefinition.maxSpeed; //最大移动速度
        runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
        timePassed = 1.3f;
        SimulationRun.pilotDetectedMode = PilotDetectedMode.notFound;
    }
}
