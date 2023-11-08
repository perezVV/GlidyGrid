using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private PlayerController playerScript;

    private float cooldown;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerController>();
        }
        if (gameObject.CompareTag("Spike"))
        {
            cooldown = 1f;
        }
        else if (gameObject.CompareTag("Log"))
        {
            cooldown = 0.15f;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.GotHit(cooldown);
        }
    }
}
