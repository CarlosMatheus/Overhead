using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageSpawn : MonoBehaviour {

    private Vector3 finalScale;
	
	void Start () {
        finalScale = transform.localScale;
        if (SceneManager.GetActiveScene().name == "Main")
        {
            transform.localScale = Vector3.zero;
            StartCoroutine(Spawn());
        }
        
	}

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3f);
        while (finalScale.y - transform.localScale.y >= 0.0005f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, 0.05f);
            yield return null;
        }
    }
}
