namespace WinFrontEnd
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
            components = new System.ComponentModel.Container();
            pnlSearch = new Panel();
            label10 = new Label();
            cmbMeet = new ComboBox();
            pnlDetails = new Panel();
            lblMeetType = new Label();
            cmbMeetType = new ComboBox();
            lblMeetId = new Label();
            txtInformation = new TextBox();
            label8 = new Label();
            cmbVenue = new ComboBox();
            label7 = new Label();
            txtEntryFee = new TextBox();
            label6 = new Label();
            txtFeePerEvent = new TextBox();
            label5 = new Label();
            dtpRegDeadline = new DateTimePicker();
            label4 = new Label();
            dtpEndDate = new DateTimePicker();
            label3 = new Label();
            dtpStartDate = new DateTimePicker();
            label2 = new Label();
            txtMeetName = new TextBox();
            label1 = new Label();
            btnSave = new Button();
            btnAddNew = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            errorProvider1 = new ErrorProvider(components);
            pnlEvents = new Panel();
            btnAddEvents = new Button();
            dgvMeetEvents = new DataGridView();
            dgvAllEvents = new DataGridView();
            label9 = new Label();
            pnlSearch.SuspendLayout();
            pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            pnlEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMeetEvents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAllEvents).BeginInit();
            SuspendLayout();
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.FromArgb(243, 243, 243);
            pnlSearch.Controls.Add(label10);
            pnlSearch.Controls.Add(cmbMeet);
            pnlSearch.Location = new Point(42, 12);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(452, 81);
            pnlSearch.TabIndex = 4;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = Color.FromArgb(39, 55, 71);
            label10.Location = new Point(18, 12);
            label10.Name = "label10";
            label10.Size = new Size(84, 15);
            label10.TabIndex = 18;
            label10.Text = "Select a Meet";
            // 
            // cmbMeet
            // 
            cmbMeet.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMeet.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbMeet.FormattingEnabled = true;
            cmbMeet.Location = new Point(17, 30);
            cmbMeet.Name = "cmbMeet";
            cmbMeet.Size = new Size(418, 23);
            cmbMeet.TabIndex = 1;
            cmbMeet.SelectionChangeCommitted += cmbMeet_SelectionChangeCommitted;
            // 
            // pnlDetails
            // 
            pnlDetails.BackColor = Color.FromArgb(243, 243, 243);
            pnlDetails.Controls.Add(lblMeetType);
            pnlDetails.Controls.Add(cmbMeetType);
            pnlDetails.Controls.Add(lblMeetId);
            pnlDetails.Controls.Add(txtInformation);
            pnlDetails.Controls.Add(label8);
            pnlDetails.Controls.Add(cmbVenue);
            pnlDetails.Controls.Add(label7);
            pnlDetails.Controls.Add(txtEntryFee);
            pnlDetails.Controls.Add(label6);
            pnlDetails.Controls.Add(txtFeePerEvent);
            pnlDetails.Controls.Add(label5);
            pnlDetails.Controls.Add(dtpRegDeadline);
            pnlDetails.Controls.Add(label4);
            pnlDetails.Controls.Add(dtpEndDate);
            pnlDetails.Controls.Add(label3);
            pnlDetails.Controls.Add(dtpStartDate);
            pnlDetails.Controls.Add(label2);
            pnlDetails.Controls.Add(txtMeetName);
            pnlDetails.Controls.Add(label1);
            pnlDetails.Location = new Point(42, 108);
            pnlDetails.Name = "pnlDetails";
            pnlDetails.Size = new Size(452, 406);
            pnlDetails.TabIndex = 6;
            // 
            // lblMeetType
            // 
            lblMeetType.AutoSize = true;
            lblMeetType.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblMeetType.ForeColor = Color.FromArgb(39, 55, 71);
            lblMeetType.Location = new Point(235, 164);
            lblMeetType.Name = "lblMeetType";
            lblMeetType.Size = new Size(66, 15);
            lblMeetType.TabIndex = 35;
            lblMeetType.Text = "Meet Type";
            // 
            // cmbMeetType
            // 
            cmbMeetType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMeetType.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbMeetType.FormattingEnabled = true;
            cmbMeetType.Location = new Point(235, 182);
            cmbMeetType.Name = "cmbMeetType";
            cmbMeetType.Size = new Size(199, 23);
            cmbMeetType.TabIndex = 34;
            cmbMeetType.Validating += cmbMeetType_Validating;
            cmbMeetType.Validated += cmbMeetType_Validated;
            // 
            // lblMeetId
            // 
            lblMeetId.AutoSize = true;
            lblMeetId.Location = new Point(19, 12);
            lblMeetId.Name = "lblMeetId";
            lblMeetId.Size = new Size(0, 15);
            lblMeetId.TabIndex = 33;
            lblMeetId.Visible = false;
            // 
            // txtInformation
            // 
            txtInformation.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtInformation.Location = new Point(18, 269);
            txtInformation.Multiline = true;
            txtInformation.Name = "txtInformation";
            txtInformation.Size = new Size(417, 104);
            txtInformation.TabIndex = 32;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.FromArgb(39, 55, 71);
            label8.Location = new Point(17, 253);
            label8.Name = "label8";
            label8.Size = new Size(74, 15);
            label8.TabIndex = 31;
            label8.Text = "Information";
            // 
            // cmbVenue
            // 
            cmbVenue.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVenue.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbVenue.FormattingEnabled = true;
            cmbVenue.Location = new Point(20, 94);
            cmbVenue.Name = "cmbVenue";
            cmbVenue.Size = new Size(415, 23);
            cmbVenue.TabIndex = 30;
            cmbVenue.Validating += cmbVenue_Validating;
            cmbVenue.Validated += cmbVenue_Validated;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(39, 55, 71);
            label7.Location = new Point(18, 77);
            label7.Name = "label7";
            label7.Size = new Size(42, 15);
            label7.TabIndex = 29;
            label7.Text = "Venue";
            // 
            // txtEntryFee
            // 
            txtEntryFee.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtEntryFee.Location = new Point(18, 227);
            txtEntryFee.Name = "txtEntryFee";
            txtEntryFee.Size = new Size(200, 23);
            txtEntryFee.TabIndex = 28;
            txtEntryFee.TextAlign = HorizontalAlignment.Right;
            txtEntryFee.Validating += txtEntryFee_Validating;
            txtEntryFee.Validated += txtEntryFee_Validated;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.FromArgb(39, 55, 71);
            label6.Location = new Point(18, 209);
            label6.Name = "label6";
            label6.Size = new Size(59, 15);
            label6.TabIndex = 27;
            label6.Text = "Entry Fee";
            // 
            // txtFeePerEvent
            // 
            txtFeePerEvent.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtFeePerEvent.Location = new Point(235, 227);
            txtFeePerEvent.Name = "txtFeePerEvent";
            txtFeePerEvent.Size = new Size(200, 23);
            txtFeePerEvent.TabIndex = 26;
            txtFeePerEvent.TextAlign = HorizontalAlignment.Right;
            txtFeePerEvent.Validating += txtFeePerEvent_Validating;
            txtFeePerEvent.Validated += txtFeePerEvent_Validated;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.FromArgb(39, 55, 71);
            label5.Location = new Point(235, 209);
            label5.Name = "label5";
            label5.Size = new Size(84, 15);
            label5.TabIndex = 25;
            label5.Text = "Fee per Event";
            // 
            // dtpRegDeadline
            // 
            dtpRegDeadline.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpRegDeadline.Location = new Point(18, 182);
            dtpRegDeadline.Name = "dtpRegDeadline";
            dtpRegDeadline.Size = new Size(200, 23);
            dtpRegDeadline.TabIndex = 24;
            dtpRegDeadline.Validating += dtpRegDeadline_Validating;
            dtpRegDeadline.Validated += dtpRegDeadline_Validated;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(39, 55, 71);
            label4.Location = new Point(18, 164);
            label4.Name = "label4";
            label4.Size = new Size(127, 15);
            label4.TabIndex = 23;
            label4.Text = "Registration Deadline";
            // 
            // dtpEndDate
            // 
            dtpEndDate.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpEndDate.Location = new Point(235, 138);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(200, 23);
            dtpEndDate.TabIndex = 22;
            dtpEndDate.Validating += dtpEndDate_Validating;
            dtpEndDate.Validated += dtpEndDate_Validated;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(39, 55, 71);
            label3.Location = new Point(235, 120);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 21;
            label3.Text = "End Date";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpStartDate.Location = new Point(18, 138);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(200, 23);
            dtpStartDate.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(39, 55, 71);
            label2.Location = new Point(17, 120);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 19;
            label2.Text = "Start Date";
            // 
            // txtMeetName
            // 
            txtMeetName.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtMeetName.Location = new Point(18, 51);
            txtMeetName.Name = "txtMeetName";
            txtMeetName.Size = new Size(417, 23);
            txtMeetName.TabIndex = 18;
            txtMeetName.Validating += txtMeetName_Validating;
            txtMeetName.Validated += txtMeetName_Validated;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(39, 55, 71);
            label1.Location = new Point(18, 33);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 17;
            label1.Text = "Name";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(147, 36, 52);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.ForeColor = Color.FromArgb(243, 243, 243);
            btnSave.Location = new Point(419, 520);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 30);
            btnSave.TabIndex = 7;
            btnSave.Text = "SAVE";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnAddNew
            // 
            btnAddNew.BackColor = Color.FromArgb(147, 36, 52);
            btnAddNew.FlatStyle = FlatStyle.Flat;
            btnAddNew.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddNew.ForeColor = Color.FromArgb(243, 243, 243);
            btnAddNew.Location = new Point(42, 520);
            btnAddNew.Name = "btnAddNew";
            btnAddNew.Size = new Size(91, 30);
            btnAddNew.TabIndex = 8;
            btnAddNew.Text = "ADD NEW";
            btnAddNew.UseVisualStyleBackColor = false;
            btnAddNew.Click += btnAddNew_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(147, 36, 52);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnDelete.ForeColor = Color.FromArgb(243, 243, 243);
            btnDelete.Location = new Point(139, 520);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(91, 30);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(147, 36, 52);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnClear.ForeColor = Color.FromArgb(243, 243, 243);
            btnClear.Location = new Point(338, 520);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 30);
            btnClear.TabIndex = 10;
            btnClear.Text = "CLEAR";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // pnlEvents
            // 
            pnlEvents.BackColor = Color.FromArgb(243, 243, 243);
            pnlEvents.Controls.Add(btnAddEvents);
            pnlEvents.Controls.Add(dgvMeetEvents);
            pnlEvents.Controls.Add(dgvAllEvents);
            pnlEvents.Controls.Add(label9);
            pnlEvents.Location = new Point(522, 12);
            pnlEvents.Name = "pnlEvents";
            pnlEvents.Size = new Size(850, 502);
            pnlEvents.TabIndex = 19;
            // 
            // btnAddEvents
            // 
            btnAddEvents.BackColor = Color.FromArgb(147, 36, 52);
            btnAddEvents.FlatStyle = FlatStyle.Flat;
            btnAddEvents.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddEvents.ForeColor = Color.FromArgb(243, 243, 243);
            btnAddEvents.Location = new Point(21, 201);
            btnAddEvents.Name = "btnAddEvents";
            btnAddEvents.Size = new Size(161, 30);
            btnAddEvents.TabIndex = 20;
            btnAddEvents.Text = "ADD EVENTS";
            btnAddEvents.UseVisualStyleBackColor = false;
            btnAddEvents.Click += btnAddEvents_Click;
            // 
            // dgvMeetEvents
            // 
            dgvMeetEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMeetEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMeetEvents.Location = new Point(21, 278);
            dgvMeetEvents.Name = "dgvMeetEvents";
            dgvMeetEvents.RowHeadersWidth = 51;
            dgvMeetEvents.RowTemplate.Height = 25;
            dgvMeetEvents.Size = new Size(786, 150);
            dgvMeetEvents.TabIndex = 20;
            // 
            // dgvAllEvents
            // 
            dgvAllEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAllEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAllEvents.Location = new Point(21, 37);
            dgvAllEvents.Name = "dgvAllEvents";
            dgvAllEvents.RowHeadersWidth = 51;
            dgvAllEvents.RowTemplate.Height = 25;
            dgvAllEvents.Size = new Size(786, 150);
            dgvAllEvents.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = Color.FromArgb(39, 55, 71);
            label9.Location = new Point(18, 12);
            label9.Name = "label9";
            label9.Size = new Size(104, 15);
            label9.TabIndex = 18;
            label9.Text = "Select the Events";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            BackColor = Color.FromArgb(223, 233, 245);
            ClientSize = new Size(1382, 582);
            Controls.Add(pnlEvents);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnAddNew);
            Controls.Add(btnSave);
            Controls.Add(pnlDetails);
            Controls.Add(pnlSearch);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Meet Maintenance";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlDetails.ResumeLayout(false);
            pnlDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            pnlEvents.ResumeLayout(false);
            pnlEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMeetEvents).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAllEvents).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSearch;
        private Label label10;
        private ComboBox cmbMeet;
        private Panel pnlDetails;
        private TextBox txtInformation;
        private Label label8;
        private ComboBox cmbVenue;
        private Label label7;
        private TextBox txtEntryFee;
        private Label label6;
        private TextBox txtFeePerEvent;
        private Label label5;
        private DateTimePicker dtpRegDeadline;
        private Label label4;
        private DateTimePicker dtpEndDate;
        private Label label3;
        private DateTimePicker dtpStartDate;
        private Label label2;
        private TextBox txtMeetName;
        private Label label1;
        private Button btnSave;
        private Button btnAddNew;
        private Button btnDelete;
        private Button btnClear;
        private Label lblMeetId;
        private ErrorProvider errorProvider1;
        private Panel pnlEvents;
        private DataGridView dgvMeetEvents;
        private DataGridView dgvAllEvents;
        private Label label9;
        private Button btnAddEvents;
        private Label lblMeetType;
        private ComboBox cmbMeetType;
    }
}