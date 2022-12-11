using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILimbDetails : MonoBehaviour
{
    public TextMeshProUGUI limbSetText;
    public TextMeshProUGUI limbSetRarity;

    public TextMeshProUGUI[] stats;


    public void SetStats(int str, int agi, int intl)
    {
        stats[0].text = "" + str;
        stats[1].text = "" + agi;
        stats[2].text = "" + intl;
    }

    public void SetNames(string SetName,RarityEnum rarity)
    {
        limbSetText.text = SetName;
        limbSetRarity.text = ""+rarity;
    }
}
