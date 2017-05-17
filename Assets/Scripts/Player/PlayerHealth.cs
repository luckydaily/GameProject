using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int iHealth = 100;
    public int iCurrentHealth = 0;
    public Slider healthSlider;
    public Image damage;
    public AudioClip deathClip;

    Animator playerAnim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    bool bIsDead = false;

    void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        iCurrentHealth = iHealth;
    }

	public void TakeDamage(int amount)
    {
        iCurrentHealth -= amount;
        healthSlider.value = iCurrentHealth;

        if(iCurrentHealth <= 0 && bIsDead == true)
        {
            Death();
        }
        else
        {
            playerAnim.SetTrigger("Damage");
        }
    }

    void Death()
    {
        bIsDead = true;

        playerAnim.SetTrigger("Die");
        playerMovement.enabled = false;
    }
}
