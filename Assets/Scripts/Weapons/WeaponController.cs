using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected GameObject prefab;
    [SerializeField] float damage;
    [SerializeField] public float speed;
    [SerializeField] float cooldownDuration;
    float currentCooldown;
    [SerializeField] int pierce;
    protected PlayerMovement playerMovement;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        currentCooldown = cooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = cooldownDuration;
    }
}
