using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private GameObject player;
    private GameManager gameManager;
    public float hitpoints;
    public ParticleSystem explosion;
    public AudioClip click_sounds;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.game_is_active)
        {
            if (this.hitpoints>0)
                {
                    this.hitpoints--;
                }
            else
            {
                Destroy(gameObject);
                int randomNumber = Random.Range(1, 7);
                switch (randomNumber)
                {
                    case 1:
                        destroy_all_enemies();
                        break;
                    case 2:
                        spawn_enemies();
                        break;
                    case 3:
                        add_10k_points();
                        break;
                    case 4:
                        add_lives();
                        break;
                    case 5:
                        remove_lives();
                        break;
                    case 6:
                        spawn_boxes();
                        break;
                }           
            }               
        }
    }

    private void spawn_boxes()
    {
        gameManager.spawn_boxes_powerup();
        Vector3 spawnPosition = transform.position; // Get the position of the power-up box
        float spacing = 2f;
        for (int i = 0; i < 5; i++)
        {
            // Calculate offset based on index to create a formation
            float xOffset = (i % 3 - 1) * spacing; // Adjust 3 to change formation width
            float zOffset = (i / 3 - 1) * spacing; // Adjust 3 to change formation depth

            Vector3 enemyPosition = spawnPosition + new Vector3(xOffset, 0f, zOffset);
            GameObject newBox= Instantiate(gameManager.targets[4], enemyPosition, Quaternion.identity);
            Rigidbody rb = newBox.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    private void remove_lives()
    {
        gameManager.remove_lives();
    }

    private void add_lives()
    {
        gameManager.add_lives();
    }

    private void add_10k_points()
    {
        gameManager.Update_score_powerup();
    }

    private void spawn_enemies()
    {
        gameManager.spawned_enemies();
        Vector3 spawnPosition = transform.position; // Get the position of the power-up box
        float spacing = 2f; 
        for (int i = 0; i < 10; i++)
        {
            // Calculate offset based on index to create a formation
            float xOffset = (i % 3 - 1) * spacing; // Adjust 3 to change formation width
            float zOffset = (i / 3 - 1) * spacing; // Adjust 3 to change formation depth

            Vector3 enemyPosition = spawnPosition + new Vector3(xOffset, 0f, zOffset);

            int enemyIndex = Random.Range(0, 4); // Randomly select an enemy prefab
            Instantiate(gameManager.targets[enemyIndex], enemyPosition, Quaternion.identity);
        }

    }

    private void destroy_all_enemies()
    {
        gameManager.destory_all_enemies();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().OnMouseDown();
        }
    }
}
