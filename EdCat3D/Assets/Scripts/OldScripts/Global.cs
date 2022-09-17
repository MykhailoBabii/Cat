using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/*****************************************************************************************
* Global functions and variables
* 
* @author N-Game Studios 
* Research & Development	
* 
*****************************************************************************************/
[System.Serializable]
public class Global : object
{
     /*public static var imagewidth:float = 1920;
    public static var imageheight:float = 1080;

    public static var newwidth:float = ((parseFloat(Screen.height)*Global.imagewidth)/Global.imageheight);

    public static var xposition:float = (Screen.width - Global.newwidth)*0.5f;
    public static var yposition:float = (Screen.height - Global.imageheight)*0.5f;*/
    public static float imagewidth;
    public static float imageheight;
    public static float newwidth;
    public static float xposition;
    public static float yposition;
    // =================================================================
    public static int _options_music_volume_orig;
    public static int _options_sound_volume_orig;
    public static int _options_music_volume;
    public static int _options_sound_volume;
    public static bool _options_is_fullscreen;
    public static bool _options_is_gamecursor;
    public static bool _options_is_quality;
    public static float screen_width;
    public static float screen_height;
    public static bool _isSoundOn;
    public static bool _isMusicOn;
    public static c_AudioSource audio_player;
    public static AudioSource _globalmapambient;
    public static void _game_OffSound()
    {
        Global._p_muteGameSound("globalsound_s", true);
        Global._globalmapambient.mute = true;
    }

    public static void _game_OnSound()
    {
        Global._p_muteGameSound("globalsound_s", false);
        Global._globalmapambient.mute = false;
    }

    public static void _game_OffMusic()
    {
        Global._p_muteGameSound("globalsound_m", true);
    }

    public static void _game_OnMusic()
    {
        Global._p_muteGameSound("globalsound_m", false);
    }

    public static void _game_VolumeSound()
    {
        Global._p_volumeGameSound("globalsound_s", (Global._options_sound_volume / 100f) * 1f);
    }

    public static void _game_VolumeMusic()
    {
        Global._p_volumeGameSound("globalsound_m", (Global._options_music_volume / 100f) * 1f);
    }

    public static void _p_muteGameSound(string wgroup, bool isMute)
    {
        if (wgroup == "globalsound_s")
        {
            Global._globalmapambient.mute = isMute;
        }
        if (Global._globalDestroyer_groups.ContainsKey(wgroup))
        {
            foreach (DictionaryEntry oneobj in Global._globalDestroyer_groups[wgroup] as Hashtable)
            {
                GameObject someobj = oneobj.Value as GameObject;
                if (someobj != null)
                {
                    ((AudioSource) someobj.GetComponent(typeof(AudioSource))).mute = isMute;
                }
            }
        }
    }

