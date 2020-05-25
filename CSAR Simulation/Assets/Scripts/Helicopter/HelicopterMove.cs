using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class HelicopterMove : MonoBehaviour
{
    private GameObject pilot;
    private MainMenu mainMenu;
    private RunPanel runPanel;

    private Vector3 StartPosition;

    public bool Hover = true;
    public bool Go = false;
    public bool Down = false;
    public bool Up = false;
    public  bool Back = false;

    void Awake()
    {
        pilot = GameObject .Find("Pilot");
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = new Vector3(2f, 0.7f, 0.5f);
        this.transform.position = StartPosition;
        mainMenu.OnStart += OnStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            if (Hover)
            {
                HelicopterHover();
                Hover = false;
                Go = true;
            }

            if (Go)
            {
                HelicopterGo();
                Go = false;
                Down = true;
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
        runPanel.AddInformation("直升机盘旋");
        Debug.Log("1");

    }

    void HelicopterGo()
    {
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
