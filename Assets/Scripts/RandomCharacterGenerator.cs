using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class CharacterParts
{
    public string thatLimbName;
    public GameObject thatLimb;
    public SpriteRenderer thatLimbSpriteRenderer;
    public Element element;
}

public enum Element
{
    none, Fire, Grass, Water
}


public class RandomCharacterGenerator : MonoBehaviour
{
    public CharacterSpriteFinder CharacterSpriteFinder;

    public CharacterParts[] characterParts;
    public string charName;
    public string personality;


    public SpriteRenderer face;
    CharacterStats cs;
    StatGiver sg;

    private void OnValidate()
    {
        foreach (Transform child in transform)
        {

            for (int i = 0; i < characterParts.Length; i++)
            {

                if (child.name == characterParts[i].thatLimbName)
                {
                    Debug.Log("aaaa");
                    characterParts[i].thatLimb = child.gameObject;
                    characterParts[i].thatLimbSpriteRenderer = child.GetComponent<SpriteRenderer>();
                }
            }

        }
        sg = CharacterSpriteFinder.gameObject.GetComponent<StatGiver>();
        cs = GetComponent<CharacterStats>();
    }



    void SavePrevious()
    {
        PreviousCharacters.Instance.PreviousCharactersList.Add(new PreviousCharactersList());
        PreviousCharactersList toBeSaved = PreviousCharacters.Instance.PreviousCharactersList[PreviousCharacters.Instance.PreviousCharactersList.Count - 1];
        PreviousCharactersList currentChar = PreviousCharacters.Instance.CurrentCharacter;
        toBeSaved.name = new string("" + charName);

        for (int i = 0; i < currentChar.limbElement.Length; i++)
        {

            toBeSaved.limbElement[i] = currentChar.limbElement[i];

        }
        for (int i = 0; i < currentChar.limbInt.Length; i++)
        {
            //  new int()  = limbInt[i];
            toBeSaved.limbInt[i] = currentChar.limbInt[i];


        }

        //PreviousCharacters.Instance.setStats(1, toBeSaved.stats);
        Stats tempList = new Stats();
        tempList.agi = cs.stats.agi;
        tempList.str = cs.stats.str;
        tempList.intl = cs.stats.intl;
        toBeSaved.stats = tempList;

        //    toBeSaved.stats = currentChar.stats;
        //  toBeSaved.Stats.intl = cs.stats.intl;


        toBeSaved.personality = currentChar.personality;


    }

    public void LoadPrevious()
    {
        PreviousCharactersList previousCharacter = PreviousCharacters.Instance.PreviousCharactersList[PreviousCharacters.Instance.PreviousCharactersList.Count - 1];
        PreviousCharactersList currentChar = PreviousCharacters.Instance.CurrentCharacter;
        charName = previousCharacter.name;
        currentChar.personality = previousCharacter.personality;

        for (int i = 0; i < currentChar.limbElement.Length; i++)
        {
            currentChar.limbElement[i] = previousCharacter.limbElement[i];
            characterParts[i].thatLimbSpriteRenderer.color = ElementColor(currentChar.limbElement[i]);
        }
        personality = CharacterSpriteFinder.faces[currentChar.personality].Personality;
        face.sprite = CharacterSpriteFinder.faces[currentChar.personality].Face;

        // PreviousCharacters.Instance.setStats(0, previousCharacter.stats);
        cs.stats = previousCharacter.stats;

      


        for (int i = 0; i < characterParts.Length; i++)
        {
            characterParts[i].thatLimbSpriteRenderer.sprite = CharacterSpriteFinder.CharactersList[previousCharacter.limbInt[i]].limbSprite[i];
        }
    }

    public void Randomize()
    {

        SavePrevious();

        charName = CharacterSpriteFinder.gameObject.GetComponent<NameGenerator>().generateRandomName();
        PreviousCharactersList currentCharacter = PreviousCharacters.Instance.CurrentCharacter;

        cs.ResetStats();
        for (int i = 0; i < characterParts.Length; i++)
        {


            int j = Random.Range(0, CharacterSpriteFinder.CharactersList.Count);
            characterParts[i].thatLimbSpriteRenderer.sprite = CharacterSpriteFinder.CharactersList[j].limbSprite[i];
            currentCharacter.limbInt[i] = j;
            int elementcount = (System.Enum.GetValues(typeof(Element)).Length);

            int k = Random.Range(0, elementcount);

            characterParts[i].element = (Element)k;
            currentCharacter.limbElement[i] = characterParts[i].element;
            characterParts[i].thatLimbSpriteRenderer.color = ElementColor((Element)k);


            int l = Random.Range(0, CharacterSpriteFinder.faces.Count);
            currentCharacter.personality = l;
            personality = CharacterSpriteFinder.faces[l].Personality;
            face.sprite = CharacterSpriteFinder.faces[l].Face;


            cs.stats.str += sg.stats[j].limbStats[i].str;
            cs.stats.agi += sg.stats[j].limbStats[i].agi;
            cs.stats.intl += sg.stats[j].limbStats[i].intl;
        }


        // nameGenerator.generateRandomName();

    }

    public Color ElementColor(Element element)
    {





        foreach (Colors cl in CharacterSpriteFinder.colors)
        {
            if ("" + cl.colorName == "" + element)
            {
                return cl.color;
            }

        }


        return Color.white;
    }


    private void Update()
    {

    }
}
