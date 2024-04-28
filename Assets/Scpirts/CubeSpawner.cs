using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Explosion _explosionManager;
    [SerializeField] private List<Color> _cubeColors;
    [SerializeField] private List<Cube> _cubes;

    public event Action<Cube> CubeDestroyed;

    private List<Cube> _spawnedCubes = new List<Cube>();
    private int _numberChanceReduction = 2;
    private int _numberSizeReductions = 2;
    private int _minCountCubesSpawn = 2;
    private int _maxCountCubesSpawn = 6;
    private int _maxSplitChance = 100;
    private int _currentSplitChance;

    private void Awake()
    {
        _currentSplitChance = _maxSplitChance;
    }

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
            cube.MouseButtonPressed += SplitCube;
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
            cube.MouseButtonPressed -= SplitCube;
    }

    private Cube SpawnCube(Cube originalCube)
    {
        Cube newCube = Instantiate(originalCube, originalCube.transform.position, originalCube.transform.rotation);

        newCube.transform.localScale = originalCube.transform.localScale / _numberSizeReductions;
        newCube.GetComponent<Renderer>().material.color = SelectColor();

        AddCubeInList(newCube);

        return newCube;
    }

    private void SplitCube(Cube cube)
    {
        if (UnityEngine.Random.Range(0, _maxSplitChance) <= _currentSplitChance)
        {
            int countCubesSpawn = UnityEngine.Random.Range(_minCountCubesSpawn, _maxCountCubesSpawn);

            for (int i = 0; i < countCubesSpawn; i++)
                _spawnedCubes.Add(SpawnCube(cube));

            _currentSplitChance /= _numberChanceReduction;
        }

        _explosionManager.Explode(cube.transform.position, _spawnedCubes);
        CubeDestroyed?.Invoke(cube);
        _cubes.Remove(cube);
        Destroy(cube.gameObject);
        _spawnedCubes.Clear();
    }

    private Color SelectColor()
    {
        return _cubeColors[UnityEngine.Random.Range(0, _cubeColors.Count)];
    }

    private void AddCubeInList(Cube cube)
    {
        _cubes.Add(cube);
        cube.MouseButtonPressed += SplitCube;
    }
}