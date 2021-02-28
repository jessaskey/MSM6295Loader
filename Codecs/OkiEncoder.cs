using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSM6295Loader.Codecs
{
    public static class OkiEncoder
    {
        public static byte[] EncodeAddress(int address)
        {
            byte[] encodedAddress = new byte[3];
            encodedAddress[0] = (byte)((address >> 16) & 0x03);
            encodedAddress[1] = (byte)(address >> 8);
            encodedAddress[2] = (byte)(address & 0xFF);
            return encodedAddress;
        }
    }
}
