using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Songs
{
    [RequireComponent(typeof(ImageColorChanger))]
    public class SongButton : MonoBehaviour
    {
        [SerializeField] private Button[] _songButtons;

        public UnityEvent OnButtonClick;

        private ImageColorChanger _colorChanger;

        private void Awake()
        {
            _colorChanger = GetComponent<ImageColorChanger>();
        }

        private void Start()
        {
            HideNotActiveButtons();
        }

        public void OnMouseDown()
        {
            OnButtonClick?.Invoke();
            HideNotActiveButtons();
        }

        private void HideNotActiveButtons()
        {
            foreach (var songButton in _songButtons)
            {
                if (songButton.TryGetComponent(out Image image))
                {
                    _colorChanger.TurnOffAlpha(image);
                }
            }
        }
    }
}