using UnityEngine;
using UnityEngine.UI;

namespace Lean.Touch
{
	// This script will tell you which direction you swiped in
	public class LeanSwipeDirection4 : MonoBehaviour
	{
		[Tooltip("The text element we will display the swipe information in")]
		public Text InfoText;
	
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
			// Make sure the info text exists
			if (InfoText != null)
			{
				// Store the swipe delta in a temp variable
				var swipe = finger.SwipeScreenDelta;
			
				if (swipe.x < -Mathf.Abs(swipe.y))
				{
					InfoText.text = "You swiped left!";
				}
			
				if (swipe.x > Mathf.Abs(swipe.y))
				{
					InfoText.text = "You swiped right!";
				}
			
				if (swipe.y < -Mathf.Abs(swipe.x))
				{
					InfoText.text = "You swiped down!";
				}
			
				if (swipe.y > Mathf.Abs(swipe.x))
				{
					InfoText.text = "You swiped up!";
				}
			}
		}
	}
}