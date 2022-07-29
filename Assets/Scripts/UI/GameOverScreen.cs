using UnityEngine.Events;

namespace UI
{
    public class GameOverScreen : Screen
    {
        public event UnityAction RestartButtonClick;

        protected override void OnButtonClick()
        {
            RestartButtonClick?.Invoke();
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