using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float fTimeBetweenAttack = 0.5f;
    public int iAttackDamage = 10;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    bool bPlayerInRange;
    float fTimer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == player)
        {
            bPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == player)
        {
            bPlayerInRange = false;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        fTimer = Time.deltaTime;
        if(fTimer >= fTimeBetweenAttack && bPlayerInRange == true && enemyHealth.iCurrentHealth > 0)
        {
            Attack();
        }
	}

    void Attack()
    {
        fTimer = 0f;

        if(playerHealth.iCurrentHealth > 0)
        {
            playerHealth.TakeDamage(iAttackDamage);
        }
    }
}
