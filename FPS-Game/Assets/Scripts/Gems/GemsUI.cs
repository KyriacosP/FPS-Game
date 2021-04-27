using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsUI : MonoBehaviour
{	
	public int maxgems=7;
	public Text gems;
	public Text gemsdistance;
	public GameObject Panel;

	public void Start()
	{
		gems.text = maxgems.ToString();
	}

    public void SetGems()
	{
        maxgems--;
		gems.text=maxgems.ToString();
	}

	public string message;
	public bool randombool;
	
	public void Update(){
		Panel.gameObject.SetActive(randombool);
	 	gemsdistance.text=message;
	}
}
