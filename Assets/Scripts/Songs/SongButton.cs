using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Songs
{
    public class SongButton : MonoBehaviour
    {
        [SerializeField] private Button[] _songButtons;

        public UnityEvent OnButtonClick;

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
                    Color imageColor = image.color;
                    imageColor.a = 0.25f;
                    image.color = imageColor;
                }
            }
        }
    }
}