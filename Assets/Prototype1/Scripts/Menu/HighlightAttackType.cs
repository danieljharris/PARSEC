public class HighlightAttackType : MenuHighlighting
{
    public AttackType attackType;
    public override void addFilter() => nodeList.addFilter(attackType, presenter.IsPresenter);
    public override void removeFilter() => nodeList.removeFilter(attackType, presenter.IsPresenter);
}