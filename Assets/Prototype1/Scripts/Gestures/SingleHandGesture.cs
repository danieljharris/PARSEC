public abstract class SingleHandGesture : Gesture
{
    protected bool LControllerActive;
    public abstract bool LeftButton();
    public abstract bool RightButton();
    public override bool Trigger() => LeftButton() ^ RightButton();
    public override void StartGesture() => LControllerActive = LeftButton();
}