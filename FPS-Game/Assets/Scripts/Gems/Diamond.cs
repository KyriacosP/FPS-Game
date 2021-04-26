using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Diamond : MonoBehaviour
{
    public GameObject diamond;
    public float VerticalBobFrequency = 1f;
    public int level;
    public float BobbingAmount = 1f;
    public float RotatingSpeed = 360f;

    public Rigidbody PickupRigidbody { get; private set; }
    public int treasures;
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

        Image.gameObject.SetActive(false);
    }

    void Update()
    {
        // Handle bobbing
        float bobbingAnimationPhase = ((Mathf.Sin(Time.time * VerticalBobFrequency) * 0.5f) + 0.5f) * BobbingAmount;
        transform.position = m_StartPosition + Vector3.up * bobbingAnimationPhase;

        // Handle rotating
        transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f), RotatingSpeed * Time.deltaTime, Space.Self);
    }

    public GameObject Image;

     void OnTriggerEnter(Collider other)
     {
  
        if (other.gameObject.tag == "Player" && treasures==0 && level==1){
                Image.gameObject.SetActive(true);
                SceneManager.LoadScene("Level2");
        }
        if (other.gameObject.tag == "Player" && treasures==0 && level==2){
                Image.gameObject.SetActive(true);
                Time.timeScale = 0f;
        }

     }
}

