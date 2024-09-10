using UnityEngine;

public class autoSaveManager : MonoBehaviour{

    public GameManager gameManager;
    public SaveLoadManager saveLoadManager;
    private float saveInterval = 3f;
    private float timeSinceLastSave = 0f;
    private string saveFilePath;

    void Start(){
        saveFilePath = Application.persistentDataPath + "/gameState.json";
    }

    void Update(){
        timeSinceLastSave += Time.deltaTime;
        if (timeSinceLastSave >= saveInterval){
            PlayerState state = gameManager.GetCurrentPlayerState();
            saveLoadManager.SaveGameState(state, saveFilePath);
            timeSinceLastSave =0f;
        }
        
    }

}
