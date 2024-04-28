using System;
using System.Collections.Generic;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006D0 RID: 1744
	public class ExperienceManager : MonoBehaviour
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x00094559 File Offset: 0x00092759
		// (set) Token: 0x0600225C RID: 8796 RVA: 0x00094560 File Offset: 0x00092760
		public static ExperienceManager instance { get; private set; }

		// Token: 0x0600225D RID: 8797 RVA: 0x00094568 File Offset: 0x00092768
		private static float CalcOrbTravelTime(float timeOffset)
		{
			return 0.5f + 1.5f * timeOffset;
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00094577 File Offset: 0x00092777
		private void OnEnable()
		{
			if (ExperienceManager.instance && ExperienceManager.instance != this)
			{
				Debug.LogError("Only one ExperienceManager can exist at a time.");
				return;
			}
			ExperienceManager.instance = this;
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000945A3 File Offset: 0x000927A3
		private void OnDisable()
		{
			if (ExperienceManager.instance == this)
			{
				ExperienceManager.instance = null;
			}
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000945B8 File Offset: 0x000927B8
		private void Start()
		{
			this.localTime = 0f;
			this.nextAward = float.PositiveInfinity;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000945D0 File Offset: 0x000927D0
		private void FixedUpdate()
		{
			this.localTime += Time.fixedDeltaTime;
			if (this.pendingAwards.Count > 0 && this.nextAward <= this.localTime)
			{
				this.nextAward = float.PositiveInfinity;
				for (int i = this.pendingAwards.Count - 1; i >= 0; i--)
				{
					if (this.pendingAwards[i].awardTime <= this.localTime)
					{
						if (TeamManager.instance)
						{
							TeamManager.instance.GiveTeamExperience(this.pendingAwards[i].recipient, this.pendingAwards[i].awardAmount);
						}
						this.pendingAwards.RemoveAt(i);
					}
					else if (this.pendingAwards[i].awardTime < this.nextAward)
					{
						this.nextAward = this.pendingAwards[i].awardTime;
					}
				}
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000946CC File Offset: 0x000928CC
		public void AwardExperience(Vector3 origin, CharacterBody body, ulong amount)
		{
			CharacterMaster master = body.master;
			if (!master)
			{
				return;
			}
			TeamIndex teamIndex = master.teamIndex;
			List<ulong> list = this.CalculateDenominations(amount);
			uint num = 0U;
			for (int i = 0; i < list.Count; i++)
			{
				this.AddPendingAward(this.localTime + 0.5f + 1.5f * ExperienceManager.orbTimeOffsetSequence[(int)num], teamIndex, list[i]);
				num += 1U;
				if ((ulong)num >= (ulong)((long)ExperienceManager.orbTimeOffsetSequence.Length))
				{
					num = 0U;
				}
			}
			ExperienceManager.currentOutgoingCreateExpEffectMessage.awardAmount = amount;
			ExperienceManager.currentOutgoingCreateExpEffectMessage.origin = origin;
			ExperienceManager.currentOutgoingCreateExpEffectMessage.targetBody = body.gameObject;
			NetworkServer.SendToAll(55, ExperienceManager.currentOutgoingCreateExpEffectMessage);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00094780 File Offset: 0x00092980
		private void AddPendingAward(float awardTime, TeamIndex recipient, ulong awardAmount)
		{
			this.pendingAwards.Add(new ExperienceManager.TimedExpAward
			{
				awardTime = awardTime,
				recipient = recipient,
				awardAmount = awardAmount
			});
			if (this.nextAward > awardTime)
			{
				this.nextAward = awardTime;
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000947CC File Offset: 0x000929CC
		public List<ulong> CalculateDenominations(ulong total)
		{
			List<ulong> list = new List<ulong>();
			while (total > 0UL)
			{
				ulong num = (ulong)Math.Pow(6.0, (double)Mathf.Floor(Mathf.Log(total, 6f)));
				total = Math.Max(total - num, 0UL);
				list.Add(num);
			}
			return list;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0009481D File Offset: 0x00092A1D
		[NetworkMessageHandler(msgType = 55, client = true)]
		private static void HandleCreateExpEffect(NetworkMessage netMsg)
		{
			if (ExperienceManager.instance)
			{
				ExperienceManager.instance.HandleCreateExpEffectInternal(netMsg);
			}
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x00094838 File Offset: 0x00092A38
		private void HandleCreateExpEffectInternal(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ExperienceManager.CreateExpEffectMessage>(ExperienceManager.currentIncomingCreateExpEffectMessage);
			if (!SettingsConVars.cvExpAndMoneyEffects.value)
			{
				return;
			}
			GameObject targetBody = ExperienceManager.currentIncomingCreateExpEffectMessage.targetBody;
			if (!targetBody)
			{
				return;
			}
			HurtBox hurtBox = Util.FindBodyMainHurtBox(targetBody);
			Transform targetTransform = hurtBox ? hurtBox.transform : targetBody.transform;
			List<ulong> list = this.CalculateDenominations(ExperienceManager.currentIncomingCreateExpEffectMessage.awardAmount);
			uint num = 0U;
			for (int i = 0; i < list.Count; i++)
			{
				ExperienceOrbBehavior component = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/ExpOrb"), ExperienceManager.currentIncomingCreateExpEffectMessage.origin, Quaternion.identity).GetComponent<ExperienceOrbBehavior>();
				component.targetTransform = targetTransform;
				component.travelTime = ExperienceManager.CalcOrbTravelTime(ExperienceManager.orbTimeOffsetSequence[(int)num]);
				component.exp = list[i];
				num += 1U;
				if ((ulong)num >= (ulong)((long)ExperienceManager.orbTimeOffsetSequence.Length))
				{
					num = 0U;
				}
			}
		}

		// Token: 0x0400275E RID: 10078
		private float localTime;

		// Token: 0x0400275F RID: 10079
		private List<ExperienceManager.TimedExpAward> pendingAwards = new List<ExperienceManager.TimedExpAward>();

		// Token: 0x04002760 RID: 10080
		private float nextAward;

		// Token: 0x04002761 RID: 10081
		private const float minOrbTravelTime = 0.5f;

		// Token: 0x04002762 RID: 10082
		public const float maxOrbTravelTime = 2f;

		// Token: 0x04002763 RID: 10083
		public static readonly float[] orbTimeOffsetSequence = new float[]
		{
			0.841f,
			0.394f,
			0.783f,
			0.799f,
			0.912f,
			0.197f,
			0.335f,
			0.768f,
			0.278f,
			0.554f,
			0.477f,
			0.629f,
			0.365f,
			0.513f,
			0.953f,
			0.917f
		};

		// Token: 0x04002764 RID: 10084
		private static ExperienceManager.CreateExpEffectMessage currentOutgoingCreateExpEffectMessage = new ExperienceManager.CreateExpEffectMessage();

		// Token: 0x04002765 RID: 10085
		private static ExperienceManager.CreateExpEffectMessage currentIncomingCreateExpEffectMessage = new ExperienceManager.CreateExpEffectMessage();

		// Token: 0x020006D1 RID: 1745
		[Serializable]
		private struct TimedExpAward
		{
			// Token: 0x04002766 RID: 10086
			public float awardTime;

			// Token: 0x04002767 RID: 10087
			public ulong awardAmount;

			// Token: 0x04002768 RID: 10088
			public TeamIndex recipient;
		}

		// Token: 0x020006D2 RID: 1746
		private class CreateExpEffectMessage : MessageBase
		{
			// Token: 0x0600226A RID: 8810 RVA: 0x00094958 File Offset: 0x00092B58
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.origin);
				writer.Write(this.targetBody);
				writer.WritePackedUInt64(this.awardAmount);
			}

			// Token: 0x0600226B RID: 8811 RVA: 0x0009497E File Offset: 0x00092B7E
			public override void Deserialize(NetworkReader reader)
			{
				this.origin = reader.ReadVector3();
				this.targetBody = reader.ReadGameObject();
				this.awardAmount = reader.ReadPackedUInt64();
			}

			// Token: 0x04002769 RID: 10089
			public Vector3 origin;

			// Token: 0x0400276A RID: 10090
			public GameObject targetBody;

			// Token: 0x0400276B RID: 10091
			public ulong awardAmount;
		}
	}
}
