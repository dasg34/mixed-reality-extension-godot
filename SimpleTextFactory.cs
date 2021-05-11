// Licensed under the MIT License.
using MixedRealityExtension.Core.Interfaces;
using MixedRealityExtension.PluginInterfaces;
using Godot;

public class TmpTextFactory : ITextFactory
{
	public IText CreateText(IActor actor)
	{
		var actorGo = actor.GameObject;
		var tmp = actorGo.GetComponent<TextMeshPro>();
		if (tmp == null)
		{
			tmp = actorGo.AddComponent<TextMeshPro>();
		}

		return new TmpText(this, tmp);
	}
}
