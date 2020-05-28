using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalParameters
{
    static class SimulationRun
    {
        public static RunMode runMode = RunMode.pause;
        public static PilotDetectedMode pilotDetectedMode = PilotDetectedMode.notFound;
        public static bool inputPhase = true;
        public static bool pilotRecovered = false;
    }

    static class TaskDefinition
    {
        public static int pilotNum = 1; //遇险人数
        public static int lifeLeft = 8; //剩余生命
        public static HideEnemyCap hideEnemyCap = HideEnemyCap.high; //躲避敌方的能力
        public static int maxSpeed = 2; //最大移动速度
    }

    static class EnemyDefinition
    {
        public static int enemyNum = 2; //火力点数量
        public static int missileRange = 50; //对空导弹射程
        public static int missileMach = 2; //对空导弹马赫数
        public static int maxOverload = 15; //对空导弹最大过载
        public static int detectR = 20; //探测范围半径
    }

    static class EquipmentSelection
    {
        public static YDYH ydyh = YDYH.A_10;
        public static SAR sar = SAR.MH_53;
    }

    static class A_10
    {
        public static int maxSpeed = 700; //最大巡航速度
        public static int minSpeed = 500; //最小巡航速度
        public static int minR = 8; //最小转弯半径
        public static int containMembers = 1; //载员能力
        public static int fuelWeight = 1800; //燃油重量
        public static float fuelConsumption = 1.2f; //耗油率
        public static int height = 2000; //飞行高度
        public static int width = 2000; //扫掠宽度
        public static int weight = 11321; //空重
        public static Weapon aircraftGun = new Weapon(1, 5, 5, -1, -1, -1); //航炮
        public static Weapon rocket = new Weapon(1, 3, 8, 5, -1, -1); //火箭弹
        public static Weapon bomb = new Weapon(1, -1, 10, 7, -1, -1); //航弹
        public static Weapon missile = new Weapon(1, -1, 15, 12, 50, 3); //对地导弹
    }

    static class MH_53
    {
        public static int speed = 278; //巡航速度
        public static int num = 3; //载员人数
        public static int height = 1000; //飞行高度
        public static int maxDis = 1000; //最大航程
        public static int weight = 14515; //空重
        public static int fuelWeight = 3000; //燃油重量
        public static SARWeapon sarWeapon = SARWeapon.antiTank; //携带武器
        public static int weaponNum = 1; //武器数量
    }

    static class AC_130
    {
        public static int maxSpeed = 600; //最大巡航速度
        public static int minSpeed = 400; //最小巡航速度
        public static int minR = 8; //最小转弯半径
        public static int containMembers = 1; //载员能力
        public static int fuelWeight = 1800; //燃油重量
        public static float fuelConsumption = 1.2f; //耗油率
        public static int height = 2000; //飞行高度
        public static int width = 2000; //扫掠宽度
        public static int weight = 11321; //空重
        public static Weapon aircraftGun = new Weapon(1, 5, 5, -1, -1, -1); //航炮
        public static Weapon rocket = new Weapon(1, 3, 8, 5, -1, -1); //火箭弹
        public static Weapon bomb = new Weapon(1, -1, 10, 7, -1, -1); //航弹
        public static Weapon missile = new Weapon(1, -1, 15, 12, 50, 3); //对地导弹
    }

    static class MH_60
    {
        public static int speed = 250; //巡航速度
        public static int num = 3; //载员人数
        public static int height = 1000; //飞行高度
        public static int maxDis = 1000; //最大航程
        public static int weight = 14515; //空重
        public static int fuelWeight = 3000; //燃油重量
        public static SARWeapon sarWeapon = SARWeapon.antiTank; //携带武器
        public static int weaponNum = 1; //武器数量
    }

    static class ActionResult
    {
        public static bool reachSARArea = false; //是否到达搜索区域
        public static bool findTarget = false; //是否搜索到待救目标
        public static bool targetAlive = false; //待救目标是否还存活
        public static bool targetRescued = false; //是否救起待救目标
        public static bool returnToBase = false; //是否返回基地
        public static bool missionSucceed = false; //任务是否成功
    }

    static class TimeResult
    {
        public static float reachTime = -1f; //到达搜索区域耗时
        public static float searchTime = -1f; //搜索耗时
        public static float reachTargetTime = -1f; //到达待救目标位置耗时
        public static float targetRescued = -1f; //救起待救目标耗时
        public static float returnToBase = -1f; //返回基地耗时
        public static float time = -1f; //行动总耗时
    }

    static class LossResult
    {
        public static int aircraftLoss = 0; //装备损失数量
        public static int peopleLoss = 0; //人员损失数量
        public static float aircraftLossRate = 0f; //装备损失率
        public static float peopleLossRate = 0f; //人员损失率
    }

    static class AttackResult
    {
        public static int tankDestroied = 0; //击毁敌方装备数量
        public static int peopleKilled = 0; //击杀敌方人员数量
    }

    static class FuelResult
    {
        public static float fuelConsumed = 0f; //救援直升机耗油量
    }

    static class ScoreValue
    {
        public static float actionScore = 0f;
        public static float lossScore = 100f;
        public static float attackScore = 0f;
        public static float fuelScore = 100f;
        public static float overallScore = 30f;
    }

    enum YDYH
    {
        A_10,
        AC_130
    }

    enum SAR
    {
        MH_53,
        MH_60
    }

    enum HideEnemyCap
    {
        high, //高
        medium, //中
        low //低
    }

    struct Weapon
    {
        public Weapon(int num, int speed, int demage, int range, int maxRange, int minR)
        {
            this.num = num;
            this.speed = speed;
            this.demage = demage;
            this.range = range;
            this.maxRange = maxRange;
            this.minR = minR;
        }
        public int num; //载弹量
        public int speed; //射速
        public int demage; //单位毁伤能力
        public int range; //单位毁伤面积
        public int maxRange; //最大射程
        public int minR; //最小转弯半径
    }

    enum SARWeapon
    {
        antiTank, //反坦克武器
        gatlin, //加特林机枪
        gun //机枪吊舱
    }

    enum RunMode
    {
        pause,
        run
    }
    enum PilotDetectedMode
    {
        notFound,
        foundByEnemy,
        foundBySARTeam,
        foundByBoth
    }
}
