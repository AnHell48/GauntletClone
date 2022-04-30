using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    GameObject enemy, enemyGroup;
    Image healthBar;
    
    Vector3 spawnPosition;
    public bool canSpawn;
    private int maxSpawns, spawnCounter;
    private float spawnerCooldown, spawnerCooldownTime, currentHealth,maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 10f;
        currentHealth = maxHealth;
        healthBar = GameObject.Find("green").GetComponentInChildren<Image>();
        enemyGroup = new GameObject("EnemyGroup");
        enemyGroup.transform.position = this.transform.position;
        SpawnNewEnemy();
        canSpawn = true;
        maxSpawns = 3;
        spawnCounter = 0;
        spawnerCooldownTime = 5f;
        spawnerCooldown = spawnerCooldownTime;
    }

    private void SpawnNewEnemy()
    {
            Debug.Log("DING DING INCOMING!");

        spawnPosition = this.transform.localPosition; //new Vector3(this.transform.position.x, 1, this.transform.position.z + 20);
        spawnPosition.y = 1;         
        enemy = Instantiate(Resources.Load("Enemies/enemy", typeof(GameObject))) as GameObject;
        enemy.transform.position = spawnPosition;
        enemy.transform.SetParent(enemyGroup.transform);
        spawnCounter++;

        /*Attach to empty obj to keep track?*/
    }

    // Update is called once per frame
    void Update()
    {
        if (CanNewEnemySpawn())
            SpawnNewEnemy();
    }

    private bool CanNewEnemySpawn()
    {
         spawnerCooldown -= Time.deltaTime;
        //  Debug.Log("timer: "+ spawnerCooldown +"  Enemies: "+ spawnsCounter);
        if (enemyGroup.transform.childCount < maxSpawns && spawnerCooldown <= 0)
        {
            spawnerCooldown = spawnerCooldownTime; 
            return true;
        }

        return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        switch(other.transform.tag)
        {
            case "Weapon":
            //pass the damage
                TakeDamage(other.transform.GetComponent<WeaponBehaviour>().DamagePower);
                break;
        }
    }

    private void TakeDamage(float damageTaken)
    {
        //todo
        //check health
        //if 0 or less then destroy else then nothing(later display damage VISUALLY?)
        currentHealth -= damageTaken;
        //update the bar
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0f,1f);

        if(currentHealth <= 0 )
            Destroy(this.gameObject);
    }

}
