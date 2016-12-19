namespace ProgInz_MajasDarbs2
{
    partial class frmMain
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
            this.tmrRecord = new System.Windows.Forms.Timer(this.components);
            this.dgvActions = new System.Windows.Forms.DataGridView();
            this.colX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMouse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colModifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.RepeatCountControl = new System.Windows.Forms.NumericUpDown();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnPlayRecord = new System.Windows.Forms.Button();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.btnStartRecord = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatCountControl)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrRecord
            // 
            this.tmrRecord.Interval = 1;
            this.tmrRecord.Tick += new System.EventHandler(this.tmrRecord_Tick);
            // 
            // dgvActions
            // 
            this.dgvActions.AllowUserToAddRows = false;
            this.dgvActions.AllowUserToDeleteRows = false;
            this.dgvActions.AllowUserToResizeColumns = false;
            this.dgvActions.AllowUserToResizeRows = false;
            this.dgvActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX,
            this.colY,
            this.colPress,
            this.colTime,
            this.colMouse,
            this.colModifier});
            this.dgvActions.Location = new System.Drawing.Point(12, 42);
            this.dgvActions.MultiSelect = false;
            this.dgvActions.Name = "dgvActions";
            this.dgvActions.ReadOnly = true;
            this.dgvActions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvActions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActions.Size = new System.Drawing.Size(447, 275);
            this.dgvActions.TabIndex = 3;
            // 
            // colX
            // 
            this.colX.FillWeight = 90F;
            this.colX.HeaderText = "X";
            this.colX.Name = "colX";
            this.colX.ReadOnly = true;
            this.colX.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colX.Width = 90;
            // 
            // colY
            // 
            this.colY.FillWeight = 90F;
            this.colY.HeaderText = "Y";
            this.colY.Name = "colY";
            this.colY.ReadOnly = true;
            this.colY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPress
            // 
            this.colPress.FillWeight = 90F;
            this.colPress.HeaderText = "Press";
            this.colPress.Name = "colPress";
            this.colPress.ReadOnly = true;
            this.colPress.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colPress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colTime
            // 
            this.colTime.FillWeight = 90F;
            this.colTime.HeaderText = "Time";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTime.Width = 90;
            // 
            // colMouse
            // 
            this.colMouse.FillWeight = 90F;
            this.colMouse.HeaderText = "Mouse";
            this.colMouse.Name = "colMouse";
            this.colMouse.ReadOnly = true;
            this.colMouse.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colMouse.Visible = false;
            this.colMouse.Width = 90;
            // 
            // colModifier
            // 
            this.colModifier.FillWeight = 90F;
            this.colModifier.HeaderText = "Modifier";
            this.colModifier.Name = "colModifier";
            this.colModifier.ReadOnly = true;
            this.colModifier.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colModifier.Visible = false;
            this.colModifier.Width = 90;
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Image = global::ProgInz_MajasDarbs2.Properties.Resources.load;
            this.btnLoadFromFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadFromFile.Location = new System.Drawing.Point(238, 322);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(116, 27);
            this.btnLoadFromFile.TabIndex = 5;
            this.btnLoadFromFile.Text = "LoadFromFile";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // RepeatCountControl
            // 
            this.RepeatCountControl.Location = new System.Drawing.Point(383, 14);
            this.RepeatCountControl.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.RepeatCountControl.Name = "RepeatCountControl";
            this.RepeatCountControl.Size = new System.Drawing.Size(71, 20);
            this.RepeatCountControl.TabIndex = 6;
            this.RepeatCountControl.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RepeatCountControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RepeatCountControl_KeyPress);
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Image = global::ProgInz_MajasDarbs2.Properties.Resources.save;
            this.btnSaveToFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveToFile.Location = new System.Drawing.Point(116, 322);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(116, 27);
            this.btnSaveToFile.TabIndex = 4;
            this.btnSaveToFile.Text = "SaveToFile";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnPlayRecord
            // 
            this.btnPlayRecord.Image = global::ProgInz_MajasDarbs2.Properties.Resources.play;
            this.btnPlayRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlayRecord.Location = new System.Drawing.Point(261, 9);
            this.btnPlayRecord.Name = "btnPlayRecord";
            this.btnPlayRecord.Size = new System.Drawing.Size(116, 27);
            this.btnPlayRecord.TabIndex = 2;
            this.btnPlayRecord.Text = "PlayRecord";
            this.btnPlayRecord.UseVisualStyleBackColor = true;
            this.btnPlayRecord.Click += new System.EventHandler(this.btnPlayRecord_Click);
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Image = global::ProgInz_MajasDarbs2.Properties.Resources.stop;
            this.btnStopRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStopRecord.Location = new System.Drawing.Point(139, 9);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(116, 27);
            this.btnStopRecord.TabIndex = 1;
            this.btnStopRecord.Text = "Stop (F12)";
            this.btnStopRecord.UseVisualStyleBackColor = true;
            this.btnStopRecord.Click += new System.EventHandler(this.btnStopRecord_Click);
            // 
            // btnStartRecord
            // 
            this.btnStartRecord.Image = global::ProgInz_MajasDarbs2.Properties.Resources.record;
            this.btnStartRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStartRecord.Location = new System.Drawing.Point(17, 9);
            this.btnStartRecord.Name = "btnStartRecord";
            this.btnStartRecord.Size = new System.Drawing.Size(116, 27);
            this.btnStartRecord.TabIndex = 0;
            this.btnStartRecord.Text = "StartRecord";
            this.btnStartRecord.UseVisualStyleBackColor = true;
            this.btnStartRecord.Click += new System.EventHandler(this.btnStartRecord_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 361);
            this.Controls.Add(this.RepeatCountControl);
            this.Controls.Add(this.btnLoadFromFile);
            this.Controls.Add(this.btnSaveToFile);
            this.Controls.Add(this.dgvActions);
            this.Controls.Add(this.btnPlayRecord);
            this.Controls.Add(this.btnStopRecord);
            this.Controls.Add(this.btnStartRecord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.Text = "ProgInz_MajasDarbs2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatCountControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartRecord;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.Button btnPlayRecord;
        private System.Windows.Forms.Timer tmrRecord;
        private System.Windows.Forms.DataGridView dgvActions;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.NumericUpDown RepeatCountControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn colX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModifier;
    }
}

