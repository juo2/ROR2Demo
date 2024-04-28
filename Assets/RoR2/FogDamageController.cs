using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006DC RID: 1756
	public class FogDamageController : NetworkBehaviour
	{
		// Token: 0x060022A2 RID: 8866 RVA: 0x00095980 File Offset: 0x00093B80
		private void Start()
		{
			foreach (BaseZoneBehavior zone in this.initialSafeZones)
			{
				this.AddSafeZone(zone);
			}
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000959AD File Offset: 0x00093BAD
		public void AddSafeZone(IZone zone)
		{
			this.safeZones.Add(zone);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000959BB File Offset: 0x00093BBB
		public void RemoveSafeZone(IZone zone)
		{
			this.safeZones.Remove(zone);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000959CC File Offset: 0x00093BCC
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.damageTimer += Time.fixedDeltaTime;
				this.dictionaryValidationTimer += Time.fixedDeltaTime;
				if (this.dictionaryValidationTimer > 60f)
				{
					this.dictionaryValidationTimer = 0f;
					CharacterBody[] array = new CharacterBody[this.characterBodyToStacks.Keys.Count];
					this.characterBodyToStacks.Keys.CopyTo(array, 0);
					for (int i = 0; i < array.Length; i++)
					{
						if (!array[i])
						{
							this.characterBodyToStacks.Remove(array[i]);
						}
					}
				}
				while (this.damageTimer > this.tickPeriodSeconds)
				{
					this.damageTimer -= this.tickPeriodSeconds;
					if (this.teamFilter)
					{
						if (this.invertTeamFilter)
						{
							for (TeamIndex teamIndex = TeamIndex.Neutral; teamIndex < TeamIndex.Count; teamIndex += 1)
							{
								if (teamIndex != this.teamFilter.teamIndex && teamIndex != TeamIndex.None && teamIndex != TeamIndex.Neutral)
								{
									this.EvaluateTeam(teamIndex);
								}
							}
						}
						else
						{
							this.EvaluateTeam(this.teamFilter.teamIndex);
						}
					}
					else
					{
						for (TeamIndex teamIndex2 = TeamIndex.Neutral; teamIndex2 < TeamIndex.Count; teamIndex2 += 1)
						{
							this.EvaluateTeam(teamIndex2);
						}
					}
					foreach (KeyValuePair<CharacterBody, int> keyValuePair in this.characterBodyToStacks)
					{
						CharacterBody key = keyValuePair.Key;
						if (key && key.transform && key.healthComponent)
						{
							int num = keyValuePair.Value - 1;
							float num2 = this.healthFractionPerSecond * (1f + (float)num * this.healthFractionRampCoefficientPerSecond * this.tickPeriodSeconds) * this.tickPeriodSeconds * key.healthComponent.fullCombinedHealth;
							if (num2 > 0f)
							{
								key.healthComponent.TakeDamage(new DamageInfo
								{
									damage = num2,
									position = key.corePosition,
									damageType = (DamageType.BypassArmor | DamageType.BypassBlock),
									damageColorIndex = DamageColorIndex.Void
								});
							}
							if (this.dangerBuffDef)
							{
								key.AddTimedBuff(this.dangerBuffDef, this.dangerBuffDuration);
							}
						}
					}
				}
			}
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x00095C20 File Offset: 0x00093E20
		public IEnumerable<CharacterBody> GetAffectedBodies()
		{
			if (this.teamFilter)
			{
				if (this.invertTeamFilter)
				{
					TeamIndex teamIndex;
					for (TeamIndex currentTeam = TeamIndex.Neutral; currentTeam < TeamIndex.Count; currentTeam = teamIndex)
					{
						IEnumerable<CharacterBody> affectedBodiesOnTeam = this.GetAffectedBodiesOnTeam(currentTeam);
						foreach (CharacterBody characterBody in affectedBodiesOnTeam)
						{
							yield return characterBody;
						}
						IEnumerator<CharacterBody> enumerator = null;
						teamIndex = currentTeam + 1;
					}
				}
				else
				{
					IEnumerable<CharacterBody> affectedBodiesOnTeam2 = this.GetAffectedBodiesOnTeam(this.teamFilter.teamIndex);
					foreach (CharacterBody characterBody2 in affectedBodiesOnTeam2)
					{
						yield return characterBody2;
					}
					IEnumerator<CharacterBody> enumerator = null;
				}
			}
			else
			{
				TeamIndex teamIndex;
				for (TeamIndex currentTeam = TeamIndex.Neutral; currentTeam < TeamIndex.Count; currentTeam = teamIndex)
				{
					IEnumerable<CharacterBody> affectedBodiesOnTeam3 = this.GetAffectedBodiesOnTeam(currentTeam);
					foreach (CharacterBody characterBody3 in affectedBodiesOnTeam3)
					{
						yield return characterBody3;
					}
					IEnumerator<CharacterBody> enumerator = null;
					teamIndex = currentTeam + 1;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x00095C30 File Offset: 0x00093E30
		public IEnumerable<CharacterBody> GetAffectedBodiesOnTeam(TeamIndex teamIndex)
		{
			foreach (TeamComponent teamComponent in TeamComponent.GetTeamMembers(teamIndex))
			{
				CharacterBody body = teamComponent.body;
				bool flag = false;
				using (List<IZone>.Enumerator enumerator2 = this.safeZones.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.IsInBounds(teamComponent.transform.position))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					yield return body;
				}
			}
			IEnumerator<TeamComponent> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x00095C48 File Offset: 0x00093E48
		private void EvaluateTeam(TeamIndex teamIndex)
		{
			foreach (TeamComponent teamComponent in TeamComponent.GetTeamMembers(teamIndex))
			{
				CharacterBody body = teamComponent.body;
				bool flag = this.characterBodyToStacks.ContainsKey(body);
				bool flag2 = false;
				using (List<IZone>.Enumerator enumerator2 = this.safeZones.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.IsInBounds(teamComponent.transform.position))
						{
							flag2 = true;
							break;
						}
					}
				}
				if (flag2)
				{
					if (flag)
					{
						this.characterBodyToStacks.Remove(body);
					}
				}
				else if (!flag)
				{
					this.characterBodyToStacks.Add(body, 1);
				}
				else
				{
					Dictionary<CharacterBody, int> dictionary = this.characterBodyToStacks;
					CharacterBody key = body;
					dictionary[key]++;
				}
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x00095D64 File Offset: 0x00093F64
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040027C2 RID: 10178
		[Tooltip("Used to control which teams to damage.  If it's null, it damages ALL teams")]
		[SerializeField]
		private TeamFilter teamFilter;

		// Token: 0x040027C3 RID: 10179
		[SerializeField]
		[Tooltip("If true, it damages all OTHER teams than the one specified.  If false, it damages the specified team.")]
		private bool invertTeamFilter;

		// Token: 0x040027C4 RID: 10180
		[SerializeField]
		[Tooltip("The period in seconds in between each tick")]
		private float tickPeriodSeconds;

		// Token: 0x040027C5 RID: 10181
		[SerializeField]
		[Tooltip("The fraction of combined health to deduct per second.  Note that damage is actually applied per tick, not per second.")]
		[Range(0f, 1f)]
		private float healthFractionPerSecond;

		// Token: 0x040027C6 RID: 10182
		[SerializeField]
		[Tooltip("The coefficient to increase the damage by, for every tick they take outside the zone.")]
		private float healthFractionRampCoefficientPerSecond;

		// Token: 0x040027C7 RID: 10183
		[Tooltip("The buff to apply when in danger, i.e not in the safe zone.")]
		[SerializeField]
		private BuffDef dangerBuffDef;

		// Token: 0x040027C8 RID: 10184
		[SerializeField]
		private float dangerBuffDuration;

		// Token: 0x040027C9 RID: 10185
		[Tooltip("An initial list of safe zones behaviors which protect bodies from the fog")]
		[SerializeField]
		private BaseZoneBehavior[] initialSafeZones;

		// Token: 0x040027CA RID: 10186
		private float dictionaryValidationTimer;

		// Token: 0x040027CB RID: 10187
		private float damageTimer;

		// Token: 0x040027CC RID: 10188
		private List<IZone> safeZones = new List<IZone>();

		// Token: 0x040027CD RID: 10189
		private Dictionary<CharacterBody, int> characterBodyToStacks = new Dictionary<CharacterBody, int>();
	}
}
