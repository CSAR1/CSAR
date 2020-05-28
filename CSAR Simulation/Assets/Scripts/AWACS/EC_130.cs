using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_130 : MonoBehaviour
{

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
        angle = 0f;
        gameObject.GetComponent<Transform>().position = new Vector3(0.3f, 0.7f, 1f);  //重置做圆周的开始位置
        r = Vector3.Distance(gameObject.GetComponent<Transform>().position, sun.GetComponent<Transform>().position); //两个物品间的距离
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        w = 0.065f;
        angle -= w * Time.deltaTime;
        x = Mathf.Cos(angle) * r;
        z = -Mathf.Sin(angle) * r;
        transform.position = new Vector3(sun.GetComponent<Transform>().position.x - x, 0.7f, sun.GetComponent<Transform>().position.z - z);
        gameObject.transform.Rotate(-0.065f * Vector3.up);
    }
}
