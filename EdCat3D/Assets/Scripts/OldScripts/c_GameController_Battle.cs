using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class c_GameController_Battle : MonoBehaviour
{
    /*****************************************************************************************
* Battle functions
* 
* @author N-Game Studios
* Research & Development	
* 
*****************************************************************************************/
    public virtual void c_GameController_Battle_command_setComboPoint()
    {
        if (Global.growDamageCurStrikeCounts < Global.growDamageMaxStrikeCounts)
        {
            Global.growDamageCurStrikeCounts = Global.growDamageCurStrikeCounts + Global.growDamage_HowManyADD;
        }
        else
        {
            Global.growDamageCurStrikeCounts = 0;
            Global.growDamageMaxStrikeCounts = Global.growDamageMaxStrikeCounts + Global.growDamageMaxStrikeCountsPlus;
            Global.growDamage = Global.growDamage + Global.growDamagePlus;
        }
    }

    public virtual void c_GameController_Battle_command_unsetComboPoint()
    {
        if (Global.growDamageCurStrikeCounts > 0)
        {
            Global.growDamageCurStrikeCounts = Global.growDamageCurStrikeCounts - Global.growDamage_HowManySUB;
        }
        else
        {
            if (Global.growDamageMaxStrikeCounts > 10)
            {
                Global.growDamageCurStrikeCounts = Global.growDamageMaxStrikeCounts - Global.growDamageMaxStrikeCountsPlus;
                Global.growDamageMaxStrikeCounts = Global.growDamageMaxStrikeCounts - Global.growDamageMaxStrikeCountsPlus;
                Global.growDamage = Global.growDamage - Global.growDamagePlus;
            }
        }
    }

    public virtual void c_GameController_Battle_command_resetComboPoint()
    {
        Global.growDamage = 0;
        Global.growDamage_HowManyADD = 3;
        Global.growDamage_HowManySUB = 1;
        Global.growDamagePlus = 10;
        Global.growDamageMaxStrikeCounts = 10;
        Global.growDamageMaxStrikeCountsPlus = 2;
        Global.growDamageCurStrikeCounts = 0;
    }

}