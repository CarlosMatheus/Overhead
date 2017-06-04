using UnityEngine;

public class CameraControllerScript : MonoBehaviour {

	private bool doMovement = true;

	public float panSpeed = 30f;
	public float panBoardThickness = 10f;
	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape))
			doMovement = !doMovement;
		if (!doMovement)
			return;

		MoveScreen ();
		ZoomScroll ();
	}

	//move the camera white awsd or with mouse in the border
	void MoveScreen(){
		if ( Input.GetKey ("w") || 
			( Input.mousePosition.y >= (Screen.height - panBoardThickness) && Input.mousePosition.y <= Screen.height) ) {
			transform.Translate ( Vector3.forward * panSpeed * Time.deltaTime, Space.Self );
		}
		if ( Input.GetKey ("s") || 
			( Input.mousePosition.y <= panBoardThickness && Input.mousePosition.y >= 0f )  ) {
			transform.Translate ( Vector3.back * panSpeed * Time.deltaTime, Space.Self );
		}
		if ( Input.GetKey ("d") || 
			( Input.mousePosition.x >= Screen.width - panBoardThickness && Input.mousePosition.x <= Screen.width) ) {
			transform.Translate ( Vector3.right * panSpeed * Time.deltaTime, Space.Self );
		}
		if (Input.GetKey ("a") || 
			(Input.mousePosition.x <= panBoardThickness && Input.mousePosition.x >= 0f) ) {
			transform.Translate ( Vector3.left * panSpeed * Time.deltaTime, Space.Self );
		}
	}

	//Transform mouse scroll into zoom and defines min and max distances
	void ZoomScroll(){
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		Vector3 pos = transform.position;
		pos.y -= scroll * scrollSpeed * 200 * Time.deltaTime;
		pos.y = Mathf.Clamp (pos.y, minY, maxY); 
		transform.position = pos;
		if ( pos.y > minY+0.01f && pos.y < maxY-0.01f )
			transform.Translate (Vector3.forward * scroll * scrollSpeed * 200 * Time.deltaTime, Space.Self);
	}
}
