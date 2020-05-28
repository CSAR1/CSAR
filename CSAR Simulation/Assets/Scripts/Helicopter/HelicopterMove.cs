﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class HelicopterMove : MonoBehaviour
{
    private GameObject Pilot;
    private MainMenu mainMenu;
    private RunPanel runPanel;

    private Vector3 StartPosition;
    private Vector3 GoDirection;
    private Vector3 Jidi_Position;

    private int i = 0;
    public float timePassed_1=0;
    private float timePassed=0;
    public float speed ;
    private float step;
    private float Height=0.7f;

    public bool Hover = true;
    public bool Go = false;
    public bool Down = false;
    public bool Up = false;
    public  bool Back = false;

    public GameObject MH_53;
    public GameObject MH_60;

    void Awake()
    {
        Pilot = GameObject .Find("Pilot");
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = new Vector3(2.5f, Height , 1f);
        Jidi_Position = new Vector3(3f, 0.7f, 0.5f);

        speed = GlobalParameters.MH_53.speed;
        step = speed * 0.8f / 3600f / 50f * 2f;
        mainMenu.OnStart += OnStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            timePassed += 0.8f / 3600f;
            timePassed_1 += Time.fixedDeltaTime;
            if (Hover)
            {
                HelicopterHover();

                if (timePassed_1 > 2)
                {
                    Hover = false;
                    Go = true;
                }

            }

            if (Go)
            {
                GoDirection = new Vector3(Pilot.transform.position.x - this.transform.position.x, 0, Pilot.transform.position.z - this.transform.position.z);

                HelicopterGo();
                if (GoDirection .magnitude <0.1)
                {
                    Go = false;
                    Down = true;
                }

            }

            if (Down)
            {
                HelicopterDown();
                if ((this .transform .position  .y-Pilot .transform .position .y)<0.3)
                {
                    Down = false;
                    Pilot.transform .Find ("PILOT/Default").GetComponent<Renderer>().enabled = false;//飞行员消失
                    Up = true;
                }

            }

            if (Up)
            {
                HelicopterUp();
                if (this .transform .position .y >0.7)
                {
                    Up = false;
                    Back = true;
                }

             
            }

            if(Back)
            {
                HelicopterBack();
                if ((Jidi_Position -this .transform .position ).magnitude <0.1)
                {
                    Back = false;
                }

            }
            

        }
    }



    void HelicopterHover()
    {
        this.transform.position = StartPosition;
        if (i == 0)
        {
            runPanel.AddInformation("直升机盘旋");
            i = 1;
        }



    }

    void HelicopterGo()
    {

        this.transform.LookAt(new Vector3(Pilot.transform.position.x, Height, Pilot.transform .position.z));
        this.transform.Translate(GoDirection.normalized * step , Space.World);
        if (i == 1)
        {
            runPanel.AddInformation(timePassed.ToString("0.00") + "小时后：直升机出发");
            i = 2;
        }


    }

    void HelicopterDown()
    {
        this.transform.Translate(new Vector3 (0,-0.5f,0) * step, Space.World);
        /*
        if (i == 2)
        {
            runPanel.AddInformation("直升机下降");
            i = 3;
        }
        */

    }


    void HelicopterUp()
    {
        this.transform.Translate(new Vector3(0, 0.1f, 0) * step , Space.World);
        /*
        if (i == 3)
        {
            runPanel.AddInformation("直升机上升");
            i = 4;
        }
        */

    }

    void HelicopterBack()
    {
        this.transform.LookAt(Jidi_Position );
        this.transform.Translate((Jidi_Position -this .transform .position ).normalized * step , Space.World);
        if (i == 2)
        {
            runPanel.AddInformation("飞行员被成功救起，直升机返回");
            i = 3;
        }

    }

    private void OnStart()
    {
        runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
        Hover = true;
        Go = false;
        Down = false;
        Up = false;
        Back = false;
        i = 0;
        MeshRenderer[] renders_53 = MH_53.GetComponentsInChildren<MeshRenderer>();
        for (int j = 0; j < renders_53.Length; j++)
        {
            renders_53[j].enabled = true;
        }

        MeshRenderer[] renders_60 = MH_60.GetComponentsInChildren<MeshRenderer>();
        for (int j = 0; j < renders_60.Length; j++)
        {
            renders_60[j].enabled = true;
        }

        if (EquipmentSelection.sar == SAR.MH_53)
        {
            for (int j = 0; j<renders_60.Length; j++)
			{
                renders_60[j].enabled = false;
			}
        }

        else if (EquipmentSelection.sar == SAR.MH_60)
        {
            for (int j = 0; j < renders_53.Length; j++)
            {
                renders_53[j].enabled = false;
            }
        }
    }
}
