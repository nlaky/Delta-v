using Content.Shared.Damage;
using Content.Shared.Damage.Systems;
using Content.Shared.FixedPoint;
using Content.Shared.Popups;
using Content.Shared.Weapons.Melee;

namespace Content.Shared._DV.Weapons.Melee;

public sealed class MeleeHealthDamageModifierSystem : EntitySystem
{
    [Dependency] private readonly DamageableSystem _damageable = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MeleeHealthDamageModifierComponent, DamageChangedEvent>(OnDamageChanged);
        // TODO inventory/hand relayed damage change?
    }

    private void OnDamageChanged(Entity<MeleeHealthDamageModifierComponent> ent, ref DamageChangedEvent args)
    {
        // method is deprecated but can't find any other way to do this
        var damageTotal = _damageable.GetTotalDamage((ent, args.Damageable));

        if (GetCurrentStage(ent.Comp.Stages, damageTotal) is not { } stage)
            return;

        UpdateDamageStage(ent, stage, ent.Owner);
    }

    private void UpdateDamageStage(Entity<MeleeHealthDamageModifierComponent> ent, MeleeHealthDamageModifierStage stage, EntityUid? user)
    {
        // get out if this is already the stage we're in
        if (ent.Comp.StageIndex == stage.DamageBegin)
            return;

        TrySetMeleeDamage(ent, stage.MeleeDamage);

        var increased = ent.Comp.StageIndex < stage.DamageBegin;

        var locString = increased
            ? stage.IncreasedIntoMessage
            : stage.DecreasedIntoMessage;

        var popupType = increased
            ? stage.IncreasedPopupType
            : stage.DecreasedPopupType;

        if (locString is not null)
            _popup.PopupPredicted(Loc.GetString(locString, ("source", ent)), ent, user, popupType);

        ent.Comp.StageIndex = stage.DamageBegin;
        Dirty(ent);
    }

    private MeleeHealthDamageModifierStage? GetCurrentStage(List<MeleeHealthDamageModifierStage> list, FixedPoint2 damage)
    {
        MeleeHealthDamageModifierStage? stage = null;
        foreach (var current in list)
        {
            if (current.DamageBegin > damage)
                return stage;

            stage = current;
        }
        return stage;
    }

    // no api for this in MeleeWeaponSystem
    private void TrySetMeleeDamage(EntityUid uid, DamageSpecifier damage, MeleeWeaponComponent? melee = null)
    {
        if (!Resolve(uid, ref melee))
            return;

        melee.Damage = damage;
    }
}
