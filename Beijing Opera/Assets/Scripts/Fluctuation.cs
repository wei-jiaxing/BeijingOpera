using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluctuation : MonoBehaviour 
{
	private int index;
	private float speed;

	void FixedUpdate () 
	{
		transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
	}

	public void Init(int index, float speed)
	{
		this.index = index;
		this.speed = speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		GameManager.Instance.fluctuationSpawner.RemoveFluctuation(index);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(new Vector3(transform.position.x, -1, 0), new Vector3(transform.position.x, 1, 0));
	}
}
