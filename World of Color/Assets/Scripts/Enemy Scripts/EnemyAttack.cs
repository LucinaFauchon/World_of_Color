﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GenericStats myStats;

    //attack
    public Vector2 attackDirection = Vector2.up;
    public bool canAttack = false;  //true if enemy is within attacking range

    //cooldown
    public float attackDuration = 1.0f; //attack duration in seconds
    public float currCooldown = 0.0f;

    public Animator myAnim;

    //different colliders based on what animation is playing (up, down, left, right, idle/normal)
    public GameObject[] colliders = new GameObject[5];
    public GameObject currCollider;

    public ContactFilter2D playerLayer;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        currCollider = colliders[4];
        currCollider.SetActive(true);

        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack && currCooldown >= myStats.attackCooldown)
        {
            StartCoroutine(Attack());
        }

        if (currCooldown < myStats.attackCooldown)
        {
            //increment cooldown
            currCooldown += Time.deltaTime;
        }
    }

    //attack
    IEnumerator Attack()
    {
        //choose attack animation and collider based on attack direction
        SwitchAttackAnimations(attackDirection);

        Collider2D[] hitByAttack = new Collider2D[10];

        int numColliders = Physics2D.OverlapCollider(currCollider.GetComponent<PolygonCollider2D>(), playerLayer, hitByAttack);
        for(int i = 0; i < numColliders; i++)
        {
            //only deal damage once
            if(hitByAttack[i].gameObject.tag == "Player")
            {
                Player.GetComponent<Player_Health>().TakeDamage(myStats.attackDamage, attackDirection);
                break;
            }
        }

        //start cooldown
        currCooldown = 0;

        //wait before resetting animation/collider
        yield return new WaitForSeconds(attackDuration);

        //go to idle when not attacking
        myAnim.SetBool("is_attacking", false);

        //disable the previous collider and switch to the new collider for idle
        currCollider.SetActive(false);
        currCollider = colliders[4];
        currCollider.SetActive(true);

        StopAllCoroutines();
    }

    //choose attack animation based on direction
    void SwitchAttackAnimations(Vector2 direction)
    {
        //disable the previous collider
        currCollider.SetActive(false);

        if (direction == Vector2.up)
        {
            myAnim.SetFloat("attack_direction", 0.0f); //up
            currCollider = colliders[0];          
        }
        if (direction == Vector2.down)
        {
            myAnim.SetFloat("attack_direction", 1.0f); //down
            currCollider = colliders[1];
        }
        if (direction == Vector2.left)
        {
            myAnim.SetFloat("attack_direction", 2.0f); //left
            currCollider = colliders[2];
        }
        if (direction == Vector2.right)
        {
            myAnim.SetFloat("attack_direction", 3.0f); //right
            currCollider = colliders[3];
        }

        //enable the new collider
        currCollider.SetActive(true);
        myAnim.SetBool("is_attacking", true);
    }

}
