// Copyright 2001-2016 Crytek GmbH / Crytek Group. All rights reserved.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using CryEngine.Common;

namespace CryEngine
{
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static class BitmapExtentions
	{
		public static int GetBPP(this Bitmap bmp)
		{			
			switch(bmp.PixelFormat)       
			{
			case PixelFormat.Format8bppIndexed:
				return 1;
			case PixelFormat.Format24bppRgb:
				return 3;
			case PixelFormat.Format32bppArgb:
			case PixelFormat.Format32bppPArgb:
				return 4;
			}
			return 0;
		}

		public static byte[] GetPixels(this Bitmap bmp)
		{
			var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
			var data = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
			int bytes = System.Math.Abs (data.Stride) * bmp.Height;
			byte[] pix = new byte[bytes];
			System.Runtime.InteropServices.Marshal.Copy(data.Scan0, pix, 0, bytes);
			bmp.UnlockBits(data);

			int bpp = bmp.GetBPP();
			Action<int> SwapRnB = (ofs) => 
			{
				byte p0 = pix[ofs];
				pix[ofs] = pix[ofs + 2];
				pix[ofs + 2] = p0;
			};
			Action<int> Swap = bpp == 4 ? SwapRnB : null;

			if (Swap == null)
				throw new Exception("Unsupported Pixelformat");

			for (int i = 0; i < bytes; i += bpp)
				Swap (i);

			return pix;
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static class CryStringExtensions
	{
		public static string ToString(this CryString str)
		{
			return str.c_str();
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static class SInputEventExtentions
	{
		public static Dictionary<EKeyId, bool> KeyDownLog = new Dictionary<EKeyId, bool> ();
		public static Dictionary<EKeyId, float> KeyInputValueLog = new Dictionary<EKeyId, float> ();

		public static bool KeyDown(this SInputEvent e, EKeyId k)
		{
			bool isDown = false;
			KeyDownLog.TryGetValue (k, out isDown);
			return isDown;
		}

		public static bool KeyPressed(this SInputEvent e, EKeyId k)
		{
			return e.keyId == k && e.state == EInputState.eIS_Pressed;
		}

		public static bool KeyUp(this SInputEvent e, EKeyId k)
		{
			return e.keyId == k && e.state == EInputState.eIS_Released;
		}

		// For analog inputs
		public static bool KeyChanged(this SInputEvent e, EKeyId k){
			return e.keyId == k && e.state == EInputState.eIS_Changed;
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static class Vec3Extentions
	{
		public static string ToString(this Vec3 v, string fmt="0.0")
		{
			return v.x.ToString (fmt) + "," + v.y.ToString (fmt) + "," + v.z.ToString (fmt);
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static class Vec2Extentions
	{
		public static string ToString(this Vec2 v, string fmt="0.0")
		{
			return v.x.ToString (fmt) + "," + v.y.ToString (fmt);
		}
	}
}
