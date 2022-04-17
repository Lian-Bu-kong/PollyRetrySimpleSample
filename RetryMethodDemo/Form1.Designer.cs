
namespace WindowsFormsRetryDemo
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_normal_retry = new System.Windows.Forms.Button();
            this.richTextBox_Info = new System.Windows.Forms.RichTextBox();
            this.button_PollyRetry = new System.Windows.Forms.Button();
            this.button_AsyncRetry = new System.Windows.Forms.Button();
            this.button_pollyRetryWithDelay = new System.Windows.Forms.Button();
            this.button_asycn_retry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_normal_retry
            // 
            this.button_normal_retry.Location = new System.Drawing.Point(38, 32);
            this.button_normal_retry.Name = "button_normal_retry";
            this.button_normal_retry.Size = new System.Drawing.Size(139, 23);
            this.button_normal_retry.TabIndex = 0;
            this.button_normal_retry.Text = " Normal Retry";
            this.button_normal_retry.UseVisualStyleBackColor = true;
            this.button_normal_retry.Click += new System.EventHandler(this.button_normal_retry_Click);
            // 
            // richTextBox_Info
            // 
            this.richTextBox_Info.Location = new System.Drawing.Point(240, 32);
            this.richTextBox_Info.Name = "richTextBox_Info";
            this.richTextBox_Info.Size = new System.Drawing.Size(508, 374);
            this.richTextBox_Info.TabIndex = 1;
            this.richTextBox_Info.Text = "";
            // 
            // button_PollyRetry
            // 
            this.button_PollyRetry.Location = new System.Drawing.Point(38, 82);
            this.button_PollyRetry.Name = "button_PollyRetry";
            this.button_PollyRetry.Size = new System.Drawing.Size(139, 23);
            this.button_PollyRetry.TabIndex = 2;
            this.button_PollyRetry.Text = "Polly Retry";
            this.button_PollyRetry.UseVisualStyleBackColor = true;
            this.button_PollyRetry.Click += new System.EventHandler(this.button_PollyRetry_Click);
            // 
            // button_AsyncRetry
            // 
            this.button_AsyncRetry.Location = new System.Drawing.Point(38, 210);
            this.button_AsyncRetry.Name = "button_AsyncRetry";
            this.button_AsyncRetry.Size = new System.Drawing.Size(139, 23);
            this.button_AsyncRetry.TabIndex = 3;
            this.button_AsyncRetry.Text = "Polly Async Retry";
            this.button_AsyncRetry.UseVisualStyleBackColor = true;
            this.button_AsyncRetry.Click += new System.EventHandler(this.button_AsyncRetry_Click);
            // 
            // button_pollyRetryWithDelay
            // 
            this.button_pollyRetryWithDelay.Location = new System.Drawing.Point(38, 128);
            this.button_pollyRetryWithDelay.Name = "button_pollyRetryWithDelay";
            this.button_pollyRetryWithDelay.Size = new System.Drawing.Size(139, 23);
            this.button_pollyRetryWithDelay.TabIndex = 4;
            this.button_pollyRetryWithDelay.Tag = "";
            this.button_pollyRetryWithDelay.Text = "Polly Retry With Delay";
            this.button_pollyRetryWithDelay.UseVisualStyleBackColor = true;
            this.button_pollyRetryWithDelay.Click += new System.EventHandler(this.button_pollyRetryWithDelay_Click);
            // 
            // button_asycn_retry
            // 
            this.button_asycn_retry.Location = new System.Drawing.Point(38, 169);
            this.button_asycn_retry.Name = "button_asycn_retry";
            this.button_asycn_retry.Size = new System.Drawing.Size(139, 23);
            this.button_asycn_retry.TabIndex = 5;
            this.button_asycn_retry.Text = "Normal Async Retry";
            this.button_asycn_retry.UseVisualStyleBackColor = true;
            this.button_asycn_retry.Click += new System.EventHandler(this.button_asycn_retry_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_asycn_retry);
            this.Controls.Add(this.button_pollyRetryWithDelay);
            this.Controls.Add(this.button_AsyncRetry);
            this.Controls.Add(this.button_PollyRetry);
            this.Controls.Add(this.richTextBox_Info);
            this.Controls.Add(this.button_normal_retry);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_normal_retry;
        private System.Windows.Forms.RichTextBox richTextBox_Info;
        private System.Windows.Forms.Button button_PollyRetry;
        private System.Windows.Forms.Button button_AsyncRetry;
        private System.Windows.Forms.Button button_pollyRetryWithDelay;
        private System.Windows.Forms.Button button_asycn_retry;
    }
}

