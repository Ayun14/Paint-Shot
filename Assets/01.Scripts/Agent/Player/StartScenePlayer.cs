using UnityEngine;

public class StartScenePlayer : MonoBehaviour
{
    [SerializeField] private Material[] _colorMats;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    public void ChangePlayerMat(AgentColor color)
    {
        foreach (var mat in _colorMats)
        {
            if (mat.name == color.ToString())
            {
                _skinnedMeshRenderer.material = mat;
                break;
            }
        }
    }
}