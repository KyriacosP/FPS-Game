using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class WeaponSpawner : MonoBehaviour
{
    //public
    //Weapon Conf
    public GameObject weaponPrefab;
    public WeaponData weaponData;
    public WeaponType type;
    //animation conf
    public float VerticalBobFrequency = 1f;
    public float BobbingAmount = 1f;
    public float RotatingSpeed = 360f;

    //private
    private WeaponManager weaponManager;
    private Rigidbody pickupRigidbody;
    private Collider m_Collider;
    private Vector3 m_StartPosition;

    protected virtual void Start()
    {
        //get components
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        pickupRigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();

        // ensure the physics setup is a kinematic rigidbody trigger
        pickupRigidbody.isKinematic = true;
        m_Collider.isTrigger = true;

        // Remember start position for animation
        m_StartPosition = transform.position;
    }

    void Update()
    {
        // Handle bobbing
        float bobbingAnimationPhase = ((Mathf.Sin(Time.time * VerticalBobFrequency) * 0.5f) + 0.5f) * BobbingAmount;
        transform.position = m_StartPosition + Vector3.up * bobbingAnimationPhase;

        // Handle rotating
        transform.Rotate(Vector3.up, RotatingSpeed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cylinder"))
        {
            weaponManager.AddWeapon(weaponPrefab, weaponData, type);
            Destroy(gameObject);
        }
    }
}

