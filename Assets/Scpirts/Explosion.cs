using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public void Explode(Vector3 explosionPosition, List<Cube> spawnedCubes)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            foreach (Cube cube in spawnedCubes)
            {
                if (hit == cube)
                    if (hit.TryGetComponent(out Rigidbody rigidBody))
                        rigidBody.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
            }
        }
    }
}