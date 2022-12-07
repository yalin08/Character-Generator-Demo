using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterSpriteFinder))]
public class CustomInspector1 : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CharacterSpriteFinder CSF = (CharacterSpriteFinder)target;
        //EditorGUILayout.LabelField("");
        if (GUILayout.Button("Get Characters"))
        {
            CSF.FindLimbsFromPSBFiles();
        }
   


    }

}
[CustomEditor(typeof(RandomCharacterGenerator))]
public class CustomInspector2 : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RandomCharacterGenerator rcg = (RandomCharacterGenerator)target;
        //EditorGUILayout.LabelField("");
        if (GUILayout.Button("Randomize"))
        {
            rcg.Randomize();
        }
        if (GUILayout.Button("LoadBefore"))
        {
            rcg.LoadPrevious();
        }

    }

}
[CustomEditor(typeof(JsonSave))]
public class CustomEditorJson : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        JsonSave save = (JsonSave)target;
        //EditorGUILayout.LabelField("");
        if (GUILayout.Button("Save"))
        {
            save.SaveData();
        }
        if (GUILayout.Button("Load"))
        {
            save.LoadData();
        }

    }

}
#endif