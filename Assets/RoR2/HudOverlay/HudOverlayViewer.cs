using System;
using System.Collections.Generic;
using HG;
using HG.Collections.Generic;
using RoR2.UI;
using UnityEngine;

namespace RoR2.HudOverlay
{
	// Token: 0x02000BF1 RID: 3057
	public class HudOverlayViewer : MonoBehaviour
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x00120968 File Offset: 0x0011EB68
		// (set) Token: 0x06004558 RID: 17752 RVA: 0x00120970 File Offset: 0x0011EB70
		private CameraRigController cameraRigController
		{
			get
			{
				return this._cameraRigController;
			}
			set
			{
				if (this._cameraRigController == value)
				{
					return;
				}
				if (this._cameraRigController != null)
				{
					this.OnCameraRigControllerLost(this._cameraRigController);
				}
				this._cameraRigController = value;
				if (this._cameraRigController != null)
				{
					this.OnCameraRigControllerDiscovered(this._cameraRigController);
				}
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x001209AB File Offset: 0x0011EBAB
		// (set) Token: 0x0600455A RID: 17754 RVA: 0x001209B3 File Offset: 0x0011EBB3
		public GameObject target
		{
			get
			{
				return this._target;
			}
			private set
			{
				if (this._target == value)
				{
					return;
				}
				if (this._target != null)
				{
					this.OnTargetLost(this._target);
				}
				this._target = value;
				if (this._target != null)
				{
					this.OnTargetDiscovered(this._target);
				}
			}
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x001209EE File Offset: 0x0011EBEE
		private void OnEnable()
		{
			InstanceTracker.Add<HudOverlayViewer>(this);
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x001209F6 File Offset: 0x0011EBF6
		private void OnDisable()
		{
			InstanceTracker.Remove<HudOverlayViewer>(this);
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x00120A00 File Offset: 0x0011EC00
		private void OnDestroy()
		{
			this.target = null;
			this.cameraRigController = null;
			List<OverlayController> list = CollectionPool<OverlayController, List<OverlayController>>.RentCollection();
			this.SetOverlays(list);
			CollectionPool<OverlayController, List<OverlayController>>.ReturnCollection(list);
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x00120A30 File Offset: 0x0011EC30
		private void Update()
		{
			this.cameraRigController = (this.hud ? this.hud.cameraRigController : null);
			this.target = (this.cameraRigController ? this.cameraRigController.target : null);
			List<OverlayController> list = CollectionPool<OverlayController, List<OverlayController>>.RentCollection();
			HudOverlayManager.GetGlobalOverlayControllers(list);
			TargetTracker targetTracker = HudOverlayManager.GetTargetTracker(this.target);
			if (targetTracker != null)
			{
				targetTracker.GetOverlayControllers(list);
			}
			this.SetOverlays(list);
			CollectionPool<OverlayController, List<OverlayController>>.ReturnCollection(list);
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x00120AB0 File Offset: 0x0011ECB0
		private void OnCameraRigControllerDiscovered(CameraRigController cameraRigController)
		{
			this.target = cameraRigController.target;
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x00120ABE File Offset: 0x0011ECBE
		private void OnCameraRigControllerLost(CameraRigController cameraRigController)
		{
			this.target = null;
		}

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06004561 RID: 17761 RVA: 0x00120AC8 File Offset: 0x0011ECC8
		// (remove) Token: 0x06004562 RID: 17762 RVA: 0x00120B00 File Offset: 0x0011ED00
		public event Action<HudOverlayViewer, GameObject> onTargetDiscovered;

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06004563 RID: 17763 RVA: 0x00120B38 File Offset: 0x0011ED38
		// (remove) Token: 0x06004564 RID: 17764 RVA: 0x00120B70 File Offset: 0x0011ED70
		public event Action<HudOverlayViewer, GameObject> onTargetLost;

		// Token: 0x06004565 RID: 17765 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnTargetDiscovered(GameObject target)
		{
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnTargetLost(GameObject target)
		{
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x00120BA8 File Offset: 0x0011EDA8
		private void AddOverlay(OverlayController overlayController)
		{
			Transform transform = this.childLocator.FindChild(overlayController.creationParams.childLocatorEntry);
			if (!transform)
			{
				Debug.Log("Could not find parentTransform with name \"" + overlayController.creationParams.childLocatorEntry + "\"");
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(overlayController.creationParams.prefab, transform);
			this.overlayControllerToInstance[overlayController] = gameObject;
			overlayController.OnInstanceAdded(gameObject);
		}

		// Token: 0x06004568 RID: 17768 RVA: 0x00120C1C File Offset: 0x0011EE1C
		private void RemoveOverlay(OverlayController overlayController)
		{
			GameObject gameObject;
			if (this.overlayControllerToInstance.TryGetValue(overlayController, out gameObject))
			{
				this.overlayControllerToInstance.Remove(overlayController);
				overlayController.OnInstanceRemoved(gameObject);
				UnityEngine.Object.Destroy(gameObject);
			}
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x00120C54 File Offset: 0x0011EE54
		private void SetOverlays(List<OverlayController> newOverlayControllers)
		{
			List<OverlayController> list = CollectionPool<OverlayController, List<OverlayController>>.RentCollection();
			List<OverlayController> list2 = CollectionPool<OverlayController, List<OverlayController>>.RentCollection();
			for (int i = this.overlayControllerToInstance.Count - 1; i >= 0; i--)
			{
				OverlayController key = this.overlayControllerToInstance[i].Key;
				if (!newOverlayControllers.Contains(key))
				{
					list2.Add(key);
				}
			}
			for (int j = 0; j < newOverlayControllers.Count; j++)
			{
				OverlayController overlayController = newOverlayControllers[j];
				if (!this.overlayControllerToInstance.ContainsKey(overlayController))
				{
					list.Add(overlayController);
				}
			}
			foreach (OverlayController overlayController2 in list2)
			{
				this.RemoveOverlay(overlayController2);
			}
			foreach (OverlayController overlayController3 in list)
			{
				this.AddOverlay(overlayController3);
			}
			CollectionPool<OverlayController, List<OverlayController>>.ReturnCollection(list2);
			CollectionPool<OverlayController, List<OverlayController>>.ReturnCollection(list);
		}

		// Token: 0x04004398 RID: 17304
		public HUD hud;

		// Token: 0x04004399 RID: 17305
		public ChildLocator childLocator;

		// Token: 0x0400439A RID: 17306
		private CameraRigController _cameraRigController;

		// Token: 0x0400439B RID: 17307
		private GameObject _target;

		// Token: 0x0400439E RID: 17310
		private AssociationList<OverlayController, GameObject> overlayControllerToInstance = new AssociationList<OverlayController, GameObject>(-1, null, false);
	}
}
