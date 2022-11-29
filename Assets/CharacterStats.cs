using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
   public Stats stats;

    public void ResetStats()
    {
        stats.str = 0;
        stats.intl = 0;
        stats.agi = 0;
    }
}
