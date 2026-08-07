using UnityEngine;

public enum EquipmentType
{
    Head,
    Body,
    Weapon,
    Accessory
}

[CreateAssetMenu(fileName = "EquipableItem", menuName = "ScriptableObjects/EquipableItem", order = 1)]
public class Gear : ScriptableObject
{
    public string gearName;

    public Sprite icon;
    public EquipmentType type;
    public EquipmentSet set;

    [Header("Stats")]
    public int physicalAttack;
    public int magicAttack;
    public int armor;
    public int resistance;
    public int health;

    [Header("Price")]
    public int price;
}






