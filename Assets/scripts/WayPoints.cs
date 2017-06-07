using UnityEngine;

public class WayPoints : MonoBehaviour {

	private Transform[] points;



	public Transform GetPoints(int i){
		return points[i];
	}

	public int GetPointsLength(){
		return points.Length;
	}

	//allocate the array with the wayPoints
	// Note: all wayPoints must be child of the gameObject in which this script is
	private void Awake (){
		points = new Transform[transform.childCount];
		for (int i = 0; i < points.Length; i++) {
			points [i] = transform.GetChild (i);
		}
	}


}
