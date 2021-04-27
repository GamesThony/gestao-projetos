namespace GestaoProjetos.Views
{
    partial class LoadView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadView));
            this.gradientPanel1 = new GestaoProjetos.Controls.GradientPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pgbProgresso = new GestaoProjetos.Controls.CircularProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Angle = 40F;
            this.gradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.gradientPanel1.BottomColor = System.Drawing.Color.RoyalBlue;
            this.gradientPanel1.Controls.Add(this.label2);
            this.gradientPanel1.Controls.Add(this.pgbProgresso);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.Controls.Add(this.pictureBox1);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.gradientPanel1.Size = new System.Drawing.Size(800, 450);
            this.gradientPanel1.TabIndex = 0;
            this.gradientPanel1.Text = "gradientPanel1";
            this.gradientPanel1.TopColor = System.Drawing.Color.Crimson;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Consolas", 40F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(780, 78);
            this.label1.TabIndex = 1;
            this.label1.Text = "GESTÃO DE PROJETOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbProgresso
            // 
            this.pgbProgresso.BackColor = System.Drawing.Color.Transparent;
            this.pgbProgresso.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgbProgresso.Font = new System.Drawing.Font("Consolas", 10F);
            this.pgbProgresso.ForeColor = System.Drawing.Color.White;
            this.pgbProgresso.Location = new System.Drawing.Point(10, 321);
            this.pgbProgresso.MaximumSize = new System.Drawing.Size(0, 100);
            this.pgbProgresso.MinimumSize = new System.Drawing.Size(0, 100);
            this.pgbProgresso.Name = "pgbProgresso";
            this.pgbProgresso.Progress = 0D;
            this.pgbProgresso.Size = new System.Drawing.Size(780, 100);
            this.pgbProgresso.TabIndex = 2;
            this.pgbProgresso.Text = "0.00%";
            this.pgbProgresso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::GestaoProjetos.Properties.Resources.icone;
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(780, 129);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(10, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(780, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Carregando projetos...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Consolas", 9F);
            this.label3.Location = new System.Drawing.Point(10, 421);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(780, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Desenvolvido por Anthony Almeida Andrade © 2021";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // LoadView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gradientPanel1);
            this.Font = new System.Drawing.Font("Consolas", 12F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestão de Projetos";
            this.gradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Controls.CircularProgressBar pgbProgresso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}