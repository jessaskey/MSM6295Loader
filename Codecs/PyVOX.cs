using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSM6295Loader.Codecs
{
	public static class VoxCoder
	{
		static Int32[] ss_idx_chg_map = { -1, -1, -1, -1, 2, 4, 6, 8 };
		static Int32[] step_sizes = {16, 17, 19, 21, 23, 25, 28, 31, 34, 37, 41, 45,
									50, 55, 60, 66, 73, 80, 88, 97, 107, 118, 130, 143,
									157, 173, 190, 209, 230, 253, 279, 307, 337, 371, 408, 449,
									494, 544, 598, 658, 724, 796, 876, 963, 1060, 1166, 1282, 1411, 1552 };
		static int curr_ss_idx = 0;
		static Int16 last_output = 0;

		private static T Clip<T>(T value, T min, T max)
		{
			//return Math.Min(Math.Max(value, min), max);
			value = (Comparer<T>.Default.Compare(value, min) > 0) ? value : min;
			value = (Comparer<T>.Default.Compare(value, max) > 0) ? max : value;
			return value;
		}

		public static void Decode(byte[] input, BinaryWriter writer)
		{
			short history = 0;
			sbyte step_hist = 0;

			for (long i = 0; i < input.Length; i++)
			{
				sbyte high_4bit = (sbyte)((input[i]) / 16);
				sbyte low_4bit = (sbyte)((input[i]) % 16);

				//# first sample
				sbyte sample_0 = high_4bit;
				//# unsigned to signed
				//# 4bit : -2^4 ~ 2^(4-1)-1
				sbyte sample_4bit_0 = sample_0;
				if (sample_0 > 7)
				{
					sample_4bit_0 = (sbyte)(sample_0 - 16);
				}

				//second sample
				sbyte sample_1 = low_4bit;
				//# unsigned to signed
				sbyte sample_4bit_1 = sample_1;
				if (sample_1 > 7)
				{
					sample_4bit_1 = (sbyte)(sample_1 - 16);
				}

				//# now decode
				Int16 tmpDeS16_0 = Decode(sample_4bit_0);
				Int16 tmpDeS16_1 = Decode(sample_4bit_1);

				writer.Write(tmpDeS16_0);
				writer.Write(tmpDeS16_1);
			}
		}


		private static Int16 Decode(sbyte code)
		{
			Int32 step_size = step_sizes[curr_ss_idx];

			//inverse code into diff
			Int32 diffq = step_size >> 3;  // == step/8
			if ((code & 4) > 0)
			{
				diffq += step_size;
			}

			if ((code & 2) != 0)
			{
				diffq += step_size >> 1;
			}

			if ((code & 1) != 0)
			{
				diffq += step_size >> 2;
			}

			//add diff to predicted sample
			if ((code & 8) != 0)
			{
				diffq = -diffq;
			}

			last_output += (Int16)diffq;

			//check for overflow  clip the values to +/- 2^11 (supposed to be 16 bits)
			last_output = Clip<Int16>(last_output, -2048, 2047);
			//if (last_output > 2047) {
			//	last_output = 2047;
			//}
			//else if (last_output < -2048) {
			//	last_output = -2048;
			//}

			//find new quantizer step size
			curr_ss_idx += ss_idx_chg_map[code & 7];

			//check for overflow
			if (curr_ss_idx < 0)
			{
				curr_ss_idx = 0;
			}

			if (curr_ss_idx > 48)
			{
				curr_ss_idx = 48;
			}

			//save predict sample and de_index for next iteration
			//return new decoded sample
			//The original algorithm turned out to be 12bit, need to convert to 16bit
			return (Int16)(last_output << 4);
		}
	}

}
