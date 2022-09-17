using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;

[System.Serializable]
/*****************************************************************************************
* Video player
* 
* @author N-Game Studios
* Research & Development	
* 
*****************************************************************************************/
[UnityEngine.RequireComponent(typeof(RawImage))]
[UnityEngine.RequireComponent(typeof(AudioSource))]
public partial class s_VideoPlayer : MonoBehaviour
{
    private bool _isLoaderAnyKey;
    private bool _isAnyKeyDown;
    private WWW www;
    public virtual void Update()
    {
        if (this._isLoaderAnyKey)
        {
            if (Input.anyKeyDown)
            {
                this._isLoaderAnyKey = false;
                this._isAnyKeyDown = true;
            }
        }
    }

    public virtual IEnumerator _PlayVideo(string videoFile)
    {
        Global._gui_CloseAllInterfaces();
        yield break;
        if (Application.platform == RuntimePlatform.Android)
        {
        }
        else
        {
             //Handheld.PlayFullScreenMovie (videoFile, Color.black, FullScreenMovieControlMode.CancelOnInput);
            if (((Application.platform == RuntimePlatform.OSXEditor) || (Application.platform == RuntimePlatform.WindowsEditor)) || (Application.platform == RuntimePlatform.WindowsPlayer))
            {
                Global._isPlayVideo = true;
                Global._options_is_gamecursor = false;
                this._isLoaderAnyKey = false;
                this._isAnyKeyDown = false;
                this.www = new WWW("file:" + Persist.pathForDocumentsFile(videoFile));
                //VideoPlayer movieTexture = this.www.GetMovieTexture();
                //while (!movieTexture.isReadyToPlay)
                //{
                //    yield return null;
                //}
                //this.GetComponent<RawImage>().texture = movieTexture;

                {
                    float _162 = 0.5f;
                    Color _163 = this.GetComponent<RawImage>().color;
                    _163.a = _162;
                    this.GetComponent<RawImage>().color = _163;
                }
                this.transform.localScale = new Vector3(1, 1, 0);
                this.transform.position = new Vector3(0.5f, 0.5f, 0);

                {
                    int _164 = 0;
                    //Rect _165 = this.GetComponent<RawImage>().pixelInset;
                    //_165.xMin = _164; //-movieTexture.width / 2;
                    //this.GetComponent<RawImage>().pixelInset = _165;
                }

                {
                    int _166 = 0;
                    //Rect _167 = this.GetComponent<RawImage>().pixelInset;
                    //_167.xMax = _166; //movieTexture.width / 2;
                    //this.GetComponent<RawImage>().pixelInset = _167;
                }

                {
                    int _168 = 0;
                    //Rect _169 = this.GetComponent<RawImage>().pixelInset;
                    //_169.yMin = _168; //-movieTexture.height / 2;
                    //this.GetComponent<RawImage>().pixelInset = _169;
                }

                {
                    int _170 = 0;
                    //Rect _171 = this.GetComponent<RawImage>().pixelInset;
                    //_171.yMax = _170; //movieTexture.height / 2;
                    //this.GetComponent<RawImage>().pixelInset = _171;
                }
                //this.GetComponent<AudioSource>().clip = movieTexture.audioClip;
                //movieTexture.Play();
                this.GetComponent<AudioSource>().Play();
                this._isLoaderAnyKey = true;
                //while (movieTexture.isPlaying && !this._isAnyKeyDown)
                //{
                //    yield return null;
                //}
                //movieTexture.Pause();
                this.GetComponent<AudioSource>().Pause();

                {
                    float _172 = 0f;
                    Color _173 = this.GetComponent<RawImage>().color;
                    _173.a = _172;
                    this.GetComponent<RawImage>().color = _173;
                }
                this._isAnyKeyDown = false;
                if ((Global._isFirstGameStart && !Global._isIntro1) && !Global._isIntro2)
                {
                    Global._isIntro1 = true;
                }
                else
                {
                    if ((Global._isFirstGameStart && Global._isIntro1) && !Global._isIntro2)
                    {
                        Global._isIntro2 = true;
                    }
                }
            }
        }
        this._endOfSplashes();
    }

