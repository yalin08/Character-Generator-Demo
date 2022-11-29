using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using Pixelplacement;



[System.Serializable]
public class CharacterFaces
{
    public string Personality;
    public Sprite Face;
}

[System.Serializable]
public class Characters
{
    public string Name;
    public List<Sprite> limbSprite;
   
}

[System.Serializable]
public class Colors
{
    public Element colorName;
    public Color color;
}

public class CharacterSpriteFinder : Singleton<CharacterSpriteFinder>
{

    public string SpritePathLocation;
    public string faceSpritePathLocation;
    public List<CharacterFaces> faces;

    List<GameObject> CharacterSpriteObject = new List<GameObject>();
    List<Sprite> faceSprites = new List<Sprite>();
    List<Sprite> sprites = new List<Sprite>();
    List<SpriteRenderer> spriterenderers = new List<SpriteRenderer>();

    public List<Characters> CharactersList = new List<Characters>();
    public Colors[] colors;

    NameGenerator nameGenerator;
    void OnValidate()
    {

        nameGenerator = GetComponent<NameGenerator>();

    }


    public void FindLimbsFromPSBFiles()
    {





        CharacterSpriteObject.Clear();
        faceSprites.Clear();
        foreach (string file in System.IO.Directory.GetFiles(SpritePathLocation))
        {

            string pathFile = file.Substring(file.Length - 4);

            if (pathFile == ".psb")
                CharacterSpriteObject.Add((GameObject)AssetDatabase.LoadAssetAtPath(file, typeof(GameObject)));


        }
        foreach (string file in System.IO.Directory.GetFiles(faceSpritePathLocation))
        {

            string pathFile = file.Substring(file.Length - 4);

            if (pathFile == ".png")
                faceSprites.Add((Sprite)AssetDatabase.LoadAssetAtPath(file, typeof(Sprite)));
        }




            LimbSorter();




    }

    public void LimbSorter()
    {
        CharactersList.Clear();

        for (int i = 0; i < CharacterSpriteObject.Count; i++)
        {
            string text = CharacterSpriteObject[i].name;





            CharactersList.Add(new Characters());
            CharactersList[i].Name = text;

        }
        for (int i = 0; i < CharactersList.Count; i++)
        {
            SpriteRenderer[] sr;
            List<Sprite> sprite = new List<Sprite>();
            sr = CharacterSpriteObject[i].GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer srr in sr)
            {
                sprite.Add(srr.sprite);
            }
            CharactersList[i].limbSprite = sprite;
            //  CharactersList[i].sr = sr;
        }

        for (int i = 0; i < faceSprites.Count; i++)
        {
            if (faces[i] == null)
            {
                faces.Add(new CharacterFaces());
            }

            faces[i].Face = faceSprites[i];
            faces[i].Personality = faceSprites[i].name;
        }

    }
}
