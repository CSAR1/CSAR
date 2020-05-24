using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class Enemy : MonoBehaviour
{
    EnemyGenerate enemyParent;
    private int enemiesNum;
    private int missileRange;
    private int missileMach;
    private int maxOverload;
    private int detectR;

    void Start()
    {
        enemyParent = gameObject.GetComponentInParent<EnemyGenerate>();
    }
    
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            enemiesNum = enemyParent.enemiesNum;
            missileRange = enemyParent.missileRange;
            missileMach = enemyParent.missileMach;
            maxOverload = enemyParent.maxOverload;
            detectR = enemyParent.detectR;
            transform.position += transform.right * (-2.5f / 50000f * 2f);
        }
    }
}
