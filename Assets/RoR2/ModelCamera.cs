using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007B4 RID: 1972
	[RequireComponent(typeof(Camera))]
	public class ModelCamera : MonoBehaviour
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000B4503 File Offset: 0x000B2703
		// (set) Token: 0x060029B8 RID: 10680 RVA: 0x000B450A File Offset: 0x000B270A
		public static ModelCamera instance { get; private set; }

		// Token: 0x060029B9 RID: 10681 RVA: 0x000B4512 File Offset: 0x000B2712
		private void OnEnable()
		{
			if (ModelCamera.instance && ModelCamera.instance != this)
			{
				Debug.LogErrorFormat("Only one {0} instance can be active at a time.", new object[]
				{
					base.GetType().Name
				});
				return;
			}
			ModelCamera.instance = this;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000B4552 File Offset: 0x000B2752
		private void OnDisable()
		{
			if (ModelCamera.instance == this)
			{
				ModelCamera.instance = null;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000B4567 File Offset: 0x000B2767
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x000B456F File Offset: 0x000B276F
		public Camera attachedCamera { get; private set; }

		// Token: 0x060029BD RID: 10685 RVA: 0x000B4578 File Offset: 0x000B2778
		private void Awake()
		{
			this.attachedCamera = base.GetComponent<Camera>();
			this.attachedCamera.enabled = false;
			this.attachedCamera.cullingMask = LayerIndex.manualRender.mask;
			UnityEngine.Object.Destroy(base.GetComponent<AkAudioListener>());
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000B45C8 File Offset: 0x000B27C8
		private static void PrepareObjectForRendering(Transform objTransform, List<ModelCamera.ObjectRestoreInfo> objectRestorationList)
		{
			GameObject gameObject = objTransform.gameObject;
			objectRestorationList.Add(new ModelCamera.ObjectRestoreInfo
			{
				obj = gameObject,
				layer = gameObject.layer
			});
			gameObject.layer = LayerIndex.manualRender.intVal;
			int childCount = objTransform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				ModelCamera.PrepareObjectForRendering(objTransform.GetChild(i), objectRestorationList);
			}
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000B4630 File Offset: 0x000B2830
		public void RenderItem(GameObject obj, RenderTexture targetTexture)
		{
			for (int i = 0; i < this.lights.Count; i++)
			{
				this.lights[i].cullingMask = LayerIndex.manualRender.mask;
			}
			RenderSettingsState renderSettingsState = RenderSettingsState.FromCurrent();
			this.renderSettings.Apply();
			List<ModelCamera.ObjectRestoreInfo> list = new List<ModelCamera.ObjectRestoreInfo>();
			if (obj)
			{
				ModelCamera.PrepareObjectForRendering(obj.transform, list);
			}
			this.attachedCamera.targetTexture = targetTexture;
			this.attachedCamera.Render();
			for (int j = 0; j < list.Count; j++)
			{
				list[j].obj.layer = list[j].layer;
			}
			for (int k = 0; k < this.lights.Count; k++)
			{
				this.lights[k].cullingMask = 0;
			}
			renderSettingsState.Apply();
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000B4720 File Offset: 0x000B2920
		public void AddLight(Light light)
		{
			this.lights.Add(light);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000B472E File Offset: 0x000B292E
		public void RemoveLight(Light light)
		{
			this.lights.Remove(light);
		}

		// Token: 0x04002D09 RID: 11529
		[NonSerialized]
		public RenderSettingsState renderSettings;

		// Token: 0x04002D0B RID: 11531
		public Color ambientLight;

		// Token: 0x04002D0D RID: 11533
		private readonly List<Light> lights = new List<Light>();

		// Token: 0x020007B5 RID: 1973
		private struct ObjectRestoreInfo
		{
			// Token: 0x04002D0E RID: 11534
			public GameObject obj;

			// Token: 0x04002D0F RID: 11535
			public int layer;
		}
	}
}
