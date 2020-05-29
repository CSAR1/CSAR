using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameters;
using UIFramework;

public class RotorWingZ : MonoBehaviour
{
    private float speed_rotor = 20;
    // Start is called before the first frame update
    void Start()
    {

    }




    // Update is called once per frame
    void FixedUpdate()
    {
        if (SimulationRun.runMode == RunMode.run)
        {
            this.transform.Rotate(new Vector3(0, 0, speed_rotor));//螺旋桨旋转

        }
    }





}

