using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    private GameObject target;
    private Transform m_Transform;
    public Transform sun;
    
    public float r; //半径
    public float w; 
    public float angle;//角度
    public float speed;
    public float x;
    public float z;

    private void Awake()
    {
        target = GameObject.Find("Target");
        m_Transform = gameObject.GetComponent<Transform>();
        m_Transform.position = new Vector3(2.4f, 1.4f, 1f);  //重置做圆周的开始位置
        sun = GameObject.Find("Sun").GetComponent<Transform>();
        r = Vector3.Distance(m_Transform.position, sun.position); //两个物品间的距离
        angle = 0f;
        w = 0.2f; // ---角速度
        speed = 0.25f;
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        m_Transform.LookAt(target.GetComponent<Transform>().position);
        if (Input .GetKey (KeyCode.D))
        {
            angle += w * Time.deltaTime;
            x = Mathf.Cos(angle) * r;
            z = Mathf.Sin(angle) * r;
            transform.position = new Vector3(sun.position.x + x, sun.position.y, sun.position.z + z);
        }

        if (Input.GetKey (KeyCode .A))
        {
            angle -= w * Time.deltaTime;
            x = Mathf.Cos(angle) * r;
            z = Mathf.Sin(angle) * r;
            transform.position = new Vector3(sun.position.x + x, sun.position.y, sun.position.z + z);
        }

        if (Input.GetKey(KeyCode.W))
        {
            r -= speed * Time.deltaTime;
            x = Mathf.Cos(angle) * r;
            z = Mathf.Sin(angle) * r;
            transform.position = new Vector3(sun.position.x + x, sun.position.y, sun.position.z + z);
        }

        if (Input.GetKey(KeyCode.S))
        {
            r += speed * Time.deltaTime;
            x = Mathf.Cos(angle) * r;
            z = Mathf.Sin(angle) * r;
            transform.position = new Vector3(sun.position.x + x, sun.position.y, sun.position.z + z);
        }
    }

}
