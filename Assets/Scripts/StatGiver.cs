using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharactersStats
{
    public string name;
    public Stats[] limbStats;
}
[System.Serializable]
public class Stats
{
    public int str;
    public int agi;
    public int intl;

}


public class StatGiver : MonoBehaviour
{
    CharacterSpriteFinder csf;
    public List<CharactersStats> stats;
    Stats[] defaultLimbs = new Stats[6];
    private void OnValidate()
    {
        csf = GetComponent<CharacterSpriteFinder>();
    }

    public void EqualizeNumbers()
    {
        int difference = csf.CharactersList.Count - stats.Count;
        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                stats.Add(new CharactersStats());
                stats[i].limbStats = defaultLimbs;
                
            }
        }
           

        for (int j = 0; j < csf.CharactersList.Count; j++)
        {


            stats[j].name = csf.CharactersList[j].Name;



        }
    }
}
