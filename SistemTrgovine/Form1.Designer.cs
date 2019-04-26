namespace SistemTrgovine
{
    partial class Form1
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
            this.PNbutton = new System.Windows.Forms.Button();
            this.ASbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PNbutton
            // 
            this.PNbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PNbutton.Location = new System.Drawing.Point(267, 235);
            this.PNbutton.Name = "PNbutton";
            this.PNbutton.Size = new System.Drawing.Size(267, 95);
            this.PNbutton.TabIndex = 3;
            this.PNbutton.Text = "Prodaja/naplata";
            this.PNbutton.UseVisualStyleBackColor = true;
            this.PNbutton.Click += new System.EventHandler(this.PNbutton_Click);
            // 
            // ASbutton
            // 
            this.ASbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ASbutton.Location = new System.Drawing.Point(267, 120);
            this.ASbutton.Name = "ASbutton";
            this.ASbutton.Size = new System.Drawing.Size(267, 95);
            this.ASbutton.TabIndex = 2;
            this.ASbutton.Text = "Administracija i statiskita";
            this.ASbutton.UseVisualStyleBackColor = true;
            this.ASbutton.Click += new System.EventHandler(this.ASbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PNbutton);
            this.Controls.Add(this.ASbutton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Softver za trgovinu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PNbutton;
        private System.Windows.Forms.Button ASbutton;
    }
}

