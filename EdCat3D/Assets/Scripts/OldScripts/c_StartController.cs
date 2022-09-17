using System.Globalization;
using System.Text;
using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class c_StartController : MonoBehaviour
{
    /*****************************************************************************************
* Start game controller
* 
* @author N-Game Studios 
* Research & Development
* 
*****************************************************************************************/
    public UnityEngine.AI.NavMeshAgent _agent;
    private Resolution[] allRes;
    public virtual void Awake()
    {
        Application.targetFrameRate = 30;
        Application.runInBackground = false;
        Global.random_loader = Random.Range(1, 6);
    }

    public virtual void Start()
    {
        this.allRes = Screen.resolutions;
        if (Application.platform == RuntimePlatform.Android)
        {
            Global.guiRatio = Screen.width / 1920f;
        }
        else
        {
             //Global.guiRatio = parseFloat((allRes[allRes.Length - 1].width) / 1920f);
            Global.guiRatio = Screen.width / 1920f;
        }
        Global.screen_width = this.allRes[this.allRes.Length - 1].width;
        Global.screen_height = this.allRes[this.allRes.Length - 1].height;
        Screen.SetResolution((int) Global.screen_width, (int) Global.screen_height, true);
        Global.imagewidth = 1920;
        Global.imageheight = 1080;
        /*Global.newwidth = ((parseFloat(Screen.height)*Global.imagewidth)/Global.imageheight);

    Global.xposition = (Screen.width - Global.newwidth)*0.5f;
    Global.yposition = (Screen.height - Global.imageheight)*0.5f;*/
        Global.newwidth = (Global.screen_height * Global.imagewidth) / Global.imageheight;
        Global.xposition = (Global.screen_width - Global.newwidth) * 0.5f;
        Global.yposition = (Global.screen_height - Global.imageheight) * 0.5f;
        Cursor.visible = false;
        Global.globalBus = GameObject.Find("_bus");
        Global.videoPlayer = GameObject.Find("videoplayer");
        Global._walkerCamera = GameObject.Find("walkerCamera");
        Global._walkerCameraPhoto = GameObject.Find("walkerCamera/photo");
        Global._cutSceneCamera = GameObject.Find("_cutSceneCamera");
        Global._globalmapambient = (AudioSource) GameObject.Find("walkerCamera/_menuambient").GetComponent(typeof(AudioSource));
        Global._globalmapambient.volume = (Global._options_sound_volume / 100f) * 1f;
        Global._hero_dolly = GameObject.Find("hero_player");
        Global.buildingPyramid = GameObject.Find("b_s_bld_pyramid");
        Global.audio_player = (c_AudioSource) GameObject.Find("_audioPlayer").GetComponent(typeof(c_AudioSource));
        Global._isTutorialGreenHouse = GameObject.Find("b_s_bld_greenhouse");
        Global.GreenHouseEntry = GameObject.Find("_GreenHouseEntry");
        Global.UFOEntry = GameObject.Find("_UFOEntry");
        Global.VillaEntry = GameObject.Find("_VillaEntry");
        Global.TempleEntry = GameObject.Find("_TempleEntry");
        Global.FishingEntry = GameObject.Find("_FishingEntry");
        UnityEngine.Object.DontDestroyOnLoad(Global.globalBus);
        UnityEngine.Object.DontDestroyOnLoad(Global._walkerCamera);
        UnityEngine.Object.DontDestroyOnLoad(Global._cutSceneCamera);
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("_gui_styles_common"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("_gui_styles_font"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("_audioPlayer"));
        UnityEngine.Object.DontDestroyOnLoad(Global._hero_dolly);
        UnityEngine.Object.DontDestroyOnLoad(Global.videoPlayer);
        Global._walkerCamera.SetActive(true);
        Global._cutSceneCamera.SetActive(false);
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
        Global.globalBus.SendMessage("c_StartController_command_PlayIntro1");
    }

    public virtual void c_StartController_command_continue()
    {
        Global.globalBus.SendMessage("c_StartController_command_StartGame");
    }

    public virtual void c_StartController_command_PlayIntro1()
    {
        Global.videoPlayer.SendMessage("_PlayVideo", "Nemo-Master-Space.ogv");
    }

    public virtual void c_StartController_command_PlayIntro2()
    {
        Global.videoPlayer.SendMessage("_PlayVideo", "Nemo-Master-Skafandr.ogv");
    }

    public virtual IEnumerator c_StartController_command_StartGame()
    {
        Global._isContinueGame = true;
        if ((Application.platform == RuntimePlatform.OSXPlayer) || (Application.platform == RuntimePlatform.WindowsPlayer))
        {
            if ((this.allRes[this.allRes.Length - 1].width < 800) || (this.allRes[this.allRes.Length - 1].height < 600))
            {
                Global._gui_SetInterface("BaseBack");
                Global.isGameLoaded = true;
                Global._gui_AddInterface("DisplayErrorPopUp");
            }
            else
            {
                Global._progressComplicatedLoader = 0;
                Global._gui_SetInterface("ComlicatedLoader");
                Global.globalBus.gameObject.SendMessage("c_GameController_SetupPlayer");
                Global.globalBus.gameObject.SendMessage("c_GameController_CheckConsumables");
                Global.globalBus.gameObject.SendMessage("LoadGame");
                Global.globalBus.gameObject.SendMessage("c_GameController_LoadPlayer");
                if (Global._isWorldMap)
                {
                    Application.LoadLevel("worldmap");
                }
                if (Global._isGreenHouse)
                {
                    Application.LoadLevel("greenhouse");
                }
                if (Global._isTemple)
                {
                    Application.LoadLevel("temple");
                }
                Global._heroTargetPoint = Global._hero_dolly.transform.position;
                this._agent.Warp(Global._hero_dolly.transform.position);
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
                Global._labelComplicatedLoader = Strings.string002;
                Global._progressComplicatedLoader = 70;
                yield return new WaitForSeconds(0.1f);
                Global._progressComplicatedLoader = 80;
                yield return new WaitForSeconds(0.1f);
                Global._progressComplicatedLoader = 90;
                yield return new WaitForSeconds(0.1f);
                Global._progressComplicatedLoader = 100;
                if (!Global._isTutorial01)
                {
                    Global._isPopUpOpen = true;
                    Global._isSpeak = true;
                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                    Global._gui_SetInterface("TutorialDialogue01");
                    Global._playDialogue(Global.audio_player.sectoid_dlg_001);
                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                }
                else
                {
                    if (Global._isTutorial01 && !Global._isTutorial02)
                    {
                        Global._isPopUpOpen = true;
                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                        Global._playDialogue(Global.audio_player.sectoid_dlg_002);
                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                        Global._gui_SetInterface("TutorialDialogue02");
                    }
                    else
                    {
                        if (Global._isTutorial02 && !Global._isTutorial03)
                        {
                            Global._isPopUpOpen = true;
                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                            Global._playDialogue(Global.audio_player.sectoid_dlg_003);
                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                            Global._gui_SetInterface("TutorialDialogue03");
                        }
                        else
                        {
                            if (Global._isTutorial03 && !Global._isTutorial04)
                            {
                                Global._isPopUpOpen = true;
                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                Global._playDialogue(Global.audio_player.sectoid_dlg_004);
                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                Global._gui_SetInterface("TutorialDialogue04");
                            }
                            else
                            {
                                if (Global._isTutorial04 && !Global._isTutorial05)
                                {
                                    Global._isPopUpOpen = true;
                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                    Global._playDialogue(Global.audio_player.sectoid_dlg_005);
                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                    Global._gui_SetInterface("TutorialDialogue05");
                                }
                                else
                                {
                                    if (Global._isTutorial05 && !Global._isTutorial06)
                                    {
                                        Global._isPopUpOpen = true;
                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                        Global._playDialogue(Global.audio_player.sectoid_dlg_006);
                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                        Global._gui_SetInterface("TutorialDialogue06");
                                    }
                                    else
                                    {
                                        if (Global._isTutorial06 && !Global._isTutorial06_1)
                                        {
                                            Global._isPopUpOpen = true;
                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                            Global._playDialogue(Global.audio_player.sectoid_dlg_007);
                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                            Global._gui_SetInterface("TutorialDialogue06_1");
                                        }
                                        else
                                        {
                                            if (Global._isTutorial06_1 && !Global._isTutorial07)
                                            {
                                                Global._isPopUpOpen = true;
                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                Global._playDialogue(Global.audio_player.sectoid_dlg_008);
                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                Global._gui_SetInterface("TutorialDialogue07");
                                            }
                                            else
                                            {
                                                if (Global._isTutorial07 && !Global._isTutorial08)
                                                {
                                                    Global._isPopUpOpen = true;
                                                    Global.fuckerXCoord = (Screen.width / 2) - (404 * Global.guiRatio);
                                                    Global.fuckerYCoord = (Screen.height / 2) - (200 * Global.guiRatio);
                                                    Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                    Global._gui_SetInterface("TutorialUFOKitchen");
                                                }
                                                else
                                                {
                                                    if (Global._isTutorial08 && !Global._isTutorial09)
                                                    {
                                                        Global._isPopUpOpen = true;
                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_009);
                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                        Global._gui_SetInterface("TutorialDialogue08");
                                                    }
                                                    else
                                                    {
                                                        if (Global._isTutorial09 && !Global._isTutorialCreateSoup)
                                                        {
                                                            Global._isPopUpOpen = true;
                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                            Global._gui_SetInterface("TutorialUFOKitchenPlay");
                                                        }
                                                        else
                                                        {
                                                            if (Global._isTutorialCreateSoup && !Global._isTutorial10)
                                                            {
                                                                Global._isPopUpOpen = true;
                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_010);
                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                Global._gui_SetInterface("TutorialDialogue09");
                                                            }
                                                            else
                                                            {
                                                                if (Global._isTutorial10 && !Global._isTutorial11)
                                                                {
                                                                    Global._isPopUpOpen = true;
                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                    Global._playDialogue(Global.audio_player.sectoid_dlg_011);
                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                    Global._gui_SetInterface("TutorialDialogue10");
                                                                }
                                                                else
                                                                {
                                                                    if (Global._isTutorial11 && !Global._isTutorial12)
                                                                    {
                                                                        Global._isPopUpOpen = true;
                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_012);
                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                        Global._gui_SetInterface("TutorialDialogue11");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Global._isTutorial12 && !Global._isTutorialLearnSmousi)
                                                                        {
                                                                            Global._isPopUpOpen = true;
                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                            Global.fuckerXCoord = (Screen.width / 2) - (828 * Global.guiRatio);
                                                                            Global.fuckerYCoord = (Screen.height / 2) - (192 * Global.guiRatio);
                                                                            Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                            Global.fuckerCall = true;
                                                                            Global._isTutorial13ClickFlash = true;
                                                                            Global._gui_SetInterface("TutorialLibraryScreen");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (Global._isTutorialLearnSmousi && !Global._isTutorial14)
                                                                            {
                                                                                Global._isPopUpOpen = true;
                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_013);
                                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                Global._gui_SetInterface("TutorialDialogue12");
                                                                            }
                                                                            else
                                                                            {
                                                                                if (Global._isTutorial14 && !Global._isTutorial15)
                                                                                {
                                                                                    Global._isPopUpOpen = true;
                                                                                    Global._isKot = true;
                                                                                    Global.speakerGO = GameObject.Find("_kotSpeaker");
                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                    Global._playDialogue(Global.audio_player.kot_dlg_001);
                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                    Global._gui_SetInterface("TutorialDialogue13");
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (Global._isTutorial15 && !Global._isTutorial16)
                                                                                    {
                                                                                        Global._isPopUpOpen = true;
                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_014);
                                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                        Global._gui_SetInterface("TutorialDialogue14");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (Global._isTutorial16 && !Global._isTutorial17)
                                                                                        {
                                                                                            Global._isPopUpOpen = true;
                                                                                            Global._isKot = true;
                                                                                            Global.speakerGO = GameObject.Find("_kotSpeaker");
                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                            Global._playDialogue(Global.audio_player.kot_dlg_002);
                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                            Global._gui_SetInterface("TutorialDialogue15");
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (Global._isTutorial17 && !Global._isTutorial18)
                                                                                            {
                                                                                                Global._isPopUpOpen = true;
                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_015);
                                                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                Global._gui_SetInterface("TutorialDialogue16");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if ((Global._isTutorial17 && !Global._isTutorial19) && !Global._isTutorial20)
                                                                                                {
                                                                                                    Global._isPopUpOpen = true;
                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                    Global._playDialogue(Global.audio_player.sectoid_dlg_016);
                                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                    Global._gui_SetInterface("TutorialDialogue17");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (Global._isTutorial19 && !Global._isTutorial21)
                                                                                                    {
                                                                                                        Global._isPopUpOpen = true;
                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_017);
                                                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                        Global._gui_SetInterface("TutorialDialogue18");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (Global._isTutorial20 && !Global._isTutorial22)
                                                                                                        {
                                                                                                            Global._isPopUpOpen = true;
                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                            Global._playDialogue(Global.audio_player.sectoid_dlg_018);
                                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                            Global._gui_SetInterface("TutorialDialogue19");
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (Global._isTutorial21 && !Global._isTutorial24)
                                                                                                            {
                                                                                                                Global._isPopUpOpen = true;
                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                Global.fuckerXCoord = (Screen.width / 2) + (320 * Global.guiRatio);
                                                                                                                Global.fuckerYCoord = (Screen.height / 2) + (182 * Global.guiRatio);
                                                                                                                Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                Global.fuckerCall = true;
                                                                                                                Global._isTutorial23 = true;
                                                                                                                Global._gui_SetInterface("TutorialGoToMap");
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (Global._isTutorial24 && !Global._isTutorial25)
                                                                                                                {
                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                    Global._isKot = true;
                                                                                                                    Global.speakerGO = GameObject.Find("_kotSpeaker");
                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                    Global._playDialogue(Global.audio_player.kot_dlg_003);
                                                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                    Global._gui_SetInterface("TutorialDialogue20");
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (Global._isTutorial25 && !Global._isTutorial26)
                                                                                                                    {
                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_019);
                                                                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                        Global._gui_SetInterface("TutorialDialogue21");
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (Global._isTutorial26 && !Global._isTutorial27)
                                                                                                                        {
                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                            Global._walkerCamera.SetActive(false);
                                                                                                                            Global._cutSceneCamera.SetActive(true);
                                                                                                                            Global._gui_SetInterface("TutorialCutScene01");
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (Global._isTutorial27 && !Global._isTutorial28)
                                                                                                                            {
                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_020);
                                                                                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                Global._gui_SetInterface("TutorialDialogue22");
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (Global._isTutorial28 && !Global._isTutorial29)
                                                                                                                                {
                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                    Global._isKot = true;
                                                                                                                                    Global.speakerGO = GameObject.Find("_kotSpeaker");
                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                    Global._playDialogue(Global.audio_player.kot_dlg_004);
                                                                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                    Global._gui_SetInterface("TutorialDialogue23");
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (Global._isTutorial29 && !Global._isTutorial30)
                                                                                                                                    {
                                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_021);
                                                                                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                        Global._gui_SetInterface("TutorialDialogue24");
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (Global._isTutorial30 && !Global._isTutorialVeGameEnd)
                                                                                                                                        {
                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                            Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                            ((s_BlurEffect) Global._walkerCameraPhoto.GetComponent(typeof(s_BlurEffect))).enabled = true;
                                                                                                                                            Global.globalBus.gameObject.SendMessage("generateVegetables");
                                                                                                                                            Global._isVeGame01 = true;
                                                                                                                                            Global._gui_SetInterface("VegetablesGameScreen");
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            /*else if (Global._isTutorialVeGameEnd && Global._isEnterTutorialBattleDialogue && !Global._isTutorial31)
            {
                Global._isPopUpOpen = true;
                Global._isTutorialBattleDialogue = false;
                Global.globalBus.SendMessage("c_GameController_Base_command_setPause");
                Global._gui_SetInterface("TutorialDialogue25");
            }*/
                                                                                                                                            if (Global._isTutorial31 && !Global._isTutorial32)
                                                                                                                                            {
                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                Global.fuckerXCoord = (Screen.width / 2) + (464 * Global.guiRatio);
                                                                                                                                                Global.fuckerYCoord = (Screen.height / 2) + (182 * Global.guiRatio);
                                                                                                                                                Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                Global._system_isHeroDead = true;
                                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                                Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                Global.fuckerCall = true;
                                                                                                                                                Global._gui_SetInterface("TutorialGoToUFO");
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if (Global._isTutorial32 && !Global._isTutorial33)
                                                                                                                                                {
                                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                    Global.fuckerXCoord = (Screen.width / 2) - (430 * Global.guiRatio);
                                                                                                                                                    Global.fuckerYCoord = (Screen.height / 2) - (254 * Global.guiRatio);
                                                                                                                                                    Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                    Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                    Global._system_isHeroDead = true;
                                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                                    Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                    Global.fuckerCall = true;
                                                                                                                                                    Global._gui_SetInterface("TutorialWeaponSelectLibrary");
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    if (Global._isTutorial33 && !Global._isTutorial34)
                                                                                                                                                    {
                                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                        (Global._userBooksBiology["library_book03"] as Hashtable)["teach"] = "true";
                                                                                                                                                        Global.fuckerXCoord = (Screen.width / 2) - (487 * Global.guiRatio);
                                                                                                                                                        Global.fuckerYCoord = (Screen.height / 2) - (192 * Global.guiRatio);
                                                                                                                                                        Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                        Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                        Global._system_isHeroDead = true;
                                                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                                                        Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                        Global.fuckerCall = true;
                                                                                                                                                        Global._gui_SetInterface("TutorialWeaponLibraryScreen");
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if ((Global._isTutorial34 && !Global._isTutorial35) && !Global._isTutorial36)
                                                                                                                                                        {
                                                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                            Global._recipeItems[1]["available"] = "true";
                                                                                                                                                            Global.fuckerXCoord = (Screen.width / 2) + (152 * Global.guiRatio);
                                                                                                                                                            Global.fuckerYCoord = (Screen.height / 2) - (207 * Global.guiRatio);
                                                                                                                                                            Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                            Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                            Global._system_isHeroDead = true;
                                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                                            Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                            Global.fuckerCall = true;
                                                                                                                                                            Global._gui_SetInterface("TutorialGoToWorkroom");
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if (Global._isTutorial35 && !Global._isTutorial36)
                                                                                                                                                            {
                                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                Global.fuckerXCoord = (Screen.width / 2) - (403 * Global.guiRatio);
                                                                                                                                                                Global.fuckerYCoord = (Screen.height / 2) - (22 * Global.guiRatio);
                                                                                                                                                                Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                Global._system_isHeroDead = true;
                                                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                                                Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                Global.fuckerCall = true;
                                                                                                                                                                Global._gui_SetInterface("TutorialWorkroomScreen");
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                if (Global._isTutorial36 && !Global._isTutorial37)
                                                                                                                                                                {
                                                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                    Global._playDialogue(Global.audio_player.sectoid_dlg_023);
                                                                                                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                    Global._gui_SetInterface("TutorialWorkroomNoResDialogue1");
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    if ((Global._isTutorial37 && !Global._isTutorial38) && !Global._isTutorial38_1)
                                                                                                                                                                    {
                                                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                        Global.fuckerXCoord = (Screen.width / 2) - (828 * Global.guiRatio);
                                                                                                                                                                        Global.fuckerYCoord = (Screen.height / 2) - (192 * Global.guiRatio);
                                                                                                                                                                        Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                        Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                        Global._system_isHeroDead = true;
                                                                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                                                                        Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                        Global.fuckerCall = true;
                                                                                                                                                                        Global._gui_SetInterface("TutorialTechLibraryScreen");
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        if (Global._isTutorial38 && !Global._isTutorial38_1)
                                                                                                                                                                        {
                                                                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                            Global._recipeItems[1]["available"] = "true";
                                                                                                                                                                            Global.fuckerXCoord = (Screen.width / 2) + (152 * Global.guiRatio);
                                                                                                                                                                            Global.fuckerYCoord = (Screen.height / 2) - (207 * Global.guiRatio);
                                                                                                                                                                            Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                            Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                            Global._system_isHeroDead = true;
                                                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                                                            Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                            Global.fuckerCall = true;
                                                                                                                                                                            Global._gui_SetInterface("TutorialGoToWorkroom2");
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            if (Global._isTutorial38_1 && !Global._isTutorial39)
                                                                                                                                                                            {
                                                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                Global.fuckerXCoord = (Screen.width / 2) - (403 * Global.guiRatio);
                                                                                                                                                                                Global.fuckerYCoord = (Screen.height / 2) + (168 * Global.guiRatio);
                                                                                                                                                                                Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                                Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                                Global._system_isHeroDead = true;
                                                                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                                                                Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                                Global.fuckerCall = true;
                                                                                                                                                                                Global._gui_SetInterface("TutorialWorkroom2Screen");
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                if (Global._isTutorial39 && !Global._isTutorial40)
                                                                                                                                                                                {
                                                                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                    Global._playDialogue(Global.audio_player.sectoid_dlg_025);
                                                                                                                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                                    Global._gui_SetInterface("TutorialDialogue26");
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    if ((Global._isTutorial40 && !Global._isTutorial41) && !Global._isTutorialStorage)
                                                                                                                                                                                    {
                                                                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                        Global.fuckerXCoord = (Screen.width / 2) + (216 * Global.guiRatio);
                                                                                                                                                                                        Global.fuckerYCoord = (Screen.height / 2) - (278 * Global.guiRatio);
                                                                                                                                                                                        Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                                        Global._system_isHeroDead = false;
                                                                                                                                                                                        Global.UFOShowEntry = true;
                                                                                                                                                                                        Global.GreenHouseExitDoor = false;
                                                                                                                                                                                        Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                                                                                                                                                                                        Global._isPopUpOpen = false;
                                                                                                                                                                                        Global._isUFOScreen = false;
                                                                                                                                                                                        Global._isTutorialGreenHouseComplete = true;
                                                                                                                                                                                        Global._gui_SetInterface("WorldMap");
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        if (Global._isTutorial41 && !Global._isTutorialStorage)
                                                                                                                                                                                        {
                                                                                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                            Global._gui_SetInterface("TutorialStorageScreen");
                                                                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                                                                            Global.StorageInventoryArr = new System.Collections.Generic.List<Hashtable>(Global.globalInventoryThingsArr);
                                                                                                                                                                                            Global.StorageInventoryArr.Remove(Global.StorageInventoryArr[0]);
                                                                                                                                                                                            Global.globalBus.SendMessage("c_GameController_Base_command_SortStorageInventory");
                                                                                                                                                                                            Global.globalBus.SendMessage("c_GameController_Base_command_TutorialSearchBlaster");
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            if (Global._isTutorialStorage && !Global._isTutorialInventory)
                                                                                                                                                                                            {
                                                                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                                                                                Global.globalBus.SendMessage("c_GameController_Base_command_SortThings");
                                                                                                                                                                                                Global._gui_SetInterface("TutorialInventoryScreen");
                                                                                                                                                                                                Global.globalBus.SendMessage("c_GameController_Base_command_TutorialSearchInventoryBlaster");
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                if (Global._isTutorialInventory && !Global._isTutorialGoToGreenHouseAgain)
                                                                                                                                                                                                {
                                                                                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                    Global._playDialogue(Global.audio_player.sectoid_dlg_026);
                                                                                                                                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                                                    Global._gui_SetInterface("TutorialDialogue27");
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (Global._isTutorialDialogueSpiderEnd && Global._isTutorialAbilityClick)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                        Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                                                        Global._system_isHeroDead = false;
                                                                                                                                                                                                        Global.UFOShowEntry = true;
                                                                                                                                                                                                        Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                                                                                                                                                                                                        Global._isPopUpOpen = false;
                                                                                                                                                                                                        Global._gui_SetInterface("TutorialBattleScreen");
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (Global._isTutorialWaitingForTheSun && !Global._isTutorialMushroomGameEnd)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                            Global._playDialogue(Global.audio_player.sectoid_dlg_028);
                                                                                                                                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                                                            Global._gui_SetInterface("TutorialDialogue29");
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (Global._isTutorialMushroomGameEnd && !Global._isTutorialEnd)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_030);
                                                                                                                                                                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                                                                Global._gui_SetInterface("TutorialDialogue32");
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Global._isPopUpOpen = false;
                                                                                                                                                                                                                //Global._isWorldMap = true;
                                                                                                                                                                                                                Global.UFOShowEntry = true;
                                                                                                                                                                                                                Global._isUFOScreen = false;
                                                                                                                                                                                                                Global._gui_SetInterface("WorldMap");
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
                Global.isGameLoaded = true;
                Global._globalmapambient.Play();
            }
        }
        else
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                Global.globalBus.gameObject.SendMessage("c_GameController_SetupPlayer");
                Global.globalBus.gameObject.SendMessage("c_GameController_CheckConsumables");
                if (!Global._isTutorial01)
                {
                    Global._isPopUpOpen = true;
                    Global._gui_SetInterface("TutorialDialogue01");
                    Global._playDialogue(Global.audio_player.sectoid_dlg_001);
                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                }
                else
                {
                    Global._isPopUpOpen = false;
                    Global._isWorldMap = true;
                    Global.UFOShowEntry = true;
                    Global._isUFOScreen = false;
                    Global._gui_SetInterface("WorldMap");
                }
                Global.isGameLoaded = true;
                Global._globalmapambient.Play();
            }
            else
            {
                if ((Application.platform == RuntimePlatform.OSXEditor) || (Application.platform == RuntimePlatform.WindowsEditor))
                {
                    if ((Screen.currentResolution.width < 800) || (Screen.currentResolution.height < 600))
                    {
                        Global._gui_SetInterface("BaseBack");
                        Global.isGameLoaded = true;
                        Global._gui_AddInterface("DisplayErrorPopUp");
                    }
                    else
                    {
                         //Global._gui_SetInterface("WorldMap");
                        Global._progressComplicatedLoader = 0;
                        Global._gui_SetInterface("ComlicatedLoader");
                        Global.globalBus.gameObject.SendMessage("c_GameController_SetupPlayer");
                        Global.globalBus.gameObject.SendMessage("c_GameController_CheckConsumables");
                        Global.globalBus.gameObject.SendMessage("LoadGame");
                        Global.globalBus.gameObject.SendMessage("c_GameController_LoadPlayer");
                        if (Global._isWorldMap)
                        {
                            Application.LoadLevel("worldmap");
                        }
                        if (Global._isGreenHouse)
                        {
                            Application.LoadLevel("greenhouse");
                        }
                        if (Global._isTemple)
                        {
                            Application.LoadLevel("temple");
                        }
                        Global._heroTargetPoint = Global._hero_dolly.transform.position;
                        this._agent.Warp(Global._hero_dolly.transform.position);
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
                        Global._labelComplicatedLoader = Strings.string002;
                        Global._progressComplicatedLoader = 70;
                        yield return new WaitForSeconds(0.1f);
                        Global._progressComplicatedLoader = 80;
                        yield return new WaitForSeconds(0.1f);
                        Global._progressComplicatedLoader = 90;
                        yield return new WaitForSeconds(0.1f);
                        Global._progressComplicatedLoader = 100;
                        if (!Global._isTutorial01)
                        {
                            Global._isPopUpOpen = true;
                            Global._isSpeak = true;
                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                            Global._gui_SetInterface("TutorialDialogue01");
                            Global._playDialogue(Global.audio_player.sectoid_dlg_001);
                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                        }
                        else
                        {
                            if (Global._isTutorial01 && !Global._isTutorial02)
                            {
                                Global._isPopUpOpen = true;
                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                Global._playDialogue(Global.audio_player.sectoid_dlg_002);
                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                Global._gui_SetInterface("TutorialDialogue02");
                            }
                            else
                            {
                                if (Global._isTutorial02 && !Global._isTutorial03)
                                {
                                    Global._isPopUpOpen = true;
                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                    Global._playDialogue(Global.audio_player.sectoid_dlg_003);
                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                    Global._gui_SetInterface("TutorialDialogue03");
                                }
                                else
                                {
                                    if (Global._isTutorial03 && !Global._isTutorial04)
                                    {
                                        Global._isPopUpOpen = true;
                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                        Global._playDialogue(Global.audio_player.sectoid_dlg_004);
                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                        Global._gui_SetInterface("TutorialDialogue04");
                                    }
                                    else
                                    {
                                        if (Global._isTutorial04 && !Global._isTutorial05)
                                        {
                                            Global._isPopUpOpen = true;
                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                            Global._playDialogue(Global.audio_player.sectoid_dlg_005);
                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                            Global._gui_SetInterface("TutorialDialogue05");
                                        }
                                        else
                                        {
                                            if (Global._isTutorial05 && !Global._isTutorial06)
                                            {
                                                Global._isPopUpOpen = true;
                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                Global._playDialogue(Global.audio_player.sectoid_dlg_006);
                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                Global._gui_SetInterface("TutorialDialogue06");
                                            }
                                            else
                                            {
                                                if (Global._isTutorial06 && !Global._isTutorial06_1)
                                                {
                                                    Global._isPopUpOpen = true;
                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                    Global._playDialogue(Global.audio_player.sectoid_dlg_007);
                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                    Global._gui_SetInterface("TutorialDialogue06_1");
                                                }
                                                else
                                                {
                                                    if (Global._isTutorial06_1 && !Global._isTutorial07)
                                                    {
                                                        Global._isPopUpOpen = true;
                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_008);
                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                        Global._gui_SetInterface("TutorialDialogue07");
                                                    }
                                                    else
                                                    {
                                                        if (Global._isTutorial07 && !Global._isTutorial08)
                                                        {
                                                            Global._isPopUpOpen = true;
                                                            Global.fuckerXCoord = (Screen.width / 2) - (404 * Global.guiRatio);
                                                            Global.fuckerYCoord = (Screen.height / 2) - (200 * Global.guiRatio);
                                                            Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                            Global._gui_SetInterface("TutorialUFOKitchen");
                                                        }
                                                        else
                                                        {
                                                            if (Global._isTutorial08 && !Global._isTutorial09)
                                                            {
                                                                Global._isPopUpOpen = true;
                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_009);
                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                Global._gui_SetInterface("TutorialDialogue08");
                                                            }
                                                            else
                                                            {
                                                                if (Global._isTutorial09 && !Global._isTutorialCreateSoup)
                                                                {
                                                                    Global._isPopUpOpen = true;
                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                    Global._gui_SetInterface("TutorialUFOKitchenPlay");
                                                                }
                                                                else
                                                                {
                                                                    if (Global._isTutorialCreateSoup && !Global._isTutorial10)
                                                                    {
                                                                        Global._isPopUpOpen = true;
                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_010);
                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                        Global._gui_SetInterface("TutorialDialogue09");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Global._isTutorial10 && !Global._isTutorial11)
                                                                        {
                                                                            Global._isPopUpOpen = true;
                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                            Global._playDialogue(Global.audio_player.sectoid_dlg_011);
                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                            Global._gui_SetInterface("TutorialDialogue10");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (Global._isTutorial11 && !Global._isTutorial12)
                                                                            {
                                                                                Global._isPopUpOpen = true;
                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_012);
                                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                Global._gui_SetInterface("TutorialDialogue11");
                                                                            }
                                                                            else
                                                                            {
                                                                                if (Global._isTutorial12 && !Global._isTutorialLearnSmousi)
                                                                                {
                                                                                    Global._isPopUpOpen = true;
                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                    Global.fuckerXCoord = (Screen.width / 2) - (828 * Global.guiRatio);
                                                                                    Global.fuckerYCoord = (Screen.height / 2) - (192 * Global.guiRatio);
                                                                                    Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                    Global.fuckerCall = true;
                                                                                    Global._isTutorial13ClickFlash = true;
                                                                                    Global._gui_SetInterface("TutorialLibraryScreen");
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (Global._isTutorialLearnSmousi && !Global._isTutorial14)
                                                                                    {
                                                                                        Global._isPopUpOpen = true;
                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_013);
                                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                        Global._gui_SetInterface("TutorialDialogue12");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (Global._isTutorial14 && !Global._isTutorial15)
                                                                                        {
                                                                                            Global._isPopUpOpen = true;
                                                                                            Global._isKot = true;
                                                                                            Global.speakerGO = GameObject.Find("_kotSpeaker");
                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                            Global._playDialogue(Global.audio_player.kot_dlg_001);
                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                            Global._gui_SetInterface("TutorialDialogue13");
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (Global._isTutorial15 && !Global._isTutorial16)
                                                                                            {
                                                                                                Global._isPopUpOpen = true;
                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_014);
                                                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                Global._gui_SetInterface("TutorialDialogue14");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (Global._isTutorial16 && !Global._isTutorial17)
                                                                                                {
                                                                                                    Global._isPopUpOpen = true;
                                                                                                    Global._isKot = true;
                                                                                                    Global.speakerGO = GameObject.Find("_kotSpeaker");
                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                    Global._playDialogue(Global.audio_player.kot_dlg_002);
                                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                    Global._gui_SetInterface("TutorialDialogue15");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (Global._isTutorial17 && !Global._isTutorial18)
                                                                                                    {
                                                                                                        Global._isPopUpOpen = true;
                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_015);
                                                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                        Global._gui_SetInterface("TutorialDialogue16");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if ((Global._isTutorial17 && !Global._isTutorial19) && !Global._isTutorial20)
                                                                                                        {
                                                                                                            Global._isPopUpOpen = true;
                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                            Global._playDialogue(Global.audio_player.sectoid_dlg_016);
                                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                            Global._gui_SetInterface("TutorialDialogue17");
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (Global._isTutorial19 && !Global._isTutorial21)
                                                                                                            {
                                                                                                                Global._isPopUpOpen = true;
                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_017);
                                                                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                Global._gui_SetInterface("TutorialDialogue18");
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (Global._isTutorial20 && !Global._isTutorial22)
                                                                                                                {
                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                    Global._playDialogue(Global.audio_player.sectoid_dlg_018);
                                                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                    Global._gui_SetInterface("TutorialDialogue19");
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (Global._isTutorial21 && !Global._isTutorial24)
                                                                                                                    {
                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                        Global.fuckerXCoord = (Screen.width / 2) + (320 * Global.guiRatio);
                                                                                                                        Global.fuckerYCoord = (Screen.height / 2) + (182 * Global.guiRatio);
                                                                                                                        Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                        Global.fuckerCall = true;
                                                                                                                        Global._isTutorial23 = true;
                                                                                                                        Global._gui_SetInterface("TutorialGoToMap");
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (Global._isTutorial24 && !Global._isTutorial25)
                                                                                                                        {
                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                            Global._isKot = true;
                                                                                                                            Global.speakerGO = GameObject.Find("_kotSpeaker");
                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                            Global._playDialogue(Global.audio_player.kot_dlg_003);
                                                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                            Global._gui_SetInterface("TutorialDialogue20");
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (Global._isTutorial25 && !Global._isTutorial26)
                                                                                                                            {
                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_019);
                                                                                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                Global._gui_SetInterface("TutorialDialogue21");
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (Global._isTutorial26 && !Global._isTutorial27)
                                                                                                                                {
                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                    Global._walkerCamera.SetActive(false);
                                                                                                                                    Global._cutSceneCamera.SetActive(true);
                                                                                                                                    Global._gui_SetInterface("TutorialCutScene01");
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (Global._isTutorial27 && !Global._isTutorial28)
                                                                                                                                    {
                                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_020);
                                                                                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                        Global._gui_SetInterface("TutorialDialogue22");
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (Global._isTutorial28 && !Global._isTutorial29)
                                                                                                                                        {
                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                            Global._isKot = true;
                                                                                                                                            Global.speakerGO = GameObject.Find("_kotSpeaker");
                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                            Global._playDialogue(Global.audio_player.kot_dlg_004);
                                                                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                            Global._gui_SetInterface("TutorialDialogue23");
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if (Global._isTutorial29 && !Global._isTutorial30)
                                                                                                                                            {
                                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                Global._playDialogue(Global.audio_player.sectoid_dlg_021);
                                                                                                                                                Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                Global._gui_SetInterface("TutorialDialogue24");
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if (Global._isTutorial30 && !Global._isTutorialVeGameEnd)
                                                                                                                                                {
                                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                    Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                    ((s_BlurEffect) Global._walkerCameraPhoto.GetComponent(typeof(s_BlurEffect))).enabled = true;
                                                                                                                                                    Global.globalBus.gameObject.SendMessage("generateVegetables");
                                                                                                                                                    Global._isVeGame01 = true;
                                                                                                                                                    Global._gui_SetInterface("VegetablesGameScreen");
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    /*else if (Global._isTutorialVeGameEnd && Global._isEnterTutorialBattleDialogue && !Global._isTutorial31)
            {
                Global._isPopUpOpen = true;
                Global._isTutorialBattleDialogue = false;
                Global.globalBus.SendMessage("c_GameController_Base_command_setPause");
                Global._gui_SetInterface("TutorialDialogue25");
            }*/
                                                                                                                                                    if (Global._isTutorial31 && !Global._isTutorial32)
                                                                                                                                                    {
                                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                        Global.fuckerXCoord = (Screen.width / 2) + (464 * Global.guiRatio);
                                                                                                                                                        Global.fuckerYCoord = (Screen.height / 2) + (182 * Global.guiRatio);
                                                                                                                                                        Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                        Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                        Global._system_isHeroDead = true;
                                                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                                                        Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                        Global.fuckerCall = true;
                                                                                                                                                        Global._gui_SetInterface("TutorialGoToUFO");
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if (Global._isTutorial32 && !Global._isTutorial33)
                                                                                                                                                        {
                                                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                            Global.fuckerXCoord = (Screen.width / 2) - (430 * Global.guiRatio);
                                                                                                                                                            Global.fuckerYCoord = (Screen.height / 2) - (254 * Global.guiRatio);
                                                                                                                                                            Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                            Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                            Global._system_isHeroDead = true;
                                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                                            Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                            Global.fuckerCall = true;
                                                                                                                                                            Global._gui_SetInterface("TutorialWeaponSelectLibrary");
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if (Global._isTutorial33 && !Global._isTutorial34)
                                                                                                                                                            {
                                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                (Global._userBooksBiology["library_book03"] as Hashtable)["teach"] = "true";
                                                                                                                                                                Global.fuckerXCoord = (Screen.width / 2) - (487 * Global.guiRatio);
                                                                                                                                                                Global.fuckerYCoord = (Screen.height / 2) - (192 * Global.guiRatio);
                                                                                                                                                                Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                Global._system_isHeroDead = true;
                                                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                                                Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                Global.fuckerCall = true;
                                                                                                                                                                Global._gui_SetInterface("TutorialWeaponLibraryScreen");
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                if ((Global._isTutorial34 && !Global._isTutorial35) && !Global._isTutorial36)
                                                                                                                                                                {
                                                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                    Global._recipeItems[1]["available"] = "true";
                                                                                                                                                                    Global.fuckerXCoord = (Screen.width / 2) + (152 * Global.guiRatio);
                                                                                                                                                                    Global.fuckerYCoord = (Screen.height / 2) - (207 * Global.guiRatio);
                                                                                                                                                                    Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                    Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                    Global._system_isHeroDead = true;
                                                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                                                    Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                    Global.fuckerCall = true;
                                                                                                                                                                    Global._gui_SetInterface("TutorialGoToWorkroom");
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    if (Global._isTutorial35 && !Global._isTutorial36)
                                                                                                                                                                    {
                                                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                        Global.fuckerXCoord = (Screen.width / 2) - (403 * Global.guiRatio);
                                                                                                                                                                        Global.fuckerYCoord = (Screen.height / 2) - (22 * Global.guiRatio);
                                                                                                                                                                        Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                        Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                        Global._system_isHeroDead = true;
                                                                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                                                                        Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                        Global.fuckerCall = true;
                                                                                                                                                                        Global._gui_SetInterface("TutorialWorkroomScreen");
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        if (Global._isTutorial36 && !Global._isTutorial37)
                                                                                                                                                                        {
                                                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                            Global._playDialogue(Global.audio_player.sectoid_dlg_023);
                                                                                                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                            Global._gui_SetInterface("TutorialWorkroomNoResDialogue1");
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            if ((Global._isTutorial37 && !Global._isTutorial38) && !Global._isTutorial38_1)
                                                                                                                                                                            {
                                                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                Global.fuckerXCoord = (Screen.width / 2) - (828 * Global.guiRatio);
                                                                                                                                                                                Global.fuckerYCoord = (Screen.height / 2) - (192 * Global.guiRatio);
                                                                                                                                                                                Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                                Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                                Global._system_isHeroDead = true;
                                                                                                                                                                                Global._isPopUpOpen = true;
                                                                                                                                                                                Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                                Global.fuckerCall = true;
                                                                                                                                                                                Global._gui_SetInterface("TutorialTechLibraryScreen");
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                if (Global._isTutorial38 && !Global._isTutorial38_1)
                                                                                                                                                                                {
                                                                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                    Global._recipeItems[1]["available"] = "true";
                                                                                                                                                                                    Global.fuckerXCoord = (Screen.width / 2) + (152 * Global.guiRatio);
                                                                                                                                                                                    Global.fuckerYCoord = (Screen.height / 2) - (207 * Global.guiRatio);
                                                                                                                                                                                    Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                                    Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                                    Global._system_isHeroDead = true;
                                                                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                                                                    Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                                    Global.fuckerCall = true;
                                                                                                                                                                                    Global._gui_SetInterface("TutorialGoToWorkroom2");
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    if (Global._isTutorial38_1 && !Global._isTutorial39)
                                                                                                                                                                                    {
                                                                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                        Global.fuckerXCoord = (Screen.width / 2) - (403 * Global.guiRatio);
                                                                                                                                                                                        Global.fuckerYCoord = (Screen.height / 2) + (168 * Global.guiRatio);
                                                                                                                                                                                        Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                                        Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                                        Global._system_isHeroDead = true;
                                                                                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                                                                                        Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                                                                                                                                                                        Global.fuckerCall = true;
                                                                                                                                                                                        Global._gui_SetInterface("TutorialWorkroom2Screen");
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        if (Global._isTutorial39 && !Global._isTutorial40)
                                                                                                                                                                                        {
                                                                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                            Global._playDialogue(Global.audio_player.sectoid_dlg_025);
                                                                                                                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                                            Global._gui_SetInterface("TutorialDialogue26");
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            if ((Global._isTutorial40 && !Global._isTutorial41) && !Global._isTutorialStorage)
                                                                                                                                                                                            {
                                                                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                Global.fuckerXCoord = (Screen.width / 2) + (216 * Global.guiRatio);
                                                                                                                                                                                                Global.fuckerYCoord = (Screen.height / 2) - (278 * Global.guiRatio);
                                                                                                                                                                                                Global.FuckerYcoordOriginal = Global.fuckerYCoord;
                                                                                                                                                                                                Global._system_isHeroDead = false;
                                                                                                                                                                                                Global.UFOShowEntry = true;
                                                                                                                                                                                                Global.GreenHouseExitDoor = false;
                                                                                                                                                                                                Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                                                                                                                                                                                                Global._isPopUpOpen = false;
                                                                                                                                                                                                Global._isUFOScreen = false;
                                                                                                                                                                                                Global._isTutorialGreenHouseComplete = true;
                                                                                                                                                                                                Global._gui_SetInterface("WorldMap");
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                if (Global._isTutorial41 && !Global._isTutorialStorage)
                                                                                                                                                                                                {
                                                                                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                    Global._gui_SetInterface("TutorialStorageScreen");
                                                                                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                                                                                    Global.StorageInventoryArr = new System.Collections.Generic.List<Hashtable>(Global.globalInventoryThingsArr);
                                                                                                                                                                                                    Global.StorageInventoryArr.Remove(Global.StorageInventoryArr[0]);
                                                                                                                                                                                                    Global.globalBus.SendMessage("c_GameController_Base_command_SortStorageInventory");
                                                                                                                                                                                                    Global.globalBus.SendMessage("c_GameController_Base_command_TutorialSearchBlaster");
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (Global._isTutorialStorage && !Global._isTutorialInventory)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                                                                                                        Global.globalBus.SendMessage("c_GameController_Base_command_SortThings");
                                                                                                                                                                                                        Global._gui_SetInterface("TutorialInventoryScreen");
                                                                                                                                                                                                        Global.globalBus.SendMessage("c_GameController_Base_command_TutorialSearchInventoryBlaster");
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (Global._isTutorialInventory && !Global._isTutorialGoToGreenHouseAgain)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Global._isPopUpOpen = true;
                                                                                                                                                                                                            Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                            Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                            Global._playDialogue(Global.audio_player.sectoid_dlg_026);
                                                                                                                                                                                                            Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                                                            Global._gui_SetInterface("TutorialDialogue27");
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (Global._isTutorialDialogueSpiderEnd && Global._isTutorialAbilityClick)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                                Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                                Global.globalBus.SendMessage("c_GameController_Base_command_unsetPause");
                                                                                                                                                                                                                Global._system_isHeroDead = false;
                                                                                                                                                                                                                Global.UFOShowEntry = true;
                                                                                                                                                                                                                Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                                                                                                                                                                                                                Global._isPopUpOpen = false;
                                                                                                                                                                                                                Global._gui_SetInterface("TutorialBattleScreen");
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (Global._isTutorialWaitingForTheSun && !Global._isTutorialMushroomGameEnd)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Global._isPopUpOpen = true;
                                                                                                                                                                                                                    Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                                    Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                                    Global._playDialogue(Global.audio_player.sectoid_dlg_028);
                                                                                                                                                                                                                    Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                                                                    Global._gui_SetInterface("TutorialDialogue29");
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (Global._isTutorialMushroomGameEnd && !Global._isTutorialEnd)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Global._isPopUpOpen = true;
                                                                                                                                                                                                                        Global.speakerGO = GameObject.Find("_sectoidSpeaker");
                                                                                                                                                                                                                        Global.holoGO = GameObject.Find("_sectodSpeakerHolo");
                                                                                                                                                                                                                        Global._playDialogue(Global.audio_player.sectoid_dlg_030);
                                                                                                                                                                                                                        Global.globalBus.gameObject.SendMessage("DialogueEnd");
                                                                                                                                                                                                                        Global._gui_SetInterface("TutorialDialogue32");
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Global._isPopUpOpen = false;
                                                                                                                                                                                                                        //Global._isWorldMap = true;
                                                                                                                                                                                                                        Global.UFOShowEntry = true;
                                                                                                                                                                                                                        Global._isUFOScreen = false;
                                                                                                                                                                                                                        Global._gui_SetInterface("WorldMap");
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
                        Global.isGameLoaded = true;
                        Global._globalmapambient.Play();
                    }
                }
            }
        }
    }

}