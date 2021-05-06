using System;
using Android.Graphics;
using Android.Util;

namespace XamSvg.Internals.Utils
{
    class SVGImageParser
    {

        public void parse(SVGProperties pSVGProperties, Canvas pCanvas, SVGPaint pSVGPaint)
        {
            RectF pRect = new RectF();
            float x = pSVGProperties.getFloatAttribute(SVGConstants.ATTRIBUTE_X, 0f);
            float y = pSVGProperties.getFloatAttribute(SVGConstants.ATTRIBUTE_Y, 0f);
            float width = pSVGProperties.getFloatAttribute(SVGConstants.ATTRIBUTE_WIDTH, 0f);
            float height = pSVGProperties.getFloatAttribute(SVGConstants.ATTRIBUTE_HEIGHT, 0f);
            pRect.Set(x, y, x + width, y + height);
            string xlinkhref = pSVGProperties.GetStringAttribute(SVGConstants.ATTRIBUTE_XLINKHREF);
            xlinkhref = xlinkhref.Substring(xlinkhref.IndexOf("base64,")+7);
            byte[] imageAsBytes = System.Convert.FromBase64String(xlinkhref);
            Bitmap b = BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
            pCanvas.DrawBitmap(b, null, pRect, pSVGPaint.mPaint);
        }
    }
}