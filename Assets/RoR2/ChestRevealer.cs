using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000651 RID: 1617
	public class ChestRevealer : NetworkBehaviour
	{
		// Token: 0x06001F5F RID: 8031 RVA: 0x00086C64 File Offset: 0x00084E64
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			RoR2Application.onFixedUpdate += ChestRevealer.StaticFixedUpdate;
			ChestRevealer.typesToCheck = (from t in typeof(ChestRevealer).Assembly.GetTypes()
			where typeof(IInteractable).IsAssignableFrom(t)
			select t).ToArray<Type>();
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00086CC4 File Offset: 0x00084EC4
		private static void StaticFixedUpdate()
		{
			ChestRevealer.pendingReveals.Sort();
			while (ChestRevealer.pendingReveals.Count > 0)
			{
				ChestRevealer.PendingReveal pendingReveal = ChestRevealer.pendingReveals[0];
				if (!pendingReveal.time.hasPassed)
				{
					break;
				}
				if (pendingReveal.gameObject)
				{
					ChestRevealer.RevealedObject.RevealObject(pendingReveal.gameObject, pendingReveal.duration);
				}
				ChestRevealer.pendingReveals.RemoveAt(0);
			}
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00086D30 File Offset: 0x00084F30
		public void Pulse()
		{
			ChestRevealer.<>c__DisplayClass12_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.origin = base.transform.position;
			CS$<>8__locals1.radiusSqr = this.radius * this.radius;
			CS$<>8__locals1.invPulseTravelSpeed = 1f / this.pulseTravelSpeed;
			Type[] array = ChestRevealer.typesToCheck;
			for (int i = 0; i < array.Length; i++)
			{
				foreach (MonoBehaviour monoBehaviour in InstanceTracker.FindInstancesEnumerable(array[i]))
				{
					if (((IInteractable)monoBehaviour).ShouldShowOnScanner())
					{
						this.<Pulse>g__TryAddRevealable|12_0(monoBehaviour.transform, ref CS$<>8__locals1);
					}
				}
			}
			EffectManager.SpawnEffect(this.pulseEffectPrefab, new EffectData
			{
				origin = CS$<>8__locals1.origin,
				scale = this.radius * this.pulseEffectScale
			}, false);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00086E1C File Offset: 0x0008501C
		private void FixedUpdate()
		{
			if (this.nextPulse.hasPassed)
			{
				this.Pulse();
				this.nextPulse = Run.FixedTimeStamp.now + this.pulseInterval;
			}
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00086E94 File Offset: 0x00085094
		[CompilerGenerated]
		private void <Pulse>g__TryAddRevealable|12_0(Transform revealableTransform, ref ChestRevealer.<>c__DisplayClass12_0 A_2)
		{
			float sqrMagnitude = (revealableTransform.position - A_2.origin).sqrMagnitude;
			if (sqrMagnitude > A_2.radiusSqr)
			{
				return;
			}
			float b = Mathf.Sqrt(sqrMagnitude) * A_2.invPulseTravelSpeed;
			ChestRevealer.PendingReveal item = new ChestRevealer.PendingReveal
			{
				gameObject = revealableTransform.gameObject,
				time = Run.FixedTimeStamp.now + b,
				duration = this.revealDuration
			};
			ChestRevealer.pendingReveals.Add(item);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x00086F18 File Offset: 0x00085118
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x00086F2B File Offset: 0x0008512B
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

		// Token: 0x06001F69 RID: 8041 RVA: 0x00086F40 File Offset: 0x00085140
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

		// Token: 0x06001F6A RID: 8042 RVA: 0x00086FAC File Offset: 0x000851AC
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

		// Token: 0x06001F6B RID: 8043 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040024E5 RID: 9445
		[SyncVar]
		public float radius;

		// Token: 0x040024E6 RID: 9446
		public float pulseTravelSpeed = 10f;

		// Token: 0x040024E7 RID: 9447
		public float revealDuration = 10f;

		// Token: 0x040024E8 RID: 9448
		public float pulseInterval = 1f;

		// Token: 0x040024E9 RID: 9449
		private Run.FixedTimeStamp nextPulse = Run.FixedTimeStamp.negativeInfinity;

		// Token: 0x040024EA RID: 9450
		public GameObject pulseEffectPrefab;

		// Token: 0x040024EB RID: 9451
		public float pulseEffectScale = 1f;

		// Token: 0x040024EC RID: 9452
		private static readonly List<ChestRevealer.PendingReveal> pendingReveals = new List<ChestRevealer.PendingReveal>();

		// Token: 0x040024ED RID: 9453
		private static Type[] typesToCheck;

		// Token: 0x02000652 RID: 1618
		private struct PendingReveal : IComparable<ChestRevealer.PendingReveal>
		{
			// Token: 0x06001F6C RID: 8044 RVA: 0x00086FED File Offset: 0x000851ED
			public int CompareTo(ChestRevealer.PendingReveal other)
			{
				return this.time.CompareTo(other.time);
			}

			// Token: 0x040024EE RID: 9454
			public GameObject gameObject;

			// Token: 0x040024EF RID: 9455
			public Run.FixedTimeStamp time;

			// Token: 0x040024F0 RID: 9456
			public float duration;
		}

		// Token: 0x02000653 RID: 1619
		private class RevealedObject : MonoBehaviour
		{
			// Token: 0x06001F6D RID: 8045 RVA: 0x00087000 File Offset: 0x00085200
			public static void RevealObject(GameObject gameObject, float duration)
			{
				ChestRevealer.RevealedObject revealedObject;
				if (!ChestRevealer.RevealedObject.currentlyRevealedObjects.TryGetValue(gameObject, out revealedObject))
				{
					revealedObject = gameObject.AddComponent<ChestRevealer.RevealedObject>();
				}
				if (revealedObject.lifetime < duration)
				{
					revealedObject.lifetime = duration;
				}
			}

			// Token: 0x06001F6E RID: 8046 RVA: 0x00087034 File Offset: 0x00085234
			private void OnEnable()
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/PositionIndicators/PoiPositionIndicator"), base.transform.position, base.transform.rotation);
				this.positionIndicator = gameObject.GetComponent<PositionIndicator>();
				this.positionIndicator.insideViewObject.GetComponent<SpriteRenderer>().sprite = PingIndicator.GetInteractableIcon(base.gameObject);
				this.positionIndicator.targetTransform = base.transform;
				ChestRevealer.RevealedObject.currentlyRevealedObjects[base.gameObject] = this;
			}

			// Token: 0x06001F6F RID: 8047 RVA: 0x000870B5 File Offset: 0x000852B5
			private void OnDisable()
			{
				ChestRevealer.RevealedObject.currentlyRevealedObjects.Remove(base.gameObject);
				if (this.positionIndicator)
				{
					UnityEngine.Object.Destroy(this.positionIndicator.gameObject);
				}
				this.positionIndicator = null;
			}

			// Token: 0x06001F70 RID: 8048 RVA: 0x000870EC File Offset: 0x000852EC
			private void FixedUpdate()
			{
				this.lifetime -= Time.fixedDeltaTime;
				if (this.lifetime <= 0f)
				{
					UnityEngine.Object.Destroy(this);
				}
			}

			// Token: 0x040024F1 RID: 9457
			private float lifetime;

			// Token: 0x040024F2 RID: 9458
			private static readonly Dictionary<GameObject, ChestRevealer.RevealedObject> currentlyRevealedObjects = new Dictionary<GameObject, ChestRevealer.RevealedObject>();

			// Token: 0x040024F3 RID: 9459
			private PositionIndicator positionIndicator;
		}
	}
}
