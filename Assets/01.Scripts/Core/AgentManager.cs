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
    [SerializeField] private int _enemySpawnCnt;

    private AgentColor _agentColor;
    public AgentColor AgentColor => _agentColor;

    private Vector3 _agentSpawnPos;
    public Vector3 AgentSpawnPos => _agentSpawnPos;

    public void ChangePlayerColor(AgentColor newColor)
    {
        if (newColor == _agentColor) return;
        _agentColor = newColor;
    }

    public Material GetAgentMat()
    {
        foreach (var mat in _colorMatList)
        {
            if (mat.name == $"{_agentColor}ParticleMat")
                return mat;
        }

        return null;
    }

    public void AgentSpawn()
    {
        for (int i = 0; i <= _enemySpawnCnt; ++i)
        {
            float angle = ((Mathf.PI * 2) / (_enemySpawnCnt + 1)) * i;

            float radius = 12f;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            Vector3 spawnPos = new Vector3(x, 0, z);

            if (i == 0) // Player
            {
                _agentSpawnPos = spawnPos;
                continue;
            }

            Quaternion quaternion = Quaternion.LookRotation(-spawnPos);
            Transform obj = Instantiate(PickRandomEnemy(), spawnPos,
                quaternion, transform);

            if (obj.TryGetComponent(out Enemy enemy))
                enemy.spawnPos = spawnPos;
        }
    }

    private Transform PickRandomEnemy()
    {
        Transform enemy = null;

        while (true)
        {
            int rand = Random.Range(0, _enemyList.Count);
            if (_enemyList[rand].name == $"Enemy_{_agentColor}") continue;

            bool isSame = false;
            for (int i = 0; i < transform.childCount; ++i)
            {
                if ($"{_enemyList[rand].name}(Clone)" == transform.GetChild(i).name)
                {
                    isSame = true;
                    break;
                }
            }

            if (isSame) continue;

            enemy = _enemyList[rand];
            break;
        }

        return enemy;
    }

    public void GameStart()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).TryGetComponent(out Enemy enemy))
                enemy.GameStart();
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).TryGetComponent(out Enemy enemy))
                enemy.GameOver();
        }
    }
}
