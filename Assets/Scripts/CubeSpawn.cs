using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawn : MonoBehaviour {

    public Material[] materials;
    public bool IsProducing = false;
    public TouchToPlane cubePrefab;
    public float spawnTime = 0.3f;

    float m_time = 0;

    Vector3 lastPoint;

    // Update is called once per frame
    void Update () {

        if (IsProducing)
        {
            if (m_time > spawnTime)
            {
                m_time -= spawnTime;
                var cube = Instantiate(cubePrefab);
                cube.GetComponent<MeshRenderer>().material = materials[UnityEngine.Random.Range(0, materials.Length)];
                var random = (Vector3)Random.insideUnitCircle;
                while ((lastPoint - random).magnitude < 0.3f)
                {
                    random = (Vector3)Random.insideUnitCircle;
                }
                lastPoint = random;
                cube.transform.position = transform.position + random;
                cube.transform.rotation = transform.rotation;
            }
            else
            {
                m_time += Time.deltaTime;
            }
        }

	}
}
