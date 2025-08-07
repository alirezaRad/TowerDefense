using Service;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private ObjectPool objectPool;

    private void Awake()
    {
        ServiceLocator.Clear();
        
        //init services
        if (eventManager)
            ServiceLocator.Register(eventManager);
        else
            Debug.LogError("No event manager found.");
        
        if(objectPool)
            ServiceLocator.Register(objectPool);
        else
            Debug.LogError("No event manager found.");
    }
}