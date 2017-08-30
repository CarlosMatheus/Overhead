using UnityEngine;
using UnityEngine.UI;

public class Perk : MonoBehaviour {

	private new string name;
    private Text buttonName;
	private Button perkButton;

    [SerializeField] private float minWaveToActivate = 0f;
    [SerializeField] private float maxLevel = 0f;
    [SerializeField] private float cost = 0f;
    [SerializeField] private float addScore = 0f;

    //private List<Perk> childs;
    [SerializeField] private Perk[] childs;

    private PropertiesManager currentTowerProperties;
    private GameObject gameMaster;
	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;

    public bool isCallable = false;
	private int level = 0;

    void Start()
    {
        name = gameObject.name;

        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        soulsCounter = gameMaster.GetComponent<SoulsCounter>();
        scoreCounter = gameMaster.GetComponent<ScoreCounter>();

        currentTowerProperties = gameObject.GetComponentInParent<PropertiesManager>();
        if (currentTowerProperties == null)
        {
            Debug.LogError("There are perks without reference to their towers");
            return;
        }

        perkButton = gameObject.GetComponentInChildren<Button>();
        if (perkButton == null)
        {
            Debug.LogError("There are perks without reference to their buttons");
            return;
        }

        perkButton.onClick.AddListener(LevelUp);

        Text[] aux = gameObject.GetComponentsInChildren<Text>();

        foreach (Text t in aux)
        {
            if (t.gameObject.name == "UpgradeName")
            {
                buttonName = t;
                buttonName.text = name;
                break;
            }
        }

        /* // If you wanna set automatically, try this. In our case, we choose not because of Canvas Horizontal Layout Group
		Perk[] _childs = gameObject.GetComponentsInChildren<Perk> ();
		foreach (Perk p in _childs) {
			if (p.transform.parent != this.transform)
				_childs[Array.FindIndex (_childs, perk => perk == p)] = null;
		}

		childs = new List<Perk> ();

		foreach (Perk p in _childs) {
			if (p != null) {
				childs.Add (p);
			}
		}
		*/
    }

	void Update () {
		GetButton().interactable = CheckIfItsAvailable();
	}

	public virtual void LevelUp () {
		
		// Set childs callables
		foreach (Perk p in childs) {
			p.TurnCallable ();
			p.GetButton ().interactable = true;
		}

		// Upgrade perk
		level++;

		// Consume souls to level up
		soulsCounter.SetSouls (soulsCounter.GetSouls () - cost);

		// Add score
		scoreCounter.SetScore (scoreCounter.GetScore () + addScore);

		// Call a default function on skill gameObject to alter values
		Debug.Log (buttonName.text + " level up to level " + level + " on tower " + currentTowerProperties.name);
	}

	public void LevelDown () {
		if (level > 0) {
			// Set childs callables
			foreach (Perk p in childs) {
				p.TurnUncallable ();
			}

			level--;

			// Call a default function on skill gameObject to alter values
			Debug.Log("LevelDown!");

			if (level == 0) {
				TurnUncallable ();
			}
		}
	}

	public void TurnCallable () {
		isCallable = true;
	}

	public void TurnUncallable () {
		isCallable = false;
	}

	public Button GetButton () {
		return perkButton;
	}

    public PropertiesManager GetCurrentTower ()
    {
        return currentTowerProperties;
    }

	private bool CheckIfItsAvailable () {
		if (level < maxLevel && soulsCounter.GetSouls () >= cost && gameMaster.GetComponent<WaveSpawner> ().GetWave () >= minWaveToActivate && isCallable) {
			return true;
		}
		return false;
	}
}

// https://unity3d.com/pt/learn/tutorials/topics/scripting/overriding
