using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float damage = 20f;

    public GameObject explosionEffect;

    private float countdown;
    private bool hasExploded=false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown<=0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
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

            Target target = collider.GetComponent<Target>();
            if (target != null)
            {
                // linear falloff of effect
                float proximity = (transform.position - target.transform.position).magnitude;
                float effect = 1 - (proximity / radius);

                target.Damage(damage * effect);
            }
        }

        Destroy(explosion, 2f);
        Destroy(gameObject);
        
    }
}
