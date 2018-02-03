using UnityEngine;
using UnityEngine.Events;

namespace Lean.Touch
{
	// This script allows you to select this GameObject via another component
	public class LeanSelectable : MonoBehaviour
	{
		// Event signature
		[System.Serializable] public class LeanFingerEvent : UnityEvent<LeanFinger> {}

		[Tooltip("Is this GameObject currently selected? NOTE: Don't modify this from the inspector")]
		public bool IsSelected;

		// This stores the finger that last began selection of this LeanSelectable
		// NOTE: This finger is only valid while the finger is 
		[HideInInspector]
		public LeanFinger SelectingFinger;
		
		// Called when selection begins (finger = the finger that selected this)
		public LeanFingerEvent OnSelect;

		// Called when the selecting finger goes up (finger = the finger that selected this)
		public LeanFingerEvent OnSelectUp;

		// Called when this is deselected, if OnSelectUp hasn't been called yet, it will get called first
		public UnityEvent OnDeselect;
		
		[ContextMenu("Select")]
		public void Select()
		{
			Select(null);
		}

		public void Select(LeanFinger finger)
		{
			IsSelected      = true;
			SelectingFinger = finger;

			OnSelect.Invoke(finger);
		}

		[ContextMenu("Deselect")]
		public void Deselect()
		{
			if (SelectingFinger != null)
			{
				OnSelectUp.Invoke(SelectingFinger);

				SelectingFinger = null;
			}

			IsSelected = false;

			OnDeselect.Invoke();
		}

		protected virtual void OnEnable()
		{
			// Hook events
			LeanTouch.OnFingerUp += OnFingerUp;
		}

		protected virtual void OnDisable()
		{
			// Unhook events
			LeanTouch.OnFingerUp -= OnFingerUp;
		}

		private void OnFingerUp(LeanFinger finger)
		{
			// If the finger went up, it's no longer selecting anything
			if (finger == SelectingFinger)
			{
				OnSelectUp.Invoke(SelectingFinger);

				SelectingFinger = null;
			}
		}
	}
}