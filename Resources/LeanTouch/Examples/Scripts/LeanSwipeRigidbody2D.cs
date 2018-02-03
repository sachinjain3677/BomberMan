using UnityEngine;

namespace Lean.Touch
{
	// This script will push a rigidbody around when you swipe
	[RequireComponent(typeof(Rigidbody2D))]
	public class LeanSwipeRigidbody2D : MonoBehaviour
	{
		// This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)
		public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;
		
		// This allows use to set how powerful the swipe will be
		public float ForceMultiplier = 1.0f;
		
		protected virtual void OnEnable()
		{
			// Hook into the events we need
			LeanTouch.OnFingerSwipe += OnFingerSwipe;
		}
		
		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerSwipe -= OnFingerSwipe;
		}
		
		public void OnFingerSwipe(LeanFinger finger)
		{
			// Find the position under the current finger
			var ray = finger.GetStartWorldPosition(1.0f);

			// Find the collider at this position
			var hit = Physics2D.OverlapPoint(ray, LayerMask);
			
			// Was a collider found?
			if (hit != null)
			{
				// Get the rigidbody component
				var rigidbody = hit.attachedRigidbody;

				// Is the rigidbody attached to this GameObject?
				if (rigidbody.gameObject == gameObject)
				{
					// Add force to the rigidbody based on the swipe force
					rigidbody.AddForce(finger.SwipeScaledDelta * ForceMultiplier);
				}
			}
		}
	}
}