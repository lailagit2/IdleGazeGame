public class WalkSpeedSlider : Slider
{
    void Start()
    {
        init(getFollowPoint().walkSpeed);
    }

    public override void SetSliderValue()
    {
        float normalized = 0.5f + sliderObject.transform.localPosition.x;

        float sensPercent = (upperPercent - lowerPercent) * normalized + lowerPercent;
        getFollowPoint().walkSpeed = ogSens * sensPercent;
    }
}
