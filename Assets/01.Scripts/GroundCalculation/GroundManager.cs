using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundManager : Singleton<GroundManager>
{
    [SerializeField] private LayerMask _node;

    [HideInInspector] public Dictionary<string, string> nameList; // 랭킹에 표시될 이름, 색깔
    private Dictionary<string, float> _rankingDictionary;
    private List<GroundNode> _groundNodeList;

    private void Awake()
    {
        _rankingDictionary = new Dictionary<string, float>();
        nameList = new Dictionary<string, string>();
        _groundNodeList = new List<GroundNode>();
    }

    public void ResetGroundManager()
    {
        foreach (GroundNode node in _groundNodeList)
        {
            node.nodeId = "";
            _rankingDictionary.Clear();
            nameList.Clear();
        }
    }

    public void AddNodeList(GroundNode node)
    {
        _groundNodeList.Add(node);
    }

    public void AddIdList(string id, string name, string color)
    {
        _rankingDictionary.Add(id, 0);
        nameList.Add(name, color);
    }

    // 누구 땅인지 설정
    public void GroundPainted(Vector3 pos, float radius, string id)
    {
        RaycastHit[] result =
            Physics.SphereCastAll(pos, radius, Vector3.up, 0, _node);

        foreach (RaycastHit hit in result)
        {
            if (hit.transform.TryGetComponent(out GroundNode node))
                node.nodeId = id;
        }
    }

    // 땅 차지율을 이름, 차지율로 넘겨줌
    public Dictionary<string, float> GroundRanking()
    {
        var keys = _rankingDictionary.Keys.ToList();

        foreach (var key in keys)
        {
            float persent = GroundResult(key);
            _rankingDictionary[key] = persent;
        }

        // 땅 점유율이 많은 순서로 정렬
        _rankingDictionary = _rankingDictionary
            .OrderByDescending(item => item.Value)
            .ToDictionary(x => x.Key, x => x.Value);

        return _rankingDictionary;
    }

    // Id에 따른 땅 차지 퍼센트
    private float GroundResult(string id)
    {
        float sum = 0;

        foreach (GroundNode node in _groundNodeList)
            if (node.nodeId == id) ++sum;

        return (sum / _groundNodeList.Count) * 100f;
    }
}
