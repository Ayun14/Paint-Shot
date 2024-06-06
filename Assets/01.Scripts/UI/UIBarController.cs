using UnityEngine;
using UnityEngine.UI;

public abstract class UIBarController : MonoBehaviour
{
    [SerializeField] private Slider _HpSlider;

    private Health _health;

    protected virtual void Start()
    {
        _health = GetComponentInParent<Health>();
    }

    public void HandleHpChange()
    {
        float value = (float)_health.CurrentHealth / _health.MaxHealth;
        _HpSlider.value = value;
    }

    private void LateUpdate() // 카메라 바라보기
    {
        Transform mainCamTrm = Camera.main.transform;
        Vector3 lookDirection = (transform.position - mainCamTrm.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
