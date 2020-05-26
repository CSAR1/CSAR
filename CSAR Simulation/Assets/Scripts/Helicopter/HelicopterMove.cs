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

    public float TimePass_1=0;
    public float speed = 1f;
    private float Height=0.7f;

    public bool Hover = true;
    public bool Go = false;
    public bool Down = false;
    public bool Up = false;
    public  bool Back = false;

    void Awake()
    {
        Pilot = GameObject .Find("Pilot");
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = new Vector3(2f, Height , 0.5f);
        Jidi_Position = new Vector3(3f, 0.7f, 0.5f);
        mainMenu.OnStart += OnStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            TimePass_1 += Time.fixedDeltaTime;
            if (Hover)
            {
                HelicopterHover();

                if (TimePass_1 > 2)
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
        runPanel.AddInformation("直升机盘旋");
        Debug.Log("1");

    }

    void HelicopterGo()
    {

        this.transform.LookAt(new Vector3(Pilot.transform.position.x, Height, Pilot.transform .position.z));
        this.transform.Translate(GoDirection.normalized * speed * Time.fixedDeltaTime, Space.World);
        runPanel.AddInformation("直升机出发");
        Debug.Log("2");
    }

    void HelicopterDown()
    {
        this.transform.Translate(new Vector3 (0,-0.1f,0) * speed * Time.fixedDeltaTime, Space.World);
        runPanel.AddInformation("直升机下降");
    }


    void HelicopterUp()
    {
        this.transform.Translate(new Vector3(0, 0.1f, 0) * speed * Time.fixedDeltaTime, Space.World);
        runPanel.AddInformation("直升机上升");
    }

    void HelicopterBack()
    {
        this.transform.LookAt(Jidi_Position );
        this.transform.Translate((Jidi_Position -this .transform .position ).normalized * speed * Time.fixedDeltaTime, Space.World);
        runPanel.AddInformation("直升机返回");
    }

    private void OnStart()
    {
        runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
    }
}
