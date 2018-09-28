using System.Linq.Expressions;
using RPG.Characters;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "RPG/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Weapon, Offhand, Head, Chest, Hands, Legs, Feet }
