using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour 
{
    public List<MusicData> musicDataList;

    public float offsetTime;
	[Range(0.5f, 10f)]
	public float fluctuationSpeed = 5f;
	[Range(0.01f, 0.25f)]
	public float coolTime = 0.05f;
	[Range(0.05f, 0.5f)]
	public float goodTime = 0.15f;

    public Music music;
	public ProgressBar progressBar;
	public FluctuationSpawner fluctuationSpawner;
	public GameObject fluctuation;
	public Transform fluctuationPoint;
	public Transform greyLinePoint;

	public GameObject menu;
	public GameObject gameOver;
    public UnityEngine.UI.Text scoreText;

	private int score;
    private int missCount;

	private AudioSource audioSource;

    private MusicData currentMusicData;


    private static GameManager _instance;

	public static GameManager Instance
	{
		get
		{
			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;
		audioSource = music.GetComponent<AudioSource>();
	}


	void Start () 
	{
//		if (fluctuationSpeed < 5) fluctuationSpeed = 5;

		menu.SetActive(true);
		gameOver.SetActive(false);

	}

    private float timer;

	void Update () 
	{
        timer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(timer);
            timer = 0;
        }
	}

	public void StartGame()
	{
        if (currentMusicData == null) return;

        audioSource.clip = currentMusicData.clip;
        music.Set(currentMusicData.bmp, currentMusicData.startTime, currentMusicData.endTime);
        fluctuationSpawner.Load(currentMusicData.bmp, currentMusicData.startTime, currentMusicData.endTime);
		fluctuationSpawner.GenerateFluctuation();
		audioSource.Play();
		menu.SetActive(false);

	}

    public void SetCurrentMusic(int index)
    {
        currentMusicData = musicDataList[index];
    }


	public void Miss()
	{
        missCount++;
	}

    public void AddScore()
	{
        score++;
	}


	public void GameOver()
	{
		music.Stop();
		fluctuationSpawner.Stop();
		gameOver.SetActive(true);
        scoreText.text = (((float)score / (float)(missCount + score)) * 100).ToString("F");
	}

	public void ReLoad()
	{
		SceneManager.LoadScene(0);
	}

	public void Quit()
	{
		Application.Quit();
	}

}
