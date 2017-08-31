using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageSpawn : MonoBehaviour 
{
    private Vector3 finalScale;
    private float speed = 3f;

    void Start()
    {
        finalScale = transform.localScale;
        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "Tutorial")
        {
            transform.localScale = Vector3.zero;
            StartCoroutine(Spawn());
        }

    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.5f);
        while (finalScale.y - transform.localScale.y >= 0.0005f)
        {
            transform.localScale = transform.localScale + speed * (finalScale - transform.localScale) * Time.deltaTime;
            yield return null;
        }
    }
}