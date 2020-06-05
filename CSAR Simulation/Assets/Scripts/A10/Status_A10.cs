using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.XR.WSA;
using DG.Tweening.Plugins.Core.PathCore;
//using UnityEditor.Profiling.Memory.Experimental;

public class Status_A10 : MonoBehaviour
{

    // 声明时间与飞机生命值
    public float lifeA10;
    public float timePassedA10;

    // 定义飞行控制量
    private float maxSpeedA10;
    private float minSpeedA10;
    private float flightHeightA10;
    private float minRA10;
    private float angularVelocityA10;
    private float currentVelocityA10;

    // 定义地形中心
    private GameObject targetTerrainA10;
    private Vector3 initialStartPointA10;

    // 定义距离计算差
    private double distanceDifferenceA10;
    private Vector3 postionDifferenceVector;

    // 搜索路线切换
    private bool searchStatusA10;

    // 扇形搜索区域半径
    public float radiusSearchAreaA10;
    // 半径换算
    private float currentRadiusA10;
    // 扇形搜索边数序号
    public int pathNumberA10;
    // 扇形搜索目标点列表
    private List<Vector3> targetTerrainPositionA10 = new List<Vector3>();

    // 是否需要路径点切换
    private bool pathSwitchA10;
    // 目标路径点重置
    private bool pathResetA10;
 

    // 菜单控制
    private MainMenu mainMenuA10;
    private RunPanel runPanelA10;

    // 扫掠宽度(m)
    public float sweepWidthA10;

    // 扫掠范围定义
    private float horizontalDetectionA10;
    private float verticalDetectionA10;

    // 盒碰撞器定义
    private BoxCollider detectionBoxA10;

    // 飞行员探测列表
    public List<GameObject> pilotInSightA10 = new List<GameObject>();

    // 敌方探测列表
    public List<GameObject> enemyInsightA10 = new List<GameObject>();
    private List<float> enemyDistance = new List<float>();
    private bool enemyUpdate;
    private int enemyNumber;

    // 探测计次
    private int indexDetectionA10;

    // 巡航半径
    private float cruiseRadiusA10;

    // 飞行员目标
    private GameObject targetPilotA10;
    private Vector3 targetPilotPositionA10;
    private List<Vector3> targetCruisePositionA10 = new List<Vector3>();

    // 地方目标
    private Vector3 targetEnemyA10;
    private int indexEnemyA10;

    // 测试监控全局量
    public string mode;
    public string status;
    public float number;

