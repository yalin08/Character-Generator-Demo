using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharactersStats
{
    public string name;
    public Stats[] limbStats=new Stats[6];
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
    {stats.Clear();

        for (int i = 0; i < csf.statsObjects.Count; i++)
        {
            stats.Add(new CharactersStats());
            stats[i].name = csf.statsObjects[i].name;

            for (int j = 0; j < csf.statsObjects[i].LimbStats.Length; j++)
            {
                
                stats[i].limbStats[j] = csf.statsObjects[i].LimbStats[j].stats;
            }
        }


        for (int j = 0; j < csf.CharactersList.Count; j++)
        {


            stats[j].name = csf.CharactersList[j].Name;



        }
    }
}
