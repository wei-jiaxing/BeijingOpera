using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class CSVManager : MonoBehaviour {

	private List<float> _timing = new List<float>();
	private List<int> _lineNum = new List<int>();

	public string filePass;

	private static CSVManager _instance;

	public static CSVManager Instance
	{
		get
		{
			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;
	}

	public List<float> LoadCSV(){
		int i = 0;
		string filePath = filePass + MusicSelect.Instance.currentSelectedIndex;
		TextAsset csv = Resources.Load (filePath) as TextAsset;
		if (csv == null)
		{
			Debug.LogError("File cannot be found: " + filePath);
			return new List<float>();
		}
		StringReader reader = new StringReader (csv.text);
		while (reader.Peek () > -1) {

			string line = reader.ReadLine ();
			string[] values = line.Split (',');
			_timing.Add(float.Parse(values[0]));
			_lineNum.Add(int.Parse(values[1]));
			i++;
		}
		for (int k = 0; k < _timing.Count; k++)
		{
			Debug.LogWarning("Num: "+ k + "  Time: " + _timing[k]);
		}
		return _timing;
	}
}
