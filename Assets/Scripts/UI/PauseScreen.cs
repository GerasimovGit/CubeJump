using UnityEngine.Events;

namespace UI
{
    public class PauseScreen : Screen
    {
        public event UnityAction PauseButtonClick;

        protected override void OnButtonClick()
        {
            PauseButtonClick?.Invoke();
        }

        public override void Open()
        {
            CanvasGroup.alpha = 1f;
            CanvasGroup.blocksRaycasts = true;
            Button.interactable = false;
        }

        public override void Close()
        {
            CanvasGroup.alpha = 0f;
            CanvasGroup.blocksRaycasts = false;
            Button.interactable = true;
        }
    }
}