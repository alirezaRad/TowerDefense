using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Service;
using Enums;
using Sservice;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _serviceBehaviours;

    private void Awake()
    {
        var services = InitServices();
        LoadServices(services);
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        ServiceLocator.Get<AudioManager>().PlayMusic(AudioClipType.Music);
    }

    private void LoadServices(List<IService> services)
    {
        foreach (var service in services)
        {
            service.Load();
        }
    }

    private List<IService> InitServices()
    {
        var services = new List<IService>();

        foreach (var behaviour in _serviceBehaviours)
        {
            if (behaviour is IService service)
            {
                services.Add(service);
                ServiceLocator.Register(behaviour.GetType(), service);
            }
            else
            {
                Debug.LogError($"{behaviour.name} does not implement IService.");
            }
        }

        return services;
    }
}