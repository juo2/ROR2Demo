using System;
using EntityStates.LaserTurbine;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000793 RID: 1939
	[RequireComponent(typeof(GenericOwnership))]
	[RequireComponent(typeof(NetworkIdentity))]
	[RequireComponent(typeof(EntityStateMachine))]
	[RequireComponent(typeof(NetworkStateMachine))]
	public class LaserTurbineController : NetworkBehaviour
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x000B1973 File Offset: 0x000AFB73
		// (set) Token: 0x060028EC RID: 10476 RVA: 0x000B197B File Offset: 0x000AFB7B
		public float charge { get; private set; }

		// Token: 0x060028ED RID: 10477 RVA: 0x000B1984 File Offset: 0x000AFB84
		private void Awake()
		{
			this.genericOwnership = base.GetComponent<GenericOwnership>();
			this.genericOwnership.onOwnerChanged += this.OnOwnerChanged;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000B19A9 File Offset: 0x000AFBA9
		private void Update()
		{
			if (NetworkClient.active)
			{
				this.UpdateClient();
			}
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000B19B8 File Offset: 0x000AFBB8
		private void FixedUpdate()
		{
			int killChargeCount = 0;
			if (this.cachedOwnerBody)
			{
				killChargeCount = this.cachedOwnerBody.GetBuffCount(RoR2Content.Buffs.LaserTurbineKillCharge);
			}
			this.charge = this.CalcCurrentChargeValue(killChargeCount);
			if (this.turbineDisplayRoot)
			{
				this.turbineDisplayRoot.gameObject.SetActive(this.showTurbineDisplay);
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000B1A15 File Offset: 0x000AFC15
		private void OnEnable()
		{
			if (NetworkServer.active)
			{
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobalServer;
			}
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000B1A2F File Offset: 0x000AFC2F
		private void OnDisable()
		{
			GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobalServer;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000B1A44 File Offset: 0x000AFC44
		[Client]
		private void UpdateClient()
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.LaserTurbineController::UpdateClient()' called on server");
				return;
			}
			if (this.visualSpin <= this.charge)
			{
				this.visualSpin = this.charge;
			}
			else
			{
				this.visualSpin -= this.visualSpinDecayRate * Time.deltaTime;
			}
			this.visualSpin = Mathf.Max(this.visualSpin, 0f);
			float num = HGMath.CircleAreaToRadius(this.visualSpin * HGMath.CircleRadiusToArea(1f));
			this.chargeIndicator.localScale = new Vector3(num, num, num);
			Vector3 localEulerAngles = this.spinIndicator.localEulerAngles;
			localEulerAngles.y += this.visualSpin * Time.deltaTime * this.visualSpinRate;
			this.spinIndicator.localEulerAngles = localEulerAngles;
			AkSoundEngine.SetRTPCValue(this.spinRtpc, this.visualSpin * this.spinRtpcScale, base.gameObject);
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000B1B30 File Offset: 0x000AFD30
		[Server]
		public void ExpendCharge()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.LaserTurbineController::ExpendCharge()' called on client");
				return;
			}
			if (this.cachedOwnerBody)
			{
				this.cachedOwnerBody.ClearTimedBuffs(RoR2Content.Buffs.LaserTurbineKillCharge);
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000B1B64 File Offset: 0x000AFD64
		private void OnCharacterDeathGlobalServer(DamageReport damageReport)
		{
			if (damageReport.attacker == this.genericOwnership.ownerObject && damageReport.attacker != null)
			{
				this.OnOwnerKilledOtherServer();
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000B1B87 File Offset: 0x000AFD87
		private void OnOwnerKilledOtherServer()
		{
			if (this.cachedOwnerBody)
			{
				this.cachedOwnerBody.AddTimedBuff(RoR2Content.Buffs.LaserTurbineKillCharge, (float)RechargeState.killChargeDuration, RechargeState.killChargesRequired);
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000B1BB1 File Offset: 0x000AFDB1
		private void OnOwnerChanged(GameObject newOwner)
		{
			this.cachedOwnerBody = (newOwner ? newOwner.GetComponent<CharacterBody>() : null);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000B1BCA File Offset: 0x000AFDCA
		public float CalcCurrentChargeValue(int killChargeCount)
		{
			return Mathf.Clamp01((float)killChargeCount / (float)RechargeState.killChargesRequired);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000B1BF8 File Offset: 0x000AFDF8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002C6B RID: 11371
		public float visualSpinRate = 7200f;

		// Token: 0x04002C6C RID: 11372
		public Transform chargeIndicator;

		// Token: 0x04002C6D RID: 11373
		public Transform spinIndicator;

		// Token: 0x04002C6E RID: 11374
		public Transform turbineDisplayRoot;

		// Token: 0x04002C6F RID: 11375
		public bool showTurbineDisplay;

		// Token: 0x04002C70 RID: 11376
		public string spinRtpc;

		// Token: 0x04002C71 RID: 11377
		public float spinRtpcScale;

		// Token: 0x04002C72 RID: 11378
		public float visualSpin;

		// Token: 0x04002C73 RID: 11379
		public float visualSpinDecayRate = 0.2f;

		// Token: 0x04002C74 RID: 11380
		private GenericOwnership genericOwnership;

		// Token: 0x04002C76 RID: 11382
		private CharacterBody cachedOwnerBody;
	}
}
