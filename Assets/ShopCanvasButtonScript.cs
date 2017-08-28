using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCanvasButtonScript : MonoBehaviour {

    private Animation animation;

    private void Start()
    {
        animation = gameObject.GetComponent<Animation>();
    }

    private void OnMouseEnter()
    {
        print("entrou");
        animation.PlayQueued("hover");
    }

    private void OnMouseExit()
    {
        animation.PlayQueued("unhover");
    }

    private void OnMouseDown()
    {
        animation.PlayQueued("unhover");
    }

}
