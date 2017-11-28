using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour 
{
	private Slider slider;


	void Start () 
	{
		slider = GetComponent<Slider>();
		slider.value = 0;
	}
	

	void Update () 
	{
		
	}

    public void SetProgress(float progress)
    {
        slider.value = progress;
    }
}
