using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {

    //public Vector3 destPoint = Vector3.zero;

    public Vector3 direction = Vector3.zero;
    public float speed = 0.05f;

	// Use this for initialization
	void Start () 
    {
        //direction = destPoint - transform.position;
        Destroy(gameObject, 90);
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 movement = speed * direction * Time.deltaTime;
        transform.position = transform.position + movement;
	}
}
