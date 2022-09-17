using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_VillaEntry : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        Global.VillaOpenDoor = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        Global.VillaOpenDoor = false;
    }

}