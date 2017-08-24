using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDistance : MonoBehaviour
{
    private GameObject cameraObj;
    private MeshRenderer meshRenderer;
    private Renderer rend;
    private float limitDist = 50f;

    private void Start()
    {
        cameraObj = GameObject.FindWithTag("GameMaster").GetComponent<InstancesManager>().GetCameraPlayer();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        InvokeRepeating("CalculateDist", 0f, 0.3f);
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
