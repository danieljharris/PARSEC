public class HighlightAttackType : MenuHighlighting
{
    public AttackType attackType;
    public override void addFilter()
    {
        foreach (NodeList nodeList in nodeLists)
        {
            nodeList.addFilter(attackType, presenter.IsPresenter);
        }
    }
    public override void removeFilter()
    {
        foreach (NodeList nodeList in nodeLists)
        {
            nodeList.removeFilter(attackType, presenter.IsPresenter);
        }
    }
}