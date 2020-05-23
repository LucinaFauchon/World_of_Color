﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GenericStats myStats;
    public EnemyMovement myMovement;

    public int currHealth;

    public AudioSource takeDamageSound;

    public GameObject RoomManager;

    public bool isPurified = false;

    private void Awake()
    {
        RoomManager.GetComponent<StartingAreaManager>().UnPurified();
    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = myStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currHealth > myStats.maxHealth)
        {
            currHealth = myStats.maxHealth;
        }

        if(currHealth <= 0 && isPurified == false)
        {
            //play purification animation?
            RoomManager.GetComponent<StartingAreaManager>().Purified();
            myMovement.enemyState = EnemyMovement.EnemyState.Purified;
            //keep this or break shader
            isPurified = true;
        }
    }

    //reduce health if we take damage
    public void TakeDamage(int damage)
    {
        if (myMovement.enemyState != EnemyMovement.EnemyState.Purified)
        {
            takeDamageSound.Play();
            currHealth -= damage;
        }
    }
}
