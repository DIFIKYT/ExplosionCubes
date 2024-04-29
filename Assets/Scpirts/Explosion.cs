using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public void Explode(Vector3 explosionPosition, List<Cube> spawnedCubes)
    {
        foreach (Cube cube in spawnedCubes)
        {
            if (cube.TryGetComponent(out Rigidbody rigidBody))
                rigidBody.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
        }
    }
}