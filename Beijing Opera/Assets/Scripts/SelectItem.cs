using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour 
{
    public Toggle toggle;
    public Text text;
    public int index;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void OnSelect()
    {
        MusicSelect.Instance.OnSelect(index);
    }

}
