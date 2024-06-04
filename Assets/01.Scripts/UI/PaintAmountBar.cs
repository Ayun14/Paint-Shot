using UnityEngine;
using UnityEngine.UI;

public class PaintAmountBar : MonoBehaviour
{
    //[SerializeField] private Player _owner;
    [SerializeField] private Image _fillImage;

    private void HandlePaintEvent()
    {
        //float fillAmount = _owner.HealthCompo.GetNormalizedHealth();
        //_fillImage.fillAmount = fillAmount;
    }

    private void LateUpdate() // 카메라 바라보기
    {
        Transform mainCamTrm = Camera.main.transform;
        Vector3 lookDirection = (transform.position - mainCamTrm.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
