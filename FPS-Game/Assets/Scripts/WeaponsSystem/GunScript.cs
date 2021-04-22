using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public AudioSource audioSource;
   
    public Camera fpsCam;
    public WeaponManager weaponManager;
    public GameObject activeWeapon;
    public WeaponData activeWeaponData;

    public int maxAmmo = 10;
    public int currentAmmo = -1;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        audioSource = GetComponent<AudioSource>();
        fpsCam = gameObject.GetComponentInParent<Camera>();
        currentAmmo = maxAmmo;
      
    }

    void OnEnable()
    {
        isReloading = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isReloading || activeWeaponData == null)
            return;
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / activeWeaponData.fireRate;
            Shoot();
        }
    }

	private void LateUpdate()
	{
        GetWeapon();
    }

	private void GetWeapon()
    {
        if (weaponManager == null || weaponManager.active == null)
            return;
        if (activeWeapon != null && activeWeapon == weaponManager.active)
            return;
        
        activeWeapon = weaponManager.active;
        activeWeaponData = weaponManager.activeWeaponData;
        audioSource.clip = activeWeaponData.sound;
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
        EnemyController enemyScript;
        GuardController guardScript;
        activeWeaponData.muzzleFlash.Play();
        audioSource.Play();

        currentAmmo--;

		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, activeWeaponData.range))
		{
			Target target = hit.transform.GetComponent<Target>();
			if (target != null)
			{
				target.Damage(activeWeaponData.damage);
			}

			if (hit.transform.CompareTag("Enemy"))
			{
				enemyScript = hit.transform.GetComponent<EnemyController>();
				enemyScript.health -= activeWeaponData.damage;
				enemyScript.iwashit = true;
			}
			else if (hit.transform.CompareTag("Guard"))
			{
				guardScript = hit.transform.GetComponent<GuardController>();
				guardScript.health-= activeWeaponData.damage;
				guardScript.iwashit = true;
			}

			if (hit.rigidbody != null)
			{
				hit.rigidbody.AddForce(-hit.normal * activeWeaponData.impactForce);
			}

			GameObject impactEffectObject = Instantiate(activeWeaponData.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(impactEffectObject, 0.3f);
		}
	}

}
