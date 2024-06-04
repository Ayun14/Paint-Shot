using System;
using System.Collections.Generic;

public class EnemyStateMachine<T> where T : Enum
{
    public EnemyState<T> CurrentState { get; private set; }
    public Dictionary<T, EnemyState<T>> StateDictionary = new Dictionary<T, EnemyState<T>>();

    public Enemy enemyBase;

    public void Initialize(T state, Enemy enemy)
    {
        enemyBase = enemy;
        CurrentState = StateDictionary[state];
        CurrentState.Enter();
    }

    public void ChangeState(T newState)
    {
        CurrentState.Eixt();
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(T enemy, EnemyState<T> state)
    {
        StateDictionary.Add(enemy, state);
    }
}
