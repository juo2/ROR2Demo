using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2.HudOverlay
{
	// Token: 0x02000BF0 RID: 3056
	public static class HudOverlayManager
	{
		// Token: 0x0600454D RID: 17741 RVA: 0x001207EC File Offset: 0x0011E9EC
		public static OverlayController AddOverlay(GameObject target, OverlayCreationParams overlayCreationParams)
		{
			if (target != null)
			{
				TargetTracker andIncrementTargetTracker = HudOverlayManager.GetAndIncrementTargetTracker(target);
				OverlayController overlayController = new OverlayController(andIncrementTargetTracker, overlayCreationParams);
				andIncrementTargetTracker.AddOverlay(overlayController);
				return overlayController;
			}
			Debug.LogError("AddOverlay can't be called with no target--did you mean to use AddGlobalOverlay?");
			return null;
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x00120824 File Offset: 0x0011EA24
		public static void RemoveOverlay(OverlayController overlayController)
		{
			TargetTracker owner = overlayController.owner;
			if (owner != null)
			{
				owner.RemoveOverlay(overlayController);
				HudOverlayManager.DecrementTargetTracker(owner);
				return;
			}
			Debug.LogError("RemoveOverlay can't be called on an OverlayController with no target--did you mean to use RemoveGlobalOverlay?");
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x00120854 File Offset: 0x0011EA54
		public static OverlayController AddGlobalOverlay(OverlayCreationParams overlayCreationParams)
		{
			OverlayController overlayController = new OverlayController(null, overlayCreationParams);
			HudOverlayManager.globalOverlays.Add(overlayController);
			return overlayController;
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x00120875 File Offset: 0x0011EA75
		public static void RemoveGlobalOverlay(OverlayController overlayController)
		{
			if (overlayController.owner == null)
			{
				HudOverlayManager.globalOverlays.Remove(overlayController);
				return;
			}
			Debug.LogError("RemoveGlobalOverlay can't be called on an OverlayController with a target--did you mean to use RemoveOverlay?");
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x00120896 File Offset: 0x0011EA96
		public static void GetGlobalOverlayControllers(List<OverlayController> dest)
		{
			ListUtils.AddRange<OverlayController, List<OverlayController>>(dest, HudOverlayManager.globalOverlays);
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x001208A4 File Offset: 0x0011EAA4
		private static TargetTracker GetAndIncrementTargetTracker(GameObject target)
		{
			TargetTracker targetTracker;
			if (!HudOverlayManager.targetToTargetTracker.TryGetValue(target, out targetTracker))
			{
				targetTracker = HudOverlayManager.CreateTargetTracker(target);
			}
			targetTracker.refCount++;
			return targetTracker;
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x001208D6 File Offset: 0x0011EAD6
		private static void DecrementTargetTracker(TargetTracker targetTracker)
		{
			targetTracker.refCount--;
			if (targetTracker.refCount <= 0)
			{
				HudOverlayManager.targetToTargetTracker.Remove(targetTracker.target);
				targetTracker.Dispose();
			}
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x00120908 File Offset: 0x0011EB08
		private static TargetTracker CreateTargetTracker(GameObject target)
		{
			TargetTracker targetTracker = new TargetTracker
			{
				target = target
			};
			HudOverlayManager.targetToTargetTracker.Add(target, targetTracker);
			return targetTracker;
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x00120930 File Offset: 0x0011EB30
		public static TargetTracker GetTargetTracker(GameObject target)
		{
			TargetTracker result;
			if (target != null && HudOverlayManager.targetToTargetTracker.TryGetValue(target, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04004396 RID: 17302
		private static Dictionary<GameObject, TargetTracker> targetToTargetTracker = new Dictionary<GameObject, TargetTracker>();

		// Token: 0x04004397 RID: 17303
		private static List<OverlayController> globalOverlays = new List<OverlayController>();
	}
}
