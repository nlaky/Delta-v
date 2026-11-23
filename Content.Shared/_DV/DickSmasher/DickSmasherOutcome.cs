using Content.Shared.Damage;

namespace Content.Shared._DV.DickSmasher;

[DataDefinition]
public sealed partial class DickSmasherOutcome
{
    [DataField(required: true)]
    public DamageSpecifier Damage;

    [DataField]
    public string Message;

    [DataField]
    public int TimesRequired = 0;

    public sealed class DickSmasherOutcomeComparer : Comparer<DickSmasherOutcome>
    {
        public override int Compare(DickSmasherOutcome? x, DickSmasherOutcome? y)
        {
            if (x is null || y is null)
                return 0;

            return x.TimesRequired.CompareTo(y.TimesRequired);
        }
    }
}
