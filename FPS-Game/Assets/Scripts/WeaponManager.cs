using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum WeaponType {RIFLE=1, HEAVY=2, HANDGUN=3, GRENADE=4};

    public GameObject rifle;
    public GameObject heavy;
    public GameObject handgun;
    public GameObject grenade;
    public WeaponType selectedWeapon;

    //public int selectedWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = WeaponType.RIFLE;
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponType previousSelectedWeapon = selectedWeapon;

        /*if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapon >= transform.childCount-1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }*/

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
        else if (Input.GetKeyDown(KeyCode.Alpha4) && grenade != null)
        {
            selectedWeapon = WeaponType.GRENADE;
        }


        if (previousSelectedWeapon!=selectedWeapon)
        {
            SelectWeapon();
        }
    }

    public void AddWeapon(GameObject weapon, WeaponType type)
    {
        Debug.Log("TYPE" + type);
        GameObject go = Instantiate(weapon, transform.position, transform.rotation);
        go.transform.parent = transform;
        go.transform.localScale = new Vector3(3, 3, 3);
        go.SetActive(false);
        if (type == WeaponType.RIFLE)
        {
            rifle = go;           
        }
        else if (type == WeaponType.HEAVY)
        {
            heavy = go;
        }
        else if (type == WeaponType.HANDGUN)
        {
            handgun = go;
        }
        else if (type == WeaponType.GRENADE)
        {
            grenade = go;
        }
    }    

    private void SelectWeapon()
    {
        if(selectedWeapon==WeaponType.RIFLE)
        {
            EnableWeapon(rifle);
            DisableWeapon(heavy);
            DisableWeapon(handgun);
            DisableWeapon(grenade);
        }
        else if (selectedWeapon == WeaponType.HEAVY)
        {
            DisableWeapon(rifle);
            EnableWeapon(heavy);
            DisableWeapon(handgun);
            DisableWeapon(grenade);
        }
        else if (selectedWeapon == WeaponType.HANDGUN)
        {
            DisableWeapon(rifle);
            DisableWeapon(heavy);
            EnableWeapon(handgun);
            DisableWeapon(grenade);
        }
        else if (selectedWeapon == WeaponType.GRENADE)
        {
            DisableWeapon(rifle);
            DisableWeapon(heavy);
            DisableWeapon(handgun);
            EnableWeapon(grenade);
        }
        /*int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i==selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }*/
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
