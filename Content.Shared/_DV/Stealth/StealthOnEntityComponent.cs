using Content.Shared.Whitelist;

namespace Content.Shared._DV.Stealth;

[RegisterComponent]
public sealed partial class StealthOnEntityComponent : Component
{
    /// <summary>
    /// Which entities will trigger stealth.
    /// </summary>
    [DataField(required:true)]
    public EntityWhitelist Entities;

    /// <summary>
    /// The stealth visiblity that is set when colliding with a whitelisted entity.
    /// Goes from -1.0 (invisible) to 1.0 (fully visible)
    /// </summary>
    [DataField]
    public float Visibility = 0.5f;
}
