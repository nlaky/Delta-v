using Content.Shared.Stealth;
using Content.Shared.Whitelist;
using Robust.Shared.Physics.Events;
using Robust.Shared.Physics.Systems;

namespace Content.Shared._DV.Stealth;

public sealed class StealthOnEntitySystem : EntitySystem
{
    [Dependency] private readonly SharedStealthSystem _stealth = default!;
    [Dependency] private readonly EntityWhitelistSystem _whitelist = default!;
    [Dependency] private readonly SharedPhysicsSystem _physics = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<StealthOnEntityComponent, StartCollideEvent>(OnStartCollide);
        SubscribeLocalEvent<StealthOnEntityComponent, EndCollideEvent>(OnEndCollide);
    }

    private void OnStartCollide(Entity<StealthOnEntityComponent> ent, ref StartCollideEvent args)
    {
        if (_whitelist.IsWhitelistFail(ent.Comp.Entities, args.OtherEntity))
            return;

        _stealth.SetEnabled(ent, true);
        _stealth.SetVisibility(ent, ent.Comp.Visibility);
    }

    private void OnEndCollide(Entity<StealthOnEntityComponent> ent, ref EndCollideEvent args)
    {
        if (_whitelist.IsWhitelistFail(ent.Comp.Entities, args.OtherEntity))
            return;

        foreach (var colliding in _physics.GetContactingEntities(ent))
        {
            if (_whitelist.IsWhitelistPass(ent.Comp.Entities, colliding)
                && colliding != args.OtherEntity) // entity not removed yet
                return;
        }

        _stealth.SetEnabled(ent, false);
        _stealth.SetVisibility(ent, 1.0f);
    }
}
