using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public int maxAmmo = 10;
    public int currentAmmo = -1;
    public float reloadTime = 1f;
    private bool isReloading = false;


    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;
        if(currentAmmo<=0)
        {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target !=null)
            {
                target.Damage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactEffectObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactEffectObject, 0.3f);
        }
    }
}
