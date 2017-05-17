using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement: MonoBehaviour {
	
	protected Animator avatar;
    protected PlayerAttack playerAttack;

    float fLastAttackTime = 0f, fLastSkillTime = 0f, fLastDashTime = 0f;
    float fHeight, fWidth;
    public bool bAttacking = false;
    public bool bDashing = false;

    void Start () 
	{
		avatar = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
	}
    
	public void OnStickChanged(Vector2 stickPos)
	{
		fHeight = stickPos.x;
		fWidth = stickPos.y;
	}

	void Update () 
	{
        if(avatar)
		{
            float fBack = 1f;

            if (fWidth < 0f)
                fBack = -1f;

			avatar.SetFloat("Speed", (fHeight*fHeight+fWidth*fWidth));

		    Rigidbody rigidbody = GetComponent<Rigidbody>();

            if(rigidbody)
            {
                Vector3 vSpeed = rigidbody.velocity;
                vSpeed.x = 4 * fHeight;
                vSpeed.z = 4 * fWidth;
                rigidbody.velocity = vSpeed;

				if(fHeight != 0f && fWidth != 0f){
					transform.rotation = Quaternion.LookRotation(new Vector3(fHeight, 0f, fWidth));
				}
            }
		}		
	}

    public void OnAttackDown()
    {
        bAttacking = true;
        avatar.SetBool("Combo", true);
        StartCoroutine(StartAttack());
    }

    public void OnAttackUp()
    {
        avatar.SetBool("Combo", false);
        bAttacking = false;
    }

    public void OnSkiilDown()
    {
        if (Time.time - fLastSkillTime > 1f)
        {
            avatar.SetBool("Skill", true);
            fLastSkillTime = Time.time;
            playerAttack.SkillAttack();
        }
    }

    public void OnSkillUp()
    {
        avatar.SetBool("Skill", false);
    }

    public void OnDashDown()
    {
        if (Time.time - fLastDashTime > 1f)
        {
            fLastDashTime = Time.time;
            bDashing = true;
            avatar.SetTrigger("Dash");
            playerAttack.DashAttack();
        }
    }

    public void OnDashUp()
    {
        bDashing = false;
    }

    IEnumerator StartAttack()
    {
        if(Time.time - fLastAttackTime > 1f)
        {
            fLastAttackTime = Time.time;
            while(bAttacking == true)
            {
                avatar.SetTrigger("AttackStart");
                playerAttack.NormalAttack();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
