using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MSM6295Loader.Containers
{
    [Serializable]
    public class ProjectFile
    {
        public enum ProjectFileStatus
        {
            NotAssigned,
            Good,
            NotFound,
            NotADPCM
        }

        public Guid Id { get; set; }

        public int Index { get; set; }

        public string SourceFilename { get; set; }

        public string Descripton { get; set; }

        public ProjectFileStatus Status { get; set; }

        public byte[] OkiADPCM { get; set; }

        [XmlIgnore]
        public int Size
        {
            get { return OkiADPCM.Length;  }
        }

    }
}
