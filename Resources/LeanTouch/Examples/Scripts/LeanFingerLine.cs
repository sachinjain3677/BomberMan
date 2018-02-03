#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4
	#define UNITY_OLD_LINE_RENDERER
#endif
using UnityEngine;

namespace Lean.Touch
{
	// This script will draw a line between the start and current finger points
	public class LeanFingerLine : LeanFingerTrail
	{
		protected override void WritePositions(LineRenderer line, LeanFinger finger)
		{
			// Get start and current world position of finger
			var start = finger.GetStartWorldPosition(Distance);
			var end   = finger.GetWorldPosition(Distance);

			// Write positions
#if UNITY_OLD_LINE_RENDERER
			line.SetVertexCount(2);
#else
			line.positionCount = 2;
#endif

			line.SetPosition(0, start);
			line.SetPosition(1, end);
		}
	}
}