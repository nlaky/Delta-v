using Content.Shared.Damage;
using Content.Shared.Popups;
using Robust.Shared.Serialization;

namespace Content.Shared._DV.Weapons.Melee;

[DataDefinition, Serializable, NetSerializable]
public sealed partial class MeleeHealthDamageModifierStage
{
    /// <summary>
    /// The damage the weapon will do in this tier
    /// </summary>
    [DataField(required:true)]
    public DamageSpecifier Damage;

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
    /// The popup type to use when increasing into the stage
    /// </summary>
    [DataField]
    public PopupType IncreasedPopupType = PopupType.Small;

    /// <summary>
    /// The message for the popup shown when this tier is exited
    /// Leave null for no popup
    /// </summary>
    /// <remarks>
    /// This is only used when entering this tier from a higher one
    /// </remarks>
    [DataField]
    public string? DecreasedIntoMessage;

    /// <summary>
    /// The popup type to use when decreasing into the stage
    /// </summary>
    [DataField]
    public PopupType DecreasedPopupType = PopupType.Small;
}
