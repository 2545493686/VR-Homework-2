using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawn : MonoBehaviour {

    public TouchToPlane cubePrefab;
    public float spawnTime = 0.3f;

    bool m_IsProducing = true;
    float m_time = 0;


    // Update is called once per frame
    void Update () {

        if (m_IsProducing)
        {
            if (m_time > spawnTime)
            {
                m_time -= spawnTime;
                var cube = Instantiate(cubePrefab);
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
