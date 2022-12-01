using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LimbsStats
{
   [HideInInspector] public string LimbName;
    public Stats stats;
}

[CreateAssetMenu(fileName = "CharacterLimbStats", menuName = "ScriptableObjects/CharacterLimbStats")]
public class CharacterLimbStats : ScriptableObject
{
    public LimbsStats[] LimbStats=new LimbsStats[6];

}
