using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class WeaponSpawner : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public WeaponManager weaponManager;
    public float VerticalBobFrequency = 1f;

    public float BobbingAmount = 1f;
    public float RotatingSpeed = 360f;

    public Rigidbody PickupRigidbody { get; private set; }

    Collider m_Collider;
    Vector3 m_StartPosition;
    bool m_HasPlayedFeedback;

    protected virtual void Start()
    {
        PickupRigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();

        // ensure the physics setup is a kinematic rigidbody trigger
        PickupRigidbody.isKinematic = true;
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
        Debug.Log("HIT");
        WeaponManager playerWeaponsManager = other.GetComponent<WeaponManager>();
        Debug.Log(other);
        if (weaponManager)
        {
            Debug.Log("Player");
            weaponManager.AddWeapon(WeaponPrefab, WeaponManager.WeaponType.HEAVY);
            Destroy(gameObject);
        }
    }
}

