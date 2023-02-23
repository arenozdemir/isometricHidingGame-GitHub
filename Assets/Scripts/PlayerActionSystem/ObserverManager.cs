using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverManager : MonoBehaviour
{
    List<IObserver> observers = new List<IObserver>();
    private static ObserverManager instance;
    public static ObserverManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ObserverManager>();
            }
            return instance;
        }
    }
    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    protected void NotifyObservers(PlayerActionsEnum action)
    {
        observers.ForEach(observer => observer.OnNotify(action));
    }
}