    public virtual IEnumerator Fade(float start, float end, float length)
    {
        float i = 0f;
        while (i < 1f)
        {

            {
                float _174 = Mathf.Lerp(start, end, i);
                Color _175 = this.GetComponent<RawImage>().color;
                _175.a = _174;
                this.GetComponent<RawImage>().color = _175;
            }
            yield return null;
            i = i + (Time.deltaTime * (1 / length));
        }
    }

    private void _endOfSplashes()
    {
        Global._isPlayVideo = false;
        Global._options_is_gamecursor = true;
        if ((((Global._isFirstGameStart && !Global._isTutorialVeGameComplete) && !Global._isTutorialSpidersBookComplete) && !Global.MosaicComplete) && !Global._isViewFromLibrary)
        {
            Global._gui_SetInterface("TutorialLibraryScreen");
            Global._gui_AddInterface("TutorialLibraryPopUp");
        }
        if ((Global._isFirstGameStart && Global._isIntro1) && !Global._isIntro2)
        {
            Global.globalBus.SendMessage("c_StartController_command_PlayIntro2");
        }
        if ((Global._isFirstGameStart && Global._isIntro2) && !Global._isContinueGame)
        {
            Global.globalBus.SendMessage("c_StartController_command_continue");
        }
        else
        {
            if (((Global._isFirstGameStart && Global._isTutorialVeGameComplete) && !Global._isTutorialSpidersBookComplete) && !Global._isViewFromLibrary)
            {
                Global._gui_SetInterface("VegetablesGameScreen");
                Global._gui_AddInterface("TutorialVeGameEndPopUp");
            }
            else
            {
                if (((Global._isFirstGameStart && Global._isTutorialSpidersBookComplete) && !Global._isTutorialSkafBookComplete) && !Global._isViewFromLibrary)
                {
                    Global._gui_SetInterface("TutorialWeaponLibraryScreen");
                    Global._gui_AddInterface("TutorialFeatureLearn");
                }
                else
                {
                    if (((Global._isFirstGameStart && Global._isTutorialSkafBookComplete) && !Global._isTutorialMushroomGameEnd) && !Global._isViewFromLibrary)
                    {
                        Global._gui_SetInterface("TutorialTechLibraryScreen");
                        Global._gui_AddInterface("TutorialBlasterLearn");
                    }
                    else
                    {
                        if (((Global._isFirstGameStart && Global._isTutorialMushroomGameEnd) && !Global._isTutorialMushroomVideo) && !Global._isViewFromLibrary)
                        {
                            Global._isUFOScreen = false;
                            Global._isPopUpOpen = true;
                            Global._gui_SetInterface("WorldMap");
                            Global._gui_AddInterface("TutorialMushroomSoupLearn");
                        }
                    }
                }
            }
        }
        if ((Global._isViewZoeStory && !Global._isLearnZoeStory) && Global._isPopUpOpen)
        {
            Global.speakerGO = GameObject.Find("_zoeSpeaker");
            Global.holoGO = GameObject.Find("_zoeSpeakerHolo");
            Global._playDialogue(Global.audio_player.zoe_dlg_009);
            Global.globalBus.gameObject.SendMessage("DialogueEnd");
            Global._gui_SetInterface("GirlDialogue12");
        }
        if (Global.MosaicComplete)
        {
            Global._isPopUpOpen = false;
            Global._isUFOScreen = false;
            Global.UFOShowEntry = true;
            Global._gui_SetInterface("WorldMap");
        }
        if (Global._isViewFromLibrary)
        {
            Global._gui_SetInterface("LibraryScreen");
            Global._isViewFromLibrary = false;
        }
    }

}