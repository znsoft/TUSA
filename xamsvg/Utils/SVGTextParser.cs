using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace XamSvg.Internals.Utils
{
    public class SVGTextParser
    {
        public static void Parse(SVGProperties pSVGProperties, Canvas pCanvas, SVGPaint pSVGPaint, string text)
        {
            float? X = pSVGProperties.getFloatAttribute(SVGConstants.ATTRIBUTE_X);
            float? Y = pSVGProperties.getFloatAttribute(SVGConstants.ATTRIBUTE_Y);
            float? size = pSVGProperties.getFloatAttribute(SVGConstants.ATTRIBUTE_SIZE);
            Paint pPaint = pSVGPaint.getPaint();
            float fontsize = pPaint.TextSize;
            if (size != null) {
                fontsize = (float)size;
                pPaint = new Paint();// pPaint);
                pPaint.TextSize = fontsize;

            }
            if (X != null && Y != null )
            {
                bool fill = pSVGPaint.setFill(pSVGProperties);

                bool stroke = pSVGPaint.setStroke(pSVGProperties);
                // ширина текста
                float width = pPaint.MeasureText(text);
                
                pCanvas.DrawText(text, X.Value, Y.Value, pPaint);

                if (fill || stroke)
                {
                    pSVGPaint.ensureComputedBoundsInclude(X.Value + width, Y.Value + pPaint.TextSize);
                }
            }
        }

    }
}



