//using Configuration;
using Core.DI;
using Core.Dialog;
using Core.DISimple;
using Core.Events;
using Core.Helpers;
using Core.ResourceInner;
using Core.Scenes;
using Core.States;
using Core.Storage;
using Core.UI;
using Core.UI.MVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{


    public class ApplicationStart : MonoBehaviour
    {
        //[SerializeField] private GameConfiguration _gameConfiguration;
        [SerializeField] private int _coinsOnStart;

        [SerializeField] private VideoConfiguration _videoConfiguration;
        [SerializeField] private UIManagerAdvanced _uIManagerAdvanced;
        [SerializeField] private Localization.LocalizationDescriptor _languageDescriptor;

        [SerializeField] private DialogDataBase _dialogDataBase;
        [SerializeField] private ResourceProvider _resourceProvider;
        [SerializeField] private CharacterDialogController _characterDialogController;

        private Core.PlayerData.IPlayer _player;

        private IStateMachine<ApplicationStates> _stateMachine;

        private StorageData _storageData = new StorageData();

        private DiServiceCollection _diServiceCollection;
        private DiContainer _diContainer;

        private LobbyState _lobbyState;

        // Start is called before the first frame update
        void Awake()
        {
            //ServiceLocator.Register<GameConfiguration>(_gameConfiguration);
            DontDestroyOnLoad(gameObject);

            Subscribe();


            PreparePlayer();

            PrepareDiService();

            PrepareStateteMachine();
        }

        private void PrepareDiService()
        {
            _diServiceCollection = new DiServiceCollection();

            _diServiceCollection.RegisterSingleton<IUIManager>(_uIManagerAdvanced);
            _diServiceCollection.RegisterSingleton<PlayerData.IPlayer>(_player);
            _diServiceCollection.RegisterSingleton<PlayerData.ILevelProgression>(_player.LevelProgression);
            _diServiceCollection.RegisterSingleton<PlayerData.ISettings>(_player.Settings);
            _diServiceCollection.RegisterSingleton<Localization.LocalizationDescriptor>(_languageDescriptor);
            _diServiceCollection.RegisterSingleton<DialogDataBase>(_dialogDataBase);
            _diServiceCollection.RegisterSingleton<ResourceProvider>(_resourceProvider);

            _diServiceCollection.RegisterSingleton<VideoConfiguration>(_videoConfiguration);
            _diServiceCollection.RegisterSingleton<ICharacterDialogController>(_characterDialogController);
            PrepareLoadingCommand(_diServiceCollection);

            //presenters
            DIPreparePresenters(_diServiceCollection);

            //use cases
            DIPrepareUseCases(_diServiceCollection);

            //states
            DIPrepareStates(_diServiceCollection);

            _diServiceCollection.RegisterSingleton<Localization.ILocalizationService, Localization.LocalizationService>();

            _diServiceCollection.RegisterSingleton<IDialogController, DialogController>();

            _diServiceCollection.RegisterSingleton<IDynamicObjectHolder, DynamicObjectHolder>();

            _diContainer = _diServiceCollection.GenerateContainer();

            _diServiceCollection.RegisterSingleton<DI.DiContainer>(_diContainer);
            ServiceLocator.Register<DI.DiContainer>(_diContainer);
        }

        private void DIPreparePresenters(DiServiceCollection serviceCollection)
        {
            serviceCollection.RegisterSingleton<IProxyView<IVideoPlayerScreeenView>, VideoPlayerScreenProxyView>();
            serviceCollection.RegisterSingleton<IVideoPlayerScreenPresenter, VideoPlayerScreenPresenter>();

            serviceCollection.RegisterSingleton<IProxyView<IDialogPanelView>, DialogPanelProxyView>();
            serviceCollection.RegisterSingleton<IDialogPanelPresenter, DialogPanelPresenter>();

            serviceCollection.RegisterSingleton<IProxyView<IShipScreenView>, ShipScreenProxyView>();
            serviceCollection.RegisterSingleton<IShipScreenPresenter, ShipScreenPresenter>();

            serviceCollection.RegisterSingleton<IProxyView<ILoadingScreenView>, LoadingScreenProxyView>();
            serviceCollection.RegisterSingleton<ILoadingScreenPresenter, LoadingScreenPresenter>();
        }

        private void DIPrepareUseCases(DiServiceCollection serviceCollection)
        {
            serviceCollection.RegisterSingleton<IShipScreenUseCases, ShipScreenUseCases>();
        }

        private void DIPrepareStates(DiServiceCollection serviceCollection)
        {
            serviceCollection.RegisterSingleton<IntroState, IntroState>();
            serviceCollection.RegisterSingleton<LobbyState, LobbyState>();
            serviceCollection.RegisterSingleton<LoadingState, LoadingState>();
        }

        private void PrepareLoadingCommand(DiServiceCollection serviceCollection)
        {
            var loadingCommand = new LoadingCommand();
            serviceCollection.RegisterSingleton<ILoadingCommand>(loadingCommand);
        }

        private void Start()
        {
            _stateMachine.SwitchToState(ApplicationStates.Intro);
        }

        private void Subscribe()
        {
            EventAggregator.Subscribe<StorageEvent>(OnStorageEventHandler);
        }

        private void Unsubscribe()
        {
            EventAggregator.Unsubscribe<StorageEvent>(OnStorageEventHandler);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                OnStorageEventHandler(this, new StorageEvent());
            }
            else
            {
                _player.Load();
                LoadStorageData();
            }
        }

        private void OnApplicationQuit()
        {
            OnStorageEventHandler(this, new StorageEvent());
        }

        private void PreparePlayer()
        {
            var wallet = new Core.PlayerData.Wallet(Core.PlayerData.MoneyType.Coins, _coinsOnStart);
            var settings = new Core.PlayerData.Settings();
            var levelProgression = new Core.PlayerData.LevelProgression();
            var privacyPolicy = new Core.PlayerData.PrivacyPolicy();
            _player = new Core.PlayerData.Player(privacyPolicy, settings, wallet, levelProgression);

            _player.Load();
            LoadStorageData();
            ServiceLocator.Register<Core.PlayerData.IPlayer>(_player);
        }

        private void PrepareStateteMachine()
        {

            _lobbyState = _diContainer.GetService<LobbyState>();

            var introState = _diContainer.GetService<IntroState>();
            introState.InitStateComplitationCallBack(OnIntroStateCompletationHandler);
            var loadingState = _diContainer.GetService<LoadingState>();
            loadingState.InitStateComplitationCallBack(OnLoadingStateCompleteHandler);
            _stateMachine = new BaseStateMachine<ApplicationStates>(_lobbyState, introState, loadingState);
        }

        [ContextMenu("Switch to Lobby")]
        private void TestSwitchToLobby()
        {
            _stateMachine.SwitchToState(ApplicationStates.Lobby);
        }

        [ContextMenu("Test change progress value")]
        private void TestChangeProgressValue()
        {

            //Debug.Log($"[TestChangeProgressValue] value: {progressValue}");

        }

        private void OnStorageEventHandler(object sender, StorageEvent eventData)
        {
            _player.Save();
            //_player.Save(_storageData);
            SaveStorageData();
        }


        private void LoadStorageData()
        {
            const string StorageDataKey = "ApplicationStart";//nameof(ApplicationStart);


            var rawData = PlayerPrefs.GetString(StorageDataKey, "");
            _storageData = JsonUtility.FromJson<StorageData>(rawData);
            if (_storageData != null)
            {
                _player.Load(_storageData);
            }
        }

        private void SaveStorageData()
        {
            const string StorageDataKey = "ApplicationStart";//nameof(ApplicationStart);
            if (_storageData == null)
            {
                _storageData = new StorageData();
            }

            _player.Save(_storageData);
            var rawData = JsonUtility.ToJson(_storageData);
            PlayerPrefs.SetString(StorageDataKey, rawData);
        }

        private void OnIntroStateCompletationHandler()
        {
            var loadingCommand = _diContainer.GetService<ILoadingCommand>();
            //loadingCommand.InitExecutionAction(()=>SceneHelper.LoadScene(SceneType.Worldmap));
            _stateMachine.SwitchToState(ApplicationStates.Loading);
        }

        private void OnLoadingStateCompleteHandler()
        {
            _stateMachine.SwitchToState(ApplicationStates.Lobby);
        }
    }
}