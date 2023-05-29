using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBattle : MonoBehaviour
{
    public List<Transform> spawnLocations = new List<Transform>();
    public List<int> spawnAmt = new List<int>();

    public GameObject toSpawn;
    public bool spawn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!spawn)
        {
            if(other.transform.tag == "Player")
            {
                int i = 0;
                foreach(Transform t in spawnLocations)
                {
                    for (int j = 0; j < spawnAmt[i]; j++)
                    {
                        Instantiate(toSpawn, t.position, Quaternion.identity);
                    }
                    i++;
                }
                spawn = true;
            }
        }
    }
}
