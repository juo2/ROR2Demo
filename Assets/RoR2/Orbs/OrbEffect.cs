using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.Orbs
{
	// Token: 0x02000B1F RID: 2847
	[RequireComponent(typeof(EffectComponent))]
	public class OrbEffect : MonoBehaviour
	{
		// Token: 0x060040EA RID: 16618 RVA: 0x0010C9F8 File Offset: 0x0010ABF8
		private void Start()
		{
			EffectComponent component = base.GetComponent<EffectComponent>();
			this.startPosition = component.effectData.origin;
			this.previousPosition = this.startPosition;
			GameObject gameObject = component.effectData.ResolveHurtBoxReference();
			this.targetTransform = (gameObject ? gameObject.transform : null);
			this.duration = component.effectData.genericFloat;
			if (this.duration == 0f)
			{
				Debug.LogFormat("Zero duration for effect \"{0}\"", new object[]
				{
					base.gameObject.name
				});
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			this.lastKnownTargetPosition = (this.targetTransform ? this.targetTransform.position : this.startPosition);
			if (this.startEffect)
			{
				EffectData effectData = new EffectData
				{
					origin = base.transform.position,
					scale = this.startEffectScale
				};
				if (this.startEffectCopiesRotation)
				{
					effectData.rotation = base.transform.rotation;
				}
				EffectManager.SpawnEffect(this.startEffect, effectData, false);
			}
			this.startVelocity.x = Mathf.Lerp(this.startVelocity1.x, this.startVelocity2.x, UnityEngine.Random.value);
			this.startVelocity.y = Mathf.Lerp(this.startVelocity1.y, this.startVelocity2.y, UnityEngine.Random.value);
			this.startVelocity.z = Mathf.Lerp(this.startVelocity1.z, this.startVelocity2.z, UnityEngine.Random.value);
			this.endVelocity.x = Mathf.Lerp(this.endVelocity1.x, this.endVelocity2.x, UnityEngine.Random.value);
			this.endVelocity.y = Mathf.Lerp(this.endVelocity1.y, this.endVelocity2.y, UnityEngine.Random.value);
			this.endVelocity.z = Mathf.Lerp(this.endVelocity1.z, this.endVelocity2.z, UnityEngine.Random.value);
			this.UpdateOrb(0f);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x0010CC1D File Offset: 0x0010AE1D
		private void Update()
		{
			this.UpdateOrb(Time.deltaTime);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x0010CC2C File Offset: 0x0010AE2C
		private void UpdateOrb(float deltaTime)
		{
			if (this.parentObjectTransform)
			{
				this.startPosition = this.parentObjectTransform.position;
			}
			if (this.targetTransform)
			{
				this.lastKnownTargetPosition = this.targetTransform.position;
			}
			float num = Mathf.Clamp01(this.age / this.duration);
			float num2 = this.movementCurve.Evaluate(num);
			Vector3 vector = Vector3.Lerp(this.startPosition + this.startVelocity * num2, this.lastKnownTargetPosition + this.endVelocity * (1f - num2), num2);
			base.transform.position = vector;
			if (this.faceMovement && vector != this.previousPosition)
			{
				base.transform.forward = vector - this.previousPosition;
			}
			this.UpdateBezier();
			if (num == 1f || (this.callArrivalIfTargetIsGone && this.targetTransform == null))
			{
				this.onArrival.Invoke();
				if (this.endEffect)
				{
					EffectData effectData = new EffectData
					{
						origin = base.transform.position,
						scale = this.endEffectScale
					};
					if (this.endEffectCopiesRotation)
					{
						effectData.rotation = base.transform.rotation;
					}
					EffectManager.SpawnEffect(this.endEffect, effectData, false);
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
			this.previousPosition = vector;
			this.age += deltaTime;
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x0010CDB0 File Offset: 0x0010AFB0
		private void UpdateBezier()
		{
			if (this.bezierCurveLine)
			{
				this.bezierCurveLine.p1 = this.startPosition;
				this.bezierCurveLine.v0 = this.endVelocity;
				this.bezierCurveLine.v1 = this.startVelocity;
				this.bezierCurveLine.UpdateBezier(0f);
			}
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x0010CE0D File Offset: 0x0010B00D
		public void InstantiatePrefab(GameObject prefab)
		{
			UnityEngine.Object.Instantiate<GameObject>(prefab, base.transform.position, base.transform.rotation);
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x0010CE2C File Offset: 0x0010B02C
		public void InstantiateEffect(GameObject prefab)
		{
			EffectManager.SpawnEffect(prefab, new EffectData
			{
				origin = base.transform.position
			}, false);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x0010CE4B File Offset: 0x0010B04B
		public void InstantiateEffectCopyRotation(GameObject prefab)
		{
			EffectManager.SpawnEffect(prefab, new EffectData
			{
				origin = base.transform.position,
				rotation = base.transform.rotation
			}, false);
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x0010CE7B File Offset: 0x0010B07B
		public void InstantiateEffectOppositeFacing(GameObject prefab)
		{
			EffectManager.SpawnEffect(prefab, new EffectData
			{
				origin = base.transform.position,
				rotation = Util.QuaternionSafeLookRotation(-base.transform.forward)
			}, false);
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x0010CEB5 File Offset: 0x0010B0B5
		public void InstantiatePrefabOppositeFacing(GameObject prefab)
		{
			UnityEngine.Object.Instantiate<GameObject>(prefab, base.transform.position, Util.QuaternionSafeLookRotation(-base.transform.forward));
		}

		// Token: 0x04003F58 RID: 16216
		private Transform targetTransform;

		// Token: 0x04003F59 RID: 16217
		private float duration;

		// Token: 0x04003F5A RID: 16218
		private Vector3 startPosition;

		// Token: 0x04003F5B RID: 16219
		private Vector3 previousPosition;

		// Token: 0x04003F5C RID: 16220
		private Vector3 lastKnownTargetPosition;

		// Token: 0x04003F5D RID: 16221
		private float age;

		// Token: 0x04003F5E RID: 16222
		[HideInInspector]
		public Transform parentObjectTransform;

		// Token: 0x04003F5F RID: 16223
		[Header("Curve Parameters")]
		public Vector3 startVelocity1;

		// Token: 0x04003F60 RID: 16224
		public Vector3 startVelocity2;

		// Token: 0x04003F61 RID: 16225
		public Vector3 endVelocity1;

		// Token: 0x04003F62 RID: 16226
		public Vector3 endVelocity2;

		// Token: 0x04003F63 RID: 16227
		private Vector3 startVelocity;

		// Token: 0x04003F64 RID: 16228
		private Vector3 endVelocity;

		// Token: 0x04003F65 RID: 16229
		public AnimationCurve movementCurve;

		// Token: 0x04003F66 RID: 16230
		public BezierCurveLine bezierCurveLine;

		// Token: 0x04003F67 RID: 16231
		public bool faceMovement = true;

		// Token: 0x04003F68 RID: 16232
		public bool callArrivalIfTargetIsGone;

		// Token: 0x04003F69 RID: 16233
		[Header("Start Effect")]
		[Tooltip("An effect prefab to spawn on Start")]
		public GameObject startEffect;

		// Token: 0x04003F6A RID: 16234
		public float startEffectScale = 1f;

		// Token: 0x04003F6B RID: 16235
		public bool startEffectCopiesRotation;

		// Token: 0x04003F6C RID: 16236
		[Header("End Effect")]
		[Tooltip("An effect prefab to spawn on end")]
		public GameObject endEffect;

		// Token: 0x04003F6D RID: 16237
		public float endEffectScale = 1f;

		// Token: 0x04003F6E RID: 16238
		public bool endEffectCopiesRotation;

		// Token: 0x04003F6F RID: 16239
		[Header("Arrival Behavior")]
		public UnityEvent onArrival;
	}
}
