using System;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Action<float> OnPaintChange;

    [SerializeField] protected Transform _shootTrm;
    [SerializeField] protected ParticleSystem _shootParticle;

    protected bool _isPainting = false;

    private float _usePaintAmount = 2; // 쏘면 사용되는 페인트 양
    private float _currentPaintAmount = 0;
    private float _paintMax = 100;

    private float _currentTime;
    private float _paintTime = 0.3f; // 페인트 닳는 시간

    protected virtual void Start()
    {
        _currentPaintAmount = _paintMax;
        _shootParticle.transform.position = _shootTrm.transform.position;
    }

    protected virtual void Update()
    {
        if (_isPainting)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _paintTime)
            {
                _currentTime = 0;
                SetPaintAmount(_usePaintAmount);
            }

            if (_currentPaintAmount <= 0)
                StopPaintParticle();
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

    public void SetPaintAmount(float paintAmount)
    {
        if (_currentPaintAmount <= 0)
        {
            _currentPaintAmount = 0;
            return;
        }

        _currentPaintAmount -= paintAmount;
        float paintValue = _currentPaintAmount / _paintMax;
        OnPaintChange?.Invoke(paintValue);
    }
}
