using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "WeaponData") ]
public class WeaponData : ScriptableObject
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public ParticleSystem muzzleFlashHolder;
    public GameObject impactEffect;
    public AudioClip sound;
    public ParticleSystem muzzleFlash;
}
