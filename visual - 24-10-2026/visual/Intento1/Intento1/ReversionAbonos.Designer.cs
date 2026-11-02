namespace Intento1
{
    partial class ReversionAbonos
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtReversionesPendientes = new System.Windows.Forms.TextBox();
            this.cmbMotivo = new System.Windows.Forms.ComboBox();
            this.btnSolicitarReversion = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.btnVolverMenu = new System.Windows.Forms.Button();
            this.dgvReversiones = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReversiones)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "REVERSIÓN DE ABONOS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Codigo de operación :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Motivo de reversión :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "PILA DE REVERSIONES PENDIENTES";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 477);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Reversiones pendientes :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(229, 89);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(121, 20);
            this.txtCodigo.TabIndex = 5;
            // 
            // txtReversionesPendientes
            // 
            this.txtReversionesPendientes.Location = new System.Drawing.Point(173, 474);
            this.txtReversionesPendientes.Name = "txtReversionesPendientes";
            this.txtReversionesPendientes.Size = new System.Drawing.Size(100, 20);
            this.txtReversionesPendientes.TabIndex = 6;
            this.txtReversionesPendientes.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // cmbMotivo
            // 
            this.cmbMotivo.FormattingEnabled = true;
            this.cmbMotivo.Location = new System.Drawing.Point(229, 126);
            this.cmbMotivo.Name = "cmbMotivo";
            this.cmbMotivo.Size = new System.Drawing.Size(121, 21);
            this.cmbMotivo.TabIndex = 7;
            // 
            // btnSolicitarReversion
            // 
            this.btnSolicitarReversion.Location = new System.Drawing.Point(406, 86);
            this.btnSolicitarReversion.Name = "btnSolicitarReversion";
            this.btnSolicitarReversion.Size = new System.Drawing.Size(133, 23);
            this.btnSolicitarReversion.TabIndex = 8;
            this.btnSolicitarReversion.Text = "Solicitar reversión";
            this.btnSolicitarReversion.UseVisualStyleBackColor = true;
            this.btnSolicitarReversion.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(406, 126);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(133, 23);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(173, 521);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(114, 23);
            this.btnProcesar.TabIndex = 10;
            this.btnProcesar.Text = "Procesar reversión";
            this.btnProcesar.UseVisualStyleBackColor = true;
            // 
            // btnVolverMenu
            // 
            this.btnVolverMenu.Location = new System.Drawing.Point(306, 521);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(114, 23);
            this.btnVolverMenu.TabIndex = 11;
            this.btnVolverMenu.Text = "Volver al menú";
            this.btnVolverMenu.UseVisualStyleBackColor = true;
            this.btnVolverMenu.Click += new System.EventHandler(this.btnVolverMenu_Click);
            // 
            // dgvReversiones
            // 
            this.dgvReversiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReversiones.Location = new System.Drawing.Point(48, 229);
            this.dgvReversiones.Name = "dgvReversiones";
            this.dgvReversiones.Size = new System.Drawing.Size(501, 209);
            this.dgvReversiones.TabIndex = 12;
            // 
            // ReversionAbonos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 591);
            this.Controls.Add(this.dgvReversiones);
            this.Controls.Add(this.btnVolverMenu);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnSolicitarReversion);
            this.Controls.Add(this.cmbMotivo);
            this.Controls.Add(this.txtReversionesPendientes);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ReversionAbonos";
            this.Text = "ReversionAbonos";
            this.Load += new System.EventHandler(this.ReversionAbonos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReversiones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtReversionesPendientes;
        private System.Windows.Forms.ComboBox cmbMotivo;
        private System.Windows.Forms.Button btnSolicitarReversion;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Button btnVolverMenu;
        private System.Windows.Forms.DataGridView dgvReversiones;
    }
}