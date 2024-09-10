using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerState{
    public Vector2 playerPosition;
    public float maxLife;
    public float life;
    public float moveSpeed;
    public float dmgUp;
    public int bulletNumber; 
    public float ratioBuff;
    public float areaBuff;
    public int silverKey;
    public int goldenKey;
    public int currentExp;
    public int maxExp;
    public int currentLevel;
    public float BulletForceBuff;
    public float SpeedUpRool;
    public float areaExplosionsEffectBuff;
    public int counterBossKill;
    public int counterEnemyKill;
    public int weaponName;
    public string sceneName;
    public List<string> aliveEnemyIds = new List<string>(); 
    public List<string> existingKeys = new List<string>(); 
    public List<DoorState> doorsStates = new List<DoorState>();     
    public List<LeverState> leverStates = new List<LeverState>();
    public float musicVolume;
    public float effectsVolume;
}

[System.Serializable]
public class DoorState{
    public string doorId;
    public bool isOpen;
}

[System.Serializable]
public class LeverState{
    public string leverId;
    public bool isOpen;
}