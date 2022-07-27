using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public class PlaySoundEffects : MonoBehaviour
    {
        private const float MAXPitch = 1f;
        private const float MINPitch = 0.5f;

        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _sounds;
        [SerializeField] private float _speed;

        public void Play(string id)
        {
            foreach (var audioData in _sounds)
            {
                if (audioData.Id != id) continue;

                _source.PlayOneShot(audioData.Clip);
                break;
            }
        }

        public void PlayFromFade()
        {
            StartCoroutine(FadeOutPitch(MAXPitch));
        }

        public void PlaySelectedSong(AudioClip audioClip)
        {
            foreach (var audioData in _sounds)
            {
                if (audioData.Clip != audioClip) continue;

                _source.clip = audioData.Clip;
                _source.Play();
                break;
            }
        }

        public void TurnDownPitch()
        {
            _source.pitch = MINPitch;
        }

        public void StopSong()
        {
            _source.Stop();
        }

        private IEnumerator FadeOutPitch(float target)
        {
            while (_source.pitch != target)
            {
                _source.pitch = Mathf.MoveTowards(_source.pitch, target, _speed * Time.deltaTime);
                yield return null;
            }
        }

        public bool TryGetSong(AudioClip audioClip, out AudioData Song)
        {
            bool isExist = false;
            Song = null;

            foreach (var audioData in _sounds)
            {
                if (audioClip != audioData.Clip)
                {
                    continue;
                }

                Song = audioData;
                isExist = true;
                break;
            }

            return isExist;
        }

        [Serializable]
        public class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;
            [SerializeField] private Image _image;

            public string Id => _id;
            public AudioClip Clip => _clip;
            public Image Image => _image;
        }
    }
}