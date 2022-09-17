using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_TempleEntry : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        Global.TempleOpenDoor = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        Global.TempleOpenDoor = false;
    }

}