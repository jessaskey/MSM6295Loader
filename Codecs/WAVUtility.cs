using MSM6295Loader.Containers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSM6295Loader.Codecs
{
    public static class WAVUtility
    {
		public static WavInfo GetWavInfo(byte[] fileBytes)
        {
			WavInfo info = new WavInfo();
			info.Bytes = fileBytes;
			info.ChunkId = System.Text.Encoding.UTF8.GetString(fileBytes, 0, 4);
			info.Format = System.Text.Encoding.UTF8.GetString(fileBytes, 8, 4);
			info.SubChunk1Id = System.Text.Encoding.UTF8.GetString(fileBytes, 12, 4);
			info.SubChunk1Size = BitConverter.ToInt32(fileBytes, 16);
			info.AudioFormat = BitConverter.ToInt16(fileBytes, 20);
			info.Channels = BitConverter.ToInt16(fileBytes, 22);
			info.SampleRate = BitConverter.ToInt32(fileBytes, 24);
			info.ByteRate = BitConverter.ToInt32(fileBytes, 28);
			info.BlockAlign = BitConverter.ToInt16(fileBytes, 32);
			info.BitsPerSample = BitConverter.ToInt16(fileBytes, 34);
			info.SubChunk2Id = System.Text.Encoding.UTF8.GetString(fileBytes, 36, 4);
			info.SubChunk2Size = BitConverter.ToInt32(fileBytes, 40);

			info.Samples = new short[(int)Math.Ceiling((double)((fileBytes.Length - 44) / 2))];
			Buffer.BlockCopy(fileBytes, 44, info.Samples, 0, fileBytes.Length - 44);

			List<string> validationErrors = new List<string>();

			if (info.ChunkId != "RIFF")
			{
				info.IsValid = false;
				validationErrors.Add("File is not RIFF format");
			}
			else
			{
				if (info.Format != "WAVE")
				{
					info.IsValid = false;
					validationErrors.Add("File is not WAVE encoded RIFF");
				}
				else
				{
					if (info.AudioFormat != 1)
					{
						info.IsValid = false;
						validationErrors.Add("File is not PCM Audio Format");
					}
					else
					{
						if (info.Channels != 1)
						{
							info.IsValid = false;
							validationErrors.Add("File must be Mono (Single Channel)");
						}
						else
						{
							if (info.SampleRate != 8000)
							{
								info.IsValid = false;
								validationErrors.Add("Sample rate must be 8000Hz");
							}
							else
                            {
								info.IsValid = true;  //YAY!!!
                            }
						}
					}
				}
			}
			info.ValidationErrors = String.Join(",", validationErrors.ToArray());
			return info;
		}

		public static void AddOkiADPCMDefaultHeader(long samples, BinaryWriter writer)
        {
			int sampleRate = 8000;
			int bitsPerSample = 16;
			int numChannels = 1;
			AddHeader(samples, numChannels, sampleRate, bitsPerSample, writer);
		}
        public static void AddHeader(long numSamples, int numChannels, int sampleRate, int bitsPerSample, BinaryWriter writer)
        {
			// ChunkID: "RIFF"
			writer.Write(Encoding.ASCII.GetBytes("RIFF")); // 0x46464952);
			// ChunkSize: The size of the entire file in bytes minus 8 bytes for the two fields not included in this count: ChunkID and ChunkSize.
			writer.Write((int)(numSamples * bitsPerSample / 8) + 36);
			// Format: "WAVE"
			writer.Write(Encoding.ASCII.GetBytes("WAVE")); // 0x45564157);
			// Subchunk1ID: "fmt " (with the space).
			writer.Write(Encoding.ASCII.GetBytes("fmt ")); // 0x20746D66);
			// Subchunk1Size: 16 for PCM.
			writer.Write(16);
			// AudioFormat: 1 for PCM.
			writer.Write((short)1);
			// NumChannels: 1 for Mono. 2 for Stereo.
			writer.Write((short)numChannels);
			// SampleRate: 8000 is usually the default for VOX.
			writer.Write(sampleRate);
			// ByteRate: SampleRate * NumChannels * BitsPerSample / 8.
			writer.Write((int)(sampleRate * numChannels * bitsPerSample / 8));
			// BlockAlign: NumChannels * BitsPerSample / 8. I rounded this up to 2. It sounds best this way.
			writer.Write((short)2);
			// BitsPerSample: I will set this as 16
			writer.Write((short)bitsPerSample);
			// Subchunk2ID: "data"
			writer.Write(Encoding.ASCII.GetBytes("data")); // ( 0x61746164);
			// Subchunk2Size: NumSamples * NumChannels * BitsPerSample / 8. You can also think of this as the size of the read of the subchunk following this number.
			writer.Write((int)(numSamples * numChannels * bitsPerSample / 8));
		}
    }
}
