using UnityEngine;

public class EnemyGun : Gun
{
    protected override void Start()
    {
        _paintMax = 80;
        base.Start();
    }

    public bool IsCanPaint()
    {
        int rand = Random.Range(2, 6);
        return _currentPaintAmount > _paintMax / rand;
    }

    public bool IsNonePaint()
    {
        int rand = Random.Range(2, 6);
        return _currentPaintAmount < _paintMax / rand;
    }
}
