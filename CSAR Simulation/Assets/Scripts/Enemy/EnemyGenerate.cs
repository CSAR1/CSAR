using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class EnemyGenerate : MonoBehaviour
{
    private int enemiesNum;
    private int missileRange;
    private int missileMach;
    private int maxOverload;
    private int detectR;
    private MainMenu mainMenu;

    void Start()
    {
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenu.OnStart += InitValue;
    }
    
    void FIxedUpdate()
    {
        
    }

    void InitValue()
    {
        enemiesNum = EnemyDefinition.enemyNum;
        missileRange = EnemyDefinition.missileRange;
        missileMach = EnemyDefinition.missileMach;
        maxOverload = EnemyDefinition.maxOverload;
        detectR = EnemyDefinition.detectR;
    }
}
