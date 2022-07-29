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
            TrySetImage();
        }

        public void Play()
        {
            _soundEffects.StopCurrentSong();
            TryPlaySelectedSong();
            _songButton.OnMouseDown();
            _colorChanger.TurnOnAlpha(_image);
        }

        private void TryPlaySelectedSong()
        {
            if (_soundEffects.TryGetSong(_audioClip, out var song))
            {
                _soundEffects.PlaySelectedSong(song.Clip);
            }
        }

        private void TrySetImage()
        {
            if (_soundEffects.TryGetSong(_audioClip, out var song))
            {
                _image.sprite = song.Image.sprite;
            }
        }
    }
}