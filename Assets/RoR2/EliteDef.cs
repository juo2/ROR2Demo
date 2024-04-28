using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000531 RID: 1329
	[CreateAssetMenu(menuName = "RoR2/EliteDef")]
	public class EliteDef : ScriptableObject
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00069FC0 File Offset: 0x000681C0
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x00069FC8 File Offset: 0x000681C8
		public EliteIndex eliteIndex { get; set; } = EliteIndex.None;

		// Token: 0x0600182D RID: 6189 RVA: 0x00069FD1 File Offset: 0x000681D1
		public bool IsAvailable()
		{
			return !this.eliteEquipmentDef || (Run.instance && !Run.instance.IsEquipmentExpansionLocked(this.eliteEquipmentDef.equipmentIndex));
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0006A008 File Offset: 0x00068208
		[ConCommand(commandName = "elites_migrate", flags = ConVarFlags.None, helpText = "Generates EliteDef assets from the existing catalog entries.")]
		private static void CCElitesMigrate(ConCommandArgs args)
		{
			for (EliteIndex eliteIndex = (EliteIndex)0; eliteIndex < (EliteIndex)EliteCatalog.eliteList.Count; eliteIndex++)
			{
				EditorUtil.CopyToScriptableObject<EliteDef, EliteDef>(EliteCatalog.GetEliteDef(eliteIndex), "Assets/RoR2/Resources/EliteDefs/");
			}
		}

		// Token: 0x04001DC8 RID: 7624
		public string modifierToken;

		// Token: 0x04001DC9 RID: 7625
		public EquipmentDef eliteEquipmentDef;

		// Token: 0x04001DCA RID: 7626
		public Color32 color;

		// Token: 0x04001DCB RID: 7627
		public int shaderEliteRampIndex = -1;

		// Token: 0x04001DCC RID: 7628
		public float healthBoostCoefficient = 1f;

		// Token: 0x04001DCD RID: 7629
		public float damageBoostCoefficient = 1f;
	}
}
