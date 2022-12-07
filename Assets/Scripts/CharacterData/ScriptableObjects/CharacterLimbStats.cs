using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LimbsStats
{
 public string LimbName;
    public Stats stats;
}

public enum RarityEnum
{
    Common,Uncommon,Rare,Legendary
}

[CreateAssetMenu(fileName = "CharacterLimbStats", menuName = "ScriptableObjects/CharacterLimbStats")]
public class CharacterLimbStats : ScriptableObject
{
    public LimbsStats[] LimbStats=new LimbsStats[6];
    public Sprite[] sprites;
    public RarityEnum rarity;
}