    public static bool _p_CheckIfMusicPlaying()
    {
        if (Global._globalDestroyer_groups.ContainsKey("globalsound_m"))
        {
            foreach (DictionaryEntry oneobj in Global._globalDestroyer_groups["globalsound_m"] as Hashtable)
            {
                GameObject someobj = oneobj.Value as GameObject;
                if (someobj != null)
                {
                    if (((AudioSource) someobj.GetComponent(typeof(AudioSource))).isPlaying)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static void _p_volumeGameSound(string wgroup, float vol)
    {
        if (wgroup == "globalsound_s")
        {
            Global._globalmapambient.volume = vol;
        }
        if (Global._globalDestroyer_groups.ContainsKey(wgroup))
        {
            foreach (DictionaryEntry oneobj in Global._globalDestroyer_groups[wgroup] as Hashtable)
            {
                GameObject someobj = oneobj.Value as GameObject;
                if (someobj != null)
                {
                    ((AudioSource) someobj.GetComponent(typeof(AudioSource))).volume = vol;
                }
            }
        }
    }

    public static void _p_musicTakeSingle()
    {
        if (Global._globalDestroyer_groups.ContainsKey("globalsound_m"))
        {
            foreach (DictionaryEntry oneobj in Global._globalDestroyer_groups["globalsound_m"] as Hashtable)
            {
                GameObject someobj = oneobj.Value as GameObject;
                if (someobj != null)
                {
                    ((c_DestroyController) Global.globalBus.GetComponent(typeof(c_DestroyController))).StartCoroutine(Global._p_FadeAudio_destroy(3.5f, "FadeOut", someobj));
                }
            }
        }
    }

    public static IEnumerator _p_FadeAudio_destroy(float timer, string fadeType, GameObject someobj)
    {
        if (someobj != null)
        {
            float start = fadeType == "FadeIn" ? 0f : (Global._options_music_volume / 100f) * 1f;
            float end = fadeType == "FadeIn" ? (Global._options_music_volume / 100f) * 1f : 0f;
            float i = 0f;
            float step = 1f / timer;
            while (i <= 1f)
            {
                i = i + (step * Time.deltaTime);
                if (someobj != null)
                {
                    ((AudioSource) someobj.GetComponent(typeof(AudioSource))).volume = Mathf.Lerp(start, end, i);
                }
                yield return null;
            }
            GameObject.DestroyImmediate(someobj);
        }
    }

    public static IEnumerator _p_FadeAudio(float timer, string fadeType, GameObject someobj)
    {
        if (someobj != null)
        {
            float start = fadeType == "FadeIn" ? 0f : (Global._options_music_volume / 100f) * 1f;
            float end = fadeType == "FadeIn" ? (Global._options_music_volume / 100f) * 1f : 0f;
            float i = 0f;
            float step = 1f / timer;
            while (i <= 1f)
            {
                i = i + (step * Time.deltaTime);
                if (someobj != null)
                {
                    ((AudioSource) someobj.GetComponent(typeof(AudioSource))).volume = Mathf.Lerp(start, end, i);
                }
                yield return null;
            }
        }
    }

    public static void _playUI(AudioClip whatsound)
    {
        if ((Global._walkerCamera == null) || (whatsound == null))
        {
            return;
        }
        //var soundHelper:GameObject = GameObject.Instantiate(Global.gass.LoadAsset("sound_2d"), Global._walkerCamera.transform.position, Quaternion.identity);
        GameObject soundHelper = GameObject.Instantiate(Global.audio_player.sound_2d, Global._walkerCamera.transform.position, Quaternion.identity);
        soundHelper.transform.parent = Global._walkerCamera.transform;
        soundHelper.transform.localPosition = Vector3.zero;
        //soundHelper.GetComponent(AudioSource).clip = Global.gass.LoadAsset(whatsound);
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).clip = whatsound;
        if (!Global._isSoundOn)
        {
            ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).mute = true;
        }
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).volume = whatsound.name == "sound_ui_bigwave" ? (Global._options_sound_volume_orig / 100f) * 1f : (Global._options_sound_volume / 100f) * 1f;
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).Stop();
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).Play();
        Global.globalBus.SendMessage("c_DestroyController_command_ADD_to_GROUP", new object[] {"globalsound_s", soundHelper});
    }

    public static GameObject soundDialogue;
    public static void _playDialogue(AudioClip whatsound)
    {
        if ((Global._walkerCamera == null) || (whatsound == null))
        {
            return;
        }
        //Global._isSpeak = true;
        //var soundHelper:GameObject = GameObject.Instantiate(Global.gass.LoadAsset("sound_2d"), Global._walkerCamera.transform.position, Quaternion.identity);
        GameObject soundHelper = GameObject.Instantiate(Global.audio_player.sound_2d, Global._walkerCamera.transform.position, Quaternion.identity);
        soundHelper.transform.parent = Global._walkerCamera.transform;
        soundHelper.transform.localPosition = Vector3.zero;
        //soundHelper.GetComponent(AudioSource).clip = Global.gass.LoadAsset(whatsound);
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).clip = whatsound;
        Global.dialogueLength = ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).clip.length;
        Global.soundDialogue = soundHelper;
        if (!Global._isSoundOn)
        {
            ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).mute = true;
        }
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).volume = whatsound.name == "sound_ui_bigwave" ? (Global._options_sound_volume_orig / 100f) * 1f : (Global._options_sound_volume / 100f) * 1f;
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).Stop();
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).Play();
        Global.globalBus.SendMessage("c_DestroyController_command_ADD_to_GROUP", new object[] {"globalsound_s", soundHelper});
    }

    public static GameObject soundBegun;
    public static void _playSound(GameObject obj, string whatsound)
    {
        if ((obj == null) || (whatsound == ""))
        {
            return;
        }
        GameObject soundHelper = GameObject.Instantiate(Global.gass.LoadAsset<GameObject>("sound_3d"), obj.transform.position, Quaternion.identity);
        soundHelper.transform.parent = obj.transform;
        soundHelper.transform.localPosition = Vector3.zero;
        if (((whatsound == "sound_rogue_walk") || (whatsound == "sound_warrior_walk")) || (whatsound == "sound_mage_walk"))
        {
            Global.soundBegun = soundHelper;
        }
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).clip = Global.gass.LoadAsset<AudioClip>(whatsound);
        if (!Global._isSoundOn)
        {
            ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).mute = true;
        }
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).volume = (Global._options_sound_volume / 100f) * 1f;
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).Stop();
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).Play();
        Global.globalBus.SendMessage("c_DestroyController_command_ADD_to_GROUP", new object[] {"globalsound_s", soundHelper});
    }

    public static void _stopSoundBegun()
    {
        ((AudioSource) Global.soundBegun.GetComponent(typeof(AudioSource))).Stop();
    }

    public static void _stopSoundDialogue()
    {
        if (Global.soundDialogue != null)
        {
            ((AudioSource) Global.soundDialogue.GetComponent(typeof(AudioSource))).Stop();
        }
    }

    public static void _playMusic(string whatsound)
    {
        if ((Global._walkerCamera == null) || (whatsound == ""))
        {
            return;
        }
        Global._p_musicTakeSingle();
        GameObject soundHelper = GameObject.Instantiate(Global.gass.LoadAsset<GameObject>("sound_2d"), Global._walkerCamera.transform.position, Quaternion.identity);
        soundHelper.transform.parent = Global._walkerCamera.transform;
        soundHelper.transform.localPosition = Vector3.zero;
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).clip = Global.gass.LoadAsset<AudioClip>(whatsound);
        if (!Global._isMusicOn)
        {
            ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).mute = true;
        }
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).Stop();
        ((AudioSource) soundHelper.GetComponent(typeof(AudioSource))).Play();
        ((c_DestroyController) Global.globalBus.GetComponent(typeof(c_DestroyController))).StartCoroutine(Global._p_FadeAudio(4, "FadeIn", soundHelper));
        Global.globalBus.SendMessage("c_DestroyController_command_ADD_to_GROUP", new object[] {"globalsound_m", soundHelper});
    }

    // =================================================================
    public static void _p_StopTrailRenderers(GameObject someobj)
    {
        if (someobj != null)
        {
            foreach (Transform child in someobj.transform)
            {
                Component[] childTrails = child.gameObject.GetComponentsInChildren(typeof(TrailRenderer));
                foreach (TrailRenderer currentChildTrail in childTrails)
                {
                    UnityEngine.GameObject.DestroyImmediate(currentChildTrail);
                }
                Global._p_StopTrailRenderers(child.gameObject);
            }
        }
    }

    public static void _p_FadeOutLight(float timer, GameObject someobj, float fromvalue)
    {
        if (someobj != null)
        {
            foreach (Transform child in someobj.transform)
            {
                Component[] childLights = child.gameObject.GetComponentsInChildren(typeof(Light));
                foreach (Light currentChildLight in childLights)
                {
                    ((c_DestroyController) Global.globalBus.GetComponent(typeof(c_DestroyController))).StartCoroutine(Global._p_FadeOutLight_helperyield(timer, currentChildLight, fromvalue));
                }
                Global._p_FadeOutLight(timer, child.gameObject, fromvalue);
            }
        }
    }

    public static IEnumerator _p_FadeOutLight_helperyield(float timer, Light someobj, float fromvalue)
    {
        float start = fromvalue;
        float end = 0f;
        float i = 0f;
        float step = 1f / timer;
        while (i <= 1f)
        {
            i = i + (step * Time.deltaTime);
            if (someobj != null)
            {
                someobj.intensity = Mathf.Lerp(start, end, i);
            }
            yield return null;
        }
    }

    // =================================================================
    public static Hashtable cloneHashtable(Hashtable wantclone)
    {
        Hashtable cloneHashtable = new Hashtable();
        foreach (DictionaryEntry oneEntry in wantclone)
        {
            cloneHashtable.Add(oneEntry.Key, oneEntry.Value);
        }
        return cloneHashtable;
    }

    public static void concatHashtable(Hashtable original, Hashtable wantconcat)
    {
        foreach (DictionaryEntry oneEntry in wantconcat)
        {
            if (original.ContainsKey(oneEntry.Key))
            {
                original[oneEntry.Key] = oneEntry.Value;
            }
            else
            {
                original.Add(oneEntry.Key, oneEntry.Value);
            }
        }
    }

    // =================================================================
    public static float _creditRotatorY;
    public static bool _is_CreditRotating;
    public static int _mouseX_pos_for_music_slide;
    public static int _mouseX_pos_for_sound_slide;
    public static int _mouseX_pos_for_music_slide_off;
    public static int _music_volume_slide;
    public static int _sound_volume_slide;
    // =================================================================
    public static bool _isPausedByInterface;
    public static bool _isGameFocused;
    public static bool _isGamePaused;
    public static float _gameFocusLooseTime;
    public static Hashtable _globalDestroyer;
    public static Hashtable _globalDestroyer_groups;
    public static bool _notification_events;
    // =================================================================
    public static bool isGameLoaded;
    public static GameObject globalBus;
    public static Vector2 gmPos;
    public static Vector2 gmrPos;
    public static Vector2 ngmPos;
    public static AssetBundle gass;
    public static AssetBundle gmainscene;
    public static AssetBundle gadventurezone;
    public static GameObject videoPlayer;
    public static GameObject _walkerCamera;
    public static GameObject _walkerCameraPhoto;
    public static GameObject _cutSceneCamera;
    public static float _CameraPos;
    public static GameObject Hero;
    public static bool _heroismove;
    public static GameObject heroPlace;
    public static Vector3 _heroTargetPoint;
    public static GameObject _hero_dolly;
    public static bool _isMoveAllowed;
    // thanks to Leslie Young :) =======================================
    public static Vector2 GUIAniTexture(Rect r, Texture img, int frameCount, int cols, float rate, Vector2 helper, bool oneShot)
    {
        helper.y = helper.y - Time.deltaTime;
        if (helper.y <= 0)
        {
            helper.y = rate;
            helper.x = helper.x + 1;
            if (helper.x >= frameCount)
            {
                if (!oneShot)
                {
                    helper.x = 0;
                }
            }
        }
        int y = Mathf.FloorToInt(helper.x) / cols;
        int x = Mathf.FloorToInt(helper.x) - (y * cols);
        GUI.BeginGroup(r);
        GUI.DrawTexture(new Rect(-x * r.width, -y * r.height, img.width * Global.guiRatio, img.height * Global.guiRatio), img);
        GUI.EndGroup();
        return helper;
    }

    // =================================================================
    public static c_GuiCommonStyleController _gs_common;
    public static c_GuiFontStyleController _gs_font;
    //public static var resolutions:Resolution[] = Screen.resolutions;
    //public static var guiRatio:float = parseFloat((resolutions[resolutions.Length-1].width)/1920f);// = parseFloat(Screen.width/1920f);
    public static Resolution[] resolutions;
    public static float guiRatio;
    public static string _levelType;
    public static bool _musicAlarm;
    public static void _autosetcameraonmap(string what)
    {
        if (what == "GlobalMap")
        {
            Global.globalBus.SendMessage("gui_GlobalMap_normalize");
            Global.globalBus.SendMessage("gui_GlobalMap_setCameraToCurrentLevel");
            if (Global._guiLayers[0] == "GlobalMap")
            {
                Global.globalBus.SendMessage("gui_GlobalMap_playGlobalMapMusic", true);
            }
            else
            {
                Global.globalBus.SendMessage("gui_GlobalMap_playGlobalMapMusic", false);
            }
        }
        else
        {
            if (what == "MainMenu")
            {
                Global.globalBus.SendMessage("gui_GlobalMap_playMainMenuMusic", false);
            }
            else
            {
                if (what == "MissionScreen")
                {
                    Global.globalBus.SendMessage("gui_GlobalMap_playMissionMusic", false);
                }
                else
                {
                    if (what == "CastleScreen")
                    {
                        Global.globalBus.SendMessage("gui_GlobalMap_playCastleMusic", false);
                    }
                    else
                    {
                        if (what == "CreditsScreen")
                        {
                            Global.globalBus.SendMessage("_playCreditsMusic", false);
                        }
                        else
                        {
                            if (what == "Notification")
                            {
                                Global._notification_events = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void _gui_ChangeInterface(string what)
    {
        Global._gui_CloseInterface();
        Global._gui_AddInterface(what);
    }

    public static void _gui_SetInterface(string what)
    {
        Global._gui_CloseAllInterfaces();
        Global._autosetcameraonmap(what);
        Global._guiLayers[0] = what;
        Global._guiLayersPointer = 0;
    }

    public static void _gui_AddInterface(string what)//Global._isPopUpOpen = true;
    {
        if (Global._guiLayersPointer < 10)
        {
            Global._autosetcameraonmap(what);
            Global._guiLayers[Global._guiLayersPointer + 1] = what;
            Global._guiLayersPointer++;
        }
    }

    public static void _gui_InsideInterface(string what)
    {
        if (Global._guiLayersPointer < 10)
        {
            Global._autosetcameraonmap(what);
            Global._guiLayersNoBlack[Global._guiLayersPointer + 1] = "1";
            Global._guiLayers[Global._guiLayersPointer + 1] = what;
            Global._guiLayersPointer++;
        }
    }

    public static void _gui_CloseInterface()
    {
        if (Global._guiLayersPointer >= 0)
        {
            Global._guiLayersPointer--;
            Global._guiLayersNoBlack[Global._guiLayersPointer + 1] = "";
            Global._guiLayers[Global._guiLayersPointer + 1] = "";
        }
    }

    public static void _gui_CloseAllInterfaces()
    {
        Global._guiLayersPointer = -1;
        int i = 0;
        while (i < 10)
        {
            Global._guiLayersNoBlack[i] = "";
            i++;
        }
    }

    public static bool _gui_isInterfaceOpened(string what)
    {
        int i = 0;
        while (i < 10)
        {
            if (Global._guiLayers[i] == what)
            {
                return true;
            }
            i++;
        }
        return false;
    }

    public static void GUILabelStroke(Rect rct, string stext, GUIStyle shadow, GUIStyle front)
    {
        GUI.Label(new Rect(rct.x, rct.y + 1, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x + 1, rct.y, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x, rct.y - 1, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x - 1, rct.y, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x + 1, rct.y + 1, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x - 1, rct.y - 1, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x - 1, rct.y + 1, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x + 1, rct.y - 1, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x, rct.y, rct.width, rct.height), stext, front);
    }

    public static void GUILabelStroke2(Rect rct, string stext, GUIStyle shadow, GUIStyle front)
    {
        GUI.Label(new Rect(rct.x, rct.y + 2, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x + 2, rct.y, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x, rct.y - 2, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x - 2, rct.y, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x + 2, rct.y + 2, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x - 2, rct.y - 2, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x - 2, rct.y + 2, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x + 2, rct.y - 2, rct.width, rct.height), stext, shadow);
        GUI.Label(new Rect(rct.x, rct.y, rct.width, rct.height), stext, front);
    }

    public static void setBlackSquareOnScreen(c_GuiCommonStyleController st)
    {
        GUI.Box(new Rect(-20, -20, Screen.width + 40, Screen.height + 40), "", st.cf_blackscreenelement); //, GUIStyle.none
    }

    public static void setBlackSquareOnCloseUp(c_GuiCommonStyleController st)
    {
        GUI.Box(new Rect(0, 0, Screen.width, 55 * Global.guiRatio), "", st.cf_blackscreenelement);
        GUI.Box(new Rect(0, 55 * Global.guiRatio, (Screen.width / 2) - (388 * Global.guiRatio), Screen.height), "", st.cf_blackscreenelement);
        GUI.Box(new Rect((Screen.width / 2) + (387 * Global.guiRatio), 55 * Global.guiRatio, (Screen.width / 2) - (386 * Global.guiRatio), Screen.height), "", st.cf_blackscreenelement);
        GUI.Box(new Rect((Screen.width / 2) - (388 * Global.guiRatio), 556 * Global.guiRatio, 775 * Global.guiRatio, Screen.height), "", st.cf_blackscreenelement);
    }

    public static int _guiLayersPointer_current;
    public static int _guiLayersPointer;
    public static string[] _guiLayers;
    public static string[] _guiLayersNoBlack;
    public static Dictionary<string, System.Action> _guiFuncs;
    public static bool _is_global_show_interface;
    public static Hashtable _commonStyles_store;
    public static GUIStyle generate_CommonStyles(string img, int sideW, int sideH)
    {
        if (!Global._commonStyles_store.ContainsKey(img))
        {
            GUIStyle someStyle = new GUIStyle();
            someStyle.stretchWidth = false;
            someStyle.fixedWidth = sideW;
            someStyle.fixedHeight = sideH;
            someStyle.normal.background = Global.gass.LoadAsset<Texture2D>(img);
            Global._commonStyles_store.Add(img, someStyle);
        }
        return (GUIStyle)Global._commonStyles_store[img];
    }

    public static GUIStyle getCommonIconStyle(string icon, int side)
    {
        return Global.generate_CommonStyles(icon, side, side);
    }

    public static GUIStyle getCommonImgStyle(string img, int sideW, int sideH)
    {
        return Global.generate_CommonStyles(img, sideW, sideH);
    }

    public static GUIStyle getCommonIconStyleNoSqare(string img, int sideW, int sideH)
    {
        return Global.generate_CommonStyles(img, sideW, sideH);
    }

    // =================================================================
    public static int _progressComplicatedLoader;
    public static string _labelComplicatedLoader;
    public static int random_loader;
    public static int random_didyouknow;
    // =================================================================
    public static bool _isClickDlg;
    public static IEnumerator ClickDlgDisable()
    {
        yield return new WaitForSeconds(0.5f);
        Global._isClickDlg = false;
    }

    public static bool _isClickExit;
    // =================================================================
    public static bool _isPopUpOpen;
    public static bool _isMainGui;
    public static bool _isInventoryOpen;
    public static bool _isWorldMap;
    public static bool _isGreenHouse;
    public static bool _isTemple;
    public static bool _isGreenHouseExit;
    public static bool _isTempleExit;
    public static bool _isUFOScreen;
    // =================================================================
    public static bool _isBegun;
    public static bool _isClickRes;
    public static GameObject crossDrop;
    public static float _system_distanceAttack;
    public static float _system_distanceTakeDrop;
    public static float _system_distanceSpeak;
    public static bool _isAttack01;
    public static bool _isAttack02;
    public static bool _isAttack03;
    // =================================================================
    public static bool _isRemotePier;
    public static GameObject _isRemotePierPlace;
    public static bool _isRemoteNPC;
    public static GameObject _isRemoteNPCPlace;
    public static GameObject startPoint;
    public static bool _isSea;
    public static bool _isIsland;
    // =================================================================
    public static bool _system_isHeroDead;
    public static bool _isMobDead;
    public static bool _system_isDead;
    public static int _paramPremiumCrystal;
    public static int _paramHeroLevel;
    public static string _paramHeroName;
    public static int _paramCurrentExp;
    public static int _paramNextLevelExp;
    public static int _paramHeroHunger;
    public static int _paramHeroThirst;
    public static int _paramHeroAthletics;
    public static int _paramHeroLiterature;
    public static int _paramHeroMaths;
    public static int _paramHeroNatural;
    public static int _paramHeroWorms;
    public static Hashtable _system_CharacterParams;
    public static int _system_LEVELINT;
    public static int _userCurrentHealth;
    public static int _userMaxHealth;
    public static int _userCurrentMind;
    public static int _userMaxMind;
    public static float _userCrit;
    public static int _userDropChance;
    public static float _difficultAttackCoeff;
    public static bool _isRemoteMeleeAttack;
    public static GameObject _isRemoteMeleeAttackNPC;
    public static bool _isUnderAttack;
    public static GameObject _fightMobPlace;
    public static bool _isAttackMob;
    public static bool _isGetHit;
    public static bool _isRemoteDrop;
    public static GameObject _isRemoteDropPlace;
    public static string lootType;
    public static int lootAmount;
    public static string lootName;
    public static bool _isClickLoot;
    public static bool _isStatusMegaHealth;
    public static bool _isAttackCobweb;
    public static bool _isRemoteCobwebAttack;
    public static GameObject _isRemoteCobweb;
    // =================================================================
    // INVENTORY
    public static Hashtable system_dummy;
    /*public static var system_dummy:Hashtable = new Hashtable({
        "shoes":new Array([{"droptype":"thing","type":"shoes","icon":"thing_icons_shoes_test","fromdrag":"inventory","isdropable":"true","x":"2", "y":"0", "uniqueID":"1","vid":"1","tooltip":"Великолепные сапоги для долгих переходов. Ноги не трут, от скорпионов защищают!", "position":"shoes"}])
    });*/
    public static object[] system_inventory;
    public static GUIStyle _InventoryItemIcon;
    public static System.Collections.Generic.List<Hashtable> globalInventoryThingsArr;
    //public static var globalInventoryThingsArr:Array = new Array([
    /*public static var globalInventoryThingsArr:ArrayList = new ArrayList([
        {"droptype":"thing","type":"premium","icon":"icons_crystal_premium","fromdrag":"inventory","isdropable":"false","x":"0", "y":"0", "uniqueID":"1","val":"30","name":"Кристаллы","selected":"false"},
        {"droptype":"thing","type":"boots","icon":"thing_icons_r4_leg","fromdrag":"inventory","isdropable":"true","x":"1", "y":"0", "uniqueID":"13","vid":"3","level":"1","natural":"15","name":"Улучшенная броня для ног","selected":"false"},
        {"droptype":"thing","type":"gloves","icon":"thing_icons_r4_hand","fromdrag":"inventory","isdropable":"true","x":"2", "y":"0", "uniqueID":"14","vid":"4","level":"1","natural":"15","name":"Улучшенная броня для рук","selected":"false"},
        {"droptype":"thing","type":"armor","icon":"thing_icons_r4_body","fromdrag":"inventory","isdropable":"true","x":"3", "y":"0", "uniqueID":"15","vid":"5","level":"1","natural":"15","name":"Улучшенная броня","selected":"false"}*/
     /*{"droptype":"thing","type":"shoes","icon":"thing_icons_shoes_test","fromdrag":"inventory","isdropable":"true","x":"0", "y":"0", "uniqueID":"13","vid":"3","level":"1","natural":"15","name":"Кроссовки натуралиста","selected":"false"},
        {"droptype":"thing","type":"backpack","icon":"thing_icons_backpack_test","fromdrag":"inventory","isdropable":"true","x":"1", "y":"0", "uniqueID":"14","vid":"4","level":"1","natural":"15","name":"Школьный рюкзак","selected":"false"},
        {"droptype":"thing","type":"food","icon":"thing_icons_food_test","fromdrag":"inventory","isdropable":"true","x":"2", "y":"0", "uniqueID":"15","vid":"5","level":"1","natural":"15","name":"Шеф-бургер","selected":"false"},
        {"droptype":"thing","type":"hat","icon":"thing_icons_hat_test","fromdrag":"inventory","isdropable":"true","x":"3", "y":"0", "uniqueID":"16","vid":"6","level":"1","natural":"15","name":"Кепка ума","selected":"false"},
        {"droptype":"thing","type":"pants","icon":"thing_icons_pants_test","fromdrag":"inventory","isdropable":"true","x":"4", "y":"0", "uniqueID":"17","vid":"7","level":"1","natural":"15","name":"Шорты силы","selected":"false"},
        {"droptype":"thing","type":"shirt","icon":"thing_icons_shirt_test","fromdrag":"inventory","isdropable":"true","x":"5", "y":"0", "uniqueID":"18","vid":"8","level":"1","natural":"15","name":"Футболка здоровья","selected":"false"},
        {"droptype":"thing","type":"weapon","icon":"thing_icons_weapon_test","fromdrag":"inventory","isdropable":"true","x":"0", "y":"1", "uniqueID":"19","vid":"9","level":"1","natural":"15","name":"Пушка-хлопушка","selected":"false"}*/
    //]);
    //public static var StorageInventoryArr:Array = new Array();
    //public static var StorageArr:Array = new Array();
    public static System.Collections.Generic.List<Hashtable> InventoryItemsArr;
    public static bool _isInventoryFull;
    public static float labelFullInventoryAlpha;
    public static System.Collections.Generic.List<Hashtable> StorageInventoryArr;
    public static System.Collections.Generic.List<Hashtable> StorageArr;
    public static bool _is_needUpdate_dummy;
    public static bool _is_needUpdate_invthings;
    public static Rect invSlot00Rect;
    public static Rect invSlot10Rect;
    public static Rect invSlot20Rect;
    public static Rect invSlot30Rect;
    public static Rect invSlot40Rect;
    public static Rect invSlot50Rect;
    public static Rect invSlot01Rect;
    public static Rect invSlot11Rect;
    public static Rect invSlot21Rect;
    public static Rect invSlot31Rect;
    public static Rect invSlot41Rect;
    public static Rect invSlot51Rect;
    public static Rect invSlot02Rect;
    public static Rect invSlot12Rect;
    public static Rect invSlot22Rect;
    public static Rect invSlot32Rect;
    public static Rect invSlot42Rect;
    public static Rect invSlot52Rect;
    public static Rect invSlot03Rect;
    public static Rect invSlot13Rect;
    public static Rect invSlot23Rect;
    public static Rect invSlot33Rect;
    public static Rect invSlot43Rect;
    public static Rect invSlot53Rect;
    public static Hashtable _someInventoryHashPlace_RectToCoords;
    public static Rect storageInvSlot00Rect;
    public static Rect storageInvSlot10Rect;
    public static Rect storageInvSlot20Rect;
    public static Rect storageInvSlot30Rect;
    public static Rect storageInvSlot01Rect;
    public static Rect storageInvSlot11Rect;
    public static Rect storageInvSlot21Rect;
    public static Rect storageInvSlot31Rect;
    public static Rect storageInvSlot02Rect;
    public static Rect storageInvSlot12Rect;
    public static Rect storageInvSlot22Rect;
    public static Rect storageInvSlot32Rect;
    public static Rect storageInvSlot03Rect;
    public static Rect storageInvSlot13Rect;
    public static Rect storageInvSlot23Rect;
    public static Rect storageInvSlot33Rect;
    public static Hashtable _someStorageInventoryHashPlace_RectToCoords;
    public static Rect storageSlot00Rect;
    public static Rect storageSlot10Rect;
    public static Rect storageSlot20Rect;
    public static Rect storageSlot30Rect;
    public static Rect storageSlot01Rect;
    public static Rect storageSlot11Rect;
    public static Rect storageSlot21Rect;
    public static Rect storageSlot31Rect;
    public static Rect storageSlot02Rect;
    public static Rect storageSlot12Rect;
    public static Rect storageSlot22Rect;
    public static Rect storageSlot32Rect;
    public static Rect storageSlot03Rect;
    public static Rect storageSlot13Rect;
    public static Rect storageSlot23Rect;
    public static Rect storageSlot33Rect;
    public static Hashtable _someStorageHashPlace_RectToCoords;
    public static Rect armorSlotRect;
    public static Rect glovesSlotRect;
    public static Rect bootsSlotRect;
    public static Rect weaponSlotRect;
    public static Rect featureSlotRect;
    public static Rect backpackSlotRect;
    public static Hashtable _someWearHashPlace_RectToStrplace;
    //public static var  _someWearHashPlace_RectToStrplace:Hashtable = new Hashtable({
    /*Rect(755*Global.guiRatio, 219*Global.guiRatio, 68*Global.guiRatio, 68*Global.guiRatio): "helm",
        Rect(755*Global.guiRatio, 293*Global.guiRatio, 68*Global.guiRatio, 68*Global.guiRatio): "cuirass",
        Rect(90, 193, 48, 48): "pants",
        Rect(755*Global.guiRatio, 367*Global.guiRatio, 68*Global.guiRatio, 68*Global.guiRatio): "shoes",
        Rect(755*Global.guiRatio, 145*Global.guiRatio, 68*Global.guiRatio, 68*Global.guiRatio): "weapon",

        Rect(294, 91, 48, 48): "shoulders",
        Rect(1085*Global.guiRatio, 219*Global.guiRatio, 68*Global.guiRatio, 68*Global.guiRatio): "amulet",
        Rect(1085*Global.guiRatio, 367*Global.guiRatio, 68*Global.guiRatio, 68*Global.guiRatio): "gloves",
        Rect(294, 244, 48, 48): "belt",
        Rect(1085*Global.guiRatio, 145*Global.guiRatio, 68*Global.guiRatio, 68*Global.guiRatio): "shield",

        Rect(141, 346, 48, 48): "cape",
        Rect(1085*Global.guiRatio, 293*Global.guiRatio, 68*Global.guiRatio, 68*Global.guiRatio): "ring",
        Rect(243, 346, 48, 48): "ring2"*/
    //});
    public static Rect itemDummySlot;
    public static bool isInvToDummy;
    public static Hashtable _someWearHashPlace_StrplaceToRect;
    //"hat": new Array([Rect(Screen.width/2 + 754, Screen.height/2 - 413, 159, 159)]),
    /*"armor": new Array([Rect(Screen.width/2 + 755, Screen.height/2 - 315, 160, 160)]),
        "gloves": new Array([Rect(Screen.width/2 + 755, Screen.height/2 - 135, 160, 160)]),
        "boots": new Array([Rect(Screen.width/2 + 755, Screen.height/2 + 45, 160, 160)]),

        "weapon": new Array([Rect(Screen.width/2 + 143, Screen.height/2 - 315, 160, 160)]),
        "feature": new Array([Rect(Screen.width/2 + 143, Screen.height/2 - 135, 160, 160)]),
        "backpack": new Array([Rect(Screen.width/2 + 143, Screen.height/2 + 45, 160, 160)])*/
    /*,
        "food": new Array([Rect(Screen.width/2 + 209, Screen.height/2 + 94, 159, 159)])*/
    public static float lastMouseDownObjectForDrag;
    public static bool _isInventoryThing;
    public static Hashtable someDropableObject;
    public static bool isOneDropablePlaceHoverNow;
    public static Hashtable someDropablePlace;
    public static bool _isWeCanDragAndDrop;
    public static bool _isDummyThing;
    public static bool _isStorageThing;
    public static bool _isFaceCharacterInventoryThings;
    public static Hashtable _placeHash_backMoveBetweenCells;
    public static Hashtable someDropableObjectBetween;
    public static bool _isDropStart;
    public static bool isMouseHold;
    // =================================================================
    public static Hashtable dlgCurrent;
    public static int _dlgLength;
    public static int _dlgCurrent;
    public static int _dlgID;
    //public static var _dlgSpeaker:GameObject;
    public static string _dlgSpeaker;
    public static string _dlgSpeakerName;
    public static string _dlgText;
    public static bool _dlgRun;
    public static string _dlgSpeakerImage;
    public static bool _dlgHeroIsMove;
    public static string _currentDlgName;
    public static string _dlgType;
    // =================================================================
    public static System.Collections.Generic.List<GameObject> _besideMobsList;
    public static System.Collections.Generic.List<GameObject> _besideNPCList;
    public static bool _showIsMobName;
    public static bool _showIsNPCName;
    // =================================================================
    public static float _critAddControlRandom;
    public static float _sliceAddControlRandom;
    public static int _critAddControlRandom_hitavoidance;
    public static int _sliceAddControlRandom_hitavoidance;
    // =================================================================
    public static bool _showNpcBar;
    public static bool isNpcBarShow;
    public static bool isNpcNameShow;
    public static int growDamage;
    public static int growDamage_HowManyADD;
    public static int growDamage_HowManySUB;
    public static int growDamagePlus;
    public static int growDamageMaxStrikeCounts;
    public static int growDamageMaxStrikeCountsPlus;
    public static int growDamageCurStrikeCounts;
    public static bool _isBattleRagesOn;
    public static int _TempDistance;
    public static int _TempDamageMin;
    public static int _TempDamageMax;
    public static int _TempDefence;
    public static bool _isPlayerSpell;
    public static bool _isHandSpell;
    public static GameObject spellFlyGO;
    public static GameObject spellBumGO;
    public static GameObject spellBookGO;
    public static GameObject spellBone;
    public static Vector3 MobPos;
    public static bool _hasAttackSomeOneByHero;
    public static Hashtable _dropToonsScripts;
    public static Hashtable _npcsToonsScripts;
    // =================================================================
    public static bool _isAbility01Select;
    public static float Ability01Cooldown;
    public static float Ability02Cooldown;
    public static float Ability03Cooldown;
    public static float Ability04Cooldown;
    public static float Ability05Cooldown;
    public static float Ability06Cooldown;
    public static bool _isAbility01;
    public static bool _isAbility02;
    public static bool _isAbility03;
    public static bool _isAbility04;
    public static bool _isAbility05;
    public static bool _isAbility06;
    // =================================================================
    public static bool _isRecipeSelect;
    public static string _currentRecipeThingName;
    public static string _currentRecipeName;
    public static string _currentRecipeDroptype;
    public static string _currentRecipeType;
    //public static var _currentRecipeIcon:String;
    public static GUIStyle _currentRecipeIcon;
    public static string _currentRecipeUniqueID;
    public static string _currentRecipeDescription;
    public static int _currentRecipeIndex;
    public static string _currentRecipeCostType1;
    public static string _currentRecipeCostAmount1;
    public static string _currentRecipeCostName1;
    public static GUIStyle _currentRecipeCostIcon1;
    public static string _currentRecipeCostType2;
    public static string _currentRecipeCostAmount2;
    public static string _currentRecipeCostName2;
    public static GUIStyle _currentRecipeCostIcon2;
    public static string _currentRecipeCostType3;
    public static string _currentRecipeCostName3;
    public static string _currentRecipeCostAmount3;
    public static GUIStyle _currentRecipeCostIcon3;
    public static string _currentRecipeCostType4;
    public static string _currentRecipeCostAmount4;
    public static string _currentRecipeCostName4;
    public static GUIStyle _currentRecipeCostIcon4;
    // Параметры, зависимые от типа предмета
    public static string _currentRecipeAddSlots; //Для ранцев
    public static string _currentRecipeAddAbility; //Для "фичи"
    public static string _currentRecipeAddDamage; //Для оружия
    public static object[] _recipeTest;
    //public static var _Consumables:Array = new Array([
    public static System.Collections.Generic.List<Hashtable> _Consumables;
    public static bool _isLootMetal;
    public static bool _isLootPlastic;
    public static bool _isLootCobweb;
    public static bool _isLootDeadspider;
    public static bool _isLootEballon;
    public static bool _isLootCactus;
    public static Hashtable _userResourcesCraft;
    public static GUIStyle currentLootIcon;
    public static string currentLootName;
    public static GUIStyle _RecipeItemIcon01; //Ранец
    public static GUIStyle _RecipeItemIcon02; //Активатор
    public static GUIStyle _RecipeItemIcon03; //Бластер
    public static GUIStyle _RecipeItemIcon04; //Ранец-аптечка
    public static GUIStyle _RecipeItemIcon05; //Парализатор
    public static GUIStyle _RecipeCostIcon; //Компонент
    //public static var _recipeItems:Array = new Array([
    public static System.Collections.Generic.List<Hashtable> _recipeItems;
    /*{ "droptype": "thing", "type": "feature", "icon": Global._RecipeItemIcon02, "fromdrag": "inventory", "isdropable": "true", "available": "false", "addability": "all", "costtype1": "loot_cobweb", "costamount1": "4", "costicon1": Global._RecipeCostIcon, "costtype2": "loot_deadspider", "costamount2": "1", "costicon2": Global._RecipeCostIcon, "costtype3": "loot_metal", "costamount3": "4", "costicon3": Global._RecipeCostIcon, "costtype4": "none", "costamount4": "0", "costicon4": Global._RecipeCostIcon,"name":"Активатор","description":"Предназначен для демонстрации боевых способностей героя.","level":"1"},*/
    public static GUIStyle _RecipeFoodIcon; //Иконка еды
    //public static var _recipeFood:Array = new Array([
    public static System.Collections.Generic.List<Hashtable> _recipeFood;
    public static string _currentResLootName;
    public static GUIStyle _currentResLootIcon;
    public static string _currentResLootVal;
    public static bool _isFoodBanana;
    public static bool _isFoodBerries;
    public static bool _isFoodCabbage;
    public static bool _isFoodCarrot;
    public static bool _isFoodChicken;
    public static bool _isFoodEgg;
    public static bool _isFoodFish;
    public static bool _isFoodFlour;
    public static bool _isFoodMilk;
    public static bool _isFoodMushrooms;
    public static bool _isFoodOnion;
    public static bool _isFoodOrange;
    public static bool _isFoodPotatoes;
    public static bool _isFoodSourcream;
    public static bool _isFoodSpice;
    public static bool _isFoodBeans;
    public static bool _isFoodChickpea;
    public static bool _isFoodCorn;
    public static bool _isFoodKidneyBeans;
    public static bool _isFoodLentils;
    public static bool _isFoodPeanut;
    public static bool _isFoodPeas;
    public static Hashtable _userResourcesFood;
    public static bool _isRecipeFoodSelect;
    public static float currentRecipeX;
    public static float currentRecipeY;
    public static string currentFoodRecipeName;
    public static string currentFoodRecipeDescription;
    public static GUIStyle currentFoodRecipeIcon;
    public static string currentFoodRecipeCostType1;
    public static string currentFoodRecipeCostAmount1;
    public static string currentFoodRecipeCostName1;
    public static GUIStyle currentFoodRecipeCostIcon1;
    public static string currentFoodRecipeCostType2;
    public static string currentFoodRecipeCostAmount2;
    public static string currentFoodRecipeCostName2;
    public static GUIStyle currentFoodRecipeCostIcon2;
    public static string currentFoodRecipeCostType3;
    public static string currentFoodRecipeCostAmount3;
    public static string currentFoodRecipeCostName3;
    public static GUIStyle currentFoodRecipeCostIcon3;
    public static string currentFoodRecipeCostType4;
    public static string currentFoodRecipeCostAmount4;
    public static string currentFoodRecipeCostName4;
    public static GUIStyle currentFoodRecipeCostIcon4;
    public static string currentFoodRecipeAddHealth;
    public static string currentFoodRecipeVal;
    public static bool _isTabCraftSelect;
    public static bool _isTabMakeoutSelect;
    // =================================================================
    public static bool _isLibraryHealthSelect;
    public static bool _isLibraryBiologySelect;
    public static bool _isLibraryGeoSelect;
    public static bool _isLibraryTechSelect;
    public static bool _isLibraryHistorySelect;
    public static GUIStyle bookTitleImage;
    public static Hashtable _userBooksHealth;
    public static Hashtable _userBooksBiology;
    public static Hashtable _userBooksTech;
    // =================================================================
    public static GameObject _selectedDropPlace;
    public static object[] _selectedDropHashArray;
    public static Stack<object> _dropPlacePoses;
    public static int _globalVidIndexForPoisonsAndSpells;
    public static int _globalVidIndexForChest;
    public static bool _isTakeDrop;
    public static bool _isTakeLoot;
    public static Vector3 labelLootPos;
    public static float labelLootAlpha;
    public static GUIStyle lootIcon;
    //public static var _recipeDropLoot:Array = new Array([
    public static System.Collections.Generic.List<Hashtable> _recipeDropLoot;
    /*public static var _Loot:Array = new Array([
    ]);*/
    public static System.Collections.Generic.List<Hashtable> _Loot;
    public static bool _isLootSelect;
    public static int _currentLootIndex;
    public static string _currentLootLootName;
    public static string _currentLootDroptype;
    public static string _currentLootType;
    public static string _currentLootIcon;
    public static string _currentLootUniqueID;
    public static string _currentLootType1;
    public static string _currentLootAmount1;
    public static string _currentLootType2;
    public static string _currentLootAmount2;
    public static string _currentLootType3;
    public static string _currentLootAmount3;
    public static string _currentLootName;
    public static string _currentLootDescription;
    public static bool _isShowLoot;
    public static float showTime;
    // =================================================================
    public static bool _isClickVegetable;
    // VEGETABLES MINI GAME
    public static GUIStyle VeGameWinIcon;
    public static GUIStyle VeGameLooseIcon;
    public static bool _isShowHint;
    public static GUIStyle VeGameVegetable_01;
    public static GUIStyle VeGameVegetable_02;
    public static GUIStyle VeGameVegetable_03;
    public static GUIStyle VeGameVegetable_01_icon;
    public static GUIStyle VeGameVegetable_02_icon;
    public static GUIStyle VeGameVegetable_03_icon;
    public static bool _isVeGame01;
    public static bool _isVeGame02;
    public static bool _isVeGame03;
    public static bool _isVeGame04;
    public static bool _isVeGame05;
    public static bool _isVeGame06;
    public static string VeGameTitle;
    // =================================================================
    public static bool _isTakeVegetable;
    public static bool _isRemoteVegetable;
    public static GameObject _isRemoteVegetablePlace;
    public static GameObject _selectedVegetable;
    public static string VegetableType;
    public static int VegetableAmount;
    public static string VegetableName;
    public static bool _isTakeKey;
    public static bool _isRemoteKey;
    public static GameObject _isRemoteKeyPlace;
    public static GameObject _selectedKey;
    public static bool _isClickKey;
    public static bool _isGetKey;
    public static GameObject dungeonKey;
    public static bool _isTakeAnimal;
    public static bool _isRemoteAnimal;
    public static GameObject _isRemoteAnimalPlace;
    public static GameObject _selectedAnimal;
    public static bool _isClickAnimal;
    public static string AnimalType;
    public static int AnimalAmount;
    public static string AnimalName;
    // =================================================================
    public static GameObject _TempleLockedDoor;
    // MUSEUM MINI GAME
    public static GUIStyle MuseumGameWinIcon;
    public static GUIStyle MuseumGameLooseIcon;
    public static GUIStyle MuseumGameItem_01;
    public static GUIStyle MuseumGameItem_02;
    public static GUIStyle MuseumGameItem_03;
    public static GUIStyle MuseumGameItem_01_icon;
    public static GUIStyle MuseumGameItem_02_icon;
    public static GUIStyle MuseumGameItem_03_icon;
    public static bool _isMuseumGame01;
    public static bool _isMuseumGame02;
    public static bool _isMuseumGame03;
    public static string MuseumGameTitle;
    public static bool _isMuseumGameComplete;
    // BOXROOM MINI GAME
    public static GUIStyle BoxroomGameWinIcon;
    public static GUIStyle BoxroomGameLooseIcon;
    public static GUIStyle BoxroomGameItem_01;
    public static GUIStyle BoxroomGameItem_02;
    public static GUIStyle BoxroomGameItem_03;
    public static GUIStyle BoxroomGameItem_01_icon;
    public static GUIStyle BoxroomGameItem_02_icon;
    public static GUIStyle BoxroomGameItem_03_icon;
    public static bool _isBoxroomGame01;
    public static bool _isBoxroomGame02;
    public static bool _isBoxroomGame03;
    public static string BoxroomGameTitle;
    public static bool _isBoxroomGameComplete;
    // MUSHROOMS MINI GAME
    public static GUIStyle MushroomsGameWinIcon;
    public static GUIStyle MushroomsGameLooseIcon;
    public static GUIStyle MushroomsGameItem_01;
    public static GUIStyle MushroomsGameItem_02;
    public static GUIStyle MushroomsGameItem_03;
    public static GUIStyle MushroomsGameItem_01_icon;
    public static GUIStyle MushroomsGameItem_02_icon;
    public static GUIStyle MushroomsGameItem_03_icon;
    public static bool _isMushroomsGame01;
    public static bool _isMushroomsGame02;
    public static bool _isMushroomsGame03;
    public static string MushroomsGameTitle;
    public static bool _isMushroomsGameComplete;
    // =================================================================
    // TUTORIAL GAMEPLAY
    public static bool _isTutorialShowing;
    public static bool _isFirstGameStart;
    public static bool _isTutorial01;
    public static bool _isTutorial02;
    public static bool _isTutorial03;
    public static bool _isTutorial04;
    public static bool _isTutorial05;
    public static bool _isTutorial06;
    public static bool _isTutorial06_1;
    public static bool _isTutorial07;
    public static bool _isTutorial07_fucker;
    public static bool _isTutorial08;
    public static bool _isTutorial09;
    public static bool _isTutorialCreateSoup;
    public static bool _isTutorial10;
    public static bool _isTutorial11;
    public static bool _isTutorial12;
    public static bool _isTutorialLearnSmousi;
    public static bool _isTutorial13;
    public static bool _isTutorial14;
    public static bool _isTutorial15;
    public static bool _isTutorial16;
    public static bool _isTutorial17;
    public static bool _isTutorial18;
    public static bool _isTutorial19;
    public static bool _isTutorial20;
    public static bool _isTutorial21;
    public static bool _isTutorial22;
    public static bool _isTutorial23;
    public static bool _isTutorial24;
    public static bool _isTutorial25;
    public static bool _isTutorial26;
    public static bool _isTutorial27;
    public static bool _isTutorial28;
    public static bool _isTutorial29;
    public static bool _isTutorial30;
    public static bool _isTutorialVeGameEnd;
    public static bool _isEnterTutorialBattleDialogue;
    public static bool _isTutorial31;
    public static bool _isTutorial32;
    public static bool _isTutorial33;
    public static bool _isTutorial33Video;
    public static bool _isTutorial34;
    public static bool _isTutorial35;
    public static bool _isTutorial36;
    public static bool _isTutorial37;
    public static bool _isTutorial37Video;
    public static bool _isTutorial38;
    public static bool _isTutorial38_1;
    public static bool _isTutorial39;
    public static bool _isTutorial40;
    public static bool _isTutorial41;
    public static bool _isTutorial42;
    public static bool _isTutorial43;
    public static bool _isTutorial44;
    public static bool _isTutorial45;
    public static bool _isTutorialStorage;
    public static bool _isTutorialInventory;
    public static bool _isTutorialCutScene01;
    public static GameObject _isTutorialGreenHouse;
    public static bool _isTutorialVeGameComplete;
    public static bool _isTutorialKitchenRecipe1;
    public static bool _isTutorialKitchenRecipe2;
    public static bool _isTutorialKitchenRecipe3;
    public static bool _isTutorialKitchenRecipe4;
    public static bool _isTutorial10ClickRecipe;
    public static bool _isTutorial10ClickCreate;
    public static bool _isTutorial13ClickFlash;
    public static bool _isTutorial13ClickMult;
    public static bool _isTutorialBattleDialogue;
    public static bool _isTutorialSpidersBookSelect;
    public static bool _isTutorialSpidersBookComplete;
    public static bool _isTutorialWorkroomRecipeSelect;
    public static bool _isTutorialSkafBookSelect;
    public static bool _isTutorialSkafBookComplete;
    public static bool _isTutorialWorkroomRecipeSelect2;
    public static bool _isTutorialGreenHouseComplete;
    public static bool _isTutorialUFOStorageComplete;
    public static string TutorialBlasterXCoord;
    public static string TutorialBlasterYCoord;
    public static Hashtable TutorialBlasterThingInv;
    public static bool _isTutorialGoToGreenHouseAgain;
    public static GameObject _isTutorialCobweb;
    public static bool _isTutorialDestroyCobweb;
    public static bool _isTutorialCobwebArrow;
    public static bool _isTutorialCobwebArrowSetCoords;
    public static bool _isTutorialDialogueSpider;
    public static bool _isTutorialDialogueSpiderEnd;
    public static GameObject _TutorialSpider;
    public static bool _isTutorialSpiderAttack;
    public static bool _isTutorialSpiderClick;
    public static bool _isTutorialAbilityClick;
    public static bool _isTutorialWaitingForTheSun;
    public static bool _isTutorialMushroomGameEnd;
    public static bool _isTutorialMushroomVideo;
    public static bool _isTutorialEnd;
    public static bool _isVillaFirstVisit;
    public static bool _isVillaAllFound;
    public static float fuckerXCoord;
    public static float fuckerYCoord;
    public static bool animationBack;
    public static float FuckerYcoordOriginal;
    public static bool fuckerCall;
    public static GameObject GreenHouseEntry;
    public static bool GreenHouseOpenDoor;
    public static GameObject GreenHouseExit;
    public static bool GreenHouseExitDoor;
    public static GameObject VillaEntry;
    public static bool VillaOpenDoor;
    public static GameObject UFOEntry;
    public static bool UFOOpenDoor;
    public static bool UFOShowEntry;
    public static GameObject TemplePyramid;
    public static GameObject TempleEntry;
    public static bool TempleOpenDoor;
    public static bool _isFirstVisitTemple;
    public static bool _isClosedDoorDialogue;
    public static GameObject TempleExit;
    public static bool TempleExitDoor;
    public static GameObject templePoint;
    public static GameObject FishingEntry;
    public static bool FishingAvailable;
    public static bool _isFirstFishing;
    public static GameObject MosaicEntry;
    public static bool MosaicAvailable;
    public static bool MosaicComplete;
    public static bool MosaicFirstEntry;
    public static bool _isStorageFaraway;
    // =================================================================
    public static bool _isFeatureEquiped;
    // =================================================================
    public static int _Mosaic1Index;
    public static int _Mosaic2Index;
    public static int _Mosaic3Index;
    public static int _Mosaic4Index;
    public static int _Mosaic5Index;
    public static int _Mosaic6Index;
    public static int _Mosaic7Index;
    public static int _Mosaic8Index;
    public static int _Mosaic9Index;
    // =================================================================
    public static bool _isHOG1Complete;
    public static bool _isHOG2Complete;
    public static bool _isHOG3Complete;
    // HOG1 items
    public static bool _isHOG1Item1Found;
    public static bool _isHOG1Item2Found;
    public static bool _isHOG1Item3Found;
    public static bool _isHOG1Item4Found;
    public static bool _isHOG1Item5Found;
    public static bool _isHOG1Item6Found;
    public static bool _isHOG1Item7Found;
    public static bool _isHOG1Item8Found;
    public static bool _isHOG1Item1Click;
    public static bool _isHOG1Item2Click;
    public static bool _isHOG1Item3Click;
    public static bool _isHOG1Item4Click;
    public static bool _isHOG1Item5Click;
    public static bool _isHOG1Item6Click;
    public static bool _isHOG1Item7Click;
    public static bool _isHOG1Item8Click;
    public static float HOG1Item1Alpha;
    public static float HOG1Item2Alpha;
    public static float HOG1Item3Alpha;
    public static float HOG1Item4Alpha;
    public static float HOG1Item5Alpha;
    public static float HOG1Item6Alpha;
    public static float HOG1Item7Alpha;
    public static float HOG1Item8Alpha;
    // HOG2 items
    public static bool _isHOG2Item1Found;
    public static bool _isHOG2Item2Found;
    public static bool _isHOG2Item3Found;
    public static bool _isHOG2Item4Found;
    public static bool _isHOG2Item5Found;
    public static bool _isHOG2Item6Found;
    public static bool _isHOG2Item1Click;
    public static bool _isHOG2Item2Click;
    public static bool _isHOG2Item3Click;
    public static bool _isHOG2Item4Click;
    public static bool _isHOG2Item5Click;
    public static bool _isHOG2Item6Click;
    public static float HOG2Item1Alpha;
    public static float HOG2Item2Alpha;
    public static float HOG2Item3Alpha;
    public static float HOG2Item4Alpha;
    public static float HOG2Item5Alpha;
    public static float HOG2Item6Alpha;
    // HOG3 items
    public static bool _isHOG3Item1Found;
    public static bool _isHOG3Item2Found;
    public static bool _isHOG3Item3Found;
    public static bool _isHOG3Item4Found;
    public static bool _isHOG3Item5Found;
    public static bool _isHOG3Item6Found;
    public static bool _isHOG3Item7Found;
    public static bool _isHOG3Item8Found;
    public static bool _isHOG3Item1Click;
    public static bool _isHOG3Item2Click;
    public static bool _isHOG3Item3Click;
    public static bool _isHOG3Item4Click;
    public static bool _isHOG3Item5Click;
    public static bool _isHOG3Item6Click;
    public static bool _isHOG3Item7Click;
    public static bool _isHOG3Item8Click;
    public static float HOG3Item1Alpha;
    public static float HOG3Item2Alpha;
    public static float HOG3Item3Alpha;
    public static float HOG3Item4Alpha;
    public static float HOG3Item5Alpha;
    public static float HOG3Item6Alpha;
    public static float HOG3Item7Alpha;
    public static float HOG3Item8Alpha;
    public static GUIStyle HOGItemIcon;
    public static string HOGItemName;
    public static float hogFXsize;
    public static bool _isShowHintHOG;
    public static float hogFXcoord;
    public static float HOGhintAlpha;
    // =================================================================
    public static bool _isViewZoeStory;
    public static bool _isLearnZoeStory;
    // =================================================================
    public static GUIStyle exhibitTitleImage;
    public static Hashtable _userMuseum;
    // ПЕРЕОДЕВАЛКА ===================================================
    public static GameObject r3_backpack;
    public static GameObject r3_body;
    public static GameObject r3_gun;
    public static GameObject r3_hand;
    public static GameObject r3_leg;
    public static GameObject r4_backpack;
    public static GameObject r4_body;
    public static GameObject r4_gun;
    public static GameObject r4_hand;
    public static GameObject r4_leg;
    public static GameObject r5_backpack;
    public static GameObject inv_r3_backpack;
    public static GameObject inv_r3_body;
    public static GameObject inv_r3_gun;
    public static GameObject inv_r3_hand;
    public static GameObject inv_r3_leg;
    public static GameObject inv_r4_backpack;
    public static GameObject inv_r4_body;
    public static GameObject inv_r4_gun;
    public static GameObject inv_r4_hand;
    public static GameObject inv_r4_leg;
    public static GameObject inv_r5_backpack;
    public static bool _isR3Backpack;
    public static bool _isR3Body;
    public static bool _isR3Gun;
    public static bool _isR3Hand;
    public static bool _isR3Leg;
    public static bool _isR4Backpack;
    public static bool _isR4Body;
    public static bool _isR4Gun;
    public static bool _isR4Hand;
    public static bool _isR4Leg;
    public static bool _isR5Backpack;
    // ПРОСЧЕТ РАССТОЯНИЯ ДО МОБА (и до паутины) ===================================================
    public static float someDistanceHeroMob;
    public static float someDistanceHeroCobweb;
    // Эффект защиты ===================================================
    public static GameObject rr_backpack_shield;
    public static GameObject rr_body_shield;
    public static GameObject rr_gun_shield;
    public static GameObject rr_hand_shield;
    public static GameObject rr_leg_shield;
    // Эффект невидимость ===================================================
    public static GameObject rr_backpack_invisible;
    public static GameObject rr_body_invisible;
    public static GameObject rr_gun_invisible;
    public static GameObject rr_hand_invisible;
    public static GameObject rr_leg_invisible;
    // =================================================================
    public static GameObject TraverseHierarchy(Transform root, string findObjName)
    {
        foreach (Transform child in root)
        {
            if (findObjName == child.name)
            {
                return child.gameObject;
            }
            GameObject someObj = Global.TraverseHierarchy(child, findObjName);
            if (someObj != null)
            {
                return someObj;
            }
        }
        return null;
    }

    public static void TurnOffEmittersHierarchy(Transform root)
    {
        foreach (Transform child in root)
        {
            if ((child != null) && (child.gameObject != null))
            {
                ParticleSystem someEmitter = (ParticleSystem) child.gameObject.GetComponent(typeof(ParticleSystem));
                //if (someEmitter != null) someEmitter.Stop();//someEmitter.emit = false;
                Global.TurnOffEmittersHierarchy(child);
            }
        }
    }

    public static void WaiterDestroyer(GameObject someObj, float dtime)
    {
        if (someObj != null)
        {
            UnityEngine.Object.Destroy(someObj, dtime);
        }
    }

    public static void AnimationDeathWaiterDestroyer(GameObject someObj, float dtime)
    {
        iTween.MoveTo(someObj, new Hashtable() { {"x", someObj.transform.position.x },  {"y", someObj.transform.position.y - 6 },  {"z", someObj.transform.position.z },  {"time", dtime },  {"delay", 0 },  {"easetype", "linear" }, });
        if (someObj != null)
        {
            UnityEngine.Object.Destroy(someObj, dtime + 1);
        }
    }

    // =================================================================
    public static float _minimapMapX;
    public static float _minimapMapY;
    public static float _minimapHeroX;
    public static float _minimapHeroY;
    public static bool _showMiniMap;
    public static bool _showBigMap;
    // =================================================================
    public static GameObject buildingPyramid;
    public static bool _isOpenPyramid;
    public static float _zasov1X;
    public static float _zasov2X;
    public static float _zasov3X;
    public static bool _isOpenZasov1;
    public static bool _isOpenZasov2;
    public static bool _isOpenZasov3;
    public static bool _isZasov1;
    public static bool _isZasov2;
    public static bool _isZasov3;
    // =================================================================
    public static bool _isGetNewLevel;
    public static float labelNewLevelAlpha;
    // =================================================================
    public static bool _isIntro1;
    public static bool _isIntro2;
    public static bool _isIntro3;
    public static bool _isContinueGame;
    // =================================================================
    public static bool _isGameSaved;
    public static float labelGameSavedAlpha;
    public static bool _isViewFromLibrary;
    // =================================================================
    public static string _GAME_CURSOR;
    // =================================================================
    public static float dialogueLength;
    public static GameObject speakerGO;
    public static GameObject holoGO;
    public static bool _isKot;
    public static bool _isChangeChar;
    public static bool _isSpeak;
    public static bool _isClickDialogue;
    public static bool _isPlayVideo;
    public static Hashtable testGera;
    static Global()
    {
        Global._options_sound_volume_orig = 20;
        Global._options_sound_volume = 20;
        Global._options_is_fullscreen = true;
        Global._options_is_gamecursor = true;
        Global._options_is_quality = true;
        Global._isSoundOn = true;
        Global._isMusicOn = true;
        Global._mouseX_pos_for_music_slide = 184;
        Global._mouseX_pos_for_sound_slide = 184;
        Global._mouseX_pos_for_music_slide_off = 184;
        Global._music_volume_slide = 60;
        Global._sound_volume_slide = 70;
        Global._isGameFocused = true;
        Global._globalDestroyer = new Hashtable();
        Global._globalDestroyer_groups = new Hashtable();
        Global._heroismove = true;
        Global._heroTargetPoint = new Vector3();
        Global._isMoveAllowed = true;
        Global._guiLayersPointer_current = -1;
        Global._guiLayersPointer = -1;
        Global._guiLayers = new string[10];
        Global._guiLayersNoBlack = new string[10];
        Global._guiFuncs = new Dictionary<string, Action>();
        Global._is_global_show_interface = true;
        Global._commonStyles_store = new Hashtable();
        Global._labelComplicatedLoader = "";
        Global.random_loader = 1;
        Global.random_didyouknow = 1;
        Global._isClickDlg = true;
        Global._isClickExit = true;
        Global._isWorldMap = true;
        Global._isBegun = true;
        Global._isClickRes = true;
        Global._system_distanceAttack = 5;
        Global._system_distanceTakeDrop = 2;
        Global._system_distanceSpeak = 1;
        Global._isAttack01 = true;
        Global._isSea = true;
        Global._paramPremiumCrystal = 69;
        Global._paramHeroLevel = 1;
        Global._paramHeroName = "Herbert Hoover";
        Global._paramNextLevelExp = 100;
        Global._paramHeroHunger = 69;
        Global._paramHeroThirst = 69;
        Global._paramHeroAthletics = 3;
        Global._paramHeroLiterature = 4;
        Global._paramHeroMaths = 6;
        Global._paramHeroNatural = 4;
        Global._paramHeroWorms = 969;
        Global._system_CharacterParams = new Hashtable(new Hashtable() { {"attack", 10 },  {"armor", 1 },  {"dodge", 1 },  {"stamina", 1 },  {"fortune", 1 },  {"damagemin", 1 },  {"damagemax", 3 },  {"magicdamage", 0 },  {"poisondamage", 0 },  {"firedamage", 0 },  {"damagehuman", 0 },  {"damagemage", 0 },  {"damageanimal", 0 },  {"defence", 0 },  {"critchance", 1 },  {"crit", 1 },  {"skillpoints", 0 },  {"pname", "User Name" },  {"sex", "man" }, });
        Global._system_LEVELINT = 5;
        Global._userCurrentHealth = 100;
        Global._userMaxHealth = 100;
        Global._userCurrentMind = 100;
        Global._userMaxMind = 100;
        Global._userCrit = 50f;
        Global._userDropChance = 100;
        Global._difficultAttackCoeff = 1f;
        Global._isAttackCobweb = true;
        Global.system_dummy = new Hashtable();
        Global.system_inventory = new object[0];
        Global._InventoryItemIcon = new GUIStyle();
        Global.globalInventoryThingsArr = new List<Hashtable>(new Hashtable[] {new Hashtable() { {"droptype", "thing" },  {"type", "premium" },  {"thingname", "crystal_premium" },  {"icon", Global._InventoryItemIcon },  {"fromdrag", "inventory" },  {"isdropable", "false" },  {"x", "0" },  {"y", "0" },  {"uniqueID", "1" },  {"val", "30" },  {"name", "Кристаллы" },  {"selected", "false" },  {"level", "1" }, }, new Hashtable() { {"droptype", "thing" },  {"type", "boots" },  {"thingname", "thing_r4_leg" },  {"icon", Global._InventoryItemIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "1" },  {"y", "0" },  {"uniqueID", "13" },  {"vid", "3" },  {"level", "1" },  {"armor", "3" },  {"health", "7" },  {"costtype1", "loot_metal" },  {"costamount1", "6" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "loot_plastic" },  {"costamount2", "2" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "loot_cactus" },  {"costamount3", "2" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Улучшенная броня для ног" },  {"selected", "false" }, }, new Hashtable() { {"droptype", "thing" },  {"type", "gloves" },  {"thingname", "thing_r4_hand" },  {"icon", Global._InventoryItemIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "2" },  {"y", "0" },  {"uniqueID", "14" },  {"vid", "4" },  {"level", "1" },  {"armor", "2" },  {"health", "5" },  {"costtype1", "loot_metal" },  {"costamount1", "4" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "loot_plastic" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "loot_cactus" },  {"costamount3", "1" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Улучшенная броня для рук" },  {"selected", "false" }, }, new Hashtable() { {"droptype", "thing" },  {"type", "armor" },  {"thingname", "thing_r4_body" },  {"icon", Global._InventoryItemIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "3" },  {"y", "0" },  {"uniqueID", "15" },  {"vid", "5" },  {"level", "1" },  {"armor", "5" },  {"health", "15" },  {"costtype1", "loot_metal" },  {"costamount1", "8" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "loot_plastic" },  {"costamount2", "4" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "loot_cactus" },  {"costamount3", "5" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "loot_eballon" },  {"costamount4", "2" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Улучшенная броня" },  {"selected", "false" }, }, new Hashtable() { {"droptype", "thing" },  {"type", "gloves" },  {"thingname", "thing_r3_hand" },  {"icon", Global._InventoryItemIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"x", "4" },  {"y", "0" },  {"uniqueID", "16" },  {"vid", "6" },  {"level", "1" },  {"armor", "1" },  {"health", "3" },  {"costtype1", "loot_metal" },  {"costamount1", "4" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "loot_plastic" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "none" },  {"costamount3", "0" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Броня для рук" },  {"selected", "false" }, }});
        Global.InventoryItemsArr = new List<Hashtable>();
        Global.labelFullInventoryAlpha = 1f;
        Global.StorageArr = new List<Hashtable>();
        Global._someInventoryHashPlace_RectToCoords = new Hashtable();
        Global._someStorageInventoryHashPlace_RectToCoords = new Hashtable();
        Global._someStorageHashPlace_RectToCoords = new Hashtable();
        Global._someWearHashPlace_RectToStrplace = new Hashtable();
        Global.itemDummySlot = new Rect();
        Global._someWearHashPlace_StrplaceToRect = new Hashtable(new Hashtable() { {"armor", Global.itemDummySlot },  {"gloves", Global.itemDummySlot },  {"boots", Global.itemDummySlot },  {"weapon", Global.itemDummySlot },  {"feature", Global.itemDummySlot },  {"backpack", Global.itemDummySlot }, });
        Global._isInventoryThing = true;
        Global._isDummyThing = true;
        Global._isStorageThing = true;
        Global._isFaceCharacterInventoryThings = true;
        Global._placeHash_backMoveBetweenCells = new Hashtable();
        Global.someDropableObjectBetween = new Hashtable();
        Global.dlgCurrent = new Hashtable();
        Global._dlgSpeaker = "";
        Global._dlgSpeakerName = "";
        Global._dlgText = "";
        Global._dlgSpeakerImage = "";
        Global._dlgHeroIsMove = true;
        Global._currentDlgName = "";
        Global._dlgType = "";
        Global._besideMobsList = new List<GameObject>();
        Global._besideNPCList = new List<GameObject>();
        Global.growDamage_HowManyADD = 3;
        Global.growDamage_HowManySUB = 1;
        Global.growDamagePlus = 10;
        Global.growDamageMaxStrikeCounts = 10;
        Global.growDamageMaxStrikeCountsPlus = 2;
        Global.MobPos = new Vector3();
        Global._dropToonsScripts = new Hashtable();
        Global._npcsToonsScripts = new Hashtable();
        Global._currentRecipeThingName = "";
        Global._currentRecipeIcon = new GUIStyle();
        Global._currentRecipeCostIcon1 = new GUIStyle();
        Global._currentRecipeCostIcon2 = new GUIStyle();
        Global._currentRecipeCostIcon3 = new GUIStyle();
        Global._currentRecipeCostIcon4 = new GUIStyle();
        Global._recipeTest = new Hashtable[] {new Hashtable() { {"droptype", "thing" },  {"type", "tool" },  {"icon", "icons_tool_01" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"costtype1", "instr_metal" },  {"costamount1", "5" },  {"costtype2", "instr_plastic" },  {"costamount2", "2" },  {"costtype3", "instr_rubber" },  {"costamount3", "1" },  {"name", "Стальной молоток" },  {"description", "Сгодится, чтобы забить гвоздь." }, }, new Hashtable() { {"droptype", "thing" },  {"type", "tool" },  {"icon", "icons_tool_02" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"costtype1", "instr_metal" },  {"costamount1", "3" },  {"costtype2", "instr_wood" },  {"costamount2", "7" },  {"costtype3", "instr_insulating" },  {"costamount3", "3" },  {"name", "Простая кирка" },  {"description", "Дробит мягкую породу." }, }, new Hashtable() { {"droptype", "thing" },  {"type", "tool" },  {"icon", "icons_tool_03" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"costtype1", "instr_metal" },  {"costamount1", "5" },  {"costtype2", "instr_plastic" },  {"costamount2", "3" },  {"costtype3", "instr_insulating" },  {"costamount3", "3" },  {"name", "Стальная кирка" },  {"description", "Разбивает даже прочный гранит!" }, }, new Hashtable() { {"droptype", "thing" },  {"type", "tool" },  {"icon", "icons_tool_04" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"costtype1", "instr_metal" },  {"costamount1", "2" },  {"costtype2", "instr_plastic" },  {"costamount2", "6" },  {"costtype3", "instr_motor" },  {"costamount3", "3" },  {"name", "Китайская дрель" },  {"description", "Обычная дрель, быстро ломается." }, }};
        Global._Consumables = new List<Hashtable>(new Hashtable[] {new Hashtable() { {"loot_name", "instr_insulating" },  {"droptype", "consumable" },  {"type", "craft" },  {"icon", "icons_instr_insulating" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"count", "10" },  {"name", "Изолента" },  {"description", "" }, }, new Hashtable() { {"loot_name", "instr_metal" },  {"droptype", "consumable" },  {"type", "craft" },  {"icon", "icons_instr_metal" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"count", "10" },  {"name", "Металл" },  {"description", "" }, }, new Hashtable() { {"loot_name", "instr_motor" },  {"droptype", "consumable" },  {"type", "craft" },  {"icon", "icons_instr_motor" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"count", "10" },  {"name", "Двигатель" },  {"description", "" }, }, new Hashtable() { {"loot_name", "instr_plastic" },  {"droptype", "consumable" },  {"type", "craft" },  {"icon", "icons_instr_plastic" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"count", "10" },  {"name", "Пластик" },  {"description", "" }, }, new Hashtable() { {"loot_name", "instr_rubber" },  {"droptype", "consumable" },  {"type", "craft" },  {"icon", "icons_instr_rubber" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"count", "10" },  {"name", "Резина" },  {"description", "" }, }, new Hashtable() { {"loot_name", "instr_wood" },  {"droptype", "consumable" },  {"type", "craft" },  {"icon", "icons_instr_wood" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"count", "10" },  {"name", "Древесина" },  {"description", "" }, }});
        Global._userResourcesCraft = new Hashtable(new Hashtable() { {"loot_metal", new Hashtable() { {"amount", "10" },  {"name", "Металл" }, } },  {"loot_plastic", new Hashtable() { {"amount", "10" },  {"name", "Пластик" }, } },  {"loot_cobweb", new Hashtable() { {"amount", "0" },  {"name", "Паутина" }, } },  {"loot_deadspider", new Hashtable() { {"amount", "0" },  {"name", "Хитин" }, } },  {"loot_eballon", new Hashtable() { {"amount", "10" },  {"name", "Энергобаллон" }, } },  {"loot_cactus", new Hashtable() { {"amount", "0" },  {"name", "Цветок кактуса" }, } }, });
        Global.currentLootIcon = new GUIStyle();
        Global._RecipeItemIcon01 = new GUIStyle();
        Global._RecipeItemIcon02 = new GUIStyle();
        Global._RecipeItemIcon03 = new GUIStyle();
        Global._RecipeItemIcon04 = new GUIStyle();
        Global._RecipeItemIcon05 = new GUIStyle();
        Global._RecipeCostIcon = new GUIStyle();
        Global._recipeItems = new List<Hashtable>(new Hashtable[] {new Hashtable() { {"droptype", "thing" },  {"type", "backpack" },  {"thingname", "thing_r3_backpack" },  {"icon", Global._RecipeItemIcon01 },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "true" },  {"addslots", "12" },  {"costtype1", "loot_metal" },  {"costamount1", "6" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "loot_plastic" },  {"costamount2", "2" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "none" },  {"costamount3", "0" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Ранец 2 уровня" },  {"description", "Добавляет +12 слотов в инвентарь." },  {"level", "2" }, }, new Hashtable() { {"droptype", "thing" },  {"type", "weapon" },  {"thingname", "thing_r3_gun" },  {"icon", Global._RecipeItemIcon03 },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "false" },  {"adddamage", "15" },  {"costtype1", "loot_metal" },  {"costamount1", "5" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "loot_eballon" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "none" },  {"costamount3", "0" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Резак" },  {"description", "Добавляет +15 Урона." },  {"level", "1" }, }, new Hashtable() { {"droptype", "thing" },  {"type", "backpack" },  {"thingname", "thing_r5_backpack" },  {"icon", Global._RecipeItemIcon04 },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "true" },  {"addslots", "12" },  {"costtype1", "loot_metal" },  {"costamount1", "6" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "loot_plastic" },  {"costamount2", "2" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "none" },  {"costamount3", "0" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Ранец 3 уровня" },  {"description", "Добавляет +12 слотов в инвентарь. Открывает абилку Лечение." },  {"level", "3" }, }, new Hashtable() { {"droptype", "thing" },  {"type", "weapon" },  {"thingname", "thing_r4_gun" },  {"icon", Global._RecipeItemIcon05 },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "true" },  {"adddamage", "0" },  {"costtype1", "loot_metal" },  {"costamount1", "5" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "loot_deadspider" },  {"costamount2", "2" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "loot_eballon" },  {"costamount3", "1" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Парализатор" },  {"description", "Временно обездвиживает противника, не нанося тому урона." },  {"level", "2" }, }});
        Global._RecipeFoodIcon = new GUIStyle();
        Global._recipeFood = new List<Hashtable>(new Hashtable[] {new Hashtable() { {"droptype", "thing" },  {"type", "food" },  {"thingname", "food_chicken_soup" },  {"icon", Global._RecipeFoodIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "true" },  {"addhealth", "15" },  {"costtype1", "food_chicken" },  {"costamount1", "1" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "food_potatoes" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "food_carrot" },  {"costamount3", "1" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "food_onion" },  {"costamount4", "1" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Куриный суп" },  {"description", "Добавляет +15 Здоровья." }, }, new Hashtable() { {"droptype", "thing" },  {"type", "food" },  {"thingname", "food_cabbage_soup" },  {"icon", Global._RecipeFoodIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "false" },  {"addhealth", "10" },  {"costtype1", "food_cabbage" },  {"costamount1", "2" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "food_potatoes" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "food_carrot" },  {"costamount3", "1" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "food_sourcream" },  {"costamount4", "1" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Щи" },  {"description", "Добавляет +10 Здоровья." }, }, new Hashtable() { {"droptype", "thing" },  {"type", "food" },  {"thingname", "food_mushroom_soup" },  {"icon", Global._RecipeFoodIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "false" },  {"addhealth", "12" },  {"costtype1", "food_mushrooms" },  {"costamount1", "1" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "food_potatoes" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "food_onion" },  {"costamount3", "1" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "food_sourcream" },  {"costamount4", "1" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Грибной суп" },  {"description", "Добавляет +12 Здоровья." }, }, new Hashtable() { {"droptype", "thing" },  {"type", "food" },  {"thingname", "food_mushroom_potatoes" },  {"icon", Global._RecipeFoodIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "false" },  {"addhealth", "15" },  {"costtype1", "food_mushrooms" },  {"costamount1", "1" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "food_potatoes" },  {"costamount2", "2" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "none" },  {"costamount3", "0" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Картошка с грибами" },  {"description", "Добавляет +15 Здоровья." }, }, new Hashtable() { {"droptype", "thing" },  {"type", "food" },  {"thingname", "food_fish_soup" },  {"icon", Global._RecipeFoodIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "true" },  {"addhealth", "20" },  {"costtype1", "food_fish" },  {"costamount1", "2" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "food_potatoes" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "food_onion" },  {"costamount3", "1" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "food_spice" },  {"costamount4", "1" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Уха" },  {"description", "Добавляет +20 Здоровья." }, }, new Hashtable() { {"droptype", "thing" },  {"type", "food" },  {"thingname", "food_pancake" },  {"icon", Global._RecipeFoodIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "true" },  {"addhealth", "12" },  {"costtype1", "food_flour" },  {"costamount1", "2" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "food_egg" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "food_milk" },  {"costamount3", "1" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "food_sourcream" },  {"costamount4", "1" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Блинчики со сметаной" },  {"description", "Добавляет +12 Здоровья." }, }, new Hashtable() { {"droptype", "thing" },  {"type", "food" },  {"thingname", "food_smoothies" },  {"icon", Global._RecipeFoodIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "false" },  {"addhealth", "5" },  {"costtype1", "food_orange" },  {"costamount1", "1" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "food_banana" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "food_berries" },  {"costamount3", "1" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Фруктовый смузи" },  {"description", "Добавляет +5 Здоровья." }, }, new Hashtable() { {"droptype", "thing" },  {"type", "food" },  {"thingname", "food_milkshape" },  {"icon", Global._RecipeFoodIcon },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"available", "true" },  {"addhealth", "7" },  {"costtype1", "food_banana" },  {"costamount1", "1" },  {"costicon1", Global._RecipeCostIcon },  {"costtype2", "food_milk" },  {"costamount2", "1" },  {"costicon2", Global._RecipeCostIcon },  {"costtype3", "none" },  {"costamount3", "0" },  {"costicon3", Global._RecipeCostIcon },  {"costtype4", "none" },  {"costamount4", "0" },  {"costicon4", Global._RecipeCostIcon },  {"name", "Молочный коктейль" },  {"description", "Добавляет +7 Здоровья." }, }});
        Global._currentResLootIcon = new GUIStyle();
        Global._userResourcesFood = new Hashtable(new Hashtable() { {"food_banana", new Hashtable() { {"amount", "0" },  {"name", "Бананы" }, } },  {"food_berries", new Hashtable() { {"amount", "0" },  {"name", "Ягоды" }, } },  {"food_cabbage", new Hashtable() { {"amount", "0" },  {"name", "Капуста" }, } },  {"food_carrot", new Hashtable() { {"amount", "0" },  {"name", "Морковь" }, } },  {"food_chicken", new Hashtable() { {"amount", "0" },  {"name", "Курица" }, } },  {"food_egg", new Hashtable() { {"amount", "0" },  {"name", "Яйцо" }, } },  {"food_fish", new Hashtable() { {"amount", "0" },  {"name", "Рыба" }, } },  {"food_flour", new Hashtable() { {"amount", "0" },  {"name", "Мука" }, } },  {"food_milk", new Hashtable() { {"amount", "0" },  {"name", "Молоко" }, } },  {"food_mushrooms", new Hashtable() { {"amount", "0" },  {"name", "Грибы" }, } },  {"food_onion", new Hashtable() { {"amount", "0" },  {"name", "Лук" }, } },  {"food_orange", new Hashtable() { {"amount", "0" },  {"name", "Апельсин" }, } },  {"food_potatoes", new Hashtable() { {"amount", "0" },  {"name", "Картофель" }, } },  {"food_sourcream", new Hashtable() { {"amount", "0" },  {"name", "Сметана" }, } },  {"food_spice", new Hashtable() { {"amount", "0" },  {"name", "Специи" }, } },  {"food_beans", new Hashtable() { {"amount", "0" },  {"name", "Бобы" }, } },  {"food_chickpea", new Hashtable() { {"amount", "0" },  {"name", "Нут" }, } },  {"food_corn", new Hashtable() { {"amount", "0" },  {"name", "Зерно" }, } },  {"food_kidney_beans", new Hashtable() { {"amount", "0" },  {"name", "Фасоль" }, } },  {"food_lentils", new Hashtable() { {"amount", "0" },  {"name", "Чечевица" }, } },  {"food_peanut", new Hashtable() { {"amount", "0" },  {"name", "Арахис" }, } },  {"food_peas", new Hashtable() { {"amount", "0" },  {"name", "Горох" }, } }, });
        Global.currentFoodRecipeName = "";
        Global.currentFoodRecipeDescription = "";
        Global.currentFoodRecipeIcon = new GUIStyle();
        Global.currentFoodRecipeCostType1 = "";
        Global.currentFoodRecipeCostAmount1 = "";
        Global.currentFoodRecipeCostName1 = "";
        Global.currentFoodRecipeCostIcon1 = new GUIStyle();
        Global.currentFoodRecipeCostType2 = "";
        Global.currentFoodRecipeCostAmount2 = "";
        Global.currentFoodRecipeCostName2 = "";
        Global.currentFoodRecipeCostIcon2 = new GUIStyle();
        Global.currentFoodRecipeCostType3 = "";
        Global.currentFoodRecipeCostAmount3 = "";
        Global.currentFoodRecipeCostName3 = "";
        Global.currentFoodRecipeCostIcon3 = new GUIStyle();
        Global.currentFoodRecipeCostType4 = "";
        Global.currentFoodRecipeCostAmount4 = "";
        Global.currentFoodRecipeCostName4 = "";
        Global.currentFoodRecipeCostIcon4 = new GUIStyle();
        Global.currentFoodRecipeAddHealth = "";
        Global.currentFoodRecipeVal = "";
        Global._isTabCraftSelect = true;
        Global._isLibraryHealthSelect = true;
        Global.bookTitleImage = new GUIStyle();
        Global._userBooksHealth = new Hashtable(new Hashtable() { {"library_book01", new Hashtable() { {"select", "false" },  {"teach", "false" },  {"titleimage", Global.bookTitleImage },  {"titletext", "Пищевая ценность продуктов" },  {"description", "То, что качество и состав пищи влияют на самочувствие, работоспособность и выносливость, люди поняли очень давно. Примерно в 400 году до н.э. Геродот призывал: «Позвольте пище быть вашим лекарством, а лекарствам – вашей пищей». Главный источник энергии - это макронутриенты: жиры, белки, углеводы. Помимо них необходим целый набор микронутриентов - витаминов, минеральных веществ, микроэлементов, биологически активных компонентов. Это больше сотни химических соединений." }, } }, });
        Global._userBooksBiology = new Hashtable(new Hashtable() { {"library_book01", new Hashtable() { {"select", "false" },  {"teach", "false" },  {"titleimage", Global.bookTitleImage },  {"titletext", "Овощи" },  {"description", "Если вы держите в руках эту книгу и читаете эти строки, то наверняка уже не раз задавались вопросом, что у вас на тарелке. Мы все знаем, что овощи очень полезны. Но в то же время мясо — доступная, удобная, сытная еда. Могут ли овощные блюда приносить такое же удовлетворение? С уверенностью заявляю: ДА! И предлагаю всем открыть для себя массу потрясающих рецептов, которые, так уж случилось, не содержат мяса." }, } },  {"library_book02", new Hashtable() { {"select", "false" },  {"teach", "false" },  {"titleimage", Global.bookTitleImage },  {"titletext", "Грибы" },  {"description", "Наш прекрасный мир и его чудесная природа обрели свой вид только благодаря грибам, без которых немыслима ни одна экосистема. Без них не было бы ни наших лесов, ни нашего климата, да и, возможно, самой жизни. Грибы вездесущи, и, если использовать их правильно, они могут помочь нам в совершенно неожиданных областях. Грибы — партнеры, грибы — мастера утилизации отходов, грибы — чудо-лекарство." }, } },  {"library_book03", new Hashtable() { {"select", "false" },  {"teach", "false" },  {"titleimage", Global.bookTitleImage },  {"titletext", "Пауки" },  {"description", "Единственное место, где не водятся пауки, – это море. Большинство скорпионов находят своих жертв на ощупь. Многоножки играют очень важную роль в лесных экосистемах. Мы расскажем вам о пауках, одной из самых распространенных групп животных, – от гигантских птицеядов до крохотного клеща-кровососа." }, } }, });
        Global._userBooksTech = new Hashtable(new Hashtable() { {"library_book01", new Hashtable() { {"select", "false" },  {"teach", "false" },  {"titleimage", Global.bookTitleImage },  {"titletext", "Скафандры" },  {"description", "Скафандр — специальное снаряжение, предназначенное для изоляции человека от внешней среды. Части снаряжения образуют оболочку, непроницаемую для компонентов внешней среды. Скафандры в основном подразделяются на водолазные, авиационные и космические." }, } }, });
        Global._selectedDropHashArray = new object[0];
        Global._dropPlacePoses = new Stack<object>();
        Global._globalVidIndexForPoisonsAndSpells = 1;
        Global._globalVidIndexForChest = 5;
        Global._isTakeDrop = true;
        Global.labelLootAlpha = 1f;
        Global.lootIcon = new GUIStyle();
        Global._recipeDropLoot = new List<Hashtable>(new Hashtable[] {new Hashtable() { {"loot_name", "loot_garbage" },  {"droptype", "consumable" },  {"type", "loot" },  {"icon", "icons_loot_garbage" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"loottype1", "instr_insulating" },  {"lootamount1", "2" },  {"loottype2", "instr_plastic" },  {"lootamount2", "2" },  {"loottype3", "instr_rubber" },  {"lootamount3", "1" },  {"name", "Рухлядь" },  {"description", "Какой-то мусор техногенного происхождения." }, }, new Hashtable() { {"loot_name", "loot_rustydebris" },  {"droptype", "consumable" },  {"type", "craft" },  {"icon", "icons_loot_rustydebris" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"loottype1", "instr_metal" },  {"lootamount1", "5" },  {"loottype2", "instr_motor" },  {"lootamount2", "1" },  {"loottype3", "instr_insulating" },  {"lootamount3", "1" },  {"name", "Ржавые обломки" },  {"description", "Трудно сказать, чем эти обломки были раньше." }, }, new Hashtable() { {"loot_name", "loot_woodenbox" },  {"droptype", "consumable" },  {"type", "craft" },  {"icon", "icons_loot_woodenbox" },  {"fromdrag", "inventory" },  {"isdropable", "true" },  {"loottype1", "instr_wood" },  {"lootamount1", "5" },  {"loottype2", "instr_rubber" },  {"lootamount2", "1" },  {"loottype3", "instr_metal" },  {"lootamount3", "1" },  {"name", "Старый ящик" },  {"description", "Этот ящик можно только разобрать на детали." }, }});
        Global.VeGameWinIcon = new GUIStyle();
        Global.VeGameLooseIcon = new GUIStyle();
        Global.VeGameVegetable_01 = new GUIStyle();
        Global.VeGameVegetable_02 = new GUIStyle();
        Global.VeGameVegetable_03 = new GUIStyle();
        Global.VeGameVegetable_01_icon = new GUIStyle();
        Global.VeGameVegetable_02_icon = new GUIStyle();
        Global.VeGameVegetable_03_icon = new GUIStyle();
        Global._isTakeVegetable = true;
        Global._isTakeKey = true;
        Global._isTakeAnimal = true;
        Global.MuseumGameWinIcon = new GUIStyle();
        Global.MuseumGameLooseIcon = new GUIStyle();
        Global.MuseumGameItem_01 = new GUIStyle();
        Global.MuseumGameItem_02 = new GUIStyle();
        Global.MuseumGameItem_03 = new GUIStyle();
        Global.MuseumGameItem_01_icon = new GUIStyle();
        Global.MuseumGameItem_02_icon = new GUIStyle();
        Global.MuseumGameItem_03_icon = new GUIStyle();
        Global.BoxroomGameWinIcon = new GUIStyle();
        Global.BoxroomGameLooseIcon = new GUIStyle();
        Global.BoxroomGameItem_01 = new GUIStyle();
        Global.BoxroomGameItem_02 = new GUIStyle();
        Global.BoxroomGameItem_03 = new GUIStyle();
        Global.BoxroomGameItem_01_icon = new GUIStyle();
        Global.BoxroomGameItem_02_icon = new GUIStyle();
        Global.BoxroomGameItem_03_icon = new GUIStyle();
        Global.MushroomsGameWinIcon = new GUIStyle();
        Global.MushroomsGameLooseIcon = new GUIStyle();
        Global.MushroomsGameItem_01 = new GUIStyle();
        Global.MushroomsGameItem_02 = new GUIStyle();
        Global.MushroomsGameItem_03 = new GUIStyle();
        Global.MushroomsGameItem_01_icon = new GUIStyle();
        Global.MushroomsGameItem_02_icon = new GUIStyle();
        Global.MushroomsGameItem_03_icon = new GUIStyle();
        Global._isFirstGameStart = true;
        Global._isTutorial07_fucker = true;
        Global._isTutorialBattleDialogue = true;
        Global._isTutorialCobwebArrow = true;
        Global._isTutorialAbilityClick = true;
        Global._isVillaFirstVisit = true;
        Global.fuckerCall = true;
        Global._isClosedDoorDialogue = true;
        Global._isFeatureEquiped = true;
        Global._Mosaic1Index = 1;
        Global._Mosaic2Index = 3;
        Global._Mosaic3Index = 2;
        Global._Mosaic4Index = 3;
        Global._Mosaic5Index = 2;
        Global._Mosaic6Index = 1;
        Global._Mosaic7Index = 2;
        Global._Mosaic8Index = 1;
        Global._Mosaic9Index = 3;
        Global.HOG1Item1Alpha = 1f;
        Global.HOG1Item2Alpha = 1f;
        Global.HOG1Item3Alpha = 1f;
        Global.HOG1Item4Alpha = 1f;
        Global.HOG1Item5Alpha = 1f;
        Global.HOG1Item6Alpha = 1f;
        Global.HOG1Item7Alpha = 1f;
        Global.HOG1Item8Alpha = 1f;
        Global.HOG2Item1Alpha = 1f;
        Global.HOG2Item2Alpha = 1f;
        Global.HOG2Item3Alpha = 1f;
        Global.HOG2Item4Alpha = 1f;
        Global.HOG2Item5Alpha = 1f;
        Global.HOG2Item6Alpha = 1f;
        Global.HOG3Item1Alpha = 1f;
        Global.HOG3Item2Alpha = 1f;
        Global.HOG3Item3Alpha = 1f;
        Global.HOG3Item4Alpha = 1f;
        Global.HOG3Item5Alpha = 1f;
        Global.HOG3Item6Alpha = 1f;
        Global.HOG3Item7Alpha = 1f;
        Global.HOG3Item8Alpha = 1f;
        Global.HOGItemIcon = new GUIStyle();
        Global.HOGhintAlpha = 1f;
        Global.exhibitTitleImage = new GUIStyle();
        Global._userMuseum = new Hashtable(new Hashtable() { {"museum_exhibit01", new Hashtable() { {"select", "false" },  {"teach", "false" },  {"titleimage", Global.exhibitTitleImage },  {"titletext", "Вирт" },  {"description", "Вирт - устройство, предназначенное для сохранения оцифрованных личностей людей. Включив этот вирт можно пообщаться с его «обитателем»." }, } },  {"museum_exhibit02", new Hashtable() { {"select", "false" },  {"teach", "false" },  {"titleimage", Global.exhibitTitleImage },  {"titletext", "Аммонит" },  {"description", "Аммониты - вымершие головоногие моллюски, обладавшие очень красивыми наружными раковинами. Они появились 300 миллионов лет назад, в девонском периоде. Как и все головоногие, аммониты жили только в морях с нормальной соленостью, никогда не заходя в пресные водоемы и устья рек. Большинство аммонитов обладали спирально-закрученной раковиной, хотя среди них неоднократно появлялись так называемые гетероморфы – аммониты с развернутыми, закрученными в клубок, прямыми как палка или крючковидными раковинами. Аммониты были хищниками и, скорее всего, охотились на любую добычу, которую могли поймать." }, } }, });
        Global._isR3Body = true;
        Global._isR3Hand = true;
        Global._isR3Leg = true;
        Global._showMiniMap = true;
        Global.labelNewLevelAlpha = 1f;
        Global.labelGameSavedAlpha = 1f;
        Global._GAME_CURSOR = "default";
        Global.testGera = new Hashtable(new Hashtable() { {Global.HOG3Item1Alpha, 0 }, });
    }

}