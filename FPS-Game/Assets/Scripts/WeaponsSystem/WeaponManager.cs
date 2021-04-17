using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private GameObject rifle;
    private WeaponData rifleData;
    private GameObject heavy;
    private WeaponData heavyData;
    private GameObject handgun;
    private WeaponData handgunData;

    public WeaponType selectedWeapon;
    public GameObject active;
    public WeaponData activeWeaponData;

    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = WeaponType.NULL;
        active = null;
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponType previousSelectedWeapon = selectedWeapon;

        if(Input.GetKeyDown(KeyCode.Alpha1) && rifle!=null)
        {
            selectedWeapon = WeaponType.RIFLE;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && heavy!=null)
        {
            selectedWeapon = WeaponType.HEAVY;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && handgun!=null)
        {
            selectedWeapon = WeaponType.HANDGUN;
        }

        if (previousSelectedWeapon!=selectedWeapon)
        {
            SelectWeapon();
        }
    }

    public void AddWeapon(GameObject weapon,WeaponData data, WeaponType type)
    {
        GameObject go = Instantiate(weapon, transform.position, transform.rotation);
        go.transform.parent = transform;
        go.transform.localScale = new Vector3(3, 3, 3);
        go.SetActive(false);
        Transform muzzleFlashHolder = go.transform.Find("MuzzleFlashHolder");
        ParticleSystem ps = Instantiate(data.muzzleFlashHolder, muzzleFlashHolder.position, muzzleFlashHolder.rotation);
        ps.transform.parent = transform;
        data.muzzleFlash = ps;
       

        if (type == WeaponType.RIFLE)
        {
            rifle = go;
            rifleData = data;
        }
        else if (type == WeaponType.HEAVY)
        {
            heavy = go;
            heavyData = data;
        }
        else if (type == WeaponType.HANDGUN)
        {
            handgun = go;
            handgunData = data;
        }
    }    

    private void SelectWeapon()
    {
        if(selectedWeapon==WeaponType.RIFLE)
        {
            EnableWeapon(rifle);
            DisableWeapon(heavy);
            DisableWeapon(handgun);
            active = rifle;
            activeWeaponData = rifleData;
        }
        else if (selectedWeapon == WeaponType.HEAVY)
        {
            DisableWeapon(rifle);
            EnableWeapon(heavy);
            DisableWeapon(handgun);
            active = heavy;
            activeWeaponData = heavyData;
        }
        else if (selectedWeapon == WeaponType.HANDGUN)
        {
            DisableWeapon(rifle);
            DisableWeapon(heavy);
            EnableWeapon(handgun);
            active = handgun;
            activeWeaponData = handgunData;
        }
    }

    private void EnableWeapon(GameObject weapon)
    {
        if(weapon!=null)
        {
            weapon.SetActive(true);
        }
        
    }
    private void DisableWeapon(GameObject weapon)
    {
        if (weapon != null)
        {
            weapon.SetActive(false);
        }

    }
}
