using MSM6295Loader.Codecs;
using MSM6295Loader.Containers;
using MSM6295Loader.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MSM6295Loader
{
    public partial class FormMain : Form
    {
        private Project _project = null;
        private string _appTitle = "MSM6295Loader";

        public FormMain()
        {
            InitializeComponent();
            UpdateApplicationTitle();
            UpdateStatusBar();
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            if (_project != null)
            {
                if (_project.IsDirty)
                {
                    DialogResult dr = MessageBox.Show("Current project will be closed and is not currently saved. Your changes will be lost. Do you want to continue?", "Save Existing Project", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "MSM Project Files(*.msmp)| *.msmp";
            DialogResult sr = sd.ShowDialog();
            if (sr == DialogResult.OK)
            {
                Project project = new Project();
                project.Id = Guid.NewGuid();
                project.ProjectName = Path.GetFileNameWithoutExtension(sd.FileName);
                //project.FileName = sd.FileName;
                project.CurrentPath = Path.GetDirectoryName(sd.FileName);
                project.IsDirty = false;
                SaveProject(project, false);
                LoadProject(project);
            }
        }

        private void LoadProject(Project project)
        {
            _project = project;
            _project.ProjectChanged += ProjectDirtyChanged;
            _project.IsDirty = false;
            //if (!_project.Id.HasValue) _project.Id = Guid.NewGuid();
            _project.ReIndexProjectFiles();
            objectListViewMain.SetObjects(_project.ProjectFiles.OrderBy(f => f.Index));
            UpdateApplicationTitle();
        }

        private void ProjectDirtyChanged(object sender, EventArgs e)
        {
            UpdateApplicationTitle();
            UpdateStatusBar();
        }

        private void UpdateApplicationTitle()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_appTitle);
            if (_project != null)
            {
                sb.Append(" - " + _project.ProjectName);
                if (_project.IsDirty)
                {
                    sb.Append(" *");
                }
            }
            Text = sb.ToString();
        }

        private void UpdateStatusBar()
        {
            //Update Status Bar
            if (_project != null)
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append("Project: ");
                //sb.Append(_project.ProjectName);
                //sb.Append(" ");
                sb.Append(_project.ProjectFiles.Count.ToString());
                sb.Append("/127 Commands (");
                sb.Append(((_project.ProjectFiles.Count / 127f) * 100).ToString("F1"));
                sb.Append("%) - ");
                sb.Append(_project.TotalADPCMBytes.ToString());
                sb.Append("/");
                sb.Append(_project.MaxADPCMBytes.ToString());
                sb.Append(" [");
                sb.Append(((_project.TotalADPCMBytes / (float)_project.MaxADPCMBytes) * 100).ToString("F1"));
                sb.Append("%]");
                toolStripStatusLabel.Text = sb.ToString();
            }
            else
            {
                toolStripStatusLabel.Text = "No Project";
            }
        }

        private void SaveProject(Project project, bool saveAs)
        {
            string saveLocation = Path.Combine(project.CurrentPath, project.ProjectName + ".msmp");
            if (String.IsNullOrWhiteSpace(project.CurrentPath) || String.IsNullOrWhiteSpace(project.ProjectName) || saveAs)
            {
                //user must specify where to save
                SaveFileDialog sd = new SaveFileDialog();
                if (Directory.Exists(project.CurrentPath)) {
                    sd.InitialDirectory = project.CurrentPath;
                }
                sd.Filter = "MSM Project Files(*.msmp)| *.msmp";
                if (!String.IsNullOrWhiteSpace(project.ProjectName))
                {
                    sd.FileName = project.ProjectName + ".msmp";
                }
                DialogResult dr = sd.ShowDialog();
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                saveLocation = sd.FileName;
                project.ProjectName = Path.GetFileNameWithoutExtension(sd.FileName);
                project.CurrentPath = Path.GetDirectoryName(sd.FileName);
            }
            
            project.IsDirty = false;
            SerializeToXml<Project>(project, saveLocation);
            UpdateApplicationTitle();
        }

        private void objectListViewMain_DoubleClick(object sender, EventArgs e)
        {
            //ProjectFile projectFile = objectListViewMain.SelectedObject as ProjectFile;
            //if (projectFile != null)
            //{
            //    OpenFileDialog od = new OpenFileDialog();
            //    od.InitialDirectory = _project.CurrentPath;
            //    od.Filter = "ADPCM Files (*.wav)| *.wav | All Files | *.*";
            //    DialogResult dr = od.ShowDialog();
            //    if (dr == DialogResult.OK)
            //    {
            //        projectFile.SourceFilename = od.FileName;
            //    }
            //}
        }

        public T DeserializeToObject<T>(string filepath) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StreamReader sr = new StreamReader(filepath))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public static void SerializeToXml<T>(T anyobject, string xmlFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType());

            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {
                xmlSerializer.Serialize(writer, anyobject);
            }
        }

        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "All Files | *.*";
            od.Multiselect = true;
            od.ValidateNames = true;
            od.CheckFileExists = true;
            od.CheckPathExists = true;
            DialogResult dr = od.ShowDialog();
            if (dr == DialogResult.OK)
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "MSM Project Files(*.msmp)| *.msmp";
                DialogResult sr = sd.ShowDialog();
                if (sr == DialogResult.OK)
                {
                    string projectName = Path.GetFileNameWithoutExtension(sd.FileName);
                    List<byte> allBytes = new List<byte>();
                    foreach (string file in od.FileNames)
                    {
                        byte[] bytes = File.ReadAllBytes(file);
                        allBytes.AddRange(bytes);
                    }
                    Project project = new Project();
                    project.Id = Guid.NewGuid();
                    project.CurrentPath = Path.GetDirectoryName(sd.FileName);
                    //project.FileName = sd.FileName;
                    project.ProjectName = projectName;
                    project.ParseBinary(allBytes.ToArray());
                    //save all the file binaries
                    foreach(ProjectFile projectFile in project.ProjectFiles)
                    {
                        projectFile.Id = Guid.NewGuid();
                        //File.WriteAllBytes(Path.Combine(project.CurrentPath, projectFile.SourceFilename), projectFile.OkiADPCM);
                        projectFile.Status = ProjectFile.ProjectFileStatus.Good;
                        projectFile.Descripton = "-";
                    }
                    project.IsDirty = false;
                    SerializeToXml<Project>(project, Path.Combine(project.CurrentPath, sd.FileName));
                    LoadProject(project);
                }
            }
        }

        private void toolStripButtonPlay_Click(object sender, EventArgs e)
        {
            ProjectFile projectFile = objectListViewMain.SelectedObject as ProjectFile;
            if (projectFile != null)
            {
                if (projectFile.OkiADPCM.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    using (BinaryWriter writer = new BinaryWriter(ms))
                    {
                        //Samples * 2 because we have 2 nibbles per byte
                        WAVUtility.AddOkiADPCMDefaultHeader(projectFile.OkiADPCM.Length * 2, writer);
                        MameOKI.Decode(projectFile.OkiADPCM, writer);
                        //reset position so we can play it back now
                        ms.Position = 0;
                        SoundPlayer simpleSound = new SoundPlayer(ms);
                        simpleSound.Play();
                    }
                }
                else
                {
                    MessageBox.Show("File has zero bytes.", "File Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "MSM Project Files(*.msmp)| *.msmp";
            od.Multiselect = false;
            DialogResult dr = od.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Project project = DeserializeToObject<Project>(od.FileName);
                project.CurrentPath = Path.GetDirectoryName(od.FileName);
                LoadProject(project);
            }
        }

        private void toolStripButtonExportWAV_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (_project != null && Directory.Exists(_project.CurrentPath))
            {
                fb.SelectedPath = _project.CurrentPath;
            }
            DialogResult dr = fb.ShowDialog();
            if (dr == DialogResult.OK)
            {
                foreach (var pf in objectListViewMain.SelectedObjects)
                {
                    ProjectFile projectFile = pf as ProjectFile;
                    if (projectFile != null)
                    {
                        string fileName = _project.ProjectName + "_" + projectFile.Index.ToString("D3") + ".wav";
                        using (FileStream fs = new FileStream(Path.Combine(fb.SelectedPath, fileName), FileMode.OpenOrCreate))
                        using (BinaryWriter writer = new BinaryWriter(fs))
                        {
                            //Samples * 2 because we have 2 nibbles per byte
                            WAVUtility.AddOkiADPCMDefaultHeader(projectFile.OkiADPCM.Length * 2, writer);
                            MameOKI.Decode(projectFile.OkiADPCM, writer);
                        }
                    }
                }
            }
        }

        private void objectListViewMain_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            ((ProjectFile)e.RowObject).Descripton = (string)e.NewValue;
            _project.IsDirty = true;
            objectListViewMain.RefreshObjects(_project.ProjectFiles);
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            SaveProject(_project, false);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_project != null && _project.IsDirty)
            {
                DialogResult dr = MessageBox.Show("Current project has not been saved and changes will be lost. Do you still want to exit?", "Exit without saving?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            if (_project != null)
            {
                SaveProject(_project, true);
            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (_project != null)
            {
                if (objectListViewMain.SelectedObjects.Count > 0)
                {
                    objectListViewMain.SuspendLayout();
                    foreach (ProjectFile projectFile in objectListViewMain.SelectedObjects.Cast<ProjectFile>())
                    {
                        _project.ProjectFiles.Remove(projectFile);
                    }
                    _project.ReIndexProjectFiles();
                    objectListViewMain.SetObjects(_project.ProjectFiles);
                    objectListViewMain.ResumeLayout();
                }
                else
                {
                    MessageBox.Show("Nothing selected to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripButtonCreateROM_Click(object sender, EventArgs e)
        {
            if (_project != null)
            {
                ExportDialog xd = new ExportDialog();
                if (!String.IsNullOrEmpty(_project.LastExport) && Directory.Exists(_project.LastExport))
                {
                    xd.FilePathName = _project.LastExport;
                }
                else
                {
                    xd.FilePathName = Path.Combine(_project.CurrentPath, _project.ProjectName + ".bin");
                }
                DialogResult dr = xd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //save current project into ADPCM binary image for MSM6295
                    using (MemoryStream ms = new MemoryStream())
                    using (BinaryWriter writer = new BinaryWriter(ms))
                    {
                        _project.ToBinary(writer);
                        //check final length
                        if (ms.Position > 0x40000)
                        {
                            MessageBox.Show("Binary File is larger than the maximum design size of the OKI MSM6295, some commands will not be played properly. The full binary has been written but you will need to truncate to a max of 0x40000 bytes (256K bytes)", "Compilation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (xd.PadToFullSize)
                            {
                                long padBytes = 0x40000 - ms.Position;
                                while( padBytes > 0)
                                {
                                    writer.Write((byte)0);
                                    padBytes--;
                                }
                            }
                        }
                        using (FileStream fs = new FileStream(xd.FilePathName, FileMode.OpenOrCreate))
                        {
                            ms.Position = 0;
                            ms.CopyTo(fs);
                            fs.Flush();
                        }
                        MessageBox.Show("Project exported sucessfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    _project.LastExport = xd.FilePathName;
                }
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if (_project != null)
            {
                //browse for .wav files to import
                OpenFileDialog od = new OpenFileDialog();
                od.InitialDirectory = _project.CurrentPath;
                od.Filter = "WAV Files(*.wav)| *.wav|ADCPM Binary Files(*.bin)| *.bin";
                od.Multiselect = true;
                od.CheckFileExists = true;
                od.CheckPathExists = true;
                DialogResult dr = od.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (Path.GetExtension(od.FileName).ToLower() == ".bin")
                    {
                        foreach(string importFile in od.FileNames)
                        {
                            ProjectFile pf = new ProjectFile();
                            pf.Descripton = "Imported from " + Path.GetFileName(importFile);
                            pf.Id = Guid.NewGuid();
                            pf.SourceFilename = Path.GetFileName(importFile);
                            pf.Status = ProjectFile.ProjectFileStatus.Good;
                            pf.OkiADPCM = File.ReadAllBytes(importFile);
                            _project.ProjectFiles.Add(pf);
                        }
                    }
                    else
                    {
                        ImportWaveFiles(_project, od.FileNames.ToList());
                    }
                    _project.ReIndexProjectFiles();
                    objectListViewMain.SetObjects(_project.ProjectFiles);
                }
            }
            else
            {
                MessageBox.Show("There is no project loaded to add files to.", "Add Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ImportWaveFiles(Project project, List<String> importFiles)
        {
            List<WavInfo> importInfo = new List<WavInfo>();
            foreach (string importFile in importFiles)
            {
                byte[] wavBytes = File.ReadAllBytes(importFile);
                WavInfo info = WAVUtility.GetWavInfo(wavBytes);
                info.FileName = Path.GetFileName(importFile);
                importInfo.Add(info);
            }
            //validate all infos..
            if (importInfo.Where(i => !i.IsValid).Count() > 0)
            {
                //there are errors, show them here to user
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("There were errors detected on the file(s) selected for import");
                sb.AppendLine("");
                foreach (WavInfo info in importInfo.Where(i => !i.IsValid))
                {
                    sb.AppendLine(info.FileName + ": " + info.ValidationErrors);
                }
                MessageBox.Show(sb.ToString(), "Import Errors", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //things are looking okay here... import each item
                foreach (WavInfo info in importInfo)
                {
                    ProjectFile pf = new ProjectFile();
                    pf.Descripton = "Imported from " + info.FileName;
                    pf.Id = Guid.NewGuid();
                    pf.SourceFilename = info.FileName;
                    pf.Status = ProjectFile.ProjectFileStatus.Good;

                    using (MemoryStream ms = new MemoryStream())
                    using (BinaryWriter writer = new BinaryWriter(ms))
                    {
                        MameOKI.Encode(info.Samples, writer);
                        writer.Flush();
                        ms.Position = 0;
                        pf.OkiADPCM = ms.ToArray();
                    }
                    pf.Index = _project.ProjectFiles.Count;
                    _project.ProjectFiles.Add(pf);
                }
            }
        }

        private void toolStripButtonUp_Click(object sender, EventArgs e)
        {
            if (_project != null)
            {
                if (objectListViewMain.SelectedObjects.Count > 0)
                {
                    if (objectListViewMain.SelectedObjects.Count == 1) {
                        objectListViewMain.SuspendLayout();
                        ProjectFile projectFile = objectListViewMain.SelectedObject as ProjectFile;
                        if(projectFile != null)
                        {
                            int currentIndex = _project.ProjectFiles.IndexOf(projectFile);
                            if (currentIndex > 0)
                            {
                                int targetIndex = currentIndex - 1;
                                _project.ProjectFiles.RemoveAt(currentIndex);
                                _project.ProjectFiles.Insert(targetIndex, projectFile);
                                _project.ReIndexProjectFiles();
                                objectListViewMain.SetObjects(_project.ProjectFiles.OrderBy(f => f.Index));
                                objectListViewMain.SelectObject(projectFile);
                            }
                        }
                        objectListViewMain.ResumeLayout();
                    }
                    else
                    {
                        MessageBox.Show("Select one item to move.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Nothing selected to move.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripButtonDown_Click(object sender, EventArgs e)
        {
            if (_project != null)
            {
                if (objectListViewMain.SelectedObjects.Count > 0)
                {
                    if (objectListViewMain.SelectedObjects.Count == 1)
                    {
                        objectListViewMain.SuspendLayout();
                        ProjectFile projectFile = objectListViewMain.SelectedObject as ProjectFile;
                        if (projectFile != null)
                        {
                            int currentIndex = _project.ProjectFiles.IndexOf(projectFile);
                            if (currentIndex < _project.ProjectFiles.Count -1)
                            {
                                int targetIndex = projectFile.Index;
                                _project.ProjectFiles.RemoveAt(currentIndex);
                                _project.ProjectFiles.Insert(targetIndex, projectFile);
                                _project.ReIndexProjectFiles();
                                objectListViewMain.SetObjects(_project.ProjectFiles.OrderBy(f => f.Index));
                                objectListViewMain.SelectObject(projectFile);
                            }
                        }
                        objectListViewMain.ResumeLayout();
                    }
                    else
                    {
                        MessageBox.Show("Select one item to move.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Nothing selected to move.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void saveSampleAswavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectFile projectFile = objectListViewMain.SelectedObject as ProjectFile;
            if (projectFile != null)
            {
                if (projectFile.OkiADPCM.Length > 0)
                {
                    SaveFileDialog sd = new SaveFileDialog();
                    sd.InitialDirectory = _project.CurrentPath;
                    sd.Filter = "WAV Files(*.wav)| *.wav";
                    DialogResult dr = sd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        using (FileStream fs = new FileStream(sd.FileName, FileMode.OpenOrCreate))
                        using (BinaryWriter writer = new BinaryWriter(fs))
                        {
                            //Samples * 2 because we have 2 nibbles per byte
                            WAVUtility.AddOkiADPCMDefaultHeader(projectFile.OkiADPCM.Length * 2, writer);
                            MameOKI.Decode(projectFile.OkiADPCM, writer);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("File has zero bytes.", "File Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }

        private void saveSampleAsbinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectFile projectFile = objectListViewMain.SelectedObject as ProjectFile;
            if (projectFile != null)
            {
                if (projectFile.OkiADPCM.Length > 0)
                {
                    SaveFileDialog sd = new SaveFileDialog();
                    sd.InitialDirectory = _project.CurrentPath;
                    sd.Filter = "Binary Files(*.bin)| *.bin";
                    DialogResult dr = sd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        using (FileStream fs = new FileStream(sd.FileName, FileMode.OpenOrCreate))
                        {
                            foreach(byte b in projectFile.OkiADPCM)
                            {
                                fs.WriteByte(b);
                            }
                            fs.Flush();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("File has zero bytes.", "File Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }
    }
}
