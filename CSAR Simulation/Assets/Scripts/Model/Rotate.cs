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
        if (gameObject.name == "A_10" || gameObject.name == "Plane001" || gameObject.name == "Plane002")
        {
            m_Transform.Rotate(Vector3.forward * speed, Space.Self);
        }
        if (gameObject.name == "MH-53" || gameObject.name == "AC130" || gameObject.name == "MH-60")
        {
            m_Transform.Rotate(Vector3.up * speed, Space.Self);
        }
    }
}
