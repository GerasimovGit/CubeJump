using UnityEngine.Events;

namespace UI
{
    public class StartScreen : Screen
    {
        public event UnityAction PlayButtonClick;

        protected override void OnButtonClick()
        {
            PlayButtonClick?.Invoke();
        }

        public override void Open()
        {
            gameObject.SetActive(true);
            Button.interactable = true;
        }

        public override void Close()
        {
            gameObject.SetActive(false);
            Button.interactable = false;
        }
    }
}