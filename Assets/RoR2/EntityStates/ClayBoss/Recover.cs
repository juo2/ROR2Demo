﻿using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ClayBoss
{
	// Token: 0x02000408 RID: 1032
	public class Recover : BaseState
	{
		// Token: 0x06001290 RID: 4752 RVA: 0x00052E40 File Offset: 0x00051040
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			if (NetworkServer.active && base.characterBody)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.ArmorBoost);
			}
			if (base.modelLocator)
			{
				ChildLocator component = base.modelLocator.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					this.muzzleTransform = component.FindChild("MuzzleMulch");
				}
			}
			this.subState = Recover.SubState.Entry;
			base.PlayCrossfade("Body", "PrepSiphon", "PrepSiphon.playbackRate", Recover.entryDuration, 0.1f);
			this.soundID = Util.PlayAttackSpeedSound(Recover.enterSoundString, base.gameObject, this.attackSpeedStat);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00052EFC File Offset: 0x000510FC
		private void FireTethers()
		{
			Vector3 position = this.muzzleTransform.position;
			float breakDistanceSqr = Recover.maxTetherDistance * Recover.maxTetherDistance;
			List<GameObject> list = new List<GameObject>();
			this.tetherControllers = new List<TarTetherController>();
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = position;
			bullseyeSearch.maxDistanceFilter = Recover.maxTetherDistance;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.filterByLoS = true;
			bullseyeSearch.searchDirection = Vector3.up;
			bullseyeSearch.RefreshCandidates();
			bullseyeSearch.FilterOutGameObject(base.gameObject);
			List<HurtBox> list2 = bullseyeSearch.GetResults().ToList<HurtBox>();
			Debug.Log(list2);
			for (int i = 0; i < list2.Count; i++)
			{
				GameObject gameObject = list2[i].healthComponent.gameObject;
				if (gameObject)
				{
					list.Add(gameObject);
				}
			}
			float tickInterval = 1f / Recover.damageTickFrequency;
			float damageCoefficientPerTick = Recover.damagePerSecond / Recover.damageTickFrequency;
			float mulchDistanceSqr = Recover.tetherMulchDistance * Recover.tetherMulchDistance;
			GameObject original = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/TarTether");
			for (int j = 0; j < list.Count; j++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original, position, Quaternion.identity);
				TarTetherController component = gameObject2.GetComponent<TarTetherController>();
				component.NetworkownerRoot = base.gameObject;
				component.NetworktargetRoot = list[j];
				component.breakDistanceSqr = breakDistanceSqr;
				component.damageCoefficientPerTick = damageCoefficientPerTick;
				component.tickInterval = tickInterval;
				component.tickTimer = (float)j * 0.1f;
				component.mulchDistanceSqr = mulchDistanceSqr;
				component.mulchDamageScale = Recover.tetherMulchDamageScale;
				component.mulchTickIntervalScale = Recover.tetherMulchTickIntervalScale;
				this.tetherControllers.Add(component);
				NetworkServer.Spawn(gameObject2);
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000530A8 File Offset: 0x000512A8
		private void DestroyTethers()
		{
			if (this.tetherControllers != null)
			{
				for (int i = this.tetherControllers.Count - 1; i >= 0; i--)
				{
					if (this.tetherControllers[i])
					{
						EntityState.Destroy(this.tetherControllers[i].gameObject);
					}
				}
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00053100 File Offset: 0x00051300
		public override void OnExit()
		{
			this.DestroyTethers();
			if (this.mulchEffect)
			{
				EntityState.Destroy(this.mulchEffect);
			}
			AkSoundEngine.StopPlayingID(this.soundID);
			Util.PlaySound(Recover.stopMulchSoundString, base.gameObject);
			if (NetworkServer.active && base.characterBody)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.ArmorBoost);
			}
			base.OnExit();
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00053174 File Offset: 0x00051374
		private static void RemoveDeadTethersFromList(List<TarTetherController> tethersList)
		{
			for (int i = tethersList.Count - 1; i >= 0; i--)
			{
				if (!tethersList[i])
				{
					tethersList.RemoveAt(i);
				}
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000531AC File Offset: 0x000513AC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.subState == Recover.SubState.Entry)
			{
				if (this.stopwatch >= Recover.entryDuration)
				{
					this.subState = Recover.SubState.Tethers;
					this.stopwatch = 0f;
					this.PlayAnimation("Body", "ChannelSiphon");
					Util.PlaySound(Recover.beginMulchSoundString, base.gameObject);
					if (NetworkServer.active)
					{
						this.FireTethers();
						this.mulchEffect = UnityEngine.Object.Instantiate<GameObject>(Recover.mulchEffectPrefab, this.muzzleTransform.position, Quaternion.identity);
						ChildLocator component = this.mulchEffect.gameObject.GetComponent<ChildLocator>();
						if (component)
						{
							Transform transform = component.FindChild("AreaIndicator");
							if (transform)
							{
								transform.localScale = new Vector3(Recover.maxTetherDistance * 2f, Recover.maxTetherDistance * 2f, Recover.maxTetherDistance * 2f);
							}
						}
						this.mulchEffect.transform.parent = this.muzzleTransform;
						return;
					}
				}
			}
			else if (this.subState == Recover.SubState.Tethers && NetworkServer.active)
			{
				Recover.RemoveDeadTethersFromList(this.tetherControllers);
				if ((this.stopwatch >= Recover.duration || this.tetherControllers.Count == 0) && base.isAuthority)
				{
					this.outer.SetNextState(new RecoverExit());
					return;
				}
			}
		}

		// Token: 0x040017ED RID: 6125
		public static float duration = 15f;

		// Token: 0x040017EE RID: 6126
		public static float maxTetherDistance = 40f;

		// Token: 0x040017EF RID: 6127
		public static float tetherMulchDistance = 5f;

		// Token: 0x040017F0 RID: 6128
		public static float tetherMulchDamageScale = 2f;

		// Token: 0x040017F1 RID: 6129
		public static float tetherMulchTickIntervalScale = 0.5f;

		// Token: 0x040017F2 RID: 6130
		public static float damagePerSecond = 2f;

		// Token: 0x040017F3 RID: 6131
		public static float damageTickFrequency = 3f;

		// Token: 0x040017F4 RID: 6132
		public static float entryDuration = 1f;

		// Token: 0x040017F5 RID: 6133
		public static GameObject mulchEffectPrefab;

		// Token: 0x040017F6 RID: 6134
		public static string enterSoundString;

		// Token: 0x040017F7 RID: 6135
		public static string beginMulchSoundString;

		// Token: 0x040017F8 RID: 6136
		public static string stopMulchSoundString;

		// Token: 0x040017F9 RID: 6137
		private GameObject mulchEffect;

		// Token: 0x040017FA RID: 6138
		private Transform muzzleTransform;

		// Token: 0x040017FB RID: 6139
		private List<TarTetherController> tetherControllers;

		// Token: 0x040017FC RID: 6140
		private float stopwatch;

		// Token: 0x040017FD RID: 6141
		private uint soundID;

		// Token: 0x040017FE RID: 6142
		private Recover.SubState subState;

		// Token: 0x02000409 RID: 1033
		private enum SubState
		{
			// Token: 0x04001800 RID: 6144
			Entry,
			// Token: 0x04001801 RID: 6145
			Tethers
		}
	}
}
