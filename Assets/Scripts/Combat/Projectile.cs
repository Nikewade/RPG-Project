using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] bool isHoming = false;
    [SerializeField] GameObject hitEffect = null;

    Health target = null;
    float damage = 0;

    private void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        if(isHoming && !target.IsDead()) transform.LookAt(GetAimLocation());

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2; // Adding + 1 Y * capsule height / 2
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;
        if (target.IsDead()) return;
        target.TakeDamage(damage);

        if (hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }

        Destroy(gameObject);

    }
}
