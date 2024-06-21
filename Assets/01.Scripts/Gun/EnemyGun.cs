using UnityEngine;

public class EnemyGun : Gun
{
    private int rand;

    protected override void Start()
    {
        _paintMax = 80;
        rand = Random.Range(2, 6);

        base.Start();
    }

    public bool IsCanPaint()
    {
        return _currentPaintAmount > _paintMax / rand;
    }

    public bool IsNonePaint()
    {
        return _currentPaintAmount < _paintMax / rand;
    }
}
