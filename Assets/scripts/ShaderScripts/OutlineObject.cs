using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineObject : MonoBehaviour {

    [SerializeField] public Color GlowColor;
    public float FadeFactor = 5;

    private Renderer[] _renderers;
    private List<Material> _materials = new List<Material>();
    private Color _targetColor;
    public Color _currentColor;

	void Start () {
        _renderers = GetComponentsInChildren<Renderer>(); //Gets all the Children Renderers, to get access to their materials

        foreach (Renderer renderer in _renderers)
        {
            _materials.AddRange(renderer.materials); //Getting access to their materials
        }
	}

    private void OnMouseEnter()
    {
        Debug.Log("Entrouu");
        _targetColor = GlowColor; //If the mouse is on the collider, select the color to glow
        enabled = true;
    }

    private void OnMouseExit()
    {
        _targetColor = Color.black; //If the mouse left the collider, make the aura black, so the effect won't appear
        enabled = true;
    }

    void Update () {
        _currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * FadeFactor); //Just some fade in and out effect

		for (int i = 0; i < _materials.Count; i ++)
        {
            _materials[i].SetColor("_glowColor", _currentColor); //Changing the color of the aura
        }

        if (_currentColor.Equals(_targetColor)) //When to stop the fade
            enabled = false;
	}
}
