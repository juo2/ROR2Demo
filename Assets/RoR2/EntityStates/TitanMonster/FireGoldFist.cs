using System;
using RoR2;
using UnityEngine;

namespace EntityStates.TitanMonster
{
	// Token: 0x02000364 RID: 868
	public class FireGoldFist : FireFist
	{
		// Token: 0x06000F9A RID: 3994 RVA: 0x000449B4 File Offset: 0x00042BB4
		protected override void PlacePredictedAttack()
		{
			int num = 0;
			Vector3 predictedTargetPosition = this.predictedTargetPosition;
			Vector3 a = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f) * Vector3.forward;
			for (int i = -(FireGoldFist.fistCount / 2); i < FireGoldFist.fistCount / 2; i++)
			{
				Vector3 vector = predictedTargetPosition + a * FireGoldFist.distanceBetweenFists * (float)i;
				float num2 = 60f;
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(vector + Vector3.up * (num2 / 2f), Vector3.down), out raycastHit, num2, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					vector = raycastHit.point;
				}
				base.PlaceSingleDelayBlast(vector, FireGoldFist.delayBetweenFists * (float)num);
				num++;
			}
		}

		// Token: 0x040013D5 RID: 5077
		public static int fistCount;

		// Token: 0x040013D6 RID: 5078
		public static float distanceBetweenFists;

		// Token: 0x040013D7 RID: 5079
		public static float delayBetweenFists;
	}
}
