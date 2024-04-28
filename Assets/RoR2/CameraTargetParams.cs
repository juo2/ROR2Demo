using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000610 RID: 1552
	public class CameraTargetParams : MonoBehaviour
	{
		// Token: 0x06001C98 RID: 7320 RVA: 0x00079B73 File Offset: 0x00077D73
		public void AddRecoil(float verticalMin, float verticalMax, float horizontalMin, float horizontalMax)
		{
			this.targetRecoil += new Vector2(UnityEngine.Random.Range(horizontalMin, horizontalMax), UnityEngine.Random.Range(verticalMin, verticalMax));
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00079B9C File Offset: 0x00077D9C
		public CameraTargetParams.AimRequest RequestAimType(CameraTargetParams.AimType aimType)
		{
			if (aimType == CameraTargetParams.AimType.Aura)
			{
				CharacterCameraParamsData data = this.cameraParams.data;
				data.idealLocalCameraPos.value = data.idealLocalCameraPos.value + new Vector3(0f, 1.5f, -7f);
				CameraTargetParams.CameraParamsOverrideHandle overrideHandle = this.AddParamsOverride(new CameraTargetParams.CameraParamsOverrideRequest
				{
					cameraParamsData = data,
					priority = 0.1f
				}, 0.5f);
				CameraTargetParams.AimRequest aimRequest2 = new CameraTargetParams.AimRequest(aimType, delegate(CameraTargetParams.AimRequest aimRequest)
				{
					this.RemoveRequest(aimRequest);
					this.RemoveParamsOverride(overrideHandle, 0.5f);
				});
				this.aimRequestStack.Add(aimRequest2);
				return aimRequest2;
			}
			return null;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00079C49 File Offset: 0x00077E49
		private void RemoveRequest(CameraTargetParams.AimRequest request)
		{
			this.aimRequestStack.Remove(request);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00079C58 File Offset: 0x00077E58
		private void Awake()
		{
			CharacterBody component = base.GetComponent<CharacterBody>();
			if (component && this.cameraPivotTransform == null)
			{
				this.cameraPivotTransform = component.aimOriginTransform;
			}
			this.cameraParamsOverrides = CollectionPool<CameraTargetParams.CameraParamsOverride, List<CameraTargetParams.CameraParamsOverride>>.RentCollection();
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00079C99 File Offset: 0x00077E99
		private void OnDestroy()
		{
			this.cameraParamsOverrides = CollectionPool<CameraTargetParams.CameraParamsOverride, List<CameraTargetParams.CameraParamsOverride>>.ReturnCollection(this.cameraParamsOverrides);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00079CAC File Offset: 0x00077EAC
		private void Update()
		{
			this.targetRecoil = Vector2.SmoothDamp(this.targetRecoil, Vector2.zero, ref this.targetRecoilVelocity, CameraTargetParams.targetRecoilDampTime, 180f, Time.deltaTime);
			this.recoil = Vector2.SmoothDamp(this.recoil, this.targetRecoil, ref this.recoilVelocity, CameraTargetParams.recoilDampTime, 180f, Time.deltaTime);
			this.CalcParams(this.currentCameraParamsData);
			float time = Time.time;
			for (int i = this.cameraParamsOverrides.Count - 1; i >= 0; i--)
			{
				if (this.cameraParamsOverrides[i].exitEndTime <= time)
				{
					this.cameraParamsOverrides.RemoveAt(i);
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00079D5A File Offset: 0x00077F5A
		public ref CharacterCameraParamsData currentCameraParamsData
		{
			get
			{
				return ref this._currentCameraParamsData;
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00079D64 File Offset: 0x00077F64
		public CameraTargetParams.CameraParamsOverrideHandle AddParamsOverride(CameraTargetParams.CameraParamsOverrideRequest request, float transitionDuration = 0.2f)
		{
			float time = Time.time;
			CameraTargetParams.CameraParamsOverride cameraParamsOverride = new CameraTargetParams.CameraParamsOverride
			{
				cameraParamsData = request.cameraParamsData,
				enterStartTime = time,
				enterEndTime = time + transitionDuration,
				exitStartTime = float.PositiveInfinity,
				exitEndTime = float.PositiveInfinity,
				priority = request.priority
			};
			int num = 0;
			while (num < this.cameraParamsOverrides.Count && request.priority > this.cameraParamsOverrides[num].priority)
			{
				num++;
			}
			this.cameraParamsOverrides.Insert(num, cameraParamsOverride);
			return new CameraTargetParams.CameraParamsOverrideHandle(cameraParamsOverride);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00079E00 File Offset: 0x00078000
		public CameraTargetParams.CameraParamsOverrideHandle RemoveParamsOverride(CameraTargetParams.CameraParamsOverrideHandle handle, float transitionDuration = 0.2f)
		{
			if (this.cameraParamsOverrides == null)
			{
				return default(CameraTargetParams.CameraParamsOverrideHandle);
			}
			CameraTargetParams.CameraParamsOverride cameraParamsOverride = null;
			for (int i = 0; i < this.cameraParamsOverrides.Count; i++)
			{
				if (handle.target == this.cameraParamsOverrides[i])
				{
					cameraParamsOverride = this.cameraParamsOverrides[i];
					break;
				}
			}
			if (cameraParamsOverride == null || cameraParamsOverride.exitStartTime != float.PositiveInfinity)
			{
				return default(CameraTargetParams.CameraParamsOverrideHandle);
			}
			float time = Time.time;
			cameraParamsOverride.exitStartTime = time;
			cameraParamsOverride.exitEndTime = time + transitionDuration;
			return default(CameraTargetParams.CameraParamsOverrideHandle);
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00079E94 File Offset: 0x00078094
		public void CalcParams(out CharacterCameraParamsData dest)
		{
			dest = (this.cameraParams ? this.cameraParams.data : CharacterCameraParamsData.basic);
			float time = Time.time;
			for (int i = 0; i < this.cameraParamsOverrides.Count; i++)
			{
				CameraTargetParams.CameraParamsOverride cameraParamsOverride = this.cameraParamsOverrides[i];
				CharacterCameraParamsData.Blend(cameraParamsOverride.cameraParamsData, ref dest, cameraParamsOverride.CalculateAlpha(time));
			}
		}

		// Token: 0x04002263 RID: 8803
		public CharacterCameraParams cameraParams;

		// Token: 0x04002264 RID: 8804
		public Transform cameraPivotTransform;

		// Token: 0x04002265 RID: 8805
		[ShowFieldObsolete]
		[Obsolete]
		public float fovOverride;

		// Token: 0x04002266 RID: 8806
		[HideInInspector]
		public Vector2 recoil;

		// Token: 0x04002267 RID: 8807
		[HideInInspector]
		public bool dontRaycastToPivot;

		// Token: 0x04002268 RID: 8808
		private static float targetRecoilDampTime = 0.08f;

		// Token: 0x04002269 RID: 8809
		private static float recoilDampTime = 0.05f;

		// Token: 0x0400226A RID: 8810
		private Vector2 targetRecoil;

		// Token: 0x0400226B RID: 8811
		private Vector2 recoilVelocity;

		// Token: 0x0400226C RID: 8812
		private Vector2 targetRecoilVelocity;

		// Token: 0x0400226D RID: 8813
		private List<CameraTargetParams.AimRequest> aimRequestStack = new List<CameraTargetParams.AimRequest>();

		// Token: 0x0400226E RID: 8814
		private static readonly AnimationCurve easeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x0400226F RID: 8815
		private List<CameraTargetParams.CameraParamsOverride> cameraParamsOverrides;

		// Token: 0x04002270 RID: 8816
		private CharacterCameraParamsData _currentCameraParamsData;

		// Token: 0x02000611 RID: 1553
		public enum AimType
		{
			// Token: 0x04002272 RID: 8818
			Standard,
			// Token: 0x04002273 RID: 8819
			FirstPerson,
			// Token: 0x04002274 RID: 8820
			Aura,
			// Token: 0x04002275 RID: 8821
			Sprinting,
			// Token: 0x04002276 RID: 8822
			OverTheShoulder
		}

		// Token: 0x02000612 RID: 1554
		public class AimRequest : IDisposable
		{
			// Token: 0x06001CA4 RID: 7332 RVA: 0x00079F49 File Offset: 0x00078149
			public AimRequest(CameraTargetParams.AimType type, Action<CameraTargetParams.AimRequest> onDispose)
			{
				this.disposeCallback = onDispose;
				this.aimType = type;
			}

			// Token: 0x06001CA5 RID: 7333 RVA: 0x00079F5F File Offset: 0x0007815F
			public void Dispose()
			{
				Action<CameraTargetParams.AimRequest> action = this.disposeCallback;
				if (action != null)
				{
					action(this);
				}
				this.disposeCallback = null;
			}

			// Token: 0x04002277 RID: 8823
			public readonly CameraTargetParams.AimType aimType;

			// Token: 0x04002278 RID: 8824
			private Action<CameraTargetParams.AimRequest> disposeCallback;
		}

		// Token: 0x02000613 RID: 1555
		public struct CameraParamsOverrideRequest
		{
			// Token: 0x04002279 RID: 8825
			public CharacterCameraParamsData cameraParamsData;

			// Token: 0x0400227A RID: 8826
			public float priority;
		}

		// Token: 0x02000614 RID: 1556
		internal class CameraParamsOverride
		{
			// Token: 0x06001CA6 RID: 7334 RVA: 0x00079F7C File Offset: 0x0007817C
			public float CalculateAlpha(float t)
			{
				float num = 1f;
				if (t < this.enterEndTime)
				{
					num *= Mathf.Clamp01(Mathf.InverseLerp(this.enterStartTime, this.enterEndTime, t));
				}
				if (t > this.exitStartTime)
				{
					num *= Mathf.Clamp01(Mathf.InverseLerp(this.exitEndTime, this.exitStartTime, t));
				}
				return CameraTargetParams.easeCurve.Evaluate(num);
			}

			// Token: 0x0400227B RID: 8827
			public CharacterCameraParamsData cameraParamsData;

			// Token: 0x0400227C RID: 8828
			public float priority;

			// Token: 0x0400227D RID: 8829
			public float enterStartTime;

			// Token: 0x0400227E RID: 8830
			public float enterEndTime;

			// Token: 0x0400227F RID: 8831
			public float exitStartTime;

			// Token: 0x04002280 RID: 8832
			public float exitEndTime;
		}

		// Token: 0x02000615 RID: 1557
		public struct CameraParamsOverrideHandle
		{
			// Token: 0x06001CA8 RID: 7336 RVA: 0x00079FE0 File Offset: 0x000781E0
			internal CameraParamsOverrideHandle(CameraTargetParams.CameraParamsOverride target)
			{
				this.target = target;
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x00079FE9 File Offset: 0x000781E9
			public bool isValid
			{
				get
				{
					return this.target != null;
				}
			}

			// Token: 0x04002281 RID: 8833
			internal readonly CameraTargetParams.CameraParamsOverride target;
		}
	}
}
