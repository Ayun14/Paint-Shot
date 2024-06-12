using System;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Action<float> OnPaintChange;

    [SerializeField] protected Transform _shootTrm;
    [SerializeField] protected ParticleSystem _shootParticle;

    protected bool _isPainting = false;

    private float _usePaintAmount = 2; // 쏘면 사용되는 페인트 양
    private float _fillPaintAmount = 2; // 안쏘면 채워지는 페인트 양
    private float _currentPaintAmount = 0;
    private float _paintMax = 100;

    private float _currentPaintingTime = 0;
    private float _paintTime = 0.3f; // 페인트 닳는 텀

    private float _currentFillingTime = 0;
    private float _fillTime = 0.5f; // 페인트 채워지는 텀

    protected virtual void Start()
    {
        _currentPaintAmount = _paintMax;
        _shootParticle.transform.position = _shootTrm.transform.position;
    }

    protected virtual void Update()
    {
        if (_isPainting)
        {
            _currentPaintingTime += Time.deltaTime;
            if (_currentPaintingTime >= _paintTime)
            {
                _currentPaintingTime = 0;
                SetPaintAmount(_usePaintAmount);
            }

            if (_currentPaintAmount <= 0)
                StopPaintParticle();
        }
        else
        {
            _currentFillingTime += Time.deltaTime;
            if (_currentFillingTime >= _fillTime)
            {
                _currentFillingTime = 0;
                SetPaintAmount(-_fillPaintAmount);
            }
        }
    }

    public void PlayPaintParticle()
    {
        if (_currentPaintAmount <= 0) return;

        _isPainting = true;
        _shootParticle.Play();
    }

    public void StopPaintParticle()
    {
        _isPainting = false;
        _shootParticle.Stop();
    }

    private void SetPaintAmount(float paintAmount)
    {
        _currentPaintAmount -= paintAmount;
        float paintValue = _currentPaintAmount / _paintMax;
        OnPaintChange?.Invoke(paintValue);

        if (_currentPaintAmount > _paintMax)
            _currentPaintAmount = _paintMax;
    }

    public bool IsCanPaint()
    {
        return _currentPaintAmount > _paintMax / 3;
    }

    public bool IsNonePaint()
    {
        return _currentPaintAmount <= 0;
    }
}
