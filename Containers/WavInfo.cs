using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSM6295Loader.Containers
{
    public class WavInfo
    {
        public bool IsValid { get; set; }
        public string ValidationErrors { get; set; }
        public string FileName { get; set; }
        public byte[] Bytes { get; set; }
        public string ChunkId { get; set; }
        public string Format { get; set; }
        public string SubChunk1Id { get; set; }
        public Int32 SubChunk1Size { get; set; }
        public Int16 AudioFormat { get; set; }
        public Int16 Channels { get; set; }
        public Int32 SampleRate { get; set; }
        public Int32 ByteRate { get; set; }
        public Int16 BlockAlign { get; set; }
        public Int16 BitsPerSample { get; set; }
        public string SubChunk2Id { get; set; }
        public Int32 SubChunk2Size { get; set; }
        public short[] Samples { get; set; }

    }
}
