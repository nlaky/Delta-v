using Robust.Shared.GameStates;

namespace Content.Shared._DV.DickSmasher;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class DickSmasherComponent : Component
{
    [DataField(required: true)]
    public List<DickSmasherOutcome> Outcomes;

    [DataField, AutoNetworkedField]
    public int TimesUsed = 0;
}
