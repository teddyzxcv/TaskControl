﻿
namespace TaskControlForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newPrjStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newTaskStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.userStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.taskmanageStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeNameStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPrjStripMenuItem1,
            this.newTaskStripMenuItem1,
            this.userStripMenuItem1,
            this.taskmanageStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(484, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newPrjStripMenuItem1
            // 
            this.newPrjStripMenuItem1.Name = "newPrjStripMenuItem1";
            this.newPrjStripMenuItem1.Size = new System.Drawing.Size(90, 21);
            this.newPrjStripMenuItem1.Text = "New Project";
            this.newPrjStripMenuItem1.Click += new System.EventHandler(this.newPrjStripMenuItem1_Click);
            // 
            // newTaskStripMenuItem1
            // 
            this.newTaskStripMenuItem1.Name = "newTaskStripMenuItem1";
            this.newTaskStripMenuItem1.Size = new System.Drawing.Size(77, 21);
            this.newTaskStripMenuItem1.Text = "New Task";
            this.newTaskStripMenuItem1.Click += new System.EventHandler(this.newTaskStripMenuItem1_Click);
            // 
            // userStripMenuItem1
            // 
            this.userStripMenuItem1.Name = "userStripMenuItem1";
            this.userStripMenuItem1.Size = new System.Drawing.Size(99, 21);
            this.userStripMenuItem1.Text = "User Manage";
            this.userStripMenuItem1.Click += new System.EventHandler(this.userStripMenuItem1_Click);
            // 
            // taskmanageStripMenuItem1
            // 
            this.taskmanageStripMenuItem1.Name = "taskmanageStripMenuItem1";
            this.taskmanageStripMenuItem1.Size = new System.Drawing.Size(152, 21);
            this.taskmanageStripMenuItem1.Text = "Manage Selected Task";
            this.taskmanageStripMenuItem1.Click += new System.EventHandler(this.taskmanageStripMenuItem1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Size = new System.Drawing.Size(484, 436);
            this.splitContainer1.SplitterDistance = 219;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(219, 436);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(261, 436);
            this.listBox1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeNameStripMenuItem1,
            this.deleteStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 48);
            // 
            // changeNameStripMenuItem1
            // 
            this.changeNameStripMenuItem1.Name = "changeNameStripMenuItem1";
            this.changeNameStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.changeNameStripMenuItem1.Text = "Change Name";
            this.changeNameStripMenuItem1.Click += new System.EventHandler(this.changeNameStripMenuItem1_Click);
            // 
            // deleteStripMenuItem1
            // 
            this.deleteStripMenuItem1.Name = "deleteStripMenuItem1";
            this.deleteStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.deleteStripMenuItem1.Text = "Delete";
            this.deleteStripMenuItem1.Click += new System.EventHandler(this.deleteStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "Form1";
            this.Text = "Task Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newPrjStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem newTaskStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem userStripMenuItem1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changeNameStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem taskmanageStripMenuItem1;
    }
}

