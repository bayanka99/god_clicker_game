using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_out_of_bound : MonoBehaviour
{
    public float topbound = 30.0f;
    public float lowerbound = -10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z>topbound)
        {
            Destroy(gameObject);
        }
        if (transform.position.z < lowerbound)
        {
            Destroy(gameObject);
        }

    }
}
