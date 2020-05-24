using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class EnemyGenerate : MonoBehaviour
{
    public int enemiesNum;
    public int missileRange;
    public int missileMach;
    public int maxOverload;
    public int detectR;
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
        if (enemiesNum > 5)
        {
            GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.88f, 0.042f, 0.63f), Quaternion.Euler(0f, 80f, 0f), transform);
            GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.88f, 0.0433f, 0.7215f), Quaternion.Euler(0f, 80f, 0f), transform);
            GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.91f, 0.0374f, 0.83f), Quaternion.Euler(0f, 80f, 0f), transform);
            GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.157f, 0.038f, 0.133f), Quaternion.Euler(0f, 160f, 0f), transform);
            GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.297f, 0.038f, 0.191f), Quaternion.Euler(0f, 160f, 0f), transform);
        }
    }
}
