﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    public int currDurability = 3;
    public int maxDurability = 3;

    public float currCooldownTime = 0.0f;
    public float maxCooldownTime = 15.0f;

    public Animator myAnim;
    public bool isBroken = false;

    //sounds
    public PlayerSounds mySounds;
    public AudioSource audioSource;

    //UI
    public Image shieldBar;
    public Material shieldBarMat;

    // Start is called before the first frame update
    void Start()
    {
        shieldBarMat.SetFloat("Saturation", 1.0f);
        myAnim.SetBool("HasShield", true);
    }

    private void OnDisable()
    {
        shieldBarMat.SetFloat("Saturation", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(currDurability <= 0 && !isBroken)
        {
            shieldBarMat.SetFloat("Saturation", 0.0f);
            audioSource.PlayOneShot(mySounds.player_shieldBreak);
            myAnim.SetBool("HasShield", false);
            isBroken = true;
        }

        if (isBroken)
        {
            currCooldownTime += Time.deltaTime;

            shieldBar.fillAmount = (1f / maxCooldownTime) * currCooldownTime;

            if (currCooldownTime >= maxCooldownTime)
            {
                isBroken = false;
                myAnim.SetBool("HasShield", true);
                currCooldownTime = 0;
                currDurability = maxDurability;
                shieldBar.fillAmount = 1.0f;
                audioSource.PlayOneShot(mySounds.player_shieldRecharge);
                shieldBarMat.SetFloat("Saturation", 1.0f);
            }
        }
        else
        {
            shieldBar.fillAmount = (1f / maxDurability) * currDurability;
        }
    }

    public void BlockAttack()
    {
        audioSource.PlayOneShot(mySounds.player_shield);
        currDurability--;
    }
}
