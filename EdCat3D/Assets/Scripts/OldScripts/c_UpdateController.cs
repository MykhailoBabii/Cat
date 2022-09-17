using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public partial class c_UpdateController : MonoBehaviour
{
    /*****************************************************************************************
* Main Update Controller (and for raycast stuff too)
* 
* @author N-Game Studio Ltd. 
* Research & Development	
* 
*****************************************************************************************/
    private float guiRatio;
    public LayerMask layerMaskSquare;
    public LayerMask layerMaskHeroMoveSquare;
    public float lastRemoteMeleeAttackDistanceTime;
    public float lastPreventDoubleCollideMouseTime;
    public float lastGiveEnergyTime;
    public float lastDeathTimeOutTime;
    private bool _hasAttackSomeOne;
    private CharacterMotor characterMotor;
    public UnityEngine.AI.NavMeshAgent _agent;
    private UnityEngine.AI.NavMeshHit NavHit;
    private bool NavMove;
    private GameObject _lightObj;
    public Transform gunBone;
    public GameObject blasterFire;
    public GameObject blasterProjectile;
    public GameObject blasterExplosion;
    public GameObject bigGunFire;
    public GameObject bigGunProjectile;
    public GameObject bigGunExplosion;
    public GameObject fistSparkle;
    public GameObject paralizatorFire;
    public GameObject paralizatorExplosion;
    private bool isClickOnMob;
    private bool isClickOnCobweb;
    private bool isStorage;
    private int randomAttack;

    private Rect correctedRect;

    public virtual void Start()
    {
        this.lastRemoteMeleeAttackDistanceTime = Time.time;
        this.lastPreventDoubleCollideMouseTime = Time.time;
    }

    private bool notInterface(Vector3 mv)
    {
        if ((new Rect((Screen.width / 2) - (681 * this.guiRatio), Screen.height - (138 * this.guiRatio), 1363 * this.guiRatio, 138 * this.guiRatio).Contains(mv) || new Rect(Screen.width - (307 * this.guiRatio), 17 * this.guiRatio, 301 * this.guiRatio, 280 * this.guiRatio).Contains(mv)) || new Rect(Screen.width - (340 * this.guiRatio), 74 * this.guiRatio, 328 * this.guiRatio, 104 * this.guiRatio).Contains(mv))
        {
            return false;
        }
        if (!Global._isPopUpOpen && !Global._isBattleRagesOn)
        {
            if (new Rect(0, Screen.height - (144 * Global.guiRatio), 1920 * Global.guiRatio, 144 * Global.guiRatio).Contains(mv))
            {
                return false;
            }
        }
        if (Global._isPopUpOpen)
        {
            if (new Rect(0, 0, Screen.width, Screen.height).Contains(mv))
            {
                return false;
            }
        }
        if (Global._isBattleRagesOn)
        {
            if (new Rect((Screen.width / 2) - (940 * Global.guiRatio), Screen.height - (118 * Global.guiRatio), 633 * Global.guiRatio, 98 * Global.guiRatio).Contains(mv))
            {
                return false;
            }
        }
        return true;
    }

    private bool isPointInRect(Rect place, Vector2 mv)
    {
        if (place.Contains(mv))
        {
            return true;
        }
        return false;
    }

    public virtual void LateUpdate()
    {
        RaycastHit hitL = default(RaycastHit);
        RaycastHit hitR = default(RaycastHit);
        if (!Global.isGameLoaded)
        {
            return;
        }
        Global.guiRatio = (Screen.width / 1920f);
        Global._game_VolumeSound();
        Global._game_VolumeMusic();
        if (Global._isPopUpOpen)
        {
            Global._system_isHeroDead = true;
            Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }
        else
        {
            Global._system_isHeroDead = false;
            Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
        if (Global._options_sound_volume > 0)
        {
            Global._game_OnSound();
        }
        if (Global._options_music_volume > 0)
        {
            Global._game_OnMusic();
        }
        if (Global._options_sound_volume <= 0)
        {
            Global._game_OffSound();
        }
        if (Global._options_music_volume <= 0)
        {
            Global._game_OffMusic();
        }
        Vector3 mPos = Input.mousePosition;
        Global.gmPos = new Vector2(mPos.x, Screen.height - mPos.y);
        if (((mPos.x >= 0) && (mPos.x <= Screen.width)) && ((mPos.y >= 0) && (mPos.y <= Screen.height)))
        {
            if (Camera.main == null)
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(mPos);
            Vector3 newPosCheck = mPos;
            newPosCheck.y = Screen.height - newPosCheck.y;
            if (Global.isGameLoaded && this.notInterface(newPosCheck))
            {
                 //*************** MOVING BY KEYBOARD
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Vector3 position = Global._hero_dolly.transform.position;
                    position.x--;
                    Global._isBegun = true;
                    Global._heroTargetPoint = position;
                    Global._isAttackMob = false;
                    Global._isMoveAllowed = true;
                }
                //**********************************
                foreach (DictionaryEntry oneNpcToon in Global._npcsToonsScripts)
                {
                    GameObject crossGObjectExit = GameObject.Find(oneNpcToon.Key.ToString());
                    if ((crossGObjectExit != null) && crossGObjectExit.CompareTag("MONSTER")) //Убрали мышку с монстра
                    {
                        ((s_CreatureHoverToon) crossGObjectExit.transform.Find("character").GetComponent(typeof(s_CreatureHoverToon))).mouseExitOnIt();
                    }
                }
                if (Physics.Raycast(ray, out hitR, 100, this.layerMaskSquare.value))
                {
                    Vector3 crossPointEnter = hitR.point;
                    GameObject crossGObjectEnter = hitR.transform.gameObject;
                    if (!Global._isPopUpOpen)
                    {
                        Global._isClickRes = true;
                    }
                    if (crossGObjectEnter.CompareTag("MONSTER"))
                    {
                        ((s_CreatureHoverToon) crossGObjectEnter.GetComponent(typeof(s_CreatureHoverToon))).mouseEnterOnIt();
                    }
                    if (crossGObjectEnter.CompareTag("DROP"))
                    {
                        ((s_DropHoverToon) crossGObjectEnter.transform.GetComponent(typeof(s_DropHoverToon))).mouseEnterOnIt();
                    }
                    if (crossGObjectEnter.CompareTag("VEGETABLE"))
                    {
                        ((s_DropHoverToon) crossGObjectEnter.transform.GetComponent(typeof(s_DropHoverToon))).mouseEnterOnIt();
                    }
                    if (crossGObjectEnter.CompareTag("KEY"))
                    {
                        ((s_DropHoverToon) crossGObjectEnter.transform.GetComponent(typeof(s_DropHoverToon))).mouseEnterOnIt();
                    }
                    if (crossGObjectEnter.CompareTag("ANIMAL") && ((s_AnimalController) crossGObjectEnter.GetComponent(typeof(s_AnimalController))).isActive)
                    {
                        ((s_DropHoverToon) crossGObjectEnter.transform.GetComponent(typeof(s_DropHoverToon))).mouseEnterOnIt();
                    }
                }
                else
                {
                    Global._GAME_CURSOR = "default";
                }
                if ((Input.GetMouseButtonUp(0) && Global._isClickDlg) && !Global._isPopUpOpen)
                {
                    if (Physics.Raycast(ray, out hitR, 100, this.layerMaskSquare.value))
                    {
                        Vector3 crossPoint = hitR.point;
                        GameObject crossGObject = hitR.transform.gameObject;
                        float someDistance = Vector3.Distance(Global._hero_dolly.transform.position, crossGObject.transform.position);
                        if (crossGObject.CompareTag("PIER"))
                        {
                            Global._isRemotePierPlace = crossGObject;
                            Global._isRemotePier = true;
                            this.StartCoroutine(this.c_UpdateController_command_goToPier(crossGObject));
                            this.lastPreventDoubleCollideMouseTime = Time.time;
                        }
                        else
                        {
                            if (crossGObject.CompareTag("NPC"))
                            {
                                Global._isRemoteNPCPlace = crossGObject;
                                Global._isRemoteNPC = true;
                                this.StartCoroutine(this.c_UpdateController_command_goToNPC(crossGObject));
                                this.lastPreventDoubleCollideMouseTime = Time.time;
                            }
                            else
                            {
                                if (crossGObject.CompareTag("MONSTER"))
                                {
                                    this.isClickOnMob = true;
                                    Global._isAttackMob = true;
                                    Global._isBattleRagesOn = true;
                                    if ((Global._isAttackMob && (crossGObject != null)) && !((s_AIController) crossGObject.GetComponent(typeof(s_AIController))).isNpcDead)
                                    {
                                        Global._isRemoteMeleeAttackNPC = crossGObject;
                                        Global._isRemoteMeleeAttack = true;
                                        ((MeshRenderer) ((s_AIController) Global._isRemoteMeleeAttackNPC.GetComponent(typeof(s_AIController))).enemySelect.GetComponent(typeof(MeshRenderer))).enabled = true;
                                        this.lastPreventDoubleCollideMouseTime = Time.time;
                                    }
                                }
                                else
                                {
                                    if (crossGObject.CompareTag("DROP"))
                                    {
                                        if (Global._isRemoteMeleeAttackNPC != null)
                                        {
                                            ((MeshRenderer) ((s_AIController) Global._isRemoteMeleeAttackNPC.GetComponent(typeof(s_AIController))).enemySelect.GetComponent(typeof(MeshRenderer))).enabled = false;
                                        }
                                        Global._isTakeDrop = true;
                                        ((s_DropHoverToon) crossGObject.transform.GetComponent(typeof(s_DropHoverToon))).mouseDownOnIt();
                                        //V2
                                        if (Global._isTakeDrop && (crossGObject != null))
                                        {
                                            Global._isRemoteDropPlace = crossGObject;
                                            Global._isRemoteDrop = true;
                                            this.StartCoroutine(this.c_UpdateController_command_goToDrop(crossGObject));
                                            this.lastPreventDoubleCollideMouseTime = Time.time;
                                        }
                                    }
                                    else
                                    {
                                        if (crossGObject.CompareTag("VEGETABLE"))
                                        {
                                            if (Global._isRemoteMeleeAttackNPC != null)
                                            {
                                                ((MeshRenderer) ((s_AIController) Global._isRemoteMeleeAttackNPC.GetComponent(typeof(s_AIController))).enemySelect.GetComponent(typeof(MeshRenderer))).enabled = false;
                                            }
                                            Global._isTakeVegetable = true;
                                            if (Global._isTakeVegetable && (crossGObject != null))
                                            {
                                                Global._isRemoteVegetablePlace = crossGObject;
                                                Global._isRemoteVegetable = true;
                                                this.StartCoroutine(this.c_UpdateController_command_goToVegetable(crossGObject));
                                                this.lastPreventDoubleCollideMouseTime = Time.time;
                                            }
                                        }
                                        else
                                        {
                                            if (crossGObject.CompareTag("ANIMAL") && ((s_AnimalController) crossGObject.GetComponent(typeof(s_AnimalController))).isActive)
                                            {
                                                if (Global._isRemoteMeleeAttackNPC != null)
                                                {
                                                    ((MeshRenderer) ((s_AIController) Global._isRemoteMeleeAttackNPC.GetComponent(typeof(s_AIController))).enemySelect.GetComponent(typeof(MeshRenderer))).enabled = false;
                                                }
                                                Global._isTakeAnimal = true;
                                                if (Global._isTakeAnimal && (crossGObject != null))
                                                {
                                                    Global._isRemoteAnimalPlace = crossGObject;
                                                    Global._isRemoteAnimal = true;
                                                    this.StartCoroutine(this.c_UpdateController_command_goToAnimal(crossGObject));
                                                    this.lastPreventDoubleCollideMouseTime = Time.time;
                                                }
                                            }
                                            else
                                            {
                                                if (crossGObject.CompareTag("COBWEB"))
                                                {
                                                    this.isClickOnCobweb = true;
                                                    Global._isAttackCobweb = true;
                                                    if ((Global._isAttackCobweb && (crossGObject != null)) && Global._isR3Gun)
                                                    {
                                                        Global._isRemoteCobweb = crossGObject;
                                                        Global._isRemoteCobwebAttack = true;
                                                        Global.someDistanceHeroCobweb = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteCobweb.transform.position);
                                                        this.StartCoroutine(this.c_UpdateController_command_goToCobweb(crossGObject));
                                                        this.lastPreventDoubleCollideMouseTime = Time.time;
                                                    }
                                                }
                                                else
                                                {
                                                    if (crossGObject.CompareTag("KEY"))
                                                    {
                                                        if (Global._isRemoteMeleeAttackNPC != null)
                                                        {
                                                            ((MeshRenderer) ((s_AIController) Global._isRemoteMeleeAttackNPC.GetComponent(typeof(s_AIController))).enemySelect.GetComponent(typeof(MeshRenderer))).enabled = false;
                                                        }
                                                        Global._isTakeKey = true;
                                                        if (Global._isTakeKey && (crossGObject != null))
                                                        {
                                                            Global._isRemoteKeyPlace = crossGObject;
                                                            Global._isRemoteKey = true;
                                                            this.StartCoroutine(this.c_UpdateController_command_goToKey(crossGObject));
                                                            this.lastPreventDoubleCollideMouseTime = Time.time;
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
                else
                {
                    if (Input.GetMouseButton(0) && ((Time.time - this.lastPreventDoubleCollideMouseTime) > 0.5f))
                    {
                        Vector3 NavTemp = new Vector3();
                        if (Global._isRemoteMeleeAttackNPC != null)
                        {
                            ((MeshRenderer) ((s_AIController) Global._isRemoteMeleeAttackNPC.GetComponent(typeof(s_AIController))).enemySelect.GetComponent(typeof(MeshRenderer))).enabled = false;
                        }
                        if (Physics.Raycast(ray, out hitL, 100, this.layerMaskHeroMoveSquare.value) && !this._hasAttackSomeOne)
                        {
                            NavTemp = hitL.point;
                        }
                        this.NavMove = UnityEngine.AI.NavMesh.SamplePosition(NavTemp, out this.NavHit, 0.5f, -1);
                        if (this.NavMove)
                        {
                        }
                        else
                        {
                        }
                         //nothing
                        if (Physics.Raycast(ray, out hitR, 100, this.layerMaskSquare.value) && !this._hasAttackSomeOne)
                        {
                        }
                        else
                        {
                             //nothing
                            if (Physics.Raycast(ray, out hitL, 100, this.layerMaskHeroMoveSquare.value) && this.NavMove)
                            {
                                Global._isBegun = true;
                                Global._heroTargetPoint = hitL.point;
                                Global._isAttackMob = false;
                                Global._isMoveAllowed = true;
                                Global._isBattleRagesOn = false;
                                ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerAttack1");
                                ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerAttack2");
                            }
                            else
                            {
                                Global._isBegun = true;
                                Global._heroTargetPoint = hitL.point;
                                Global._isAttackMob = false;
                                Global._isMoveAllowed = true;
                                Global._isBattleRagesOn = false;
                                ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerAttack1");
                                ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerAttack2");
                            }
                        }
                    }
                }
            }
            else
            {
                if ((Global._isWeCanDragAndDrop && Global.isMouseHold) && !((Global.someDropableObject == null) == null))
                {
                    Global.someDropablePlace = this.isCanDropObjectToSomePlace();
                    if (Input.GetMouseButtonUp(0) && ((Time.realtimeSinceStartup - Global.lastMouseDownObjectForDrag) > 0.2f))
                    {
                        if ((bool)Global.someDropablePlace["isdropable"])
                        {
                            Global._isWeCanDragAndDrop = false;
                            Global.globalBus.SendMessage("c_GameController_Base_command_dropSomeObject");
                        }
                        else
                        {
                            if (!Global._isTutorial41 && Global._isTutorial42)
                            {
                            }
                            else
                            {
                                 //nothing
                                Global.globalBus.SendMessage("c_GameController_Base_command_cancelDropSomeObject");
                            }
                        }
                    }
                }
                else
                {
                    Global.someDropablePlace = this.isCanDropObjectToSomePlace();
                    if (Input.GetMouseButtonUp(0) && ((Time.realtimeSinceStartup - Global.lastMouseDownObjectForDrag) > 0.2f))
                    {
                        if ((bool)Global.someDropablePlace["isdropable"])
                        {
                            if (Global._isTutorialStorage && !Global._isTutorialInventory)
                            {
                            }
                            else
                            {
                                 //nothing
                                Global._isWeCanDragAndDrop = false;
                                Global.globalBus.SendMessage("c_GameController_Base_command_dropSomeObject");
                            }
                        }
                        else
                        {
                            if (!Global._isTutorial41 && Global._isTutorial42)
                            {
                            }
                            else
                            {
                                 //nothing
                                Global.globalBus.SendMessage("c_GameController_Base_command_cancelDropSomeObject");
                            }
                        }
                    }
                }
            }
        }
        if (((Time.time - this.lastRemoteMeleeAttackDistanceTime) > 0.2f) && Global._isRemoteDrop)
        {
            float someDistanceHeroDrop = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteDropPlace.transform.position);
            this.lastRemoteMeleeAttackDistanceTime = Time.time;
        }
        if (((Time.time - this.lastRemoteMeleeAttackDistanceTime) > 0.2f) && Global._isRemoteVegetable)
        {
            if (Global._isTakeVegetable && (Global._isRemoteVegetablePlace != null))
            {
                float someDistanceHeroVegetable = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteVegetablePlace.transform.position);
                this.StartCoroutine(this.c_UpdateController_command_goToVegetable(Global._isRemoteVegetablePlace));
            }
            this.lastRemoteMeleeAttackDistanceTime = Time.time;
        }
        if (((Time.time - this.lastRemoteMeleeAttackDistanceTime) > 0.2f) && Global._isRemoteAnimal)
        {
            if (Global._isTakeAnimal && (Global._isRemoteAnimalPlace != null))
            {
                float someDistanceHeroAnimal = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteAnimalPlace.transform.position);
                this.StartCoroutine(this.c_UpdateController_command_goToAnimal(Global._isRemoteAnimalPlace));
            }
            this.lastRemoteMeleeAttackDistanceTime = Time.time;
        }
        if (((Time.time - this.lastRemoteMeleeAttackDistanceTime) > 0.2f) && Global._isRemoteKey)
        {
            if (Global._isTakeKey && (Global._isRemoteKeyPlace != null))
            {
                float someDistanceHeroKey = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteKeyPlace.transform.position);
                this.StartCoroutine(this.c_UpdateController_command_goToKey(Global._isRemoteKeyPlace));
            }
            this.lastRemoteMeleeAttackDistanceTime = Time.time;
        }
        if (Global._dlgRun)
        {
            string _currentDlgLineName = Global._dlgID.ToString();
            Hashtable _currentDlgLine = (Hashtable) Global.dlgCurrent[_currentDlgLineName];
            Global._dlgSpeakerName = _currentDlgLine["name"].ToString();
            Global._dlgSpeaker = _currentDlgLine["speaker"].ToString();
            Global._dlgText = _currentDlgLine["text"].ToString();
            Global._dlgType = _currentDlgLine["type"].ToString();
            Global._dlgSpeakerImage = (string) _currentDlgLine["image"];
        }
        if ((((Global._isRemoteMeleeAttackNPC != null) && Global._isAttackMob) && !((s_AIController) Global._isRemoteMeleeAttackNPC.GetComponent(typeof(s_AIController))).isNpcDead) && Global._isBattleRagesOn)
        {
            Global.someDistanceHeroMob = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteMeleeAttackNPC.transform.position);
            if (Global.someDistanceHeroMob > (Global._system_distanceAttack + 1.5f))
            {
                this.StartCoroutine(this.c_UpdateController_command_goToMob(Global._isRemoteMeleeAttackNPC));
            }
            else
            {
                Global._isMoveAllowed = false;
                this.StartCoroutine(this.c_UpdateController_command_attackSomeSelectedCreature(Global._isRemoteMeleeAttackNPC));
            }
            if ((Global.someDistanceHeroMob <= Global._system_distanceAttack) && !Global._isGetHit)
            {
                Global._isUnderAttack = true;
                Global._isRemoteMeleeAttack = false;
                this.StartCoroutine(this.c_UpdateController_command_attackSomeSelectedCreature(Global._isRemoteMeleeAttackNPC));
            }
        }
        if (Global._isInventoryFull)
        {
            if (Global.labelFullInventoryAlpha > 0)
            {
                Global.labelFullInventoryAlpha = Global.labelFullInventoryAlpha - 0.015f;
            }
            else
            {
                Global._isInventoryFull = false;
                Global.labelFullInventoryAlpha = 1f;
            }
        }
        if (Global._isGetNewLevel)
        {
            if (Global.labelNewLevelAlpha > 0)
            {
                Global.labelNewLevelAlpha = Global.labelNewLevelAlpha - 0.005f;
            }
            else
            {
                Global._isGetNewLevel = false;
                Global.labelNewLevelAlpha = 1f;
            }
        }
        if (Global._isGameSaved)
        {
            if (Global.labelGameSavedAlpha > 0)
            {
                Global.labelGameSavedAlpha = Global.labelGameSavedAlpha - 0.005f;
            }
            else
            {
                Global._isGameSaved = false;
                Global.labelGameSavedAlpha = 1f;
            }
        }
        if (Global._isTakeLoot)
        {
            if (Global._selectedVegetable != null)
            {
                GameObject vegetableGO = Global._selectedVegetable;
                Vector3 vegetableGOCurrentPos = Camera.main.WorldToScreenPoint(new Vector3(vegetableGO.transform.position.x, vegetableGO.transform.position.y + 3, vegetableGO.transform.position.z));
                vegetableGOCurrentPos.y = Screen.height - vegetableGOCurrentPos.y;
                Global.labelLootPos = vegetableGOCurrentPos;
            }
            if (Global._selectedDropPlace != null)
            {
                GameObject dropGO = Global._selectedDropPlace;
                Vector3 dropGOCurrentPos = Camera.main.WorldToScreenPoint(new Vector3(dropGO.transform.position.x, dropGO.transform.position.y + 3, dropGO.transform.position.z));
                dropGOCurrentPos.y = Screen.height - dropGOCurrentPos.y;
                Global.labelLootPos = dropGOCurrentPos;
            }
            if (Global._selectedAnimal != null)
            {
                GameObject animalGO = Global._selectedAnimal;
                Vector3 animalGOCurrentPos = Camera.main.WorldToScreenPoint(new Vector3(animalGO.transform.position.x, animalGO.transform.position.y + 3, animalGO.transform.position.z));
                animalGOCurrentPos.y = Screen.height - animalGOCurrentPos.y;
                Global.labelLootPos = animalGOCurrentPos;
            }
            if (Global.labelLootAlpha > 0)
            {
                Global.labelLootPos.y = Global.labelLootPos.y - 1.5f;
                Global.labelLootAlpha = Global.labelLootAlpha - 0.015f;
            }
            else
            {
                Global._isTakeLoot = false;
                Global.labelLootAlpha = 1f;
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_goToPier(GameObject what) //nothing
    {
        float someDistanceHeroPier = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemotePierPlace.transform.position);
        float someTime = (someDistanceHeroPier * 1f) / 6f;
        Global._isBegun = true;
        Global._heroTargetPoint = Vector3.MoveTowards(Global._hero_dolly.transform.position, what.transform.position, someDistanceHeroPier - (Global._system_distanceSpeak - 0.5f));
        yield return new WaitForSeconds(someTime);
        if (Global._isRemotePierPlace != null)
        {
            someDistanceHeroPier = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemotePierPlace.transform.position);
            if (someDistanceHeroPier <= Global._system_distanceSpeak)
            {
                Global._isRemotePier = false;
                Global._isPopUpOpen = true;
                Global._gui_SetInterface("PierScreen");
            }
            else
            {
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_goToNPC(GameObject what) //nothing
    {
        float someDistanceHeroNPC = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteNPCPlace.transform.position);
        float someTime = (someDistanceHeroNPC * 1f) / 6f;
        Global._isBegun = true;
        Global._heroTargetPoint = Vector3.MoveTowards(Global._hero_dolly.transform.position, what.transform.position, someDistanceHeroNPC - (Global._system_distanceSpeak - 0.5f));
        yield return new WaitForSeconds(someTime);
        if (Global._isRemoteNPCPlace != null)
        {
            someDistanceHeroNPC = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteNPCPlace.transform.position);
            if (someDistanceHeroNPC <= Global._system_distanceSpeak)
            {
                int i = 0;
                while (i < ((s_NPCController) what.GetComponent(typeof(s_NPCController))).dlgCount)
                {
                    if (((s_NPCController) what.GetComponent(typeof(s_NPCController))).dlgIsActive[i] == true)
                    {
                        if (((s_NPCController) what.GetComponent(typeof(s_NPCController))).dlgTitle[i] == "nemo_dlg01")
                        {
                            Global._currentDlgName = "nemo_dlg01";
                            Global.dlgCurrent = Strings.nemo_dlg01;
                        }
                        if (((s_NPCController) what.GetComponent(typeof(s_NPCController))).dlgTitle[i] == "nemo_dlg02")
                        {
                            Global._currentDlgName = "nemo_dlg02";
                            Global.dlgCurrent = Strings.nemo_dlg02;
                        }
                        this.c_UpdateController_command_speakSomeSelectedCreature(Global.dlgCurrent);
                    }
                    i++;
                }
            }
            else
            {
            }
        }
    }

    public virtual void c_UpdateController_command_speakSomeSelectedCreature(Hashtable dialogue)
    {
        Global._isRemoteNPC = false;
        Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        Global._dlgRun = true;
        Global._gui_SetInterface("DialogueScreen");
        Global._dlgLength = dialogue.Count;
        string _currentDlgLineName = Global._dlgID.ToString();
        Hashtable _currentDlgLine = (Hashtable) Global.dlgCurrent[_currentDlgLineName];
    }

    public virtual IEnumerator c_UpdateController_command_goToMob(GameObject what)
    {
        float someTime = (Global.someDistanceHeroMob * 1f) / 6f;
        if (Global.someDistanceHeroMob > (Global._system_distanceAttack + 1.5f))
        {
            Global._isBegun = true;
            Global._isMoveAllowed = true;
            Global._heroTargetPoint = Vector3.MoveTowards(Global._hero_dolly.transform.position, what.transform.position, Global.someDistanceHeroMob - Global._system_distanceAttack);
            if (this._agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                this.transform.rotation = Quaternion.LookRotation(this._agent.velocity.normalized);
            }
            yield return new WaitForSeconds(someTime);
        }
        else
        {
            Global._isMoveAllowed = false;
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerRun");
            this.StartCoroutine(this.c_UpdateController_command_attackSomeSelectedCreature(what));
        }
        if (Global._isRemoteMeleeAttackNPC != null)
        {
            if ((Global.someDistanceHeroMob <= Global._system_distanceAttack) && !Global._isGetHit)
            {
                ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
                ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerRun");
                Global._isUnderAttack = true;
                Global._isRemoteMeleeAttack = false;
            }
            else
            {
                ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
                ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerRun");
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_attackSomeSelectedCreature(GameObject whom)
    {
        this.randomAttack = Random.Range(1, 5);
        ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
        ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerRun");
        if (whom != null)
        {
            GameObject spellFireGO = null;
            GameObject spellFlyGO = null;
            GameObject spellBumGO = null;
            Global._hero_dolly.transform.LookAt(new Vector3(whom.transform.position.x, Global._hero_dolly.transform.position.y, whom.transform.position.z));
            if (!((s_AIController) whom.GetComponent(typeof(s_AIController))).isNpcDead)
            {
                if (!this._hasAttackSomeOne)
                {
                    Vector3 MobPos = new Vector3(whom.transform.position.x, whom.transform.position.y, whom.transform.position.z);
                    Global.MobPos = MobPos;
                    this._hasAttackSomeOne = true;
                    Global._hasAttackSomeOneByHero = true;
                    if (((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character != null)
                    {
                        if (Global._isAttack01)
                        {
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerHit");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerEndBattle");
                            if (this.randomAttack == 1)
                            {
                                ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerAttack1");
                            }
                            else
                            {
                                if (this.randomAttack == 2)
                                {
                                    ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerAttack3");
                                }
                                else
                                {
                                    if (this.randomAttack == 3)
                                    {
                                        ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerAttack4");
                                    }
                                    else
                                    {
                                        if (this.randomAttack == 4)
                                        {
                                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerAttack5");
                                        }
                                    }
                                }
                            }
                            int damageHandMin = (int) Global._system_CharacterParams["damagemin"];
                            int damageHandMax = (int) Global._system_CharacterParams["damagemax"];
                            int heroHandAttack = Mathf.FloorToInt((float) Global._system_CharacterParams["attack"]);
                            int realHandDamage = Mathf.FloorToInt(Random.Range(damageHandMin, damageHandMax));
                            this.StartCoroutine(((s_AIController) whom.GetComponent(typeof(s_AIController))).s_AIController_command_makeDamageToCreature(heroHandAttack + realHandDamage, true));
                            spellBumGO = GameObject.Instantiate(this.fistSparkle, new Vector3(MobPos.x, MobPos.y + 1, MobPos.z), Quaternion.identity);
                            yield return new WaitForSeconds(0.8f);
                        }
                        if (Global._isAttack02)
                        {
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerRun");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerHit");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerEndBattle");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerAttack2");
                            yield return new WaitForSeconds(0.2f);
                            spellFireGO = GameObject.Instantiate(this.blasterFire, new Vector3(this.gunBone.position.x, this.gunBone.position.y, this.gunBone.position.z), Quaternion.identity);
                            spellFlyGO = GameObject.Instantiate(this.blasterProjectile, new Vector3(this.gunBone.position.x, this.gunBone.position.y, this.gunBone.position.z), Quaternion.identity);
                            iTween.MoveTo(spellFlyGO, new Hashtable() { {"x", MobPos.x },  {"y", MobPos.y + 1 },  {"z", MobPos.z },  {"time", 0.4f },  {"delay", 0 },  {"easetype", "easeOutQuad" }, });
                            int damageBlasterMinTEMP = (int) Global._system_CharacterParams["damagemin"];
                            int damageBlasterMaxTEMP = (int) Global._system_CharacterParams["damagemax"];
                            int damageBlasterMin = damageBlasterMinTEMP + 5;
                            int damageBlasterMax = damageBlasterMaxTEMP + 10;
                            int heroBlasterAttack = Mathf.FloorToInt((float) Global._system_CharacterParams["attack"]);
                            int realBlasterDamage = Mathf.FloorToInt(Random.Range(damageBlasterMin, damageBlasterMax));
                            this.StartCoroutine(((s_AIController) whom.GetComponent(typeof(s_AIController))).s_AIController_command_makeDamageToCreature(heroBlasterAttack + realBlasterDamage, true));
                            spellBumGO = GameObject.Instantiate(this.blasterExplosion, new Vector3(MobPos.x, MobPos.y + 1, MobPos.z), Quaternion.identity);
                            yield return new WaitForSeconds(0.4f);
                            if (spellFlyGO != null)
                            {
                                GameObject.Destroy(spellFlyGO);
                            }
                        }
                        if (Global._isAttack03)
                        {
                            Global._isAttackMob = false;
                            Global._isBattleRagesOn = false;
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerRun");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerHit");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerEndBattle");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerAttack2");
                            yield return new WaitForSeconds(0.05f);
                            spellFlyGO = GameObject.Instantiate(this.paralizatorFire, new Vector3(this.gunBone.position.x, this.gunBone.position.y, this.gunBone.position.z), Quaternion.identity);
                            spellFlyGO.transform.LookAt(new Vector3(MobPos.x, MobPos.y, MobPos.z));
                            iTween.MoveTo(spellFlyGO, new Hashtable() { {"x", MobPos.x },  {"y", MobPos.y + 1 },  {"z", MobPos.z },  {"time", 0.4f },  {"delay", 0 },  {"easetype", "easeOutQuad" }, });
                            /*var damageBlasterMinTEMP:int = Global._system_CharacterParams["damagemin"];
                        var damageBlasterMaxTEMP:int = Global._system_CharacterParams["damagemax"];

                        var damageBlasterMin:int = damageBlasterMinTEMP + 5;
                        var damageBlasterMax:int = damageBlasterMaxTEMP + 10;
                        var heroBlasterAttack:int = Mathf.FloorToInt(Global._system_CharacterParams["attack"]);
                        var realBlasterDamage:int = Mathf.FloorToInt(Random.Range(damageBlasterMin, damageBlasterMax));

                        whom.GetComponent(s_AIController).s_AIController_command_makeDamageToCreature(heroBlasterAttack + realBlasterDamage, true);*/
                            yield return new WaitForSeconds(0.25f);
                            Global.globalBus.SendMessage("c_GameController_command_Paralizator", whom);
                            spellBumGO = GameObject.Instantiate(this.paralizatorExplosion, new Vector3(MobPos.x, MobPos.y + 1, MobPos.z), Quaternion.identity);
                            if (spellFlyGO != null)
                            {
                                GameObject.Destroy(spellFlyGO);
                            }
                        }
                        if (Global._isAbility01)
                        {
                            Global._isAbility01Select = false;
                            Global.globalBus.SendMessage("c_GameController_Base_command_Cooldown01", 10f);
                            Global.globalBus.SendMessage("c_GameController_Base_command_Ability01");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerRun");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerHit");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerEndBattle");
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerAttack2");
                            yield return new WaitForSeconds(0.4f);
                            spellFireGO = GameObject.Instantiate(this.bigGunFire, new Vector3(this.gunBone.position.x, this.gunBone.position.y, this.gunBone.position.z), Quaternion.identity);
                            spellFlyGO = GameObject.Instantiate(this.bigGunProjectile, new Vector3(this.gunBone.position.x, this.gunBone.position.y, this.gunBone.position.z), Quaternion.identity);
                            iTween.MoveTo(spellFlyGO, new Hashtable() { {"x", MobPos.x },  {"y", MobPos.y + 1 },  {"z", MobPos.z },  {"time", 0.8f },  {"delay", 0 },  {"easetype", "easeOutQuad" }, });
                            /*Global._hero_dolly.GetComponent(s_Character).character.GetComponent(s_CreatureAnimationController).animationt.Rewind("attack");
                        Global._hero_dolly.GetComponent(s_Character).character.GetComponent(s_CreatureAnimationController).setAttackAnimation();
                        yield WaitForSeconds(0.8);
                        spellBumGO = GameObject.Instantiate(Global.gass.LoadAsset("spells_spellflashing_exp"), MobPos, Quaternion.identity);*/
                            //var damageW1Min:int = Global._system_CharacterParams["damagemin"];
                            //var damageW1Max:int = Global._system_CharacterParams["damagemax"];
                            //var realW1Damage:int = Mathf.FloorToInt(Random.Range(damageW1Min, damageW1Max));
                            int realW1Damage = Mathf.FloorToInt(Random.Range(50, 100));
                            //whom.GetComponent(s_AIController).s_AIController_command_makeDamageToCreature(Mathf.FloorToInt(realW1Damage*5), true);
                            this.StartCoroutine(((s_AIController) whom.GetComponent(typeof(s_AIController))).s_AIController_command_makeDamageToCreature(Mathf.FloorToInt(realW1Damage), true));
                            /*yield WaitForSeconds(0.15);
                        if (spellBumGO != null) GameObject.Destroy(spellBumGO);*/
                            spellBumGO = GameObject.Instantiate(this.bigGunExplosion, new Vector3(MobPos.x, MobPos.y + 1, MobPos.z), Quaternion.identity);
                            yield return new WaitForSeconds(0.4f);
                            if (spellFlyGO != null)
                            {
                                GameObject.Destroy(spellFlyGO);
                            }
                            Global._isAbility01 = false;
                            Global._system_distanceAttack = Global._TempDistance;
                        }
                    }
                    Global._hasAttackSomeOneByHero = false;
                    this._hasAttackSomeOne = false;
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_goToCobweb(GameObject what) //nothing
    {
        float someTime = (Global.someDistanceHeroMob * 1f) / 6f;
        if (Global.someDistanceHeroCobweb > (Global._system_distanceAttack + 1.5f))
        {
            Global._isBegun = true;
            Global._isMoveAllowed = true;
            Global._heroTargetPoint = Vector3.MoveTowards(Global._hero_dolly.transform.position, what.transform.position, Global.someDistanceHeroCobweb - 5f);
            if (this._agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                this.transform.rotation = Quaternion.LookRotation(this._agent.velocity.normalized);
            }
            yield return new WaitForSeconds(someTime);
        }
        else
        {
            Global._isMoveAllowed = false;
            this.StartCoroutine(this.c_UpdateController_command_destroyCobweb(what));
        }
        if (Global._isRemoteCobweb != null)
        {
            if (Global.someDistanceHeroCobweb <= Global._system_distanceAttack)
            {
                this.StartCoroutine(this.c_UpdateController_command_destroyCobweb(what));
            }
            else
            {
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_destroyCobweb(GameObject whom)
    {
        if (whom != null)
        {
            GameObject spellFireGO = null;
            GameObject spellFlyGO = null;
            GameObject spellBumGO = null;
            Vector3 MobPos = new Vector3(whom.transform.position.x, whom.transform.position.y, whom.transform.position.z);
            Global._hero_dolly.transform.LookAt(new Vector3(whom.transform.position.x, Global._hero_dolly.transform.position.y, whom.transform.position.z));
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerHit");
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerEndBattle");
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerAttack1");
            yield return new WaitForSeconds(0.4f);
            spellFireGO = GameObject.Instantiate(this.blasterFire, new Vector3(this.gunBone.position.x, this.gunBone.position.y, this.gunBone.position.z), Quaternion.identity);
            spellFlyGO = GameObject.Instantiate(this.blasterProjectile, new Vector3(this.gunBone.position.x, this.gunBone.position.y, this.gunBone.position.z), Quaternion.identity);
            iTween.MoveTo(spellFlyGO, new Hashtable() { {"x", MobPos.x },  {"y", MobPos.y + 1 },  {"z", MobPos.z },  {"time", 0.4f },  {"delay", 0 },  {"easetype", "easeOutQuad" }, });
            spellBumGO = GameObject.Instantiate(this.blasterExplosion, new Vector3(MobPos.x, MobPos.y + 1, MobPos.z), Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            if (spellFlyGO != null)
            {
                GameObject.Destroy(spellFlyGO);
            }
            yield return new WaitForSeconds(0.85f);
            GameObject.Destroy(whom);
            if (!Global._isTutorialDestroyCobweb)
            {
                Global._isTutorialDestroyCobweb = true;
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_goToDrop(GameObject what) //nothing
    {
        float someDistanceHeroDrop = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteDropPlace.transform.position);
        float someTime = (someDistanceHeroDrop * 1f) / 6f;
        bool playerRunAnim = false;
        Global._isBegun = true;
        Global._heroTargetPoint = Vector3.MoveTowards(Global._hero_dolly.transform.position, what.transform.position, someDistanceHeroDrop - (Global._system_distanceTakeDrop - 0.5f));
        if (!playerRunAnim)
        {
            playerRunAnim = true;
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerRun");
        }
        yield return new WaitForSeconds(someTime);
        if ((Global._isRemoteDropPlace != null) && !Global._isClickLoot)
        {
            someDistanceHeroDrop = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteDropPlace.transform.position);
            if (someDistanceHeroDrop <= Global._system_distanceTakeDrop)
            {
                Global.lootType = ((s_DropParams) Global._isRemoteDropPlace.transform.GetComponent(typeof(s_DropParams))).lootType;
                Global.VegetableAmount = ((s_DropParams) Global._isRemoteDropPlace.transform.GetComponent(typeof(s_DropParams))).lootAmount;
                int i = 0;
                while (i < Global.globalInventoryThingsArr.Count)
                {
                    if (Global.globalInventoryThingsArr[i]["thingname"] == Global.lootType)
                    {
                        Global._isRemoteDrop = false;
                        Global._isClickLoot = true;
                        this.StartCoroutine(this.c_UpdateController_command_TakeDrop(what));
                        break;
                    }
                    else
                    {
                        if (i == (Global.globalInventoryThingsArr.Count - 1))
                        {
                            if ((Global._isR3Backpack || Global._isR5Backpack) && (Global.globalInventoryThingsArr.Count < 24))
                            {
                                Global._isRemoteDrop = false;
                                Global._isClickLoot = true;
                                this.StartCoroutine(this.c_UpdateController_command_TakeDrop(what));
                            }
                            else
                            {
                                if ((!Global._isR3Backpack || !Global._isR5Backpack) && (Global.globalInventoryThingsArr.Count < 12))
                                {
                                    Global._isRemoteDrop = false;
                                    Global._isClickLoot = true;
                                    this.StartCoroutine(this.c_UpdateController_command_TakeDrop(what));
                                }
                                else
                                {
                                    Global._isInventoryFull = true;
                                    Global._isBegun = false;
                                    Global._heroTargetPoint = Global._hero_dolly.transform.position;
                                    Global._isRemoteDropPlace = null;
                                }
                            }
                        }
                    }
                    i++;
                }
            }
            else
            {
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_TakeDrop(GameObject what)
    {
        if (what != null)
        {
            Global._selectedDropPlace = what;
            Global._hero_dolly.transform.LookAt(new Vector3(what.transform.position.x, Global._hero_dolly.transform.position.y, what.transform.position.z));
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerTake");
            (Global._userResourcesCraft[Global.lootType] as Hashtable)["amount"] = (int.Parse((string) (Global._userResourcesCraft[Global.lootType] as Hashtable)["amount"]) + Global.VegetableAmount).ToString();
            yield return new WaitForSeconds(0.85f);
            Global._isTakeLoot = true;
            yield return new WaitForSeconds(0.5f);
            GameObject.Destroy(Global._selectedDropPlace);
            Global.lootType = "";
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerTake");
            Global._isClickLoot = false;
        }
    }

    public virtual IEnumerator c_UpdateController_command_goToVegetable(GameObject what) //nothing
    {
        float someDistanceHeroVegetable = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteVegetablePlace.transform.position);
        float someTime = (someDistanceHeroVegetable * 1f) / 6f;
        bool playerRunAnim = false;
        Global._isBegun = true;
        Global._heroTargetPoint = Vector3.MoveTowards(Global._hero_dolly.transform.position, what.transform.position, someDistanceHeroVegetable - (Global._system_distanceTakeDrop - 0.5f));
        if (!playerRunAnim)
        {
            playerRunAnim = true;
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerRun");
        }
        yield return new WaitForSeconds(someTime);
        if ((Global._isRemoteVegetablePlace != null) && !Global._isClickVegetable)
        {
            someDistanceHeroVegetable = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteVegetablePlace.transform.position);
            if (someDistanceHeroVegetable <= Global._system_distanceTakeDrop)
            {
                Global.VegetableType = ((s_VegetableController) what.transform.GetComponent(typeof(s_VegetableController))).resType;
                Global.VegetableAmount = ((s_VegetableController) what.transform.GetComponent(typeof(s_VegetableController))).resAmount;
                int i = 0;
                while (i < Global.globalInventoryThingsArr.Count)
                {
                    if (Global.globalInventoryThingsArr[i]["thingname"] == Global.VegetableType)
                    {
                        Global._isRemoteVegetable = false;
                        Global._isClickVegetable = true;
                        this.StartCoroutine(this.c_UpdateController_command_TakeVegetable(what));
                        ((s_VegetableController) what.transform.GetComponent(typeof(s_VegetableController))).isActive = false;
                        break;
                    }
                    else
                    {
                        if (i == (Global.globalInventoryThingsArr.Count - 1))
                        {
                            if ((Global._isR3Backpack || Global._isR5Backpack) && (Global.globalInventoryThingsArr.Count < 24))
                            {
                                Global._isRemoteVegetable = false;
                                Global._isClickVegetable = true;
                                this.StartCoroutine(this.c_UpdateController_command_TakeVegetable(what));
                                ((s_VegetableController) what.transform.GetComponent(typeof(s_VegetableController))).isActive = false;
                            }
                            else
                            {
                                if ((!Global._isR3Backpack || !Global._isR5Backpack) && (Global.globalInventoryThingsArr.Count < 12))
                                {
                                    Global._isRemoteVegetable = false;
                                    Global._isClickVegetable = true;
                                    this.StartCoroutine(this.c_UpdateController_command_TakeVegetable(what));
                                    ((s_VegetableController) what.transform.GetComponent(typeof(s_VegetableController))).isActive = false;
                                }
                                else
                                {
                                    Global._isInventoryFull = true;
                                    Global._isBegun = false;
                                    Global._heroTargetPoint = Global._hero_dolly.transform.position;
                                    Global._isRemoteVegetablePlace = null;
                                }
                            }
                        }
                    }
                    i++;
                }
            }
            else
            {
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_TakeVegetable(GameObject what)
    {
        if (what != null)
        {
            Global._selectedVegetable = what;
            Global._hero_dolly.transform.LookAt(new Vector3(what.transform.position.x, Global._hero_dolly.transform.position.y, what.transform.position.z));
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerTake");
            (Global._userResourcesFood[Global.VegetableType] as Hashtable)["amount"] = (int.Parse((string) (Global._userResourcesFood[Global.VegetableType] as Hashtable)["amount"]) + Global.VegetableAmount).ToString();
            yield return new WaitForSeconds(0.85f);
            Global._isTakeLoot = true;
            yield return new WaitForSeconds(0.5f);
            GameObject.Destroy(Global._selectedVegetable);
            Global.VegetableType = "";
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerTake");
            Global._isClickVegetable = false;
        }
    }

    // ************************* ДОИМ КОРОВУ И КОЗЛА ;)
    public virtual IEnumerator c_UpdateController_command_goToAnimal(GameObject what) //nothing
    {
        float someDistanceHeroAnimal = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteAnimalPlace.transform.position);
        float someTime = (someDistanceHeroAnimal * 1f) / 6f;
        bool playerRunAnim = false;
        Global._isBegun = true;
        Global._heroTargetPoint = Vector3.MoveTowards(Global._hero_dolly.transform.position, what.transform.position, someDistanceHeroAnimal - (Global._system_distanceTakeDrop - 0.5f));
        if (!playerRunAnim)
        {
            playerRunAnim = true;
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerRun");
        }
        yield return new WaitForSeconds(someTime);
        if ((Global._isRemoteAnimalPlace != null) && !Global._isClickAnimal)
        {
            someDistanceHeroAnimal = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteAnimalPlace.transform.position);
            if (someDistanceHeroAnimal <= Global._system_distanceTakeDrop)
            {
                Global.AnimalType = ((s_AnimalController) what.transform.GetComponent(typeof(s_AnimalController))).resType;
                Global.VegetableAmount = ((s_AnimalController) what.transform.GetComponent(typeof(s_AnimalController))).resAmount;
                int i = 0;
                while (i < Global.globalInventoryThingsArr.Count)
                {
                    if (Global.globalInventoryThingsArr[i]["thingname"] == Global.AnimalType)
                    {
                        Global._isRemoteAnimal = false;
                        Global._isClickAnimal = true;
                        this.StartCoroutine(this.c_UpdateController_command_TakeAnimal(what));
                        ((s_AnimalController) what.transform.GetComponent(typeof(s_AnimalController))).isActive = false;
                        break;
                    }
                    else
                    {
                        if (i == (Global.globalInventoryThingsArr.Count - 1))
                        {
                            if ((Global._isR3Backpack || Global._isR5Backpack) && (Global.globalInventoryThingsArr.Count < 24))
                            {
                                Global._isRemoteAnimal = false;
                                Global._isClickAnimal = true;
                                this.StartCoroutine(this.c_UpdateController_command_TakeAnimal(what));
                                ((s_AnimalController) what.transform.GetComponent(typeof(s_AnimalController))).isActive = false;
                            }
                            else
                            {
                                if ((!Global._isR3Backpack || !Global._isR5Backpack) && (Global.globalInventoryThingsArr.Count < 12))
                                {
                                    Global._isRemoteAnimal = false;
                                    Global._isClickAnimal = true;
                                    this.StartCoroutine(this.c_UpdateController_command_TakeAnimal(what));
                                    ((s_AnimalController) what.transform.GetComponent(typeof(s_AnimalController))).isActive = false;
                                }
                                else
                                {
                                    Global._isInventoryFull = true;
                                    Global._isBegun = false;
                                    Global._heroTargetPoint = Global._hero_dolly.transform.position;
                                    Global._isRemoteAnimalPlace = null;
                                }
                            }
                        }
                    }
                    i++;
                }
            }
            else
            {
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_TakeAnimal(GameObject what)
    {
        if (what != null)
        {
            Global._selectedAnimal = what;
            Global._hero_dolly.transform.LookAt(new Vector3(what.transform.position.x, Global._hero_dolly.transform.position.y, what.transform.position.z));
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerTake");
            (Global._userResourcesFood[Global.AnimalType] as Hashtable)["amount"] = (int.Parse((string) (Global._userResourcesFood[Global.AnimalType] as Hashtable)["amount"]) + Global.VegetableAmount).ToString();
            yield return new WaitForSeconds(0.85f);
            Global._isTakeLoot = true;
            yield return new WaitForSeconds(0.5f);
            Global._selectedAnimal = null;
            Global.AnimalType = "";
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerTake");
            Global._isClickAnimal = false;
        }
    }

    // ************************* ПОДБИРАЕМ КЛЮЧ
    public virtual IEnumerator c_UpdateController_command_goToKey(GameObject what) //nothing
    {
        float someDistanceHeroKey = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteKeyPlace.transform.position);
        float someTime = (someDistanceHeroKey * 1f) / 6f;
        bool playerRunAnim = false;
        Global._isBegun = true;
        Global._heroTargetPoint = Vector3.MoveTowards(Global._hero_dolly.transform.position, what.transform.position, someDistanceHeroKey - (Global._system_distanceTakeDrop - 0.5f));
        if (!playerRunAnim)
        {
            playerRunAnim = true;
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerRun");
        }
        yield return new WaitForSeconds(someTime);
        if ((Global._isRemoteKeyPlace != null) && !Global._isClickKey)
        {
            someDistanceHeroKey = Vector3.Distance(Global._hero_dolly.transform.position, Global._isRemoteKeyPlace.transform.position);
            if (someDistanceHeroKey <= Global._system_distanceTakeDrop)
            {
                int i = 0;
                while (i < Global.globalInventoryThingsArr.Count)
                {
                    if (i == (Global.globalInventoryThingsArr.Count - 1))
                    {
                        if ((Global._isR3Backpack || Global._isR5Backpack) && (Global.globalInventoryThingsArr.Count < 24))
                        {
                            Global._isRemoteKey = false;
                            Global._isClickKey = true;
                            this.StartCoroutine(this.c_UpdateController_command_TakeKey(what));
                        }
                        else
                        {
                            if ((!Global._isR3Backpack || !Global._isR5Backpack) && (Global.globalInventoryThingsArr.Count < 12))
                            {
                                Global._isRemoteKey = false;
                                Global._isClickKey = true;
                                this.StartCoroutine(this.c_UpdateController_command_TakeKey(what));
                            }
                            else
                            {
                                Global._isInventoryFull = true;
                                Global._isBegun = false;
                                Global._heroTargetPoint = Global._hero_dolly.transform.position;
                                Global._isRemoteKeyPlace = null;
                            }
                        }
                    }
                    i++;
                }
            }
            else
            {
            }
        }
    }

    public virtual IEnumerator c_UpdateController_command_TakeKey(GameObject what)
    {
        if (what != null)
        {
            Global._selectedKey = what;
            Global._hero_dolly.transform.LookAt(new Vector3(what.transform.position.x, Global._hero_dolly.transform.position.y, what.transform.position.z));
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerTake");
            yield return new WaitForSeconds(0.85f);
            GameObject.Destroy(Global._selectedKey);
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerTake");
            Global._isClickKey = false;
            Global._isGetKey = true;
            Global.globalBus.gameObject.SendMessage("SaveGame");
        }
    }

    public virtual void c_UpdateController_command_updateConsumables()
    {
        Global.globalBus.gameObject.SendMessage("c_GameController_CheckConsumables");
    }

    public virtual IEnumerator c_UpdateController_command_openDropPlace(GameObject what)
    {
        int index = Random.Range(0, Global._recipeDropLoot.Count);
        if (what != null)
        {
            Global._selectedDropPlace = what;
            Global._hero_dolly.transform.LookAt(new Vector3(what.transform.position.x, Global._hero_dolly.transform.position.y, what.transform.position.z));
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerTake");
            yield return new WaitForSeconds(0.85f);
            if (Global._selectedDropPlace.transform.Find("particlefx").gameObject != null)
            {
                Global._selectedDropPlace.transform.Find("particlefx").gameObject.SetActive(false);
            }
            Global.globalBus.SendMessage("c_GameController_Base_command_GenerateDrop", index);
            yield return new WaitForSeconds(0.5f);
            GameObject.Destroy(Global._selectedDropPlace);
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerTake");
        }
    }

    private Hashtable isCanDropObjectToSomePlace()
    {
        Hashtable someDropablePlaceInfo = null;
        List<object> rectArray = new List<object>();
        List<object> storageArray = new List<object>();
        if (!(Global.someDropableObject == null))
        {
            foreach (DictionaryEntry dropType in Global.someDropableObject["drops"] as Hashtable)
            {
                if (((dropType.Key as string == "wear") 
                    && !(Global.someDropableObject["type"] as string == "food")) 
                    && !(Global.someDropableObject["type"] as string == "loot"))
                {
                    foreach (DictionaryEntry dropTypeWear in dropType.Value as Hashtable)
                    {
                        rectArray.Add(dropTypeWear.Value);
                        someDropablePlaceInfo = this.whatCanWeDropObjectToSomePlaceRealy((string) dropType.Key, (string) dropTypeWear.Key, rectArray);
                        if ((bool)someDropablePlaceInfo["isdropable"])
                        {
                            return someDropablePlaceInfo;
                        }
                    }
                }
                else
                {
                    if (dropType.Key as string == "storage_inv")
                    {
                        this.isStorage = false;
                        someDropablePlaceInfo = this.whatCanWeDropObjectToSomePlaceRealy((string) dropType.Key, "", dropType.Value as List<object>);
                        if ((bool)someDropablePlaceInfo["isdropable"])
                        {
                            return someDropablePlaceInfo;
                        }
                    }
                    else
                    {
                        if (dropType.Key as string == "storage")
                        {
                            this.isStorage = true;
                            someDropablePlaceInfo = this.whatCanWeDropObjectToSomePlaceRealy((string) dropType.Key, "", dropType.Value as List<object>);
                            if ((bool)someDropablePlaceInfo["isdropable"])
                            {
                                return someDropablePlaceInfo;
                            }
                        }
                        else
                        {
                            someDropablePlaceInfo = this.whatCanWeDropObjectToSomePlaceRealy((string) dropType.Key, "", dropType.Value as List<object>);
                            if ((bool)someDropablePlaceInfo["isdropable"])
                            {
                                return someDropablePlaceInfo;
                            }
                        }
                    }
                }
            }
        }
        else
        {
        }
         //nothing
        someDropablePlaceInfo = new Hashtable(new Hashtable() { {"isdropable", false }, });
        return someDropablePlaceInfo;
    }

    private Hashtable whatCanWeDropObjectToSomePlaceRealy(string droptype, string droptypewear, List<object> rectArray)
    {
        foreach (Rect oneRect in rectArray)
        {
            if (Global._isInventoryOpen)
            {
                correctedRect = new Rect(oneRect.x, oneRect.y, oneRect.width, oneRect.height);
            }
            else
            {
                correctedRect = new Rect((oneRect.x + (Screen.width / 2)) - (861 * Global.guiRatio), oneRect.y, oneRect.width, oneRect.height);
            }
            if (this.isPointInRect(correctedRect, Global.gmPos))
            {
                Hashtable someDropablePlaceInfo = new Hashtable(new Hashtable() { {"droptype", droptype },  {"droptypewear", droptypewear },  {"droprect", oneRect },  {"isdropable", true }, });
                return someDropablePlaceInfo;
            }
        }
        Hashtable someDropablePlaceInfoNo = new Hashtable(new Hashtable() { {"isdropable", false }, });
        return someDropablePlaceInfoNo;
    }

    public c_UpdateController()
    {
        this.guiRatio = (Screen.width / 1920f);
    }

}