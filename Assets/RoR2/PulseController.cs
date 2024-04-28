using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HG;
using Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000831 RID: 2097
	public class PulseController : NetworkBehaviour
	{
		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06002DA3 RID: 11683 RVA: 0x000C2798 File Offset: 0x000C0998
		// (remove) Token: 0x06002DA4 RID: 11684 RVA: 0x000C27D0 File Offset: 0x000C09D0
		public event PulseController.PerformSearchDelegate performSearch;

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06002DA5 RID: 11685 RVA: 0x000C2808 File Offset: 0x000C0A08
		// (remove) Token: 0x06002DA6 RID: 11686 RVA: 0x000C2840 File Offset: 0x000C0A40
		public event PulseController.OnPulseHitDelegate onPulseHit;

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000C2875 File Offset: 0x000C0A75
		private void OnEnable()
		{
			if (NetworkServer.active)
			{
				this.NetworkstartTime = Run.FixedTimeStamp.positiveInfinity;
				this.hitObjects = CollectionPool<UnityEngine.Object, List<UnityEngine.Object>>.RentCollection();
			}
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000C2894 File Offset: 0x000C0A94
		private void OnDisable()
		{
			if (NetworkServer.active)
			{
				this.NetworkstartTime = Run.FixedTimeStamp.positiveInfinity;
				this.hitObjects = CollectionPool<UnityEngine.Object, List<UnityEngine.Object>>.ReturnCollection(this.hitObjects);
			}
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000C28BC File Offset: 0x000C0ABC
		private void FixedUpdate()
		{
			float num = Mathf.Clamp01((Run.FixedTimeStamp.now - this.startTime) / this.duration);
			AnimationCurveAsset animationCurveAsset = this.normalizedRadiusCurve;
			if (((animationCurveAsset != null) ? animationCurveAsset.value : null) != null)
			{
				num *= this.normalizedRadiusCurve.value.Evaluate(num);
			}
			float radius = this.CalcRadius(num);
			if (NetworkServer.active)
			{
				this.StepPulse(radius);
			}
			if (num == 1f && NetworkServer.active)
			{
				this.NetworkstartTime = Run.FixedTimeStamp.positiveInfinity;
				UnityEvent unityEvent = this.onPulseEndServer;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000C2950 File Offset: 0x000C0B50
		private void Update()
		{
			float num = Mathf.Clamp01((Run.TimeStamp.now - this.startTime) / this.duration);
			AnimationCurveAsset animationCurveAsset = this.normalizedRadiusCurve;
			if (((animationCurveAsset != null) ? animationCurveAsset.value : null) != null)
			{
				num *= this.normalizedRadiusCurve.value.Evaluate(num);
			}
			float num2 = this.CalcRadius(num);
			if (this.effectTransform)
			{
				bool flag = num > 0f && num < 1f;
				this.effectTransform.gameObject.SetActive(flag);
				if (flag)
				{
					this.effectTransform.localScale = new Vector3(num2, num2, num2);
				}
			}
			if (this.previousVfxNormalizedTime != num)
			{
				this.previousVfxNormalizedTime = num;
				UnityEventFloat unityEventFloat = this.updateVfx;
				if (unityEventFloat == null)
				{
					return;
				}
				unityEventFloat.Invoke(num);
			}
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000C2A13 File Offset: 0x000C0C13
		[Server]
		public void StartPulseServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PulseController::StartPulseServer()' called on client");
				return;
			}
			if (!base.enabled)
			{
				return;
			}
			this.NetworkstartTime = Run.FixedTimeStamp.now;
			this.hitObjects.Clear();
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000C2A49 File Offset: 0x000C0C49
		private float CalcRadius(float normalizedTime)
		{
			return normalizedTime * this.finalRadius;
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000C2A54 File Offset: 0x000C0C54
		private void StepPulse(float radius)
		{
			List<PulseController.PulseSearchResult> list = CollectionPool<PulseController.PulseSearchResult, List<PulseController.PulseSearchResult>>.RentCollection();
			Vector3 position = base.transform.position;
			try
			{
				PulseController.PerformSearchDelegate performSearchDelegate = this.performSearch;
				if (performSearchDelegate != null)
				{
					performSearchDelegate(this, position, radius, list);
				}
				for (int i = 0; i < list.Count; i++)
				{
					PulseController.PulseSearchResult pulseSearchResult = list[i];
					if (!this.hitObjects.Contains(pulseSearchResult.hitObject))
					{
						try
						{
							float num = Vector3.Distance(position, pulseSearchResult.hitPos);
							PulseController.OnPulseHitDelegate onPulseHitDelegate = this.onPulseHit;
							if (onPulseHitDelegate != null)
							{
								onPulseHitDelegate(this, new PulseController.PulseHit
								{
									hitObject = pulseSearchResult.hitObject,
									hitPos = pulseSearchResult.hitPos,
									pulseOrigin = position,
									hitDistance = num,
									hitSeverity = Mathf.Clamp01(1f - num / this.finalRadius)
								});
							}
						}
						catch (Exception message)
						{
							Debug.LogError(message);
						}
						finally
						{
							this.hitObjects.Add(pulseSearchResult.hitObject);
						}
					}
				}
			}
			finally
			{
				CollectionPool<PulseController.PulseSearchResult, List<PulseController.PulseSearchResult>>.RentCollection();
			}
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000C2B90 File Offset: 0x000C0D90
		// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x000C2BA3 File Offset: 0x000C0DA3
		public float NetworkfinalRadius
		{
			get
			{
				return this.finalRadius;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.finalRadius, 1U);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x000C2BB8 File Offset: 0x000C0DB8
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x000C2BCB File Offset: 0x000C0DCB
		public float Networkduration
		{
			get
			{
				return this.duration;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.duration, 2U);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x000C2BE0 File Offset: 0x000C0DE0
		// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x000C2BF3 File Offset: 0x000C0DF3
		public Run.FixedTimeStamp NetworkstartTime
		{
			get
			{
				return this.startTime;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Run.FixedTimeStamp>(value, ref this.startTime, 4U);
			}
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000C2C08 File Offset: 0x000C0E08
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.finalRadius);
				writer.Write(this.duration);
				GeneratedNetworkCode._WriteFixedTimeStamp_Run(writer, this.startTime);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.finalRadius);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.duration);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				GeneratedNetworkCode._WriteFixedTimeStamp_Run(writer, this.startTime);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000C2CF4 File Offset: 0x000C0EF4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.finalRadius = reader.ReadSingle();
				this.duration = reader.ReadSingle();
				this.startTime = GeneratedNetworkCode._ReadFixedTimeStamp_Run(reader);
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.finalRadius = reader.ReadSingle();
			}
			if ((num & 2) != 0)
			{
				this.duration = reader.ReadSingle();
			}
			if ((num & 4) != 0)
			{
				this.startTime = GeneratedNetworkCode._ReadFixedTimeStamp_Run(reader);
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002FA9 RID: 12201
		[Tooltip("How far the pulse can ultimately reach.")]
		[SyncVar]
		public float finalRadius;

		// Token: 0x04002FAA RID: 12202
		[SyncVar]
		[Tooltip("How long it takes for the pulse to complete.")]
		public float duration;

		// Token: 0x04002FAB RID: 12203
		[Tooltip("The curve by which normalized time will map to normalized radius.")]
		public AnimationCurveAsset normalizedRadiusCurve;

		// Token: 0x04002FAC RID: 12204
		[Tooltip("An object which will be enabled and scaled across the duration of the pulse.")]
		public Transform effectTransform;

		// Token: 0x04002FAD RID: 12205
		[Tooltip("Fires off the normalized time, useful for updating any VFX.")]
		public UnityEventFloat updateVfx;

		// Token: 0x04002FAE RID: 12206
		[Tooltip("Fired when the pulse is over.")]
		public UnityEvent onPulseEndServer;

		// Token: 0x04002FB1 RID: 12209
		[SyncVar]
		private Run.FixedTimeStamp startTime;

		// Token: 0x04002FB2 RID: 12210
		private List<UnityEngine.Object> hitObjects;

		// Token: 0x04002FB3 RID: 12211
		private float previousVfxNormalizedTime = float.NaN;

		// Token: 0x02000832 RID: 2098
		// (Invoke) Token: 0x06002DBA RID: 11706
		public delegate void PerformSearchDelegate(PulseController pulseController, Vector3 origin, float radius, List<PulseController.PulseSearchResult> dest);

		// Token: 0x02000833 RID: 2099
		// (Invoke) Token: 0x06002DBE RID: 11710
		public delegate void OnPulseHitDelegate(PulseController pulseController, PulseController.PulseHit hitInfo);

		// Token: 0x02000834 RID: 2100
		public struct PulseSearchResult
		{
			// Token: 0x04002FB4 RID: 12212
			public UnityEngine.Object hitObject;

			// Token: 0x04002FB5 RID: 12213
			public Vector3 hitPos;
		}

		// Token: 0x02000835 RID: 2101
		public struct PulseHit
		{
			// Token: 0x04002FB6 RID: 12214
			public UnityEngine.Object hitObject;

			// Token: 0x04002FB7 RID: 12215
			public Vector3 hitPos;

			// Token: 0x04002FB8 RID: 12216
			public Vector3 pulseOrigin;

			// Token: 0x04002FB9 RID: 12217
			public float hitDistance;

			// Token: 0x04002FBA RID: 12218
			public float hitSeverity;
		}
	}
}
