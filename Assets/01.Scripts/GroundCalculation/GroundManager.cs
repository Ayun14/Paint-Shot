using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : Singleton<GroundManager>
{
    private List<GroundNodeParent> _groundNodeParentList = new List<GroundNodeParent>();

    private RaycastHit[] result;

    public void AddList(GroundNodeParent parent)
    {
        _groundNodeParentList.Add(parent);
    }

    // 누구 땅인지 설정
    public void GroundPainted(Vector3 pos, float radius, string id)
    {
        Physics.SphereCastNonAlloc(pos, radius, Vector3.zero, result);

        foreach (RaycastHit hit in result)
        {
            if (hit.transform.TryGetComponent(out GroundNode node))
            {
                node.nodeId = id;
            }
        }
    }
}
