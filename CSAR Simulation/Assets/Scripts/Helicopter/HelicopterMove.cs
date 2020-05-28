using System.Collections;
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
    //public float timePassed_1=0;
    public  float timePassed;
    public float speed ;
    private float step;
    private float Height=0.7f;
    public float fuel;

    public bool Hover = true;
    public bool Go = false;
    public bool Down = false;
    public bool Up = false;
    public  bool Back = false;
    public bool Stop = false;

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
        StartPosition = new Vector3(3f, Height , 1f);
        //Jidi_Position = new Vector3(3f, 0.7f, 0.5f);



        mainMenu.OnStart += OnStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            timePassed += 0.8f / 3600f;
            //油量消耗模型
            if (timePassed > 1.35f)
            {
                fuel -= (2275f / 3600f) * 0.8f;
            }
            //timePassed_1 += Time.fixedDeltaTime;
            if (Hover)
            {
                HelicopterHover();

                if (timePassed > 1.35f)
                {
                    Hover = false;
                    Go = true;
                    fuel -= (2275f / 3600f) * 0.8f;
                }

            }

            if (Go)
            {
                GoDirection = new Vector3(Pilot.transform.position.x - this.transform.position.x, 0, Pilot.transform.position.z - this.transform.position.z);

                HelicopterGo();
                if (GoDirection .magnitude <0.1)
                {
                    Go = false;
                    TimeResult.reachTargetTime = timePassed;
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
                    TimeResult.targetRescued = timePassed;
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
                if ((StartPosition  -this .transform .position ).magnitude <0.01)
                {
                    Back = false;
                    TimeResult.returnToBase = timePassed;
                    Stop = true;
                }

            }

            if(Stop)
            {
                HelicopterStop();
            }

        }
    }



    void HelicopterHover()
    {

        if (i == 0)
        {
            //runPanel.AddInformation(timePassed.ToString("0.00") + "小时后，直升机接到引导掩护机指令，起飞前往目标位置");
            i = 1;
        }



    }

    void HelicopterGo()
    {

        this.transform.LookAt(new Vector3(Pilot.transform.position.x, Height, Pilot.transform .position.z));
        this.transform.Translate(GoDirection.normalized * step , Space.World);
        if (i == 1)
        {
            runPanel.AddInformation(timePassed.ToString("0.00") + "小时后：直升机接到引导掩护机指令，起飞前往目标位置。");
            i = 2;
        }


    }

    void HelicopterDown()
    {
        this.transform.Translate(new Vector3 (0,-0.3f,0) * step, Space.World);
        
        if (i == 2)
        {
            runPanel.AddInformation(timePassed.ToString("0.00") + "小时后：直升机锁定飞行员位置，开始实施救援。");
            i = 3;
        }
        

    }


    void HelicopterUp()
    {
        this.transform.Translate(new Vector3(0, 0.5f, 0) * step , Space.World);
        /*
        if (i == 3)
        {
            runPanel.AddInformation("直升机上升");
            i = 4;
        }
        */
        ActionResult.targetRescued = true;


    }

    void HelicopterBack()
    {
        this.transform.LookAt(StartPosition  );
        this.transform.Translate((StartPosition  -this .transform .position ).normalized * step , Space.World);
        if (i == 3)
        {
            runPanel.AddInformation(timePassed.ToString("0.00") + "小时后：飞行员被成功救起，直升机返回。");
            i = 4;
        }

    }

    void HelicopterStop()
    {
        if (i == 4)
        {

            TimeResult.time = timePassed;//总耗时
            if (EquipmentSelection.sar == SAR.MH_53)
            {
                FuelResult.fuelConsumed = GlobalParameters.MH_53.fuelWeight - fuel;
            }
            else if (EquipmentSelection.sar == SAR.MH_60)
            {
                FuelResult.fuelConsumed = GlobalParameters.MH_60.fuelWeight - fuel;
            }
            UIManager.Instance.PushInfo("搜救直升机安全返回基地，救援成功！");
            SimulationRun.runMode = RunMode.pause;
            i = 5;
            ActionResult.targetAlive = true;
            ActionResult.missionSucceed = true;
            ActionResult.returnToBase = true;
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
        Stop = false;
        i = 0;

        this.transform.position = StartPosition;

        timePassed = 1.3f;
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
            speed = GlobalParameters.MH_53.speed;
            fuel = GlobalParameters.MH_53.fuelWeight - 2275f * 0.36f;
            for (int j = 0; j<renders_60.Length; j++)
			{
                renders_60[j].enabled = false;
			}
        }

        else if (EquipmentSelection.sar == SAR.MH_60)
        {
            speed = GlobalParameters.MH_60.speed;
            fuel = GlobalParameters.MH_60.fuelWeight;
            for (int j = 0; j < renders_53.Length; j++)
            {
                renders_53[j].enabled = false;
            }
        }

        step = speed * 0.8f / 3600f / 50f * 2f;
    }
}
