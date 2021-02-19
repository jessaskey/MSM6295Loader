using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSM6295Loader.Codecs
{
	public static class MameOKI
	{
		private static sbyte[] s_index_shift = new sbyte[8] {-1,-1,-1,-1,2,4,6,8};
		private static short[] oki_step_table = new short[49] {
												16, 17, 19, 21, 23, 25, 28, 31,
												34, 37, 41, 45, 50, 55, 60, 66,
												73, 80, 88, 97, 107,118,130,143,
												157,173,190,209,230,253,279,307,
												337,371,408,449,494,544,598,658,
												724,796,876,963,1060,1166,1282,1411,1552
											};
		private static sbyte[] delta_table = new sbyte[16] {1,3,5,7,9,11,13,15, -1,-3,-5,-7,-9,-11,-13,-15};
		private static short[] s_diff_lookup = new short[49 * 16];

		private static short _lastSample = 0;
		private static sbyte _lastStep = 0;

		public static void Decode(byte[] input, BinaryWriter writer)
		{
			sbyte[,] nbl2bit = new sbyte[16, 4] {
											{ 1, 0, 0, 0}, { 1, 0, 0, 1}, { 1, 0, 1, 0}, { 1, 0, 1, 1},
											{ 1, 1, 0, 0}, { 1, 1, 0, 1}, { 1, 1, 1, 0}, { 1, 1, 1, 1},
											{ -1, 0, 0, 0}, { -1, 0, 0, 1}, { -1, 0, 1, 0}, { -1, 0, 1, 1},
											{ -1, 1, 0, 0}, { -1, 1, 0, 1}, { -1, 1, 1, 0}, { -1, 1, 1, 1}
										};

			// loop over all possible steps
			for (int step = 0; step <= 48; step++)
			{
				// compute the step value
				int stepval = (int)(Math.Floor(16.0 * Math.Pow(11.0 / 10.0, (double)step)));

				// loop over all nibbles and compute the difference
				for (int nib = 0; nib < 16; nib++)
				{
					s_diff_lookup[step * 16 + nib] = (short)(nbl2bit[nib, 0] *
						(stepval * nbl2bit[nib, 1] + stepval / 2 * nbl2bit[nib, 2] + stepval / 4 * nbl2bit[nib, 3] + stepval / 8));
				}
			}

			for (long i = 0; i < input.Length; i++)
			{
				byte high_nibble = (byte)((input[i]) / 16);
				byte low_nibble = (byte)((input[i]) % 16);

				//# now decode
				short sample_high = DecodeSample(high_nibble);
				short sample_low = DecodeSample(low_nibble);

				writer.Write((short)(sample_high << 4));
				writer.Write((short)(sample_low << 4));
			}
		}

		public static void Encode(short[] input, BinaryWriter writer)
        {
			int i = 0;
			_lastSample = 0;
			_lastStep = 0;

			while(i < input.Length)
            {
				byte step_h = EncodeSample(input[i++]);
				//in case we have odd # of samples samples
				if (i < input.Length)
				{
					byte step_l = EncodeSample(input[i++]);
					byte step = (byte)(((step_h & 0xF) << 4) + (step_l & 0xF));
					writer.Write(step);
				}
			}
		}

		private static byte EncodeSample(short sample)
        {
			//if (sample < 0x7ff8)
			//{
			//	// round up
			//	sample += 8;
			//}
			sample = (short)(sample >>4);

			short step_size = oki_step_table[_lastStep];
			short delta = (short)(sample - _lastSample);
			byte adpcm_sample = (byte)((delta < 0) ? 8 : 0);
			if (delta < 0)
			{
				adpcm_sample = 8;
			}
			delta = Math.Abs(delta);
			for (int bit = 3; bit > 0;  bit--)
			{
				if (delta >= step_size)
				{
					adpcm_sample |= (byte)(1 << bit);
					delta -= step_size;
				}
				step_size >>= 1;
			}
			oki_step(adpcm_sample);
			return adpcm_sample;
		}

		private static short oki_step(byte step)
		{
			short step_size = oki_step_table[_lastStep];
			short delta = (short)(delta_table[step & 15] * step_size / 8);
			short output = (short)(_lastSample + delta);
			_lastSample = output = Clip<short>(output, -2048, 2047); // Saturate output
			sbyte adjusted_step = (sbyte)(_lastStep + s_index_shift[step & 7]);
			_lastStep = (sbyte)Clip<sbyte>(adjusted_step, 0, 48);
			return output;
		}

		private static T Clip<T>(T value, T min, T max)
		{
			//return Math.Min(Math.Max(value, min), max);
			value = (Comparer<T>.Default.Compare(value, min) > 0) ? value : min;
			value = (Comparer<T>.Default.Compare(value, max) > 0) ? max : value;
			return value;
		}

		private static short DecodeSample(byte nibble)
		{
			// update the sample
			_lastSample += s_diff_lookup[_lastStep * 16 + (nibble & 0xF)];
			// clamp to the maximum
			_lastSample = Clip<short>(_lastSample, -2048, 2047);
			// adjust the step size and clamp
			_lastStep += s_index_shift[nibble & 7];
			_lastStep = Clip<sbyte>(_lastStep, 0, 48);
			return _lastSample;
		}
	}
}
