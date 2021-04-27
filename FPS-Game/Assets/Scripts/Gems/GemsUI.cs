using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsUI : MonoBehaviour
{	
	public int maxgems=7;
	public Text gems;
	public Text gemsdistance;
	public void Start()
	{
		player = GameObject.FindWithTag("Player").transform;
		gem = GameObject.FindWithTag("Treasure").transform;
		Panel.gameObject.SetActive (false);
		gems.text = maxgems.ToString();
	}

    public void SetGems()
	{
        maxgems--;
		gems.text=maxgems.ToString();
	}
	
	private Transform player;
	private Transform gem;
	public GameObject Panel;
	public void Update(){
		if(Vector3.Distance(gem.position, player.position) <200f && Vector3.Distance(gem.position, player.position) >170f) {
			Panel.gameObject.SetActive (true);
			if(player.position.z<gem.position.z){
				if(player.position.x<gem.position.x)
					gemsdistance.text = ("Gem 200m. straight on your right");
				if(player.position.x>gem.position.x)
					gemsdistance.text = ("Gem 200m. straight on your left");
			}
			if(player.position.z>gem.position.z){	
				if(player.position.x<gem.position.x)
					gemsdistance.text = ("Gem 200m. behind on your right");
				if(player.position.x>gem.position.x)
					gemsdistance.text = ("Gem 200m. behind on your left");
			}		
		}
		else{
			Panel.gameObject.SetActive (false);
			gemsdistance.text="";
		}
	}
	
}
