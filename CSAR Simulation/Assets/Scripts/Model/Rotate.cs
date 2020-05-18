using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    private Transform m_Transform;

    // Use this for initialization
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        //speed = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Transform.Rotate(Vector3.forward * speed, Space.Self);
    }
}