    // Start is called before the first frame update
    void Start()
    {

        // 仿真控制设置
        mainMenuA10 = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenuA10.OnStart += InitValueA10;


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
            timePassedA10 += 0.8f / 3600f;

        }



    }

    void DetectionA10()  // 探测函数
    {

        // 扫掠范围计算
        horizontalDetectionA10 = sweepWidthA10 / 50000f * 2 / 2;
        verticalDetectionA10 = horizontalDetectionA10 / 2f;

        // 碰撞器体积设置
        detectionBoxA10.size = new Vector3(horizontalDetectionA10 * 2 * 100, flightHeightA10 * 2 * 100, verticalDetectionA10 * 2 * 100);

        // 碰撞触发判断
        if (pilotInSightA10.Count > 0)
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
                targetPilotA10 = pilotInSightA10[0];
                pathNumberA10 = 0;

                // 输出时间及结果
                runPanelA10.AddInformation(timePassedA10.ToString("0.00") + "小时后：A10 已发现待救援飞行员");
                TimeResult.searchTime = timePassedA10;
                ActionResult.findTarget = true;

                // 开启路径切换
                pathResetA10 = true;
                pathNumberA10 = 0;
            }
        }

    }


    private void OnTriggerEnter(Collider other)  // 通过进入碰撞器实现探测
    {
        if (other.tag == "Pilot")
        {
            if (indexDetectionA10 == 0)
            {
                // pilot 只获取一次
                pilotInSightA10.Add(other.gameObject);

            }

        }

        if (other.tag == "Enemy")
        {
            // 判断是否已经存在于列表中
            if (enemyInsightA10.Contains(other.gameObject) == false)
            {
                enemyInsightA10.Add(other.gameObject);
            }
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
        initialStartPointA10 = targetPosition;

        // 计算位置距离差
        distanceDifferenceA10 = System.Math.Sqrt(System.Math.Pow((this.transform.position.x - initialStartPointA10.x), 2)
            + System.Math.Pow((this.transform.position.y - initialStartPointA10.y), 2));

        // 计算方向向量
        postionDifferenceVector = new Vector3((this.transform.position.x - initialStartPointA10.x),
            (this.transform.position.y - initialStartPointA10.y), flightHeightA10).normalized;

        // 朝向预定位置飞行
        Quaternion rotate = Quaternion.LookRotation(initialStartPointA10 - this.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * angularVelocityA10);
        transform.Translate(Vector3.forward * currentVelocityA10 * Time.deltaTime, Space.Self);

        // 判断是否需要路径切换
        if (pathSwitchA10 == true)
        {
            // 是否到达搜索起始点
            if (distanceDifferenceA10 >= 0.1)
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
        pathSwitchA10 = true;

        // 以最大飞行速度进入
        this.currentVelocityA10 = this.maxSpeedA10;

        if (pathResetA10 == true)
        {

            targetTerrainPositionA10.Clear();
            // 路径点计算
            // No.0 路径点，搜索中心
            targetTerrainPositionA10.Add(new Vector3(targetTerrainA10.transform.position.x, flightHeightA10, targetTerrainA10.transform.position.z));
            // No.1
            targetTerrainPositionA10.Add(new Vector3((targetTerrainPositionA10[0].x - 0.5f * currentRadiusA10), flightHeightA10, (targetTerrainPositionA10[0].z + 1.73f / 2f * currentRadiusA10)));
            // No.2
            targetTerrainPositionA10.Add(new Vector3((targetTerrainPositionA10[1].x + currentRadiusA10), flightHeightA10, targetTerrainPositionA10[1].z));
            // No.3
            targetTerrainPositionA10.Add(new Vector3(targetTerrainPositionA10[0].x, flightHeightA10, targetTerrainPositionA10[0].z));
            // No.4
            targetTerrainPositionA10.Add(new Vector3((targetTerrainPositionA10[3].x - 0.5f * currentRadiusA10), flightHeightA10, (targetTerrainPositionA10[3].z - 1.73f / 2 * currentRadiusA10)));
            // No.5
            targetTerrainPositionA10.Add(new Vector3((targetTerrainPositionA10[4].x - 0.5f * currentRadiusA10), flightHeightA10, targetTerrainPositionA10[0].z));
            // No.6
            targetTerrainPositionA10.Add(new Vector3(targetTerrainPositionA10[0].x, flightHeightA10, targetTerrainPositionA10[0].z));
            // No.7
            targetTerrainPositionA10.Add(new Vector3((targetTerrainPositionA10[6].x + currentRadiusA10), flightHeightA10, targetTerrainPositionA10[0].z));
            // No.8
            targetTerrainPositionA10.Add(new Vector3(targetTerrainPositionA10[2].x, flightHeightA10, targetTerrainPositionA10[4].z));
            // No.9
            targetTerrainPositionA10.Add(new Vector3(targetTerrainPositionA10[0].x, flightHeightA10, targetTerrainPositionA10[0].z));

            pathResetA10 = false;
        }

        if (SimulationRun.pilotDetectedMode == PilotDetectedMode.notFound || SimulationRun.pilotDetectedMode==PilotDetectedMode.foundByEnemy)
        {
            number = pathNumberA10;
            if (pathNumberA10 < 9)
            {
                // 执行搜索
                if (searchStatusA10 == true)
                {
                    pathNumberA10 += 1;
                }

                PathSetting(targetTerrainPositionA10[pathNumberA10]);
            }
            else
            {
                pathNumberA10 = 0;
                PathSetting(targetTerrainPositionA10[pathNumberA10]);
                
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
        pathSwitchA10 = true;

        // 设定巡航半径
        if (horizontalDetectionA10 > minRA10)
        {
            cruiseRadiusA10 = horizontalDetectionA10;
        }
        else
        {
            cruiseRadiusA10 = minRA10;
        }

        // 放大巡航半径
        cruiseRadiusA10 = cruiseRadiusA10 * 2f;

        // 获取目标点坐标
        targetPilotPositionA10 = new Vector3(targetPilotA10.transform.position.x, targetPilotA10.transform.position.y, targetPilotA10.transform.position.z);

        if (pathResetA10 == true)
        {
            targetCruisePositionA10.Clear();
            // No.0
            targetCruisePositionA10.Add(new Vector3((targetPilotPositionA10.x + cruiseRadiusA10), flightHeightA10, targetPilotPositionA10.z));
            // No.1
            targetCruisePositionA10.Add(new Vector3(targetPilotPositionA10.x, flightHeightA10, (targetPilotPositionA10.z - cruiseRadiusA10)));
            // No.2
            targetCruisePositionA10.Add(new Vector3((targetPilotPositionA10.x - cruiseRadiusA10), flightHeightA10, targetPilotPositionA10.z));
            // No.3
            targetCruisePositionA10.Add(new Vector3(targetPilotPositionA10.x, flightHeightA10, (targetPilotPositionA10.z + cruiseRadiusA10)));
            
            pathResetA10 = false;


        }
        else
        {

            if (SimulationRun.pilotDetectedMode == PilotDetectedMode.foundBySARTeam || SimulationRun.pilotDetectedMode == PilotDetectedMode.foundByBoth)
            {
                number = pathNumberA10;

                if (searchStatusA10 == true)
                {
                    pathNumberA10 += 1;
                }

                if (pathNumberA10 >= 4 )
                {

                    pathNumberA10 = 0;
                }

                PathSetting(targetCruisePositionA10[pathNumberA10]);

                if (SimulationRun.pilotRecovered == true)
                {
                    pathResetA10 = true;
                }
                else
                {
                    pathResetA10 = false;
                }

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

        pathSwitchA10 = true;

        enemyNumber = enemyInsightA10.Count;

        // 现阶段演示不需要执行具体计算
        // 计算敌方和飞行员距离
        //if (enemyUpdate)
        //{
        //    enemyDistance.Clear();

        //    for(int i=0; i < enemyNumber; i++)
        //    {
        //        enemyDistance.Add((float)Math.Pow((enemyInsightA10[i].transform.position.x - pilotInSightA10[0].transform.position.x), 2f)
        //            + (float)Math.Pow((enemyInsightA10[i].transform.position.z - pilotInSightA10[0].transform.position.z), 2f));
        //    }

        //}
        //else
        //{
        //    enemyUpdate = true;
        //}

        // 判断距离最短的敌方，并设置为目标点
        // 判断敌方生命值
        // 距离较小时，执行打击函数（或脚本）

        // 获取目标点位置
        if (indexEnemyA10 <= enemyNumber)
        {
            targetEnemyA10 = enemyInsightA10[indexEnemyA10].transform.position;
            PathSetting(targetEnemyA10);
            if (searchStatusA10 == true)
            {
                indexEnemyA10 += 1;
            }
        }
        else
        {
            indexEnemyA10 = 0;
        }


    }

    void InitValueA10()
    {
        // 机型选择与脚本激活
        if (EquipmentSelection.ydyh != YDYH.A_10)
        {
            GameObject.Find("A10").GetComponent<Status_A10>().enabled = false;
        }
        else
        {
            GameObject.Find("A10").GetComponent<Status_A10>().enabled = true;
        }

        lifeA10 = 100f; //剩余生命（换算成秒）

        timePassedA10 = 1.3f;

        // 性能设置
        //this.maxSpeedA10 = A_10.maxSpeed / 25000 * 40;
        //this.minSpeedA10 = A_10.minSpeed / 25000 * 40;

        this.maxSpeedA10 = 0.2f;
        this.minSpeedA10 = 0.1f;

        // this.minRA10 = A_10.minR / 25000;
        this.minRA10 = 0.1f;

        // 搜索起始点
        targetTerrainA10 = GameObject.Find("/Terrain/Target");

        // 飞行高度
        flightHeightA10 = 0.6f;

        // 入场起始点
        this.transform.position = new Vector3(2.5f, flightHeightA10, -0.5f);

        // 生命值设置
        lifeA10 = 100;

        // 扫掠宽度
        sweepWidthA10 = 800;

        // 获取碰撞器
        detectionBoxA10 = this.GetComponent<BoxCollider>();

        // 发现计次
        indexDetectionA10 = 0;

        // 搜索状态
        searchStatusA10 = false;
        pathNumberA10 = 0;

        // 扇形搜索初始化
        radiusSearchAreaA10 = 15000;
        currentRadiusA10 = radiusSearchAreaA10 / 25000;
        pathSwitchA10 = false;
        pathResetA10 = true;

        // 巡航半径
        cruiseRadiusA10 = 0;
        targetPilotPositionA10 = new Vector3(0, 0, 0);

        // enemyUpdate = true;
        indexEnemyA10 = 0;

        runPanelA10 = UIManager.Instance.GetPanel(UIPanelType.Run) as RunPanel;
    }

    //void OnDrawGizmosSelected()
    //{
    //    // 画出探测线
    //    Gizmos.color = Color.red;
    //    Vector3 direction1 = new Vector3(horizontalDetection, -this.transform.position.y, verticalDetection);
    //    Gizmos.DrawRay(this.transform.position, direction1);
    //    Vector3 direction2 = new Vector3(-horizontalDetection, -this.transform.position.y, verticalDetection);
    //    Gizmos.DrawRay(this.transform.position, direction2);
    //    Vector3 direction3 = new Vector3(-horizontalDetection, -this.transform.position.y, -verticalDetection);
    //    Gizmos.DrawRay(this.transform.position, direction3);
    //    Vector3 direction4 = new Vector3(horizontalDetection, -this.transform.position.y, -verticalDetection);
    //    Gizmos.DrawRay(this.transform.position, direction4);
    //}

}
