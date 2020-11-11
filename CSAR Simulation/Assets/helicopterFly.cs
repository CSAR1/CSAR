using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopterFly : MonoBehaviour
{
    public GameObject  pilot;
    private bool fly = true;
    private bool Down = false;
    private bool up = false;
    private bool back = false;
    public float speed;

    private float h = -0.5f;

    private Vector3 startPosition;
    private Vector3 distanceToPilot;
    private Vector3 distanceToStart;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(0, h, -3f);
        this.transform.position =startPosition ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceToStart = new Vector3(startPosition.x - this.transform.position.x, 0, startPosition.z - this.transform.position.z);
        distanceToPilot = new Vector3(pilot.transform.position.x - this.transform.position.x, 0, pilot.transform.position .z - this.transform.position.z);
        if (fly)
        {
            this.transform.Translate(distanceToPilot.normalized*speed*Time .fixedDeltaTime  , Space.World);
            this.transform.LookAt(new Vector3(pilot.transform.position.x, h, pilot.transform.position.z));
            if(distanceToPilot .magnitude < 0.01)
            {
                fly = false;
                Down = true;
            }
        }

        if(Down)
        {
            this.transform.Translate(-transform.up *speed* Time.fixedDeltaTime, Space.World);
            if((this .transform .position .y-pilot .transform .position.y) < 0.3)
            {
                Down = false;
                pilot.GetComponent<Renderer>().enabled = false;
                up = true;
            }
        }

        if (up)
        {
            this.transform.Translate(transform.up *speed*Time.fixedDeltaTime, Space.World);
            if (this.transform.position.y >h)
            {
                up  = false;

                back = true;
            }
        }

        if(back)
        {
            this.transform.Translate(distanceToStart .normalized  *speed* Time.fixedDeltaTime, Space.World);
            this.transform.LookAt(new Vector3(startPosition .x, h, startPosition .z));
            if (distanceToStart .magnitude < 0.01)
            {
                this.transform.Translate(Vector3 .zero  * Time.fixedDeltaTime, Space.World);
            }
        }
        
    }
}
