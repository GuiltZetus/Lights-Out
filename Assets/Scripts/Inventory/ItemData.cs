using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int width;
    public int height;

    public string Name;
    public Sprite itemIcon;
    public Sprite DarkBG;
    public int dropChance;

    public ItemData(string Name, int dropChance)
    {
        this.Name = Name;
        this.dropChance = dropChance;
    }
}
 