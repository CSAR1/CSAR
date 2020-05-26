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

    public float TimePass_1=0;
    public float speed = 100f;
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

                if(TimePass_1 > 2)
                {
                Hover = false;
                Go = true;
                }

            }

            if (Go)
            {
                GoDirection = new Vector3(Pilot.transform.position.x - this.transform.position.x, 0, Pilot.transform.position.z - this.transform.position.z);

                HelicopterGo();
                if (GoDirection .magnitude <1)
                {
                    Go = false;
                    Down = true;
                }

            }

            if (Down)
            {
                HelicopterDown();
                Down = false;
                Up = true;
            }

            if (Up)
            {
                HelicopterUp();
                Up = false;
                Back = true;
             
            }

            if(Back)
            {
                HelicopterBack();
                Back = false;
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
        runPanel.AddInformation("直升机下降");
    }


    void HelicopterUp()
    {
        runPanel.AddInformation("直升机上升");
    }

    void HelicopterBack()
    {
        runPanel.AddInformation("直升机返回");
    }

    private void OnStart()
    {
        runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
    }
}
