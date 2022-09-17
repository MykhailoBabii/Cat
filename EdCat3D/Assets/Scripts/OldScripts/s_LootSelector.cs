using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_LootSelector : MonoBehaviour
{
    public GameObject lootSelector;
    public virtual void OnTriggerEnter(Collider other)
    {
        ((MeshRenderer) this.lootSelector.GetComponent(typeof(MeshRenderer))).enabled = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        ((MeshRenderer) this.lootSelector.GetComponent(typeof(MeshRenderer))).enabled = false;
    }

}