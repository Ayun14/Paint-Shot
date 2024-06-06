using UnityEngine;
using UnityEngine.UI;

public class PlayerUIBarController : UIBarController
{
    [SerializeField] private Slider _PaintSlider;
    [SerializeField] private Gun _gun;

    protected override void Start()
    {
        _gun.OnPaintChange += HandlePaintChange;
    }

    private void OnDestroy()
    {
        _gun.OnPaintChange -= HandlePaintChange;
    }

    private void HandlePaintChange(float paintValue)
    {
        _PaintSlider.value = paintValue;
    }
}
