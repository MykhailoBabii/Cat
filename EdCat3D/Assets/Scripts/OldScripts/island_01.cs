using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class island_01 : MonoBehaviour
{
    public virtual void Start()
    {
        Global.startPoint = GameObject.Find("_start_point");
        if (Global._isGreenHouse)
        {
            Global.GreenHouseExit = GameObject.Find("_entry_zone");
            if (!Global._isTutorialDestroyCobweb)
            {
                Global._isTutorialCobweb = GameObject.Find("cobweb_interactive");
            }
            if (!Global._isTutorialSpiderAttack)
            {
                Global._TutorialSpider = GameObject.Find("quest_spider");
                ((s_AIController) Global._TutorialSpider.GetComponent(typeof(s_AIController))).srv_reactionradius = 0;
            }
            //LOAD CHANGES
            if (Global._isTutorialDestroyCobweb)
            {
                Global._isTutorialCobweb = GameObject.Find("cobweb_interactive");
                GameObject.Destroy(Global._isTutorialCobweb);
            }
            if (Global._isTutorialWaitingForTheSun)
            {
                Global._TutorialSpider = GameObject.Find("quest_spider");
                GameObject.Destroy(Global._TutorialSpider);
            }
        }
        if (Global._isTemple)
        {
            Global.TempleExit = GameObject.Find("_entry_zone");
            Global._TempleLockedDoor = GameObject.Find("_closeddoor");
            if (Global._isGetKey)
            {
                Global.dungeonKey = GameObject.Find("stonekey_01");
                GameObject.Destroy(Global.dungeonKey);
                GameObject.Destroy(Global._TempleLockedDoor);
            }
            Global.MosaicEntry = GameObject.Find("_MosaicEntry");
        }
        if (Global._isWorldMap)
        {
            Global._isTutorialGreenHouse = GameObject.Find("b_s_bld_greenhouse");
            Global.GreenHouseEntry = GameObject.Find("_GreenHouseEntry");
            Global.UFOEntry = GameObject.Find("_UFOEntry");
            Global.VillaEntry = GameObject.Find("_VillaEntry");
            Global.TempleEntry = GameObject.Find("_TempleEntry");
            Global.templePoint = GameObject.Find("_temple_point");
            Global.FishingEntry = GameObject.Find("_FishingEntry");
            Global.buildingPyramid = GameObject.Find("b_s_bld_pyramid");
            if (Global._isOpenPyramid)
            {
                Global.buildingPyramid.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                Global.buildingPyramid.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                Global.buildingPyramid.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                Global.buildingPyramid.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        Global.globalBus.gameObject.SendMessage("c_GameController_SetupPlayerInventory");
    }

}