using UnityEngine;

public class PropertiesManager : MonoBehaviour {

	// Atributes from spell
	private float damage;
	private float cooldown;
	private float range;
	private float soulBonusChance = 5f;
	private bool soulBonusActivation = false;
    private float slowTimeChance = 5f;
    private bool slowTimeActivation = false;
    private float fatalHitChance = 5f;
    private bool fatalHitActivation = false;
    private float burnRate;
	private float slowFactor;
	private float rangeRadius;
	private float effectDuration;

	// Reference to objects
	private GameObject masterTower;
	private GameObject player;

	void Start () {
		GameObject gameMaster = GameObject.FindGameObjectWithTag ("GameMaster");
		player = gameMaster.GetComponent<InstancesManager> ().GetPlayerObj ();
		masterTower = gameMaster.GetComponent<InstancesManager> ().GetMasterTowerObj ();
        SetMasterEffect(null);
	}

    #region Get methods
    public float GetDamage () {
		return damage;
	}

	public float GetCooldown () {
		return cooldown;
	}

	public float GetRange () {
		return range;
	}

	public float GetSlowFactor () {
		return slowFactor;
	}

	public float GetBurnRate () {
		return burnRate;
	}

	public float GetRangeRadius () {
		return rangeRadius;
	}

	public float GetEffectDuration () {
		return effectDuration;
	}

    #endregion

    #region Set methods

    public void SetDamage (float multiplicationFactor) {
		damage *= multiplicationFactor;// * bulletPrefab.GetComponent<SkillsProperties> ().GetDamage ();
	}

	public void SetCooldown (float multiplicationFactor) {
		cooldown *= multiplicationFactor;// * bulletPrefab.GetComponent<SkillsProperties> ().GetCooldown ();
	}

	public void SetRange (float multiplicationFactor) {
		range *= multiplicationFactor;// * bulletPrefab.GetComponent<SkillsProperties> ().GetRange ();
	}

	public void SetBurnRate (float multiplicationFactor) {
		burnRate *= multiplicationFactor;
	}

	public void SetSlowFactor (float adictionFactor) {
		slowFactor += adictionFactor;
	}

	public void SetRangeRadius (float multiplicationFactor) {
		rangeRadius *= multiplicationFactor;
	}

	public void SetEffectDuration (float multiplicationFactor) {
		effectDuration *= multiplicationFactor;
	}

    #region Soul bonus methods
    public bool HasSoulBonusEffect () {
		return soulBonusActivation;
	}

	public void SetSoulBonusEffect (bool _act) {
		soulBonusActivation = _act;
    }

    public float GetSoulBonusChance()
    {
        return soulBonusChance / 100;
    }

    public void SetSoulBonusChance(float multiplicationFactor)
    {
        soulBonusChance *= multiplicationFactor;
    }
#endregion

    #region Slow time methods
    public bool HasSlowTimeEffect()
    {
        return slowTimeActivation;
    }

    public void SetSlowTimeEffect(bool _act)
    {
        slowTimeActivation = _act;
    }

    public float GetSlowTimeChance()
    {
        return slowTimeChance / 100;
    }

    public void SetSlowTimeChance (float multiplicationFactor)
    {
        soulBonusChance *= multiplicationFactor;
    }
    #endregion

    #region Fatal hit methods
    public bool HasFatalHitEffect()
    {
        return fatalHitActivation;
    }

    public void SetFatalHitEffect(bool _act)
    {
        fatalHitActivation = _act;
    }

    public float GetFatalHitChance()
    {
        return fatalHitChance / 100;
    }

    public void SetFatalHitChance(float multiplicationFactor)
    {
        fatalHitChance *= multiplicationFactor;
    }
    #endregion

    public void SetMasterEffect (GameObject _effect) {
		player.GetComponent<PlayerController> ().currentSkill.GetComponent<SkillsProperties> ().SetEffect (_effect);
		masterTower.GetComponent<TowerScript> ().bulletPrefab.GetComponent<SkillsProperties> ().SetEffect (_effect);
	}

	public void SetValues (SkillsProperties sp) {

		damage = sp.GetDamage ();
		cooldown = sp.GetCooldown ();
		range = sp.GetRange ();
        effectDuration = sp.GetEffectDuration();
		burnRate = sp.GetBurnRate ();
		slowFactor = sp.GetSlowFactor ();
        rangeRadius = sp.GetRangeRadius ();
	}
#endregion
}
