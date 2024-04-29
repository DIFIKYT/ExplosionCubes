using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private int _currentSplitplitChance = 100;
    private int _numberChanceReduction = 2;

    public event Action<Cube> MouseButtonPressed;

    public int CurrentSplitChance => _currentSplitplitChance;

    private void OnEnable()
    {
        _cubeSpawner.CubeSplit += SplitChance;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeSplit -= SplitChance;
    }

    private void OnMouseUpAsButton()
    {
        MouseButtonPressed?.Invoke(this);
    }

    private void SplitChance()
    {
        _currentSplitplitChance /= _numberChanceReduction;
    }
}