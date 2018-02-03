using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to transform the current GameObject with inertia
	[RequireComponent(typeof(Rigidbody2D))]
	public class LeanSelectableTranslateInertia2D : LeanSelectableBehaviour
	{
		protected virtual void Update()
		{
			if (Selectable.IsSelected == true)
			{
				// Screen position of the transform
				var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
			
				// Add the deltaPosition
				screenPosition += (Vector3)LeanGesture.GetScreenDelta();
			
				// Convert back to world space
				transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
			}
		}

		protected override void OnSelectUp(LeanFinger finger)
		{
			// Get the Rigidbody2D atached to this GameObject
			var body = GetComponent<Rigidbody2D>();

			// Convert this GameObject's world position into screen coordinates and store it in a temp variable
			var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
				
			// Modify screen position by the finger's delta screen position over the past 0.1 seconds
			screenPosition += (Vector3)finger.GetSnapshotScreenDelta(0.1f);
				
			// Convert the screen position into world coordinates and subtract it by the old position to find the world delta over the past 0.1 seconds
			var worldDelta = Camera.main.ScreenToWorldPoint(screenPosition) - transform.position;
				
			// Set the velocity and divide it by 0.1, because velocity is applied over 1 second, and our delta is currently only for 0.1 second
			body.velocity = worldDelta / 0.1f;
		}
	}
}