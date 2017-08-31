using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomVoxelSpawn : MonoBehaviour
{

    public GameObject[] arr;
    public float[] chance;

    private float sum;
    private float val;
    private float rot;

    private Vector3 curPos;
    private Vector3 finalPos;
    private GameObject voxel;

    private float speed = 6f;

    void Start()
    {
        Ajust();
        Select();
    }

    //Ajust correctly the chances and randoms rotations values
    void Ajust()
    {
        sum = 0;
        for (int i = 0; i < chance.Length; i++)
            sum += chance[i];
        val = Random.value * sum;
        rot = Mathf.Round(Random.value * 4f);
    }

    //select randomly a element of the array
    void Select()
    {
        float compVal = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            compVal += chance[i];
            if (val <= compVal)
            {
                if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "Tutorial")
                    StartCoroutine(Spawn(i));
                else
                    SpawnVoxel(i);
                return;
            }
        }
    }

    //spawn in a random y rotation, the quaternion need to be converted to vector 3 to oparate it
    IEnumerator Spawn(int i)
    {
        Quaternion rotat = arr[i].transform.rotation;
        Vector3 rotatInVector = rotat.eulerAngles;
        finalPos = transform.position;
        curPos = finalPos + new Vector3(0f, 4f, 0f);
        rotatInVector = new Vector3(rotatInVector.x, 90 * rot, rotatInVector.z);

        float time = Mathf.Pow((transform.position.x * transform.position.x + transform.position.z * transform.position.z), 0.43f);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(((time-1)+Random.Range(0, 1.5f))/8);
        voxel = Instantiate(arr[i], curPos, Quaternion.Euler(rotatInVector));

        while (voxel.transform.position.y - finalPos.y >= 0.0005f)
        {
            voxel.transform.position = voxel.transform.position + speed*(finalPos - voxel.transform.position)*Time.deltaTime;
            yield return null;
        }

        voxel.transform.position = finalPos;
        Destroy(gameObject);
    }

    //spawn in a random y rotation, the quaternion need to be converted to vector 3 to oparate it
    void SpawnVoxel(int i)
    {
        Quaternion rotat = arr[i].transform.rotation;
        Vector3 rotatInVector = rotat.eulerAngles;
        rotatInVector = new Vector3(rotatInVector.x, 90 * rot, rotatInVector.z);
        Instantiate(arr[i], transform.position, Quaternion.Euler(rotatInVector));
        Destroy(gameObject);
    }

}