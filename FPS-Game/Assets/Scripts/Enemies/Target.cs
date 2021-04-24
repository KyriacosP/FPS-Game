using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITarget
{
	public float health = 100;
	public void Damage(float damage)
	{
		health -= damage;
		if(health<=0)
		{
			Destroy(gameObject);
		}
	}
}
