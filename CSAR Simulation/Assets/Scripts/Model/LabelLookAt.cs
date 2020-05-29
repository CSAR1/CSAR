using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelLookAt : MonoBehaviour {

    private GameObject Camera;
    private float distance;
    public float scale;

	// Use this for initialization
	void Start () {
        Camera = GameObject.Find("Main Camera");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        distance = 1.5f * Vector3.Magnitude(transform.position - Camera.transform.position);
        transform.LookAt(Camera.transform.position);
        transform.localScale = scale * Mathf.Pow(distance, 1f / 3f) * new Vector3(1f, 1f, 0.1f);
    }
}
