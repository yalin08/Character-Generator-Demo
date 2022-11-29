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
public enum Personality
{
    some, thing, cursed
}

public class RandomCharacterGenerator : MonoBehaviour
{
    public CharacterSpriteFinder CharacterSpriteFinder;

    public CharacterParts[] characterParts;
    public string charName;
    public string personality;
    public SpriteRenderer face;

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


    }

    public void Randomize()
    {

        charName = CharacterSpriteFinder.gameObject.GetComponent<NameGenerator>().generateRandomName();
        for (int i = 0; i < characterParts.Length; i++)
        {


            int j = Random.Range(0, CharacterSpriteFinder.CharactersList.Count);
            characterParts[i].thatLimbSpriteRenderer.sprite = CharacterSpriteFinder.CharactersList[j].limbSprite[i];
            int namesCount = (System.Enum.GetValues(typeof(Element)).Length);
            int k = Random.Range(0, namesCount);

            characterParts[i].element = (Element)k;

            characterParts[i].thatLimbSpriteRenderer.color = ElementColor((Element)k);

         
            int l = Random.Range(0, CharacterSpriteFinder.faces.Count);

            personality = CharacterSpriteFinder.faces[l].Personality;
            face.sprite = CharacterSpriteFinder.faces[l].Face;

        }



        // nameGenerator.generateRandomName();

    }

    public Color ElementColor(Element element)
    {





        foreach (Colors cl in CharacterSpriteFinder.colors)
        {
            if (""+cl.colorName == "" + element)
            {
                return cl.color;
            }

        }


        return Color.white;
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Randomize();
        }
    }
}
