using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_TempleClosedDoorDialogue : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!Global._isGetKey)
            {
                if (Global._isClosedDoorDialogue)
                {
                    Global._isClosedDoorDialogue = false;
                    Global._isPopUpOpen = true;
                    Global._isKot = false;
                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                    Global._isClickDialogue = true;
                    if (((Animator) Global.speakerGO.GetComponent(typeof(Animator))).GetCurrentAnimatorStateInfo(0).IsName("npcSpeak"))
                    {
                    }
                    else
                    {
                         //speak
                        Global.globalBus.gameObject.SendMessage("ResetDialogue");
                    }
                    Global._playDialogue(Global.audio_player.sectoid_dlg_044);
                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                    Global._gui_SetInterface("TempleDialogue01");
                }
            }
            else
            {
                GameObject.Destroy(Global._TempleLockedDoor);
            }
        }
    }

}