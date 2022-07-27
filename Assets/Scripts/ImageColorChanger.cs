using UnityEngine;
using UnityEngine.UI;

public class ImageColorChanger : MonoBehaviour
{
    private Color _imageColor;
    
    private readonly float _maxAlpha = 1f;
    private readonly float _minAlpha = 0.25f;

    public void TurnOffAlpha(Image image)
    {
        _imageColor = image.color;
        _imageColor.a = _minAlpha;
        image.color = _imageColor;
    }

    public void TurnOnAlpha(Image image)
    {
        _imageColor = image.color;
        _imageColor.a = _maxAlpha;
        image.color = _imageColor;
    }
}