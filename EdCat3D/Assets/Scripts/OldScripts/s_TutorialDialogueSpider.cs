using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_TutorialDialogueSpider : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!Global._isTutorialDialogueSpider && !Global._isTutorialDialogueSpiderEnd)
            {
                Global._isTutorialDialogueSpider = true;
                //Global.globalBus.SendMessage("c_GameController_Base_command_setPause");
                Global._isPopUpOpen = true;
                Global._isSpeak = true;
                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                Global._playDialogue(Global.audio_player.sectoid_dlg_027);
                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                Global._gui_SetInterface("TutorialDialogue28");
            }
        }
    }

}