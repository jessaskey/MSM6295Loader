namespace MSM6295Loader
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCreateROM = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPlay = new System.Windows.Forms.ToolStripButton();
            this.objectListViewMain = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnIndex = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStripProjectFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveSampleAswavToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewMain)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStripProjectFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNew,
            this.toolStripButtonOpen,
            this.toolStripButtonImport,
            this.toolStripButtonSave,
            this.toolStripButtonSaveAs,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripButtonAdd,
            this.toolStripButtonDelete,
            this.toolStripButtonCreateROM,
            this.toolStripSeparator2,
            this.toolStripButtonUp,
            this.toolStripButtonDown,
            this.toolStripSeparator3,
            this.toolStripButtonPlay});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNew.Image")));
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNew.Text = "New Empty Project";
            this.toolStripButtonNew.Click += new System.EventHandler(this.toolStripButtonNew_Click);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.Text = "Open Existing Project";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripButtonImport
            // 
            this.toolStripButtonImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImport.Image")));
            this.toolStripButtonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImport.Name = "toolStripButtonImport";
            this.toolStripButtonImport.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonImport.Text = "Import from ROMs";
            this.toolStripButtonImport.Click += new System.EventHandler(this.toolStripButtonImport_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Text = "Save Project";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveAs.Image")));
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSaveAs.Text = "Save Project As";
            this.toolStripButtonSaveAs.Click += new System.EventHandler(this.toolStripButtonSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel1.Text = "Files:";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAdd.Text = "Add New File to Project";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDelete.Text = "Delete Selected File from Project";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonCreateROM
            // 
            this.toolStripButtonCreateROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCreateROM.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCreateROM.Image")));
            this.toolStripButtonCreateROM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateROM.Name = "toolStripButtonCreateROM";
            this.toolStripButtonCreateROM.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCreateROM.Text = "Create ROM Images";
            this.toolStripButtonCreateROM.Click += new System.EventHandler(this.toolStripButtonCreateROM_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonUp
            // 
            this.toolStripButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUp.Image")));
            this.toolStripButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUp.Name = "toolStripButtonUp";
            this.toolStripButtonUp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUp.Text = "Move Selected File Up";
            this.toolStripButtonUp.Click += new System.EventHandler(this.toolStripButtonUp_Click);
            // 
            // toolStripButtonDown
            // 
            this.toolStripButtonDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDown.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDown.Image")));
            this.toolStripButtonDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDown.Name = "toolStripButtonDown";
            this.toolStripButtonDown.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDown.Text = "Move Selected File Down";
            this.toolStripButtonDown.Click += new System.EventHandler(this.toolStripButtonDown_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonPlay
            // 
            this.toolStripButtonPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPlay.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPlay.Image")));
            this.toolStripButtonPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlay.Name = "toolStripButtonPlay";
            this.toolStripButtonPlay.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPlay.Text = "Play Selected File";
            this.toolStripButtonPlay.Click += new System.EventHandler(this.toolStripButtonPlay_Click);
            // 
            // objectListViewMain
            // 
            this.objectListViewMain.AllColumns.Add(this.olvColumnIndex);
            this.objectListViewMain.AllColumns.Add(this.olvColumnStatus);
            this.objectListViewMain.AllColumns.Add(this.olvColumnSize);
            this.objectListViewMain.AllColumns.Add(this.olvColumnDescription);
            this.objectListViewMain.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.objectListViewMain.CellEditUseWholeCell = false;
            this.objectListViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnIndex,
            this.olvColumnStatus,
            this.olvColumnSize,
            this.olvColumnDescription});
            this.objectListViewMain.ContextMenuStrip = this.contextMenuStripProjectFile;
            this.objectListViewMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewMain.FullRowSelect = true;
            this.objectListViewMain.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.objectListViewMain.HideSelection = false;
            this.objectListViewMain.Location = new System.Drawing.Point(0, 25);
            this.objectListViewMain.Name = "objectListViewMain";
            this.objectListViewMain.ShowGroups = false;
            this.objectListViewMain.Size = new System.Drawing.Size(800, 425);
            this.objectListViewMain.TabIndex = 1;
            this.objectListViewMain.UseCompatibleStateImageBehavior = false;
            this.objectListViewMain.UseTranslucentSelection = true;
            this.objectListViewMain.View = System.Windows.Forms.View.Details;
            this.objectListViewMain.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.objectListViewMain_CellEditFinished);
            this.objectListViewMain.DoubleClick += new System.EventHandler(this.objectListViewMain_DoubleClick);
            // 
            // olvColumnIndex
            // 
            this.olvColumnIndex.AspectName = "Index";
            this.olvColumnIndex.IsEditable = false;
            this.olvColumnIndex.Text = "#";
            this.olvColumnIndex.Width = 34;
            // 
            // olvColumnStatus
            // 
            this.olvColumnStatus.AspectName = "Status";
            this.olvColumnStatus.IsEditable = false;
            this.olvColumnStatus.Text = "Status";
            this.olvColumnStatus.Width = 97;
            // 
            // olvColumnSize
            // 
            this.olvColumnSize.AspectName = "Size";
            this.olvColumnSize.Text = "Size";
            // 
            // olvColumnDescription
            // 
            this.olvColumnDescription.AspectName = "Descripton";
            this.olvColumnDescription.CellEditUseWholeCell = true;
            this.olvColumnDescription.Text = "Descripton";
            this.olvColumnDescription.Width = 547;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(785, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStripProjectFile
            // 
            this.contextMenuStripProjectFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSampleAswavToolStripMenuItem});
            this.contextMenuStripProjectFile.Name = "contextMenuStripProjectFile";
            this.contextMenuStripProjectFile.Size = new System.Drawing.Size(182, 26);
            // 
            // saveSampleAswavToolStripMenuItem
            // 
            this.saveSampleAswavToolStripMenuItem.Name = "saveSampleAswavToolStripMenuItem";
            this.saveSampleAswavToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.saveSampleAswavToolStripMenuItem.Text = "Save Sample as .wav";
            this.saveSampleAswavToolStripMenuItem.Click += new System.EventHandler(this.saveSampleAswavToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.objectListViewMain);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "MSM6295Loader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewMain)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStripProjectFile.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonUp;
        private System.Windows.Forms.ToolStripButton toolStripButtonDown;
        private BrightIdeasSoftware.ObjectListView objectListViewMain;
        private BrightIdeasSoftware.OLVColumn olvColumnIndex;
        private BrightIdeasSoftware.OLVColumn olvColumnStatus;
        private System.Windows.Forms.ToolStripButton toolStripButtonImport;
        private BrightIdeasSoftware.OLVColumn olvColumnDescription;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateROM;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private BrightIdeasSoftware.OLVColumn olvColumnSize;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripProjectFile;
        private System.Windows.Forms.ToolStripMenuItem saveSampleAswavToolStripMenuItem;
    }
}

