using System;
using System.Collections.Generic;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000682 RID: 1666
	public class Corpse : MonoBehaviour
	{
		// Token: 0x0600208B RID: 8331 RVA: 0x0008C01A File Offset: 0x0008A21A
		private void CollectRenderers()
		{
			if (this.renderers == null)
			{
				this.renderers = base.GetComponentsInChildren<Renderer>();
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0008C030 File Offset: 0x0008A230
		private void OnEnable()
		{
			Corpse.instancesList.Add(this);
			if (Corpse.disposalMode == Corpse.DisposalMode.OutOfSight)
			{
				this.CollectRenderers();
			}
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x0008C04B File Offset: 0x0008A24B
		private void OnDisable()
		{
			Corpse.instancesList.Remove(this);
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0008C059 File Offset: 0x0008A259
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void StaticInit()
		{
			RoR2Application.onUpdate += Corpse.StaticUpdate;
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x0008C06C File Offset: 0x0008A26C
		private static void IncrementCurrentCheckIndex()
		{
			Corpse.currentCheckIndex++;
			if (Corpse.currentCheckIndex >= Corpse.instancesList.Count)
			{
				Corpse.currentCheckIndex = 0;
			}
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0008C094 File Offset: 0x0008A294
		private static bool CheckCorpseOutOfSight(Corpse corpse)
		{
			foreach (Renderer renderer in corpse.renderers)
			{
				if (renderer && renderer.isVisible)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x0008C0D0 File Offset: 0x0008A2D0
		private static void StaticUpdate()
		{
			if (Corpse.maxCorpses < 0)
			{
				return;
			}
			int num = Corpse.instancesList.Count - Corpse.maxCorpses;
			int num2 = Math.Min(Math.Min(num, Corpse.maxChecksPerUpdate), Corpse.instancesList.Count);
			Corpse.DisposalMode disposalMode = Corpse.disposalMode;
			if (disposalMode == Corpse.DisposalMode.Hard)
			{
				for (int i = num - 1; i >= 0; i--)
				{
					Corpse.DestroyCorpse(Corpse.instancesList[i]);
				}
				return;
			}
			if (disposalMode != Corpse.DisposalMode.OutOfSight)
			{
				return;
			}
			for (int j = 0; j < num2; j++)
			{
				Corpse.IncrementCurrentCheckIndex();
				if (Corpse.CheckCorpseOutOfSight(Corpse.instancesList[Corpse.currentCheckIndex]))
				{
					Corpse.DestroyCorpse(Corpse.instancesList[Corpse.currentCheckIndex]);
				}
			}
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x0008C17E File Offset: 0x0008A37E
		private static void DestroyCorpse(Corpse corpse)
		{
			if (corpse)
			{
				UnityEngine.Object.Destroy(corpse.gameObject);
			}
		}

		// Token: 0x040025D2 RID: 9682
		private static readonly List<Corpse> instancesList = new List<Corpse>();

		// Token: 0x040025D3 RID: 9683
		private Renderer[] renderers;

		// Token: 0x040025D4 RID: 9684
		private static int maxCorpses = 25;

		// Token: 0x040025D5 RID: 9685
		private static Corpse.DisposalMode disposalMode = Corpse.DisposalMode.OutOfSight;

		// Token: 0x040025D6 RID: 9686
		private static int maxChecksPerUpdate = 3;

		// Token: 0x040025D7 RID: 9687
		private static int currentCheckIndex = 0;

		// Token: 0x02000683 RID: 1667
		private class CorpsesMaxConVar : BaseConVar
		{
			// Token: 0x06002095 RID: 8341 RVA: 0x00009F73 File Offset: 0x00008173
			private CorpsesMaxConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06002096 RID: 8342 RVA: 0x0008C1B8 File Offset: 0x0008A3B8
			public override void SetString(string newValue)
			{
				int maxCorpses;
				if (TextSerialization.TryParseInvariant(newValue, out maxCorpses))
				{
					Corpse.maxCorpses = maxCorpses;
				}
			}

			// Token: 0x06002097 RID: 8343 RVA: 0x0008C1D5 File Offset: 0x0008A3D5
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(Corpse.maxCorpses);
			}

			// Token: 0x040025D8 RID: 9688
			private static Corpse.CorpsesMaxConVar instance = new Corpse.CorpsesMaxConVar("corpses_max", ConVarFlags.Archive | ConVarFlags.Engine, "25", "The maximum number of corpses allowed.");
		}

		// Token: 0x02000684 RID: 1668
		public enum DisposalMode
		{
			// Token: 0x040025DA RID: 9690
			Hard,
			// Token: 0x040025DB RID: 9691
			OutOfSight
		}

		// Token: 0x02000685 RID: 1669
		private class CorpseDisposalConVar : BaseConVar
		{
			// Token: 0x06002099 RID: 8345 RVA: 0x00009F73 File Offset: 0x00008173
			private CorpseDisposalConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x0600209A RID: 8346 RVA: 0x0008C200 File Offset: 0x0008A400
			public override void SetString(string newValue)
			{
				try
				{
					Corpse.DisposalMode disposalMode = (Corpse.DisposalMode)Enum.Parse(typeof(Corpse.DisposalMode), newValue, true);
					if (disposalMode != Corpse.disposalMode)
					{
						Corpse.disposalMode = disposalMode;
						if (disposalMode != Corpse.DisposalMode.Hard && disposalMode == Corpse.DisposalMode.OutOfSight)
						{
							foreach (Corpse corpse in Corpse.instancesList)
							{
								corpse.CollectRenderers();
							}
						}
					}
				}
				catch (ArgumentException)
				{
					Console.ShowHelpText(this.name);
				}
			}

			// Token: 0x0600209B RID: 8347 RVA: 0x0008C29C File Offset: 0x0008A49C
			public override string GetString()
			{
				return Corpse.disposalMode.ToString();
			}

			// Token: 0x040025DC RID: 9692
			private static Corpse.CorpseDisposalConVar instance = new Corpse.CorpseDisposalConVar("corpses_disposal", ConVarFlags.Archive | ConVarFlags.Engine, null, "The corpse disposal mode. Choices are Hard and OutOfSight.");
		}
	}
}
