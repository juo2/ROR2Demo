using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000603 RID: 1539
	[RequireComponent(typeof(TeamFilter))]
	public class BuffWard : NetworkBehaviour
	{
		// Token: 0x06001C31 RID: 7217 RVA: 0x00077D94 File Offset: 0x00075F94
		private void Awake()
		{
			this.teamFilter = base.GetComponent<TeamFilter>();
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00077DA2 File Offset: 0x00075FA2
		private void OnEnable()
		{
			if (this.rangeIndicator)
			{
				this.rangeIndicator.gameObject.SetActive(true);
			}
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00077DC2 File Offset: 0x00075FC2
		private void OnDisable()
		{
			if (this.rangeIndicator)
			{
				this.rangeIndicator.gameObject.SetActive(false);
			}
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00077DE4 File Offset: 0x00075FE4
		private void Start()
		{
			if (this.removalTime > 0f)
			{
				this.needsRemovalTime = true;
			}
			RaycastHit raycastHit;
			if (this.floorWard && Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, 500f, LayerIndex.world.mask))
			{
				base.transform.position = raycastHit.point;
				base.transform.up = raycastHit.normal;
			}
			if (this.rangeIndicator && this.expires)
			{
				ScaleParticleSystemDuration component = this.rangeIndicator.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.expireDuration;
				}
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00077E98 File Offset: 0x00076098
		private void Update()
		{
			this.calculatedRadius = (this.animateRadius ? (this.radius * this.radiusCoefficientCurve.Evaluate(this.stopwatch / this.expireDuration)) : this.radius);
			this.stopwatch += Time.deltaTime;
			if (this.expires && NetworkServer.active)
			{
				if (this.needsRemovalTime)
				{
					if (this.stopwatch >= this.expireDuration - this.removalTime)
					{
						this.needsRemovalTime = false;
						Util.PlaySound(this.removalSoundString, base.gameObject);
						this.onRemoval.Invoke();
					}
				}
				else if (this.expireDuration <= this.stopwatch)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			if (this.rangeIndicator)
			{
				float num = Mathf.SmoothDamp(this.rangeIndicator.localScale.x, this.calculatedRadius, ref this.rangeIndicatorScaleVelocity, 0.2f);
				this.rangeIndicator.localScale = new Vector3(num, num, num);
			}
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00077FA0 File Offset: 0x000761A0
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.buffTimer -= Time.fixedDeltaTime;
				if (this.buffTimer <= 0f)
				{
					this.buffTimer = this.interval;
					float radiusSqr = this.calculatedRadius * this.calculatedRadius;
					Vector3 position = base.transform.position;
					if (this.invertTeamFilter)
					{
						for (TeamIndex teamIndex = TeamIndex.Neutral; teamIndex < TeamIndex.Count; teamIndex += 1)
						{
							if (teamIndex != this.teamFilter.teamIndex)
							{
								this.BuffTeam(TeamComponent.GetTeamMembers(teamIndex), radiusSqr, position);
							}
						}
						return;
					}
					this.BuffTeam(TeamComponent.GetTeamMembers(this.teamFilter.teamIndex), radiusSqr, position);
				}
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00078048 File Offset: 0x00076248
		private void BuffTeam(IEnumerable<TeamComponent> recipients, float radiusSqr, Vector3 currentPosition)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (!this.buffDef)
			{
				return;
			}
			foreach (TeamComponent teamComponent in recipients)
			{
				Vector3 vector = teamComponent.transform.position - currentPosition;
				if (this.shape == BuffWard.BuffWardShape.VerticalTube)
				{
					vector.y = 0f;
				}
				if (vector.sqrMagnitude <= radiusSqr)
				{
					CharacterBody component = teamComponent.GetComponent<CharacterBody>();
					if (component && (!this.requireGrounded || !component.characterMotor || component.characterMotor.isGrounded))
					{
						component.AddTimedBuff(this.buffDef.buffIndex, this.buffDuration);
					}
				}
			}
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00078120 File Offset: 0x00076320
		private void OnValidate()
		{
			if (!this.buffDef)
			{
				Debug.LogWarningFormat(this, "BuffWard {0} has no buff specified.", new object[]
				{
					this
				});
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x00078164 File Offset: 0x00076364
		// (set) Token: 0x06001C3C RID: 7228 RVA: 0x00078177 File Offset: 0x00076377
		public float Networkradius
		{
			get
			{
				return this.radius;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.radius, 1U);
			}
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0007818C File Offset: 0x0007638C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.radius);
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
				writer.Write(this.radius);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000781F8 File Offset: 0x000763F8
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.radius = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.radius = reader.ReadSingle();
			}
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040021ED RID: 8685
		[Tooltip("The shape of the area")]
		public BuffWard.BuffWardShape shape;

		// Token: 0x040021EE RID: 8686
		[Tooltip("The area of effect.")]
		[SyncVar]
		public float radius;

		// Token: 0x040021EF RID: 8687
		[Tooltip("How long between buff pulses in the area of effect.")]
		public float interval = 1f;

		// Token: 0x040021F0 RID: 8688
		[Tooltip("The child range indicator object. Will be scaled to the radius.")]
		public Transform rangeIndicator;

		// Token: 0x040021F1 RID: 8689
		[Tooltip("The buff type to grant")]
		public BuffDef buffDef;

		// Token: 0x040021F2 RID: 8690
		[Tooltip("The buff duration")]
		public float buffDuration;

		// Token: 0x040021F3 RID: 8691
		[Tooltip("Should the ward be floored on start")]
		public bool floorWard;

		// Token: 0x040021F4 RID: 8692
		[Tooltip("Does the ward disappear over time?")]
		public bool expires;

		// Token: 0x040021F5 RID: 8693
		[Tooltip("If set, applies to all teams BUT the one selected.")]
		public bool invertTeamFilter;

		// Token: 0x040021F6 RID: 8694
		public float expireDuration;

		// Token: 0x040021F7 RID: 8695
		public bool animateRadius;

		// Token: 0x040021F8 RID: 8696
		public AnimationCurve radiusCoefficientCurve;

		// Token: 0x040021F9 RID: 8697
		[Tooltip("If set, the ward will give you this amount of time to play removal effects.")]
		public float removalTime;

		// Token: 0x040021FA RID: 8698
		private bool needsRemovalTime;

		// Token: 0x040021FB RID: 8699
		public string removalSoundString = "";

		// Token: 0x040021FC RID: 8700
		public UnityEvent onRemoval;

		// Token: 0x040021FD RID: 8701
		public bool requireGrounded;

		// Token: 0x040021FE RID: 8702
		private TeamFilter teamFilter;

		// Token: 0x040021FF RID: 8703
		private float buffTimer;

		// Token: 0x04002200 RID: 8704
		private float rangeIndicatorScaleVelocity;

		// Token: 0x04002201 RID: 8705
		private float stopwatch;

		// Token: 0x04002202 RID: 8706
		private float calculatedRadius;

		// Token: 0x02000604 RID: 1540
		public enum BuffWardShape
		{
			// Token: 0x04002204 RID: 8708
			Sphere,
			// Token: 0x04002205 RID: 8709
			VerticalTube,
			// Token: 0x04002206 RID: 8710
			Count
		}
	}
}
