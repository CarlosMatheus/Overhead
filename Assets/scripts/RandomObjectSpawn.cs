using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomObjectSpawn : MonoBehaviour {

	public GameObject[] arr;
	public float[] chance;

	private float sum;
	private float val;
	private float rot;
	private float tam;

    private float speed = 3f;
    [SerializeField] float tolerance = 0.0005f;

	void Start ()
    {
		Ajust ();
		Select ();
	}

	//Ajust correctly the chances and randoms rotations values
	void Ajust()
    {
		sum = 0;
		for (int i = 0; i < chance.Length; i++)
			sum += chance [i];
		val = Random.value*sum;
		rot = Random.value * 360;
		tam = Random.value + 1;
	}

	//select randomly a element of the array
	void Select(){
		float compVal = 0;
		for (int i = 0; i < arr.Length; i++) {
			compVal += chance[i];
			if (val <= compVal) {
                if (SceneManager.GetActiveScene().name == "Main")
                    StartCoroutine(Spawn(i));
                else
                    SpawnObject(i);
                return;
			}
		}
	}

	//spawn in a random y rotation, the quaternion need to be converted to vector 3 to oparate it
	IEnumerator Spawn(int i)
    {
        yield return new WaitForSeconds(3f);

        Quaternion rotat = arr [i].transform.rotation;
		Vector3 rotatInVector = rotat.eulerAngles;
		rotatInVector = new Vector3 (rotatInVector.x, rot, rotatInVector.z);
		Vector3 pos = transform.position;
		pos = new Vector3 (pos.x, pos.y + 0.05f, pos.z);

		GameObject insta = (GameObject)Instantiate (arr [i], pos, Quaternion.Euler(rotatInVector));
        

		Vector3 finalScale = insta.transform.localScale * tam;
        insta.transform.localScale = Vector3.zero;

        while (finalScale.y - insta.transform.localScale.y >= tolerance)
        {
            insta.transform.localScale = insta.transform.localScale + speed * (finalScale - insta.transform.localScale) * Time.deltaTime;
            yield return null;
        }
    }

    void SpawnObject(int i)
    {
        Quaternion rotat = arr[i].transform.rotation;
        Vector3 rotatInVector = rotat.eulerAngles;
        rotatInVector = new Vector3(rotatInVector.x, rot, rotatInVector.z);
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x, pos.y + 0.05f, pos.z);
        GameObject insta = (GameObject)Instantiate(arr[i], pos, Quaternion.Euler(rotatInVector));
        insta.transform.localScale = insta.transform.localScale * tam;
    }
}
