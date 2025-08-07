using Enums;
using Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuManger : MonoBehaviour
    {
        public void PlayGame()
        {
            ServiceLocator.Get<AudioManager>().PlaySfx(AudioClipType.ButtonClick);
            SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
            ServiceLocator.Get<EventManager>().Raise(GameEventType.GameStart);
            SceneManager.UnloadSceneAsync("Menu");
        }
    }
}