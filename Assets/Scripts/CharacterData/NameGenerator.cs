using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
public class NameGenerator :Singleton<NameGenerator>
{
    public string Name;

    public JsonParse names;
    public JsonParse adjectives;

    string[] namesList;
    string[] adjectivesList;


    private void Start()
    {
        GetNames();
    }
 
    void GetNames()
    {
        namesList = names._nameData.nameArray;
        adjectivesList = adjectives._nameData.nameArray;




        for (int i = 0; i < adjectivesList.Length; i++)
        {
            adjectivesList[i] = (char.ToUpper(adjectivesList[i][0]) + adjectivesList[i].Substring(1));
        }
    }

    public string generateRandomName()
    {
      //  GetNames();
        int i = Random.Range(0, namesList.Length);
        int j = Random.Range(0, adjectivesList.Length);
        int k = Random.Range(0, 2);

        if (k == 0)
        {
            Name = namesList[i] + " the " + adjectivesList[j];
        }
        else if (k == 1)
        {
            Name = adjectivesList[j] + " " + namesList[i];
        }
      
        return Name;

    }
}
