using System;

namespace UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBar : MonoBehaviour
    {
        private Image _fillImage;
        private int _maxHealth;

        private void Awake()
        {
            _fillImage = transform.GetChild(0).GetComponent<Image>();
        }

        public void Init(int maxHealth)
        {
            _maxHealth = maxHealth;
            gameObject.SetActive(false);
        }

        public void SetHealth(float health)
        {
            if(gameObject.activeSelf == false)
                gameObject.SetActive(true);
            
            health /= _maxHealth;
            _fillImage.fillAmount = Mathf.Clamp01(health);
        }
    }

}