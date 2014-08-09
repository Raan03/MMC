using UnityEngine;
using System.Collections.Generic;

namespace Classes
{
	public static class Shared
	{

		public static void RenderBackGround()
		{
			string backSideCard = "picture";

			GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height),
				Resources.Load<Texture2D>(backSideCard),
				ScaleMode.ScaleToFit);

			GUI.EndGroup();
		}

		public static Dictionary<string,int> CalculatePaddingInPixels(int paddingPercentage)
		{
			return CalculatePaddingInPixels(
				paddingPercentage, 
				paddingPercentage, 
				paddingPercentage, 
				paddingPercentage);
		}

		public static Dictionary<string,int> CalculatePaddingInPixels(int paddingWidthPercentage, int paddingHeightPercentage)
		{
			return CalculatePaddingInPixels(
				paddingWidthPercentage, 
				paddingHeightPercentage, 
				paddingWidthPercentage, 
				paddingHeightPercentage);
		}


		static Dictionary<string,int> CalculatePaddingInPixels(int paddingPercentageLeft, int paddingPercentageTop, int paddingPercentageRight, int paddingPercentageBottom)
		{
			Dictionary<string,int> output = new Dictionary<string, int>();
			output.Add("Left", Mathf.CeilToInt(Screen.width * (paddingPercentageLeft * 0.01f)));
			output.Add("Top", Mathf.CeilToInt(Screen.height * (paddingPercentageTop * 0.01f)));
			output.Add("Right", Mathf.CeilToInt(Screen.width * (paddingPercentageRight * 0.01f)));
			output.Add("Bottom", Mathf.CeilToInt(Screen.height * (paddingPercentageBottom * 0.01f)));
			return output;
		}
           
	}
}