using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AgentColor
{
    Red, Orange, Yellow, Green, Blue, Mint, Pink, Purple
}

public class AgentManager : Singleton<AgentManager>
{
    [SerializeField] private List<Transform> _enemyList = new List<Transform>();
}
