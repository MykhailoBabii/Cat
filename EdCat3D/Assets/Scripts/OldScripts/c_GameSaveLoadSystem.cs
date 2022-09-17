using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class SaveData : object
{
     //Положение игрока в мире
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
    public float playerRotationX;
    public float playerRotationY;
    public float playerRotationZ;
    public bool _isWorldMap;
    public bool _isGreenHouse;
    public bool _isTemple;
    //Параметры героя
    public int _paramPremiumCrystal;
    public int _paramHeroLevel;
    public int _paramCurrentExp;
    public int _paramNextLevelExp;
    public int _paramHeroHunger;
    public int _paramHeroThirst;
    public int _userCurrentHealth;
    public int _userMaxHealth;
    public bool _isStatusMegaHealth;
    public Hashtable _system_CharacterParams;
    public float _system_distanceAttack;
    public float _system_distanceTakeDrop;
    public float _system_distanceSpeak;
    public bool _isAttack01;
    public bool _isAttack02;
    public bool _isAttack03;
    //Инвентарь, склад и снаряжение игрока
    public Hashtable system_dummy;
    public System.Collections.Generic.List<Hashtable> globalInventoryThingsArr;
    public System.Collections.Generic.List<Hashtable> StorageArr;
    public bool _isR3Backpack;
    public bool _isR3Body;
    public bool _isR3Gun;
    public bool _isR3Hand;
    public bool _isR3Leg;
    public bool _isR4Backpack;
    public bool _isR4Body;
    public bool _isR4Gun;
    public bool _isR4Hand;
    public bool _isR4Leg;
    public bool _isR5Backpack;
    //Ресурсы для крафта и кухни
    public Hashtable _userResourcesCraft;
    public Hashtable _userResourcesFood;
    //Данные туториала
    public bool _isTutorial01;
    public bool _isTutorial02;
    public bool _isTutorial03;
    public bool _isTutorial04;
    public bool _isTutorial05;
    public bool _isTutorial06;
    public bool _isTutorial06_1;
    public bool _isTutorial07;
    public bool _isTutorial08;
    public bool _isTutorial09;
    public bool _isTutorialCreateSoup;
    public bool _isTutorial10;
    public bool _isTutorial11;
    public bool _isTutorial12;
    public bool _isTutorialLearnSmousi;
    public bool _isTutorial14;
    public bool _isTutorial15;
    public bool _isTutorial16;
    public bool _isTutorial17;
    public bool _isTutorial18;
    public bool _isTutorial19;
    public bool _isTutorial20;
    public bool _isTutorial21;
    public bool _isTutorial22;
    public bool _isTutorial23;
    public bool _isTutorial24;
    public bool _isTutorial25;
    public bool _isTutorial26;
    public bool _isTutorial27;
    public bool _isTutorial28;
    public bool _isTutorial29;
    public bool _isTutorial30;
    public bool _isTutorialVeGameEnd;
    public bool _isTutorial31;
    public bool _isTutorial32;
    public bool _isTutorial33;
    public bool _isTutorial33Video;
    public bool _isTutorial34;
    public bool _isTutorial35;
    public bool _isTutorial36;
    public bool _isTutorial37;
    public bool _isTutorial37Video;
    public bool _isTutorial38;
    public bool _isTutorial38_1;
    public bool _isTutorial39;
    public bool _isTutorial40;
    public bool _isTutorial41;
    public bool _isTutorial42;
    public bool _isTutorial43;
    public bool _isTutorial44;
    public bool _isTutorial45;
    public bool _isTutorialStorage;
    public bool _isTutorialInventory;
    public bool _isTutorialGoToGreenHouseAgain;
    public bool _isTutorialDestroyCobweb;
    public bool _isTutorialDialogueSpiderEnd;
    public bool _isTutorialSpiderAttack;
    public bool _isTutorialSpiderClick;
    public bool _isTutorialAbilityClick;
    public bool _isTutorialWaitingForTheSun;
    public bool _isTutorialMushroomGameEnd;
    public bool _isTutorialMushroomVideo;
    public bool _isTutorialEnd;
    public bool _isTutorialVeGameComplete;
    public bool _isTutorialSpidersBookComplete;
    public bool _isTutorialSkafBookComplete;
    //ХОГи
    public bool _isHOG1Complete;
    public bool _isHOG2Complete;
    public bool _isHOG3Complete;
    //Подземелье
    public bool TempleOpenDoor;
    public bool _isOpenPyramid;
    public bool _isFirstVisitTemple;
    public bool _isClosedDoorDialogue;
    public bool TempleExitDoor;
    public bool _isTakeKey;
    public bool _isRemoteKey;
    public bool _isClickKey;
    public bool _isGetKey;
    public bool MosaicAvailable;
    public bool MosaicComplete;
    public bool MosaicFirstEntry;
    //Музей
    public Hashtable _userMuseum;
    public bool _isViewZoeStory;
    public bool _isLearnZoeStory;
    //Библиотека
    public Hashtable _userBooksHealth;
    public Hashtable _userBooksBiology;
    public Hashtable _userBooksTech;
    //Рецепты
    public System.Collections.Generic.List<Hashtable> _recipeItems;
    public System.Collections.Generic.List<Hashtable> _recipeFood;
    //Рыбалка
    public bool _isFirstFishing;
    public SaveData()
    {
        this._system_CharacterParams = new Hashtable();
        this.system_dummy = new Hashtable();
        this.globalInventoryThingsArr = new System.Collections.Generic.List<Hashtable>();
        this.StorageArr = new System.Collections.Generic.List<Hashtable>();
        this._userResourcesCraft = new Hashtable();
        this._userResourcesFood = new Hashtable();
        this._userMuseum = new Hashtable();
        this._userBooksHealth = new Hashtable();
        this._userBooksBiology = new Hashtable();
        this._userBooksTech = new Hashtable();
        this._recipeItems = new System.Collections.Generic.List<Hashtable>();
        this._recipeFood = new System.Collections.Generic.List<Hashtable>();
    }

}
[System.Serializable]
public partial class c_GameSaveLoadSystem : MonoBehaviour
{
    private c_GuiCommonStyleController gs_common;
    public virtual void Start()
    {
         //this.SaveGame();
        this.gs_common = (c_GuiCommonStyleController) GameObject.Find("_gui_styles_common").GetComponent(typeof(c_GuiCommonStyleController));
    }

