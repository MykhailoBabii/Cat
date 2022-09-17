using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_TutorialBattleDialogue : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Global._isTutorialBattleDialogue && !Global._isTutorial31)
            {
                Global._isEnterTutorialBattleDialogue = true;
                Global._isTutorialBattleDialogue = false;
                Global._isPopUpOpen = true;
                //Global.globalBus.SendMessage("c_GameController_Base_command_setPause");
                Global._isSpeak = true;
                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                Global._playDialogue(Global.audio_player.sectoid_dlg_022);
                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                Global._gui_SetInterface("TutorialDialogue25");
            }
        }
    }

}