namespace CebuMap
{
    partial class FormMap
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
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.lblStartVertex = new System.Windows.Forms.Label();
            this.lblTargetVertex = new System.Windows.Forms.Label();
            this.cboSearch = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDisplay
            // 
            this.btnDisplay.Location = new System.Drawing.Point(1070, 191);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(163, 42);
            this.btnDisplay.TabIndex = 0;
            this.btnDisplay.Text = "Display Points";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.BtnDisplay_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(1070, 239);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(163, 42);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // panelGraph
            // 
            this.panelGraph.BackgroundImage = global::CebuMap.Properties.Resources.map;
            this.panelGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGraph.Location = new System.Drawing.Point(12, 12);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(1048, 648);
            this.panelGraph.TabIndex = 0;
            // 
            // lblStartVertex
            // 
            this.lblStartVertex.AutoSize = true;
            this.lblStartVertex.Location = new System.Drawing.Point(1066, 67);
            this.lblStartVertex.Name = "lblStartVertex";
            this.lblStartVertex.Size = new System.Drawing.Size(98, 20);
            this.lblStartVertex.TabIndex = 8;
            this.lblStartVertex.Text = "Start Vertex:";
            // 
            // lblTargetVertex
            // 
            this.lblTargetVertex.AutoSize = true;
            this.lblTargetVertex.Location = new System.Drawing.Point(1066, 96);
            this.lblTargetVertex.Name = "lblTargetVertex";
            this.lblTargetVertex.Size = new System.Drawing.Size(109, 20);
            this.lblTargetVertex.TabIndex = 9;
            this.lblTargetVertex.Text = "Target Vertex:";
            // 
            // cboSearch
            // 
            this.cboSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearch.Enabled = false;
            this.cboSearch.FormattingEnabled = true;
            this.cboSearch.Items.AddRange(new object[] {
            "Breadth-First Search",
            "Depth-First Search",
            "Greedy Best-First Search",
            "A* Search"});
            this.cboSearch.Location = new System.Drawing.Point(1070, 12);
            this.cboSearch.Name = "cboSearch";
            this.cboSearch.Size = new System.Drawing.Size(244, 28);
            this.cboSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(1070, 143);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(163, 42);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // FormMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1389, 681);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cboSearch);
            this.Controls.Add(this.lblTargetVertex);
            this.Controls.Add(this.lblStartVertex);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.panelGraph);
            this.Name = "FormMap";
            this.Text = "Cebu Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblStartVertex;
        private System.Windows.Forms.Label lblTargetVertex;
        private System.Windows.Forms.ComboBox cboSearch;
        private System.Windows.Forms.Button btnSearch;
    }
}

