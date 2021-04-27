using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Gem : MonoBehaviour
{
    public float VerticalBobFrequency = 1f;
    public Diamond diamond;
    public float BobbingAmount = 1f;
    public float RotatingSpeed = 360f;
    public GemsUI gemStats;

    public Rigidbody PickupRigidbody { get; private set; }

    Collider m_Collider;
    Vector3 m_StartPosition;
    bool m_HasPlayedFeedback;
    private Transform player;
    private Transform gem;
    protected virtual void Start()
    {
        GameObject gemchild = transform.parent.gameObject.transform.GetChild(0).gameObject;
        gem=gemchild.transform;
        player = GameObject.FindWithTag("Player").transform;
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
        transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f), RotatingSpeed * Time.deltaTime, Space.Self);
        Checkdistance();

	}
    public void Checkdistance(){
        float distance = Vector3.Distance(gem.position, player.position);
        if( distance<200f && distance >100f) {
                gemStats.randombool=true;
                if(player.position.z<gem.position.z){
                    if(player.position.x<gem.position.x)
                        gemStats.message="Gem 200m. straight on your right";
                    if(player.position.x>gem.position.x)
                        gemStats.message="Gem 200m. straight on your left";
                }
                if(player.position.z>gem.position.z){	
                    if(player.position.x<gem.position.x)
                        gemStats.message="Gem 200m. behind on your right";
                    if(player.position.x>gem.position.x)
                        gemStats.message="Gem 200m. behind on your left";
                }		
            }
            else{
                gemStats.message="";
                gemStats.randombool=false;
            }
    }

     void OnTriggerEnter(Collider other)
     {
         
        if (other.gameObject.tag == "Player"){
                diamond.treasures++;
                gemStats.SetGems();
                Destroy(gameObject);
        }
     }
}

