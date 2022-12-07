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
 

   
}
