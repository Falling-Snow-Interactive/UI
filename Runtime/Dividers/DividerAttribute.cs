using System;
using UnityEngine;

namespace Fsi.Ui.Dividers
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class DividerAttribute : PropertyAttribute
	{
		private const float Shade = 0.19f;
		private const float DefaultSize = 4f;
		private Color DefaultColor => new(Shade, Shade, Shade, 1f);
		
		public float Size { get; }
		public Color Color { get; }
		
		public DividerAttribute() : base(true)
		{
			Size = DefaultSize;
			Color = DefaultColor;
		}
	}
}