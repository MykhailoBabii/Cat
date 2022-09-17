using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_VegetableController : MonoBehaviour
{
    /*****************************************************************************************
* Controller for vegetables
* 
* @author N-Game Studios Ltd. 
* Research & Development	
* 
*****************************************************************************************/
    public string resType;
    public int resAmount;
    public string uniqueID;
    public bool isActive;
    private float resReactionRadius;
    private bool isPlayS;
    public virtual void Start() //Global._npcsToonsScripts.Add(this.name, this.transform.Find("mesh").GetComponent(s_ResourceHoverToon));
    {
    }

    public virtual void Update()
    {
        if (!this.isActive)
        {
            this.getToDeath();
        }
    }

     /*if (!isPlayS)
	{
		if (Global.ResType == "0") {
			Global._playSound(this.gameObject, "sound_barrell_destroy");
		}
		else {
			Global._playSound(this.gameObject, "sound_stone_destroy");
		}
		isPlayS = true;
		var spellDeathGO: GameObject = GameObject.Instantiate(Global.gass.LoadAsset("spells_towers_destroy"), new Vector3(
			this.gameObject.transform.position.x,
			this.gameObject.transform.position.y,
			this.gameObject.transform.position.z
		), Quaternion.identity);
	}*/    private void getToDeath() //if (spellDeathGO != null) GameObject.Destroy(spellDeathGO);
    {
    }

    public s_VegetableController()
    {
        this.resType = "";
        this.uniqueID = "";
        this.isActive = true;
        this.resReactionRadius = 5f;
    }

}