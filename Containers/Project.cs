using MSM6295Loader.Codecs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MSM6295Loader.Containers
{
    [Serializable]
    public class Project
    {
        private bool _isDirty = false;

        protected virtual void OnProjectChanged(EventArgs e)
        {
            EventHandler handler = ProjectChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ProjectChanged;

        public Guid Id { get; set; }
        public List<ProjectFile> ProjectFiles { get; set; } = new List<ProjectFile>();
        public bool IsDirty { get { return _isDirty; } set { _isDirty = value; OnProjectChanged(EventArgs.Empty); } }
        public string ProjectName { get; set; }
        public string CurrentPath { get; set; }
        public string LastExport { get; set; }

        [XmlIgnore]
        public int TotalADPCMBytes 
        { 
            get 
            {
                int totalBytes = 0;
                foreach(ProjectFile projectFile in ProjectFiles)
                {
                    totalBytes += projectFile.OkiADPCM.Length;
                }
                return totalBytes;
            } 
        }

        [XmlIgnore]
        public int MaxADPCMBytes
        {
            get
            {
                return 0x40000;
            }
        }

        public int IndexOfFile(ProjectFile pf)
        {
            return ProjectFiles.IndexOf(pf) + 1;
        }

        public void ParseBinary(byte[] bytes)
        {
            //max limit is 128 commands to the OKI
            for(int i = 1; i < 128; i++)
            {
                int definitionPointer = i * 8;
                Int32 startAddress = ((bytes[definitionPointer] & 0x3) << 16) + (bytes[definitionPointer + 1] << 8) + bytes[definitionPointer + 2];
                Int32 endAddress = ((bytes[definitionPointer+3] & 0x3) << 16) + (bytes[definitionPointer + 4] << 8) + bytes[definitionPointer + 5];
                Int32 length = endAddress - startAddress;

                if (bytes[definitionPointer+6] == 0 && bytes[definitionPointer+7] == 0 && length > 0)
                {
                    ProjectFile projectFile = new ProjectFile();
                    projectFile.OkiADPCM = bytes.Skip(startAddress).Take(length).ToArray();
                    projectFile.Index = i - 1;
                    ProjectFiles.Add(projectFile);
                }
                else
                {
                    //end of files
                    break;
                }
            }
        }

        public void ReIndexProjectFiles()
        {
            int currentIndex = 1;
            foreach(ProjectFile projectFile in this.ProjectFiles)
            {
                projectFile.Index = currentIndex++;
                //if (!projectFile.Id.HasValue) projectFile.Id = Guid.NewGuid();
            }
        }

        public void ToBinary(BinaryWriter writer)
        {
            //8 bytes per OKI command
            int currentDataAddress = (ProjectFiles.Count * 8) + 8;
            writer.Write((long)0);     //8 bytes of all zeros

            //do the lengths first
            foreach (ProjectFile projectFile in ProjectFiles.OrderBy(f => f.Index))
            { 
                writer.Write(OkiEncoder.EncodeAddress(currentDataAddress));
                currentDataAddress += projectFile.OkiADPCM.Length;
                writer.Write(OkiEncoder.EncodeAddress(currentDataAddress-1));
                writer.Write((Int16)0);     //two empty bytes
            }
            //now dump binary
            foreach (ProjectFile projectFile in ProjectFiles.OrderBy(f => f.Index))
            {
                writer.Write(projectFile.OkiADPCM);
            }
        }
    }
}
