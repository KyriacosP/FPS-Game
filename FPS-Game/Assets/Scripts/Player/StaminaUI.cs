using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxStam(float stamina)
	{
		slider.maxValue = stamina;
		slider.value = stamina;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetStamina(float stamina)
	{
		slider.value = stamina;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
