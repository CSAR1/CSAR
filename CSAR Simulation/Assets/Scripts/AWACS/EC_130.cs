using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class EC_130 : MonoBehaviour
{
    private MainMenu mainMenu;

    private float angle;
    public float r = 200;
    public float x;
    public float w;
    public float z;
    private GameObject sun;

    // Use this for initialization
    void Start()
    {
        sun = GameObject.Find("Sun");
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenu.OnStart += OnStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            w = 0.065f;
            angle -= w * Time.deltaTime;
            x = Mathf.Cos(angle) * r;
            z = -Mathf.Sin(angle) * r;
            transform.position = new Vector3(sun.GetComponent<Transform>().position.x - x, 0.7f, sun.GetComponent<Transform>().position.z - z);
            gameObject.transform.Rotate(-0.065f * Vector3.up);
        }
    }

    void OnStart()
    {
        angle = 0f;
        gameObject.transform.position = new Vector3(0.3f, 0.7f, 1f);  //重置做圆周的开始位置
        gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        r = Vector3.Distance(gameObject.GetComponent<Transform>().position, sun.GetComponent<Transform>().position); //两个物品间的距离
    }
}
