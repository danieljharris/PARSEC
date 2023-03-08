public class HighlightSoftware : MenuHighlighting
{
    public Software software;
    public override void addFilter() => nodeList.addFilter(software, presenter.IsPresenter);
    public override void removeFilter() => nodeList.removeFilter(software, presenter.IsPresenter);
}