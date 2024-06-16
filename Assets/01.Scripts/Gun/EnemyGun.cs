using UnityEngine;

public class EnemyGun : Gun
{
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
