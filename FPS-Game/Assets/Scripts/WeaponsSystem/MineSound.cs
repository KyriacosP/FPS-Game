using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSound : MonoBehaviour
{
    public GameObject mine;
    public AudioSource mineaudio;
    // Start is called before the first frame update
    void Start()
    {
        mineaudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other){
        if( other.gameObject.tag == "Player"){
            mineaudio.Play();
            StartCoroutine(MineKill());
        }    
    }
    
    IEnumerator MineKill()
    {
         yield return new WaitForSeconds(3);
         gameObject.SetActive(false);
         
    }

}
