namespace R6Shack {
    partial class Form1 {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            this.checkBox_lockScroe = new System.Windows.Forms.CheckBox();
            this.label_BEService = new System.Windows.Forms.Label();
            this.label_ifGameRunning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox_lockScroe
            // 
            this.checkBox_lockScroe.AutoSize = true;
            this.checkBox_lockScroe.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_lockScroe.Location = new System.Drawing.Point(85, 91);
            this.checkBox_lockScroe.Name = "checkBox_lockScroe";
            this.checkBox_lockScroe.Size = new System.Drawing.Size(105, 28);
            this.checkBox_lockScroe.TabIndex = 0;
            this.checkBox_lockScroe.Text = "鎖定分數";
            this.checkBox_lockScroe.UseVisualStyleBackColor = true;
            this.checkBox_lockScroe.CheckedChanged += new System.EventHandler(this.checkBox_lockScroe_CheckedChanged);
            // 
            // label_BEService
            // 
            this.label_BEService.AutoSize = true;
            this.label_BEService.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_BEService.Location = new System.Drawing.Point(59, 215);
            this.label_BEService.Name = "label_BEService";
            this.label_BEService.Size = new System.Drawing.Size(169, 24);
            this.label_BEService.TabIndex = 3;
            this.label_BEService.Text = "未偵測到BattleEye";
            // 
            // label_ifGameRunning
            // 
            this.label_ifGameRunning.AutoSize = true;
            this.label_ifGameRunning.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ifGameRunning.Location = new System.Drawing.Point(85, 184);
            this.label_ifGameRunning.Name = "label_ifGameRunning";
            this.label_ifGameRunning.Size = new System.Drawing.Size(105, 20);
            this.label_ifGameRunning.TabIndex = 4;
            this.label_ifGameRunning.Text = "未偵測到遊戲";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label_ifGameRunning);
            this.Controls.Add(this.label_BEService);
            this.Controls.Add(this.checkBox_lockScroe);
            this.Name = "Form1";
            this.Text = "Rainbow 6 Siege 分數外掛";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        protected System.Windows.Forms.CheckBox checkBox_lockScroe;
        private System.Windows.Forms.Label label_BEService;
        private System.Windows.Forms.Label label_ifGameRunning;
    }
}

