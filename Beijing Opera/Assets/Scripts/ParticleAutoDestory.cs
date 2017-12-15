using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 実際ObjectPoolの方がいいけど、パフォーマンスの問題なさそうなのでこのまま
/// </summary>
public class ParticleAutoDestory : MonoBehaviour {

	public ParticleSystem _particle;

	void Update()
	{
		if (_particle.isStopped)
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}
