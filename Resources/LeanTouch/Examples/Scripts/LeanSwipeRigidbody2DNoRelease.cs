using UnityEngine;

namespace Lean.Touch
{
	// This script will push a rigidbody around when you swipe, without requiring you to release the mouse for it to register
	[RequireComponent(typeof(Rigidbody2D))]
	public class LeanSwipeRigidbody2DNoRelease : MonoBehaviour
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
				var recentDelta = finger.GetSnapshotScreenDelta(tapThreshold);

				// Has the finger recently swiped?
				if (recentDelta.magnitude > swipeThreshold)
				{
					// Get the rigidbody component
					var rigidbody = GetComponent<Rigidbody2D>();
					
					// Add force to the rigidbody based on the swipe force
					rigidbody.AddForce(recentDelta.normalized * ImpulseForce, ForceMode2D.Impulse);

					// Unset the finger so we don't continually add forces to it
					swipingFinger = null;
				}
			}
		}

		public void OnFingerDown(LeanFinger finger)
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