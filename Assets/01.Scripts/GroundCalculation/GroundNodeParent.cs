using System.Collections.Generic;
using UnityEngine;

public class GroundNodeParent : MonoBehaviour
{
    [SerializeField] private Transform _node;

    private Vector3 _parentSize;
    private Vector3 _nodeSize = new Vector3(1f, 0.1f, 1f);

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _parentSize = _renderer.bounds.size;

        NodeSetting();
    }

    private void NodeSetting()
    {
        if (_node == null) return;

        Vector3 parentCenter = _renderer.bounds.center;

        int x = Mathf.FloorToInt(_parentSize.x / _nodeSize.x);
        int z = Mathf.FloorToInt(_parentSize.z / _nodeSize.z);

        Vector3 startPos = new Vector3(
            parentCenter.x + (x / 2f) - (_nodeSize.x / 2f),
            transform.position.y,
            parentCenter.z - (z / 2f) + (_nodeSize.z / 2f));

        for (int i = 0; i < z; ++i)
        {
            for (int j = 0; j < x; ++j)
            {
                Transform node = Instantiate(_node);

                Vector3 spawnPos = new Vector3(
                    startPos.x - (_nodeSize.x * j),
                    startPos.y,
                    startPos.z + (_nodeSize.z * i));

                node.position = spawnPos;
                node.parent = transform;
                node.rotation = Quaternion.identity;

                if (node.TryGetComponent(out GroundNode groundNode))
                    GroundManager.Instance.AddNodeList(groundNode);
            }
        }
    }
}
