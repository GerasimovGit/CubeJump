using Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Songs
{
    [RequireComponent(typeof(ImageColorChanger), typeof(Image))]
    public class SongSelector : MonoBehaviour
    {
        [SerializeField] private PlaySoundEffects _soundEffects;
        [SerializeField] private SongButton _songButton;
        [SerializeField] private AudioClip _audioClip;

        private ImageColorChanger _colorChanger;
        private Image _image;

        private void Awake()
        {
            _colorChanger = GetComponent<ImageColorChanger>();
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            if (_soundEffects.TryGetSong(_audioClip, out var song))
            {
                _image.sprite = song.Image.sprite;
            }
        }

        public void Play()
        {
            _soundEffects.StopSong();
            _soundEffects.PlaySelectedSong(_audioClip);
            _songButton.OnMouseDown();
            _colorChanger.TurnOnAlpha(_image);
        }
    }
}