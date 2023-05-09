using System.Collections.Generic;
using UnityEngine;

public class SpriteCenter
{
	public static Dictionary<string, Sprite> AllSprites = new Dictionary<string, Sprite>();

	public static Sprite GetSprite(string name)
	{
		Sprite sprite;
		if (!AllSprites.ContainsKey(name))
		{
			Texture2D texture2D = (Texture2D)Resources.Load(name);
			sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
			AllSprites.Add(name, sprite);
			return sprite;
		}
		AllSprites.TryGetValue(name, out sprite);
		return sprite;
	}
}
