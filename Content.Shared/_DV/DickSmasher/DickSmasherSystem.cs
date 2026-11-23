using Content.Shared.Damage.Components;
using Content.Shared.Damage.Systems;
using Content.Shared.Interaction.Events;
using Content.Shared.Popups;
using JetBrains.Annotations;

namespace Content.Shared._DV.DickSmasher;

public sealed class DickSmasherSystem : EntitySystem
{
    [Dependency] private readonly DamageableSystem _damage = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<DickSmasherComponent, ComponentInit>(OnComponentInit);
        SubscribeLocalEvent<DickSmasherComponent, UseInHandEvent>(OnUseInHand);
    }

    [PublicAPI]
    public void TrySmash(Entity<DickSmasherComponent> ent, EntityUid? target)
    {
        if (GetCurrentOutcome(ent) is not { } outcome)
        {
            _popup.PopupClient("You can't bring yourself to do it...", ent, target);
            return;
        }

        _popup.PopupClient(outcome.Message, ent, target);
        if (target is { } targetCheck)
            _damage.ChangeDamage(targetCheck, outcome.Damage);
    }

    private void OnComponentInit(Entity<DickSmasherComponent> ent, ref ComponentInit args)
    {
        ent.Comp.Outcomes.Sort(new DickSmasherOutcome.DickSmasherOutcomeComparer());
    }

    private void OnUseInHand(Entity<DickSmasherComponent> ent, ref UseInHandEvent args)
    {
        TrySmash(ent, args.User);
        ent.Comp.TimesUsed++;
        Dirty(ent);

    }

    private DickSmasherOutcome? GetCurrentOutcome(Entity<DickSmasherComponent> ent)
    {
        DickSmasherOutcome? outcome = null;
        foreach (var choice in ent.Comp.Outcomes)
        {
            if (choice.TimesRequired > ent.Comp.TimesUsed)
                return outcome;

            outcome = choice;
        }
        return outcome;
    }
}
