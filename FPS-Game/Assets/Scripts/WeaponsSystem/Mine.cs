using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float damage = 20f;
    public GameObject explosionEffect;
    private float countdown;
    public PlayerHealth healthscript;
    

    void Start()
    {
        healthscript=GameObject.FindWithTag("Cylinder").GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other){
        if( other.gameObject.tag == "Player"){
            healthscript.health-=5; 
            Explode();
        }
        }
    

        void Explode()
    {   
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if(rb!=null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            ITarget target = collider.GetComponent<ITarget>();
            Transform collTrans = collider.GetComponent<Transform>();
            if (target != null && collTrans != null)
            { 
                // linear falloff of effect

                float proximity = (transform.position - collTrans.position).magnitude;
                float effect = 1 - (proximity / radius);

                target.Damage(damage * effect);
            }
        }

       // Destroy(explosion, 2f);
        
        
    }
}
