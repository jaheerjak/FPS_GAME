using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private PlayerHealth target;
    [SerializeField] private float damage = 10f;

    void Start()
    {
        target=FindObjectOfType<PlayerHealth>();
    }

   public void AttackAnimEvent()
    {
        if (target == null) return;
        target.TakeDamage(damage);
        Debug.Log("Attacking....");
        
    }
}
