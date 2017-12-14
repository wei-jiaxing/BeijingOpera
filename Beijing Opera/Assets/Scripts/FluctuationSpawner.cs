using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluctuationSpawner : MonoBehaviour 
{
	public GameObject fluctuation;
	public Transform fluctuationPoint;
	public Transform greyLinePoint;

	private float bmp;
	private float fluctuationSpeed;
	private float rhythmicPointTime;
	public float distance;
	public float reachGreyTime { get; private set; }

    private float startTime;
    private float endTime;

	private bool startGenerate = false;

//    private List<float> rhythmicPointList;

    private int nextPointIndex = 0;
	private Dictionary<int, GameObject> _noteObjs = new Dictionary<int, GameObject>();

	// Use this for initialization
	void Awake () 
	{
		distance = greyLinePoint.position.x - fluctuationPoint.position.x;
	}

    public void Load(float _bmp, float _startTime, float _endTime)
    {
		_noteObjs.Clear();
		_removeLines.Clear();
		fluctuationSpeed = GameManager.Instance.FluctuationSpeed;
		Debug.Log(fluctuationSpeed);
		bmp = _bmp;
//        startTime = _startTime;
//        endTime = _endTime;
//        rhythmicPointTime = 60 / bmp;
//        Debug.Log(rhythmicPointTime);
    }

	// Update is called once per frame
	void FixedUpdate()
	{
		// 判定範囲が正しいか確認用
		foreach (var keyValue in _removeLines)
		{
			Debug.DrawLine(
				new Vector3(keyValue.Key, -1, 0),
				new Vector3(keyValue.Key, 1, 0),
				keyValue.Value
			);
		}
	}

	public void SpawnFluctuation(int index, float bpm, float deltaTime)
	{
		if (startGenerate)
		{
			Vector3 pos = fluctuationPoint.position;
			pos.x += deltaTime * fluctuationSpeed;
			GameObject go3 = Instantiate(fluctuation, pos, Quaternion.identity);
			go3.GetComponent<Fluctuation>().Init(index, fluctuationSpeed);
			_noteObjs.Add(index, go3);
			nextPointIndex++;
		}
	}

	Dictionary<float, Color> _removeLines = new Dictionary<float, Color>();

	public void RemoveFluctuation(int index, Color color = default(Color))
	{
		if (_noteObjs.ContainsKey(index))
		{
			_removeLines[_noteObjs[index].transform.position.x] = color;
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
