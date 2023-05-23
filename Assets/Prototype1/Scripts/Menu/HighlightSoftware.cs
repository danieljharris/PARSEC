public class HighlightSoftware : MenuHighlighting
{
    public Software software;
    public override void addFilter()
    {
        foreach (NodeList nodeList in nodeLists)
        {
            nodeList.addFilter(software, presenter.IsPresenter);
        }
    }
    public override void removeFilter()
    {
        foreach (NodeList nodeList in nodeLists)
        {
            nodeList.removeFilter(software, presenter.IsPresenter);
        }
    }
}