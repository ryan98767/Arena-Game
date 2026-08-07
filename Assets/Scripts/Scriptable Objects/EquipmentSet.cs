using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Set", menuName = "Items/Equipment Set")]
public class EquipmentSet : ScriptableObject
{
    public string setName;
    [TextArea(2, 4)] public string setDescription;

    [Header("Kit")]
    public AbilityData basicAttack;
    public AbilityData spell1;
    public AbilityData spell2;
    public AbilityData ultimate;
    public int ultimateCost;
}
