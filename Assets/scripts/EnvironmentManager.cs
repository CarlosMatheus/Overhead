using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {

    public GameObject[] Birds;
    public float birdAltitude;
    public float birdSpawnTimer;
    public float gameDimensions;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnBirds", 5, birdSpawnTimer);
	}
	

    void SpawnBirds()
    {
        int index = Random.Range(0, Birds.Length - 1);

        Vector3 initialPosition = new Vector3(-gameDimensions, birdAltitude, Random.Range(-gameDimensions, gameDimensions));
        Vector3 finalPosition = new Vector3(gameDimensions, birdAltitude, Random.Range(-gameDimensions, gameDimensions));
        Vector3 direction = finalPosition - initialPosition;
        direction = direction.normalized;
        float angle = Vector3.SignedAngle(Vector3.left, direction, Vector3.up);

        GameObject bird = Instantiate(Birds[index], initialPosition, Quaternion.Euler(new Vector3(0, angle, 0)));
        bird.GetComponent<BirdScript>().direction = direction;
    }
}
