using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class ScriptableObjectHolder : Singleton<ScriptableObjectHolder>
{
   public List<CharacterLimbStats> statsObjects = new List<CharacterLimbStats>();

    [Range(1, 100)]
    public int UncommonChance;
    [Range(1, 100)]
    public int RareChance;
    [Range(1, 100)]
    public int LegendaryChance;

    public List<CharacterLimbStats> CommonObjects = new List<CharacterLimbStats>();
    public List<CharacterLimbStats> UncommonObjects = new List<CharacterLimbStats>();
    public List<CharacterLimbStats> RareObjects = new List<CharacterLimbStats>();
    public List<CharacterLimbStats> LegendaryObjects = new List<CharacterLimbStats>();

    private void Start()
    {
        DistributeObjects();
    }
    public void DistributeObjects()
    {
        CommonObjects.Clear();
        UncommonObjects.Clear();
        RareObjects.Clear();
        LegendaryObjects.Clear();


        foreach (CharacterLimbStats chr in statsObjects)
        {
            if (chr.rarity == RarityEnum.Common)
            {
                CommonObjects.Add(chr);
            }
            if (chr.rarity == RarityEnum.Uncommon)
            {
                UncommonObjects.Add(chr);

            }
            if (chr.rarity == RarityEnum.Rare)
            {
                RareObjects.Add(chr);
            }
            if (chr.rarity == RarityEnum.Legendary)
            {
                LegendaryObjects.Add(chr);
            }
        }
    }

}
