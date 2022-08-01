using UnityEngine.Events;

namespace UI
{
    public class GameHud : Screen
    {
        public event UnityAction GameStartButtonClick;

        protected override void OnButtonClick()
        {
            GameStartButtonClick?.Invoke();
        }

        public override void Open()
        {
            gameObject.SetActive(true);
            Button.interactable = true;
        }

        public override void Close()
        {
            gameObject.SetActive(false);
            Button.interactable = true;
        }
    }
}