using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000681 RID: 1665
	public class ConvertPlayerMoneyToExperience : MonoBehaviour
	{
		// Token: 0x06002088 RID: 8328 RVA: 0x0008BE7A File Offset: 0x0008A07A
		private void Start()
		{
			if (!NetworkServer.active)
			{
				Debug.LogErrorFormat("Component {0} can only be added on the server!", new object[]
				{
					base.GetType().Name
				});
				UnityEngine.Object.Destroy(this);
				return;
			}
			this.burstTimer = 0f;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0008BEB4 File Offset: 0x0008A0B4
		private void FixedUpdate()
		{
			this.burstTimer -= Time.fixedDeltaTime;
			if (this.burstTimer <= 0f)
			{
				bool flag = false;
				ReadOnlyCollection<PlayerCharacterMasterController> instances = PlayerCharacterMasterController.instances;
				for (int i = 0; i < instances.Count; i++)
				{
					GameObject gameObject = instances[i].gameObject;
					CharacterMaster component = gameObject.GetComponent<CharacterMaster>();
					uint num;
					if (!this.burstSizes.TryGetValue(gameObject, out num))
					{
						num = (uint)Mathf.CeilToInt(component.money / (float)this.burstCount);
						this.burstSizes[gameObject] = num;
					}
					if (num > component.money)
					{
						num = component.money;
					}
					component.money -= num;
					GameObject bodyObject = component.GetBodyObject();
					ulong num2 = (ulong)(num / 2f / (float)instances.Count);
					if (num > 0U)
					{
						flag = true;
					}
					if (bodyObject)
					{
						ExperienceManager.instance.AwardExperience(base.transform.position, bodyObject.GetComponent<CharacterBody>(), num2);
					}
					else
					{
						TeamManager.instance.GiveTeamExperience(component.teamIndex, num2);
					}
				}
				if (flag)
				{
					this.burstTimer = this.burstInterval;
					return;
				}
				if (this.burstTimer < -2.5f)
				{
					UnityEngine.Object.Destroy(this);
				}
			}
		}

		// Token: 0x040025CE RID: 9678
		private Dictionary<GameObject, uint> burstSizes = new Dictionary<GameObject, uint>();

		// Token: 0x040025CF RID: 9679
		private float burstTimer;

		// Token: 0x040025D0 RID: 9680
		public float burstInterval = 0.25f;

		// Token: 0x040025D1 RID: 9681
		public int burstCount = 8;
	}
}
