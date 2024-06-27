using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Follower : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + new Vector3(0.49f, 6.4f, -17.25f);
    }
}
