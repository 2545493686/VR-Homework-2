using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawn : MonoBehaviour {

    public Material[] materials;
    public bool IsProducing = false;
    public Cube cubePrefab;
    public float spawnTime = 0.3f;

    List<Cube> cubes;
    Vector3 lastPoint;
    float m_time = 0;

    void Awake()
    {
        cubes = new List<Cube>();
    }
    
    public void ClearAllCube()
    {
        foreach (var item in cubes)
        {
            if (item)
            {
                Destroy(item.gameObject);
            }
        }
        cubes.Clear();
    }

    // Update is called once per frame
    void Update () {

        if (IsProducing)
        {
            if (m_time > spawnTime)
            {
                m_time -= spawnTime;

                var cube = Instantiate(cubePrefab);

                cube.GetComponent<MeshRenderer>().material = GetRandomMaterials();

                SetCubePosition(cube);
                SetCubeRotation(cube);

                cubes.Add(cube);
            }
            else
            {
                m_time += Time.deltaTime;
            }
        }

	}

    private void SetCubeRotation(Cube cube)
    {
        cube.transform.rotation = transform.rotation;
    }

    private void SetCubePosition(Cube cube)
    {
        var random = (Vector3)Random.insideUnitCircle;
        while ((lastPoint - random).magnitude < 0.3f)
        {
            random = (Vector3)Random.insideUnitCircle;
        }
        lastPoint = random;

        cube.transform.position = transform.position + random;
    }

    private Material GetRandomMaterials()
    {
        return materials[UnityEngine.Random.Range(0, materials.Length)];
    }
}
