using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluctuation : MonoBehaviour 
{
	private float speed;



	void Start () 
	{
		
	}
	

	void FixedUpdate () 
	{
		transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
	}

	public void Init(float speed)
	{
		this.speed = speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(gameObject);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(new Vector3(transform.position.x, -1, 0), new Vector3(transform.position.x, 1, 0));
	}
}
