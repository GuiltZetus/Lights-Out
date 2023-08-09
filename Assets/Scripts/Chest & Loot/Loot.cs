using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public string ID;
    public Sprite lootSprite;
    public string lootName;
    public int dropChance;

    public Loot(string ID,string lootName, int dropChance) 
    {
        this.ID = ID;
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
}
