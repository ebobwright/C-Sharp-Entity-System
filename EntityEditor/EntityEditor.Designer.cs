namespace EntityEditor
{
	partial class EntityEditor
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
            this.btnDeleteEntity = new System.Windows.Forms.Button();
            this.btnAddEntity = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteComponent = new System.Windows.Forms.Button();
            this.tabComponents = new System.Windows.Forms.TabControl();
            this.ddlComponents = new System.Windows.Forms.ComboBox();
            this.btnAddComponent = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnApplyEntityChanges = new System.Windows.Forms.Button();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEntityID = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ddlAssemblages = new System.Windows.Forms.ComboBox();
            this.btnCreateAssemblage = new System.Windows.Forms.Button();
            this.lstEntityList = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboClickTo = new System.Windows.Forms.ComboBox();
            this.rbWall = new System.Windows.Forms.RadioButton();
            this.rbEntity = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDeleteEntity
            // 
            this.btnDeleteEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteEntity.Location = new System.Drawing.Point(87, 339);
            this.btnDeleteEntity.Name = "btnDeleteEntity";
            this.btnDeleteEntity.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteEntity.TabIndex = 3;
            this.btnDeleteEntity.Text = "Delete";
            this.btnDeleteEntity.UseVisualStyleBackColor = true;
            this.btnDeleteEntity.Click += new System.EventHandler(this.btnDeleteEntity_Click);
            // 
            // btnAddEntity
            // 
            this.btnAddEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddEntity.Location = new System.Drawing.Point(6, 339);
            this.btnAddEntity.Name = "btnAddEntity";
            this.btnAddEntity.Size = new System.Drawing.Size(75, 23);
            this.btnAddEntity.TabIndex = 3;
            this.btnAddEntity.Text = "Add";
            this.btnAddEntity.UseVisualStyleBackColor = true;
            this.btnAddEntity.Click += new System.EventHandler(this.btnAddEntity_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDeleteComponent);
            this.groupBox1.Controls.Add(this.tabComponents);
            this.groupBox1.Controls.Add(this.ddlComponents);
            this.groupBox1.Controls.Add(this.btnAddComponent);
            this.groupBox1.Location = new System.Drawing.Point(191, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 391);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Components";
            // 
            // btnDeleteComponent
            // 
            this.btnDeleteComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteComponent.Location = new System.Drawing.Point(216, 358);
            this.btnDeleteComponent.Name = "btnDeleteComponent";
            this.btnDeleteComponent.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteComponent.TabIndex = 3;
            this.btnDeleteComponent.Text = "Delete";
            this.btnDeleteComponent.UseVisualStyleBackColor = true;
            this.btnDeleteComponent.Click += new System.EventHandler(this.btnDeleteComponent_Click);
            // 
            // tabComponents
            // 
            this.tabComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabComponents.Location = new System.Drawing.Point(7, 19);
            this.tabComponents.Multiline = true;
            this.tabComponents.Name = "tabComponents";
            this.tabComponents.SelectedIndex = 0;
            this.tabComponents.Size = new System.Drawing.Size(284, 333);
            this.tabComponents.TabIndex = 2;
            // 
            // ddlComponents
            // 
            this.ddlComponents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlComponents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlComponents.FormattingEnabled = true;
            this.ddlComponents.Location = new System.Drawing.Point(88, 360);
            this.ddlComponents.Name = "ddlComponents";
            this.ddlComponents.Size = new System.Drawing.Size(121, 21);
            this.ddlComponents.TabIndex = 1;
            // 
            // btnAddComponent
            // 
            this.btnAddComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddComponent.Location = new System.Drawing.Point(7, 358);
            this.btnAddComponent.Name = "btnAddComponent";
            this.btnAddComponent.Size = new System.Drawing.Size(75, 23);
            this.btnAddComponent.TabIndex = 0;
            this.btnAddComponent.Text = "Add";
            this.btnAddComponent.UseVisualStyleBackColor = true;
            this.btnAddComponent.Click += new System.EventHandler(this.btnAddComponent_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnApplyEntityChanges);
            this.groupBox2.Controls.Add(this.txtEntityName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblEntityID);
            this.groupBox2.Location = new System.Drawing.Point(191, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 59);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entity Info";
            // 
            // btnApplyEntityChanges
            // 
            this.btnApplyEntityChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyEntityChanges.Location = new System.Drawing.Point(248, 20);
            this.btnApplyEntityChanges.Name = "btnApplyEntityChanges";
            this.btnApplyEntityChanges.Size = new System.Drawing.Size(43, 23);
            this.btnApplyEntityChanges.TabIndex = 2;
            this.btnApplyEntityChanges.Text = "Apply";
            this.btnApplyEntityChanges.UseVisualStyleBackColor = true;
            this.btnApplyEntityChanges.Click += new System.EventHandler(this.btnApplyEntityChanges_Click);
            // 
            // txtEntityName
            // 
            this.txtEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityName.Location = new System.Drawing.Point(178, 22);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(64, 20);
            this.txtEntityName.TabIndex = 1;
            this.txtEntityName.Validated += new System.EventHandler(this.btnApplyEntityChanges_Click);
            this.txtEntityName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEntityName_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entity Name: ";
            // 
            // lblEntityID
            // 
            this.lblEntityID.AutoSize = true;
            this.lblEntityID.Location = new System.Drawing.Point(8, 25);
            this.lblEntityID.Name = "lblEntityID";
            this.lblEntityID.Size = new System.Drawing.Size(74, 13);
            this.lblEntityID.TabIndex = 0;
            this.lblEntityID.Text = "EntityID: 9999";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.ddlAssemblages);
            this.groupBox3.Controls.Add(this.btnCreateAssemblage);
            this.groupBox3.Controls.Add(this.btnAddEntity);
            this.groupBox3.Controls.Add(this.btnDeleteEntity);
            this.groupBox3.Controls.Add(this.lstEntityList);
            this.groupBox3.Location = new System.Drawing.Point(12, 111);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(173, 372);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Entity List";
            // 
            // ddlAssemblages
            // 
            this.ddlAssemblages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAssemblages.FormattingEnabled = true;
            this.ddlAssemblages.Location = new System.Drawing.Point(7, 47);
            this.ddlAssemblages.Name = "ddlAssemblages";
            this.ddlAssemblages.Size = new System.Drawing.Size(160, 21);
            this.ddlAssemblages.TabIndex = 5;
            // 
            // btnCreateAssemblage
            // 
            this.btnCreateAssemblage.Location = new System.Drawing.Point(7, 20);
            this.btnCreateAssemblage.Name = "btnCreateAssemblage";
            this.btnCreateAssemblage.Size = new System.Drawing.Size(160, 23);
            this.btnCreateAssemblage.TabIndex = 4;
            this.btnCreateAssemblage.Text = "Create Assemblage";
            this.btnCreateAssemblage.UseVisualStyleBackColor = true;
            this.btnCreateAssemblage.Click += new System.EventHandler(this.btnCreateAssemblage_Click);
            // 
            // lstEntityList
            // 
            this.lstEntityList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstEntityList.DisplayMember = "EntityName";
            this.lstEntityList.FormattingEnabled = true;
            this.lstEntityList.Location = new System.Drawing.Point(7, 74);
            this.lstEntityList.Name = "lstEntityList";
            this.lstEntityList.Size = new System.Drawing.Size(161, 251);
            this.lstEntityList.TabIndex = 0;
            this.lstEntityList.SelectedIndexChanged += new System.EventHandler(this.lstEntityList_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(500, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "File";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Entity System XML| *.esx";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Entity System XML| *.esx";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.cboClickTo);
            this.groupBox4.Controls.Add(this.rbWall);
            this.groupBox4.Controls.Add(this.rbEntity);
            this.groupBox4.Location = new System.Drawing.Point(12, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(173, 77);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selection Mode";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Click To:";
            // 
            // cboClickTo
            // 
            this.cboClickTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClickTo.FormattingEnabled = true;
            this.cboClickTo.Items.AddRange(new object[] {
            "None",
            "16",
            "32",
            "64"});
            this.cboClickTo.Location = new System.Drawing.Point(74, 50);
            this.cboClickTo.Name = "cboClickTo";
            this.cboClickTo.Size = new System.Drawing.Size(88, 21);
            this.cboClickTo.TabIndex = 3;
            this.cboClickTo.SelectedIndexChanged += new System.EventHandler(this.cboClickTo_SelectedIndexChanged);
            // 
            // rbWall
            // 
            this.rbWall.AutoSize = true;
            this.rbWall.Location = new System.Drawing.Point(83, 22);
            this.rbWall.Name = "rbWall";
            this.rbWall.Size = new System.Drawing.Size(79, 17);
            this.rbWall.TabIndex = 0;
            this.rbWall.TabStop = true;
            this.rbWall.Text = "Wall Vertex";
            this.rbWall.UseVisualStyleBackColor = true;
            // 
            // rbEntity
            // 
            this.rbEntity.AutoSize = true;
            this.rbEntity.Location = new System.Drawing.Point(7, 22);
            this.rbEntity.Name = "rbEntity";
            this.rbEntity.Size = new System.Drawing.Size(51, 17);
            this.rbEntity.TabIndex = 0;
            this.rbEntity.TabStop = true;
            this.rbEntity.Text = "Entity";
            this.rbEntity.UseVisualStyleBackColor = true;
            this.rbEntity.CheckedChanged += new System.EventHandler(this.rbEntity_CheckedChanged);
            // 
            // EntityEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 490);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(508, 487);
            this.Name = "EntityEditor";
            this.Text = "Entity Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button btnDeleteEntity;
		private System.Windows.Forms.Button btnAddEntity;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabControl tabComponents;
		private System.Windows.Forms.ComboBox ddlComponents;
		private System.Windows.Forms.Button btnAddComponent;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtEntityName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblEntityID;
		private System.Windows.Forms.Button btnApplyEntityChanges;
		private System.Windows.Forms.Button btnDeleteComponent;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ComboBox ddlAssemblages;
		private System.Windows.Forms.Button btnCreateAssemblage;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbWall;
        private System.Windows.Forms.RadioButton rbEntity;
        private System.Windows.Forms.ListBox lstEntityList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboClickTo;
	}
}

