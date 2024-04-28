using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RoR2
{
	// Token: 0x020008DC RID: 2268
	[RequireComponent(typeof(Camera))]
	public class UICamera : MonoBehaviour
	{
		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x060032D9 RID: 13017 RVA: 0x000D6820 File Offset: 0x000D4A20
		// (remove) Token: 0x060032DA RID: 13018 RVA: 0x000D6854 File Offset: 0x000D4A54
		public static event UICamera.UICameraDelegate onUICameraPreCull;

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x060032DB RID: 13019 RVA: 0x000D6888 File Offset: 0x000D4A88
		// (remove) Token: 0x060032DC RID: 13020 RVA: 0x000D68BC File Offset: 0x000D4ABC
		public static event UICamera.UICameraDelegate onUICameraPreRender;

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x060032DD RID: 13021 RVA: 0x000D68F0 File Offset: 0x000D4AF0
		// (remove) Token: 0x060032DE RID: 13022 RVA: 0x000D6924 File Offset: 0x000D4B24
		public static event UICamera.UICameraDelegate onUICameraPostRender;

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000D6957 File Offset: 0x000D4B57
		// (set) Token: 0x060032E0 RID: 13024 RVA: 0x000D695F File Offset: 0x000D4B5F
		public Camera camera { get; private set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000D6968 File Offset: 0x000D4B68
		// (set) Token: 0x060032E2 RID: 13026 RVA: 0x000D6970 File Offset: 0x000D4B70
		public CameraRigController cameraRigController { get; private set; }

		// Token: 0x060032E3 RID: 13027 RVA: 0x000D6979 File Offset: 0x000D4B79
		private void Awake()
		{
			this.camera = base.GetComponent<Camera>();
			this.cameraRigController = base.GetComponentInParent<CameraRigController>();
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000D6993 File Offset: 0x000D4B93
		private void OnEnable()
		{
			UICamera.instancesList.Add(this);
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000D69A0 File Offset: 0x000D4BA0
		private void OnDisable()
		{
			UICamera.instancesList.Remove(this);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000D69AE File Offset: 0x000D4BAE
		private void OnPreCull()
		{
			if (UICamera.onUICameraPreCull != null)
			{
				UICamera.onUICameraPreCull(this);
			}
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000D69C2 File Offset: 0x000D4BC2
		private void OnPreRender()
		{
			if (UICamera.onUICameraPreRender != null)
			{
				UICamera.onUICameraPreRender(this);
			}
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000D69D6 File Offset: 0x000D4BD6
		private void OnPostRender()
		{
			if (UICamera.onUICameraPostRender != null)
			{
				UICamera.onUICameraPostRender(this);
			}
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000D69EA File Offset: 0x000D4BEA
		public EventSystem GetAssociatedEventSystem()
		{
			if (this.cameraRigController.viewer && this.cameraRigController.viewer.localUser != null)
			{
				return this.cameraRigController.viewer.localUser.eventSystem;
			}
			return null;
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000D6A28 File Offset: 0x000D4C28
		public static UICamera FindViewerUICamera(LocalUser localUserViewer)
		{
			if (localUserViewer != null)
			{
				for (int i = 0; i < UICamera.instancesList.Count; i++)
				{
					if (UICamera.instancesList[i].cameraRigController.viewer.localUser == localUserViewer)
					{
						return UICamera.instancesList[i];
					}
				}
			}
			return null;
		}

		// Token: 0x040033FE RID: 13310
		private static readonly List<UICamera> instancesList = new List<UICamera>();

		// Token: 0x040033FF RID: 13311
		public static readonly ReadOnlyCollection<UICamera> readOnlyInstancesList = new ReadOnlyCollection<UICamera>(UICamera.instancesList);

		// Token: 0x020008DD RID: 2269
		// (Invoke) Token: 0x060032EE RID: 13038
		public delegate void UICameraDelegate(UICamera sceneCamera);
	}
}
