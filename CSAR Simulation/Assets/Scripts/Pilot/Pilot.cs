using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class Pilot : MonoBehaviour
{
    public float life;

    void Start()
    {
        life = TaskDefinition.lifeLeft * 3600f; //剩余生命（换算成秒）
        Debug.Log("?");
    }

    void FixedUpdate()
    {
        Debug.Log(life);
        if (SimulationRun.runMode == RunMode.run)
        {
            life -= 300f;
            Debug.Log(life);
            if (life <= 0f)
            {
                SimulationRun.runMode = RunMode.pause;
                UIManager.Instance.PushInfo("待救飞行员已死亡，救援失败。");
            }
        }
    }
}
