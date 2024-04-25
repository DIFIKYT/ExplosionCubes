using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> MouseButtonPressed;

    private void OnMouseUpAsButton()
    {
        MouseButtonPressed?.Invoke(gameObject.GetComponent<Cube>());
    }
}