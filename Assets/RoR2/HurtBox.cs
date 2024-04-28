using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200073E RID: 1854
	[RequireComponent(typeof(Collider))]
	public class HurtBox : MonoBehaviour
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x000A80C8 File Offset: 0x000A62C8
		// (set) Token: 0x06002688 RID: 9864 RVA: 0x000A80D0 File Offset: 0x000A62D0
		public TeamIndex teamIndex { get; set; } = TeamIndex.None;

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000A80D9 File Offset: 0x000A62D9
		// (set) Token: 0x0600268A RID: 9866 RVA: 0x000A80E1 File Offset: 0x000A62E1
		public Collider collider { get; private set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x000A80EA File Offset: 0x000A62EA
		// (set) Token: 0x0600268C RID: 9868 RVA: 0x000A80F2 File Offset: 0x000A62F2
		public float volume { get; private set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x000A80FB File Offset: 0x000A62FB
		public Vector3 randomVolumePoint
		{
			get
			{
				return Util.RandomColliderVolumePoint(this.collider);
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000A8108 File Offset: 0x000A6308
		private void Awake()
		{
			this.collider = base.GetComponent<Collider>();
			this.collider.isTrigger = false;
			Rigidbody rigidbody = base.GetComponent<Rigidbody>();
			if (!rigidbody)
			{
				rigidbody = base.gameObject.AddComponent<Rigidbody>();
			}
			rigidbody.isKinematic = true;
			Vector3 lossyScale = base.transform.lossyScale;
			this.volume = lossyScale.x * 2f * (lossyScale.y * 2f) * (lossyScale.z * 2f);
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x000A8187 File Offset: 0x000A6387
		public static IReadOnlyList<HurtBox> readOnlySniperTargetsList
		{
			get
			{
				return HurtBox.sniperTargetsList;
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000A818E File Offset: 0x000A638E
		private void OnEnable()
		{
			if (this.isBullseye)
			{
				HurtBox.bullseyesList.Add(this);
				this.isInBullseyeList = true;
			}
			if (this.isSniperTarget)
			{
				HurtBox.sniperTargetsList.Add(this);
				this.isInSniperTargetList = true;
			}
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000A81C4 File Offset: 0x000A63C4
		private void OnDisable()
		{
			if (this.isInSniperTargetList)
			{
				this.isInSniperTargetList = false;
				HurtBox.sniperTargetsList.Remove(this);
			}
			if (this.isInBullseyeList)
			{
				this.isInBullseyeList = false;
				HurtBox.bullseyesList.Remove(this);
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000A81FC File Offset: 0x000A63FC
		public static GameObject FindEntityObject([NotNull] HurtBox hurtBox)
		{
			if (!hurtBox.healthComponent)
			{
				return null;
			}
			return hurtBox.healthComponent.gameObject;
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000A8218 File Offset: 0x000A6418
		public static bool HurtBoxesShareEntity([NotNull] HurtBox a, [NotNull] HurtBox b)
		{
			return HurtBox.FindEntityObject(a) == HurtBox.FindEntityObject(b);
		}

		// Token: 0x04002A7A RID: 10874
		[Tooltip("The health component to which this hurtbox belongs.")]
		public HealthComponent healthComponent;

		// Token: 0x04002A7B RID: 10875
		[Tooltip("Whether or not this hurtbox is considered a bullseye. Do not change this at runtime!")]
		public bool isBullseye;

		// Token: 0x04002A7C RID: 10876
		[Tooltip("Whether or not this hurtbox is considered a sniper target. Do not change this at runtime!")]
		public bool isSniperTarget;

		// Token: 0x04002A7D RID: 10877
		public HurtBox.DamageModifier damageModifier;

		// Token: 0x04002A7F RID: 10879
		[SerializeField]
		[HideInInspector]
		public HurtBoxGroup hurtBoxGroup;

		// Token: 0x04002A80 RID: 10880
		[SerializeField]
		[HideInInspector]
		public short indexInGroup = -1;

		// Token: 0x04002A83 RID: 10883
		private bool isInBullseyeList;

		// Token: 0x04002A84 RID: 10884
		private bool isInSniperTargetList;

		// Token: 0x04002A85 RID: 10885
		private static readonly List<HurtBox> bullseyesList = new List<HurtBox>();

		// Token: 0x04002A86 RID: 10886
		public static readonly ReadOnlyCollection<HurtBox> readOnlyBullseyesList = HurtBox.bullseyesList.AsReadOnly();

		// Token: 0x04002A87 RID: 10887
		private static readonly List<HurtBox> sniperTargetsList = new List<HurtBox>();

		// Token: 0x04002A88 RID: 10888
		public static readonly float sniperTargetRadius = 1f;

		// Token: 0x04002A89 RID: 10889
		public static readonly float sniperTargetRadiusSqr = HurtBox.sniperTargetRadius * HurtBox.sniperTargetRadius;

		// Token: 0x0200073F RID: 1855
		public enum DamageModifier
		{
			// Token: 0x04002A8B RID: 10891
			Normal,
			// Token: 0x04002A8C RID: 10892
			[Obsolete]
			SniperTarget,
			// Token: 0x04002A8D RID: 10893
			Weak,
			// Token: 0x04002A8E RID: 10894
			Barrier
		}

		// Token: 0x02000740 RID: 1856
		public struct EntityEqualityComparer : IEqualityComparer<HurtBox>
		{
			// Token: 0x06002696 RID: 9878 RVA: 0x000A827D File Offset: 0x000A647D
			public bool Equals(HurtBox a, HurtBox b)
			{
				return HurtBox.HurtBoxesShareEntity(a, b);
			}

			// Token: 0x06002697 RID: 9879 RVA: 0x000A8286 File Offset: 0x000A6486
			public int GetHashCode(HurtBox hurtBox)
			{
				return HurtBox.FindEntityObject(hurtBox).GetHashCode();
			}
		}
	}
}
