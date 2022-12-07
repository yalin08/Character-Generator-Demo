using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;


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


public class RandomCharacterGenerator : Singleton<RandomCharacterGenerator>
{
    public CharacterSpriteFinder CharacterSpriteFinder;
    public ScriptableObjectHolder objectHolder;
    public CharacterParts[] characterParts;
    public string charName;
    public string personality;


    public SpriteRenderer face;
    CharacterStats cs;


    private void Start()
    {
        foreach (Transform child in transform)
        {

            for (int i = 0; i < characterParts.Length; i++)
            {

                if (child.name == characterParts[i].thatLimbName)
                {

                    characterParts[i].thatLimb = child.gameObject;
                    characterParts[i].thatLimbSpriteRenderer = child.GetComponent<SpriteRenderer>();
                }
            }

        }
        //sg = CharacterSpriteFinder.transform.parent.GetComponentInChildren<StatGiver>();

        objectHolder = CharacterSpriteFinder.transform.parent.GetComponentInChildren<ScriptableObjectHolder>();

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
        if (PreviousCharacters.Instance.PreviousCharactersList.Count == 0)
            SavePrevious();
        PreviousCharactersList currentCharacter = PreviousCharacters.Instance.CurrentCharacter;

        charName = NameGenerator.Instance.generateRandomName();
        currentCharacter.name = charName;
        cs.ResetStats();
        for (int i = 0; i < characterParts.Length; i++)
        {
            CharacterLimbStats[] characterLimbStats=null;
            float rarityCheck = Random.value * 100;
            Debug.Log(rarityCheck);
            if (rarityCheck < ScriptableObjectHolder.Instance.UncommonChance)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.UncommonObjects.ToArray();
            }
            else if (rarityCheck < ScriptableObjectHolder.Instance.RareChance)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.RareObjects.ToArray();
            }
            else if (rarityCheck < ScriptableObjectHolder.Instance.LegendaryChance)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.LegendaryObjects.ToArray();
            }
            else
            {
                characterLimbStats = ScriptableObjectHolder.Instance.CommonObjects.ToArray();
            }

            int j = Random.Range(0, characterLimbStats.Length);
           
            characterParts[i].thatLimbSpriteRenderer.sprite = characterLimbStats[j].sprites[i];
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


            cs.stats.str += characterLimbStats[j].LimbStats[i].stats.str;
            cs.stats.agi += characterLimbStats[j].LimbStats[i].stats.agi;
            cs.stats.intl += characterLimbStats[j].LimbStats[i].stats.intl;

        }

        currentCharacter.stats = cs.stats;
        SavePrevious();
        ShowName.Instance.UpdateText();
        ShowStats.Instance.UpdateStats();

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
