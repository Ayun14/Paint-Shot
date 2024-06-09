using System.Collections.Generic;
using UnityEngine;

public enum AgentColor
{
    Red, Orange, Yellow, Green, Blue, Mint, Pink, Purple
}

public class AgentManager : Singleton<AgentManager>
{
    [SerializeField] private List<Transform> _enemyList = new List<Transform>();
    [SerializeField] private List<Material> _colorMatList = new List<Material>();

    private AgentColor _agentColor;
    public AgentColor AgentColor => _agentColor;

    public void ChangePlayerColor(AgentColor newColor)
    {
        if (newColor == _agentColor) return;
        _agentColor = newColor;
    }

    public Material GetAgentMat()
    {
        Debug.Log(_agentColor);
        foreach(var mat  in _colorMatList)
        {
            if (mat.name == $"{_agentColor}ParticleMat")
                return mat;
        }

        return null;
    }

    private void EnemySpawn()
    {
        foreach (var enemy  in _enemyList)
        {
            if (enemy.name == $"Enemy_{_agentColor}") continue;
            // 나머지 중에 3명 소환
        }
    }
}
