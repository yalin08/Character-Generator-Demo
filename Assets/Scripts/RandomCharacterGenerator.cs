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
    int personalityInt;
    public Element[] elements = new Element[6];
    public int[] limbInt = new int[6];

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
        var thatList = PreviousCharacters.Instance.PreviousCharactersList[PreviousCharacters.Instance.PreviousCharactersList.Count - 1];
        thatList.name = new string("" + charName);

        for (int i = 0; i < elements.Length; i++)
        {
           
            thatList.limbElement[i] = elements[i];

        }
        for (int i = 0; i < limbInt.Length; i++)
        {
          //  new int()  = limbInt[i];
            thatList.limbInt[i] = limbInt[i];
           

        }

        
        thatList.Stats = cs.stats;
        int prsn = new int();
        prsn = personalityInt;
        thatList.personality = prsn;


    }

    public void LoadPrevious()
    {
        var thatList = PreviousCharacters.Instance.PreviousCharactersList[PreviousCharacters.Instance.PreviousCharactersList.Count - 1];

        charName = thatList.name;
        personalityInt = thatList.personality;

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = thatList.limbElement[i];
            characterParts[i].thatLimbSpriteRenderer.color = ElementColor(elements[i]);
        }
        personality = CharacterSpriteFinder.faces[personalityInt].Personality;

        cs.stats= thatList.Stats ;

        for (int i = 0; i < characterParts.Length; i++)
        {
            characterParts[i].thatLimbSpriteRenderer.sprite = CharacterSpriteFinder.CharactersList[thatList.limbInt[i]].limbSprite[i];
        }
    }

    public void Randomize()
    {

        SavePrevious();

        charName = CharacterSpriteFinder.gameObject.GetComponent<NameGenerator>().generateRandomName();

        cs.ResetStats();
        for (int i = 0; i < characterParts.Length; i++)
        {


            int j = Random.Range(0, CharacterSpriteFinder.CharactersList.Count);
            characterParts[i].thatLimbSpriteRenderer.sprite = CharacterSpriteFinder.CharactersList[j].limbSprite[i];
            limbInt[i] = j;
            int elementcount = (System.Enum.GetValues(typeof(Element)).Length);

            int k = Random.Range(0, elementcount);

            characterParts[i].element = (Element)k;
            elements[i] = characterParts[i].element;
            characterParts[i].thatLimbSpriteRenderer.color = ElementColor((Element)k);


            int l = Random.Range(0, CharacterSpriteFinder.faces.Count);
            personalityInt = l;
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