    public virtual void SaveGame()
    {
        SaveData data = new SaveData();
        //Сохраняем положение игрока в мире
        data.playerPositionX = Global._hero_dolly.transform.position.x;
        data.playerPositionY = Global._hero_dolly.transform.position.y;
        data.playerPositionZ = Global._hero_dolly.transform.position.z;
        data.playerRotationX = Global._hero_dolly.transform.rotation.x;
        data.playerRotationY = Global._hero_dolly.transform.rotation.y;
        data.playerRotationZ = Global._hero_dolly.transform.rotation.z;
        data._isWorldMap = Global._isWorldMap;
        data._isGreenHouse = Global._isGreenHouse;
        data._isTemple = Global._isTemple;
        //Параметры героя
        data._paramPremiumCrystal = Global._paramPremiumCrystal;
        data._paramHeroLevel = Global._paramHeroLevel;
        data._paramCurrentExp = Global._paramCurrentExp;
        data._paramNextLevelExp = Global._paramNextLevelExp;
        data._paramHeroHunger = Global._paramHeroHunger;
        data._paramHeroThirst = Global._paramHeroThirst;
        data._userCurrentHealth = Global._userCurrentHealth;
        data._userMaxHealth = Global._userMaxHealth;
        data._isStatusMegaHealth = Global._isStatusMegaHealth;
        data._system_CharacterParams = (Hashtable) Global._system_CharacterParams.Clone();
        data._system_distanceAttack = Global._system_distanceAttack;
        data._system_distanceTakeDrop = Global._system_distanceTakeDrop;
        data._system_distanceSpeak = Global._system_distanceSpeak;
        data._isAttack01 = Global._isAttack01;
        data._isAttack02 = Global._isAttack02;
        data._isAttack03 = Global._isAttack03;
        //Инвентарь, склад и снаряжение игрока
        data.system_dummy = (Hashtable) Global.system_dummy.Clone();
        data._isR3Backpack = Global._isR3Backpack;
        data._isR3Body = Global._isR3Body;
        data._isR3Gun = Global._isR3Gun;
        data._isR3Hand = Global._isR3Hand;
        data._isR3Leg = Global._isR3Leg;
        data._isR4Backpack = Global._isR4Backpack;
        data._isR4Body = Global._isR4Body;
        data._isR4Gun = Global._isR4Gun;
        data._isR4Hand = Global._isR4Hand;
        data._isR4Leg = Global._isR4Leg;
        data._isR5Backpack = Global._isR5Backpack;
        data.globalInventoryThingsArr = new System.Collections.Generic.List<Hashtable>(Global.globalInventoryThingsArr);
        data.StorageArr = new System.Collections.Generic.List<Hashtable>(Global.StorageArr);
        //Ресурсы для крафта и кухни
        data._userResourcesCraft = (Hashtable) Global._userResourcesCraft.Clone();
        data._userResourcesFood = (Hashtable) Global._userResourcesFood.Clone();
        //Сохраняем данные туториала
        data._isTutorial01 = Global._isTutorial01;
        data._isTutorial02 = Global._isTutorial02;
        data._isTutorial03 = Global._isTutorial03;
        data._isTutorial04 = Global._isTutorial04;
        data._isTutorial05 = Global._isTutorial05;
        data._isTutorial06 = Global._isTutorial06;
        data._isTutorial06_1 = Global._isTutorial06_1;
        data._isTutorial07 = Global._isTutorial07;
        data._isTutorial08 = Global._isTutorial08;
        data._isTutorial09 = Global._isTutorial09;
        data._isTutorialCreateSoup = Global._isTutorialCreateSoup;
        data._isTutorial10 = Global._isTutorial10;
        data._isTutorial11 = Global._isTutorial11;
        data._isTutorial12 = Global._isTutorial12;
        data._isTutorialLearnSmousi = Global._isTutorialLearnSmousi;
        data._isTutorial14 = Global._isTutorial14;
        data._isTutorial15 = Global._isTutorial15;
        data._isTutorial16 = Global._isTutorial16;
        data._isTutorial17 = Global._isTutorial17;
        data._isTutorial18 = Global._isTutorial18;
        data._isTutorial19 = Global._isTutorial19;
        data._isTutorial20 = Global._isTutorial20;
        data._isTutorial21 = Global._isTutorial21;
        data._isTutorial22 = Global._isTutorial22;
        data._isTutorial23 = Global._isTutorial23;
        data._isTutorial24 = Global._isTutorial24;
        data._isTutorial25 = Global._isTutorial25;
        data._isTutorial26 = Global._isTutorial26;
        data._isTutorial27 = Global._isTutorial27;
        data._isTutorial28 = Global._isTutorial28;
        data._isTutorial29 = Global._isTutorial29;
        data._isTutorial30 = Global._isTutorial30;
        data._isTutorialVeGameEnd = Global._isTutorialVeGameEnd;
        data._isTutorial31 = Global._isTutorial31;
        data._isTutorial32 = Global._isTutorial32;
        data._isTutorial33 = Global._isTutorial33;
        data._isTutorial33Video = Global._isTutorial33Video;
        data._isTutorial34 = Global._isTutorial34;
        data._isTutorial35 = Global._isTutorial35;
        data._isTutorial36 = Global._isTutorial36;
        data._isTutorial37 = Global._isTutorial37;
        data._isTutorial37Video = Global._isTutorial37Video;
        data._isTutorial38 = Global._isTutorial38;
        data._isTutorial38_1 = Global._isTutorial38_1;
        data._isTutorial39 = Global._isTutorial39;
        data._isTutorial40 = Global._isTutorial40;
        data._isTutorial41 = Global._isTutorial41;
        data._isTutorial42 = Global._isTutorial42;
        data._isTutorial43 = Global._isTutorial43;
        data._isTutorial44 = Global._isTutorial44;
        data._isTutorial45 = Global._isTutorial45;
        data._isTutorialStorage = Global._isTutorialStorage;
        data._isTutorialInventory = Global._isTutorialInventory;
        data._isTutorialGoToGreenHouseAgain = Global._isTutorialGoToGreenHouseAgain;
        data._isTutorialDestroyCobweb = Global._isTutorialDestroyCobweb;
        data._isTutorialDialogueSpiderEnd = Global._isTutorialDialogueSpiderEnd;
        data._isTutorialSpiderAttack = Global._isTutorialSpiderAttack;
        data._isTutorialSpiderClick = Global._isTutorialSpiderClick;
        data._isTutorialAbilityClick = Global._isTutorialAbilityClick;
        data._isTutorialWaitingForTheSun = Global._isTutorialWaitingForTheSun;
        data._isTutorialMushroomGameEnd = Global._isTutorialMushroomGameEnd;
        data._isTutorialMushroomVideo = Global._isTutorialMushroomVideo;
        data._isTutorialEnd = Global._isTutorialEnd;
        data._isTutorialVeGameComplete = Global._isTutorialVeGameComplete;
        data._isTutorialSpidersBookComplete = Global._isTutorialSpidersBookComplete;
        data._isTutorialSkafBookComplete = Global._isTutorialSkafBookComplete;
        //ХОГи
        data._isHOG1Complete = Global._isHOG1Complete;
        data._isHOG2Complete = Global._isHOG2Complete;
        data._isHOG3Complete = Global._isHOG3Complete;
        //Подземелье
        data.TempleOpenDoor = Global.TempleOpenDoor;
        data._isOpenPyramid = Global._isOpenPyramid;
        data._isFirstVisitTemple = Global._isFirstVisitTemple;
        data._isClosedDoorDialogue = Global._isClosedDoorDialogue;
        data.TempleExitDoor = Global.TempleExitDoor;
        data._isTakeKey = Global._isTakeKey;
        data._isRemoteKey = Global._isRemoteKey;
        data._isClickKey = Global._isClickKey;
        data._isGetKey = Global._isGetKey;
        data.MosaicAvailable = Global.MosaicAvailable;
        data.MosaicComplete = Global.MosaicComplete;
        data.MosaicFirstEntry = Global.MosaicFirstEntry;
        //Музей
        data._userMuseum = Global._userMuseum;
        data._isViewZoeStory = Global._isViewZoeStory;
        data._isLearnZoeStory = Global._isLearnZoeStory;
        //Библиотека
        data._userBooksHealth = Global._userBooksHealth;
        data._userBooksBiology = Global._userBooksBiology;
        data._userBooksTech = Global._userBooksTech;
        //Рецепты
        data._recipeItems = Global._recipeItems;
        data._recipeFood = Global._recipeFood;
        //Рыбалка
        data._isFirstFishing = Global._isFirstFishing;
        BinaryFormatter bin = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/autosave.sav", FileMode.Create);
        bin.Serialize(stream, data);
        stream.Close();
    }

