using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;


[System.Serializable]
public class PreviousCharactersList
{
    public string name;
    public Stats Stats;
    public int personality;
    public Element[] limbElement = new Element[6];
    public int[] limbInt = new int[6];
}

public class PreviousCharacters : Singleton<PreviousCharacters>
{
  public List<PreviousCharactersList> PreviousCharactersList; 
 
}
