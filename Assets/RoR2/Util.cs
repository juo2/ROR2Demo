using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Facepunch.Steamworks;
using JetBrains.Annotations;
using Rewired;
using RoR2.Networking;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000A97 RID: 2711
	public static class Util
	{
		// Token: 0x06003E23 RID: 15907 RVA: 0x00100378 File Offset: 0x000FE578
		public static void CleanseBody(CharacterBody characterBody, bool removeDebuffs, bool removeBuffs, bool removeCooldownBuffs, bool removeDots, bool removeStun, bool removeNearbyProjectiles)
		{
			if (removeDebuffs || removeBuffs || removeCooldownBuffs)
			{
				BuffIndex buffIndex = (BuffIndex)0;
				BuffIndex buffCount = (BuffIndex)BuffCatalog.buffCount;
				while (buffIndex < buffCount)
				{
					BuffDef buffDef = BuffCatalog.GetBuffDef(buffIndex);
					if ((buffDef.isDebuff && removeDebuffs) || (buffDef.isCooldown && removeCooldownBuffs) || (!buffDef.isDebuff && !buffDef.isCooldown && removeBuffs))
					{
						characterBody.ClearTimedBuffs(buffIndex);
					}
					buffIndex++;
				}
			}
			if (removeDots)
			{
				DotController.RemoveAllDots(characterBody.gameObject);
			}
			if (removeStun)
			{
				SetStateOnHurt component = characterBody.GetComponent<SetStateOnHurt>();
				if (component)
				{
					component.Cleanse();
				}
			}
			if (removeNearbyProjectiles)
			{
				float num = 6f;
				float num2 = num * num;
				TeamIndex teamIndex = characterBody.teamComponent.teamIndex;
				List<ProjectileController> instancesList = InstanceTracker.GetInstancesList<ProjectileController>();
				List<ProjectileController> list = new List<ProjectileController>();
				int i = 0;
				int count = instancesList.Count;
				while (i < count)
				{
					ProjectileController projectileController = instancesList[i];
					if (!projectileController.cannotBeDeleted && projectileController.teamFilter.teamIndex != teamIndex && (projectileController.transform.position - characterBody.corePosition).sqrMagnitude < num2)
					{
						list.Add(projectileController);
					}
					i++;
				}
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					ProjectileController projectileController2 = list[j];
					if (projectileController2)
					{
						UnityEngine.Object.Destroy(projectileController2.gameObject);
					}
					j++;
				}
			}
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x001004D0 File Offset: 0x000FE6D0
		public static WeightedSelection<DirectorCard> CreateReasonableDirectorCardSpawnList(float availableCredit, int maximumNumberToSpawnBeforeSkipping, int minimumToSpawn)
		{
			WeightedSelection<DirectorCard> monsterSelection = ClassicStageInfo.instance.monsterSelection;
			WeightedSelection<DirectorCard> weightedSelection = new WeightedSelection<DirectorCard>(8);
			for (int i = 0; i < monsterSelection.Count; i++)
			{
				DirectorCard value = monsterSelection.choices[i].value;
				float combatDirectorHighestEliteCostMultiplier = CombatDirector.CalcHighestEliteCostMultiplier(value.spawnCard.eliteRules);
				if (Util.DirectorCardIsReasonableChoice(availableCredit, maximumNumberToSpawnBeforeSkipping, minimumToSpawn, value, combatDirectorHighestEliteCostMultiplier))
				{
					weightedSelection.AddChoice(value, monsterSelection.choices[i].weight);
				}
			}
			return weightedSelection;
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x0010054C File Offset: 0x000FE74C
		public static bool DirectorCardIsReasonableChoice(float availableCredit, int maximumNumberToSpawnBeforeSkipping, int minimumToSpawn, DirectorCard card, float combatDirectorHighestEliteCostMultiplier)
		{
			float num = (float)(card.cost * maximumNumberToSpawnBeforeSkipping) * ((card.spawnCard as CharacterSpawnCard).noElites ? 1f : combatDirectorHighestEliteCostMultiplier);
			return card.IsAvailable() && (float)card.cost * (float)minimumToSpawn <= availableCredit && num / 2f > availableCredit;
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x001005A0 File Offset: 0x000FE7A0
		public static CharacterBody HurtBoxColliderToBody(Collider collider)
		{
			HurtBox component = collider.GetComponent<HurtBox>();
			if (component && component.healthComponent)
			{
				return component.healthComponent.body;
			}
			return null;
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x001005D6 File Offset: 0x000FE7D6
		public static float ConvertAmplificationPercentageIntoReductionPercentage(float amplificationPercentage)
		{
			return (1f - 100f / (100f + amplificationPercentage)) * 100f;
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x001005F4 File Offset: 0x000FE7F4
		public static Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
		{
			Vector3 rhs = vPoint - vA;
			Vector3 normalized = (vB - vA).normalized;
			float num = Vector3.Distance(vA, vB);
			float num2 = Vector3.Dot(normalized, rhs);
			if (num2 <= 0f)
			{
				return vA;
			}
			if (num2 >= num)
			{
				return vB;
			}
			Vector3 b = normalized * num2;
			return vA + b;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x0010064C File Offset: 0x000FE84C
		public static CharacterBody TryToCreateGhost(CharacterBody targetBody, CharacterBody ownerBody, int duration)
		{
			Util.<>c__DisplayClass7_0 CS$<>8__locals1 = new Util.<>c__DisplayClass7_0();
			CS$<>8__locals1.duration = duration;
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'RoR2.CharacterBody RoR2.Util::TryToCreateGhost(RoR2.CharacterBody, RoR2.CharacterBody, int)' called on client");
				return null;
			}
			if (!targetBody)
			{
				return null;
			}
			CS$<>8__locals1.bodyPrefab = BodyCatalog.FindBodyPrefab(targetBody);
			if (!CS$<>8__locals1.bodyPrefab)
			{
				return null;
			}
			CharacterMaster characterMaster = MasterCatalog.allAiMasters.FirstOrDefault((CharacterMaster master) => master.bodyPrefab == CS$<>8__locals1.bodyPrefab);
			if (!characterMaster)
			{
				return null;
			}
			MasterSummon masterSummon = new MasterSummon();
			masterSummon.masterPrefab = characterMaster.gameObject;
			masterSummon.ignoreTeamMemberLimit = false;
			masterSummon.position = targetBody.footPosition;
			CharacterDirection component = targetBody.GetComponent<CharacterDirection>();
			masterSummon.rotation = (component ? Quaternion.Euler(0f, component.yaw, 0f) : targetBody.transform.rotation);
			masterSummon.summonerBodyObject = (ownerBody ? ownerBody.gameObject : null);
			masterSummon.inventoryToCopy = targetBody.inventory;
			masterSummon.useAmbientLevel = null;
			masterSummon.preSpawnSetupCallback = (Action<CharacterMaster>)Delegate.Combine(masterSummon.preSpawnSetupCallback, new Action<CharacterMaster>(CS$<>8__locals1.<TryToCreateGhost>g__PreSpawnSetup|1));
			CharacterMaster characterMaster2 = masterSummon.Perform();
			if (!characterMaster2)
			{
				return null;
			}
			CharacterBody body = characterMaster2.GetBody();
			if (body)
			{
				foreach (EntityStateMachine entityStateMachine in body.GetComponents<EntityStateMachine>())
				{
					entityStateMachine.initialStateType = entityStateMachine.mainStateType;
				}
			}
			return body;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x001007BC File Offset: 0x000FE9BC
		public static float OnHitProcDamage(float damageThatProccedIt, float damageStat, float damageCoefficient)
		{
			float b = damageThatProccedIt * damageCoefficient;
			return Mathf.Max(1f, b);
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x001007D8 File Offset: 0x000FE9D8
		public static float OnKillProcDamage(float baseDamage, float damageCoefficient)
		{
			return baseDamage * damageCoefficient;
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x001007E0 File Offset: 0x000FE9E0
		public static Quaternion QuaternionSafeLookRotation(Vector3 forward)
		{
			Quaternion result = Quaternion.identity;
			if (forward.sqrMagnitude > Mathf.Epsilon)
			{
				result = Quaternion.LookRotation(forward);
			}
			return result;
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x0010080C File Offset: 0x000FEA0C
		public static Quaternion QuaternionSafeLookRotation(Vector3 forward, Vector3 upwards)
		{
			Quaternion result = Quaternion.identity;
			if (forward.sqrMagnitude > Mathf.Epsilon)
			{
				result = Quaternion.LookRotation(forward, upwards);
			}
			return result;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x00100838 File Offset: 0x000FEA38
		public static bool HasParameterOfType(Animator animator, string name, AnimatorControllerParameterType type)
		{
			foreach (AnimatorControllerParameter animatorControllerParameter in animator.parameters)
			{
				if (animatorControllerParameter.type == type && animatorControllerParameter.name == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00100878 File Offset: 0x000FEA78
		public static uint PlaySound(string soundString, GameObject gameObject)
		{
			if (string.IsNullOrEmpty(soundString))
			{
				return 0U;
			}
			return AkSoundEngine.PostEvent(soundString, gameObject);
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x0010088C File Offset: 0x000FEA8C
		public static uint PlaySound(string soundString, GameObject gameObject, string RTPCstring, float RTPCvalue)
		{
			uint num = Util.PlaySound(soundString, gameObject);
			if (num != 0U)
			{
				AkSoundEngine.SetRTPCValueByPlayingID(RTPCstring, RTPCvalue, num);
			}
			return num;
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x001008B0 File Offset: 0x000FEAB0
		public static uint PlayAttackSpeedSound(string soundString, GameObject gameObject, float attackSpeedStat)
		{
			uint num = Util.PlaySound(soundString, gameObject);
			if (num != 0U)
			{
				float in_value = Util.CalculateAttackSpeedRtpcValue(attackSpeedStat);
				AkSoundEngine.SetRTPCValueByPlayingID("attackSpeed", in_value, num);
			}
			return num;
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x001008E0 File Offset: 0x000FEAE0
		public static float CalculateAttackSpeedRtpcValue(float attackSpeedStat)
		{
			float num = Mathf.Log(attackSpeedStat, 2f);
			return 1200f * num / 96f + 50f;
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x0010090C File Offset: 0x000FEB0C
		public static void RotateAwayFromWalls(float raycastLength, int raycastCount, Vector3 raycastOrigin, Transform referenceTransform)
		{
			float num = 360f / (float)raycastCount;
			float angle = 0f;
			float num2 = 0f;
			for (int i = 0; i < raycastCount; i++)
			{
				Vector3 direction = Quaternion.Euler(0f, num * (float)i, 0f) * Vector3.forward;
				float num3 = raycastLength;
				RaycastHit raycastHit;
				if (Physics.Raycast(raycastOrigin, direction, out raycastHit, raycastLength, LayerIndex.world.mask))
				{
					num3 = raycastHit.distance;
				}
				if (raycastHit.distance > num2)
				{
					angle = num * (float)i;
					num2 = num3;
				}
			}
			referenceTransform.Rotate(Vector3.up, angle, Space.Self);
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x001009A4 File Offset: 0x000FEBA4
		public static string GetActionDisplayString(ActionElementMap actionElementMap)
		{
			if (actionElementMap == null)
			{
				return "";
			}
			string elementIdentifierName = actionElementMap.elementIdentifierName;
			if (elementIdentifierName == "Left Mouse Button")
			{
				return "M1";
			}
			if (elementIdentifierName == "Right Mouse Button")
			{
				return "M2";
			}
			if (!(elementIdentifierName == "Left Shift"))
			{
				return actionElementMap.elementIdentifierName;
			}
			return "Shift";
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x00100A02 File Offset: 0x000FEC02
		public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
		{
			return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x0007235F File Offset: 0x0007055F
		public static float Remap(float value, float inMin, float inMax, float outMin, float outMax)
		{
			return outMin + (value - inMin) / (inMax - inMin) * (outMax - outMin);
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x00100A24 File Offset: 0x000FEC24
		public static bool HasAnimationParameter(string paramName, Animator animator)
		{
			AnimatorControllerParameter[] parameters = animator.parameters;
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i].name == paramName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x00100A5C File Offset: 0x000FEC5C
		public static bool HasAnimationParameter(int paramHash, Animator animator)
		{
			int i = 0;
			int parameterCount = animator.parameterCount;
			while (i < parameterCount)
			{
				if (animator.GetParameter(i).nameHash == paramHash)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x00100A90 File Offset: 0x000FEC90
		public static bool CheckRoll(float percentChance, float luck = 0f, CharacterMaster effectOriginMaster = null)
		{
			if (percentChance <= 0f)
			{
				return false;
			}
			int num = Mathf.CeilToInt(Mathf.Abs(luck));
			float num2 = UnityEngine.Random.Range(0f, 100f);
			float num3 = num2;
			for (int i = 0; i < num; i++)
			{
				float b = UnityEngine.Random.Range(0f, 100f);
				num2 = ((luck > 0f) ? Mathf.Min(num2, b) : Mathf.Max(num2, b));
			}
			if (num2 <= percentChance)
			{
				if (num3 > percentChance && effectOriginMaster)
				{
					GameObject bodyObject = effectOriginMaster.GetBodyObject();
					if (bodyObject)
					{
						CharacterBody component = bodyObject.GetComponent<CharacterBody>();
						if (component)
						{
							component.wasLucky = true;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x00100B3B File Offset: 0x000FED3B
		public static bool CheckRoll(float percentChance, CharacterMaster master)
		{
			return Util.CheckRoll(percentChance, master ? master.luck : 0f, master);
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x00100B5C File Offset: 0x000FED5C
		public static float EstimateSurfaceDistance(Collider a, Collider b)
		{
			Vector3 center = a.bounds.center;
			Vector3 center2 = b.bounds.center;
			RaycastHit raycastHit;
			Vector3 a2;
			if (b.Raycast(new Ray(center, center2 - center), out raycastHit, float.PositiveInfinity))
			{
				a2 = raycastHit.point;
			}
			else
			{
				a2 = b.ClosestPointOnBounds(center);
			}
			Vector3 b2;
			if (a.Raycast(new Ray(center2, center - center2), out raycastHit, float.PositiveInfinity))
			{
				b2 = raycastHit.point;
			}
			else
			{
				b2 = a.ClosestPointOnBounds(center2);
			}
			return Vector3.Distance(a2, b2);
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x00100BEF File Offset: 0x000FEDEF
		public static bool HasEffectiveAuthority(GameObject gameObject)
		{
			return gameObject && Util.HasEffectiveAuthority(gameObject.GetComponent<NetworkIdentity>());
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x00100C06 File Offset: 0x000FEE06
		public static bool HasEffectiveAuthority(NetworkIdentity networkIdentity)
		{
			return networkIdentity && (networkIdentity.hasAuthority || (NetworkServer.active && networkIdentity.clientAuthorityOwner == null));
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x00100C2E File Offset: 0x000FEE2E
		public static float CalculateSphereVolume(float radius)
		{
			return 4.1887903f * radius * radius * radius;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x00100C3B File Offset: 0x000FEE3B
		public static float CalculateCylinderVolume(float radius, float height)
		{
			return 3.1415927f * radius * radius * height;
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x00100C48 File Offset: 0x000FEE48
		public static float CalculateColliderVolume(Collider collider)
		{
			Vector3 lossyScale = collider.transform.lossyScale;
			float num = lossyScale.x * lossyScale.y * lossyScale.z;
			float num2 = 0f;
			if (collider is BoxCollider)
			{
				Vector3 size = ((BoxCollider)collider).size;
				num2 = size.x * size.y * size.z;
			}
			else if (collider is SphereCollider)
			{
				num2 = Util.CalculateSphereVolume(((SphereCollider)collider).radius);
			}
			else if (collider is CapsuleCollider)
			{
				CapsuleCollider capsuleCollider = (CapsuleCollider)collider;
				float radius = capsuleCollider.radius;
				float num3 = Util.CalculateSphereVolume(radius);
				float num4 = Mathf.Max(capsuleCollider.height - num3, 0f);
				float num5 = 3.1415927f * radius * radius * num4;
				num2 = num3 + num5;
			}
			else if (collider is CharacterController)
			{
				CharacterController characterController = (CharacterController)collider;
				float radius2 = characterController.radius;
				float num6 = Util.CalculateSphereVolume(radius2);
				float num7 = Mathf.Max(characterController.height - num6, 0f);
				float num8 = 3.1415927f * radius2 * radius2 * num7;
				num2 = num6 + num8;
			}
			return num2 * num;
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x00100D60 File Offset: 0x000FEF60
		public static Vector3 RandomColliderVolumePoint(Collider collider)
		{
			Transform transform = collider.transform;
			Vector3 vector = Vector3.zero;
			if (collider is BoxCollider)
			{
				BoxCollider boxCollider = (BoxCollider)collider;
				Vector3 size = boxCollider.size;
				Vector3 center = boxCollider.center;
				vector = new Vector3(center.x + UnityEngine.Random.Range(size.x * -0.5f, size.x * 0.5f), center.y + UnityEngine.Random.Range(size.y * -0.5f, size.y * 0.5f), center.z + UnityEngine.Random.Range(size.z * -0.5f, size.z * 0.5f));
			}
			else if (collider is SphereCollider)
			{
				SphereCollider sphereCollider = (SphereCollider)collider;
				vector = sphereCollider.center + UnityEngine.Random.insideUnitSphere * sphereCollider.radius;
			}
			else if (collider is CapsuleCollider)
			{
				CapsuleCollider capsuleCollider = (CapsuleCollider)collider;
				float radius = capsuleCollider.radius;
				float num = Mathf.Max(capsuleCollider.height - radius, 0f);
				float num2 = Util.CalculateSphereVolume(radius);
				float num3 = Util.CalculateCylinderVolume(radius, num);
				float max = num2 + num3;
				if (UnityEngine.Random.Range(0f, max) <= num2)
				{
					vector = UnityEngine.Random.insideUnitSphere * radius;
					float num4 = ((float)UnityEngine.Random.Range(0, 2) * 2f - 1f) * num * 0.5f;
					switch (capsuleCollider.direction)
					{
					case 0:
						vector.x += num4;
						break;
					case 1:
						vector.y += num4;
						break;
					case 2:
						vector.z += num4;
						break;
					}
				}
				else
				{
					Vector2 vector2 = UnityEngine.Random.insideUnitCircle * radius;
					float num5 = UnityEngine.Random.Range(num * -0.5f, num * 0.5f);
					switch (capsuleCollider.direction)
					{
					case 0:
						vector = new Vector3(num5, vector2.x, vector2.y);
						break;
					case 1:
						vector = new Vector3(vector2.x, num5, vector2.y);
						break;
					case 2:
						vector = new Vector3(vector2.x, vector2.y, num5);
						break;
					}
				}
				vector += capsuleCollider.center;
			}
			else if (collider is CharacterController)
			{
				CharacterController characterController = (CharacterController)collider;
				float radius2 = characterController.radius;
				float num6 = Mathf.Max(characterController.height - radius2, 0f);
				float num7 = Util.CalculateSphereVolume(radius2);
				float num8 = Util.CalculateCylinderVolume(radius2, num6);
				float max2 = num7 + num8;
				if (UnityEngine.Random.Range(0f, max2) <= num7)
				{
					vector = UnityEngine.Random.insideUnitSphere * radius2;
					float num9 = ((float)UnityEngine.Random.Range(0, 2) * 2f - 1f) * num6 * 0.5f;
					vector.y += num9;
				}
				else
				{
					Vector2 vector3 = UnityEngine.Random.insideUnitCircle * radius2;
					float y = UnityEngine.Random.Range(num6 * -0.5f, num6 * 0.5f);
					vector = new Vector3(vector3.x, y, vector3.y);
				}
				vector += characterController.center;
			}
			return transform.TransformPoint(vector);
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x001010B0 File Offset: 0x000FF2B0
		public static CharacterBody GetFriendlyEasyTarget(CharacterBody casterBody, Ray aimRay, float maxDistance, float maxDeviation = 20f)
		{
			TeamIndex teamIndex = TeamIndex.Neutral;
			TeamComponent component = casterBody.GetComponent<TeamComponent>();
			if (component)
			{
				teamIndex = component.teamIndex;
			}
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
			Vector3 origin = aimRay.origin;
			Vector3 direction = aimRay.direction;
			List<Util.EasyTargetCandidate> candidatesList = new List<Util.EasyTargetCandidate>(teamMembers.Count);
			List<int> list = new List<int>(teamMembers.Count);
			float num = Mathf.Cos(maxDeviation * 0.017453292f);
			for (int i = 0; i < teamMembers.Count; i++)
			{
				Transform transform = teamMembers[i].transform;
				Vector3 a2 = transform.position - origin;
				float magnitude = a2.magnitude;
				float num2 = Vector3.Dot(a2 * (1f / magnitude), direction);
				CharacterBody component2 = transform.GetComponent<CharacterBody>();
				if (num2 >= num && component2 != casterBody)
				{
					float num3 = 1f / magnitude;
					float score = num2 + num3;
					candidatesList.Add(new Util.EasyTargetCandidate
					{
						transform = transform,
						score = score,
						distance = magnitude
					});
					list.Add(list.Count);
				}
			}
			list.Sort(delegate(int a, int b)
			{
				float score2 = candidatesList[a].score;
				float score3 = candidatesList[b].score;
				if (score2 == score3)
				{
					return 0;
				}
				if (score2 <= score3)
				{
					return 1;
				}
				return -1;
			});
			for (int j = 0; j < list.Count; j++)
			{
				int index = list[j];
				CharacterBody component3 = candidatesList[index].transform.GetComponent<CharacterBody>();
				if (component3 && component3 != casterBody)
				{
					return component3;
				}
			}
			return null;
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x00101248 File Offset: 0x000FF448
		public static CharacterBody GetEnemyEasyTarget(CharacterBody casterBody, Ray aimRay, float maxDistance, float maxDeviation = 20f)
		{
			TeamIndex teamIndex = TeamIndex.Neutral;
			TeamComponent component = casterBody.GetComponent<TeamComponent>();
			if (component)
			{
				teamIndex = component.teamIndex;
			}
			List<TeamComponent> list = new List<TeamComponent>();
			for (TeamIndex teamIndex2 = TeamIndex.Neutral; teamIndex2 < TeamIndex.Count; teamIndex2 += 1)
			{
				if (teamIndex2 != teamIndex)
				{
					list.AddRange(TeamComponent.GetTeamMembers(teamIndex2));
				}
			}
			Vector3 origin = aimRay.origin;
			Vector3 direction = aimRay.direction;
			List<Util.EasyTargetCandidate> candidatesList = new List<Util.EasyTargetCandidate>(list.Count);
			List<int> list2 = new List<int>(list.Count);
			float num = Mathf.Cos(maxDeviation * 0.017453292f);
			for (int i = 0; i < list.Count; i++)
			{
				Transform transform = list[i].transform;
				Vector3 a2 = transform.position - origin;
				float magnitude = a2.magnitude;
				float num2 = Vector3.Dot(a2 * (1f / magnitude), direction);
				CharacterBody component2 = transform.GetComponent<CharacterBody>();
				if (num2 >= num && component2 != casterBody && magnitude < maxDistance)
				{
					float num3 = 1f / magnitude;
					float score = num2 + num3;
					candidatesList.Add(new Util.EasyTargetCandidate
					{
						transform = transform,
						score = score,
						distance = magnitude
					});
					list2.Add(list2.Count);
				}
			}
			list2.Sort(delegate(int a, int b)
			{
				float score2 = candidatesList[a].score;
				float score3 = candidatesList[b].score;
				if (score2 == score3)
				{
					return 0;
				}
				if (score2 <= score3)
				{
					return 1;
				}
				return -1;
			});
			for (int j = 0; j < list2.Count; j++)
			{
				int index = list2[j];
				CharacterBody component3 = candidatesList[index].transform.GetComponent<CharacterBody>();
				if (component3 && component3 != casterBody)
				{
					return component3;
				}
			}
			return null;
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x00101408 File Offset: 0x000FF608
		public static float GetBodyPrefabFootOffset(GameObject prefab)
		{
			CapsuleCollider component = prefab.GetComponent<CapsuleCollider>();
			if (component)
			{
				return component.height * 0.5f - component.center.y;
			}
			return 0f;
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x00101444 File Offset: 0x000FF644
		public static void ShuffleList<T>(List<T> list)
		{
			for (int i = list.Count - 1; i > 0; i--)
			{
				int index = UnityEngine.Random.Range(0, i + 1);
				T value = list[i];
				list[i] = list[index];
				list[index] = value;
			}
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x0010148C File Offset: 0x000FF68C
		public static void ShuffleList<T>(List<T> list, Xoroshiro128Plus rng)
		{
			for (int i = list.Count - 1; i > 0; i--)
			{
				int index = rng.RangeInt(0, i + 1);
				T value = list[i];
				list[i] = list[index];
				list[index] = value;
			}
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x001014D8 File Offset: 0x000FF6D8
		public static void ShuffleArray<T>(T[] array)
		{
			for (int i = array.Length - 1; i > 0; i--)
			{
				int num = UnityEngine.Random.Range(0, i + 1);
				T t = array[i];
				array[i] = array[num];
				array[num] = t;
			}
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x00101520 File Offset: 0x000FF720
		public static void ShuffleArray<T>(T[] array, Xoroshiro128Plus rng)
		{
			for (int i = array.Length - 1; i > 0; i--)
			{
				int num = rng.RangeInt(0, i + 1);
				T t = array[i];
				array[i] = array[num];
				array[num] = t;
			}
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x00101568 File Offset: 0x000FF768
		public static Transform FindNearest(Vector3 position, List<Transform> transformsList, float range = float.PositiveInfinity)
		{
			Transform result = null;
			float num = range * range;
			for (int i = 0; i < transformsList.Count; i++)
			{
				float sqrMagnitude = (transformsList[i].position - position).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					result = transformsList[i];
				}
			}
			return result;
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x001015B8 File Offset: 0x000FF7B8
		public static Vector3 ApplySpread(Vector3 aimDirection, float minSpread, float maxSpread, float spreadYawScale, float spreadPitchScale, float bonusYaw = 0f, float bonusPitch = 0f)
		{
			Vector3 up = Vector3.up;
			Vector3 axis = Vector3.Cross(up, aimDirection);
			float x = UnityEngine.Random.Range(minSpread, maxSpread);
			float z = UnityEngine.Random.Range(0f, 360f);
			Vector3 vector = Quaternion.Euler(0f, 0f, z) * (Quaternion.Euler(x, 0f, 0f) * Vector3.forward);
			float y = vector.y;
			vector.y = 0f;
			float angle = (Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f + bonusYaw) * spreadYawScale;
			float angle2 = (Mathf.Atan2(y, vector.magnitude) * 57.29578f + bonusPitch) * spreadPitchScale;
			return Quaternion.AngleAxis(angle, up) * (Quaternion.AngleAxis(angle2, axis) * aimDirection);
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x0010168C File Offset: 0x000FF88C
		public static string GenerateColoredString(string str, Color32 color)
		{
			return string.Format(CultureInfo.InvariantCulture, "<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", new object[]
			{
				color.r,
				color.g,
				color.b,
				str
			});
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x001016DC File Offset: 0x000FF8DC
		public static bool GuessRenderBounds(GameObject gameObject, out Bounds bounds)
		{
			Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
			if (componentsInChildren.Length != 0)
			{
				bounds = componentsInChildren[0].bounds;
				for (int i = 1; i < componentsInChildren.Length; i++)
				{
					bounds.Encapsulate(componentsInChildren[i].bounds);
				}
				return true;
			}
			bounds = new Bounds(gameObject.transform.position, Vector3.zero);
			return false;
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x0010173C File Offset: 0x000FF93C
		public static bool GuessRenderBoundsMeshOnly(GameObject gameObject, out Bounds bounds)
		{
			Renderer[] array = (from renderer in gameObject.GetComponentsInChildren<Renderer>()
			where renderer is MeshRenderer || renderer is SkinnedMeshRenderer
			select renderer).ToArray<Renderer>();
			if (array.Length != 0)
			{
				bounds = array[0].bounds;
				for (int i = 1; i < array.Length; i++)
				{
					bounds.Encapsulate(array[i].bounds);
				}
				return true;
			}
			bounds = new Bounds(gameObject.transform.position, Vector3.zero);
			return false;
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x001017C5 File Offset: 0x000FF9C5
		public static GameObject FindNetworkObject(NetworkInstanceId networkInstanceId)
		{
			if (NetworkServer.active)
			{
				return NetworkServer.FindLocalObject(networkInstanceId);
			}
			return ClientScene.FindLocalObject(networkInstanceId);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x001017DC File Offset: 0x000FF9DC
		public static string GetGameObjectHierarchyName(GameObject gameObject)
		{
			int num = 0;
			Transform transform = gameObject.transform;
			while (transform)
			{
				num++;
				transform = transform.parent;
			}
			string[] array = new string[num];
			Transform transform2 = gameObject.transform;
			while (transform2)
			{
				array[--num] = transform2.gameObject.name;
				transform2 = transform2.parent;
			}
			return string.Join("/", array);
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00101844 File Offset: 0x000FFA44
		public static string GetBestBodyName(GameObject bodyObject)
		{
			CharacterBody characterBody = null;
			string text = "???";
			if (bodyObject)
			{
				characterBody = bodyObject.GetComponent<CharacterBody>();
			}
			if (characterBody)
			{
				text = characterBody.GetUserName();
			}
			else
			{
				IDisplayNameProvider component = bodyObject.GetComponent<IDisplayNameProvider>();
				if (component != null)
				{
					text = component.GetDisplayName();
				}
			}
			string text2 = text;
			if (characterBody)
			{
				if (characterBody.isElite)
				{
					foreach (BuffIndex buffIndex in BuffCatalog.eliteBuffIndices)
					{
						if (characterBody.HasBuff(buffIndex))
						{
							text2 = Language.GetStringFormatted(BuffCatalog.GetBuffDef(buffIndex).eliteDef.modifierToken, new object[]
							{
								text2
							});
						}
					}
				}
				if (characterBody.inventory)
				{
					if (characterBody.inventory.GetItemCount(RoR2Content.Items.InvadingDoppelganger) > 0)
					{
						text2 = Language.GetStringFormatted("BODY_MODIFIER_DOPPELGANGER", new object[]
						{
							text2
						});
					}
					if (characterBody.inventory.GetItemCount(DLC1Content.Items.GummyCloneIdentifier) > 0)
					{
						text2 = Language.GetStringFormatted("BODY_MODIFIER_GUMMYCLONE", new object[]
						{
							text2
						});
					}
				}
			}
			return text2;
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x0010194C File Offset: 0x000FFB4C
		public static string GetBestBodyNameColored(GameObject bodyObject)
		{
			if (bodyObject)
			{
				CharacterBody component = bodyObject.GetComponent<CharacterBody>();
				if (component)
				{
					CharacterMaster master = component.master;
					if (master)
					{
						PlayerCharacterMasterController component2 = master.GetComponent<PlayerCharacterMasterController>();
						if (component2)
						{
							GameObject networkUserObject = component2.networkUserObject;
							if (networkUserObject)
							{
								NetworkUser component3 = networkUserObject.GetComponent<NetworkUser>();
								if (component3)
								{
									return Util.GenerateColoredString(component3.userName, component3.userColor);
								}
							}
						}
					}
				}
				IDisplayNameProvider component4 = bodyObject.GetComponent<IDisplayNameProvider>();
				if (component4 != null)
				{
					return component4.GetDisplayName();
				}
			}
			return "???";
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x001019DC File Offset: 0x000FFBDC
		public static string GetBestMasterName(CharacterMaster characterMaster)
		{
			if (characterMaster)
			{
				PlayerCharacterMasterController playerCharacterMasterController = characterMaster.playerCharacterMasterController;
				string text;
				if (playerCharacterMasterController == null)
				{
					text = null;
				}
				else
				{
					NetworkUser networkUser = playerCharacterMasterController.networkUser;
					text = ((networkUser != null) ? networkUser.userName : null);
				}
				string text2 = text;
				if (text2 == null)
				{
					GameObject bodyPrefab = characterMaster.bodyPrefab;
					string text3 = (bodyPrefab != null) ? bodyPrefab.GetComponent<CharacterBody>().baseNameToken : null;
					if (text3 != null)
					{
						text2 = Language.GetString(text3);
					}
				}
				return text2;
			}
			return "Null Master";
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x00101A3C File Offset: 0x000FFC3C
		public static NetworkUser LookUpBodyNetworkUser(GameObject bodyObject)
		{
			if (bodyObject)
			{
				return Util.LookUpBodyNetworkUser(bodyObject.GetComponent<CharacterBody>());
			}
			return null;
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x00101A54 File Offset: 0x000FFC54
		public static NetworkUser LookUpBodyNetworkUser(CharacterBody characterBody)
		{
			if (characterBody)
			{
				CharacterMaster master = characterBody.master;
				if (master)
				{
					PlayerCharacterMasterController component = master.GetComponent<PlayerCharacterMasterController>();
					if (component)
					{
						GameObject networkUserObject = component.networkUserObject;
						if (networkUserObject)
						{
							NetworkUser component2 = networkUserObject.GetComponent<NetworkUser>();
							if (component2)
							{
								return component2;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00101AA8 File Offset: 0x000FFCA8
		private static bool HandleCharacterPhysicsCastResults(GameObject bodyObject, Ray ray, RaycastHit[] hits, out RaycastHit hitInfo)
		{
			int num = -1;
			float num2 = float.PositiveInfinity;
			for (int i = 0; i < hits.Length; i++)
			{
				float distance = hits[i].distance;
				if (distance < num2)
				{
					HurtBox component = hits[i].collider.GetComponent<HurtBox>();
					if (component)
					{
						HealthComponent healthComponent = component.healthComponent;
						if (healthComponent && healthComponent.gameObject == bodyObject)
						{
							goto IL_82;
						}
					}
					if (distance == 0f)
					{
						hitInfo = hits[i];
						hitInfo.point = ray.origin;
						return true;
					}
					num = i;
					num2 = distance;
				}
				IL_82:;
			}
			if (num == -1)
			{
				hitInfo = default(RaycastHit);
				return false;
			}
			hitInfo = hits[num];
			return true;
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x00101B5C File Offset: 0x000FFD5C
		public static bool CharacterRaycast(GameObject bodyObject, Ray ray, out RaycastHit hitInfo, float maxDistance, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, layerMask, queryTriggerInteraction);
			return Util.HandleCharacterPhysicsCastResults(bodyObject, ray, hits, out hitInfo);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00101B84 File Offset: 0x000FFD84
		public static bool CharacterSpherecast(GameObject bodyObject, Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit[] hits = Physics.SphereCastAll(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
			return Util.HandleCharacterPhysicsCastResults(bodyObject, ray, hits, out hitInfo);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x00101BAD File Offset: 0x000FFDAD
		public static bool ConnectionIsLocal([NotNull] NetworkConnection conn)
		{
			return !(conn is SteamNetworkConnection) && !(conn is EOSNetworkConnection) && conn.GetType() != typeof(NetworkConnection);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00101BD8 File Offset: 0x000FFDD8
		public static string EscapeRichTextForTextMeshPro(string rtString)
		{
			string str = rtString.Replace("<", "</noparse><noparse><</noparse><noparse>");
			return "<noparse>" + str + "</noparse>";
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00101C08 File Offset: 0x000FFE08
		public static string EscapeQuotes(string str)
		{
			str = Util.backlashSearch.Replace(str, Util.strBackslashBackslash);
			str = Util.quoteSearch.Replace(str, Util.strBackslashQuote);
			return str;
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00101C2F File Offset: 0x000FFE2F
		public static string RGBToHex(Color32 rgb)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0:X2}{1:X2}{2:X2}", rgb.r, rgb.g, rgb.b);
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x00101C61 File Offset: 0x000FFE61
		public static Vector2 Vector3XZToVector2XY(Vector3 vector3)
		{
			return new Vector2(vector3.x, vector3.z);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x00101C61 File Offset: 0x000FFE61
		public static Vector2 Vector3XZToVector2XY(ref Vector3 vector3)
		{
			return new Vector2(vector3.x, vector3.z);
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x00101C74 File Offset: 0x000FFE74
		public static void Vector3XZToVector2XY(Vector3 vector3, out Vector2 vector2)
		{
			vector2.x = vector3.x;
			vector2.y = vector3.z;
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x00101C74 File Offset: 0x000FFE74
		public static void Vector3XZToVector2XY(ref Vector3 vector3, out Vector2 vector2)
		{
			vector2.x = vector3.x;
			vector2.y = vector3.z;
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x00101C90 File Offset: 0x000FFE90
		public static Vector2 RotateVector2(Vector2 vector2, float degrees)
		{
			float num = Mathf.Sin(degrees * 0.017453292f);
			float num2 = Mathf.Cos(degrees * 0.017453292f);
			return new Vector2(num2 * vector2.x - num * vector2.y, num * vector2.x + num2 * vector2.y);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x00101CE0 File Offset: 0x000FFEE0
		public static Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
		{
			float num = Quaternion.Angle(current, target);
			num = Mathf.SmoothDamp(0f, num, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
			return Quaternion.RotateTowards(current, target, num);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x00101D10 File Offset: 0x000FFF10
		public static Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref float currentVelocity, float smoothTime)
		{
			float num = Quaternion.Angle(current, target);
			num = Mathf.SmoothDamp(0f, num, ref currentVelocity, smoothTime);
			return Quaternion.RotateTowards(current, target, num);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x00101D3B File Offset: 0x000FFF3B
		public static HurtBox FindBodyMainHurtBox(CharacterBody characterBody)
		{
			return characterBody.mainHurtBox;
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x00101D44 File Offset: 0x000FFF44
		public static HurtBox FindBodyMainHurtBox(GameObject bodyObject)
		{
			CharacterBody component = bodyObject.GetComponent<CharacterBody>();
			if (component)
			{
				return Util.FindBodyMainHurtBox(component);
			}
			return null;
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x00101D68 File Offset: 0x000FFF68
		public static Vector3 GetCorePosition(CharacterBody characterBody)
		{
			return characterBody.corePosition;
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x00101D70 File Offset: 0x000FFF70
		public static Vector3 GetCorePosition(GameObject bodyObject)
		{
			CharacterBody component = bodyObject.GetComponent<CharacterBody>();
			if (component)
			{
				return Util.GetCorePosition(component);
			}
			return bodyObject.transform.position;
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00101DA0 File Offset: 0x000FFFA0
		public static Transform GetCoreTransform(GameObject bodyObject)
		{
			CharacterBody component = bodyObject.GetComponent<CharacterBody>();
			if (component)
			{
				return component.coreTransform;
			}
			return bodyObject.transform;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x00101DC9 File Offset: 0x000FFFC9
		public static float SphereRadiusToVolume(float radius)
		{
			return 4.1887903f * (radius * radius * radius);
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00101DD6 File Offset: 0x000FFFD6
		public static float SphereVolumeToRadius(float volume)
		{
			return Mathf.Pow(3f * volume / 12.566371f, 0.33333334f);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x00101DF0 File Offset: 0x000FFFF0
		public static void CopyList<T>(List<T> src, List<T> dest)
		{
			dest.Clear();
			foreach (T item in src)
			{
				dest.Add(item);
			}
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00101E44 File Offset: 0x00100044
		public static float ScanCharacterAnimationClipForMomentOfRootMotionStop(GameObject characterPrefab, string clipName, string rootBoneNameInChildLocator)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(characterPrefab);
			Transform modelTransform = gameObject.GetComponent<ModelLocator>().modelTransform;
			Transform transform = modelTransform.GetComponent<ChildLocator>().FindChild(rootBoneNameInChildLocator);
			AnimationClip animationClip = modelTransform.GetComponent<Animator>().runtimeAnimatorController.animationClips.FirstOrDefault((AnimationClip c) => c.name == clipName);
			float result = 1f;
			animationClip.SampleAnimation(gameObject, 0f);
			Vector3 b = transform.position;
			for (float num = 0.1f; num < 1f; num += 0.1f)
			{
				animationClip.SampleAnimation(gameObject, num);
				Vector3 position = transform.position;
				if ((position - b).magnitude == 0f)
				{
					result = num;
					break;
				}
				b = position;
			}
			UnityEngine.Object.Destroy(gameObject);
			return result;
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x00101F14 File Offset: 0x00100114
		public static void DebugCross(Vector3 position, float radius, UnityEngine.Color color, float duration)
		{
			Debug.DrawLine(position - Vector3.right * radius, position + Vector3.right * radius, color, duration);
			Debug.DrawLine(position - Vector3.up * radius, position + Vector3.up * radius, color, duration);
			Debug.DrawLine(position - Vector3.forward * radius, position + Vector3.forward * radius, color, duration);
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x00101F9C File Offset: 0x0010019C
		public static void DebugBounds(Bounds bounds, UnityEngine.Color color, float duration)
		{
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			Vector3 start = new Vector3(min.x, min.y, min.z);
			Vector3 vector = new Vector3(min.x, min.y, max.z);
			Vector3 vector2 = new Vector3(min.x, max.y, min.z);
			Vector3 end = new Vector3(min.x, max.y, max.z);
			Vector3 vector3 = new Vector3(max.x, min.y, min.z);
			Vector3 vector4 = new Vector3(max.x, min.y, max.z);
			Vector3 end2 = new Vector3(max.x, max.y, min.z);
			Vector3 start2 = new Vector3(max.x, max.y, max.z);
			Debug.DrawLine(start, vector, color, duration);
			Debug.DrawLine(start, vector3, color, duration);
			Debug.DrawLine(start, vector2, color, duration);
			Debug.DrawLine(vector2, end, color, duration);
			Debug.DrawLine(vector2, end2, color, duration);
			Debug.DrawLine(start2, end, color, duration);
			Debug.DrawLine(start2, end2, color, duration);
			Debug.DrawLine(start2, vector4, color, duration);
			Debug.DrawLine(vector4, vector3, color, duration);
			Debug.DrawLine(vector4, vector, color, duration);
			Debug.DrawLine(vector, end, color, duration);
			Debug.DrawLine(vector3, end2, color, duration);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x001020FC File Offset: 0x001002FC
		public static bool PositionIsValid(Vector3 value)
		{
			float f = value.x + value.y + value.z;
			return !float.IsInfinity(f) && !float.IsNaN(f);
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x00102134 File Offset: 0x00100334
		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x0010215C File Offset: 0x0010035C
		public static DateTime UnixTimeStampToDateTimeUtc(uint unixTimeStamp)
		{
			return Util.dateZero.AddSeconds(unixTimeStamp).ToUniversalTime();
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00102183 File Offset: 0x00100383
		public static double GetCurrentUnixEpochTimeInSeconds()
		{
			return Client.Instance.Utils.GetServerRealTime();
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00102196 File Offset: 0x00100396
		public static bool IsValid(UnityEngine.Object o)
		{
			return o;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x001021A0 File Offset: 0x001003A0
		public static string BuildPrefabTransformPath(Transform root, Transform transform, bool appendCloneSuffix = false, bool includeRoot = false)
		{
			Util.<>c__DisplayClass92_0 CS$<>8__locals1;
			CS$<>8__locals1.appendCloneSuffix = appendCloneSuffix;
			if (!root)
			{
				throw new ArgumentException("Value provided as 'root' is an invalid object.", "root");
			}
			if (!transform)
			{
				throw new ArgumentException("Value provided as 'transform' is an invalid object.", "transform");
			}
			string result;
			try
			{
				for (Transform transform2 = transform; transform2 != root; transform2 = transform2.parent)
				{
					if (!transform2)
					{
						string arg = Util.<BuildPrefabTransformPath>g__TakeResult|92_0(ref CS$<>8__locals1);
						throw new ArgumentException(string.Format("\"{0}\" is not a child of \"{1}\".", arg, root));
					}
					if (Util.sharedStringStack.Count > 0)
					{
						Util.sharedStringStack.Push("/");
					}
					Util.sharedStringStack.Push(transform2.gameObject.name);
				}
				if (includeRoot)
				{
					if (Util.sharedStringStack.Count > 0)
					{
						Util.sharedStringStack.Push("/");
					}
					Util.sharedStringStack.Push(root.gameObject.name);
				}
				result = Util.<BuildPrefabTransformPath>g__TakeResult|92_0(ref CS$<>8__locals1);
			}
			finally
			{
				Util.sharedStringBuilder.Clear();
				Util.sharedStringStack.Clear();
			}
			return result;
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x001022B0 File Offset: 0x001004B0
		public static int GetItemCountForTeam(TeamIndex teamIndex, ItemIndex itemIndex, bool requiresAlive, bool requiresConnected = true)
		{
			int num = 0;
			ReadOnlyCollection<CharacterMaster> readOnlyInstancesList = CharacterMaster.readOnlyInstancesList;
			int i = 0;
			int count = readOnlyInstancesList.Count;
			while (i < count)
			{
				CharacterMaster characterMaster = readOnlyInstancesList[i];
				if (characterMaster.teamIndex == teamIndex && (!requiresAlive || characterMaster.hasBody) && (!requiresConnected || !characterMaster.playerCharacterMasterController || characterMaster.playerCharacterMasterController.isConnected))
				{
					num += characterMaster.inventory.GetItemCount(itemIndex);
				}
				i++;
			}
			return num;
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x00102328 File Offset: 0x00100528
		public static int GetItemCountGlobal(ItemIndex itemIndex, bool requiresAlive, bool requiresConnected = true)
		{
			int num = 0;
			ReadOnlyCollection<CharacterMaster> readOnlyInstancesList = CharacterMaster.readOnlyInstancesList;
			int i = 0;
			int count = readOnlyInstancesList.Count;
			while (i < count)
			{
				CharacterMaster characterMaster = readOnlyInstancesList[i];
				if ((!requiresAlive || characterMaster.hasBody) && (!requiresConnected || !characterMaster.playerCharacterMasterController || characterMaster.playerCharacterMasterController.isConnected))
				{
					num += characterMaster.inventory.GetItemCount(itemIndex);
				}
				i++;
			}
			return num;
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x00102395 File Offset: 0x00100595
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NullifyIfInvalid<T>(ref T objRef) where T : UnityEngine.Object
		{
			if (objRef == null)
			{
				return;
			}
			if (!objRef)
			{
				objRef = default(T);
			}
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x001023C0 File Offset: 0x001005C0
		public static bool IsPrefab(GameObject gameObject)
		{
			return gameObject && !gameObject.scene.IsValid();
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x001023E8 File Offset: 0x001005E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint IntToUintPlusOne(int value)
		{
			return (uint)(value + 1);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x001023ED File Offset: 0x001005ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int UintToIntMinusOne(uint value)
		{
			return (int)(value - 1U);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x001023F2 File Offset: 0x001005F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong LongToUlongPlusOne(long value)
		{
			return (ulong)(value + 1L);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x001023F8 File Offset: 0x001005F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long UlongToLongMinusOne(ulong value)
		{
			return (long)(value - 1UL);
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x00102400 File Offset: 0x00100600
		public static float GetExpAdjustedDropChancePercent(float baseChancePercent, GameObject characterBodyObject)
		{
			int num = 0;
			DeathRewards component = characterBodyObject.GetComponent<DeathRewards>();
			if (component)
			{
				num = component.spawnValue;
			}
			return baseChancePercent * Mathf.Log((float)num + 1f, 2f);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x0010243C File Offset: 0x0010063C
		public static bool IsDontDestroyOnLoad(GameObject go)
		{
			return go.scene.name == "DontDestroyOnLoad";
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x00102500 File Offset: 0x00100700
		[CompilerGenerated]
		internal static string <BuildPrefabTransformPath>g__TakeResult|92_0(ref Util.<>c__DisplayClass92_0 A_0)
		{
			while (Util.sharedStringStack.Count > 0)
			{
				Util.sharedStringBuilder.Append(Util.sharedStringStack.Pop());
			}
			if (A_0.appendCloneSuffix)
			{
				Util.sharedStringBuilder.Append(Util.cloneString);
			}
			return Util.sharedStringBuilder.Take();
		}

		// Token: 0x04003CC9 RID: 15561
		public const string attackSpeedRtpcName = "attackSpeed";

		// Token: 0x04003CCA RID: 15562
		private static readonly string strBackslash = "\\";

		// Token: 0x04003CCB RID: 15563
		private static readonly string strBackslashBackslash = Util.strBackslash + Util.strBackslash;

		// Token: 0x04003CCC RID: 15564
		private static readonly string strQuote = "\"";

		// Token: 0x04003CCD RID: 15565
		private static readonly string strBackslashQuote = Util.strBackslash + Util.strQuote;

		// Token: 0x04003CCE RID: 15566
		private static readonly Regex backlashSearch = new Regex(Util.strBackslashBackslash);

		// Token: 0x04003CCF RID: 15567
		private static readonly Regex quoteSearch = new Regex(Util.strQuote);

		// Token: 0x04003CD0 RID: 15568
		public static readonly DateTime dateZero = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04003CD1 RID: 15569
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();

		// Token: 0x04003CD2 RID: 15570
		private static readonly Stack<string> sharedStringStack = new Stack<string>();

		// Token: 0x04003CD3 RID: 15571
		private static readonly string cloneString = "(Clone)";

		// Token: 0x02000A98 RID: 2712
		private struct EasyTargetCandidate
		{
			// Token: 0x04003CD4 RID: 15572
			public Transform transform;

			// Token: 0x04003CD5 RID: 15573
			public float score;

			// Token: 0x04003CD6 RID: 15574
			public float distance;
		}
	}
}
