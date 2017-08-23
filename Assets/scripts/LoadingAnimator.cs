using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimator : MonoBehaviour {

    public RuntimeAnimatorController controller;

    Image image;

    SpriteRenderer fakeRenderer;

    Animator animator;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        fakeRenderer = GetComponent<SpriteRenderer>();
        fakeRenderer.enabled = false;

        animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = controller;
	}
	
	// Update is called once per frame
	void Update () {
		if(animator.runtimeAnimatorController)
        {
            image.sprite = fakeRenderer.sprite;
        }
	}
}
