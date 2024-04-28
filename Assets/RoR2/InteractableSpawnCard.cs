using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000541 RID: 1345
	[CreateAssetMenu(menuName = "RoR2/SpawnCards/InteractableSpawnCard")]
	public class InteractableSpawnCard : SpawnCard
	{
		// Token: 0x0600187B RID: 6267 RVA: 0x0006ACAC File Offset: 0x00068EAC
		protected override void Spawn(Vector3 position, Quaternion rotation, DirectorSpawnRequest directorSpawnRequest, ref SpawnCard.SpawnResult result)
		{
			ulong nextUlong = directorSpawnRequest.rng.nextUlong;
			InteractableSpawnCard.rng.ResetSeed(nextUlong);
			if (this.skipSpawnWhenSacrificeArtifactEnabled && RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.sacrificeArtifactDef))
			{
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, position, rotation);
			Transform transform = gameObject.transform;
			if (this.orientToFloor)
			{
				Vector3 up = gameObject.transform.up;
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(position + up * InteractableSpawnCard.floorOffset, -up), out raycastHit, InteractableSpawnCard.raycastLength + InteractableSpawnCard.floorOffset, LayerIndex.world.mask))
				{
					transform.up = raycastHit.normal;
				}
			}
			transform.Rotate(Vector3.up, InteractableSpawnCard.rng.RangeFloat(0f, 360f), Space.Self);
			if (this.slightlyRandomizeOrientation)
			{
				transform.Translate(Vector3.down * 0.3f, Space.Self);
				transform.rotation *= Quaternion.Euler(InteractableSpawnCard.rng.RangeFloat(-30f, 30f), InteractableSpawnCard.rng.RangeFloat(-30f, 30f), InteractableSpawnCard.rng.RangeFloat(-30f, 30f));
			}
			NetworkServer.Spawn(gameObject);
			result.spawnedInstance = gameObject;
			result.success = true;
		}

		// Token: 0x04001E0D RID: 7693
		[Tooltip("Whether or not to orient the object to the normal of the ground it spawns on.")]
		public bool orientToFloor;

		// Token: 0x04001E0E RID: 7694
		[Tooltip("Slightly tweaks the rotation for things like chests and barrels so it looks more natural.")]
		public bool slightlyRandomizeOrientation;

		// Token: 0x04001E0F RID: 7695
		public bool skipSpawnWhenSacrificeArtifactEnabled;

		// Token: 0x04001E10 RID: 7696
		[Tooltip("When Sacrifice is enabled, this is multiplied by the card's weight")]
		public float weightScalarWhenSacrificeArtifactEnabled = 1f;

		// Token: 0x04001E11 RID: 7697
		[Tooltip("Won't spawn more than this many per stage.  If it's negative, there's no cap")]
		public int maxSpawnsPerStage = -1;

		// Token: 0x04001E12 RID: 7698
		private static readonly float floorOffset = 3f;

		// Token: 0x04001E13 RID: 7699
		private static readonly float raycastLength = 6f;

		// Token: 0x04001E14 RID: 7700
		private static readonly Xoroshiro128Plus rng = new Xoroshiro128Plus(0UL);
	}
}
