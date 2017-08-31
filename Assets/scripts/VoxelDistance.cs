using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoxelDistance : MonoBehaviour 
{
    private Mesh voxel4x4 = null;
    private Mesh voxel3x3 = null;
    private Mesh voxel2x2 = null;
    private Mesh voxel1x1 = null;
    private MeshFilter meshFilter;
    private GameObject cameraObj;
    private MeshRenderer meshRenderer;
    private InstancesManager instanceManager;

    private float distance;
    private float limitDist1 = 50f;
    private float limitDist2 = 40f;
    private float limitDist3 = 30f;
    private float limitDist4 = 20f;

    private void Start()
    {
        if (IsInCorrectScene() == false) return;
        meshFilter = GetComponent<MeshFilter>();
        instanceManager = GameObject.FindWithTag("GameMaster").GetComponent<InstancesManager>();
        cameraObj = instanceManager.GetCameraPlayer();
        voxel1x1 = instanceManager.GetVoxel1x1();
        voxel2x2 = instanceManager.GetVoxel2x2();
        voxel3x3 = instanceManager.GetVoxel3x3();
        voxel4x4 = instanceManager.GetVoxel4x4();
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
        distance = Vector3.Distance(transform.position, cameraObj.transform.position);

        if (distance < limitDist4)
        {
            meshFilter.mesh = voxel4x4;
            return;
        }
        if (distance < limitDist3 )
        {
            meshFilter.mesh = voxel3x3;
            return;
        }
        if (distance < limitDist2)
        {
            meshFilter.mesh = voxel2x2;
            return;
        }
        else
        {
            meshFilter.mesh = voxel1x1;
            return;
        }
    }
}
