using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineWitch : MonoBehaviour {

    private Color GlowColor;

    private Renderer[] _renderers;
    private List<Material> _materials = new List<Material>();
    private Color _currentColor;

    void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>(); //Gets all the Children Renderers, to get access to their materials

        foreach (Renderer renderer in _renderers)
        {
            _materials.AddRange(renderer.materials); //Getting access to their materials
        }
    }

    void Update()
    {
        if (!GameObject.ReferenceEquals(GetComponent<PlayerController>().currentTower, GameObject.Find("MasterTower")))
            _currentColor = GetComponent<PlayerController>().currentTower.GetComponent<OutlineObject>()._currentColor;
        else
            _currentColor = GetComponent<PlayerController>().currentTower.GetComponentInChildren<OutlineObjectMainTower>()._currentColor;

        for (int i = 0; i < _materials.Count; i++)
        {
            _materials[i].SetColor("_glowColor", _currentColor); //Changing the color of the aura
        }
    }
}
