using UnityEngine;

public class WayPoints2 : MonoBehaviour {

	//This array will be accessed from other script
	public static Transform[] points;

	//allocate the array with the wayPoints
	// Note: all wayPoints must be child of the gameObject in which this script is
	void Awake (){
		points = new Transform[transform.childCount];
		for (int i = 0; i < points.Length; i++) {
			points [i] = transform.GetChild (i);
		}
	}
}
