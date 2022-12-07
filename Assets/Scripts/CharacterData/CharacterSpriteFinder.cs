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
    public string statsFolder;
    public List<CharacterFaces> faces;


    List<GameObject> CharacterSpriteObject = new List<GameObject>();
    ScriptableObjectHolder scriptableObjects;
    List<Sprite> faceSprites = new List<Sprite>();
    List<Sprite> sprites = new List<Sprite>();
    List<SpriteRenderer> spriterenderers = new List<SpriteRenderer>();

    public List<Characters> CharactersList = new List<Characters>();
    public Colors[] colors;


    void OnValidate()
    {


        scriptableObjects = transform.parent.GetComponentInChildren<ScriptableObjectHolder>();
    }
#if UNITY_EDITOR
    void UpdateRarity(CharacterLimbStats limbStats, string rarityString)
    {
        if (rarityString == "COM_")
        {
            limbStats.rarity = RarityEnum.Common;

        }
        if (rarityString == "UNC_")
        {
            limbStats.rarity = RarityEnum.Uncommon;

        }
        if (rarityString == "RAR_")
        {
            limbStats.rarity = RarityEnum.Rare;

        }
        if (rarityString == "LEG_")
        {
            limbStats.rarity = RarityEnum.Legendary;

        }
    }

    public void FindLimbsFromPSBFiles()
    {





        CharacterSpriteObject.Clear();
        faceSprites.Clear();
        //  statsObjects.Clear();

        foreach (string file in System.IO.Directory.GetFiles(SpritePathLocation))
        {


            string pathFile = file.Substring(file.Length - 4);
            if (pathFile == ".psb")
            {
                CharacterSpriteObject.Add((GameObject)AssetDatabase.LoadAssetAtPath(file, typeof(GameObject)));
                CharacterLimbStats asset = null;
                string createdFileName = file.Substring(SpritePathLocation.Length + 1);
                createdFileName = createdFileName.Remove(createdFileName.Length - 4);

                string rarity = createdFileName.Remove(4);
                // createdFileName = createdFileName.Substring(4);

                //     Debug.Log(createdFileName);


                if (!File.Exists(statsFolder + createdFileName + ".asset"))
                {
                    Debug.Log("first rarirtty " + rarity);
                    asset = ScriptableObject.CreateInstance<CharacterLimbStats>();

                    AssetDatabase.CreateAsset(asset, (statsFolder + createdFileName + ".asset"));
                    AssetDatabase.SaveAssets();


                }


            }


        }
        scriptableObjects.statsObjects.Clear();
        foreach (string file in System.IO.Directory.GetFiles(statsFolder))
        {
            string pathFile = file.Substring(file.Length - 6);

            if (pathFile == ".asset")
            {
                CharacterLimbStats stats = (CharacterLimbStats)AssetDatabase.LoadAssetAtPath(file, typeof(CharacterLimbStats));
                scriptableObjects.statsObjects.Add(stats);
                string createdFileName = file.Substring(statsFolder.Length );
                string rarity = createdFileName.Remove(4);
                Debug.Log("scnd rarirtty " + rarity);
                UpdateRarity(stats, rarity);
            }

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

            for (int j = 0; j < sr.Length; j++)
            {

                sprite.Add(sr[j].sprite);

                scriptableObjects.statsObjects[i].LimbStats[j].LimbName = sr[j].sprite.name;


            }
            CharactersList[i].limbSprite = sprite;
            scriptableObjects.statsObjects[i].sprites = sprite.ToArray();
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
        // StatGiver sg = GetComponent<StatGiver>();
        // sg.EqualizeNumbers();
    }
#endif
}
