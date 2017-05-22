using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int iHealth = 100;
    public int iCurrentHealth;

    public float fFlashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public float fSinkSpeed = 1f;

    bool bIsDead = false;
    bool bIsSinking = false;
    bool bDamaged = false;

    void Awake()
    {
        iCurrentHealth = iHealth;
    }

    public void TakeDamage(int amount)
    {
        bDamaged = true;
        iCurrentHealth -= amount;

        if(iCurrentHealth <= 0 && bIsDead == false)
        {
            Death();
        }
    }

    public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack)
    {
        yield return new WaitForSeconds(delay);

        try
        {
            TakeDamage(damage);

            Vector3 diff = playerPosition - transform.position;
            diff = diff / diff.sqrMagnitude;
            GetComponent<Rigidbody>().AddForce((transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);
        }catch(MissingReferenceException e)
        {
            Debug.Log(e.ToString());
        }
    }

    void Update()
    {
        if(bDamaged == true)
        {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("OutlineColor", flashColor);
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", 
                Color.Lerp(transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_OutlineColor"), Color.black, fFlashSpeed * Time.deltaTime));
        }

        bDamaged = false;

        if(bIsSinking == true)
        {
            transform.Translate(-Vector3.up * fSinkSpeed * Time.deltaTime);
        }
    }

    void Death()
    {
        bIsDead = true;

        transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
        StartSinking();
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        bIsSinking = true;
    }
}
