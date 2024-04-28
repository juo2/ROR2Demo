using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200094A RID: 2378
	public struct LayerIndex
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060035DD RID: 13789 RVA: 0x000E38E0 File Offset: 0x000E1AE0
		public LayerMask mask
		{
			get
			{
				return (this.intVal >= 0) ? (1 << this.intVal) : this.intVal;
			}
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000E3904 File Offset: 0x000E1B04
		static LayerIndex()
		{
			for (int i = 0; i < 32; i++)
			{
				string text = LayerMask.LayerToName(i);
				if (text != "" && (LayerIndex.assignedLayerMask & 1U << i) == 0U)
				{
					Debug.LogWarningFormat("Layer \"{0}\" is defined in this project's \"Tags and Layers\" settings but is not defined in LayerIndex!", new object[]
					{
						text
					});
				}
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x000E3ACE File Offset: 0x000E1CCE
		public LayerMask collisionMask
		{
			get
			{
				return LayerIndex.collisionMasks[this.intVal];
			}
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000E3AE0 File Offset: 0x000E1CE0
		private static LayerIndex GetLayerIndex(string layerName)
		{
			LayerIndex layerIndex = new LayerIndex
			{
				intVal = LayerMask.NameToLayer(layerName)
			};
			if (layerIndex.intVal == LayerIndex.invalidLayer.intVal)
			{
				Debug.LogErrorFormat("Layer \"{0}\" is not defined in this project's \"Tags and Layers\" settings.", new object[]
				{
					layerName
				});
			}
			else
			{
				LayerIndex.assignedLayerMask |= 1U << layerIndex.intVal;
			}
			return layerIndex;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000E3B44 File Offset: 0x000E1D44
		private static LayerMask[] CalcCollisionMasks()
		{
			LayerMask[] array = new LayerMask[32];
			for (int i = 0; i < 32; i++)
			{
				LayerMask layerMask = default(LayerMask);
				for (int j = 0; j < 32; j++)
				{
					if (!Physics.GetIgnoreLayerCollision(i, j))
					{
						layerMask |= 1 << j;
					}
				}
				array[i] = layerMask;
			}
			return array;
		}

		// Token: 0x04003680 RID: 13952
		public int intVal;

		// Token: 0x04003681 RID: 13953
		private static uint assignedLayerMask = 0U;

		// Token: 0x04003682 RID: 13954
		public static readonly LayerIndex invalidLayer = new LayerIndex
		{
			intVal = -1
		};

		// Token: 0x04003683 RID: 13955
		public static readonly LayerIndex defaultLayer = LayerIndex.GetLayerIndex("Default");

		// Token: 0x04003684 RID: 13956
		public static readonly LayerIndex transparentFX = LayerIndex.GetLayerIndex("TransparentFX");

		// Token: 0x04003685 RID: 13957
		public static readonly LayerIndex ignoreRaycast = LayerIndex.GetLayerIndex("Ignore Raycast");

		// Token: 0x04003686 RID: 13958
		public static readonly LayerIndex water = LayerIndex.GetLayerIndex("Water");

		// Token: 0x04003687 RID: 13959
		public static readonly LayerIndex ui = LayerIndex.GetLayerIndex("UI");

		// Token: 0x04003688 RID: 13960
		public static readonly LayerIndex fakeActor = LayerIndex.GetLayerIndex("FakeActor");

		// Token: 0x04003689 RID: 13961
		public static readonly LayerIndex noCollision = LayerIndex.GetLayerIndex("NoCollision");

		// Token: 0x0400368A RID: 13962
		public static readonly LayerIndex pickups = LayerIndex.GetLayerIndex("Pickups");

		// Token: 0x0400368B RID: 13963
		public static readonly LayerIndex world = LayerIndex.GetLayerIndex("World");

		// Token: 0x0400368C RID: 13964
		public static readonly LayerIndex entityPrecise = LayerIndex.GetLayerIndex("EntityPrecise");

		// Token: 0x0400368D RID: 13965
		public static readonly LayerIndex debris = LayerIndex.GetLayerIndex("Debris");

		// Token: 0x0400368E RID: 13966
		public static readonly LayerIndex projectile = LayerIndex.GetLayerIndex("Projectile");

		// Token: 0x0400368F RID: 13967
		public static readonly LayerIndex manualRender = LayerIndex.GetLayerIndex("ManualRender");

		// Token: 0x04003690 RID: 13968
		public static readonly LayerIndex collideWithCharacterHullOnly = LayerIndex.GetLayerIndex("CollideWithCharacterHullOnly");

		// Token: 0x04003691 RID: 13969
		public static readonly LayerIndex ragdoll = LayerIndex.GetLayerIndex("Ragdoll");

		// Token: 0x04003692 RID: 13970
		public static readonly LayerIndex noDraw = LayerIndex.GetLayerIndex("NoDraw");

		// Token: 0x04003693 RID: 13971
		public static readonly LayerIndex prefabBrush = LayerIndex.GetLayerIndex("PrefabBrush");

		// Token: 0x04003694 RID: 13972
		public static readonly LayerIndex postProcess = LayerIndex.GetLayerIndex("PostProcess");

		// Token: 0x04003695 RID: 13973
		public static readonly LayerIndex uiWorldSpace = LayerIndex.GetLayerIndex("UI, WorldSpace");

		// Token: 0x04003696 RID: 13974
		public static readonly string modderMessage = "Layers below are used only by console versions.";

		// Token: 0x04003697 RID: 13975
		public static readonly LayerIndex playerBody = LayerIndex.GetLayerIndex("PlayerBody");

		// Token: 0x04003698 RID: 13976
		public static readonly LayerIndex enemyBody = LayerIndex.GetLayerIndex("EnemyBody");

		// Token: 0x04003699 RID: 13977
		public static readonly LayerIndex triggerZone = LayerIndex.GetLayerIndex("TriggerZone");

		// Token: 0x0400369A RID: 13978
		private static readonly LayerMask[] collisionMasks = LayerIndex.CalcCollisionMasks();

		// Token: 0x0200094B RID: 2379
		public static class CommonMasks
		{
			// Token: 0x0400369B RID: 13979
			public static readonly LayerMask bullet = LayerIndex.world.mask | LayerIndex.entityPrecise.mask;

			// Token: 0x0400369C RID: 13980
			public static readonly LayerMask interactable = LayerIndex.defaultLayer.mask | LayerIndex.world.mask | LayerIndex.pickups.mask;
		}
	}
}
