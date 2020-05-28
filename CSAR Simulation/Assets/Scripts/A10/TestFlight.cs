﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.XR.WSA;

public class TestFlight: MonoBehaviour
{
    // 声明时间与飞机生命值
    public float lifeA10;
    public float timePassed;

    // 定义飞行控制量
    private float maxSpeedA10;
    private float minSpeedA10;
    private float flightHeightA10;
    private float minRA10;
    private float angularVelocityA10;
    public float currentVelocityA10;

    // 定义地形中心
    public GameObject targetTerrain;
    private Vector3 initialStartPoint;

    // 定义距离计算差
    private double distanceDifference;
    private Vector3 postionDifferenceVector;
    private bool searchStatusA10;


    // 菜单控制
    private MainMenu mainMenu;
    private RunPanel runPanel;

    // 扫掠宽度(m)
    private float sweepWidth;

    // 扫掠范围定义
    private float horizontalDetection;
    private float verticalDetection;

    // 盒碰撞器定义
    private BoxCollider detectionBox;

    // 探测列表
    public List<GameObject> pilotInSight = new List<GameObject>();
    public List<GameObject> enemyInsight = new List<GameObject>();

    int indexDetectionA10;


    // Start is called before the first frame update
    void Start()
    {

        // 仿真控制设置
        //mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        //mainMenu.OnStart += InitValueA10;
        timePassed = 1.3f;
        sweepWidth = 800;

        // 性能设置
        this.maxSpeedA10 = A_10.maxSpeed;
        this.minSpeedA10 = A_10.minSpeed;
        this.minRA10 = A_10.minR;

        // 搜索起始点
        targetTerrain = GameObject.Find("Terrain/Target");
        flightHeightA10 = 1.0f;

        // 入场起始点
        this.transform.position = new Vector3(2, flightHeightA10, 0);

        // 生命值设置
        lifeA10 = 100;

        // 获取碰撞器
        detectionBox = GetComponent<BoxCollider>();

        // 抵达中心
        searchStatusA10 = false;

        // 发现计次
        indexDetectionA10 = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // 实时角速度
        this.angularVelocityA10 = this.currentVelocityA10 / this.minRA10 * 10;

        SearchA10();
        DetectionA10();

    }

    void DetectionA10()  // 探测函数
    {

        // 扫掠范围计算
        horizontalDetection = sweepWidth / 50000f * 2 / 2;
        verticalDetection = horizontalDetection / 2f;

        // 碰撞器体积设置
        detectionBox.size = new Vector3(horizontalDetection * 200, flightHeightA10 * 200, verticalDetection * 200);

        // 碰撞触发判断
        if (pilotInSight.Count > 0)
        {
            if (indexDetectionA10 == 0)
            {
                SimulationRun.pilotDetectedMode = PilotDetectedMode.foundBySARTeam;
                indexDetectionA10 = 1;
                Console.WriteLine("Pilot has been found");
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pilot")
        {
            pilotInSight.Add(other.gameObject);
        }

        if (other.tag == "Enemy")
        {
            enemyInsight.Add(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pilot")
        {
            pilotInSight.Remove(other.gameObject);
        }
        if (other.tag == "Enemy")
        {
            enemyInsight.Remove(other.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        // 画出探测线
        Gizmos.color = Color.red;
        Vector3 direction1 = new Vector3(horizontalDetection, -this.transform.position.y, verticalDetection);
        Gizmos.DrawRay(this.transform.position, direction1);
        Vector3 direction2 = new Vector3(-horizontalDetection, -this.transform.position.y, verticalDetection);
        Gizmos.DrawRay(this.transform.position, direction2);
        Vector3 direction3 = new Vector3(-horizontalDetection, -this.transform.position.y, -verticalDetection);
        Gizmos.DrawRay(this.transform.position, direction3);
        Vector3 direction4 = new Vector3(horizontalDetection, -this.transform.position.y, -verticalDetection);
        Gizmos.DrawRay(this.transform.position, direction4);
    }

    void SearchA10() // 搜索函数
    {

        // 获取初始所搜起始点坐标
        initialStartPoint = new Vector3(targetTerrain.transform.position.x,flightHeightA10, targetTerrain.transform.position.z);

        // 计算位置距离差
        distanceDifference = System.Math.Sqrt(System.Math.Pow((this.transform.position.x - initialStartPoint.x), 2)
            + System.Math.Pow((this.transform.position.z - initialStartPoint.z), 2));

        // 计算方向向量
        postionDifferenceVector = new Vector3((this.transform.position.x - initialStartPoint.x),
            (this.transform.position.z - initialStartPoint.z), flightHeightA10).normalized;

        if (searchStatusA10 == false)
        {
            // 是否到达搜索起始点
            if (distanceDifference >= 0.1)
            {
                // 以最大飞行速度进入
                this.currentVelocityA10 = 0.05f;

                Quaternion rotate = Quaternion.LookRotation(initialStartPoint - this.transform.position);
                this.transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * angularVelocityA10);
                this.transform.Translate(this.transform.forward * currentVelocityA10 * Time.deltaTime, Space.World);
            }
            else
            {
                searchStatusA10 = true;
            }
        }
        else
        {

        }


        // 
    }

    void CoverA10()
    {
        // 掩护函数
    }

    void EscortA10()
    {
        // 护航函数
    }

    void InitValueA10()
    {
        lifeA10 = 100f; //剩余生命（换算成秒）

        //runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
    }

}