using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody rb;
    private GameObject player;
    private GameManager gameManager;
    public float points;
    public ParticleSystem explosion;
    public AudioClip click_sounds;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        gameManager= GameObject.Find("GameManager").GetComponent<GameManager>();

        switch (gameManager.difficulty)
        {
            case 1:
                speed = speed*1; break;
            case 2:
                speed = speed*2; break;
            case 3:
                speed = speed*4; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce((player.transform.position - transform.position).normalized * speed);
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
 
    }


    private void OnMouseDown()
    {
        if (gameManager.game_is_active)
        {
            Destroy(gameObject);
            gameManager.Update_score(this.points, this.click_sounds);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
        }


    }

    private void OnTriggerEnter(Collider other)
    {

       
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (gameManager.game_is_active)
            {
                gameManager.edit_life(-1,other);
            }
        }
      //  if (!gameObject.CompareTag("Bad"))
      //  {
       //     manager.gameover();
      //  }
    }

}
