public class HighlightHardware : MenuHighlighting
{
    public Hardware hardware;
    public override void addFilter()
    {
        foreach (NodeList nodeList in nodeLists)
        {
            nodeList.addFilter(hardware, presenter.IsPresenter);
        }
    }
    public override void removeFilter()
    {
        foreach (NodeList nodeList in nodeLists)
        {
            nodeList.removeFilter(hardware, presenter.IsPresenter);
        }
    }
}