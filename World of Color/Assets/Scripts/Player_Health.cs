﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    //health UI 
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite EmptyHeart;

    //player graidiant Control
    public Material PlayerMat;
    float distance;

    private void Update()
    {
        HealthUIController();
        PlayerMatController();
    }


    void HealthUIController()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void PlayerMatController()
    {
        distance = (0.5f / numOfHearts) * health + 0.06f;
        PlayerMat.SetFloat("Distance", distance);
    }
}
