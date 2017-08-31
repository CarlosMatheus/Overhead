using UnityEngine;

public class CameraControllerScript : MonoBehaviour {

	public float panSpeed = 30f;
	public float panBoardThickness = 10f;
	public float scrollSpeed = 5f;
	public float scrollMaxSensibility = 1f;
	public float moduleDimension;
	public float minY = 10f;
	public float maxY = 80f;

	private float minX = 10f;
	private float maxX = 80f;
	private float minZ = 10f;
	private float maxZ = 80f;

	void Update () {
		MoveScreen ();
		ZoomScroll ();
		LimitPosition ();
	}

	private void Start(){
        minX = (-1f) * moduleDimension - 8f;
        maxX = moduleDimension - 10f;
		minZ = (-1f)*moduleDimension - 17f;
        maxZ = moduleDimension - 15f;
	}

	//move the camera white awsd or with mouse in the border
	void MoveScreen(){
		if ( Input.GetKey ("w") || 
			( Input.mousePosition.y >= (Screen.height - panBoardThickness) && Input.mousePosition.y <= Screen.height)
			|| Input.GetKey(KeyCode.UpArrow)) {
			GoForward ();
		}
		if ( Input.GetKey ("s") || 
			( Input.mousePosition.y <= panBoardThickness && Input.mousePosition.y >= 0f ) ||
			Input.GetKey(KeyCode.DownArrow)) {
			GoBack ();
		}
		if ( Input.GetKey ("d") || 
			( Input.mousePosition.x >= Screen.width - panBoardThickness && Input.mousePosition.x <= Screen.width) ||
			Input.GetKey(KeyCode.RightArrow)) {
			GoRight ();
		}
		if (Input.GetKey ("a") || 
			(Input.mousePosition.x <= panBoardThickness && Input.mousePosition.x >= 0f) ||
			Input.GetKey(KeyCode.LeftArrow)) {
			GoLeft ();
		}
	}

	//Transform mouse scroll into zoom and defines min and max distances
	void ZoomScroll(){
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		//limit de scrollMaxSensibility
		scroll = Mathf.Clamp (scroll, (-1f)*scrollMaxSensibility, scrollMaxSensibility);

		ZoomVerticalMoviment (scroll);
		ZoomHorizontalMoviment (scroll);
	}

	//Camera vetical moviment
	void ZoomVerticalMoviment(float scroll){
		Vector3 pos = transform.position;
		pos.y -= scroll * scrollSpeed * 200 * Time.deltaTime;
		transform.position = pos;
	}

	//this makes the camera go forward only if it is not in the heights limits
	void ZoomHorizontalMoviment(float scroll){
		Vector3 pos = transform.position;
		if ( pos.y > minY+0.01f && pos.y < maxY-0.01f )
			transform.Translate (Vector3.forward * scroll * scrollSpeed * 200 * Time.deltaTime, Space.Self);
	}

	//limit camera position
	void LimitPosition(){
		float posX = transform.position.x;
		float posY = transform.position.y;
		float posZ = transform.position.z;

		posX = Mathf.Clamp (posX, minX, maxX);
		posY = Mathf.Clamp (posY, minY, maxY);
		posZ = Mathf.Clamp (posZ, minZ, maxZ);

		transform.position = new Vector3 (posX, posY, posZ);
	}

	public void GoForward () {
		transform.Translate ( Vector3.forward * panSpeed * Time.deltaTime, Space.Self );
	}

	public void GoBack () {
		transform.Translate ( Vector3.back * panSpeed * Time.deltaTime, Space.Self );
	}

	public void GoLeft () {
		transform.Translate ( Vector3.left * panSpeed * Time.deltaTime, Space.Self );
	}

	public void GoRight () {
		transform.Translate ( Vector3.right * panSpeed * Time.deltaTime, Space.Self );
	}
}
