using UnityEngine;

namespace Lean.Touch
{
	// This script can be used to spawn a GameObject via an event
	public class LeanSpawn : MonoBehaviour
	{
		[Tooltip("The prefab that gets spawned")]
		public Transform Prefab;

		[Tooltip("The distance from the finger the prefab will be spawned in world space")]
		public float Distance = 10.0f;

		public void Spawn()
		{
			if (Prefab != null)
			{
				Instantiate(Prefab, transform.position, transform.rotation);
			}
		}

		public void Spawn(LeanFinger finger)
		{
			if (Prefab != null && finger != null)
			{
				Instantiate(Prefab, finger.GetWorldPosition(Distance), transform.rotation);
			}
		}
	}
}