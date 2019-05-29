using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawn : MonoBehaviour {

    public Material[] materials;
    public bool IsProducing = false;
    public TouchToPlane cubePrefab;
    public float spawnTime = 0.3f;

    float m_time = 0;

    // Update is called once per frame
    void Update () {

        if (IsProducing)
        {
            if (m_time > spawnTime)
            {
                m_time -= spawnTime;
                var cube = Instantiate(cubePrefab);
                cube.GetComponent<MeshRenderer>().material = materials[UnityEngine.Random.Range(0, materials.Length)];
                cube.transform.position = transform.position + (Vector3)Random.insideUnitCircle;
                cube.transform.rotation = transform.rotation;
            }
            else
            {
                m_time += Time.deltaTime;
            }
        }

	}
}
