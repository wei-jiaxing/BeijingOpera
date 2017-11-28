using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelect : MonoBehaviour 
{
    public ToggleGroup tg;
    public GameObject selectItemPrefab;
    public Transform contentTF;
    [HideInInspector]
    public int currentSelectedIndex;

    private static MusicSelect _instance;

    public static MusicSelect Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }


    // Use this for initialization
    void Start () 
    {
        Init();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void Init()
    {
        for (int i = 0; i < GameManager.Instance.musicDataList.Count; i++)
        {
            GameObject go = Instantiate(selectItemPrefab, contentTF);
            go.transform.localScale = Vector3.one;
            go.transform.position = Vector3.zero;

            SelectItem item = go.GetComponent<SelectItem>();
            item.index = i;
            item.text.text = GameManager.Instance.musicDataList[i].musicName;
            item.toggle.group = tg;
        }
    }

    public void OnSelect(int index)
    {
        currentSelectedIndex = index;
        GameManager.Instance.SetCurrentMusic(currentSelectedIndex);
    }

}
