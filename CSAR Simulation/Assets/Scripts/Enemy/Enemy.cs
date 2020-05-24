using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class Enemy : MonoBehaviour
{
    EnemyGenerate enemyParent;

    void Start()
    {
        enemyParent = gameObject.GetComponentInParent<EnemyGenerate>();
    }
    
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {

        }
    }
}
