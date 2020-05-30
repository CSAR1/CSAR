using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.XR.WSA;
using DG.Tweening.Plugins.Core.PathCore;

public class Status_A10 : MonoBehaviour
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
    private float currentVelocityA10;

    // 定义地形中心
    private GameObject targetTerrain;
    private Vector3 initialStartPoint;

    // 定义距离计算差
    private double distanceDifference;
    private Vector3 postionDifferenceVector;

    // 搜索路线切换
    private bool searchStatusA10;

    // 扇形搜索区域半径
    public float radiusSearchArea;
    // 半径换算
    private float currentRadius;
    // 扇形搜索边数序号
    public int pathNumber;
    // 扇形搜索目标点列表
    private List<Vector3> targetTerrainPosition = new List<Vector3>();

    // 是否需要路径点切换
    private bool pathSwitch;
    // 目标路径点重置
    private bool pathReset;
 

    // 菜单控制
    private MainMenu mainMenu;
    private RunPanel runPanel;

    // 扫掠宽度(m)
    public float sweepWidth;

    // 扫掠范围定义
    private float horizontalDetection;
    private float verticalDetection;

    // 盒碰撞器定义
    private BoxCollider detectionBox;

    // 探测列表
    public List<GameObject> pilotInSight = new List<GameObject>();
    public List<GameObject> enemyInsight = new List<GameObject>();

    // 探测计次
    private int indexDetectionA10;

    // 巡航半径
    private float cruiseRadius;

    // 飞行员目标
    private GameObject targetPilot;
    private Vector3 targetPilotPosition;
    private List<Vector3> targetCruisePosition = new List<Vector3>();


    // 测试监控全局量
    public string mode;
    public string status;
    public float number;

    // Start is called before the first frame update
    void Start()
    {

        // 仿真控制设置
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenu.OnStart += InitValueA10;


        runPanel = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // 测试全局量
        //mode = SimulationRun.pilotDetectedMode.ToString();
        //path = targetTerrainPosition[pathNumber].ToString();

        // 实时角速度
        this.angularVelocityA10 = this.currentVelocityA10 / this.minRA10;

        if (SimulationRun.runMode == RunMode.run)
        {

            // 搜索发现
            DetectionA10();

            if (SimulationRun.pilotRecovered == false)
            {
                // 飞行员未被救起

                switch (SimulationRun.pilotDetectedMode)
                {
                    case PilotDetectedMode.notFound:
                        // 未被发现，搜索函数
                        SearchA10();
                        break;

                    case PilotDetectedMode.foundByEnemy:
                        // 未被发现，搜索函数
                        SearchA10();
                        break;

                    case PilotDetectedMode.foundBySARTeam:
                        // 发现，掩护函数
                        CoverA10();
                        break;

                    case PilotDetectedMode.foundByBoth:
                        // 发现，掩护函数
                        DefenseA10();
                        break;
                }
            }
            else
            {
                // 飞行员已被救起
                switch (SimulationRun.pilotDetectedMode)
                {
                    case PilotDetectedMode.foundBySARTeam:
                        // 发现，掩护函数
                        CoverA10();
                        break;

                    case PilotDetectedMode.foundByBoth:
                        // 发现，掩护函数
                        DefenseA10();
                        break;
                }
            }

            // timer
            timePassed += 0.8f / 3600f;

        }



    }

    void DetectionA10()  // 探测函数
    {

        // 扫掠范围计算
        horizontalDetection = sweepWidth / 50000f * 2 / 2;
        verticalDetection = horizontalDetection / 2f;

        // 碰撞器体积设置
        detectionBox.size = new Vector3(horizontalDetection * 2 * 100, flightHeightA10 * 2 * 100, verticalDetection * 2 * 100);

        // 碰撞触发判断
        if (pilotInSight.Count > 0)
        {
            if (indexDetectionA10 == 0)
            {
                if (SimulationRun.pilotDetectedMode == PilotDetectedMode.foundByEnemy)
                {
                    SimulationRun.pilotDetectedMode = PilotDetectedMode.foundByBoth;
                }
                else
                {
                    SimulationRun.pilotDetectedMode = PilotDetectedMode.foundBySARTeam;
                }

                indexDetectionA10 = 1;
                //Console.WriteLine("Pilot has been found");
                targetPilot = pilotInSight[0];
                pathNumber = 0;

                // 输出时间及结果
                runPanel.ShowInformation(timePassed.ToString("0.00") + "小时后：A10 已发现待救援飞行员");
                TimeResult.searchTime = timePassed;
                ActionResult.findTarget = true;

                // 开启路径切换
                pathReset = true;
                pathNumber = 0;
            }
        }

    }


    private void OnTriggerEnter(Collider other)  // 通过进入碰撞器实现探测
    {
        if (other.tag == "Pilot")
        {
            if (indexDetectionA10 == 0)
            {
                pilotInSight.Add(other.gameObject);

            }

        }

        if (other.tag == "Enemy")
        {
            enemyInsight.Add(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)  // 通过退出碰撞器实现探测
    {
        if (other.tag == "Pilot")
        {
            // pilotInSight.Remove(other.gameObject);
        }
        if (other.tag == "Enemy")
        {
            // enemyInsight.Remove(other.gameObject);
        }
    }

    // 前往目标点运动函数
    void PathSetting(Vector3 targetPosition)  // 寻路函数
    {
        // 获取初始所搜起始点坐标
        initialStartPoint = targetPosition;

        // 计算位置距离差
        distanceDifference = System.Math.Sqrt(System.Math.Pow((this.transform.position.x - initialStartPoint.x), 2)
            + System.Math.Pow((this.transform.position.y - initialStartPoint.y), 2));

        // 计算方向向量
        postionDifferenceVector = new Vector3((this.transform.position.x - initialStartPoint.x),
            (this.transform.position.y - initialStartPoint.y), flightHeightA10).normalized;

        // 朝向预定位置飞行
        Quaternion rotate = Quaternion.LookRotation(initialStartPoint - this.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * angularVelocityA10);
        transform.Translate(Vector3.forward * currentVelocityA10 * Time.deltaTime, Space.Self);

        // 判断是否需要路径切换
        if (pathSwitch == true)
        {
            // 是否到达搜索起始点
            if (distanceDifference >= 0.1)
            {
                // 未到不切换
                searchStatusA10 = false;
            }
            else
            {
                // 到达切换
                searchStatusA10 = true;
            }
        }



    }  

    void SearchA10() // 搜索函数
    {
        status = "Searching";
        // 需切换路径点
        pathSwitch = true;

        // 以最大飞行速度进入
        this.currentVelocityA10 = this.maxSpeedA10;

        if (pathReset == true)
        {

            targetTerrainPosition.Clear();
            // 路径点计算
            // No.0 路径点，搜索中心
            targetTerrainPosition.Add(new Vector3(targetTerrain.transform.position.x, flightHeightA10, targetTerrain.transform.position.z));
            // No.1
            targetTerrainPosition.Add(new Vector3((targetTerrainPosition[0].x - 0.5f * currentRadius), flightHeightA10, (targetTerrainPosition[0].z + 1.73f / 2f * currentRadius)));
            // No.2
            targetTerrainPosition.Add(new Vector3((targetTerrainPosition[1].x + currentRadius), flightHeightA10, targetTerrainPosition[1].z));
            // No.3
            targetTerrainPosition.Add(new Vector3(targetTerrainPosition[0].x, flightHeightA10, targetTerrainPosition[0].z));
            // No.4
            targetTerrainPosition.Add(new Vector3((targetTerrainPosition[3].x - 0.5f * currentRadius), flightHeightA10, (targetTerrainPosition[3].z - 1.73f / 2 * currentRadius)));
            // No.5
            targetTerrainPosition.Add(new Vector3((targetTerrainPosition[4].x - 0.5f * currentRadius), flightHeightA10, targetTerrainPosition[0].z));
            // No.6
            targetTerrainPosition.Add(new Vector3(targetTerrainPosition[0].x, flightHeightA10, targetTerrainPosition[0].z));
            // No.7
            targetTerrainPosition.Add(new Vector3((targetTerrainPosition[6].x + currentRadius), flightHeightA10, targetTerrainPosition[0].z));
            // No.8
            targetTerrainPosition.Add(new Vector3(targetTerrainPosition[2].x, flightHeightA10, targetTerrainPosition[4].z));
            // No.9
            targetTerrainPosition.Add(new Vector3(targetTerrainPosition[0].x, flightHeightA10, targetTerrainPosition[0].z));

            pathReset = false;
        }

        if (SimulationRun.pilotDetectedMode == PilotDetectedMode.notFound || SimulationRun.pilotDetectedMode==PilotDetectedMode.foundByEnemy)
        {
            number = pathNumber;
            if (pathNumber < 9)
            {
                // 执行搜索
                if (searchStatusA10 == true)
                {
                    pathNumber += 1;
                }

                PathSetting(targetTerrainPosition[pathNumber]);
            }
            else
            {
                pathNumber = 0;
                PathSetting(targetTerrainPosition[pathNumber]);
                
            }
        }
        else
        {
            // 发现飞行员
            // 构成循环引用问题

            // CoverA10();
        }

    }

    void CoverA10()  // 掩护函数
    {
        status = "Covering";

        this.currentVelocityA10 = this.minSpeedA10;

        // 需切换路径点
        pathSwitch = true;

        // 设定巡航半径
        if (horizontalDetection > minRA10)
        {
            cruiseRadius = horizontalDetection;
        }
        else
        {
            cruiseRadius = minRA10;
        }

        // 放大巡航半径
        cruiseRadius = cruiseRadius * 2f;

        // 获取目标点坐标
        targetPilotPosition = new Vector3(targetPilot.transform.position.x, targetPilot.transform.position.y, targetPilot.transform.position.z);

        if (pathReset == true)
        {
            targetCruisePosition.Clear();
            // No.0
            targetCruisePosition.Add(new Vector3((targetPilotPosition.x + cruiseRadius), flightHeightA10, targetPilotPosition.z));
            // No.1
            targetCruisePosition.Add(new Vector3(targetPilotPosition.x, flightHeightA10, (targetPilotPosition.z - cruiseRadius)));
            // No.2
            targetCruisePosition.Add(new Vector3((targetPilotPosition.x - cruiseRadius), flightHeightA10, targetPilotPosition.z));
            // No.3
            targetCruisePosition.Add(new Vector3(targetPilotPosition.x, flightHeightA10, (targetPilotPosition.z + currentRadius)));

            pathReset = false;

        }
        else
        {

            if (SimulationRun.pilotDetectedMode == PilotDetectedMode.foundBySARTeam || SimulationRun.pilotDetectedMode == PilotDetectedMode.foundByBoth)
            {
                number = pathNumber;

                if (searchStatusA10 == true)
                {
                    pathNumber += 1;
                }

                if (pathNumber >= 4 )
                {

                    pathNumber = 0;
                }

                PathSetting(targetCruisePosition[pathNumber]);

            }
            else
            {
                // 构成循环引用问题

                // SearchA10();
            }
        }


    }

    void DefenseA10()  // 护航攻击函数
    {
        // 不切换路径点
        pathSwitch = false;

        // 计算敌方和飞行员距离

        // 判断距离最短的敌方，并设置为目标点

        // 判断敌方生命值
        // 距离较小时，执行打击函数（或脚本）


    }

    void InitValueA10()
    {
        lifeA10 = 100f; //剩余生命（换算成秒）

        timePassed = 1.3f;

        // 性能设置
        //this.maxSpeedA10 = A_10.maxSpeed / 25000 * 40;
        //this.minSpeedA10 = A_10.minSpeed / 25000 * 40;

        this.maxSpeedA10 = 0.2f;
        this.minSpeedA10 = 0.1f;

        // this.minRA10 = A_10.minR / 25000;
        this.minRA10 = 0.1f;

        // 搜索起始点
        targetTerrain = GameObject.Find("/Terrain/Target");

        // 飞行高度
        flightHeightA10 = 0.6f;

        // 入场起始点
        this.transform.position = new Vector3(2.5f, flightHeightA10, -0.5f);

        // 生命值设置
        lifeA10 = 100;

        // 扫掠宽度
        sweepWidth = 800;

        // 获取碰撞器
        detectionBox = this.GetComponent<BoxCollider>();

        // 发现计次
        indexDetectionA10 = 0;

        // 搜索状态
        searchStatusA10 = false;
        pathNumber = 0;

        // 扇形搜索初始化
        radiusSearchArea = 15000;
        currentRadius = radiusSearchArea / 25000;
        pathSwitch = false;
        pathReset = true;

        // 巡航半径
        cruiseRadius = 0;
        targetPilotPosition = new Vector3(0, 0, 0);
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

}
