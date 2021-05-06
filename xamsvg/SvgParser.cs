using System;
using System.IO;
using System.Xml;

using Android.Content.Res;
using Android.Graphics;
using Attributes = System.Collections.Generic.Dictionary<string, string>;

namespace XamSvg
{
    using Internals;
    using System.Collections.Generic;
    

    public class SVGParser
	{

        public static Svg ParseSVGFromString (string pString)
		{
			return ParseSVGFromString(pString, null);
		}

        public static Svg ParseSVGFromString(string pString, ISvgColorMapper pSVGColorMapper)
        {
            return ParseSvgFromReader(new StringReader(pString), pSVGColorMapper);
        }

        public static Svg ParseSVGFromString (string pString, ISvgColorMapper pSVGColorMapper, out List<Attributes> SvgAttributes, bool isStecil = false)
		{
			return ParseSvgFromReader(new StringReader(pString), pSVGColorMapper, out SvgAttributes, isStecil);
		}

		public static Svg ParseSVGFromResource (Resources pResources, int pRawResourceID)
		{
			return ParseSVGFromResource(pResources, pRawResourceID, null);
		}

		public static Svg ParseSVGFromResource (Resources pResources, int pRawResourceID, ISvgColorMapper pSVGColorMapper)
		{
			return ParseSvgFromStream(pResources.OpenRawResource (pRawResourceID), pSVGColorMapper);
		}

		public static Svg ParseSvgFromAsset (AssetManager pAssetManager, string pAssetPath)
		{
			return ParseSvgFromAsset(pAssetManager, pAssetPath, null);
		}

		public static Svg ParseSvgFromAsset (AssetManager pAssetManager, string pAssetPath, ISvgColorMapper pSVGColorMapper)
		{
			using (var stream = pAssetManager.Open (pAssetPath))
				return ParseSvgFromStream (stream, pSVGColorMapper);
		}

		public static Svg ParseSvgFromStream(Stream pInputStream, ISvgColorMapper pSVGColorMapper)
		{
			return ParseSvgFromReader (new StreamReader (pInputStream), pSVGColorMapper);
		}

        public static Svg ParseSvgFromReader(TextReader reader, ISvgColorMapper pSVGColorMapper)
        {
            List<Attributes> SvgAttributes;
            return ParseSvgFromReader(reader, pSVGColorMapper, out SvgAttributes, false);
        }

        public static Svg ParseSvgFromReader (TextReader reader, ISvgColorMapper pSVGColorMapper, out List<Attributes> SvgAttributes, bool isStecil = false)
		{
			try {
				var xmlReader = XmlReader.Create (reader);
				Picture picture = new Picture();
				var svgHandler = new SVGHandler (picture, pSVGColorMapper, isStecil);
                SvgAttributes = svgHandler.Parse (xmlReader);
              
                Svg svg = new Svg(picture, svgHandler.getBounds(), svgHandler.getComputedBounds());
				return svg;
			} catch (Exception e) {
				throw new SVGParseException(e);
			}
		}
	}
}
