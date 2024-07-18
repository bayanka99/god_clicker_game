using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody rb;
    public List<GameObject> blood;
    private GameObject player;
    private GameManager gameManager;
    public float points;
    public ParticleSystem explosion;
    public AudioClip bite_sound;
    


    private Animator animator;



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        gameManager= GameObject.Find("GameManager").GetComponent<GameManager>();

        switch (gameManager.difficulty)
        {
            case 1:
                speed = speed*1; break;
            case 2:
                speed = speed*2; break;
            case 3:
                speed = speed*3; break;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
     
        animator.SetFloat("speed", 1);
        if (player != null)
        {
            //rb.AddForce((player.transform.position - transform.position).normalized * speed);
            rb.velocity = (player.transform.position - transform.position).normalized * speed;
        }
 
    }


    public void OnMouseDown()
    {
        if (!gameManager.game_is_paused)
        {

            int index = UnityEngine.Random.Range(0, 3);

            // Instantiate(targets[index],spawnPosition, Quaternion.identity);
            GameObject blood_stains = Instantiate(blood[index], transform.position, Quaternion.identity);
            gameManager.destroy_blood(blood_stains);


            if (gameManager.game_is_active)
            {

                Destroy(gameObject);
                gameManager.Update_score(this.points);

                ParticleSystem explosionParticles = Instantiate(explosion, transform.position, explosion.transform.rotation);

                // Destroy the explosion GameObject after the duration of the particle system
                if (explosionParticles != null)
                {
                    Destroy(explosionParticles, explosionParticles.main.duration);
                }
            }
        }


    }


  

    private void OnTriggerEnter(Collider other)
    {
        int index = UnityEngine.Random.Range(0, 3);
        GameObject blood_stains = Instantiate(blood[index], transform.position, Quaternion.identity);
        gameManager.destroy_blood(blood_stains);

        if (other.CompareTag("Player"))
        {

            Destroy(gameObject);

            if (gameManager.game_is_active)
            {
                gameManager.edit_life(-1,other, this.bite_sound);

            }
        }

        if (other.CompareTag("Power_up"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            
        }

        //  if (!gameObject.CompareTag("Bad"))
        //  {
        //     manager.gameover();
        //  }
    }

}
