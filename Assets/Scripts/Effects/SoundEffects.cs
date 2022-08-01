using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public class SoundEffects : MonoBehaviour
    {
        private const float MAXPitch = 1f;
        private const float MINPitch = 0.5f;

        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _sounds;
        [SerializeField] private float _pitchChangeSpeed;

        public void Play(string id)
        {
            foreach (var audioData in _sounds)
            {
                if (audioData.Id != id) continue;

                _source.PlayOneShot(audioData.Clip);
                break;
            }
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

        public bool TryGetSong(AudioClip audioClip, out AudioData song)
        {
            bool isExist = false;
            song = null;

            foreach (var audioData in _sounds)
            {
                if (audioClip != audioData.Clip)
                {
                    continue;
                }

                song = audioData;
                isExist = true;
                break;
            }

            return isExist;
        }

        public void StopCurrentSong()
        {
            _source.Stop();
        }

        public void TurnDownPitch()
        {
            _source.pitch = MINPitch;
        }

        public void PlayFromFade()
        {
            StartCoroutine(FadeOutPitch(MAXPitch));
        }

        private IEnumerator FadeOutPitch(float target)
        {
            while (_source.pitch != target)
            {
                _source.pitch = Mathf.MoveTowards(_source.pitch, target, _pitchChangeSpeed * Time.deltaTime);
                yield return null;
            }
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