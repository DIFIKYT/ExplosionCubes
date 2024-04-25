using System;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Color[] _cubeColors;

    private Cube[] _cubes;
    private int _numberChanceReduction = 2;
    private int _numberSizeReductions = 2;
    private int _minCountCubesSpawn = 2;
    private int _maxCountCubesSpawn = 6;
    private int _maxSplitChance = 100;
    private int _currentSplitChance;

    private void Awake()
    {
        _cubes = FindObjectsOfType<Cube>();
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

    private void SpawnCube(Cube cube)
    {
        Component[] components = cube.GetComponents<Component>();
        Renderer renderer;

        Cube newCube = Instantiate(_cubePrefab, cube.transform.position, cube.transform.rotation);
        newCube.transform.localScale = cube.transform.localScale / _numberSizeReductions;

        renderer = newCube.GetComponent<Renderer>();
        renderer.material.color = SelectColor();

        AddComponents(components, newCube);

        Array.Resize(ref _cubes, _cubes.Length + 1);
        _cubes[_cubes.Length - 1] = newCube;

        newCube.MouseButtonPressed += SplitCube;
    }

    private void SplitCube(Cube cube)
    {
        if (UnityEngine.Random.Range(0, _maxSplitChance) <= _currentSplitChance)
        {
            int countCubesSpawn = UnityEngine.Random.Range(_minCountCubesSpawn, _maxCountCubesSpawn);

            for (int i = 0; i < countCubesSpawn; i++)
                SpawnCube(cube);

            _currentSplitChance /= _numberChanceReduction;
        }

        Destroy(cube.gameObject);
    }

    private void AddComponents(Component[] components, Cube cube)
    {
        foreach (Component component in components)
        {
            if (component is not Transform && component is not MeshRenderer && component is not MeshFilter && component is not Cube)
                cube.gameObject.AddComponent(component.GetType());
        }
    }

    private Color SelectColor()
    {
        Color color;
        color = _cubeColors[UnityEngine.Random.Range(0, _cubeColors.Length)];
        return color;
    }
}