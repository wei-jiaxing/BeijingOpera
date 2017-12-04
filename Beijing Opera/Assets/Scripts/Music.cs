using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [HideInInspector]
	public float bmp;
    [HideInInspector]
	public float offsetTime;
    [HideInInspector]
	public float startTime;
    [HideInInspector]
	public float endTime;

	private float rhythmicPointTime;//seconds

	private float timer;

    private int score;
	public Text scoreText;

	public Button button;
    public Sprite normal;
    public Sprite press;
    private Image image;

	private bool push;
	private bool checking = false;
	bool pushed = false;
	bool _checked = false;
	bool missed = false;

    private bool set = false;

	private AudioSource audioSource;

	private FluctuationSpawner _spawner;

	private List<float> _noteTimeList = new List<float>();
	private float _startShowTime;
	private int _showNoteIndex = 0;

	private int _hitNoteIndex = 0;

	private static Music _instance;
	public static Music Instance
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

	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
        offsetTime = GameManager.Instance.offsetTime;
		_spawner = GameManager.Instance.fluctuationSpawner;
		
        image = button.GetComponent<Image>();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            push = true;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            image.sprite = press;
        }
        else
        {
            image.sprite = normal;
        }
    }

	void FixedUpdate()
	{
        if (!set) return;

        GameManager.Instance.progressBar.SetProgress(audioSource.time / endTime);

//		if (audioSource.time < startTime) return;

		if (audioSource.time > endTime)
		//if (audioSource.time >= audioSource.clip.length)
		{
            GameManager.Instance.GameOver();
			return;
		}

		if (_showNoteIndex < _noteTimeList.Count && audioSource.time + _startShowTime >= _noteTimeList[_showNoteIndex])
		{
			Debug.LogWarning("Spawn: " + _showNoteIndex + " |at time: " + audioSource.time);

			GenerateFluction(_showNoteIndex);
		}


		timer += Time.fixedDeltaTime;

		if (_hitNoteIndex < _noteTimeList.Count && 
			// 一番目のNoteから判断が始まる
			audioSource.time - _noteTimeList[0] > GameManager.Instance.goodTime)
		{
			float deltaTime = Mathf.Abs(audioSource.time - _noteTimeList[_hitNoteIndex]);
			if (deltaTime <= GameManager.Instance.goodTime)
			{
				checking = true;
				missed = false;
				if (push)
				{
					Debug.Log("Delta time: " + deltaTime);
					_spawner.RemoveFluctuation(_hitNoteIndex);
					AddScore( (deltaTime <= GameManager.Instance.coolTime));
				}
			}
			else
			{
				checking = false;
				if (push&&!missed)
				{
					Miss();
					Debug.Log("Miss!!!Delta time: " + deltaTime);
					missed = true;
				}
			}


			if (checking)
			{
				if (push)
				{
					pushed = true;
				}
				_checked = false;
			}
			else if (!_checked)
			{
				if (!pushed&!missed)
				{
					Miss();
					missed = true;
				}
				pushed = false;
				_checked = true;
			}
		}

		push = false;
	}

    public void Set(float _bmp,float _startTime, float _endTime)
    {
        bmp = _bmp;
        startTime = _startTime;
        endTime = _endTime;

        rhythmicPointTime = 60 / bmp;

        set = true;

		_noteTimeList.Clear();
		_noteTimeList.AddRange(CSVManager.Instance.LoadCSV());
		_showNoteIndex = _hitNoteIndex = 0;

		_startShowTime = _spawner.reachGreyTime;
    }

	void GenerateFluction(int index)
	{
		_spawner.SpawnFluctuation(index);
		_showNoteIndex++;
	}

	public void AddScore(bool coolOrGood)
	{
		score++;
		scoreText.text = score+" Combo" + (coolOrGood? " <color=blue>Cool</color>" : " <color=green>Good</color>");
		_hitNoteIndex++;
		GameManager.Instance.AddScore();
	}

	public void Miss()
	{
		score = 0;
		scoreText.text = "Miss";
		_hitNoteIndex++;
		GameManager.Instance.Miss();
	}

	public void OnButtonClick()
	{
		push = true;
	}

	public void Stop()
	{
		audioSource.Stop();
	}


}
