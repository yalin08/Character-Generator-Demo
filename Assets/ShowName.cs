using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pixelplacement;
public class ShowName : Singleton<ShowName>
{
    public TextMeshProUGUI Text;
    private void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        Text.text = PreviousCharacters.Instance.CurrentCharacter.name;
    }
}
