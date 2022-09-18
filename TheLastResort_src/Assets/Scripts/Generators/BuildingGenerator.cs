using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public MeshCombiner buildingMesh;
    public List<GameObject> Building;

    public List<GameObject> modules;
    public List<GameObject> decoratives;

    public int width;
    public int Depth;
    public int height;

    [SerializeField] float m_width;
    [SerializeField] float m_height;
    [SerializeField] bool m_backface;

#if UNITY_EDITOR
    public bool generateBuilding = false;
    public bool clear = false;
    private void OnValidate()
    {
        if(generateBuilding)
        {
            generate(modules);
            generate(decoratives);
            generateBuilding = false;
        }

        if(clear)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            clear = false;
        }
    }
#endif

    private void generate(List<GameObject> _l)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int p = Random.Range(0, _l.Count);
                GameObject temp =
                    Instantiate(_l[p],
                    new Vector3(j * m_width, i * m_height, 0),
                    Quaternion.Euler(-90, 180, _l[p].transform.localRotation.z), transform);

                if(m_backface)
                {
                    int q = Random.Range(0, _l.Count);
                    GameObject temp2 =
                        Instantiate(_l[q],
                        new Vector3(j * m_width, i * m_height, Depth * m_width),
                        Quaternion.Euler(-90, 0, _l[q].transform.localRotation.z), transform);
                }
            }

            for (int k = 0; k < Depth; k++)
            {
                int p = Random.Range(0, _l.Count);
                GameObject temp =
                    Instantiate(_l[p],
                    new Vector3(-m_width/2, i * m_height, k * m_width + (m_width / 2)),
                    Quaternion.Euler(-90, -90, _l[p].transform.rotation.z), transform);

                int q = Random.Range(0, _l.Count);
                GameObject temp2 =
                    Instantiate(_l[q],
                    new Vector3(width * m_width - (m_width/2), i * m_height, k * m_width + (m_width / 2)),
                    Quaternion.Euler(-90, 90, _l[q].transform.rotation.z), transform);
            }
        }
    }

    private void Start()
    {
        buildingMesh = gameObject.AddComponent<MeshCombiner>();

        buildingMesh.CreateMultiMaterialMesh = true;
        buildingMesh.DestroyCombinedChildren = true;

        buildingMesh.CombineMeshes(false);
    }
}
