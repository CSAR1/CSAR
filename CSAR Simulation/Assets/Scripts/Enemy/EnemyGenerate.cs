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

    private GameObject[] enemies = new GameObject[8];

    void Start()
    {
        mainMenu = UIManager.Instance.GetPanel(UIPanelType.MainMenu) as MainMenu;
        mainMenu.OnStart += InitValue;
        enemies[0] = GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(0.41f, 0.062f, 0.39f), Quaternion.Euler(0f, 92f, 0f), transform);
        enemies[1] = GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(0.515f, 0.07f, 0.385f), Quaternion.Euler(0f, 92f, 0f), transform);
        enemies[2] = GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(0.316f, 0.095f, 0.456f), Quaternion.Euler(0f, 92f, 0f), transform);
        enemies[3] = GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.88f, 0.042f, 0.63f), Quaternion.Euler(0f, 80f, 0f), transform);
        enemies[4] = GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.88f, 0.044f, 0.7215f), Quaternion.Euler(0f, 80f, 0f), transform);
        enemies[5] = GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.91f, 0.0374f, 0.83f), Quaternion.Euler(0f, 80f, 0f), transform);
        enemies[6] = GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.157f, 0.038f, 0.133f), Quaternion.Euler(0f, 160f, 0f), transform);
        enemies[7] = GameObject.Instantiate(Resources.Load<GameObject>("Enemies/Military Truck Variant"), new Vector3(1.297f, 0.038f, 0.191f), Quaternion.Euler(0f, 160f, 0f), transform);
        for (int i = 3; i < 8; i++)
        {
            enemies[i].SetActive(false);
        }

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
        ResetEnemies();

        if (enemiesNum > 5)
        {
            for (int i = 3; i < 8; i++)
            {
                enemies[i].SetActive(true);
            }
        }
    }

    void ResetEnemies()
    {
        enemies[0].transform.position = new Vector3(0.41f, 0.062f, 0.39f);
        enemies[0].transform.eulerAngles = new Vector3(0f, 92f, 0f);
        enemies[1].transform.position = new Vector3(0.515f, 0.07f, 0.385f);
        enemies[1].transform.eulerAngles = new Vector3(0f, 92f, 0f);
        enemies[2].transform.position = new Vector3(0.316f, 0.095f, 0.456f);
        enemies[2].transform.eulerAngles = new Vector3(0f, 92f, 0f);
        enemies[3].transform.position = new Vector3(1.88f, 0.042f, 0.63f);
        enemies[3].transform.eulerAngles = new Vector3(0f, 80f, 0f);
        enemies[4].transform.position = new Vector3(1.88f, 0.044f, 0.7215f);
        enemies[4].transform.eulerAngles = new Vector3(0f, 80f, 0f);
        enemies[5].transform.position = new Vector3(1.91f, 0.0374f, 0.83f);
        enemies[5].transform.eulerAngles = new Vector3(0f, 80f, 0f);
        enemies[6].transform.position = new Vector3(1.157f, 0.038f, 0.133f);
        enemies[6].transform.eulerAngles = new Vector3(0f, 160f, 0f);
        enemies[7].transform.position = new Vector3(1.297f, 0.038f, 0.191f);
        enemies[7].transform.eulerAngles = new Vector3(0f, 160f, 0f);
    }
}
