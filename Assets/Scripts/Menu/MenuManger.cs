using System.Collections;
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
            StartCoroutine(LoadGameScene());
        }
        private IEnumerator LoadGameScene()
        {
            AsyncOperation loadGameLogic = SceneManager.LoadSceneAsync("GameLogicScene", LoadSceneMode.Additive);
            AsyncOperation loadGameUI = SceneManager.LoadSceneAsync("GameUIScene", LoadSceneMode.Additive);
            while (!loadGameLogic.isDone || !loadGameUI.isDone)
                yield return null;
            ServiceLocator.Get<EventManager>().Raise(GameEventType.GameStart);
            SceneManager.UnloadSceneAsync("Menu");
        }

    }
}