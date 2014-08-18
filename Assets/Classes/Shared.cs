using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    /// <summary>
    /// Shared class for functions cross-manager
    /// </summary>
    public static class Shared
    {

        /// <summary>
        /// Renders the back ground.
        /// </summary>
        public static void RenderBackGround()
        {
            string backSideCard = "picture";

            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height),
				Resources.Load<Texture2D>(backSideCard),
				ScaleMode.ScaleToFit);

            GUI.EndGroup();
        }

        /// <summary>
        /// How many pixels do we actually have if all sides have equal % border?
        /// </summary>
        /// <returns>The padding in absolute pixels.</returns>
        /// <param name="paddingPercentage">Padding percentage.</param>
        public static Dictionary<string,int> CalculatePaddingInPixels(int paddingPercentage)
        {
            return CalculatePaddingInPixels(
				paddingPercentage, 
				paddingPercentage, 
				paddingPercentage, 
				paddingPercentage);
        }

        /// <summary>
        /// Calculates the padding in pixels.
        /// </summary>
        /// <returns>The padding in absolute pixels.</returns>
        /// <param name="paddingWidthPercentage">Padding width percentage.</param>
        /// <param name="paddingHeightPercentage">Padding height percentage.</param>
        public static Dictionary<string,int> CalculatePaddingInPixels(int paddingWidthPercentage, int paddingHeightPercentage)
        {
            return CalculatePaddingInPixels(
				paddingWidthPercentage, 
				paddingHeightPercentage, 
				paddingWidthPercentage, 
				paddingHeightPercentage);
        }

        /// <summary>
        /// Calculates the padding in pixels, the magic workflow.
        /// </summary>
        /// <returns>The padding in pixels.</returns>
        /// <param name="paddingPercentageLeft">Padding percentage left.</param>
        /// <param name="paddingPercentageTop">Padding percentage top.</param>
        /// <param name="paddingPercentageRight">Padding percentage right.</param>
        /// <param name="paddingPercentageBottom">Padding percentage bottom.</param>
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