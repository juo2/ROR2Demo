using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000AA1 RID: 2721
	public static class ViewablesCatalog
	{
		// Token: 0x06003EA2 RID: 16034 RVA: 0x00102960 File Offset: 0x00100B60
		public static void AddNodeToRoot(ViewablesCatalog.Node node)
		{
			node.SetParent(ViewablesCatalog.rootNode);
			foreach (ViewablesCatalog.Node node2 in node.Descendants())
			{
				if (ViewablesCatalog.fullNameToNodeMap.ContainsKey(node2.fullName))
				{
					Debug.LogFormat("Tried to add duplicate node {0}", new object[]
					{
						node2.fullName
					});
				}
				else
				{
					ViewablesCatalog.fullNameToNodeMap.Add(node2.fullName, node2);
				}
			}
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x001029F0 File Offset: 0x00100BF0
		public static ViewablesCatalog.Node FindNode(string fullName)
		{
			ViewablesCatalog.Node result;
			if (ViewablesCatalog.fullNameToNodeMap.TryGetValue(fullName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x00102A10 File Offset: 0x00100C10
		[ConCommand(commandName = "viewables_list", flags = ConVarFlags.None, helpText = "Displays the full names of all viewables.")]
		private static void CCViewablesList(ConCommandArgs args)
		{
			Debug.Log(string.Join("\n", (from node in ViewablesCatalog.rootNode.Descendants()
			select node.fullName).ToArray<string>()));
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x00102A60 File Offset: 0x00100C60
		[ConCommand(commandName = "viewables_list_unviewed", flags = ConVarFlags.None, helpText = "Displays the full names of all unviewed viewables.")]
		private static void CCViewablesListUnviewed(ConCommandArgs args)
		{
			UserProfile userProfile = args.GetSenderLocalUser().userProfile;
			Debug.Log(string.Join("\n", (from node in ViewablesCatalog.rootNode.Descendants()
			where node.shouldShowUnviewed(userProfile)
			select node.fullName).ToArray<string>()));
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x00102AD8 File Offset: 0x00100CD8
		[ConCommand(commandName = "viewables_clear_viewed", flags = ConVarFlags.None, helpText = "Clears all viewed viewables")]
		private static void CCViewablesClearViewed(ConCommandArgs args)
		{
			args.GetSenderLocalUser().userProfile.ClearAllViewablesAsViewed();
			Debug.Log("Cleared!");
		}

		// Token: 0x04003CEE RID: 15598
		private static readonly ViewablesCatalog.Node rootNode = new ViewablesCatalog.Node("", true, null);

		// Token: 0x04003CEF RID: 15599
		private static readonly Dictionary<string, ViewablesCatalog.Node> fullNameToNodeMap = new Dictionary<string, ViewablesCatalog.Node>();

		// Token: 0x02000AA2 RID: 2722
		public class Node
		{
			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x00102B12 File Offset: 0x00100D12
			// (set) Token: 0x06003EA9 RID: 16041 RVA: 0x00102B1A File Offset: 0x00100D1A
			public ViewablesCatalog.Node parent { get; private set; }

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06003EAA RID: 16042 RVA: 0x00102B23 File Offset: 0x00100D23
			public string fullName
			{
				get
				{
					if (this.fullNameDirty)
					{
						this.GenerateFullName();
					}
					return this._fullName;
				}
			}

			// Token: 0x06003EAB RID: 16043 RVA: 0x00102B3C File Offset: 0x00100D3C
			public Node(string name, bool isFolder, ViewablesCatalog.Node parent = null)
			{
				this.name = name;
				this.isFolder = isFolder;
				this.shouldShowUnviewed = new Func<UserProfile, bool>(this.DefaultShouldShowUnviewedTest);
				this.children = this._children.AsReadOnly();
				this.SetParent(parent);
			}

			// Token: 0x06003EAC RID: 16044 RVA: 0x00102B9C File Offset: 0x00100D9C
			public void SetParent(ViewablesCatalog.Node newParent)
			{
				if (this.parent == newParent)
				{
					return;
				}
				ViewablesCatalog.Node parent = this.parent;
				if (parent != null)
				{
					parent._children.Remove(this);
				}
				this.parent = newParent;
				ViewablesCatalog.Node parent2 = this.parent;
				if (parent2 != null)
				{
					parent2._children.Add(this);
				}
				this.fullNameDirty = true;
			}

			// Token: 0x06003EAD RID: 16045 RVA: 0x00102BF0 File Offset: 0x00100DF0
			private void GenerateFullName()
			{
				string text = this.name;
				if (this.parent != null)
				{
					text = this.parent.fullName + text;
				}
				if (this.isFolder)
				{
					text += "/";
				}
				this._fullName = text;
				this.fullNameDirty = false;
			}

			// Token: 0x06003EAE RID: 16046 RVA: 0x00102C40 File Offset: 0x00100E40
			public bool DefaultShouldShowUnviewedTest(UserProfile userProfile)
			{
				if (!this.isFolder && userProfile.HasViewedViewable(this.fullName))
				{
					return false;
				}
				using (IEnumerator<ViewablesCatalog.Node> enumerator = this.children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.shouldShowUnviewed(userProfile))
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06003EAF RID: 16047 RVA: 0x00102CB4 File Offset: 0x00100EB4
			public IEnumerable<ViewablesCatalog.Node> Descendants()
			{
				yield return this;
				foreach (ViewablesCatalog.Node node in this._children)
				{
					foreach (ViewablesCatalog.Node node2 in node.Descendants())
					{
						yield return node2;
					}
					IEnumerator<ViewablesCatalog.Node> enumerator2 = null;
				}
				List<ViewablesCatalog.Node>.Enumerator enumerator = default(List<ViewablesCatalog.Node>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x04003CF0 RID: 15600
			public readonly string name;

			// Token: 0x04003CF1 RID: 15601
			public readonly bool isFolder;

			// Token: 0x04003CF3 RID: 15603
			private readonly List<ViewablesCatalog.Node> _children = new List<ViewablesCatalog.Node>();

			// Token: 0x04003CF4 RID: 15604
			public ReadOnlyCollection<ViewablesCatalog.Node> children;

			// Token: 0x04003CF5 RID: 15605
			private string _fullName;

			// Token: 0x04003CF6 RID: 15606
			private bool fullNameDirty = true;

			// Token: 0x04003CF7 RID: 15607
			public Func<UserProfile, bool> shouldShowUnviewed;
		}
	}
}
