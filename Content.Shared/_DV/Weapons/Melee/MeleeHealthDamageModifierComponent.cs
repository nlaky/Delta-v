using Robust.Shared.GameStates;

namespace Content.Shared._DV.Weapons.Melee;

/// <summary>
/// Changes the damage done by an entity with an intrinsic MeleeWeaponComponent
/// based on the health of that entity.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class MeleeHealthDamageModifierComponent : Component
{
    /// <summary>
    /// A list of health-based stages with damage specifiers.
    /// </summary>
    /// <remarks>
    /// Needs to be ordered lowest damage first->highest damage last to work properly
    /// </remarks>
    [DataField(required: true)]
    public List<MeleeHealthDamageModifierStage> Stages;

    /// <summary>
    /// Contains the damage start amount of the current stage.
    /// Used to check if the stage has changed
    /// </summary>
    [DataField, AutoNetworkedField]
    public int StageIndex = 0;
}
