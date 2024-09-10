using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Transform playerTransform;
    public Inventory inGameInventory;
    public Player inGamePlayer;
    public AudioSource pistolSound;
    public AudioSource rifleSound;
    public AudioSource shotgunSound;
    private string saveFilePath;

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/gamestate.json";

        if(Parser.ResumePressed){
            LoadGameFromFile();
            Parser.ResumePressed = false;
        }
        if(Parser.loadParser)
            LoadParser();
    }

    private void LoadParser(){
        inGameInventory.maxLife = Parser.maxLife;
        inGameInventory.life = Parser.life;
        inGameInventory.moveSpeed = Parser.moveSpeed;
        inGameInventory.dmgUp = Parser.dmgUp;
        inGameInventory.bulletNumber = Parser.bulletNumber;
        inGameInventory.ratioBuff = Parser.ratioBuff;
        inGameInventory.areaBuff = Parser.areaBuff;
        inGameInventory.silverKey = Parser.silverKey;
        inGameInventory.goldenKey  = Parser.goldenKey;
        inGameInventory.currentExp = Parser.currentExp;
        inGameInventory.maxExp = Parser.maxExp;
        inGameInventory.currentLevel = Parser.currentLevel;
        inGameInventory.BulletForceBuff = Parser.BulletForceBuff;
        inGameInventory.SpeedUpRool = Parser.SpeedUpRool;
        inGameInventory.areaExplosionsEffectBuff = Parser.areaExplosionsEffectBuff;
        EquipWeaponByName(Parser.weapon);
        Parser.loadParser = false;
    }

    private IEnumerator attackWeapon(GameObject x){
        yield return new WaitForSeconds(0.1f);
        Player.PlayerManager.EquipWeapon(x);
    }

    public PlayerState GetCurrentPlayerState(){
        PlayerState state = new PlayerState{
            playerPosition = playerTransform.position,
            maxLife = inGameInventory.maxLife,
            life = inGameInventory.life,
            moveSpeed = inGameInventory.moveSpeed,
            dmgUp = inGameInventory.dmgUp,
            bulletNumber = inGameInventory.bulletNumber,
            ratioBuff = inGameInventory.ratioBuff,
            areaBuff = inGameInventory.areaBuff,
            silverKey = inGameInventory.silverKey,
            goldenKey  = inGameInventory.goldenKey,
            currentExp = inGameInventory.currentExp,
            maxExp = inGameInventory.maxExp,
            currentLevel = inGameInventory.currentLevel,
            BulletForceBuff = inGameInventory.BulletForceBuff,
            SpeedUpRool = inGameInventory.SpeedUpRool,
            areaExplosionsEffectBuff = inGameInventory.areaExplosionsEffectBuff,
            counterEnemyKill = Parser.counterEnemyKill,
            counterBossKill = Parser.counterBossKill,

            weaponName = GetEquippedWeaponName(inGamePlayer),
            
            sceneName = SceneManager.GetActiveScene().name,
            aliveEnemyIds = GetAllEntityIds<Enemy>(),
            existingKeys = GetAllEntityIds<Key>(),
            doorsStates = GetAllDoorStates(),
            leverStates = GetAllLeverStates(),

            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f),
            effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 0.5f)
        };
        return state;
    }

    public void LoadPlayerState(PlayerState state){
        if (state != null){
            playerTransform.position = state.playerPosition;
            inGameInventory.maxLife = state.maxLife;
            inGameInventory.life = state.life;
            inGameInventory.moveSpeed = state.moveSpeed;
            inGameInventory.dmgUp = state.dmgUp;
            inGameInventory.bulletNumber = state.bulletNumber;
            inGameInventory.ratioBuff = state.ratioBuff;
            inGameInventory.areaBuff = state.areaBuff;
            inGameInventory.silverKey = state.silverKey;
            inGameInventory.goldenKey = state.goldenKey;
            inGameInventory.currentExp = state.currentExp;
            inGameInventory.maxExp = state.maxExp;
            inGameInventory.currentLevel = state.currentLevel;
            inGameInventory.SpeedUpRool = state.SpeedUpRool;
            inGameInventory.BulletForceBuff = state.BulletForceBuff;
            inGameInventory.BulletForceBuff = state.areaExplosionsEffectBuff;
            Parser.counterBossKill = state.counterBossKill;
            Parser.counterBossKill = state.counterBossKill;
            
            EquipWeaponByName(state.weaponName);

            DestroyEnemiesNotInList(state.aliveEnemyIds);
            DestroyKeysNotInList(state.existingKeys);
            SetAllDoorStates(state.doorsStates);
            SetAllLeverStates(state.leverStates);

            PlayerPrefs.SetFloat("MusicVolume", state.musicVolume);
            PlayerPrefs.SetFloat("EffectsVolume", state.effectsVolume);
        }
    }

    private void LoadGameFromFile(){
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerState savedState = JsonUtility.FromJson<PlayerState>(json);
            LoadPlayerState(savedState);
        }
    }

    private int GetEquippedWeaponName(Player player){
        GameObject actualWeapon = inGamePlayer.GetEquippedWeapon();
        return actualWeapon != null ? actualWeapon.GetComponent<Weapon>().getType() : -1;
    }

    private void EquipWeaponByName(int weaponName){
    
        GameObject x = new();
        if(weaponName == 1){
            x = Instantiate(Player.PlayerManager.weapon1Prefabs,Player.PlayerManager.transform.position, Player.PlayerManager.transform.rotation);
            x.GetComponent<Weapon>().firingSound = pistolSound;
        }
        if(weaponName == 2)
            {x = Instantiate(Player.PlayerManager.weapon2Prefabs,Player.PlayerManager.transform.position, Player.PlayerManager.transform.rotation);
            x.GetComponent<Weapon>().firingSound = rifleSound;}
        if(weaponName == 3)
            {x = Instantiate(Player.PlayerManager.weapon3Prefabs,Player.PlayerManager.transform.position, Player.PlayerManager.transform.rotation);
            x.GetComponent<Weapon>().firingSound = shotgunSound;}
        StartCoroutine(attackWeapon(x));
    }

    public static List<string> GetAllEntityIds<T>() where T : MonoBehaviour, IIdentifiable{
        List<string> entityIds = new List<string>();
        T[] allEntities = GameObject.FindObjectsOfType<T>();
        
        foreach (T entity in allEntities){
            if (entity != null){
                entityIds.Add(entity.GetId());
            }
        }
        return entityIds;
    }

    public static List<DoorState> GetAllDoorStates(){
        List<DoorState> doorStates = new List<DoorState>();
        Door[] allDoors = FindObjectsOfType<Door>();


        foreach (Door door in allDoors){
            if (door != null){
                doorStates.Add(
                    new DoorState{
                        doorId = door.GetId(),
                        isOpen = door.IsOpen(),
                });
            }
        }
        return doorStates;
    }

    public static List<LeverState> GetAllLeverStates(){
        List<LeverState> leverStates = new List<LeverState>();
        Lever[] allLever = FindObjectsOfType<Lever>();

        foreach (Lever lever in allLever){
            if (lever != null){
                leverStates.Add(
                    new LeverState{
                        leverId = lever.GetId(),
                        isOpen = lever.IsOpen(),
                });
            }
        }
        return leverStates;
    }


    public static void DestroyEnemiesNotInList(List<string> aliveEnemyIds){
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in allEnemies){
            if (enemy != null && !aliveEnemyIds.Contains(enemy.GetId())){   
                enemy.kill();
            }
        }
    }

    public static void DestroyKeysNotInList(List<string> existingKeysIds){
        Key[] allKeys = FindObjectsOfType<Key>();

        foreach (Key key in allKeys){
            if (key != null && !existingKeysIds.Contains(key.GetId())){
                Destroy(key.gameObject);
            }
        }
    }

    public void SetAllDoorStates(List<DoorState> doorStates){
        Dictionary<string, bool> doorStateDictionary = new Dictionary<string, bool>();
        foreach (DoorState state in doorStates){
            doorStateDictionary[state.doorId] = state.isOpen;
        }

        Door[] allDoors = GameObject.FindObjectsOfType<Door>();

        foreach (Door door in allDoors){
            if (doorStateDictionary.TryGetValue(door.doorId, out bool isOpen)){
                if (isOpen)
                    StartCoroutine(openDoor(door));
            }
        }
    }



    private IEnumerator openDoor(Door door){
        yield return new WaitForSeconds(0.1f);
        door.OpenDoor();
    }


    public void SetAllLeverStates(List<LeverState> leverStates){
        Dictionary<string, bool> leverStateDictionary = new Dictionary<string, bool>();
        foreach (LeverState state in leverStates){
            leverStateDictionary[state.leverId] = state.isOpen;
        }

        Lever[] allLevers = GameObject.FindObjectsOfType<Lever>();

        foreach (Lever lever in allLevers){
            if (leverStateDictionary.TryGetValue(lever.leverId, out bool isOpen)){
                if (isOpen){
                    lever.actualState = isOpen;
                    StartCoroutine(openLever(lever));
                }
            }
        }
    }

    private IEnumerator openLever(Lever lever){
        yield return new WaitForSeconds(0.1f);
        lever.animator.SetBool("Active", true);
        lever.OpenGate();    
    }


}
