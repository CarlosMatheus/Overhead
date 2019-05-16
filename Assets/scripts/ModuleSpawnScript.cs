using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleSpawnScript : MonoBehaviour {

	public GameObject[] modules;
	public int modulesToInstantiate = 4;

	private int randomSelection;
	private bool[] selectedModules;

	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start () {
		InitializeSelectedModules ();
		InstantiateModules ();
	}
		
	/// <summary>
	/// Initializes the selected modules.
	/// </summary>
	private void InitializeSelectedModules(){
		selectedModules = new bool[modules.Length];
		for (int i = 0; i < selectedModules.Length - 1; i++)
			selectedModules [i] = false;
	}

	/// <summary>
	/// Selects the random module.
	/// </summary>
	/// <returns>The random module.</returns>
	private GameObject SelectRandomModule(){
		do{
			randomSelection = (int) Mathf.Round (Random.value*modules.Length);
			randomSelection = Mathf.Clamp(randomSelection, 0, modules.Length-1);
		}while(selectedModules[randomSelection]);
		selectedModules [randomSelection] = true;
		return modules [randomSelection];
	}

	/// <summary>
	/// Instantiates the modules.
	/// </summary>
	private void InstantiateModules(){ // ADAPT HERE
		for (int i = 0; i < modulesToInstantiate; i++) {
			Instantiate ( SelectRandomModule (), new Vector3(0,0,0), Quaternion.Euler(0,i*90,0) );
		}
	}
}
