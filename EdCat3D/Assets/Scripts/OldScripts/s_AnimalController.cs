using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_AnimalController : MonoBehaviour
{
    /*****************************************************************************************
* Controller for animals
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
    public s_AnimalController()
    {
        this.resType = "";
        this.uniqueID = "";
        this.isActive = true;
        this.resReactionRadius = 5f;
    }

}