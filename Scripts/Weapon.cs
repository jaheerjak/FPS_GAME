using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera FpCamera;
    [SerializeField] private float  range=100f;
    [SerializeField] private float  damage=25f;
    [SerializeField] private ParticleSystem  muzzleFlash;
    [SerializeField] private GameObject  hitEffect;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRayCast();
    }
    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }
    private void ProcessRayCast()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(FpCamera.transform.position, FpCamera.transform.forward, out hit, range))
        {
            CreateImpact(hit);
            //TODO: Add some hit effects
            EnemyHealth target=hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return; 
            target.TakeDamage(damage);

        }
        else
        {
            return;
        }

    }
    void CreateImpact(RaycastHit hit)
    {
        GameObject impact=Instantiate(hitEffect,hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .2f);
    }
}
