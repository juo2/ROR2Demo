using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B80 RID: 2944
	public class MegacrabProjectileController : MonoBehaviour
	{
		// Token: 0x0600430A RID: 17162 RVA: 0x0011609C File Offset: 0x0011429C
		private void OnEnable()
		{
			MegacrabProjectileController.MegacrabProjectileType megacrabProjectileType = this.megacrabProjectileType;
			if (megacrabProjectileType == MegacrabProjectileController.MegacrabProjectileType.White)
			{
				MegacrabProjectileController.whiteProjectileList.Add(this);
				return;
			}
			if (megacrabProjectileType != MegacrabProjectileController.MegacrabProjectileType.Black)
			{
				return;
			}
			MegacrabProjectileController.blackProjectileList.Add(this);
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x001160D0 File Offset: 0x001142D0
		private void OnDisable()
		{
			MegacrabProjectileController.MegacrabProjectileType megacrabProjectileType = this.megacrabProjectileType;
			if (megacrabProjectileType == MegacrabProjectileController.MegacrabProjectileType.White)
			{
				MegacrabProjectileController.whiteProjectileList.Remove(this);
				return;
			}
			if (megacrabProjectileType != MegacrabProjectileController.MegacrabProjectileType.Black)
			{
				return;
			}
			MegacrabProjectileController.blackProjectileList.Remove(this);
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x00116105 File Offset: 0x00114305
		private void Start()
		{
			this.whiteScaleSmoothtime = UnityEngine.Random.Range(this.whiteScaleSmoothtimeMin, this.whiteScaleSmoothtimeMax);
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x00116120 File Offset: 0x00114320
		private void FixedUpdate()
		{
			Vector3 vector = Vector3.zero;
			Vector3 position = base.transform.position;
			MegacrabProjectileController.MegacrabProjectileType megacrabProjectileType = this.megacrabProjectileType;
			if (megacrabProjectileType == MegacrabProjectileController.MegacrabProjectileType.White)
			{
				for (int i = 0; i < MegacrabProjectileController.blackProjectileList.Count; i++)
				{
					Vector3 vector2 = MegacrabProjectileController.blackProjectileList[i].transform.position - position;
					float magnitude = vector2.magnitude;
					Vector3 b = this.whiteForceFalloffCurve.Evaluate(magnitude) * vector2.normalized;
					vector += b;
				}
				Quaternion rotation = base.transform.rotation;
				Quaternion target = base.transform.rotation;
				Vector3 localScale = base.transform.localScale;
				Vector3 one = Vector3.one;
				if (vector.magnitude > this.minimumWhiteForceMagnitude)
				{
					Quaternion b2 = Quaternion.LookRotation(vector);
					float num = Mathf.Min(this.maximumWhiteForceMagnitude, vector.magnitude);
					float num2 = 1f / Mathf.Lerp(num, 1f, this.whitePoisson);
					one = new Vector3(num2, num2, num);
					target = Quaternion.Slerp(rotation, b2, this.whiteMinimumForceToFullyRotate);
				}
				base.transform.localScale = Vector3.SmoothDamp(localScale, one, ref this.whiteScaleVelocity, this.whiteScaleSmoothtime);
				base.transform.rotation = Util.SmoothDampQuaternion(rotation, target, ref this.whiteRotationVelocity, this.whiteScaleSmoothtime);
				return;
			}
			if (megacrabProjectileType != MegacrabProjectileController.MegacrabProjectileType.Black)
			{
				return;
			}
			for (int j = 0; j < MegacrabProjectileController.whiteProjectileList.Count; j++)
			{
				Vector3 vector3 = MegacrabProjectileController.whiteProjectileList[j].transform.position - position;
				float magnitude2 = vector3.magnitude;
				Vector3 b3 = this.blackForceFalloffCurve.Evaluate(magnitude2) * vector3.normalized;
				vector += b3;
			}
			this.projectileRigidbody.AddForce(vector);
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x001162F4 File Offset: 0x001144F4
		public void OnDestroy()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (this.megacrabProjectileType == MegacrabProjectileController.MegacrabProjectileType.Black)
			{
				Vector3 position = base.transform.position;
				for (int i = 0; i < MegacrabProjectileController.whiteProjectileList.Count; i++)
				{
					MegacrabProjectileController megacrabProjectileController = MegacrabProjectileController.whiteProjectileList[i];
					if ((MegacrabProjectileController.whiteProjectileList[i].transform.position - position).magnitude <= this.whiteToBlackTransformationRadius)
					{
						ProjectileExplosion component = megacrabProjectileController.GetComponent<ProjectileExplosion>();
						if (component.GetAlive())
						{
							ProjectileManager.instance.FireProjectile(this.whiteToBlackTransformedProjectile, megacrabProjectileController.transform.position, Quaternion.identity, this.projectileController.owner, this.projectileDamage.damage, 0f, this.projectileDamage.crit, DamageColorIndex.Default, null, -1f);
							component.SetAlive(false);
						}
					}
				}
			}
		}

		// Token: 0x04004109 RID: 16649
		[Header("Cached Properties")]
		public ProjectileController projectileController;

		// Token: 0x0400410A RID: 16650
		public ProjectileDamage projectileDamage;

		// Token: 0x0400410B RID: 16651
		public Rigidbody projectileRigidbody;

		// Token: 0x0400410C RID: 16652
		public MegacrabProjectileController.MegacrabProjectileType megacrabProjectileType;

		// Token: 0x0400410D RID: 16653
		[Header("Black Properties")]
		public AnimationCurve blackForceFalloffCurve;

		// Token: 0x0400410E RID: 16654
		public float whiteToBlackTransformationRadius;

		// Token: 0x0400410F RID: 16655
		public GameObject whiteToBlackTransformedProjectile;

		// Token: 0x04004110 RID: 16656
		[Header("White Properties")]
		public AnimationCurve whiteForceFalloffCurve;

		// Token: 0x04004111 RID: 16657
		public float minimumWhiteForceMagnitude;

		// Token: 0x04004112 RID: 16658
		public float maximumWhiteForceMagnitude;

		// Token: 0x04004113 RID: 16659
		public float whiteMinimumForceToFullyRotate;

		// Token: 0x04004114 RID: 16660
		public float whiteScaleSmoothtimeMin = 0.1f;

		// Token: 0x04004115 RID: 16661
		public float whiteScaleSmoothtimeMax = 0.1f;

		// Token: 0x04004116 RID: 16662
		[Range(0f, 1f)]
		public float whitePoisson;

		// Token: 0x04004117 RID: 16663
		public static List<MegacrabProjectileController> whiteProjectileList = new List<MegacrabProjectileController>();

		// Token: 0x04004118 RID: 16664
		public static List<MegacrabProjectileController> blackProjectileList = new List<MegacrabProjectileController>();

		// Token: 0x04004119 RID: 16665
		private float whiteScaleSmoothtime;

		// Token: 0x0400411A RID: 16666
		private Vector3 whiteScaleVelocity;

		// Token: 0x0400411B RID: 16667
		private float whiteRotationVelocity;

		// Token: 0x02000B81 RID: 2945
		public enum MegacrabProjectileType
		{
			// Token: 0x0400411D RID: 16669
			White,
			// Token: 0x0400411E RID: 16670
			Black,
			// Token: 0x0400411F RID: 16671
			Count
		}
	}
}
