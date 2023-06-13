using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public PreviousCharactersList[] previousCharacters;
    public PreviousCharactersList newestCharacter;
}

public class JsonSave : MonoBehaviour
{

    string path;
    //     =Application.persistentDataPath+Path.AltDirectorySeparatorChar+"players.json";
    public SaveData saveData;

    PreviousCharacters previousScript;

    private void OnValidate()
    {
        previousScript = GetComponent<PreviousCharacters>();
 
    }
    void GetData()
    {
        path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "savedata.json";
        saveData.previousCharacters = previousScript.PreviousCharactersList.ToArray();
        saveData.newestCharacter = previousScript.CurrentCharacter;
    }
    void DistrubuteData()
    {

        previousScript.PreviousCharactersList=new List<PreviousCharactersList>(saveData.previousCharacters);
        previousScript.CurrentCharacter = saveData.newestCharacter;
    }

    public void SaveData()
    {
        GetData();
        string savePath = path;

        string json = JsonUtility.ToJson(saveData);
        //  string jsonCurrent = JsonUtility.ToJson(newestCharacter, true);

        Debug.Log(json);
        File.WriteAllText(path, json);

        StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        writer.Close();
    }
    public void LoadData()
    {
        path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "savedata.json";
        if (!File.Exists(path))
        {
            Debug.Log("asd");
            return;
        }

       
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        Debug.Log(path);
        Debug.Log(json);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        saveData = data;
        reader.Close();
        DistrubuteData();
        RandomCharacterGenerator.Instance.LoadPrevious(1);
    }
}
