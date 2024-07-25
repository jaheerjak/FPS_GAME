using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float turnSpeed = 5f;
    NavMeshAgent navMeshAgent;
    float distancetoTarget = Mathf.Infinity;
    bool isProvoked = false;
    private Animator _animator;
   
    void Start()
    {
        navMeshAgent=GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distancetoTarget=Vector3.Distance(transform.position, _target.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if(distancetoTarget <= chaseRange)
        {
            isProvoked = true;
            // navMeshAgent.SetDestination(_target.position);

        }
    }
    private void EngageTarget()
    {
        FaceTarget();
        if (distancetoTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if(distancetoTarget < navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }

    }
    private void ChaseTarget() 
    {
        _animator.SetBool("attack", false);
        _animator.SetTrigger("move");
        navMeshAgent.SetDestination(_target.position);
    }
    private void AttackTarget() 
    {
        _animator.SetBool("attack", true);
    }
    private void FaceTarget()
    {
        Vector3 direction = (transform.position - _target.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.color = new Color(1,1,0.75f);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
