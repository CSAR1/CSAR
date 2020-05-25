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

    private bool Hover = true;
    private bool Go = false;
    private bool Down = false;
    private bool Up = false;
    private bool Back = false;

    private void Awake()
    {
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
    }

    // Start is called before the first frame update
    void Start()
    {
        pilot = this.gameObject.transform.parent.Find("Pilot").gameObject ;
        StartPosition = new Vector3(1.5f, 0, 0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {

        }
    }



    void HelicopterHover()
    {

    }

    void HelicopterGo()
    {

    }

    void HelicopterDown()
    {

    }


    void HelicopterUp()
    {

    }

    void HelicopterBack()
    {

    }
}
