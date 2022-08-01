using Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Songs
{
    [RequireComponent(typeof(Image))]
    public class SongSelector : MonoBehaviour
    {
        [SerializeField] private SoundEffects _soundEffects;
        [SerializeField] private SongButton _songButton;
        [SerializeField] private AudioClip _audioClip;
        
        private Image _image;

        private void Awake()
        {
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
            HighlightImage();
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

        private void HighlightImage()
        {
            Color imageColor = _image.color;
            imageColor.a = 1f;
            _image.color = imageColor;
        }
    }
}