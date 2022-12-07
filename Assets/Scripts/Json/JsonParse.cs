using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class JsonParse : MonoBehaviour
{
 
    public NameData _nameData;

    public string namesFile = "Names.json";
    public string path;

#if UNITY_EDITOR
    private void OnValidate()
    {
          GetNameData();

     
    }
#endif

    [Serializable]
    public class NameData
    {
        public string[] nameArray;

        public string[] GetNamesArray()
        {
            return nameArray;
        }
    }

    public NameData GetNameData()
    {
        _nameData = GetDatas<NameData>(namesFile);

        return _nameData;
    }

    private T GetDatas<T>(string fileName)
    {
        string jsonString = GetJsonString(fileName);

        if (jsonString == null)
        {
            return default;
        }

        object resultValue = JsonUtility.FromJson<T>(jsonString);

        if (resultValue != null)
        {
            return (T)Convert.ChangeType(resultValue, typeof(T));
        }

        return default;
    }

    private string GetJsonString(string fileName)
    {
        string path = GetPath(fileName);

        if (!File.Exists(path))
        {
            Debug.LogWarning(fileName + " Not Finding");
            return "";
        }
        else
        {
            return File.ReadAllText(path);
        }
    


    }

    private string GetPath(string fileName)
    {
        return path + fileName;
    }

 

}
