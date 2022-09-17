using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_DropParams : MonoBehaviour
{
    public Hashtable drophash;
    public bool isTaken;
    public string lootType;
    public int lootAmount;
    public s_DropParams()
    {
        this.drophash = new Hashtable();
    }

}