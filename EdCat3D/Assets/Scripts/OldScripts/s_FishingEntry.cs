using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_FishingEntry : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        Global.FishingAvailable = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        Global.FishingAvailable = false;
    }

}