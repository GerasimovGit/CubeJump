using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class TouchUIChecker : MonoBehaviour
    {
        public bool IsTouchingUi { get; private set; }

        private void Update()
        {
            IsTouchingUi = CheckPointerOverUIObject();
        }

        private bool CheckPointerOverUIObject()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return true;

            if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began) return false;

            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) return true;
            Debug.Log(IsTouchingUi);
            return false;
        }
    }
}