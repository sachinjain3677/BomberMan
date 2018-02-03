using UnityEngine;

namespace Lean.Touch
{
	// This script makes handling selectable actions easier
	[RequireComponent(typeof(LeanSelectable))]
	public abstract class LeanSelectableBehaviour : MonoBehaviour
	{
		[System.NonSerialized]
		private LeanSelectable selectable;

		public LeanSelectable Selectable
		{
			get
			{
				if (selectable == null)
				{
					selectable = GetComponent<LeanSelectable>();
				}

				return selectable;
			}
		}

		protected virtual void OnEnable()
		{
			if (selectable == null)
			{
				selectable = GetComponent<LeanSelectable>();
			}

			// Hook LeanSelectable events
			selectable.OnSelect.AddListener(OnSelect);
			selectable.OnSelectUp.AddListener(OnSelectUp);
			selectable.OnDeselect.AddListener(OnDeselect);
		}

		protected virtual void OnDisable()
		{
			if (selectable == null)
			{
				selectable = GetComponent<LeanSelectable>();
			}

			// Unhook LeanSelectable events
			selectable.OnSelect.RemoveListener(OnSelect);
			selectable.OnSelectUp.RemoveListener(OnSelectUp);
			selectable.OnDeselect.RemoveListener(OnDeselect);
		}
		
		// Called when selection begins (finger = the finger that selected this)
		protected virtual void OnSelect(LeanFinger finger)
		{
		}
		
		// Called when the selecting finger goes up (finger = the finger that selected this)
		protected virtual void OnSelectUp(LeanFinger finger)
		{
		}

		// Called when this is deselected, if OnSelectUp hasn't been called yet, it will get called first
		protected virtual void OnDeselect()
		{
		}
	}
}