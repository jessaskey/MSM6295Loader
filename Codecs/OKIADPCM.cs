using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSM6295Loader.Codecs
{
	/// <summary>
	/// Implemented from C version at https://github.com/superctr/adpcm by superctr
	/// </summary>
	static class OKIADPCM
	{
		private static short[] oki_step_table = new Int16[49] { 16, 17, 19, 21, 23, 25, 28, 31,
													34, 37, 41, 45, 50, 55, 60, 66,
													73, 80, 88, 97, 107,118,130,143,
													157,173,190,209,230,253,279,307,
													337,371,408,449,494,544,598,658,
													724,796,876,963,1060,1166,1282,1411,1552 };

		private static sbyte[] delta_table = new sbyte[16] { 1, 3, 5, 7, 9, 11, 13, 15, -1, -3, -5, -7, -9, -11, -13, -15 };
		private static sbyte[] adjust_table = new sbyte[8] { -1, -1, -1, -1, 2, 4, 6, 8 };


		private static short oki_step(sbyte step, short history, sbyte step_hist)
		{
			short step_size = oki_step_table[step_hist];
			short delta = (short)(delta_table[step] * step_size); //(step_size / 8));
			short o = (short)(history + delta);
			history = o = Math.Min(Math.Max(o, (short)-2048), (short)2047);
			sbyte adjusted_step = (sbyte)(step_hist + adjust_table[step & 0x7]);
			step_hist = Math.Min(Math.Max(adjusted_step, (sbyte)0), (sbyte)48);
			return o;

			//uint16_t step_size = oki_step_table[*step_hist];
			//int16_t delta = delta_table[step & 15] * step_size / 8;
			//int16_t out = *history + delta;
			//*history = out = CLAMP(out, -2048, 2047); // Saturate output
			//int8_t adjusted_step = *step_hist + adjust_table[step & 7];
			//*step_hist = CLAMP(adjusted_step, 0, 48);
			//return out;
		}

		//static inline uint8_t oki_encode_step(int16_t input, int16_t* history, uint8_t* step_hist)
		//{
		//	int bit;
		//	uint16_t step_size = oki_step_table[*step_hist];
		//	int16_t delta = input - *history;
		//	uint8_t adpcm_sample = (delta < 0) ? 8 : 0;
		//	if (delta < 0)
		//		adpcm_sample = 8;
		//	delta = abs(delta);
		//	for (bit = 3; bit--;)
		//	{
		//		if (delta >= step_size)
		//		{
		//			adpcm_sample |= (1 << bit);
		//			delta -= step_size;
		//		}
		//		step_size >>= 1;
		//	}
		//	oki_step(adpcm_sample, history, step_hist);
		//	return adpcm_sample;
		//}

		//void oki_encode(int16_t* buffer, uint8_t* outbuffer, long len)
		//{
		//	long i;

		//	int16_t history = 0;
		//	uint8_t step_hist = 0;
		//	uint8_t buf_sample = 0, nibble = 0;

		//	for (i = 0; i < len; i++)
		//	{
		//		int16_t sample = *buffer++;
		//		if (sample < 0x7ff8) // round up
		//			sample += 8;
		//		sample >>= 4;
		//		int step = oki_encode_step(sample, &history, &step_hist);
		//		if (nibble)
		//			*outbuffer++ = buf_sample | (step & 15);
		//		else
		//			buf_sample = (step & 15) << 4;
		//		nibble ^= 1;
		//	}
		//}

		public static void Decode(byte[] input, BinaryWriter writer)
		{
			short history = 0;
			sbyte step_hist = 0;

			for (long i = 0; i < input.Length; i++)
			{
				short s1 = oki_step((sbyte)((input[i]) >> 4), history, step_hist);
				short s2 = oki_step((sbyte)((input[i]) & 0xF), history, step_hist);
				writer.Write((Int16)(s1 << 4));
				writer.Write((Int16)(s2 << 4));
			}

			//long i;

			//int16_t history = 0;
			//uint8_t step_hist = 0;
			//uint8_t nibble = 0;

			//for (i = 0; i < len; i++)
			//{
			//	int8_t step = (*(int8_t*)buffer) << nibble;
			//	step >>= 4;
			//	if (nibble)
			//		buffer++;
			//	nibble ^= 4;
			//	*outbuffer++ = oki_step(step, &history, &step_hist) << 4;
			//}
		}

	}

}
