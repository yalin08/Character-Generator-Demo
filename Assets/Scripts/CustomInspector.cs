using UnityEditor;
using UnityEngine;

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


    }
}