using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[System.Serializable]
public partial class c_GameController : MonoBehaviour
{
    /*****************************************************************************************
* Main game functions
* 
* @author N-Game Studios
* Research & Development	
* 
*****************************************************************************************/
    //#pragma strict
    public UnityEngine.AI.NavMeshAgent _agent;
    private c_GuiCommonStyleController gs_common;
    public GameObject rrHealFX;

    bool isDrop = false;
    private int removeArmor = 0;
    private int addArmor = 0;
    private int removeHealth = 0;
    private int addHealth = 0;
    private int minusHealth = 0;
    private string resultExchange = "-1";

    public virtual void Start()
    {
        this.gs_common = (c_GuiCommonStyleController) GameObject.Find("_gui_styles_common").GetComponent(typeof(c_GuiCommonStyleController));
    }

    public virtual IEnumerator c_GameController_LoadZone()
    {
        Global._system_isHeroDead = true;
        Global._progressComplicatedLoader = 0;
        Global._isPopUpOpen = true;
        Global.random_loader = Random.Range(1, 6);
        Global._gui_SetInterface("ComlicatedLoader");
        if (!Global._isTutorial01)
        {
            Global._gui_SetInterface("TutorialDialogue01");
            Global._playDialogue(Global.audio_player.sectoid_dlg_001);
            Global.globalBus.gameObject.SendMessage("DialogueEnd");
        }
        else
        {
            if (Global._isWorldMap)
            {
                if (Global.GreenHouseOpenDoor)
                {
                    Global._isWorldMap = false;
                    Global._isGreenHouse = true;
                    Application.LoadLevel("greenhouse");
                    Global.GreenHouseOpenDoor = false;
                }
                else
                {
                    if (Global.TempleOpenDoor)
                    {
                        Global._isWorldMap = false;
                        Global._isTemple = true;
                        Application.LoadLevel("temple");
                        Global.TempleOpenDoor = false;
                    }
                }
            }
            else
            {
                if (Global._isGreenHouse)
                {
                    Global._isWorldMap = true;
                    Global._isGreenHouse = false;
                    Application.LoadLevel("worldmap");
                }
                else
                {
                    if (Global._isTemple)
                    {
                        Global._isWorldMap = true;
                        Global._isTemple = false;
                        Application.LoadLevel("worldmap");
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 10;
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 20;
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 30;
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 40;
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 50;
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 60;
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 70;
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 80;
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 90;
            yield return new WaitForSeconds(0.1f);
            Global._progressComplicatedLoader = 100;
            Global.globalBus.gameObject.SendMessage("c_GameController_PlaceHeroToStart");
            Global.UFOShowEntry = true;
            Global._gui_SetInterface("WorldMap");
        }
        if (Global.buildingPyramid != null)
        {
            if (Global._isOpenPyramid)
            {
                Global.buildingPyramid.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                Global.buildingPyramid.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                Global.buildingPyramid.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                Global.buildingPyramid.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public virtual void c_GameController_PlaceHeroToStart()
    {
        Global._system_isHeroDead = false;
        Global._isPopUpOpen = false;
        if ((Global._isGreenHouse || Global._isGreenHouseExit) || Global._isTemple)
        {
            this._agent.Warp(Global.startPoint.transform.position);
            Global._heroTargetPoint = Global.startPoint.transform.position;
            Global._isGreenHouseExit = false;
        }
        else
        {
            if (Global._isTempleExit)
            {
                this._agent.Warp(Global.templePoint.transform.position);
                Global._heroTargetPoint = Global.templePoint.transform.position;
                Global._isTempleExit = false;
            }
        }
    }

    public virtual IEnumerator c_GameController_Base_command_Cooldown01(float cooldown)
    {
        Global.Ability01Cooldown = cooldown;
        float i = 0;
        while (i < cooldown)
        {
            Global.Ability01Cooldown = Global.Ability01Cooldown - 1;
            yield return new WaitForSeconds(1);
            i++;
        }
    }

    public virtual IEnumerator c_GameController_Base_command_Cooldown02(float cooldown)
    {
        Global.Ability02Cooldown = cooldown;
        float i = 0;
        while (i < cooldown)
        {
            Global.Ability02Cooldown = Global.Ability02Cooldown - 1;
            yield return new WaitForSeconds(1);
            i++;
        }
    }

    public virtual IEnumerator c_GameController_Base_command_Cooldown03(float cooldown)
    {
        Global.Ability03Cooldown = cooldown;
        float i = 0;
        while (i < cooldown)
        {
            Global.Ability03Cooldown = Global.Ability03Cooldown - 1;
            yield return new WaitForSeconds(1);
            i++;
        }
    }

    public virtual IEnumerator c_GameController_Base_command_Cooldown04(float cooldown)
    {
        Global.Ability04Cooldown = cooldown;
        float i = 0;
        while (i < cooldown)
        {
            Global.Ability04Cooldown = Global.Ability04Cooldown - 1;
            yield return new WaitForSeconds(1);
            i++;
        }
    }

    public virtual IEnumerator c_GameController_Base_command_Cooldown05(float cooldown)
    {
        Global.Ability05Cooldown = cooldown;
        float i = 0;
        while (i < cooldown)
        {
            Global.Ability05Cooldown = Global.Ability05Cooldown - 1;
            yield return new WaitForSeconds(1);
            i++;
        }
    }

    public virtual IEnumerator c_GameController_Base_command_Cooldown06(float cooldown)
    {
        Global.Ability06Cooldown = cooldown;
        float i = 0;
        while (i < cooldown)
        {
            Global.Ability06Cooldown = Global.Ability06Cooldown - 1;
            yield return new WaitForSeconds(1);
            i++;
        }
    }

    public virtual void c_GameController_Base_command_Ability01()
    {
    }

    public virtual IEnumerator c_GameController_Base_command_Ability02()
    {
        Global.spellFlyGO = GameObject.Instantiate(this.rrHealFX, new Vector3(Global._hero_dolly.transform.position.x, Global._hero_dolly.transform.position.y, Global._hero_dolly.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(2);
        if (Global.spellFlyGO != null)
        {
            Global.TurnOffEmittersHierarchy(Global.spellFlyGO.transform);
        }
        if (Global.spellFlyGO != null)
        {
            Global.WaiterDestroyer(Global.spellFlyGO, 0.25f);
        }
    }

    public virtual IEnumerator c_GameController_Base_command_Ability04()
    {
        Global._TempDamageMin = (int) Global._system_CharacterParams["damagemin"];
        Global._TempDamageMax = (int) Global._system_CharacterParams["damagemax"];
        Global._system_CharacterParams["damagemin"] = float.Parse((string) Global._system_CharacterParams["damagemin"]) + 10;
        Global._system_CharacterParams["damagemax"] = float.Parse((string) Global._system_CharacterParams["damagemax"]) + 10;
        yield return new WaitForSeconds(5);
        Global._system_CharacterParams["damagemin"] = Global._TempDamageMin;
        Global._system_CharacterParams["damagemax"] = Global._TempDamageMax;
    }

    public virtual IEnumerator c_GameController_Base_command_Ability05()
    {
        int TempDefence = 0;
        Global._TempDefence = (int) Global._system_CharacterParams["defence"];
        TempDefence = Global._TempDefence + 100;
        Global._system_CharacterParams["defence"] = TempDefence;
        yield return new WaitForSeconds(5);
        Global._system_CharacterParams["defence"] = Global._TempDefence;
        if (Global._isR3Backpack)
        {
            Global.rr_backpack_shield.SetActive(false);
        }
        Global.rr_body_shield.SetActive(false);
        if (Global._isR3Gun)
        {
            Global.rr_gun_shield.SetActive(false);
        }
        Global.rr_hand_shield.SetActive(false);
        Global.rr_leg_shield.SetActive(false);
    }

    public virtual IEnumerator c_GameController_Base_command_AbilityImmortal()
    {
        Global._isPlayerSpell = true;
        Global._system_isHeroDead = true;
        yield return new WaitForSeconds(5);
        if (Global._isR3Backpack)
        {
            Global.r3_backpack.SetActive(true);
        }
        if (Global._isR3Body)
        {
            Global.r3_body.SetActive(true);
        }
        if (Global._isR3Gun)
        {
            Global.r3_gun.SetActive(true);
        }
        if (Global._isR3Hand)
        {
            Global.r3_hand.SetActive(true);
        }
        if (Global._isR3Leg)
        {
            Global.r3_leg.SetActive(true);
        }
        if (Global._isR4Backpack)
        {
            Global.r4_backpack.SetActive(true);
        }
        if (Global._isR4Body)
        {
            Global.r4_body.SetActive(true);
        }
        if (Global._isR4Gun)
        {
            Global.r4_gun.SetActive(true);
        }
        if (Global._isR4Hand)
        {
            Global.r4_hand.SetActive(true);
        }
        if (Global._isR4Leg)
        {
            Global.r4_leg.SetActive(true);
        }
        if (Global._isR5Backpack)
        {
            Global.r5_backpack.SetActive(true);
        }
        if (Global._isR3Backpack)
        {
            Global.rr_backpack_invisible.SetActive(false);
        }
        Global.rr_body_invisible.SetActive(false);
        if (Global._isR3Gun)
        {
            Global.rr_gun_invisible.SetActive(false);
        }
        Global.rr_hand_invisible.SetActive(false);
        Global.rr_leg_invisible.SetActive(false);
        Global._isPlayerSpell = false;
        Global._system_isHeroDead = false;
    }

    public virtual void c_GameController_Base_command_CreateItem()
    {
        if (Global._currentRecipeType == "backpack")
        {
            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", Global._currentRecipeType },  {"thingname", Global._currentRecipeThingName },  {"icon", Global._currentRecipeIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", Global._currentRecipeUniqueID },  {"vid", "1" },  {"addslots", Global._currentRecipeAddSlots },  {"name", Global._currentRecipeName },  {"selected", "false" },  {"costtype1", Global._currentRecipeCostType1 },  {"costamount1", Global._currentRecipeCostAmount1 },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", Global._currentRecipeCostType2 },  {"costamount2", Global._currentRecipeCostAmount2 },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", Global._currentRecipeCostType3 },  {"costamount3", Global._currentRecipeCostAmount3 },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", Global._currentRecipeCostType4 },  {"costamount4", Global._currentRecipeCostAmount4 },  {"costicon4", Global._RecipeCostIcon },  {"level", "1" }, }));
        }
        else
        {
            if (Global._currentRecipeType == "feature")
            {
                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", Global._currentRecipeType },  {"thingname", Global._currentRecipeThingName },  {"icon", Global._currentRecipeIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", Global._currentRecipeUniqueID },  {"vid", "1" },  {"addability", Global._currentRecipeAddAbility },  {"name", Global._currentRecipeName },  {"selected", "false" },  {"costtype1", Global._currentRecipeCostType1 },  {"costamount1", Global._currentRecipeCostAmount1 },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", Global._currentRecipeCostType2 },  {"costamount2", Global._currentRecipeCostAmount2 },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", Global._currentRecipeCostType3 },  {"costamount3", Global._currentRecipeCostAmount3 },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", Global._currentRecipeCostType4 },  {"costamount4", Global._currentRecipeCostAmount4 },  {"costicon4", Global._RecipeCostIcon },  {"level", "1" }, }));
            }
            else
            {
                if (Global._currentRecipeType == "weapon")
                {
                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", Global._currentRecipeType },  {"thingname", Global._currentRecipeThingName },  {"icon", Global._currentRecipeIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", Global._currentRecipeUniqueID },  {"vid", "1" },  {"addability", Global._currentRecipeAddDamage },  {"name", Global._currentRecipeName },  {"selected", "false" },  {"costtype1", Global._currentRecipeCostType1 },  {"costamount1", Global._currentRecipeCostAmount1 },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", Global._currentRecipeCostType2 },  {"costamount2", Global._currentRecipeCostAmount2 },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", Global._currentRecipeCostType3 },  {"costamount3", Global._currentRecipeCostAmount3 },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", Global._currentRecipeCostType4 },  {"costamount4", Global._currentRecipeCostAmount4 },  {"costicon4", Global._RecipeCostIcon },  {"level", "1" }, }));
                }
            }
        }
        Global.globalBus.SendMessage("c_GameController_Base_command_SortThings");
    }

    public virtual void c_GameController_Base_command_CreateStorageItem()
    {
        if (Global._currentRecipeType == "backpack")
        {
            Global.StorageArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", Global._currentRecipeType },  {"thingname", Global._currentRecipeThingName },  {"icon", Global._currentRecipeIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", Global._currentRecipeUniqueID },  {"vid", "1" },  {"addslots", Global._currentRecipeAddSlots },  {"name", Global._currentRecipeName },  {"selected", "false" },  {"costtype1", Global._currentRecipeCostType1 },  {"costamount1", Global._currentRecipeCostAmount1 },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", Global._currentRecipeCostType2 },  {"costamount2", Global._currentRecipeCostAmount2 },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", Global._currentRecipeCostType3 },  {"costamount3", Global._currentRecipeCostAmount3 },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", Global._currentRecipeCostType4 },  {"costamount4", Global._currentRecipeCostAmount4 },  {"costicon4", Global._RecipeCostIcon },  {"level", "1" }, }));
        }
        else
        {
            if (Global._currentRecipeType == "feature")
            {
                Global.StorageArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", Global._currentRecipeType },  {"thingname", Global._currentRecipeThingName },  {"icon", Global._currentRecipeIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", Global._currentRecipeUniqueID },  {"vid", "1" },  {"addability", Global._currentRecipeAddAbility },  {"name", Global._currentRecipeName },  {"selected", "false" },  {"costtype1", Global._currentRecipeCostType1 },  {"costamount1", Global._currentRecipeCostAmount1 },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", Global._currentRecipeCostType2 },  {"costamount2", Global._currentRecipeCostAmount2 },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", Global._currentRecipeCostType3 },  {"costamount3", Global._currentRecipeCostAmount3 },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", Global._currentRecipeCostType4 },  {"costamount4", Global._currentRecipeCostAmount4 },  {"costicon4", Global._RecipeCostIcon },  {"level", "1" }, }));
            }
            else
            {
                if (Global._currentRecipeType == "weapon")
                {
                    Global.StorageArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", Global._currentRecipeType },  {"thingname", Global._currentRecipeThingName },  {"icon", Global._currentRecipeIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", Global._currentRecipeUniqueID },  {"vid", "1" },  {"addability", Global._currentRecipeAddDamage },  {"name", Global._currentRecipeName },  {"selected", "false" },  {"costtype1", Global._currentRecipeCostType1 },  {"costamount1", Global._currentRecipeCostAmount1 },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", Global._currentRecipeCostType2 },  {"costamount2", Global._currentRecipeCostAmount2 },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", Global._currentRecipeCostType3 },  {"costamount3", Global._currentRecipeCostAmount3 },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", Global._currentRecipeCostType4 },  {"costamount4", Global._currentRecipeCostAmount4 },  {"costicon4", Global._RecipeCostIcon },  {"level", "1" }, }));
                }
            }
        }
        Global.globalBus.SendMessage("c_GameController_Base_command_SortStorage");
    }

    public virtual void c_GameController_Base_command_CreateFood()
    {
        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "food" },  {"thingname", Global._currentRecipeThingName },  {"icon", Global.currentFoodRecipeIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", Global._currentRecipeUniqueID },  {"vid", "1" },  {"val", "1" },  {"addhealth", Global.currentFoodRecipeAddHealth },  {"name", Global.currentFoodRecipeName },  {"selected", "false" }, }));
        Global.globalBus.SendMessage("c_GameController_Base_command_SortThings");
    }

    public virtual void c_GameController_Base_command_CheckFood()
    {
        if (Global.globalInventoryThingsArr.Count > 0)
        {
            int i = 0;
            while (i < Global.globalInventoryThingsArr.Count)
            {
                if (Global.globalInventoryThingsArr[i]["thingname"] == Global._currentRecipeThingName)
                {
                    Global.globalInventoryThingsArr[i]["val"] = (int.Parse((string) Global.globalInventoryThingsArr[i]["val"]) + 1).ToString();
                    break;
                }
                else
                {
                    if (i == (Global.globalInventoryThingsArr.Count - 1))
                    {
                        Global.globalBus.SendMessage("c_GameController_Base_command_CreateFood");
                        break;
                    }
                }
                i++;
            }
        }
        else
        {
            Global.globalBus.SendMessage("c_GameController_Base_command_CreateFood");
        }
        Global.globalBus.gameObject.SendMessage("c_GameController_CheckConsumables");
    }

    public virtual void c_GameController_Base_command_TutorialSearchBlaster()
    {
        if (Global.StorageArr.Count > 0)
        {
            int i = 0;
            while (i < Global.StorageArr.Count)
            {
                if (Global.StorageArr[i]["type"] == "weapon")
                {
                    Global.TutorialBlasterXCoord = (string) Global.StorageArr[i]["x"];
                    Global.TutorialBlasterYCoord = (string) Global.StorageArr[i]["y"];
                    Global.TutorialBlasterThingInv = Global.StorageArr[i];
                    break;
                }
                i++;
            }
        }
    }

    public virtual void c_GameController_Base_command_TutorialSearchInventoryBlaster()
    {
        if (Global.globalInventoryThingsArr.Count > 0)
        {
            int i = 0;
            while (i < Global.globalInventoryThingsArr.Count)
            {
                if (Global.globalInventoryThingsArr[i]["type"] as string== "weapon")
                {
                    Global.TutorialBlasterXCoord = (string) Global.globalInventoryThingsArr[i]["x"];
                    Global.TutorialBlasterYCoord = (string) Global.globalInventoryThingsArr[i]["y"];
                    Global.TutorialBlasterThingInv = Global.globalInventoryThingsArr[i];
                    break;
                }
                i++;
            }
        }
    }

    // ==========================================================================================
    // === drop from dead mob
    public virtual IEnumerator c_GameController_Base_DropMob(object[] inArr)
    {
        int lootAmount = 0;
        if (Global._dropPlacePoses.Count > 0)
        {
            //inArr.Shift();
            Vector3 killedMobPlace = (Vector3) Global._dropPlacePoses.Pop();
            string lootType = null;
            if (inArr.Length > 0)
            {
                yield return new WaitForSeconds(0.6f);
                Hashtable someDropOne = new Hashtable(new Hashtable() { {"pos", killedMobPlace },  {"drops", new object[0] }, });
                GameObject dropPlaceGO = GameObject.Instantiate(Resources.Load<GameObject>("loot/base_dropdummy"), new Vector3(killedMobPlace.x, killedMobPlace.y + 0.2f, killedMobPlace.z), Quaternion.identity);
                dropPlaceGO.name = (("drop_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1111, 9999).ToString();
                ((s_DropParams) dropPlaceGO.transform.Find("mesh").GetComponent(typeof(s_DropParams))).drophash = someDropOne;
                ((s_DropParams) dropPlaceGO.transform.Find("mesh").GetComponent(typeof(s_DropParams))).lootType = (string) inArr[0];
                ((s_DropParams) dropPlaceGO.transform.Find("mesh").GetComponent(typeof(s_DropParams))).lootAmount = (int) inArr[1];
                Global._dropToonsScripts.Add(dropPlaceGO.name, (s_DropHoverToon) dropPlaceGO.transform.Find("mesh").GetComponent(typeof(s_DropHoverToon)));
                Global.globalBus.SendMessage("c_DestroyController_command_ADD_to_GROUP", new object[] {"drops", dropPlaceGO});
            }
        }
    }

    public virtual void c_GameController_Base_command_GenerateDrop(int index)
    {
        int i = 0;
        while (i < Global._recipeDropLoot.Count)
        {
            Global._currentLootLootName = (string) Global._recipeDropLoot[index]["loot_name"];
            Global._currentLootDroptype = (string) Global._recipeDropLoot[index]["droptype"];
            Global._currentLootType = (string) Global._recipeDropLoot[index]["type"];
            Global._currentLootIcon = (string) Global._recipeDropLoot[index]["icon"];
            Global._currentLootUniqueID = ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString();
            Global._currentLootType1 = (string) Global._recipeDropLoot[index]["loottype1"];
            Global._currentLootAmount1 = (string) Global._recipeDropLoot[index]["lootamount1"];
            Global._currentLootType2 = (string) Global._recipeDropLoot[index]["loottype2"];
            Global._currentLootAmount2 = (string) Global._recipeDropLoot[index]["lootamount2"];
            Global._currentLootType3 = (string) Global._recipeDropLoot[index]["loottype3"];
            Global._currentLootAmount3 = (string) Global._recipeDropLoot[index]["lootamount3"];
            Global._currentLootName = (string) Global._recipeDropLoot[index]["name"];
            Global._currentLootDescription = (string) Global._recipeDropLoot[index]["description"];
            i++;
        }
        Global._Loot.Add(new Hashtable(new Hashtable() { {"loot_name", Global._currentLootLootName },  {"droptype", Global._currentLootDroptype },  {"type", Global._currentLootType },  {"icon", Global._currentLootIcon },  {"uniqueID", Global._currentRecipeUniqueID },  {"loottype1", Global._currentLootType1 },  {"lootamount1", Global._currentLootAmount1 },  {"loottype2", Global._currentLootType2 },  {"lootamount2", Global._currentLootAmount2 },  {"loottype3", Global._currentLootType3 },  {"lootamount3", Global._currentLootAmount3 },  {"name", Global._currentLootName },  {"description", Global._currentLootDescription }, }));
        Global._isShowLoot = true;
        Global.globalBus.SendMessage("c_GameController_Base_command_ShowLootName", 5);
    }

    public virtual IEnumerator c_GameController_Base_command_ShowLootName(float cooldown)
    {
        Global.showTime = cooldown;
        float i = 0;
        while (i < cooldown)
        {
            Global.showTime = Global.showTime - 1;
            yield return new WaitForSeconds(1);
            i++;
        }
    }

    public virtual void c_GameController_Base_command_setPause()
    {
        if (!Global._isGamePaused)
        {
            Global._isGamePaused = true;
            Global._GAME_CURSOR = "default";
            Time.timeScale = 1E-05f;
            Global.GreenHouseOpenDoor = false;
            Global.GreenHouseExitDoor = false;
        }
    }

    public virtual void c_GameController_Base_command_unsetPause()
    {
        if (Global._isGamePaused)
        {
            Global._isGamePaused = false;
            Time.timeScale = 1;
            Global.GreenHouseOpenDoor = true;
            Global.GreenHouseExitDoor = true;
        }
    }

    public virtual void c_GameController_SetupPlayer()
    {
        Global.r3_backpack = GameObject.Find("/hero_player/character/r3_backpack");
        Global.r3_body = GameObject.Find("/hero_player/character/r3_body");
        Global.r3_gun = GameObject.Find("/hero_player/character/r3_gun");
        Global.r3_hand = GameObject.Find("/hero_player/character/r3_hand");
        Global.r3_leg = GameObject.Find("/hero_player/character/r3_leg");
        Global.r4_backpack = GameObject.Find("/hero_player/character/r4_backpack");
        Global.r4_body = GameObject.Find("/hero_player/character/r4_body");
        Global.r4_gun = GameObject.Find("/hero_player/character/r3_gun_2");
        Global.r4_hand = GameObject.Find("/hero_player/character/r4_hand");
        Global.r4_leg = GameObject.Find("/hero_player/character/r4_leg");
        Global.r5_backpack = GameObject.Find("/hero_player/character/r3_backpack_2");
        Global.r3_gun.SetActive(false);
        Global.r3_backpack.SetActive(false);
        Global.r4_backpack.SetActive(false);
        Global.r4_body.SetActive(false);
        Global.r4_gun.SetActive(false);
        Global.r4_hand.SetActive(false);
        Global.r4_leg.SetActive(false);
        Global.r5_backpack.SetActive(false);
        // inventory
        Global.inv_r3_backpack = GameObject.Find("/robotInv/r3_backpack");
        Global.inv_r3_body = GameObject.Find("/robotInv/r3_body");
        Global.inv_r3_gun = GameObject.Find("/robotInv/r3_gun");
        Global.inv_r3_hand = GameObject.Find("/robotInv/r3_hand");
        Global.inv_r3_leg = GameObject.Find("/robotInv/r3_leg");
        Global.inv_r4_backpack = GameObject.Find("/robotInv/r4_backpack");
        Global.inv_r4_body = GameObject.Find("/robotInv/r4_body");
        Global.inv_r4_gun = GameObject.Find("/robotInv/r3_gun_2");
        Global.inv_r4_hand = GameObject.Find("/robotInv/r4_hand");
        Global.inv_r4_leg = GameObject.Find("/robotInv/r4_leg");
        Global.inv_r5_backpack = GameObject.Find("/robotInv/r3_backpack_2");
        if (Global.inv_r3_gun != null)
        {
            Global.inv_r3_gun.SetActive(false);
        }
        if (Global.inv_r3_backpack != null)
        {
            Global.inv_r3_backpack.SetActive(false);
        }
        if (Global.inv_r4_backpack != null)
        {
            Global.inv_r4_backpack.SetActive(false);
        }
        if (Global.inv_r4_body != null)
        {
            Global.inv_r4_body.SetActive(false);
        }
        if (Global.inv_r4_gun != null)
        {
            Global.inv_r4_gun.SetActive(false);
        }
        if (Global.inv_r4_hand != null)
        {
            Global.inv_r4_hand.SetActive(false);
        }
        if (Global.inv_r4_leg != null)
        {
            Global.inv_r4_leg.SetActive(false);
        }
        if (Global.inv_r5_backpack != null)
        {
            Global.inv_r5_backpack.SetActive(false);
        }
        // shield
        Global.rr_backpack_shield = GameObject.Find("/hero_player/character/rr_backpack_shield");
        Global.rr_body_shield = GameObject.Find("/hero_player/character/rr_body_shield");
        Global.rr_gun_shield = GameObject.Find("/hero_player/character/rr_gun_shield");
        Global.rr_hand_shield = GameObject.Find("/hero_player/character/rr_hand_shield");
        Global.rr_leg_shield = GameObject.Find("/hero_player/character/rr_leg_shield");
        Global.rr_backpack_shield.SetActive(false);
        Global.rr_body_shield.SetActive(false);
        Global.rr_gun_shield.SetActive(false);
        Global.rr_hand_shield.SetActive(false);
        Global.rr_leg_shield.SetActive(false);
        // invisible
        Global.rr_backpack_invisible = GameObject.Find("/hero_player/character/rr_backpack_invisible");
        Global.rr_body_invisible = GameObject.Find("/hero_player/character/rr_body_invisible");
        Global.rr_gun_invisible = GameObject.Find("/hero_player/character/rr_gun_invisible");
        Global.rr_hand_invisible = GameObject.Find("/hero_player/character/rr_hand_invisible");
        Global.rr_leg_invisible = GameObject.Find("/hero_player/character/rr_leg_invisible");
        Global.rr_backpack_invisible.SetActive(false);
        Global.rr_body_invisible.SetActive(false);
        Global.rr_gun_invisible.SetActive(false);
        Global.rr_hand_invisible.SetActive(false);
        Global.rr_leg_invisible.SetActive(false);
    }

    public virtual void c_GameController_SetupPlayerInventory()
    {
        Global.inv_r3_backpack = GameObject.Find("/robotInv/r3_backpack");
        Global.inv_r3_body = GameObject.Find("/robotInv/r3_body");
        Global.inv_r3_gun = GameObject.Find("/robotInv/r3_gun");
        Global.inv_r3_hand = GameObject.Find("/robotInv/r3_hand");
        Global.inv_r3_leg = GameObject.Find("/robotInv/r3_leg");
        Global.inv_r4_backpack = GameObject.Find("/robotInv/r4_backpack");
        Global.inv_r4_body = GameObject.Find("/robotInv/r4_body");
        Global.inv_r4_gun = GameObject.Find("/robotInv/r3_gun_2");
        Global.inv_r4_hand = GameObject.Find("/robotInv/r4_hand");
        Global.inv_r4_leg = GameObject.Find("/robotInv/r4_leg");
        Global.inv_r5_backpack = GameObject.Find("/robotInv/r3_backpack_2");
        if (!Global._isR3Gun)
        {
            Global.inv_r3_gun.SetActive(false);
        }
        if (!Global._isR3Backpack)
        {
            Global.inv_r3_backpack.SetActive(false);
        }
        if (!Global._isR3Body)
        {
            Global.inv_r3_body.SetActive(false);
        }
        if (!Global._isR3Hand)
        {
            Global.inv_r3_hand.SetActive(false);
        }
        if (!Global._isR3Leg)
        {
            Global.inv_r3_leg.SetActive(false);
        }
        if (!Global._isR4Gun)
        {
            Global.inv_r4_gun.SetActive(false);
        }
        if (!Global._isR4Backpack)
        {
            Global.inv_r4_backpack.SetActive(false);
        }
        if (!Global._isR4Body)
        {
            Global.inv_r4_body.SetActive(false);
        }
        if (!Global._isR4Hand)
        {
            Global.inv_r4_hand.SetActive(false);
        }
        if (!Global._isR4Leg)
        {
            Global.inv_r4_leg.SetActive(false);
        }
        if (!Global._isR5Backpack)
        {
            Global.inv_r5_backpack.SetActive(false);
        }
    }

    public virtual void c_GameController_LoadPlayer()
    {
        if (Global._isR3Gun)
        {
            Global.r3_gun.SetActive(true);
            Global.inv_r3_gun.SetActive(true);
        }
        else
        {
            if (Global.r3_gun != null)
            {
                Global.r3_gun.SetActive(false);
            }
            if (Global.inv_r3_gun != null)
            {
                Global.inv_r3_gun.SetActive(false);
            }
        }
        if (Global._isR3Backpack)
        {
            Global.r3_backpack.SetActive(true);
            Global.inv_r3_backpack.SetActive(true);
        }
        else
        {
            if (Global.r3_backpack != null)
            {
                Global.r3_backpack.SetActive(false);
            }
            if (Global.inv_r3_backpack != null)
            {
                Global.inv_r3_backpack.SetActive(false);
            }
        }
        if (Global._isR4Backpack)
        {
            Global.r4_backpack.SetActive(true);
            Global.inv_r4_backpack.SetActive(true);
        }
        else
        {
            if (Global.r4_backpack != null)
            {
                Global.r4_backpack.SetActive(false);
            }
            if (Global.inv_r4_backpack != null)
            {
                Global.inv_r4_backpack.SetActive(false);
            }
        }
        if (Global._isR5Backpack)
        {
            Global.r5_backpack.SetActive(true);
            Global.inv_r5_backpack.SetActive(true);
        }
        else
        {
            if (Global.r5_backpack != null)
            {
                Global.r5_backpack.SetActive(false);
            }
            if (Global.inv_r5_backpack != null)
            {
                Global.inv_r5_backpack.SetActive(false);
            }
        }
        if (Global._isR4Body)
        {
            Global.r4_body.SetActive(true);
            Global.inv_r4_body.SetActive(true);
            if (Global.r3_body != null)
            {
                Global.r3_body.SetActive(false);
            }
            if (Global.inv_r3_body != null)
            {
                Global.inv_r3_body.SetActive(false);
            }
        }
        else
        {
            if (Global.r4_body != null)
            {
                Global.r4_body.SetActive(false);
            }
            if (Global.inv_r4_body != null)
            {
                Global.inv_r4_body.SetActive(false);
            }
        }
        if (Global._isR4Gun)
        {
            Global.r4_gun.SetActive(true);
            Global.inv_r4_gun.SetActive(true);
        }
        else
        {
            if (Global.r4_gun != null)
            {
                Global.r4_gun.SetActive(false);
            }
            if (Global.inv_r4_gun != null)
            {
                Global.inv_r4_gun.SetActive(false);
            }
        }
        if (Global._isR4Hand)
        {
            Global.r4_hand.SetActive(true);
            Global.inv_r4_hand.SetActive(true);
            if (Global.r3_hand != null)
            {
                Global.r3_hand.SetActive(false);
            }
            if (Global.inv_r3_hand != null)
            {
                Global.inv_r3_hand.SetActive(false);
            }
        }
        else
        {
            if (Global.r4_hand != null)
            {
                Global.r4_hand.SetActive(false);
            }
            if (Global.inv_r4_hand != null)
            {
                Global.inv_r4_hand.SetActive(false);
            }
        }
        if (Global._isR4Leg)
        {
            Global.r4_leg.SetActive(true);
            Global.inv_r4_leg.SetActive(true);
            if (Global.r3_leg != null)
            {
                Global.r3_leg.SetActive(false);
            }
            if (Global.inv_r3_leg != null)
            {
                Global.inv_r3_leg.SetActive(false);
            }
        }
        else
        {
            if (Global.r4_leg != null)
            {
                Global.r4_leg.SetActive(false);
            }
            if (Global.inv_r4_leg != null)
            {
                Global.inv_r4_leg.SetActive(false);
            }
        }
    }

    public virtual void c_GameController_Base_command_dropSomeObject()
    {
        int itemArmor = 0;
        int itemHealth = 0;
        int minusHealth = 0;
        Hashtable placeHash = null;
        if (Global.someDropableObject["fromdrag"] as string == "inventory")
        {
            if (Global.someDropablePlace["droptype"] as string == "inventory")
            {
                placeHash = (Hashtable) Global._someInventoryHashPlace_RectToCoords[Global.someDropablePlace["droprect"]];
                Global._placeHash_backMoveBetweenCells = new Hashtable() { {"x", (Global.someDropableObject["object"] as Hashtable)["x"] },  {"y", (Global.someDropableObject["object"] as Hashtable)["y"] }, };
                if (!(placeHash["x"] == Global._placeHash_backMoveBetweenCells["x"]) || !(placeHash["y"] == Global._placeHash_backMoveBetweenCells["y"]))
                {
                    
                    if (Global._isFaceCharacterInventoryThings)
                    {
                        Global._is_needUpdate_invthings = true;
                        this.setSomeInInventoryToOldPlace(Global.globalInventoryThingsArr, placeHash, Global._placeHash_backMoveBetweenCells);
                    }
                    else
                    {
                    }
                     //nothing
                    if (int.Parse(resultExchange) < 0)
                    {
                        (Global.someDropableObject["object"] as Hashtable)["x"] = placeHash["x"];
                        (Global.someDropableObject["object"] as Hashtable)["y"] = placeHash["y"];
                        //Для вещей
                        if ((Global.someDropableObject["object"] as Hashtable)["droptype"] as string == "thing")
                        {
                            int i = 0;
                            while (i < Global.globalInventoryThingsArr.Count)
                            {
                                if ((Global.someDropableObject["object"] as Hashtable)["uniqueID"] == Global.globalInventoryThingsArr[i]["uniqueID"])
                                {
                                    Global.globalInventoryThingsArr[i]["x"] = placeHash["x"];
                                    Global.globalInventoryThingsArr[i]["y"] = placeHash["y"];
                                    break;
                                }
                                i++;
                            }
                        }
                        //Для еды и лута
                        if ((Global.someDropableObject["object"] as Hashtable)["droptype"] as string == "consumable")
                        {
                            int j = 0;
                            while (j < Global.globalInventoryThingsArr.Count)
                            {
                                if (((Global.someDropableObject["object"] as Hashtable)["type"] as string == "food") 
                                    || ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "loot"))
                                {
                                    if ((Global.someDropableObject["object"] as Hashtable)["uniqueID"] == Global.globalInventoryThingsArr[j]["uniqueID"])
                                    {
                                        Global.globalInventoryThingsArr[j]["x"] = placeHash["x"];
                                        Global.globalInventoryThingsArr[j]["y"] = placeHash["y"];
                                        break;
                                    }
                                }
                                j++;
                            }
                        }
                    }
                    else
                    {
                        if (int.Parse(resultExchange) == 0)
                        {
                            (Global.someDropableObject["object"] as Hashtable)["x"] = "-5";
                            (Global.someDropableObject["object"] as Hashtable)["y"] = "-5";
                        }
                        else
                        {
                            if (int.Parse(resultExchange) > 0)
                            {
                                (Global.someDropableObject["object"] as Hashtable)["val"] = resultExchange;
                            }
                        }
                    }
                    Global._isDropStart = false;
                    Global._isWeCanDragAndDrop = false;
                    Global.someDropableObject = null;
                    Global.isMouseHold = false;
                }
                else
                {
                    this.c_GameController_Base_command_cancelDropSomeObject();
                    return;
                }
            }
            else
            {
                if (Global.someDropablePlace["droptype"] as string == "wear")
                {
                    bool isFullHealth = false;
                    if (Global._isFaceCharacterInventoryThings)
                    {
                        Global._is_needUpdate_dummy = true;
                        Global._is_needUpdate_invthings = true;
                        string placeString = (string) Global._someWearHashPlace_RectToStrplace[Global.someDropablePlace["droprect"]];
                        Global._placeHash_backMoveBetweenCells = new Hashtable() { {"x", (Global.someDropableObject["object"] as Hashtable)["x"] },  {"y", (Global.someDropableObject["object"] as Hashtable)["y"] }, };
                        if ((((((placeString == "armor") || (placeString == "helm")) || (placeString == "gloves")) || (placeString == "boots")) || (placeString == "amulet")) || ((placeString == "ring") || (placeString == "backpack")))
                        {
                            if ((!((Global.someDropableObject["object"] as Hashtable)["armor"] == null) 
                                && !((Global.someDropableObject["object"] as Hashtable)["armor"] as string == ""))
                                && !((Global.someDropableObject["object"] as Hashtable)["armor"] as string == "0"))
                            {
                                itemArmor = int.Parse((Global.someDropableObject["object"] as Hashtable)["armor"] as string);
                            }
                            if ((!((Global.someDropableObject["object"] as Hashtable)["health"] == null)
                                && !((Global.someDropableObject["object"] as Hashtable)["health"] as string == ""))
                                && !((Global.someDropableObject["object"] as Hashtable)["health"] as string == "0"))
                            {
                                itemHealth = int.Parse((Global.someDropableObject["object"] as Hashtable)["health"] as string);
                            }
                            minusHealth = Global._userMaxHealth - Global._userCurrentHealth;
                        }
                        if (Global.system_dummy[placeString] == null)
                        {
                            if ((placeString == "weapon") && !Global.isInvToDummy)
                            {
                                if (Global.someDropableObject["thingname"] as string == "thing_r3_gun")
                                {
                                    Global._isR3Gun = true;
                                    Global._isR4Gun = false;
                                    Global._system_distanceAttack = 10;
                                    Global._isAttack01 = false;
                                    Global._isAttack02 = true;
                                    Global._isAttack03 = false;
                                    Global.inv_r3_gun.SetActive(true);
                                    Global.r3_gun.SetActive(true);
                                    Global.inv_r4_gun.SetActive(false);
                                    Global.r4_gun.SetActive(false);
                                }
                                if (Global.someDropableObject["thingname"] as string == "thing_r4_gun")
                                {
                                    Global._isR3Gun = false;
                                    Global._isR4Gun = true;
                                    Global._system_distanceAttack = 10;
                                    Global._isAttack01 = false;
                                    Global._isAttack02 = false;
                                    Global._isAttack03 = true;
                                    Global.inv_r3_gun.SetActive(false);
                                    Global.r3_gun.SetActive(false);
                                    Global.inv_r4_gun.SetActive(true);
                                    Global.r4_gun.SetActive(true);
                                }
                            }
                            if ((((((placeString == "armor") || (placeString == "helm")) || (placeString == "gloves")) || (placeString == "boots")) || (placeString == "amulet")) || ((placeString == "ring") || (placeString == "backpack")))
                            {
                                Global._system_CharacterParams["armor"] = ((int) Global._system_CharacterParams["armor"]) + itemArmor;
                                Global._userMaxHealth = Global._userMaxHealth + itemHealth;
                                Global._userCurrentHealth = Global._userMaxHealth - minusHealth;
                                if (Global.someDropableObject["thingname"] as string == "thing_r3_backpack")
                                {
                                    Global._isR3Backpack = true;
                                    Global._isR5Backpack = false;
                                    Global.inv_r3_backpack.SetActive(true);
                                    Global.r3_backpack.SetActive(true);
                                    Global.inv_r5_backpack.SetActive(false);
                                    Global.r5_backpack.SetActive(false);
                                }
                                if (Global.someDropableObject["thingname"] as string == "thing_r5_backpack")
                                {
                                    Global._isR3Backpack = false;
                                    Global._isR5Backpack = true;
                                    Global.inv_r3_backpack.SetActive(false);
                                    Global.r3_backpack.SetActive(false);
                                    Global.inv_r5_backpack.SetActive(true);
                                    Global.r5_backpack.SetActive(true);
                                }
                                if (Global.someDropableObject["thingname"] as string == "thing_r4_body")
                                {
                                    Global._isR3Body = false;
                                    Global._isR4Body = true;
                                    Global.inv_r3_body.SetActive(false);
                                    Global.inv_r4_body.SetActive(true);
                                    Global.r3_body.SetActive(false);
                                    Global.r4_body.SetActive(true);
                                }
                                if (Global.someDropableObject["thingname"] as string == "thing_r4_hand")
                                {
                                    Global._isR3Hand = false;
                                    Global._isR4Hand = true;
                                    Global.inv_r3_hand.SetActive(false);
                                    Global.inv_r4_hand.SetActive(true);
                                    Global.r3_hand.SetActive(false);
                                    Global.r4_hand.SetActive(true);
                                }
                                if (Global.someDropableObject["thingname"] as string == "thing_r4_leg")
                                {
                                    Global._isR3Leg = false;
                                    Global._isR4Leg = true;
                                    Global.inv_r3_leg.SetActive(false);
                                    Global.inv_r4_leg.SetActive(true);
                                    Global.r3_leg.SetActive(false);
                                    Global.r4_leg.SetActive(true);
                                }
                            }
                            this.setSomeInInventoryToDummyPlace(placeString, Global._placeHash_backMoveBetweenCells);
                        }
                        else
                        {
                            if ((!((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["armor"] == null) 
                                && !((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["armor"] as string== "")) 
                                && !((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["armor"] as string== "0"))
                            {
                                removeArmor = int.Parse((string) (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["armor"]);
                            }
                            if ((!((Global.someDropableObject["object"] as Hashtable)["armor"] == null) 
                                && !((Global.someDropableObject["object"] as Hashtable)["armor"]as string == "")) 
                                && !((Global.someDropableObject["object"] as Hashtable)["armor"] as string == "0"))
                            {
                                addArmor = int.Parse((Global.someDropableObject["object"] as Hashtable)["armor"] as string);
                            }
                            if ((!((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["health"] == null) 
                                && !((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["health"] as string== "")) 
                                && !((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["health"] as string== "0"))
                            {
                                removeHealth = int.Parse((string) (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["health"]);
                            }
                            if ((!((Global.someDropableObject["object"] as Hashtable)["health"] == null) 
                                && !((Global.someDropableObject["object"] as Hashtable)["health"] as string== "")) 
                                && !((Global.someDropableObject["object"] as Hashtable)["health"] as string== "0"))
                            {
                                addHealth = int.Parse((Global.someDropableObject["object"] as Hashtable)["health"] as string);
                            }
                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["droptype"] },  {"type", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["type"] },  {"thingname", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["thingname"] },  {"icon", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["icon"] },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["x"] },  {"y", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["y"] },  {"uniqueID", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["uniqueID"] },  {"vid", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["vid"] },  {"level", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["level"] },  {"armor", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["armor"] },  {"health", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["health"] },  {"name", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["name"] },  {"costtype1", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costtype1"] },  {"costamount1", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costamount1"] },  {"costicon1", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costicon1"] },  {"costtype2", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costtype2"] },  {"costamount2", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costamount2"] },  {"costicon2", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costicon2"] },  {"costtype3", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costtype3"] },  {"costamount3", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costamount3"] },  {"costicon3", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costicon3"] },  {"costtype4", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costtype4"] },  {"costamount4", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costamount4"] },  {"costicon4", (Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["costicon4"] }, }));
                            //Вещи, которые снимаем
                            if ((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["thingname"] as string == "thing_r3_backpack")
                            {
                                Global._isR3Backpack = false;
                                Global.inv_r3_backpack.SetActive(false);
                                Global.r3_backpack.SetActive(false);
                            }
                            if ((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["thingname"] as string == "thing_r5_backpack")
                            {
                                Global._isR5Backpack = false;
                                Global.inv_r5_backpack.SetActive(false);
                                Global.r5_backpack.SetActive(false);
                            }
                            if ((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["thingname"] as string == "thing_r3_gun")
                            {
                                Global._isR3Gun = false;
                                Global._system_distanceAttack = 3;
                                Global._isAttack01 = true;
                                Global._isAttack02 = false;
                                Global.inv_r3_gun.SetActive(false);
                                Global.r3_gun.SetActive(false);
                            }
                            if ((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["thingname"] as string == "thing_r4_gun")
                            {
                                Global._isR4Gun = false;
                                Global._system_distanceAttack = 3;
                                Global._isAttack01 = true;
                                Global._isAttack03 = false;
                                Global.inv_r4_gun.SetActive(false);
                                Global.r4_gun.SetActive(false);
                            }
                            if ((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["thingname"] as string == "thing_r4_body")
                            {
                                Global._isR3Body = true;
                                Global._isR4Body = false;
                                Global.inv_r3_body.SetActive(true);
                                Global.inv_r4_body.SetActive(false);
                                Global.r3_body.SetActive(true);
                                Global.r4_body.SetActive(false);
                            }
                            if ((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["thingname"] as string == "thing_r4_hand")
                            {
                                Global._isR3Hand = true;
                                Global._isR4Hand = false;
                                Global.inv_r3_hand.SetActive(true);
                                Global.inv_r4_hand.SetActive(false);
                                Global.r3_hand.SetActive(true);
                                Global.r4_hand.SetActive(false);
                            }
                            if ((Global.system_dummy[Global.someDropableObject["type"]] as Hashtable)["thingname"] as string == "thing_r4_leg")
                            {
                                Global._isR3Leg = true;
                                Global._isR4Leg = false;
                                Global.inv_r3_leg.SetActive(true);
                                Global.inv_r4_leg.SetActive(false);
                                Global.r3_leg.SetActive(true);
                                Global.r4_leg.SetActive(false);
                            }
                            Global.system_dummy.Remove(Global.someDropableObject["type"]);
                            Global.system_dummy.Add(Global.someDropableObject["type"], new Hashtable(new Hashtable() { {"position", Global.someDropableObject["type"] },  {"droptype", Global.someDropableObject["droptype"] },  {"type", Global.someDropableObject["type"] },  {"thingname", Global.someDropableObject["thingname"] },  {"icon", Global.someDropableObject["icon"] },  {"fromdrag", Global.someDropableObject["fromdrag"] },  {"isdropable", Global.someDropableObject["isdropable"] },  {"x", Global.someDropableObject["x"] },  {"y", Global.someDropableObject["y"] },  {"uniqueID", ((string) (((string) (((string) (((string) Global.someDropableObject["type"]) + Random.Range(1111, 9999).ToString())) + "_")) + Random.Range(1411, 9599).ToString())) + Random.Range(1171, 9991).ToString() },  {"vid", Global.someDropableObject["vid"] },  {"name", Global.someDropableObject["name"] },  {"armor", Global.someDropableObject["armor"] },  {"health", Global.someDropableObject["health"] },  {"level", Global.someDropableObject["level"] },  {"costtype1", Global.someDropableObject["costtype1"] },  {"costamount1", Global.someDropableObject["costamount1"] },  {"costicon1", Global.someDropableObject["costicon1"] },  {"costtype2", Global.someDropableObject["costtype2"] },  {"costamount2", Global.someDropableObject["costamount2"] },  {"costicon2", Global.someDropableObject["costicon2"] },  {"costtype3", Global.someDropableObject["costtype3"] },  {"costamount3", Global.someDropableObject["costamount3"] },  {"costicon3", Global.someDropableObject["costicon3"] },  {"costtype4", Global.someDropableObject["costtype4"] },  {"costamount4", Global.someDropableObject["costamount4"] },  {"costicon4", Global.someDropableObject["costicon4"] }, }));
                            //Вещи, которые надеваем
                            if (Global.someDropableObject["thingname"] as string == "thing_r3_backpack")
                            {
                                Global._isR3Backpack = true;
                                Global.inv_r3_backpack.SetActive(true);
                                Global.r3_backpack.SetActive(true);
                            }
                            if (Global.someDropableObject["thingname"] as string == "thing_r5_backpack")
                            {
                                Global._isR5Backpack = true;
                                Global.inv_r5_backpack.SetActive(true);
                                Global.r5_backpack.SetActive(true);
                            }
                            if (Global.someDropableObject["thingname"] as string == "thing_r3_gun")
                            {
                                Global._isR3Gun = true;
                                Global._system_distanceAttack = 10;
                                Global._isAttack01 = false;
                                Global._isAttack02 = true;
                                Global.inv_r3_gun.SetActive(true);
                                Global.r3_gun.SetActive(true);
                            }
                            if (Global.someDropableObject["thingname"] as string == "thing_r4_gun")
                            {
                                Global._isR4Gun = true;
                                Global._system_distanceAttack = 10;
                                Global._isAttack01 = false;
                                Global._isAttack03 = true;
                                Global.inv_r4_gun.SetActive(true);
                                Global.r4_gun.SetActive(true);
                            }
                            if (Global.someDropableObject["thingname"] as string == "thing_r4_body")
                            {
                                Global._isR3Body = false;
                                Global._isR4Body = true;
                                Global.inv_r3_body.SetActive(false);
                                Global.inv_r4_body.SetActive(true);
                                Global.r3_body.SetActive(false);
                                Global.r4_body.SetActive(true);
                            }
                            if (Global.someDropableObject["thingname"] as string == "thing_r4_hand")
                            {
                                Global._isR3Hand = false;
                                Global._isR4Hand = true;
                                Global.inv_r3_hand.SetActive(false);
                                Global.inv_r4_hand.SetActive(true);
                                Global.r3_hand.SetActive(false);
                                Global.r4_hand.SetActive(true);
                            }
                            if (Global.someDropableObject["thingname"] as string == "thing_r4_leg")
                            {
                                Global._isR3Leg = false;
                                Global._isR4Leg = true;
                                Global.inv_r3_leg.SetActive(false);
                                Global.inv_r4_leg.SetActive(true);
                                Global.r3_leg.SetActive(false);
                                Global.r4_leg.SetActive(true);
                            }
                            Global._system_CharacterParams["armor"] = ((int) (((int) Global._system_CharacterParams["armor"]) - removeArmor)) + addArmor;
                            Global._userMaxHealth = (Global._userMaxHealth - removeHealth) + addHealth;
                            if (Global._userCurrentHealth > Global._userMaxHealth)
                            {
                                Global._userCurrentHealth = Global._userMaxHealth;
                            }
                            Global._userCurrentHealth = Global._userMaxHealth - minusHealth;
                            System.Collections.Generic.List<Hashtable> ThingsInvArrayTemp = Global.globalInventoryThingsArr;
                            int ThingsInvObjIndex = ThingsInvArrayTemp.IndexOf((Hashtable) Global.someDropableObject["object"]);
                            Global.globalInventoryThingsArr.Remove(Global.globalInventoryThingsArr[ThingsInvObjIndex]);
                            Global._isDropStart = false;
                            Global._isWeCanDragAndDrop = false;
                            Global.isMouseHold = false;
                            Global.someDropableObject = null;
                            Global.globalBus.SendMessage("c_GameController_Base_command_SortThings");
                        }
                        Global._isDropStart = false;
                        Global._isWeCanDragAndDrop = false;
                        Global.someDropableObject = null;
                        Global.isMouseHold = false;
                    }
                }
            }
        }
        else
        {
            if (Global.someDropableObject["fromdrag"] as string == "wear")
            {
                if (Global.someDropablePlace["droptype"] as string == "inventory")
                {
                    if (Global._isFaceCharacterInventoryThings)
                    {
                        Global._is_needUpdate_dummy = true;
                        Global._is_needUpdate_invthings = true;
                        string placeStringFrom = (string) (Global.someDropableObject["object"] as Hashtable)["position"];
                        Global._placeHash_backMoveBetweenCells = (Hashtable) Global._someInventoryHashPlace_RectToCoords[Global.someDropablePlace["droprect"]];
                        this.setSomeInInventoryFromDummyPlace(placeStringFrom, Global._placeHash_backMoveBetweenCells);
                        if (placeStringFrom == "weapon")
                        {
                        }
                        if ((((((placeStringFrom == "armor") || (placeStringFrom == "helm")) || (placeStringFrom == "gloves")) || (placeStringFrom == "boots")) || (placeStringFrom == "amulet")) || (placeStringFrom == "ring"))
                        {
                        }
                        Global._isDropStart = false;
                        Global._isWeCanDragAndDrop = false;
                        Global.someDropableObject = null;
                        Global.isMouseHold = false;
                    }
                    else
                    {
                        this.c_GameController_Base_command_cancelDropSomeObject();
                        return;
                    }
                }
                else
                {
                    if (Global.someDropablePlace["droptype"] as string == "wear")
                    {
                        string placeStringFromWear = (string) (Global.someDropableObject["object"] as Hashtable)["position"];
                        string placeStringTo = (string) Global._someWearHashPlace_RectToStrplace[Global.someDropablePlace["droprect"]];
                        if (placeStringFromWear != placeStringTo)
                        {
                            Global._is_needUpdate_dummy = true;
                            this.setSomeBetweenDummyPlace(placeStringFromWear, placeStringTo);
                        }
                        else
                        {
                            this.c_GameController_Base_command_cancelDropSomeObject();
                            return;
                        }
                    }
                    else
                    {
                        if (Global.someDropablePlace["droptype"] as string == "dummy")
                        {
                            Global._isWeCanDragAndDrop = false;
                            Global.someDropableObject = null;
                            Global.isMouseHold = false;
                        }
                    }
                }
            }
            else
            {
                if (Global.someDropableObject["fromdrag"] as string == "storage_inv")
                {
                    if (Global.someDropablePlace["droptype"] as string == "storage_inv")
                    {
                        placeHash = (Hashtable) Global._someStorageInventoryHashPlace_RectToCoords[Global.someDropablePlace["droprect"]];
                        Global._placeHash_backMoveBetweenCells = new Hashtable() { {"x", (Global.someDropableObject["object"] as Hashtable)["x"] },  {"y", (Global.someDropableObject["object"] as Hashtable)["y"] }, };
                        if (!(placeHash["x"] == Global._placeHash_backMoveBetweenCells["x"]) || !(placeHash["y"] == Global._placeHash_backMoveBetweenCells["y"]))
                        {
                            var resultExchange = "-1";
                            if (Global._isFaceCharacterInventoryThings)
                            {
                                Global._is_needUpdate_invthings = true;
                                this.setSomeInInventoryToOldPlace(Global.StorageInventoryArr, placeHash, Global._placeHash_backMoveBetweenCells);
                            }
                            else
                            {
                            }
                            //nothing
                            if (int.Parse(resultExchange) < 0)
                            {
                                (Global.someDropableObject["object"] as Hashtable)["x"] = placeHash["x"];
                                (Global.someDropableObject["object"] as Hashtable)["y"] = placeHash["y"];
                                //Для вещей
                                if ((Global.someDropableObject["object"] as Hashtable)["droptype"] as string == "thing")
                                {
                                    int k = 0;
                                    while (k < Global.StorageInventoryArr.Count)
                                    {
                                        if ((Global.someDropableObject["object"] as Hashtable)["uniqueID"] == Global.StorageInventoryArr[k]["uniqueID"])
                                        {
                                            Global.StorageInventoryArr[k]["x"] = placeHash["x"];
                                            Global.StorageInventoryArr[k]["y"] = placeHash["y"];
                                            break;
                                        }
                                        k++;
                                    }
                                }
                                //Для еды и лута
                                if ((Global.someDropableObject["object"] as Hashtable)["droptype"] as string == "consumable")
                                {
                                    int m = 0;
                                    while (m < Global.StorageInventoryArr.Count)
                                    {
                                        if (((Global.someDropableObject["object"] as Hashtable)["type"] as string == "food") 
                                            || ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "loot"))
                                        {
                                            if ((Global.someDropableObject["object"] as Hashtable)["uniqueID"] == Global.StorageInventoryArr[m]["uniqueID"])
                                            {
                                                Global.StorageInventoryArr[m]["x"] = placeHash["x"];
                                                Global.StorageInventoryArr[m]["y"] = placeHash["y"];
                                                break;
                                            }
                                        }
                                        m++;
                                    }
                                }
                            }
                            else
                            {
                                if (int.Parse(resultExchange) == 0)
                                {
                                    (Global.someDropableObject["object"] as Hashtable)["x"] = "-5";
                                    (Global.someDropableObject["object"] as Hashtable)["y"] = "-5";
                                }
                                else
                                {
                                    if (int.Parse(resultExchange) > 0)
                                    {
                                        (Global.someDropableObject["object"] as Hashtable)["val"] = resultExchange;
                                    }
                                }
                            }
                            Global._isDropStart = false;
                            Global._isWeCanDragAndDrop = false;
                            Global.someDropableObject = null;
                            Global.isMouseHold = false;
                        }
                        else
                        {
                            this.c_GameController_Base_command_cancelDropSomeObject();
                            return;
                        }
                    }
                    else
                    {
                        if (Global.someDropablePlace["droptype"] as string == "storage")
                        {
                            if (Global._isFaceCharacterInventoryThings)
                            {
                                placeHash = (Hashtable) Global._someStorageHashPlace_RectToCoords[Global.someDropablePlace["droprect"]];
                                Global._placeHash_backMoveBetweenCells = new Hashtable() { {"x", (Global.someDropableObject["object"] as Hashtable)["x"] },  {"y", (Global.someDropableObject["object"] as Hashtable)["y"] }, };
                                this.setSomeThingsToOldPlaceForStorage(Global.StorageArr, placeHash, Global._placeHash_backMoveBetweenCells, (string) (Global.someDropableObject["object"] as Hashtable)["uniqueID"], true);
                            }
                            Global._isDropStart = false;
                            Global._isWeCanDragAndDrop = false;
                            Global.someDropableObject = null;
                            Global.isMouseHold = false;
                        }
                    }
                }
                else
                {
                    if (Global.someDropableObject["fromdrag"] as string == "storage")
                    {
                        if (Global.someDropablePlace["droptype"] as string == "storage")
                        {
                            placeHash = (Hashtable) Global._someStorageHashPlace_RectToCoords[Global.someDropablePlace["droprect"]];
                            Global._placeHash_backMoveBetweenCells = new Hashtable() { {"x", (Global.someDropableObject["object"] as Hashtable)["x"] },  {"y", (Global.someDropableObject["object"] as Hashtable)["y"] }, };
                            if (!(placeHash["x"] == Global._placeHash_backMoveBetweenCells["x"]) || !(placeHash["y"] == Global._placeHash_backMoveBetweenCells["y"]))
                            {
                                resultExchange = "-1";
                                if (Global._isFaceCharacterInventoryThings)
                                {
                                    Global._is_needUpdate_invthings = true;
                                    this.setSomeInInventoryToOldPlace(Global.StorageArr, placeHash, Global._placeHash_backMoveBetweenCells);
                                }
                                else
                                {
                                }
                                //nothing
                                if (int.Parse(resultExchange) < 0)
                                {
                                    (Global.someDropableObject["object"] as Hashtable)["x"] = placeHash["x"];
                                    (Global.someDropableObject["object"] as Hashtable)["y"] = placeHash["y"];
                                    //Для вещей
                                    if ((Global.someDropableObject["object"] as Hashtable)["droptype"] as string == "thing")
                                    {
                                        int o = 0;
                                        while (o < Global.StorageArr.Count)
                                        {
                                            if ((Global.someDropableObject["object"] as Hashtable)["uniqueID"] == Global.StorageArr[o]["uniqueID"])
                                            {
                                                Global.StorageArr[o]["x"] = placeHash["x"];
                                                Global.StorageArr[o]["y"] = placeHash["y"];
                                                break;
                                            }
                                            o++;
                                        }
                                    }
                                    //Для еды и лута
                                    if ((Global.someDropableObject["object"] as Hashtable)["droptype"] as string == "consumable")
                                    {
                                        int p = 0;
                                        while (p < Global.StorageArr.Count)
                                        {
                                            if (((Global.someDropableObject["object"] as Hashtable)["type"] as string == "food") 
                                                || ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "loot"))
                                            {
                                                if ((Global.someDropableObject["object"] as Hashtable)["uniqueID"] == Global.StorageArr[p]["uniqueID"])
                                                {
                                                    Global.StorageArr[p]["x"] = placeHash["x"];
                                                    Global.StorageArr[p]["y"] = placeHash["y"];
                                                    break;
                                                }
                                            }
                                            p++;
                                        }
                                    }
                                }
                                else
                                {
                                    if (int.Parse(resultExchange) == 0)
                                    {
                                        (Global.someDropableObject["object"] as Hashtable)["x"] = "-5";
                                        (Global.someDropableObject["object"] as Hashtable)["y"] = "-5";
                                    }
                                    else
                                    {
                                        if (int.Parse(resultExchange) > 0)
                                        {
                                            (Global.someDropableObject["object"] as Hashtable)["val"] = resultExchange;
                                        }
                                    }
                                }
                                if ((!Global._isTutorial41 && Global._isTutorial42) || (!Global._isTutorial44 && Global._isTutorial45))
                                {
                                }
                                else
                                {
                                     //nothing
                                    Global._isDropStart = false;
                                    Global._isWeCanDragAndDrop = false;
                                    Global.someDropableObject = null;
                                    Global.isMouseHold = false;
                                }
                            }
                            else
                            {
                                this.c_GameController_Base_command_cancelDropSomeObject();
                                return;
                            }
                        }
                        else
                        {
                            if (Global.someDropablePlace["droptype"] as string == "storage_inv")
                            {
                                if (Global._isFaceCharacterInventoryThings)
                                {
                                    placeHash = (Hashtable) Global._someStorageInventoryHashPlace_RectToCoords[Global.someDropablePlace["droprect"]];
                                    Global._placeHash_backMoveBetweenCells = new Hashtable() { {"x", (Global.someDropableObject["object"] as Hashtable)["x"] },  {"y", (Global.someDropableObject["object"] as Hashtable)["y"] }, };
                                    this.setSomeThingsToOldPlaceForStorage(Global.StorageInventoryArr, placeHash, Global._placeHash_backMoveBetweenCells, (string) (Global.someDropableObject["object"] as Hashtable)["uniqueID"], false);
                                }
                                Global._isDropStart = false;
                                Global._isWeCanDragAndDrop = false;
                                Global.someDropableObject = null;
                                Global.isMouseHold = false;
                            }
                        }
                    }
                }
            }
        }
    }

    private void setSomeInInventoryToOldPlace(System.Collections.Generic.List<Hashtable> inArr, Hashtable newcoord, Hashtable oldcoord)
    {
        foreach (Hashtable one in inArr)
        {
            if ((one["x"] == newcoord["x"]) && (one["y"] == newcoord["y"]))
            {
                one["x"] = oldcoord["x"];
                one["y"] = oldcoord["y"];
                Global.someDropableObjectBetween = one;
            }
        }
    }

    private void setSomeThingsToOldPlaceForStorage(System.Collections.Generic.List<Hashtable> inArr, Hashtable newcoord, Hashtable oldcoord, string typethis, bool isToStorage)
    {
        int TempIndex = 0;
        bool addFood = false;
        int addFoodIndex = 0;
        bool addNewFood = false;
        bool addLoot = false;
        int addLootIndex = 0;
        bool addNewLoot = false;
        foreach (Hashtable one in inArr)
        {
            if (((one["x"] == newcoord["x"]) && (one["y"] == newcoord["y"])) && !(one["uniqueID"] as string == typethis))
            {
                Global._isDropStart = false;
                Global._isWeCanDragAndDrop = true;
                Global.someDropableObject = null;
                Global.isMouseHold = false;
                return;
            }
        }
        if (isToStorage)
        {
            if ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "food")
            {
                if (Global.StorageArr.Count > 0)
                {
                    int i = 0;
                    while (i < Global.StorageArr.Count)
                    {
                        if (Global.StorageArr[i]["thingname"] == (Global.someDropableObject["object"] as Hashtable)["thingname"])
                        {
                            Global.StorageArr[i]["val"] = (int.Parse((string) Global.StorageArr[i]["val"]) + int.Parse((string) (Global.someDropableObject["object"] as Hashtable)["val"])).ToString();
                            break;
                        }
                        else
                        {
                            if (i == (Global.StorageArr.Count - 1))
                            {
                                Global.StorageArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"addhealth", (Global.someDropableObject["object"] as Hashtable)["addhealth"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                                break;
                            }
                        }
                        i++;
                    }
                }
                else
                {
                    Global.StorageArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"addhealth", (Global.someDropableObject["object"] as Hashtable)["addhealth"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                }
            }
            else
            {
                if ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "loot")
                {
                    if (Global.StorageArr.Count > 0)
                    {
                        int k = 0;
                        while (k < Global.StorageArr.Count)
                        {
                            if (Global.StorageArr[k]["thingname"] == (Global.someDropableObject["object"] as Hashtable)["thingname"])
                            {
                                Global.StorageArr[k]["val"] = (int.Parse((string) Global.StorageArr[k]["val"]) + int.Parse((string) (Global.someDropableObject["object"] as Hashtable)["val"])).ToString();
                                break;
                            }
                            else
                            {
                                if (k == (Global.StorageArr.Count - 1))
                                {
                                    Global.StorageArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" }, }));
                                    break;
                                }
                            }
                            k++;
                        }
                    }
                    else
                    {
                        Global.StorageArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" }, }));
                    }
                    if (Global._userResourcesFood.ContainsKey((Global.someDropableObject["object"] as Hashtable)["thingname"]))
                    {
                        (Global._userResourcesFood[(Global.someDropableObject["object"] as Hashtable)["thingname"]] as Hashtable)["amount"] = "0";
                    }
                    if (Global._userResourcesCraft.ContainsKey((Global.someDropableObject["object"] as Hashtable)["thingname"]))
                    {
                        (Global._userResourcesCraft[(Global.someDropableObject["object"] as Hashtable)["thingname"]] as Hashtable)["amount"] = "0";
                    }
                }
                else
                {
                    Global.StorageArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                }
            }
            System.Collections.Generic.List<Hashtable> ThingsArrayTemp = Global.StorageInventoryArr;
            int ThingsObjIndex = ThingsArrayTemp.IndexOf((Hashtable) Global.someDropableObject["object"]);
            if (ThingsObjIndex < (Global.globalInventoryThingsArr.Count - 1))
            {
                TempIndex = ThingsObjIndex + 1;
            }
            else
            {
                TempIndex = ThingsObjIndex;
            }
            Global.StorageInventoryArr.Remove(Global.StorageInventoryArr[ThingsObjIndex]);
            Global.globalInventoryThingsArr.Remove(Global.globalInventoryThingsArr[TempIndex]);
            Global._isDropStart = false;
            Global._isWeCanDragAndDrop = false;
            Global.isMouseHold = false;
            Global.someDropableObject = null;
        }
        else
        {
            if ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "food")
            {
                if (Global.StorageInventoryArr.Count > 0)
                {
                    int j = 0;
                    while (j < Global.StorageInventoryArr.Count)
                    {
                        if (Global.StorageInventoryArr[j]["thingname"] == (Global.someDropableObject["object"] as Hashtable)["thingname"])
                        {
                            Global.StorageInventoryArr[j]["val"] = (int.Parse((string) Global.StorageInventoryArr[j]["val"]) + int.Parse((string) (Global.someDropableObject["object"] as Hashtable)["val"])).ToString();
                            break;
                        }
                        if (Global.StorageInventoryArr[j]["thingname"] == (Global.someDropableObject["object"] as Hashtable)["thingname"])
                        {
                            addFoodIndex = j + 1;
                            addFood = true;
                            break;
                        }
                        else
                        {
                            if (j == (Global.StorageInventoryArr.Count - 1))
                            {
                                Global.StorageInventoryArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage_inv" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"addhealth", (Global.someDropableObject["object"] as Hashtable)["addhealth"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                                addNewFood = true;
                                break;
                            }
                        }
                        j++;
                    }
                    if (addFood)
                    {
                        addFood = false;
                        Global.globalInventoryThingsArr[addFoodIndex]["val"] = (int.Parse((string) Global.globalInventoryThingsArr[addFoodIndex]["val"]) + int.Parse((string) (Global.someDropableObject["object"] as Hashtable)["val"])).ToString();
                    }
                    if (addNewFood)
                    {
                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"addhealth", (Global.someDropableObject["object"] as Hashtable)["addhealth"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                        addNewFood = false;
                    }
                }
                else
                {
                    Global.StorageInventoryArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage_inv" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"addhealth", (Global.someDropableObject["object"] as Hashtable)["addhealth"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"addhealth", (Global.someDropableObject["object"] as Hashtable)["addhealth"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                }
            }
            else
            {
                if ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "loot")
                {
                    if (Global.StorageInventoryArr.Count > 0)
                    {
                        int q = 0;
                        while (q < Global.StorageInventoryArr.Count)
                        {
                            if (Global.StorageInventoryArr[q]["thingname"] == (Global.someDropableObject["object"] as Hashtable)["thingname"])
                            {
                                Global.StorageInventoryArr[q]["val"] = (int.Parse((string) Global.StorageInventoryArr[q]["val"]) + int.Parse((string) (Global.someDropableObject["object"] as Hashtable)["val"])).ToString();
                                if (Global._userResourcesFood.ContainsKey((Global.someDropableObject["object"] as Hashtable)["thingname"]))
                                {
                                    (Global._userResourcesFood[(Global.someDropableObject["object"] as Hashtable)["thingname"]] as Hashtable)["amount"] = (int.Parse((string) (Global._userResourcesFood[(Global.someDropableObject["object"] as Hashtable)["thingname"]] as Hashtable)["amount"]) + int.Parse((string) (Global.someDropableObject["object"] as Hashtable)["val"])).ToString();
                                }
                                if (Global._userResourcesCraft.ContainsKey((Global.someDropableObject["object"] as Hashtable)["thingname"]))
                                {
                                    (Global._userResourcesCraft[(Global.someDropableObject["object"] as Hashtable)["thingname"]] as Hashtable)["amount"] = (int.Parse((string) (Global._userResourcesCraft[(Global.someDropableObject["object"] as Hashtable)["thingname"]] as Hashtable)["amount"]) + int.Parse((string) (Global.someDropableObject["object"] as Hashtable)["val"])).ToString();
                                }
                                break;
                            }
                            if (Global.StorageInventoryArr[q]["thingname"] == (Global.someDropableObject["object"] as Hashtable)["thingname"])
                            {
                                addLootIndex = q + 1;
                                addLoot = true;
                                break;
                            }
                            else
                            {
                                if (q == (Global.StorageInventoryArr.Count - 1))
                                {
                                    Global.StorageInventoryArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage_inv" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                                    addNewLoot = true;
                                    break;
                                }
                            }
                            q++;
                        }
                        if (addLoot)
                        {
                            addLoot = false;
                            Global.globalInventoryThingsArr[addLootIndex]["val"] = (int.Parse((string) Global.globalInventoryThingsArr[addLootIndex]["val"]) + int.Parse((string) (Global.someDropableObject["object"] as Hashtable)["val"])).ToString();
                        }
                        if (addNewLoot)
                        {
                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                            if (Global._userResourcesFood.ContainsKey((Global.someDropableObject["object"] as Hashtable)["thingname"]))
                            {
                                (Global._userResourcesFood[(Global.someDropableObject["object"] as Hashtable)["thingname"]] as Hashtable)["amount"] = (Global.someDropableObject["object"] as Hashtable)["val"];
                            }
                            if (Global._userResourcesCraft.ContainsKey((Global.someDropableObject["object"] as Hashtable)["thingname"]))
                            {
                                (Global._userResourcesCraft[(Global.someDropableObject["object"] as Hashtable)["thingname"]] as Hashtable)["amount"] = (Global.someDropableObject["object"] as Hashtable)["val"];
                            }
                            addNewLoot = false;
                        }
                    }
                    else
                    {
                        Global.StorageInventoryArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage_inv" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"addhealth", (Global.someDropableObject["object"] as Hashtable)["addhealth"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"val", (Global.someDropableObject["object"] as Hashtable)["val"] },  {"addhealth", (Global.someDropableObject["object"] as Hashtable)["addhealth"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"selected", "false" },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                    }
                }
                else
                {
                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                    Global.StorageInventoryArr.Add(new Hashtable(new Hashtable() { {"droptype", (Global.someDropableObject["object"] as Hashtable)["droptype"] },  {"type", (Global.someDropableObject["object"] as Hashtable)["type"] },  {"thingname", (Global.someDropableObject["object"] as Hashtable)["thingname"] },  {"icon", (Global.someDropableObject["object"] as Hashtable)["icon"] },  {"fromdrag", "storage_inv" },  {"isdropable", "true" },  {"x", newcoord["x"] },  {"y", newcoord["y"] },  {"uniqueID", (Global.someDropableObject["object"] as Hashtable)["uniqueID"] },  {"vid", (Global.someDropableObject["object"] as Hashtable)["vid"] },  {"level", (Global.someDropableObject["object"] as Hashtable)["level"] },  {"name", (Global.someDropableObject["object"] as Hashtable)["name"] },  {"costtype1", (Global.someDropableObject["object"] as Hashtable)["costtype1"] },  {"costamount1", (Global.someDropableObject["object"] as Hashtable)["costamount1"] },  {"costicon1", (Global.someDropableObject["object"] as Hashtable)["costicon1"] },  {"costtype2", (Global.someDropableObject["object"] as Hashtable)["costtype2"] },  {"costamount2", (Global.someDropableObject["object"] as Hashtable)["costamount2"] },  {"costicon2", (Global.someDropableObject["object"] as Hashtable)["costicon2"] },  {"costtype3", (Global.someDropableObject["object"] as Hashtable)["costtype3"] },  {"costamount3", (Global.someDropableObject["object"] as Hashtable)["costamount3"] },  {"costicon3", (Global.someDropableObject["object"] as Hashtable)["costicon3"] },  {"costtype4", (Global.someDropableObject["object"] as Hashtable)["costtype4"] },  {"costamount4", (Global.someDropableObject["object"] as Hashtable)["costamount4"] },  {"costicon4", (Global.someDropableObject["object"] as Hashtable)["costicon4"] }, }));
                }
            }
            System.Collections.Generic.List<Hashtable> StorageArrayTemp = Global.StorageArr;
            int StorageObjIndex = StorageArrayTemp.IndexOf((Hashtable) Global.someDropableObject["object"]);
            Global.StorageArr.Remove(Global.StorageArr[StorageObjIndex]);
            Global._isDropStart = false;
            Global._isWeCanDragAndDrop = false;
            Global.isMouseHold = false;
            Global.someDropableObject = null;
        }
    }

    public virtual void c_GameController_Base_command_cancelDropSomeObject()
    {
        if ((!Global._isTutorial41 && Global._isTutorial42) || (!Global._isTutorial44 && Global._isTutorial45))
        {
        }
        else
        {
             //nothing
            Global._isDropStart = false;
            Global._isWeCanDragAndDrop = false;
            Global.someDropableObject = null;
            Global.isMouseHold = false;
        }
    }

    private void setSomeInInventoryToDummyPlace(string newcoord, Hashtable oldcoord)
    {
        (Global.someDropableObject["object"] as Hashtable)["level"] = "1";
        if (int.Parse((string) (Global.someDropableObject["object"] as Hashtable)["level"]) <= Global._system_LEVELINT)
        {
            foreach (DictionaryEntry oneEntry in Global.system_dummy)
            {
                if ((oneEntry.Value as Hashtable)["position"] as string == newcoord)
                {
                    if (!(Global.system_dummy[newcoord] == null))
                    {
                    }
                     //nothing
                    Global.someDropableObjectBetween = (Hashtable) oneEntry.Value;
                    Hashtable someCopyDummyThing = Global.cloneHashtable((Hashtable) oneEntry.Value);
                    someCopyDummyThing["x"] = oldcoord["x"];
                    someCopyDummyThing["y"] = oldcoord["y"];
                    if ((oneEntry.Value as Hashtable)["position"] as string == "weapon")
                    {
                        Global.isInvToDummy = true;
                    }
                    if (((((((oneEntry.Value as Hashtable)["position"] as string == "armor")
                        || ((oneEntry.Value as Hashtable)["position"] as string == "helm")) 
                        || ((oneEntry.Value as Hashtable)["position"] as string == "gloves")) 
                        || ((oneEntry.Value as Hashtable)["position"] as string == "boots"))
                        || ((oneEntry.Value as Hashtable)["position"] as string == "amulet"))
                        || ((oneEntry.Value as Hashtable)["position"] as string == "ring"))
                    {
                        Global.isInvToDummy = true;
                    }
                    Global.concatHashtable((Hashtable) oneEntry.Value, (Hashtable) Global.someDropableObject["object"]);
                    Global.concatHashtable((Hashtable) Global.someDropableObject["object"], someCopyDummyThing);
                    return;
                }
            }
            if (Global.system_dummy[Global.someDropableObject["type"]] == null)
            {
                Hashtable someCopyInvThing = Global.cloneHashtable((Hashtable) Global.someDropableObject["object"]);
                someCopyInvThing["position"] = newcoord;
                Global.system_dummy[newcoord] = someCopyInvThing;
                System.Collections.Generic.List<Hashtable> ThingsArrayTemp = Global.globalInventoryThingsArr;
                int ThingsObjIndex = ThingsArrayTemp.IndexOf((Hashtable) Global.someDropableObject["object"]);
                Global.globalInventoryThingsArr.Remove(Global.globalInventoryThingsArr[ThingsObjIndex]);
                Global._isDropStart = false;
                Global._isWeCanDragAndDrop = false;
                Global.isMouseHold = false;
                Global.someDropableObject = null;
                Global.globalBus.SendMessage("c_GameController_Base_command_SortThings");
            }
            else
            {
            }
             //nothing
            Global.isInvToDummy = false;
        }
        else
        {
            if (Global.someDropablePlace["droptype"] as string == "wear")
            {
                if (Global._isFaceCharacterInventoryThings)
                {
                    Global._is_needUpdate_dummy = true;
                    Global._is_needUpdate_invthings = true;
                    string placeString = (string) Global._someWearHashPlace_RectToStrplace[Global.someDropablePlace["droprect"]];
                    Global._placeHash_backMoveBetweenCells = new Hashtable() { {"x", (Global.someDropableObject["object"] as Hashtable)["x"] },  {"y", (Global.someDropableObject["object"] as Hashtable)["y"] }, };
                    this.setSomeInInventoryToDummyPlace(placeString, Global._placeHash_backMoveBetweenCells);
                    if ((placeString == "weapon") && !Global.isInvToDummy)
                    {
                    }
                    if ((((((placeString == "armor") || (placeString == "helm")) || (placeString == "gloves")) || (placeString == "boots")) || (placeString == "amulet")) || (placeString == "ring"))
                    {
                    }
                    Global._isDropStart = false;
                    Global._isWeCanDragAndDrop = true;
                    Global.someDropableObject = null;
                    if ((placeString == "weapon") && !Global.isInvToDummy)
                    {
                    }
                    if (((((((placeString == "armor") && !Global.isInvToDummy) || ((placeString == "helm") && !Global.isInvToDummy)) || ((placeString == "gloves") && !Global.isInvToDummy)) || ((placeString == "boots") && !Global.isInvToDummy)) || ((placeString == "amulet") && !Global.isInvToDummy)) || ((placeString == "ring") && !Global.isInvToDummy))
                    {
                    }
                }
            }
        }
    }

    private void setSomeInInventoryFromDummyPlace(string newcoord, Hashtable oldcoord)
    {
        minusHealth = Global._userMaxHealth - Global._userCurrentHealth;
        foreach (Hashtable one in Global.globalInventoryThingsArr)
        {
            if ((one["x"] == oldcoord["x"]) && (one["y"] == oldcoord["y"]))
            {
                if (int.Parse((string) one["level"]) > Global._system_LEVELINT)
                {
                    return;
                }
                if ((Global.someDropableObject["object"] as Hashtable)["type"] == one["type"])
                {
                    Global.someDropableObjectBetween = one;
                    Hashtable someCopyDummyThing = Global.cloneHashtable((Hashtable) Global.someDropableObject["object"]);
                    Global.concatHashtable((Hashtable) Global.someDropableObject["object"], one);
                    (Global.someDropableObject["object"] as Hashtable)["position"] = newcoord;
                    Global.concatHashtable(one, someCopyDummyThing);
                    one["x"] = oldcoord["x"];
                    one["y"] = oldcoord["y"];
                    if (one["type"] as string == "weapon")
                    {
                        if (one["thingname"] as string == "thing_r3_gun")
                        {
                            Global._isR3Gun = true;
                            Global._isR4Gun = false;
                            Global._system_distanceAttack = 10;
                            Global._isAttack01 = false;
                            Global._isAttack02 = true;
                            Global._isAttack03 = false;
                            Global.inv_r3_gun.SetActive(true);
                            Global.r3_gun.SetActive(true);
                            Global.inv_r4_gun.SetActive(false);
                            Global.r4_gun.SetActive(false);
                        }
                        if (one["thingname"] as string == "thing_r4_gun")
                        {
                            Global._isR3Gun = false;
                            Global._isR4Gun = true;
                            Global._system_distanceAttack = 10;
                            Global._isAttack01 = false;
                            Global._isAttack02 = false;
                            Global._isAttack03 = true;
                            Global.inv_r3_gun.SetActive(false);
                            Global.r3_gun.SetActive(false);
                            Global.inv_r4_gun.SetActive(true);
                            Global.r4_gun.SetActive(true);
                        }
                    }
                    if ((!(one["armor"] == null) && !(one["armor"] as string == "")) && !(one["armor"] as string == "0"))
                    {
                        removeArmor = int.Parse((string) one["armor"]);
                    }
                    if ((!((Global.someDropableObject["object"] as Hashtable)["armor"] == null) 
                        && !((Global.someDropableObject["object"] as Hashtable)["armor"] as string== ""))
                        && !((Global.someDropableObject["object"] as Hashtable)["armor"] as string== "0"))
                    {
                        addArmor = int.Parse((Global.someDropableObject["object"] as Hashtable)["armor"] as string);
                    }
                    if ((!(one["health"] == null) && !(one["health"] as string == "")) && !(one["health"] as string == "0"))
                    {
                        removeHealth = int.Parse((string) one["health"]);
                    }
                    if ((!((Global.someDropableObject["object"] as Hashtable)["health"] == null) 
                        && !((Global.someDropableObject["object"] as Hashtable)["health"] as string == "")) 
                        && !((Global.someDropableObject["object"] as Hashtable)["health"] as string== "0"))
                    {
                        addHealth = int.Parse((Global.someDropableObject["object"] as Hashtable)["health"] as string);
                    }
                    Global._system_CharacterParams["armor"] = ((int) (((int) Global._system_CharacterParams["armor"]) - removeArmor)) + addArmor;
                    Global._userMaxHealth = (Global._userMaxHealth - removeHealth) + addHealth;
                    if (Global._userCurrentHealth > Global._userMaxHealth)
                    {
                        Global._userCurrentHealth = Global._userMaxHealth;
                    }
                    Global._userCurrentHealth = Global._userMaxHealth - minusHealth;
                    if ((((((one["type"] as string == "armor") 
                        || (one["type"] as string == "helm")) 
                        || (one["type"] as string == "gloves")) 
                        || (one["type"] as string == "boots"))
                        || (one["type"] as string == "amulet")) 
                        || (one["type"] as string == "ring"))
                    {
                        if (one["thingname"] as string == "thing_r3_backpack")
                        {
                            Global._isR3Backpack = true;
                            Global._isR5Backpack = false;
                            Global.inv_r3_backpack.SetActive(true);
                            Global.r3_backpack.SetActive(true);
                            Global.inv_r5_backpack.SetActive(false);
                            Global.r5_backpack.SetActive(false);
                        }
                        if (one["thingname"] as string == "thing_r5_backpack")
                        {
                            Global._isR3Backpack = false;
                            Global._isR5Backpack = true;
                            Global.inv_r3_backpack.SetActive(false);
                            Global.r3_backpack.SetActive(false);
                            Global.inv_r5_backpack.SetActive(true);
                            Global.r5_backpack.SetActive(true);
                        }
                        if (one["thingname"] as string == "thing_r4_body")
                        {
                            Global._isR3Body = false;
                            Global._isR4Body = true;
                            Global.inv_r3_body.SetActive(false);
                            Global.inv_r4_body.SetActive(true);
                            Global.r3_body.SetActive(false);
                            Global.r4_body.SetActive(true);
                        }
                        if (one["thingname"] as string == "thing_r3_hand")
                        {
                            Global._isR3Hand = false;
                            Global._isR4Hand = true;
                            Global.inv_r3_hand.SetActive(false);
                            Global.inv_r4_hand.SetActive(true);
                            Global.r3_hand.SetActive(false);
                            Global.r4_hand.SetActive(true);
                        }
                        if (one["thingname"] as string == "thing_r4_hand")
                        {
                            Global._isR3Hand = true;
                            Global._isR4Hand = false;
                            Global.inv_r3_hand.SetActive(true);
                            Global.inv_r4_hand.SetActive(false);
                            Global.r3_hand.SetActive(true);
                            Global.r4_hand.SetActive(false);
                        }
                        if (one["thingname"] as string == "thing_r4_leg")
                        {
                            Global._isR3Leg = false;
                            Global._isR4Leg = true;
                            Global.inv_r3_leg.SetActive(false);
                            Global.inv_r4_leg.SetActive(true);
                            Global.r3_leg.SetActive(false);
                            Global.r4_leg.SetActive(true);
                        }
                    }
                    return;
                }
                else
                {
                    Global._isDropStart = false;
                    Global._isWeCanDragAndDrop = true;
                    Global.someDropableObject = null;
                    return;
                }
            }
            else
            {
                if ((!((Global.someDropableObject["object"] as Hashtable)["armor"] == null)
                    && !((Global.someDropableObject["object"] as Hashtable)["armor"] as string == "")) 
                    && !((Global.someDropableObject["object"] as Hashtable)["armor"] as string == "0"))
                {
                    removeArmor = int.Parse((Global.someDropableObject["object"] as Hashtable)["armor"]as string);
                }

                if ((!((Global.someDropableObject["object"] as Hashtable)["health"] == null)
                    && !((Global.someDropableObject["object"] as Hashtable)["health"] as string == "")) 
                    && !((Global.someDropableObject["object"] as Hashtable)["health"] as string == "0"))
                {
                    removeHealth = int.Parse((Global.someDropableObject["object"] as Hashtable)["health"] as string);
                }

                if ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "weapon")
                {
                    if (Global.someDropableObject["thingname"] as string == "thing_r3_gun")
                    {
                        Global._isR3Gun = false;
                        Global._system_distanceAttack = 3;
                        Global._isAttack01 = true;
                        Global._isAttack02 = false;
                        Global.inv_r3_gun.SetActive(false);
                        Global.r3_gun.SetActive(false);
                    }
                    if (Global.someDropableObject["thingname"] as string == "thing_r4_gun")
                    {
                        Global._isR4Gun = false;
                        Global._system_distanceAttack = 3;
                        Global._isAttack01 = true;
                        Global._isAttack03 = false;
                        Global.inv_r4_gun.SetActive(false);
                        Global.r4_gun.SetActive(false);
                    }
                }
                if ((((((((Global.someDropableObject["object"] as Hashtable)["type"] as string == "armor") 
                    || ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "helm")) 
                    || ((Global.someDropableObject["object"] as Hashtable)["type"] as string== "gloves")) 
                    || ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "boots")) 
                    || ((Global.someDropableObject["object"] as Hashtable)["type"] as string== "amulet")) 
                    || ((Global.someDropableObject["object"] as Hashtable)["type"] as string == "ring")) 
                    || ((Global.someDropableObject["object"] as Hashtable)["type"] as string== "backpack"))
                {
                    if (Global.someDropableObject["thingname"] as string == "thing_r3_backpack")
                    {
                        Global._isR3Backpack = false;
                        Global.inv_r3_backpack.SetActive(false);
                        Global.r3_backpack.SetActive(false);
                    }
                    if (Global.someDropableObject["thingname"] as string == "thing_r5_backpack")
                    {
                        Global._isR5Backpack = false;
                        Global.inv_r5_backpack.SetActive(false);
                        Global.r5_backpack.SetActive(false);
                    }
                    if (Global.someDropableObject["thingname"] as string == "thing_r4_body")
                    {
                        Global._isR3Body = true;
                        Global._isR4Body = false;
                        Global.inv_r3_body.SetActive(true);
                        Global.inv_r4_body.SetActive(false);
                        Global.r3_body.SetActive(true);
                        Global.r4_body.SetActive(false);
                    }

                    if (Global.someDropableObject["thingname"] as string == "thing_r4_hand")
                    {
                        Global._isR3Hand = true;
                        Global._isR4Hand = false;
                        Global.inv_r3_hand.SetActive(true);
                        Global.inv_r4_hand.SetActive(false);
                        Global.r3_hand.SetActive(true);
                        Global.r4_hand.SetActive(false);
                    }
                    if (Global.someDropableObject["thingname"] as string == "thing_r4_leg")
                    {
                        Global._isR3Leg = true;
                        Global._isR4Leg = false;
                        Global.inv_r3_leg.SetActive(true);
                        Global.inv_r4_leg.SetActive(false);
                        Global.r3_leg.SetActive(true);
                        Global.r4_leg.SetActive(false);
                    }
                    if (!isDrop)
                    {
                        Global._system_CharacterParams["armor"] = ((int) Global._system_CharacterParams["armor"]) - removeArmor;
                        Global._userMaxHealth = Global._userMaxHealth - removeHealth;
                        if (Global._userCurrentHealth > Global._userMaxHealth)
                        {
                            Global._userCurrentHealth = Global._userMaxHealth;
                        }
                        Global._userCurrentHealth = Global._userMaxHealth - minusHealth;
                        isDrop = true;
                    }
                }
            }
        }

        (Global.someDropableObject["object"] as Hashtable)["level"] = "1";
        Hashtable someCopyInvThing = Global.cloneHashtable((Hashtable) Global.someDropableObject["object"]);
        someCopyInvThing["x"] = oldcoord["x"];
        someCopyInvThing["y"] = oldcoord["y"];
        someCopyInvThing.Remove("position");
        Global.system_dummy.Remove(Global.someDropableObject["object"]);
        Global.system_dummy.Remove(Global.someDropableObject["type"]);
        Global.globalInventoryThingsArr.Add(someCopyInvThing);
        (Global.someDropableObject["object"] as Hashtable)["position"] = "unknown";
    }

    private void setSomeBetweenDummyPlace(string placeStringFrom, string placeStringTo)
    {
        foreach (DictionaryEntry oneEntry in Global.system_dummy)
        {
            if ((oneEntry.Value as Hashtable)["position"] as string == placeStringTo)
            {
                Global.someDropableObjectBetween = (Hashtable) oneEntry.Value;
                Hashtable someCopyDummyThing = Global.cloneHashtable((Hashtable) oneEntry.Value);
                Global.concatHashtable((Hashtable) oneEntry.Value, (Hashtable) Global.someDropableObject["object"]);
                (oneEntry.Value as Hashtable)["position"] = placeStringFrom;
                someCopyDummyThing["position"] = placeStringTo;
                Global.concatHashtable((Hashtable) Global.someDropableObject["object"], someCopyDummyThing);
                return;
            }
        }
        (Global.someDropableObject["object"] as Hashtable)["position"] = placeStringTo;
    }

    public virtual void c_GameController_CheckConsumables()
    {
        int z = 0;
        while (z < Global.globalInventoryThingsArr.Count)
        {
            foreach (DictionaryEntry loots in Global._userResourcesFood)
            {
                if ((int)(loots.Value as Hashtable)["amount"] == 0)
                {
                    Global.globalInventoryThingsArr.Remove(Global.globalInventoryThingsArr[z]);
                }
            }
            z++;
        }
        int w = 0;
        while (w < Global.globalInventoryThingsArr.Count)
        {
            foreach (DictionaryEntry iloots in Global._userResourcesCraft)
            {
                if ((int)(iloots.Value as Hashtable)["amount"] == 0)
                {
                    Global.globalInventoryThingsArr.Remove(Global.globalInventoryThingsArr[w]);
                }
            }
            w++;
        }
        if (Global.globalInventoryThingsArr.Count > 0)
        {
            foreach (DictionaryEntry lootz in Global._userResourcesFood)
            {
                int y = 0;
                while (y < Global.globalInventoryThingsArr.Count)
                {
                    if ((Global.globalInventoryThingsArr[y]["thingname"] == lootz.Key) && ((lootz.Value as Hashtable)["amount"] as string == "0"))
                    {
                        Global.globalInventoryThingsArr.Remove(Global.globalInventoryThingsArr[y]);
                    }
                    y++;
                }
            }
            foreach (DictionaryEntry ilootz in Global._userResourcesCraft)
            {
                int q = 0;
                while (q < Global.globalInventoryThingsArr.Count)
                {
                    if ((Global.globalInventoryThingsArr[q]["thingname"] == ilootz.Key) && ((ilootz.Value as Hashtable)["amount"] as string == "0"))
                    {
                        Global.globalInventoryThingsArr.Remove(Global.globalInventoryThingsArr[q]);
                    }
                    q++;
                }
            }
            int i = 0;
            while (i < Global.globalInventoryThingsArr.Count)
            {
                if (Global._userResourcesFood.ContainsKey(Global.globalInventoryThingsArr[i]["thingname"]) 
                    && !(Global.globalInventoryThingsArr[i]["val"] as string == "0"))
                {
                    Global.globalInventoryThingsArr[i]["val"] = (Global._userResourcesFood[Global.globalInventoryThingsArr[i]["thingname"]] as Hashtable)["amount"].ToString();
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_banana")
                    {
                        Global._isFoodBanana = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_berries")
                    {
                        Global._isFoodBerries = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_cabbage")
                    {
                        Global._isFoodCabbage = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_carrot")
                    {
                        Global._isFoodCarrot = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_chicken")
                    {
                        Global._isFoodChicken = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_egg")
                    {
                        Global._isFoodEgg = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_fish")
                    {
                        Global._isFoodFish = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_flour")
                    {
                        Global._isFoodFlour = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_milk")
                    {
                        Global._isFoodMilk = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_mushrooms")
                    {
                        Global._isFoodMushrooms = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_onion")
                    {
                        Global._isFoodOnion = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_orange")
                    {
                        Global._isFoodOrange = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_potatoes")
                    {
                        Global._isFoodPotatoes = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_sourcream")
                    {
                        Global._isFoodSourcream = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_spice")
                    {
                        Global._isFoodSpice = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_beans")
                    {
                        Global._isFoodBeans = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_chickpea")
                    {
                        Global._isFoodChickpea = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_corn")
                    {
                        Global._isFoodCorn = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_kidney_beans")
                    {
                        Global._isFoodKidneyBeans = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_lentils")
                    {
                        Global._isFoodLentils = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_peanut")
                    {
                        Global._isFoodPeanut = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "food_peas")
                    {
                        Global._isFoodPeas = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["val"] as string == "0")
                    {
                        Global.globalInventoryThingsArr.Remove(Global.globalInventoryThingsArr[i]);
                    }
                }
                if (((i == (Global.globalInventoryThingsArr.Count - 1)) && Global._userResourcesFood.ContainsKey(Global.globalInventoryThingsArr[i]["thingname"])) || (i == (Global.globalInventoryThingsArr.Count - 1)))
                {
                    foreach (DictionaryEntry loot in Global._userResourcesFood)
                    {
                        Global._currentResLootName = (string) loot.Key;
                        Global._currentResLootVal = (string) (loot.Value as Hashtable)["amount"];
                        if (((loot.Key as string == "food_banana") && (Global._currentResLootVal != "0")) && !Global._isFoodBanana)
                        {
                            Global._currentResLootIcon = this.gs_common.gui_icons_food_banana;
                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                        }
                        else
                        {
                            if (((loot.Key as string == "food_berries") && (Global._currentResLootVal != "0")) && !Global._isFoodBerries)
                            {
                                Global._currentResLootIcon = this.gs_common.gui_icons_food_berries;
                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                            }
                            else
                            {
                                if (((loot.Key as string == "food_cabbage") && (Global._currentResLootVal != "0")) && !Global._isFoodCabbage)
                                {
                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_cabbage;
                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                }
                                else
                                {
                                    if (((loot.Key as string == "food_carrot") && (Global._currentResLootVal != "0")) && !Global._isFoodCarrot)
                                    {
                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_carrot;
                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                    }
                                    else
                                    {
                                        if (((loot.Key as string == "food_chicken") && (Global._currentResLootVal != "0")) && !Global._isFoodChicken)
                                        {
                                            Global._currentResLootIcon = this.gs_common.gui_icons_food_chicken;
                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                        }
                                        else
                                        {
                                            if (((loot.Key as string == "food_egg") && (Global._currentResLootVal != "0")) && !Global._isFoodEgg)
                                            {
                                                Global._currentResLootIcon = this.gs_common.gui_icons_food_egg;
                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                            }
                                            else
                                            {
                                                if (((loot.Key as string == "food_fish") && (Global._currentResLootVal != "0")) && !Global._isFoodFish)
                                                {
                                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_fish;
                                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                }
                                                else
                                                {
                                                    if (((loot.Key as string == "food_flour") && (Global._currentResLootVal != "0")) && !Global._isFoodFlour)
                                                    {
                                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_flour;
                                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                    }
                                                    else
                                                    {
                                                        if (((loot.Key as string == "food_milk") && (Global._currentResLootVal != "0")) && !Global._isFoodMilk)
                                                        {
                                                            Global._currentResLootIcon = this.gs_common.gui_icons_food_milk;
                                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                        }
                                                        else
                                                        {
                                                            if (((loot.Key as string == "food_mushrooms") && (Global._currentResLootVal != "0")) && !Global._isFoodMushrooms)
                                                            {
                                                                Global._currentResLootIcon = this.gs_common.gui_icons_food_mushrooms;
                                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                            }
                                                            else
                                                            {
                                                                if (((loot.Key as string == "food_onion") && (Global._currentResLootVal != "0")) && !Global._isFoodOnion)
                                                                {
                                                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_onion;
                                                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                }
                                                                else
                                                                {
                                                                    if (((loot.Key as string == "food_orange") && (Global._currentResLootVal != "0")) && !Global._isFoodOrange)
                                                                    {
                                                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_orange;
                                                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                    }
                                                                    else
                                                                    {
                                                                        if (((loot.Key as string == "food_potatoes") && (Global._currentResLootVal != "0")) && !Global._isFoodPotatoes)
                                                                        {
                                                                            Global._currentResLootIcon = this.gs_common.gui_icons_food_potatoes;
                                                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                        }
                                                                        else
                                                                        {
                                                                            if (((loot.Key as string == "food_sourcream") && (Global._currentResLootVal != "0")) && !Global._isFoodSourcream)
                                                                            {
                                                                                Global._currentResLootIcon = this.gs_common.gui_icons_food_sourcream;
                                                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                            }
                                                                            else
                                                                            {
                                                                                if (((loot.Key as string == "food_spice") && (Global._currentResLootVal != "0")) && !Global._isFoodSpice)
                                                                                {
                                                                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_spice;
                                                                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (((loot.Key as string == "food_beans") && (Global._currentResLootVal != "0")) && !Global._isFoodBeans)
                                                                                    {
                                                                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_beans;
                                                                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (((loot.Key as string == "food_chickpea") && (Global._currentResLootVal != "0")) && !Global._isFoodChickpea)
                                                                                        {
                                                                                            Global._currentResLootIcon = this.gs_common.gui_icons_food_chickpea;
                                                                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (((loot.Key as string == "food_corn") && (Global._currentResLootVal != "0")) && !Global._isFoodCorn)
                                                                                            {
                                                                                                Global._currentResLootIcon = this.gs_common.gui_icons_food_corn;
                                                                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (((loot.Key as string == "food_kidney_beans") && (Global._currentResLootVal != "0")) && !Global._isFoodKidneyBeans)
                                                                                                {
                                                                                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_kidney_beans;
                                                                                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (((loot.Key as string == "food_lentils") && (Global._currentResLootVal != "0")) && !Global._isFoodLentils)
                                                                                                    {
                                                                                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_lentils;
                                                                                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (((loot.Key as string == "food_peanut") && (Global._currentResLootVal != "0")) && !Global._isFoodPeanut)
                                                                                                        {
                                                                                                            Global._currentResLootIcon = this.gs_common.gui_icons_food_peanut;
                                                                                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (((loot.Key as string == "food_peas") && (Global._currentResLootVal != "0")) && !Global._isFoodPeas)
                                                                                                            {
                                                                                                                Global._currentResLootIcon = this.gs_common.gui_icons_food_peas;
                                                                                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //КРАФТ
                if (Global._userResourcesCraft.ContainsKey(Global.globalInventoryThingsArr[i]["thingname"]) && !(Global.globalInventoryThingsArr[i]["val"] as string == "0"))
                {
                    Global.globalInventoryThingsArr[i]["val"] = (Global._userResourcesCraft[Global.globalInventoryThingsArr[i]["thingname"]] as Hashtable)["amount"].ToString();
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "loot_metal")
                    {
                        Global._isLootMetal = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "loot_plastic")
                    {
                        Global._isLootPlastic = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "loot_cobweb")
                    {
                        Global._isLootCobweb = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "loot_deadspider")
                    {
                        Global._isLootDeadspider = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "loot_eballon")
                    {
                        Global._isLootEballon = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["thingname"] as string == "loot_cactus")
                    {
                        Global._isLootCactus = true;
                    }
                    if (Global.globalInventoryThingsArr[i]["val"] as string == "0")
                    {
                        Global.globalInventoryThingsArr.Remove(Global.globalInventoryThingsArr[i]);
                    }
                }
                if (((i == (Global.globalInventoryThingsArr.Count - 1)) && Global._userResourcesCraft.ContainsKey(Global.globalInventoryThingsArr[i]["thingname"])) || (i == (Global.globalInventoryThingsArr.Count - 1)))
                {
                    foreach (DictionaryEntry iloot in Global._userResourcesCraft)
                    {
                        Global._currentResLootName = (string) iloot.Key;
                        Global._currentResLootVal = (string) (iloot.Value as Hashtable)["amount"];
                        if (((iloot.Key as string == "loot_metal") && (Global._currentResLootVal != "0")) && !Global._isLootMetal)
                        {
                            Global._currentResLootIcon = this.gs_common.gui_icons_loot_metal;
                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                        }
                        else
                        {
                            if (((iloot.Key as string == "loot_plastic") && (Global._currentResLootVal != "0")) && !Global._isLootPlastic)
                            {
                                Global._currentResLootIcon = this.gs_common.gui_icons_loot_plastic;
                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                            }
                            else
                            {
                                if (((iloot.Key as string == "loot_cobweb") && (Global._currentResLootVal != "0")) && !Global._isLootCobweb)
                                {
                                    Global._currentResLootIcon = this.gs_common.gui_icons_loot_cobweb;
                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                }
                                else
                                {
                                    if (((iloot.Key as string == "loot_deadspider") && (Global._currentResLootVal != "0")) && !Global._isLootDeadspider)
                                    {
                                        Global._currentResLootIcon = this.gs_common.gui_icons_loot_deadspider;
                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                    }
                                    else
                                    {
                                        if (((iloot.Key as string == "loot_eballon") && (Global._currentResLootVal != "0")) && !Global._isLootEballon)
                                        {
                                            Global._currentResLootIcon = this.gs_common.gui_icons_loot_eballon;
                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                        }
                                        else
                                        {
                                            if (((iloot.Key as string == "loot_cactus") && (Global._currentResLootVal != "0")) && !Global._isLootCactus)
                                            {
                                                Global._currentResLootIcon = this.gs_common.gui_icons_loot_cactus;
                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", Global._currentResLootVal },  {"name", "loot" },  {"selected", "false" }, }));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                }
                i++;
            }
        }
        else
        {
            foreach (DictionaryEntry loot in Global._userResourcesFood)
            {
                Global._currentResLootName = (string) loot.Key;
                if ((loot.Key as string == "food_banana") && (Global._currentResLootVal != "0"))
                {
                    Global._currentResLootIcon = this.gs_common.gui_icons_food_banana;
                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                }
                else
                {
                    if ((loot.Key as string == "food_berries") && (Global._currentResLootVal != "0"))
                    {
                        Global._currentResLootIcon = this.gs_common.gui_icons_food_berries;
                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                    }
                    else
                    {
                        if ((loot.Key as string == "food_cabbage") && (Global._currentResLootVal != "0"))
                        {
                            Global._currentResLootIcon = this.gs_common.gui_icons_food_cabbage;
                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                        }
                        else
                        {
                            if ((loot.Key as string == "food_carrot") && (Global._currentResLootVal != "0"))
                            {
                                Global._currentResLootIcon = this.gs_common.gui_icons_food_carrot;
                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                            }
                            else
                            {
                                if ((loot.Key as string == "food_chicken") && (Global._currentResLootVal != "0"))
                                {
                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_chicken;
                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                }
                                else
                                {
                                    if ((loot.Key as string == "food_egg") && (Global._currentResLootVal != "0"))
                                    {
                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_egg;
                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                    }
                                    else
                                    {
                                        if ((loot.Key as string == "food_fish") && (Global._currentResLootVal != "0"))
                                        {
                                            Global._currentResLootIcon = this.gs_common.gui_icons_food_fish;
                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                        }
                                        else
                                        {
                                            if ((loot.Key as string == "food_flour") && (Global._currentResLootVal != "0"))
                                            {
                                                Global._currentResLootIcon = this.gs_common.gui_icons_food_flour;
                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                            }
                                            else
                                            {
                                                if ((loot.Key as string == "food_milk") && (Global._currentResLootVal != "0"))
                                                {
                                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_milk;
                                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                }
                                                else
                                                {
                                                    if ((loot.Key as string == "food_mushrooms") && (Global._currentResLootVal != "0"))
                                                    {
                                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_mushrooms;
                                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                    }
                                                    else
                                                    {
                                                        if ((loot.Key as string == "food_onion") && (Global._currentResLootVal != "0"))
                                                        {
                                                            Global._currentResLootIcon = this.gs_common.gui_icons_food_onion;
                                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                        }
                                                        else
                                                        {
                                                            if ((loot.Key as string == "food_orange") && (Global._currentResLootVal != "0"))
                                                            {
                                                                Global._currentResLootIcon = this.gs_common.gui_icons_food_orange;
                                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                            }
                                                            else
                                                            {
                                                                if ((loot.Key as string == "food_potatoes") && (Global._currentResLootVal != "0"))
                                                                {
                                                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_potatoes;
                                                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                }
                                                                else
                                                                {
                                                                    if ((loot.Key as string == "food_sourcream") && (Global._currentResLootVal != "0"))
                                                                    {
                                                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_sourcream;
                                                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                    }
                                                                    else
                                                                    {
                                                                        if ((loot.Key as string == "food_spice") && (Global._currentResLootVal != "0"))
                                                                        {
                                                                            Global._currentResLootIcon = this.gs_common.gui_icons_food_spice;
                                                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                        }
                                                                        else
                                                                        {
                                                                            if ((loot.Key as string == "food_beans") && (Global._currentResLootVal != "0"))
                                                                            {
                                                                                Global._currentResLootIcon = this.gs_common.gui_icons_food_beans;
                                                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                            }
                                                                            else
                                                                            {
                                                                                if ((loot.Key as string == "food_chickpea") && (Global._currentResLootVal != "0"))
                                                                                {
                                                                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_chickpea;
                                                                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                }
                                                                                else
                                                                                {
                                                                                    if ((loot.Key as string == "food_corn") && (Global._currentResLootVal != "0"))
                                                                                    {
                                                                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_corn;
                                                                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if ((loot.Key as string == "food_kidney_beans") && (Global._currentResLootVal != "0"))
                                                                                        {
                                                                                            Global._currentResLootIcon = this.gs_common.gui_icons_food_kidney_beans;
                                                                                            Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if ((loot.Key as string == "food_lentils") && (Global._currentResLootVal != "0"))
                                                                                            {
                                                                                                Global._currentResLootIcon = this.gs_common.gui_icons_food_lentils;
                                                                                                Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if ((loot.Key as string == "food_peanut") && (Global._currentResLootVal != "0"))
                                                                                                {
                                                                                                    Global._currentResLootIcon = this.gs_common.gui_icons_food_peanut;
                                                                                                    Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if ((loot.Key as string == "food_peas") && (Global._currentResLootVal != "0"))
                                                                                                    {
                                                                                                        Global._currentResLootIcon = this.gs_common.gui_icons_food_peas;
                                                                                                        Global.globalInventoryThingsArr.Add(new Hashtable(new Hashtable() { {"droptype", "thing" },  {"type", "loot" },  {"thingname", Global._currentResLootName },  {"icon", Global._currentResLootIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "-1" },  {"y", "-1" },  {"uniqueID", ((("loot_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString() },  {"vid", "1" },  {"val", "1" },  {"name", "loot" },  {"selected", "false" }, }));
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                break;
            }
        }
        Global._isFoodBanana = false;
        Global._isFoodBerries = false;
        Global._isFoodCabbage = false;
        Global._isFoodCarrot = false;
        Global._isFoodChicken = false;
        Global._isFoodEgg = false;
        Global._isFoodFish = false;
        Global._isFoodFlour = false;
        Global._isFoodMilk = false;
        Global._isFoodMushrooms = false;
        Global._isFoodOnion = false;
        Global._isFoodOrange = false;
        Global._isFoodPotatoes = false;
        Global._isFoodSourcream = false;
        Global._isFoodSpice = false;
        Global._isFoodBeans = false;
        Global._isFoodChickpea = false;
        Global._isFoodCorn = false;
        Global._isFoodKidneyBeans = false;
        Global._isFoodLentils = false;
        Global._isFoodPeanut = false;
        Global._isFoodPeas = false;
        Global._isLootMetal = false;
        Global._isLootPlastic = false;
        Global._isLootCobweb = false;
        Global._isLootDeadspider = false;
        Global._isLootEballon = false;
        Global._isLootCactus = false;
        Global.globalBus.SendMessage("c_GameController_Base_command_SortThings");
    }

    public virtual IEnumerator c_GameController_command_Paralizator(GameObject whom)
    {
        float reactionRadius_temp = 0.0f;
        reactionRadius_temp = ((s_AIController) whom.GetComponent(typeof(s_AIController))).srv_reactionradius;
        if (whom != null)
        {
            ((s_AIController) whom.GetComponent(typeof(s_AIController))).paraliticModel.SetActive(true);
            ((s_AIController) whom.GetComponent(typeof(s_AIController))).srv_reactionradius = 0;
            yield return new WaitForSeconds(5f);
            ((s_AIController) whom.GetComponent(typeof(s_AIController))).srv_reactionradius = reactionRadius_temp;
            ((s_AIController) whom.GetComponent(typeof(s_AIController))).paraliticModel.SetActive(false);
        }
    }

}