    public virtual void LoadGame() //No save file found
    {
        if (File.Exists(Application.persistentDataPath + "/autosave.sav"))
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/autosave.sav", FileMode.Open);
            SaveData data = (SaveData) bin.Deserialize(stream);

            {
                float _58 = //Загружаем положение игрока в мире
                data.playerPositionX;
                Vector3 _59 = Global._hero_dolly.transform.position;
                _59.x = _58;
                Global._hero_dolly.transform.position = _59;
            }

            {
                float _60 = data.playerPositionY;
                Vector3 _61 = Global._hero_dolly.transform.position;
                _61.y = _60;
                Global._hero_dolly.transform.position = _61;
            }

            {
                float _62 = data.playerPositionZ;
                Vector3 _63 = Global._hero_dolly.transform.position;
                _63.z = _62;
                Global._hero_dolly.transform.position = _63;
            }
            Global._hero_dolly.transform.rotation = Quaternion.Euler(data.playerRotationX, data.playerRotationY, data.playerRotationZ);
            Global._isWorldMap = data._isWorldMap;
            Global._isGreenHouse = data._isGreenHouse;
            Global._isTemple = data._isTemple;
            //Параметры героя
            Global._paramPremiumCrystal = data._paramPremiumCrystal;
            Global._paramHeroLevel = data._paramHeroLevel;
            Global._paramCurrentExp = data._paramCurrentExp;
            Global._paramNextLevelExp = data._paramNextLevelExp;
            Global._paramHeroHunger = data._paramHeroHunger;
            Global._paramHeroThirst = data._paramHeroThirst;
            Global._userCurrentHealth = data._userCurrentHealth;
            Global._userMaxHealth = data._userMaxHealth;
            Global._isStatusMegaHealth = data._isStatusMegaHealth;
            Global._system_CharacterParams = (Hashtable) data._system_CharacterParams.Clone();
            Global._system_distanceAttack = data._system_distanceAttack;
            Global._system_distanceTakeDrop = data._system_distanceTakeDrop;
            Global._system_distanceSpeak = data._system_distanceSpeak;
            Global._isAttack01 = data._isAttack01;
            Global._isAttack02 = data._isAttack02;
            Global._isAttack03 = data._isAttack03;
            //Инвентарь, склад и снаряжение игрока
            Global.system_dummy = (Hashtable) data.system_dummy.Clone();
            //R3
            if (!(Global.system_dummy["backpack"] == null) && ((Global.system_dummy["backpack"] as Hashtable)["thingname"] == "thing_r3_backpack"))
            {
                (Global.system_dummy["backpack"] as Hashtable)["icon"] = this.gs_common.gui_icons_thing_r3_backpack;
            }
            if (!(Global.system_dummy["gloves"] == null) && ((Global.system_dummy["gloves"] as Hashtable)["thingname"] == "thing_r3_hand"))
            {
                (Global.system_dummy["gloves"] as Hashtable)["icon"] = this.gs_common.gui_icons_thing_r3_hand;
            }
            if (!(Global.system_dummy["weapon"] == null) && ((Global.system_dummy["weapon"] as Hashtable)["thingname"] == "thing_r3_gun"))
            {
                (Global.system_dummy["weapon"] as Hashtable)["icon"] = this.gs_common.gui_icons_thing_r3_gun;
            }
            //R4
            if (!(Global.system_dummy["armor"] == null) && ((Global.system_dummy["armor"] as Hashtable)["thingname"] == "thing_r4_body"))
            {
                (Global.system_dummy["armor"] as Hashtable)["icon"] = this.gs_common.gui_icons_thing_r4_body;
            }
            if (!(Global.system_dummy["boots"] == null) && ((Global.system_dummy["boots"] as Hashtable)["thingname"] == "thing_r4_leg"))
            {
                (Global.system_dummy["boots"] as Hashtable)["icon"] = this.gs_common.gui_icons_thing_r4_leg;
            }
            if (!(Global.system_dummy["gloves"] == null) && ((Global.system_dummy["gloves"] as Hashtable)["thingname"] == "thing_r4_hand"))
            {
                (Global.system_dummy["gloves"] as Hashtable)["icon"] = this.gs_common.gui_icons_thing_r4_hand;
            }
            if (!(Global.system_dummy["weapon"] == null) && ((Global.system_dummy["weapon"] as Hashtable)["thingname"] == "thing_r4_gun"))
            {
                (Global.system_dummy["weapon"] as Hashtable)["icon"] = this.gs_common.gui_icons_thing_r4_gun;
            }
            //R5
            if (!(Global.system_dummy["backpack"] == null) && ((Global.system_dummy["backpack"] as Hashtable)["thingname"] == "thing_r5_backpack"))
            {
                (Global.system_dummy["backpack"] as Hashtable)["icon"] = this.gs_common.gui_icons_thing_r5_backpack;
            }
            Global._isR3Backpack = data._isR3Backpack;
            Global._isR3Body = data._isR3Body;
            Global._isR3Gun = data._isR3Gun;
            Global._isR3Hand = data._isR3Hand;
            Global._isR3Leg = data._isR3Leg;
            Global._isR4Backpack = data._isR4Backpack;
            Global._isR4Body = data._isR4Body;
            Global._isR4Gun = data._isR4Gun;
            Global._isR4Hand = data._isR4Hand;
            Global._isR4Leg = data._isR4Leg;
            Global._isR5Backpack = data._isR5Backpack;
            //Инвентарь
            Global.globalInventoryThingsArr = new System.Collections.Generic.List<Hashtable>(data.globalInventoryThingsArr);
            int i = 0;
            while (i < Global.globalInventoryThingsArr.Count)
            {
                 //Кристаллы
                if (Global.globalInventoryThingsArr[i]["thingname"] == "crystal_premium")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_crystal_premium;
                }
                //Предметы
                if (Global.globalInventoryThingsArr[i]["thingname"] == "thing_r3_backpack")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_thing_r3_backpack;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "thing_r3_hand")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_thing_r3_hand;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "thing_r3_gun")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_thing_r3_gun;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "thing_r4_body")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_thing_r4_body;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "thing_r4_leg")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_thing_r4_leg;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "thing_r4_hand")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_thing_r4_hand;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "thing_r4_gun")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_thing_r4_gun;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "thing_r5_backpack")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_thing_r5_backpack;
                }
                //Пища
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_chicken_soup")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_chicken_soup;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_cabbage_soup")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_cabbage_soup;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_mushroom_soup")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_mushroom_soup;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_mushroom_potatoes")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_mushroom_potatoes;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_fish_soup")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_fish_soup;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_pancake")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_pancake;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_smoothies")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_smoothies;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_milkshape")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_milkshape;
                }
                //Продукты - компоненты
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_banana")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_banana;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_berries")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_berries;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_cabbage")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_cabbage;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_carrot")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_carrot;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_chicken")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_chicken;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_egg")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_egg;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_fish")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_fish;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_flour")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_flour;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_milk")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_milk;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_mushrooms")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_mushrooms;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_onion")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_onion;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_orange")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_orange;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_potatoes")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_potatoes;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_sourcream")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_sourcream;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_spice")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_spice;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_beans")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_beans;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_chickpea")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_chickpea;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_corn")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_corn;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_kidney_beans")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_kidney_beans;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_lentils")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_lentils;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_peanut")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_peanut;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "food_peas")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_food_peas;
                }
                //Расходники
                if (Global.globalInventoryThingsArr[i]["thingname"] == "loot_metal")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_loot_metal;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "loot_plastic")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_loot_plastic;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "loot_cobweb")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_loot_cobweb;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "loot_deadspider")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_loot_deadspider;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "loot_eballon")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_loot_eballon;
                }
                if (Global.globalInventoryThingsArr[i]["thingname"] == "loot_cactus")
                {
                    Global.globalInventoryThingsArr[i]["icon"] = this.gs_common.gui_icons_loot_cactus;
                }
                i++;
            }
            //Склад
            Global.StorageArr = new System.Collections.Generic.List<Hashtable>(data.StorageArr);
            int j = 0;
            while (j < Global.StorageArr.Count)
            {
                 //Кристаллы
                if (Global.StorageArr[j]["thingname"] == "crystal_premium")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_crystal_premium;
                }
                //Предметы
                if (Global.StorageArr[j]["thingname"] == "thing_r3_backpack")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_thing_r3_backpack;
                }
                if (Global.StorageArr[j]["thingname"] == "thing_r3_hand")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_thing_r3_hand;
                }
                if (Global.StorageArr[j]["thingname"] == "thing_r3_gun")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_thing_r3_gun;
                }
                if (Global.StorageArr[j]["thingname"] == "thing_r4_body")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_thing_r4_body;
                }
                if (Global.StorageArr[j]["thingname"] == "thing_r4_leg")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_thing_r4_leg;
                }
                if (Global.StorageArr[j]["thingname"] == "thing_r4_hand")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_thing_r4_hand;
                }
                if (Global.StorageArr[j]["thingname"] == "thing_r4_gun")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_thing_r4_gun;
                }
                if (Global.StorageArr[j]["thingname"] == "thing_r5_backpack")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_thing_r5_backpack;
                }
                //Пища
                if (Global.StorageArr[j]["thingname"] == "food_chicken_soup")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_chicken_soup;
                }
                if (Global.StorageArr[j]["thingname"] == "food_cabbage_soup")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_cabbage_soup;
                }
                if (Global.StorageArr[j]["thingname"] == "food_mushroom_soup")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_mushroom_soup;
                }
                if (Global.StorageArr[j]["thingname"] == "food_mushroom_potatoes")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_mushroom_potatoes;
                }
                if (Global.StorageArr[j]["thingname"] == "food_fish_soup")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_fish_soup;
                }
                if (Global.StorageArr[j]["thingname"] == "food_pancake")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_pancake;
                }
                if (Global.StorageArr[j]["thingname"] == "food_smoothies")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_smoothies;
                }
                if (Global.StorageArr[j]["thingname"] == "food_milkshape")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_milkshape;
                }
                //Продукты - компоненты
                if (Global.StorageArr[j]["thingname"] == "food_banana")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_banana;
                }
                if (Global.StorageArr[j]["thingname"] == "food_berries")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_berries;
                }
                if (Global.StorageArr[j]["thingname"] == "food_cabbage")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_cabbage;
                }
                if (Global.StorageArr[j]["thingname"] == "food_carrot")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_carrot;
                }
                if (Global.StorageArr[j]["thingname"] == "food_chicken")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_chicken;
                }
                if (Global.StorageArr[j]["thingname"] == "food_egg")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_egg;
                }
                if (Global.StorageArr[j]["thingname"] == "food_fish")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_fish;
                }
                if (Global.StorageArr[j]["thingname"] == "food_flour")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_flour;
                }
                if (Global.StorageArr[j]["thingname"] == "food_milk")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_milk;
                }
                if (Global.StorageArr[j]["thingname"] == "food_mushrooms")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_mushrooms;
                }
                if (Global.StorageArr[j]["thingname"] == "food_onion")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_onion;
                }
                if (Global.StorageArr[j]["thingname"] == "food_orange")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_orange;
                }
                if (Global.StorageArr[j]["thingname"] == "food_potatoes")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_potatoes;
                }
                if (Global.StorageArr[j]["thingname"] == "food_sourcream")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_sourcream;
                }
                if (Global.StorageArr[j]["thingname"] == "food_spice")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_food_spice;
                }
                //Расходники
                if (Global.StorageArr[j]["thingname"] == "loot_metal")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_loot_metal;
                }
                if (Global.StorageArr[j]["thingname"] == "loot_plastic")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_loot_plastic;
                }
                if (Global.StorageArr[j]["thingname"] == "loot_cobweb")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_loot_cobweb;
                }
                if (Global.StorageArr[j]["thingname"] == "loot_deadspider")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_loot_deadspider;
                }
                if (Global.StorageArr[j]["thingname"] == "loot_eballon")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_loot_eballon;
                }
                if (Global.StorageArr[j]["thingname"] == "loot_cactus")
                {
                    Global.StorageArr[j]["icon"] = this.gs_common.gui_icons_loot_cactus;
                }
                j++;
            }
            //Ресурсы для крафта и кухни
            Global._userResourcesCraft = (Hashtable) data._userResourcesCraft.Clone();
            Global._userResourcesFood = (Hashtable) data._userResourcesFood.Clone();
            //Загружаем данные туториала
            Global._isTutorial01 = data._isTutorial01;
            Global._isTutorial02 = data._isTutorial02;
            Global._isTutorial03 = data._isTutorial03;
            Global._isTutorial04 = data._isTutorial04;
            Global._isTutorial05 = data._isTutorial05;
            Global._isTutorial06 = data._isTutorial06;
            Global._isTutorial06_1 = data._isTutorial06_1;
            Global._isTutorial07 = data._isTutorial07;
            Global._isTutorial08 = data._isTutorial08;
            Global._isTutorial09 = data._isTutorial09;
            Global._isTutorialCreateSoup = data._isTutorialCreateSoup;
            Global._isTutorial10 = data._isTutorial10;
            Global._isTutorial11 = data._isTutorial11;
            Global._isTutorial12 = data._isTutorial12;
            Global._isTutorialLearnSmousi = data._isTutorialLearnSmousi;
            Global._isTutorial14 = data._isTutorial14;
            Global._isTutorial15 = data._isTutorial15;
            Global._isTutorial16 = data._isTutorial16;
            Global._isTutorial17 = data._isTutorial17;
            Global._isTutorial18 = data._isTutorial18;
            Global._isTutorial19 = data._isTutorial19;
            Global._isTutorial20 = data._isTutorial20;
            Global._isTutorial21 = data._isTutorial21;
            Global._isTutorial22 = data._isTutorial22;
            Global._isTutorial23 = data._isTutorial23;
            Global._isTutorial24 = data._isTutorial24;
            Global._isTutorial25 = data._isTutorial25;
            Global._isTutorial26 = data._isTutorial26;
            Global._isTutorial27 = data._isTutorial27;
            Global._isTutorial28 = data._isTutorial28;
            Global._isTutorial29 = data._isTutorial29;
            Global._isTutorial30 = data._isTutorial30;
            Global._isTutorialVeGameEnd = data._isTutorialVeGameEnd;
            Global._isTutorial31 = data._isTutorial31;
            Global._isTutorial32 = data._isTutorial32;
            Global._isTutorial33 = data._isTutorial33;
            Global._isTutorial33Video = data._isTutorial33Video;
            Global._isTutorial34 = data._isTutorial34;
            Global._isTutorial35 = data._isTutorial35;
            Global._isTutorial36 = data._isTutorial36;
            Global._isTutorial37 = data._isTutorial37;
            Global._isTutorial37Video = data._isTutorial37Video;
            Global._isTutorial38 = data._isTutorial38;
            Global._isTutorial38_1 = data._isTutorial38_1;
            Global._isTutorial39 = data._isTutorial39;
            Global._isTutorial40 = data._isTutorial40;
            Global._isTutorial41 = data._isTutorial41;
            Global._isTutorial42 = data._isTutorial42;
            Global._isTutorial43 = data._isTutorial43;
            Global._isTutorial44 = data._isTutorial44;
            Global._isTutorial45 = data._isTutorial45;
            Global._isTutorialStorage = data._isTutorialStorage;
            Global._isTutorialInventory = data._isTutorialInventory;
            Global._isTutorialGoToGreenHouseAgain = data._isTutorialGoToGreenHouseAgain;
            Global._isTutorialDestroyCobweb = data._isTutorialDestroyCobweb;
            Global._isTutorialDialogueSpiderEnd = data._isTutorialDialogueSpiderEnd;
            Global._isTutorialSpiderAttack = data._isTutorialSpiderAttack;
            Global._isTutorialSpiderClick = data._isTutorialSpiderClick;
            Global._isTutorialAbilityClick = data._isTutorialAbilityClick;
            Global._isTutorialWaitingForTheSun = data._isTutorialWaitingForTheSun;
            Global._isTutorialMushroomGameEnd = data._isTutorialMushroomGameEnd;
            Global._isTutorialMushroomVideo = data._isTutorialMushroomVideo;
            Global._isTutorialEnd = data._isTutorialEnd;
            Global._isTutorialVeGameComplete = data._isTutorialVeGameComplete;
            Global._isTutorialSpidersBookComplete = data._isTutorialSpidersBookComplete;
            Global._isTutorialSkafBookComplete = data._isTutorialSkafBookComplete;
            //ХОГи
            Global._isHOG1Complete = data._isHOG1Complete;
            Global._isHOG2Complete = data._isHOG2Complete;
            Global._isHOG3Complete = data._isHOG3Complete;
            //Подземелье
            Global.TempleOpenDoor = data.TempleOpenDoor;
            Global._isOpenPyramid = data._isOpenPyramid;
            Global._isFirstVisitTemple = data._isFirstVisitTemple;
            Global._isClosedDoorDialogue = data._isClosedDoorDialogue;
            Global.TempleExitDoor = data.TempleExitDoor;
            Global._isTakeKey = data._isTakeKey;
            Global._isRemoteKey = data._isRemoteKey;
            Global._isClickKey = data._isClickKey;
            Global._isGetKey = data._isGetKey;
            Global.MosaicAvailable = data.MosaicAvailable;
            Global.MosaicComplete = data.MosaicComplete;
            Global.MosaicFirstEntry = data.MosaicFirstEntry;
            //Музей
            Global._userMuseum = (Hashtable) data._userMuseum.Clone();
            (Global._userMuseum["museum_exhibit01"] as Hashtable)["titleimage"] = this.gs_common.gui_exhibit_title_cd;
            (Global._userMuseum["museum_exhibit02"] as Hashtable)["titleimage"] = this.gs_common.gui_exhibit_title_ammonit;
            Global._isViewZoeStory = data._isViewZoeStory;
            Global._isLearnZoeStory = data._isLearnZoeStory;
            //Библиотека
            Global._userBooksHealth = (Hashtable) data._userBooksHealth.Clone();
            Global._userBooksBiology = (Hashtable) data._userBooksBiology.Clone();
            Global._userBooksTech = (Hashtable) data._userBooksTech.Clone();
            (Global._userBooksHealth["library_book01"] as Hashtable)["titleimage"] = this.gs_common.gui_library_booktitle_food;
            (Global._userBooksBiology["library_book01"] as Hashtable)["titleimage"] = this.gs_common.gui_library_booktitle_vegetables;
            (Global._userBooksBiology["library_book02"] as Hashtable)["titleimage"] = this.gs_common.gui_library_booktitle_mushrooms;
            (Global._userBooksBiology["library_book03"] as Hashtable)["titleimage"] = this.gs_common.gui_library_booktitle_spiders;
            (Global._userBooksTech["library_book01"] as Hashtable)["titleimage"] = this.gs_common.gui_library_booktitle_spacesuits;
            //Рецепты
            Global._recipeItems = new System.Collections.Generic.List<Hashtable>(data._recipeItems);
            Global._recipeFood = new System.Collections.Generic.List<Hashtable>(data._recipeFood);
            //Крафт предметов
            Global._recipeItems[0]["icon"] = this.gs_common.gui_RecipeItemIcon01;
            //(Global._recipeItems[1])["icon"] = gs_common.gui_RecipeItemIcon02;
            Global._recipeItems[1]["icon"] = this.gs_common.gui_RecipeItemIcon03;
            Global._recipeItems[2]["icon"] = this.gs_common.gui_RecipeItemIcon04;
            Global._recipeItems[3]["icon"] = this.gs_common.gui_RecipeItemIcon05;
            Global._recipeItems[0]["costicon1"] = this.gs_common.gui_icons_loot_metal;
            Global._recipeItems[0]["costicon2"] = this.gs_common.gui_icons_loot_plastic;
            /*(Global._recipeItems[1])["costicon1"] = gs_common.gui_icons_loot_cobweb;
		(Global._recipeItems[1])["costicon2"] = gs_common.gui_icons_loot_deadspider;
		(Global._recipeItems[1])["costicon3"] = gs_common.gui_icons_loot_metal;*/
            Global._recipeItems[1]["costicon1"] = this.gs_common.gui_icons_loot_metal;
            Global._recipeItems[1]["costicon2"] = this.gs_common.gui_icons_loot_eballon;
            Global._recipeItems[2]["costicon1"] = this.gs_common.gui_icons_loot_metal;
            Global._recipeItems[2]["costicon2"] = this.gs_common.gui_icons_loot_plastic;
            Global._recipeItems[3]["costicon1"] = this.gs_common.gui_icons_loot_cobweb;
            Global._recipeItems[3]["costicon2"] = this.gs_common.gui_icons_loot_deadspider;
            Global._recipeItems[3]["costicon3"] = this.gs_common.gui_icons_loot_metal;
            //Крафт еды
            Global._recipeFood[0]["icon"] = this.gs_common.gui_icons_food_chicken_soup;
            Global._recipeFood[1]["icon"] = this.gs_common.gui_icons_food_cabbage_soup;
            Global._recipeFood[2]["icon"] = this.gs_common.gui_icons_food_mushroom_soup;
            Global._recipeFood[3]["icon"] = this.gs_common.gui_icons_food_mushroom_potatoes;
            Global._recipeFood[4]["icon"] = this.gs_common.gui_icons_food_fish_soup;
            Global._recipeFood[5]["icon"] = this.gs_common.gui_icons_food_pancake;
            Global._recipeFood[6]["icon"] = this.gs_common.gui_icons_food_smoothies;
            Global._recipeFood[7]["icon"] = this.gs_common.gui_icons_food_milkshape;
            Global._recipeFood[0]["costicon1"] = this.gs_common.gui_icons_food_chicken;
            Global._recipeFood[0]["costicon2"] = this.gs_common.gui_icons_food_potatoes;
            Global._recipeFood[0]["costicon3"] = this.gs_common.gui_icons_food_carrot;
            Global._recipeFood[0]["costicon4"] = this.gs_common.gui_icons_food_onion;
            Global._recipeFood[1]["costicon1"] = this.gs_common.gui_icons_food_cabbage;
            Global._recipeFood[1]["costicon2"] = this.gs_common.gui_icons_food_potatoes;
            Global._recipeFood[1]["costicon3"] = this.gs_common.gui_icons_food_carrot;
            Global._recipeFood[1]["costicon4"] = this.gs_common.gui_icons_food_sourcream;
            Global._recipeFood[2]["costicon1"] = this.gs_common.gui_icons_food_mushrooms;
            Global._recipeFood[2]["costicon2"] = this.gs_common.gui_icons_food_potatoes;
            Global._recipeFood[2]["costicon3"] = this.gs_common.gui_icons_food_onion;
            Global._recipeFood[2]["costicon4"] = this.gs_common.gui_icons_food_sourcream;
            Global._recipeFood[3]["costicon1"] = this.gs_common.gui_icons_food_mushrooms;
            Global._recipeFood[3]["costicon2"] = this.gs_common.gui_icons_food_potatoes;
            Global._recipeFood[4]["costicon1"] = this.gs_common.gui_icons_food_fish;
            Global._recipeFood[4]["costicon2"] = this.gs_common.gui_icons_food_potatoes;
            Global._recipeFood[4]["costicon3"] = this.gs_common.gui_icons_food_onion;
            Global._recipeFood[4]["costicon4"] = this.gs_common.gui_icons_food_spice;
            Global._recipeFood[5]["costicon1"] = this.gs_common.gui_icons_food_flour;
            Global._recipeFood[5]["costicon2"] = this.gs_common.gui_icons_food_egg;
            Global._recipeFood[5]["costicon3"] = this.gs_common.gui_icons_food_milk;
            Global._recipeFood[5]["costicon4"] = this.gs_common.gui_icons_food_sourcream;
            Global._recipeFood[6]["costicon1"] = this.gs_common.gui_icons_food_orange;
            Global._recipeFood[6]["costicon2"] = this.gs_common.gui_icons_food_banana;
            Global._recipeFood[6]["costicon3"] = this.gs_common.gui_icons_food_berries;
            Global._recipeFood[7]["costicon1"] = this.gs_common.gui_icons_food_banana;
            Global._recipeFood[7]["costicon2"] = this.gs_common.gui_icons_food_milk;
            //Рыбалка
            Global._isFirstFishing = data._isFirstFishing;
            stream.Close();
        }
        else
        {
        }
    }

}