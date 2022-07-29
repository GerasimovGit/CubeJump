using UnityEngine;

public class ImageColorChanger : MonoBehaviour
{
    private Color _imageColor;
    
    private readonly float _maxAlpha = 1f;
    private readonly float _minAlpha = 0.25f;

    public void TurnOffAlpha(UnityEngine.UI.Image image)
    {
        _imageColor = image.color;
        _imageColor.a = _minAlpha;
        image.color = _imageColor;
    }

    public void TurnOnAlpha(UnityEngine.UI.Image image)
    {
        _imageColor = image.color;
        _imageColor.a = _maxAlpha;
        image.color = _imageColor;
    }
}