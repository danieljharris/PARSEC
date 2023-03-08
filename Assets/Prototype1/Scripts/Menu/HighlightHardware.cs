public class HighlightHardware : MenuHighlighting
{
    public Hardware hardware;
    public override void addFilter() => nodeList.addFilter(hardware, presenter.IsPresenter);
    public override void removeFilter() => nodeList.removeFilter(hardware, presenter.IsPresenter);
}