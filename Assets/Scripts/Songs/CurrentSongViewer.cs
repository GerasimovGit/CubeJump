using Effects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Songs
{
    public class CurrentSongViewer : MonoBehaviour
    {
        [SerializeField] private Image _spriteRenderer;
        [SerializeField] private TMP_Text _song;
        [SerializeField] private AudioSource _source;
        [SerializeField] private SoundEffects _soundEffects;

        private void Start()
        {
            ShowInfo();
        }

        public void ShowInfo()
        {
            if (_soundEffects.TryGetSong(_source.clip,out var song))
            {
                _spriteRenderer.sprite = song.Image.sprite;
                _song.text = song.Clip.name;
            }
        }
    }
}