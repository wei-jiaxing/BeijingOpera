using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class CSVWriter : MonoBehaviour {
	public string fileName; //保存するファイル名

	private StreamWriter _streamWriter;

	void Start () {
		//テスト用
//	    WriteCSV ("Hello,World");
	}

	public void Init(int musicIndex = 0)
	{
		int subIndex = 1;
		FileInfo fileInfo = new FileInfo (Application.dataPath +"/"+ fileName + musicIndex + ".csv");
		while (fileInfo.Exists)
		{
			fileInfo = new FileInfo(Application.dataPath + "/" + fileName + musicIndex + "(" + subIndex++ + ").csv");
		}
		_streamWriter = fileInfo.AppendText ();
	}

	//CSVに書き込む処理
	public void WriteCSV(string txt){
		_streamWriter.WriteLine (txt);
		_streamWriter.Flush();
	}

	void OnDestroy()
	{
		_streamWriter.Close();
#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh();
#endif
	}
}
