using UnityEngine;
using UnityEngine.UI;

public class Perk : MonoBehaviour
{

	private new string name;
	private Button perkButton;

    [SerializeField] private float minWaveToActivate = 0f;
    [SerializeField] private float maxLevel = 0f;
    [SerializeField] private float cost = 0f;
    [SerializeField] private float addScore = 0f;
    [SerializeField] private Sprite upgradeIcon = null;
    [TextArea(3, 5)]
    [SerializeField]
    private string description = null;

    //private List<Perk> childs;
    [SerializeField] private Perk[] childs;

    private PropertiesManager currentTowerProperties;
    private GameObject gameMaster;
	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;

    public bool isCallable = false;
	private int level = 0;

    public virtual void Start()
    {
        name = gameObject.name;

        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        soulsCounter = gameMaster.GetComponent<SoulsCounter>();
        scoreCounter = gameMaster.GetComponent<ScoreCounter>();

        currentTowerProperties = gameObject.GetComponentInParent<PropertiesManager>();

        perkButton = gameObject.GetComponentInChildren<Button>();
        if (perkButton == null)
        {
            Debug.LogWarning("There are perks without reference to their buttons");
        }

        perkButton.onClick.AddListener(LevelUp);

        Text[] aux = gameObject.GetComponentsInChildren<Text>();

        foreach (Text t in aux)
        {
            if (t.gameObject.name == "TowerName")
            {
                t.text = name;
            }

            if (t.gameObject.name == "Price")
            {
                t.text = cost.ToString() + " Souls";
            }

            if (t.gameObject.name == "UpgradeProperty")
            {
                t.text = description;
            }

            if (t.gameObject.name == "Level")
            {
                t.text = "Level: " + level.ToString() + "/" + maxLevel.ToString();
            }

            if (t.gameObject.name == "MinWave")
            {
                t.text = "Min wave: " + minWaveToActivate.ToString();
            }
        }

        Image[] aux_ = gameObject.GetComponentsInChildren<Image>();

        foreach (Image i in aux_)
        {
            if (i.gameObject.name == "ShopTowerItem")
            {
                i.sprite = upgradeIcon;
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


    public virtual void LevelUp()
    {
        if (!CheckIfItsAvailable())
            return;

        // Set childs callables
        foreach (Perk p in childs)
        {
            p.TurnCallable();
            p.GetButton().interactable = true;
        }

        // Upgrade perk
        level++;

        // Consume souls to level up
        soulsCounter.SetSouls(soulsCounter.GetSouls() - cost);

        // Add score
        scoreCounter.SetScore(scoreCounter.GetScore() + addScore);

        // Call a default function on skill gameObject to alter values
        Debug.Log(name + " level up to level " + level + " on tower " + GetCurrentTower().name);
    }

	public void LevelDown () 
    {
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

	public Button GetButton () 
    {
		return perkButton;
	}

    public PropertiesManager GetCurrentTowerProperties ()
    {
        return currentTowerProperties;
    }

    public GameObject GetCurrentTower()
    {
        return GetComponentInParent<TowerScript>().gameObject;
    }

    private bool CheckIfItsAvailable () 
    {
		if 
            (
            level < maxLevel && soulsCounter.GetSouls () >= cost && 
            gameMaster.GetComponent<WaveSpawner> ().GetWave () >= minWaveToActivate &&
            isCallable
            ) 
        {
			return true;
		}
		return false;
	}
}

// https://unity3d.com/pt/learn/tutorials/topics/scripting/overriding
