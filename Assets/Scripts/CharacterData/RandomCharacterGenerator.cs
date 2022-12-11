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
        for (int i = 0; i < currentChar.limbRarity.Length; i++)
        {
            toBeSaved.limbRarity[i] = currentChar.limbRarity[i];
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
        PreviousCharactersList previousCharacter = PreviousCharacters.Instance.PreviousCharactersList[PreviousCharacters.Instance.PreviousCharactersList.Count - 2];

        charName = previousCharacter.name;
        PreviousCharacters.Instance.CurrentCharacter.name = charName;

        for (int i = 0; i < previousCharacter.limbElement.Length; i++)
        {
            previousCharacter.limbElement[i] = previousCharacter.limbElement[i];
            characterParts[i].thatLimbSpriteRenderer.color = ElementColor(previousCharacter.limbElement[i]);
        }
    
        personality = CharacterSpriteFinder.faces[previousCharacter.personality].Personality;
        face.sprite = CharacterSpriteFinder.faces[previousCharacter.personality].Face;

        // PreviousCharacters.Instance.setStats(0, previousCharacter.stats);
        cs.stats = previousCharacter.stats;
        PreviousCharacters.Instance.CurrentCharacter.stats = cs.stats;
        CharacterLimbStats[] characterLimbStats = null;


        for (int i = 0; i < characterParts.Length; i++)
        {
            if (previousCharacter.limbRarity[i] == RarityEnum.Common)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.CommonObjects.ToArray();
                characterParts[i].thatLimbSpriteRenderer.sprite =
                    characterLimbStats[previousCharacter.limbInt[i]].sprites[i];
              // CharacterSpriteFinder.CharactersList[previousCharacter.limbInt[i]].limbSprite[i];
            }
            if (previousCharacter.limbRarity[i] == RarityEnum.Uncommon)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.UncommonObjects.ToArray();
                characterParts[i].thatLimbSpriteRenderer.sprite =
                    characterLimbStats[previousCharacter.limbInt[i]].sprites[i];
              // CharacterSpriteFinder.CharactersList[previousCharacter.limbInt[i]].limbSprite[i];
            }
            if (previousCharacter.limbRarity[i] == RarityEnum.Rare)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.RareObjects.ToArray();
                characterParts[i].thatLimbSpriteRenderer.sprite =
                    characterLimbStats[previousCharacter.limbInt[i]].sprites[i];
              // CharacterSpriteFinder.CharactersList[previousCharacter.limbInt[i]].limbSprite[i];
            }
            if (previousCharacter.limbRarity[i] == RarityEnum.Legendary)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.LegendaryObjects.ToArray();
                characterParts[i].thatLimbSpriteRenderer.sprite =
                    characterLimbStats[previousCharacter.limbInt[i]].sprites[i];
              // CharacterSpriteFinder.CharactersList[previousCharacter.limbInt[i]].limbSprite[i];
            }
           
        }

        ShowName.Instance.UpdateText();
        ShowStats.Instance.UpdateStats();
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
            CharacterLimbStats[] characterLimbStats = null;
            float rarityCheck = Random.Range(0,101);
            Debug.Log(rarityCheck); 





             if (rarityCheck <= ScriptableObjectHolder.Instance.LegendaryChance)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.LegendaryObjects.ToArray();
                currentCharacter.limbRarity[i] = RarityEnum.Legendary;
            }
            else if (rarityCheck <= ScriptableObjectHolder.Instance.RareChance)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.RareObjects.ToArray();
                currentCharacter.limbRarity[i] = RarityEnum.Rare;
            }
            else if (rarityCheck <= ScriptableObjectHolder.Instance.UncommonChance)
            {
                characterLimbStats = ScriptableObjectHolder.Instance.UncommonObjects.ToArray();
                currentCharacter.limbRarity[i] = RarityEnum.Uncommon;
            }
         
           
            else
            {
                characterLimbStats = ScriptableObjectHolder.Instance.CommonObjects.ToArray();
                currentCharacter.limbRarity[i] = RarityEnum.Common;
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

            FaceLocations  fl= GetComponentInChildren<FaceLocations>();
            if(i==1)
            face.transform.localPosition = fl.faceLocations[characterLimbStats[j].FacePosition].localPosition;
           // face.transform.localPosition =;
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
