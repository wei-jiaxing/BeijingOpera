﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesTimingMaker : MonoBehaviour {
	
	private AudioSource _audioSource;
	private float _startTime = 0;
	private CSVWriter _CSVWriter;

	private bool _isPlaying = false;
	public GameObject startButton;

	// Use this for initialization
	void Start () {
		_audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource> ();
		_CSVWriter = GameObject.Find ("CSVWriter").GetComponent<CSVWriter> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_isPlaying) {
			DetectKeys ();
		}
	}
		
	public void StartMusic(){
		startButton.SetActive (false);
		_audioSource.Play ();
		_startTime = Time.time;
		_isPlaying = true;
		_CSVWriter.Init();
	}
	void DetectKeys(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			WriteNotesTiming (1);
		}
	}
	void WriteNotesTiming(int num){
		Debug.Log (GetTiming ());
		_CSVWriter.WriteCSV (GetTiming ().ToString () + "," + num.ToString());
	}

	float GetTiming(){
		return Time.time - _startTime;
	}
}

