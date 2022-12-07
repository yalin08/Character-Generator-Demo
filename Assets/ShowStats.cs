using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pixelplacement;
public class ShowStats : Singleton<ShowStats>
{


     TextMeshProUGUI[] text;
    private void Start()
    {
        text = GetComponentsInChildren<TextMeshProUGUI>();
        UpdateStats();
    }
    public void UpdateStats()
    {
        foreach (var txt in text)
        {
            if (txt.name == "str")
            {
                txt.text ="Strength: "+ PreviousCharacters.Instance.CurrentCharacter.stats.str;
            } 
            if (txt.name == "int")
            {
                txt.text ="Intelligence: "+ PreviousCharacters.Instance.CurrentCharacter.stats.intl;
            } 
            if (txt.name == "agi")
            {
                txt.text ="Agility: "+ PreviousCharacters.Instance.CurrentCharacter.stats.agi;
            }
        }

                
    }
}
