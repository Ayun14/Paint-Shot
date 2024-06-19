using UnityEngine;

public interface IMovement
{
    public Vector3 Velocity { get; }
    public void SetMovement(Vector3 movement, bool isRotation = true);
}
