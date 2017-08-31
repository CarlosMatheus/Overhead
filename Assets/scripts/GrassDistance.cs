using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrassDistance : MonoBehaviour
{
    private GameObject cameraObj;
    private MeshRenderer meshRenderer;
    private Renderer rend;
    private float limitDist = 50f;

    private void Start()
    {
        if (IsInCorrectScene() == false) return;
        cameraObj = GameObject.FindWithTag("GameMaster").GetComponent<InstancesManager>().GetCameraPlayer();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        //InvokeRepeating("CalculateDist", 0f, 0.3f);
    }

    private bool IsInCorrectScene()
    {
        bool a = SceneManager.GetActiveScene().buildIndex != 0;
        bool b = string.Equals(SceneManager.GetActiveScene().name, "MainMenu") == false;
        return (a && b);
    }

    private void CalculateDist()
    {
        if (Vector3.Distance(transform.position, cameraObj.transform.position) > limitDist)
        {
            //print(Vector3.Distance(transform.position, cameraObj.transform.position));
            meshRenderer.enabled = false;
        }
        else
        {
            meshRenderer.enabled = true;
        }
    }

}
