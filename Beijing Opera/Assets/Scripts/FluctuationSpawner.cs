using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluctuationSpawner : MonoBehaviour 
{
	public GameObject fluctuation;
	public Transform fluctuationPoint;
	public Transform greyLinePoint;

	private Music music;
	private AudioSource audioSource;
	private float bmp;
	private float fluctuationSpeed;
	private float rhythmicPointTime;
	private float distance;
	public float reachGreyTime { get; private set; }

    private float startTime;
    private float endTime;

	private bool startGenerate = false;
	private float timer = 0;

//    private List<float> rhythmicPointList;

    private int nextPointIndex = 0;
	private Dictionary<int, GameObject> _noteObjs = new Dictionary<int, GameObject>();

	// Use this for initialization
	void Awake () 
	{
		distance = greyLinePoint.position.x - fluctuationPoint.position.x;
		fluctuationSpeed = GameManager.Instance.fluctuationSpeed;
		reachGreyTime = distance / fluctuationSpeed;
	}

    public void Load(float _bmp, float _startTime, float _endTime)
    {
        music = GameManager.Instance.music;
        audioSource = music.GetComponent<AudioSource>();
		_noteObjs.Clear();
		removeLines.Clear();
//        bmp = _bmp;
//        startTime = _startTime;
//        endTime = _endTime;
//        rhythmicPointTime = 60 / bmp;
//        timer = rhythmicPointTime + 0.1f;
//        InitrhythmicPointList();
//        Debug.Log(rhythmicPointTime);
    }


//    private void InitrhythmicPointList()
//    {
//        rhythmicPointList = new List<float>();
//        rhythmicPointList.Add(startTime-reachGreyTime);
//        while(rhythmicPointList[rhythmicPointList.Count-1]<(endTime - (reachGreyTime / rhythmicPointTime) * rhythmicPointTime))
//        {
//            rhythmicPointList.Add(rhythmicPointList[rhythmicPointList.Count - 1] + rhythmicPointTime);
//        }
//    }


	// Update is called once per frame
	void FixedUpdate()
	{
		if(startGenerate)
		{
			

			if (audioSource.time < (music.startTime - reachGreyTime))
			{
				return;
			}
			//if (audioSource.time >= (music.startTime - reachGreyTime)&&audioSource.time<=(music.endTime-(reachGreyTime/rhythmicPointTime)*rhythmicPointTime) )
			//{

			//	timer += Time.fixedDeltaTime;

			//	if (audioSource.isPlaying && timer >= rhythmicPointTime)
			//	{
			//		GameObject go3 = Instantiate(fluctuation, fluctuationPoint.position, Quaternion.identity);
			//		go3.GetComponent<Fluctuation>().Init(fluctuationSpeed);
			//		timer = 0;
			//	}

			//}


//            if (audioSource.time >= (music.startTime - reachGreyTime) /*&& audioSource.time <= (music.endTime - (reachGreyTime / rhythmicPointTime) * rhythmicPointTime)*/)
//            {
//                if (nextPointIndex >= rhythmicPointList.Count-1) return;
//                if (audioSource.isPlaying && audioSource.time>rhythmicPointList[nextPointIndex])
//                {
//                    GameObject go3 = Instantiate(fluctuation, fluctuationPoint.position, Quaternion.identity);
//                    go3.GetComponent<Fluctuation>().Init(fluctuationSpeed);
//                    nextPointIndex++;
//                }
//
//            }

			foreach (var keyValue in removeLines)
			{
				Debug.DrawLine(
					new Vector3(keyValue.Key, -1, 0),
					new Vector3(keyValue.Key, 1, 0),
					keyValue.Value
				);
			}
		}
	}

	public void SpawnFluctuation(int index, float deltaTime)
	{
		Vector3 pos = fluctuationPoint.position;
		pos.x += deltaTime * fluctuationSpeed;
		GameObject go3 = Instantiate(fluctuation, pos, Quaternion.identity);
		go3.GetComponent<Fluctuation>().Init(index, fluctuationSpeed);
		_noteObjs.Add(index, go3);
		nextPointIndex++;
	}

	Dictionary<float, Color> removeLines = new Dictionary<float, Color>();

	public void RemoveFluctuation(int index, Color color = default(Color))
	{
		if (_noteObjs.ContainsKey(index))
		{
			removeLines[_noteObjs[index].transform.position.x] = color;
			GameObject.Destroy(_noteObjs[index]);
			_noteObjs.Remove(index);
		}
	}

	public void GenerateFluctuation()
	{
		startGenerate = true;
	}

	public void Stop()
	{
		startGenerate = false;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine(new Vector3(greyLinePoint.position.x, -5, 0), new Vector3(greyLinePoint.position.x, 5, 0));
	}
}
