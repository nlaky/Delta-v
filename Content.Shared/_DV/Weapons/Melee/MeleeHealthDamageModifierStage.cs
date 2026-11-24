using Content.Shared.Damage;
using Robust.Shared.Serialization;

namespace Content.Shared._DV.Weapons.Melee;

[DataDefinition, Serializable, NetSerializable]
public sealed partial class MeleeHealthDamageModifierStage
{
    /// <summary>
    /// The damage the weapon will do in this tier
    /// </summary>
    [DataField(required:true)]
    public DamageSpecifier MeleeDamage;

    /// <summary>
    /// The total amount of damage to the entity for this tier to become active
    /// </summary>
    [DataField]
    public int DamageBegin = 0;

    /// <summary>
    /// The message for the popup shown when this tier is entered
    /// Leave null for no popup
    /// </summary>
    /// <remarks>
    /// This is only used when entering this tier from a lower one
    /// </remarks>
    [DataField]
    public string? IncreasedIntoMessage;

    /// <summary>
    /// The message for the popup shown when this tier is exited
    /// Leave null for no popup
    /// </summary>
    /// <remarks>
    /// This is only used when entering this tier from a higher one
    /// </remarks>
    [DataField]
    public string? DecreasedIntoMessage;
}
