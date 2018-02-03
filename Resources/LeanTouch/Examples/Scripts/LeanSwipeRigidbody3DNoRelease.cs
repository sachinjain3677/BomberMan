using UnityEngine;

namespace Lean.Touch
{
	// This script will push a rigidbody around when you swipe, without requiring you to release the mouse for it to register
	[RequireComponent(typeof(Rigidbody))]
	public class LeanSwipeRigidbody3DNoRelease : MonoBehaviour
	{
		[Tooltip("This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!")]
		public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;
		
		[Tooltip("This allows use to set how powerful the swipe will be")]
		public float ImpulseForce = 1.0f;

		// This stores the finger that's currently swiping this GameObject
		private LeanFinger swipingFinger;
		
		protected virtual void OnEnable()
		{
			// Hook into the events we need
			LeanTouch.OnFingerDown += OnFingerDown;
			LeanTouch.OnFingerSet  += OnFingerSet;
			LeanTouch.OnFingerUp   += OnFingerUp;
		}
		
		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerDown -= OnFingerDown;
			LeanTouch.OnFingerSet  -= OnFingerSet;
			LeanTouch.OnFingerUp   -= OnFingerUp;
		}

		public void OnFingerSet(LeanFinger finger)
		{
			// Is this the current finger?
			if (finger == swipingFinger)
			{
				// The scaled delta position magnitude required to register a swipe
				var swipeThreshold = LeanTouch.Instance.SwipeThreshold;

				// The amount of seconds we consider valid for a swipe
				var tapThreshold = LeanTouch.Instance.TapThreshold;
				
				// Get the scaled delta position between now, and 'swipeThreshold' seconds ago
				var recentDelta = finger.GetSnapshotScaledDelta(tapThreshold);

				// Has the finger recently swiped?
				if (recentDelta.magnitude > swipeThreshold)
				{
					// Get the rigidbody component
					var rigidbody = GetComponent<Rigidbody>();
					
					// Add force to the rigidbody based on the swipe force
					rigidbody.AddForce(recentDelta.normalized * ImpulseForce, ForceMode.Impulse);

					// Unset the finger so we don't continually add forces to it
					swipingFinger = null;
				}
			}
		}

		public void OnFingerDown(LeanFinger finger)
		{
			// Raycast information
			var ray = finger.GetRay();
			var hit = default(RaycastHit);
			
			// Was this finger pressed down on a collider?
			if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
			{
				// Was that collider this one?
				if (hit.collider.gameObject == gameObject)
				{
					// Set the current finger to this one
					swipingFinger = finger;
				}
			}
		}
		
		public void OnFingerUp(LeanFinger finger)
		{
			// Was the current finger lifted from the screen?
			if (finger == swipingFinger)
			{
				// Unset the current finger
				swipingFinger = null;
			}
		}
	}
}