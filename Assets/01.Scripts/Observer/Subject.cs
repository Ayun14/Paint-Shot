using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private readonly List<Observer> _observers = new List<Observer>();

    protected void Attach(Observer observer) // 붙인다
    {
        _observers.Add(observer);
    }

    protected void Detach(Observer observer) // 뗸다
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers() // 알린다
    {
        foreach (Observer observer in _observers)
        {
            observer.Notify(this);
        }
    }
}
