using System;

namespace UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBar : MonoBehaviour
    {
        private Image _fillImage;

        private void Awake()
        {
            _fillImage = transform.GetChild(0).GetComponent<Image>();
        }

        public void SetHealth(float normalizedHealth)
        {
            _fillImage.fillAmount = Mathf.Clamp01(normalizedHealth);
        }
    }

}