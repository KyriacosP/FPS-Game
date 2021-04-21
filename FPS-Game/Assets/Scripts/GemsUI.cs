using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsUI : MonoBehaviour
{	
	public int maxgems=7;
	public Text gems;
	public void Start()
	{
		gems.text = maxgems.ToString();
	}

    public void SetGems()
	{
        maxgems--;
		gems.text=maxgems.ToString();
	}
}
