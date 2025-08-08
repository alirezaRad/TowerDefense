
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI priceText;
        
        public void Init(Sprite sprite,int price)
        {
            image.sprite = sprite;
            priceText.text = price.ToString();
        }
    }
}