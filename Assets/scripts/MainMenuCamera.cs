using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {

    [SerializeField] private float speed = 1;

    private void Awake()
    {
        speed = 1;
    }

    private void Start()
    {
        speed = 1;
    }

    void Update () {
        OrbitAround();
	}

    void OrbitAround()
    {
        transform.RotateAround(new Vector3(0, this.transform.position.y, 0), Vector3.up, speed * Time.deltaTime);
    }
}
