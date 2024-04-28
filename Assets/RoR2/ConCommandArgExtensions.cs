using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HG;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004E6 RID: 1254
	public static class ConCommandArgExtensions
	{
		// Token: 0x060016C0 RID: 5824 RVA: 0x00064890 File Offset: 0x00062A90
		public static BodyIndex? TryGetArgBodyIndex(this ConCommandArgs args, int index)
		{
			if (index < args.userArgs.Count)
			{
				BodyIndex bodyIndex = BodyCatalog.FindBodyIndexCaseInsensitive(args[index]);
				if (bodyIndex != BodyIndex.None)
				{
					return new BodyIndex?(bodyIndex);
				}
			}
			return null;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000648D0 File Offset: 0x00062AD0
		public static BodyIndex GetArgBodyIndex(this ConCommandArgs args, int index)
		{
			BodyIndex? bodyIndex = args.TryGetArgBodyIndex(index);
			if (bodyIndex == null)
			{
				throw new ConCommandException(string.Format("Argument {0} is not a valid body name.", index));
			}
			return bodyIndex.GetValueOrDefault();
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0006490C File Offset: 0x00062B0C
		public static EquipmentIndex? TryGetArgEquipmentIndex(this ConCommandArgs args, int index)
		{
			string text = args.TryGetArgString(index);
			if (text != null)
			{
				EquipmentIndex equipmentIndex = EquipmentCatalog.FindEquipmentIndex(text);
				if (equipmentIndex != EquipmentIndex.None || text.Equals("None", StringComparison.Ordinal))
				{
					return new EquipmentIndex?(equipmentIndex);
				}
			}
			return null;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00064950 File Offset: 0x00062B50
		public static EquipmentIndex GetArgEquipmentIndex(this ConCommandArgs args, int index)
		{
			EquipmentIndex? equipmentIndex = args.TryGetArgEquipmentIndex(index);
			if (equipmentIndex == null)
			{
				throw new ConCommandException("No EquipmentIndex is defined for an equipment named '" + args.TryGetArgString(index) + "'. Use the \"equipment_list\" command to get a list of all valid equipment.");
			}
			return equipmentIndex.GetValueOrDefault();
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00064994 File Offset: 0x00062B94
		public static void GetArgCharacterBodyInstances(this ConCommandArgs args, int argIndex, List<CharacterBody> dest)
		{
			if (argIndex < args.userArgs.Count)
			{
				string argString = args[argIndex];
				args.TryGetSenderBody();
				List<CharacterBody> list = CollectionPool<CharacterBody, List<CharacterBody>>.RentCollection();
				try
				{
					for (int i = 0; i < ConCommandArgExtensions.finders.Length; i++)
					{
						ConCommandArgExtensions.BaseCharacterBodyInstanceSearchHandler baseCharacterBodyInstanceSearchHandler = ConCommandArgExtensions.finders[i];
						if (baseCharacterBodyInstanceSearchHandler.ShouldHandle(args, argString))
						{
							baseCharacterBodyInstanceSearchHandler.GetResults(args, argString, list);
							dest.AddRange(list);
							break;
						}
					}
				}
				catch (ConCommandException ex)
				{
					throw new ConCommandException(string.Format("Argument {0}: {1}", argIndex, ex.Message));
				}
				finally
				{
					list = CollectionPool<CharacterBody, List<CharacterBody>>.ReturnCollection(list);
				}
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00064A44 File Offset: 0x00062C44
		public static void TryGetArgCharacterBodyInstances(this ConCommandArgs args, int argIndex, List<CharacterBody> dest)
		{
			try
			{
				args.GetArgCharacterBodyInstances(argIndex, dest);
			}
			catch (ConCommandException)
			{
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00064A70 File Offset: 0x00062C70
		public static CharacterBody GetArgCharacterBodyInstance(this ConCommandArgs args, int argIndex)
		{
			List<CharacterBody> list = CollectionPool<CharacterBody, List<CharacterBody>>.RentCollection();
			CharacterBody result;
			try
			{
				args.GetArgCharacterBodyInstances(argIndex, list);
				result = ((list.Count > 0) ? list[0] : null);
			}
			finally
			{
				list = CollectionPool<CharacterBody, List<CharacterBody>>.ReturnCollection(list);
			}
			return result;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00064ABC File Offset: 0x00062CBC
		public static CharacterBody TryGetArgCharacterBodyInstance(this ConCommandArgs args, int argIndex)
		{
			CharacterBody result;
			try
			{
				result = args.GetArgCharacterBodyInstance(argIndex);
			}
			catch (ConCommandException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00064AEC File Offset: 0x00062CEC
		public static CharacterMaster GetArgCharacterMasterInstance(this ConCommandArgs args, int argIndex)
		{
			CharacterBody argCharacterBodyInstance = args.GetArgCharacterBodyInstance(argIndex);
			if (argCharacterBodyInstance)
			{
				return argCharacterBodyInstance.master;
			}
			return null;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00064B14 File Offset: 0x00062D14
		public static CharacterMaster TryGetArgCharacterMasterInstance(this ConCommandArgs args, int argIndex)
		{
			CharacterMaster result;
			try
			{
				result = args.GetArgCharacterMasterInstance(argIndex);
			}
			catch (ConCommandException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00064B44 File Offset: 0x00062D44
		public static ItemIndex? TryGetArgItemIndex(this ConCommandArgs args, int index)
		{
			string text = args.TryGetArgString(index);
			if (text != null)
			{
				ItemIndex itemIndex = ItemCatalog.FindItemIndex(text);
				if (itemIndex != ItemIndex.None || text.Equals("None", StringComparison.Ordinal))
				{
					return new ItemIndex?(itemIndex);
				}
			}
			return null;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00064B88 File Offset: 0x00062D88
		public static ItemIndex GetArgItemIndex(this ConCommandArgs args, int index)
		{
			ItemIndex? itemIndex = args.TryGetArgItemIndex(index);
			if (itemIndex == null)
			{
				throw new ConCommandException("No ItemIndex is defined for an item named '" + args.TryGetArgString(index) + "'. Use the \"item_list\" command to get a list of all valid items.");
			}
			return itemIndex.GetValueOrDefault();
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00064BCC File Offset: 0x00062DCC
		public static MasterCatalog.MasterIndex? TryGetArgMasterIndex(this ConCommandArgs args, int argIndex)
		{
			if (argIndex < args.userArgs.Count)
			{
				string text = args[argIndex];
				MasterCatalog.MasterIndex masterIndex = MasterCatalog.FindMasterIndex(text);
				if (masterIndex != MasterCatalog.MasterIndex.none || text.Equals("None", StringComparison.OrdinalIgnoreCase))
				{
					return new MasterCatalog.MasterIndex?(masterIndex);
				}
			}
			return null;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00064C24 File Offset: 0x00062E24
		public static MasterCatalog.MasterIndex GetArgMasterIndex(this ConCommandArgs args, int index)
		{
			MasterCatalog.MasterIndex? masterIndex = args.TryGetArgMasterIndex(index);
			if (masterIndex == null)
			{
				throw new ConCommandException(string.Format("Argument {0} is not a valid character master prefab name.", index));
			}
			return masterIndex.GetValueOrDefault();
		}

		// Token: 0x04001C89 RID: 7305
		private static readonly ConCommandArgExtensions.BaseCharacterBodyInstanceSearchHandler[] finders = new ConCommandArgExtensions.BaseCharacterBodyInstanceSearchHandler[]
		{
			new ConCommandArgExtensions.SenderCharacterBodyInstanceSearchHandler(),
			new ConCommandArgExtensions.NearestCharacterBodyInstanceSearchHandler()
		};

		// Token: 0x020004E7 RID: 1255
		public abstract class BaseCharacterBodyInstanceSearchHandler
		{
			// Token: 0x060016CF RID: 5839
			public abstract bool ShouldHandle(ConCommandArgs args, string argString);

			// Token: 0x060016D0 RID: 5840
			public abstract void GetResults(ConCommandArgs args, string argString, List<CharacterBody> dest);
		}

		// Token: 0x020004E8 RID: 1256
		public class NearestCharacterBodyInstanceSearchHandler : ConCommandArgExtensions.BaseCharacterBodyInstanceSearchHandler
		{
			// Token: 0x060016D2 RID: 5842 RVA: 0x00064C7C File Offset: 0x00062E7C
			public override bool ShouldHandle(ConCommandArgs args, string argString)
			{
				return argString.Equals("nearest", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060016D3 RID: 5843 RVA: 0x00064C8C File Offset: 0x00062E8C
			public override void GetResults(ConCommandArgs args, string argString, List<CharacterBody> dest)
			{
				CharacterBody senderBody = args.TryGetSenderBody();
				if (!senderBody)
				{
					throw new ConCommandException(string.Format("Sender must have a valid body to use \"{0}\".", argString));
				}
				Vector3 myPosition = senderBody.corePosition;
				ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
				dest.AddRange(from candidateBody in readOnlyInstancesList
				where senderBody != candidateBody
				orderby (candidateBody.corePosition - myPosition).sqrMagnitude
				select candidateBody);
			}
		}

		// Token: 0x020004EA RID: 1258
		public class SenderCharacterBodyInstanceSearchHandler : ConCommandArgExtensions.BaseCharacterBodyInstanceSearchHandler
		{
			// Token: 0x060016D8 RID: 5848 RVA: 0x00064D46 File Offset: 0x00062F46
			public override bool ShouldHandle(ConCommandArgs args, string argString)
			{
				return argString.Equals("me", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060016D9 RID: 5849 RVA: 0x00064D54 File Offset: 0x00062F54
			public override void GetResults(ConCommandArgs args, string argString, List<CharacterBody> dest)
			{
				CharacterBody characterBody = args.TryGetSenderBody();
				if (!characterBody)
				{
					throw new ConCommandException(string.Format("Sender must have a valid body to use \"{0}\".", argString));
				}
				dest.Add(characterBody);
			}
		}
	}
}
