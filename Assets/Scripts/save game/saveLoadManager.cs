using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour{
    public void SaveGameState(PlayerState state, string filePath){
        try{
            string json = JsonUtility.ToJson(state);
            File.WriteAllText(filePath, json);
            Debug.Log(Application.persistentDataPath);
        }
        catch(System.Exception e){
            Debug.LogError("errore nel salvataggio, errore: " + e.Message);    
        }
    }

    public PlayerState LoadGameState(string filePath){
        try{
            if (File.Exists(filePath)){
                string json = File.ReadAllText(filePath);
                Debug.Log("loaded");
                return JsonUtility.FromJson<PlayerState>(json);
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError("errore nel loading, errore: " + e.Message);
        }
        return null;
    }
    
}