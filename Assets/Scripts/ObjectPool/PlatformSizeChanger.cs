using System;
using UnityEngine;

namespace ObjectPool
{
    public class PlatformSizeChanger : MonoBehaviour
    {
        [SerializeField] private float _minPlatformWidth;
        [SerializeField] private float _changeSizeValue;

        public void ChangeSize(GameObject template,GameObject platform,out Vector3 newPlatformSize)
        {
            newPlatformSize = template.transform.localScale;
            float currentPositionY = platform.transform.position.y;
            float maxPlatformWidth = template.transform.lossyScale.x;
            float sizeAccordToPositionY = newPlatformSize.x - currentPositionY / _changeSizeValue;
            newPlatformSize.x = Mathf.Clamp(sizeAccordToPositionY, _minPlatformWidth, maxPlatformWidth);
        }
    }
    
}