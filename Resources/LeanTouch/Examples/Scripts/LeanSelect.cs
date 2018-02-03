using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to select LeanSelectable components
	public class LeanSelect : MonoBehaviour
	{
		public enum SelectType
		{
			Raycast3D,
			Overlap2D
		}

		public enum SearchType
		{
			GetComponent,
			GetComponentInParent,
			GetComponentInChildren
		}
		
		public enum ReselectType
		{
			KeepSelected,
			Deselect,
			DeselectAndSelect,
			SelectAgain
		}

		public SelectType SelectUsing;

		[Tooltip("This stores the layers we want the raycast/overlap to hit (make sure this GameObject's layer is included!)")]
		public LayerMask LayerMask = Physics.DefaultRaycastLayers;

		[Tooltip("How should the selected GameObject be searched for the LeanSelectable component?")]
		public SearchType Search;

		[Tooltip("The currently selected LeanSelectable")]
		public LeanSelectable CurrentSelectable;

		[Tooltip("If you select an already selected selectable, what should happen?")]
		public ReselectType Reselect;
		
		[Tooltip("Automatically deselect the CurrentSelectable if Select gets called with null?")]
		public bool AutoDeselect;
		
		// NOTE: This must be called from somewhere
		public void Select(LeanFinger finger)
		{
			// Stores the component we hit (Collider or Collider2D)
			var component = default(Component);

			switch (SelectUsing)
			{
				case SelectType.Raycast3D:
				{
					// Get ray for finger
					var ray = finger.GetRay();

					// Stores the raycast hit info
					var hit = default(RaycastHit);
					
					// Was this finger pressed down on a collider?
					if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
					{
						component = hit.collider;
					}
				}
				break;
				
				case SelectType.Overlap2D:
				{
					// Find the position under the current finger
					var point = finger.GetWorldPosition(1.0f);

					// Find the collider at this position
					component = Physics2D.OverlapPoint(point, LayerMask);
				}
				break;
			}

			// Select the component
			Select(finger, component);
		}

		public void Select(LeanFinger finger, Component component)
		{
			// Stores the selectable we will search for
			var selectable = default(LeanSelectable);

			// Was a collider found?
			if (component != null)
			{
				switch (Search)
				{
					case SearchType.GetComponent:           selectable = component.GetComponent          <LeanSelectable>(); break;
					case SearchType.GetComponentInParent:   selectable = component.GetComponentInParent  <LeanSelectable>(); break;
					case SearchType.GetComponentInChildren: selectable = component.GetComponentInChildren<LeanSelectable>(); break;
				}
			}

			// Select the selectable
			Select(finger, selectable);
		}

		public void Select(LeanFinger finger, LeanSelectable selectable)
		{
			// Something was selected?
			if (selectable != null)
			{
				// Did we select a new LeanSelectable?
				if (selectable != CurrentSelectable)
				{
					// Deselect the current
					Deselect();

					// Change current
					CurrentSelectable = selectable;

					// Call select event on current
					CurrentSelectable.Select(finger);
				}
				// Did we reselect the current LeanSelectable?
				else
				{
					switch (Reselect)
					{
						case ReselectType.Deselect:
						{
							Deselect();
						}
						break;

						case ReselectType.DeselectAndSelect:
						{
							// Deselect the current
							Deselect();

							// Change current
							CurrentSelectable = selectable;

							// Call select event on current
							CurrentSelectable.Select(finger);
						}
						break;

						case ReselectType.SelectAgain:
						{
							// Call select event on current
							CurrentSelectable.Select(finger);
						}
						break;
					}
				}
			}
			// Nothing was selected?
			else
			{
				// Deselect?
				if (AutoDeselect == true)
				{
					Deselect();
				}
			}
		}
		
		[ContextMenu("Deselect")]
		public void Deselect()
		{
			// Is there a selected object?
			if (CurrentSelectable != null)
			{
				// Deselect it
				CurrentSelectable.Deselect();

				// Mark it null
				CurrentSelectable = null;
			}
		}

		public void Deselect(LeanFinger finger)
		{
			// Is there a selected object?
			if (CurrentSelectable != null)
			{
				// Does its finger match?
				// NOTE: This will usually only work with OnFingerDown selection, and OnFingerUp deselection
				if (CurrentSelectable.SelectingFinger == finger)
				{
					// Deselect it
					CurrentSelectable.Deselect();

					// Mark it null
					CurrentSelectable = null;
				}
			}
		}
	}
}