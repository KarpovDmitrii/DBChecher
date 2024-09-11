namespace DBChecher
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
            button1 = new Button();
            textBoxServer = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBoxPort = new TextBox();
            textBoxUserId = new TextBox();
            textBoxPassword = new TextBox();
            textBoxDB = new TextBox();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            textBox2 = new TextBox();
            label6 = new Label();
            button2 = new Button();
            textBox4 = new TextBox();
            button3 = new Button();
            textBoxState = new TextBox();
            button4 = new Button();
            comboBoxSelectDB = new ComboBox();
            buttonRecordsAnalysis = new Button();
            dataGridViewRecords = new DataGridView();
            textBoxRecords = new TextBox();
            textBoxTimeOut = new TextBox();
            label7 = new Label();
            buttonSaveToPDF = new Button();
            buttonAddRandomRows = new Button();
            textBoxAddRowsCount = new TextBox();
            textBoxAddRowsSeconds = new TextBox();
            textBoxAddRowsStatus = new TextBox();
            label8 = new Label();
            label9 = new Label();
            buttonAddRandomRowsMeasures = new Button();
            dateTimePickerStart = new DateTimePicker();
            dateTimePickerEnd = new DateTimePicker();
            dateTimePickerStartRecords = new DateTimePicker();
            dateTimePickerEndRecords = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRecords).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(394, 180);
            button1.Name = "button1";
            button1.Size = new Size(201, 36);
            button1.TabIndex = 0;
            button1.Text = "Поиск";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBoxServer
            // 
            textBoxServer.Location = new Point(123, 30);
            textBoxServer.Name = "textBoxServer";
            textBoxServer.Size = new Size(234, 23);
            textBoxServer.TabIndex = 2;
            textBoxServer.Text = "opo-postgresql.zav.mir";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 37);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 3;
            label1.Text = "Сервер:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 77);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 4;
            label2.Text = "Порт:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 118);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 5;
            label3.Text = "Пользователь:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 155);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 6;
            label4.Text = "Пароль:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 197);
            label5.Name = "label5";
            label5.Size = new Size(78, 15);
            label5.TabIndex = 7;
            label5.Text = "База данных:";
            // 
            // textBoxPort
            // 
            textBoxPort.Location = new Point(123, 70);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(234, 23);
            textBoxPort.TabIndex = 8;
            textBoxPort.Text = "5432";
            // 
            // textBoxUserId
            // 
            textBoxUserId.Location = new Point(123, 114);
            textBoxUserId.Name = "textBoxUserId";
            textBoxUserId.Size = new Size(234, 23);
            textBoxUserId.TabIndex = 9;
            textBoxUserId.Text = "postgres";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(123, 152);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(234, 23);
            textBoxPassword.TabIndex = 10;
            textBoxPassword.Text = "mirpass";
            // 
            // textBoxDB
            // 
            textBoxDB.Location = new Point(123, 193);
            textBoxDB.Name = "textBoxDB";
            textBoxDB.Size = new Size(234, 23);
            textBoxDB.TabIndex = 11;
            textBoxDB.Text = "utek_lnx";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 295);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(567, 297);
            dataGridView1.TabIndex = 12;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(600, 295);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(567, 297);
            dataGridView2.TabIndex = 13;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(123, 258);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 16;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(28, 261);
            label6.Name = "label6";
            label6.Size = new Size(91, 15);
            label6.TabIndex = 17;
            label6.Text = "Общий размер";
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(680, 129);
            button2.Name = "button2";
            button2.Size = new Size(224, 119);
            button2.TabIndex = 19;
            button2.Text = "Анализ measures";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(680, 256);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(224, 23);
            textBox4.TabIndex = 21;
            // 
            // button3
            // 
            button3.Location = new Point(394, 19);
            button3.Name = "button3";
            button3.Size = new Size(201, 50);
            button3.TabIndex = 22;
            button3.Text = "Добавить подключение";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBoxState
            // 
            textBoxState.Location = new Point(394, 147);
            textBoxState.Name = "textBoxState";
            textBoxState.ReadOnly = true;
            textBoxState.Size = new Size(202, 23);
            textBoxState.TabIndex = 25;
            // 
            // button4
            // 
            button4.Enabled = false;
            button4.Location = new Point(394, 247);
            button4.Name = "button4";
            button4.Size = new Size(201, 34);
            button4.TabIndex = 27;
            button4.Text = "Сохранить данные";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // comboBoxSelectDB
            // 
            comboBoxSelectDB.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSelectDB.FormattingEnabled = true;
            comboBoxSelectDB.Location = new Point(394, 86);
            comboBoxSelectDB.Name = "comboBoxSelectDB";
            comboBoxSelectDB.Size = new Size(201, 23);
            comboBoxSelectDB.TabIndex = 28;
            comboBoxSelectDB.SelectedIndexChanged += comboBoxSelectDB_SelectedIndexChanged;
            // 
            // buttonRecordsAnalysis
            // 
            buttonRecordsAnalysis.Enabled = false;
            buttonRecordsAnalysis.Location = new Point(1184, 129);
            buttonRecordsAnalysis.Name = "buttonRecordsAnalysis";
            buttonRecordsAnalysis.RightToLeft = RightToLeft.No;
            buttonRecordsAnalysis.Size = new Size(224, 119);
            buttonRecordsAnalysis.TabIndex = 29;
            buttonRecordsAnalysis.Text = "Анализ records";
            buttonRecordsAnalysis.UseVisualStyleBackColor = true;
            buttonRecordsAnalysis.Click += buttonRecordsAnalysis_Click;
            // 
            // dataGridViewRecords
            // 
            dataGridViewRecords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRecords.Location = new Point(1184, 295);
            dataGridViewRecords.Name = "dataGridViewRecords";
            dataGridViewRecords.Size = new Size(567, 297);
            dataGridViewRecords.TabIndex = 30;
            dataGridViewRecords.CellContentClick += dataGridView3_CellContentClick;
            // 
            // textBoxRecords
            // 
            textBoxRecords.Location = new Point(1184, 259);
            textBoxRecords.Name = "textBoxRecords";
            textBoxRecords.ReadOnly = true;
            textBoxRecords.Size = new Size(224, 23);
            textBoxRecords.TabIndex = 31;
            // 
            // textBoxTimeOut
            // 
            textBoxTimeOut.Location = new Point(121, 603);
            textBoxTimeOut.Name = "textBoxTimeOut";
            textBoxTimeOut.Size = new Size(100, 23);
            textBoxTimeOut.TabIndex = 32;
            textBoxTimeOut.Text = "1800";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(58, 606);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 33;
            label7.Text = "Тайм-аут";
            // 
            // buttonSaveToPDF
            // 
            buttonSaveToPDF.Enabled = false;
            buttonSaveToPDF.Location = new Point(1544, 145);
            buttonSaveToPDF.Name = "buttonSaveToPDF";
            buttonSaveToPDF.Size = new Size(115, 107);
            buttonSaveToPDF.TabIndex = 34;
            buttonSaveToPDF.Text = "Сохранить в PDF";
            buttonSaveToPDF.UseVisualStyleBackColor = true;
            buttonSaveToPDF.Click += buttonSaveToPDF_Click;
            // 
            // buttonAddRandomRows
            // 
            buttonAddRandomRows.Enabled = false;
            buttonAddRandomRows.Location = new Point(1184, 19);
            buttonAddRandomRows.Name = "buttonAddRandomRows";
            buttonAddRandomRows.Size = new Size(109, 68);
            buttonAddRandomRows.TabIndex = 35;
            buttonAddRandomRows.Text = "Добавить в Records";
            buttonAddRandomRows.UseVisualStyleBackColor = true;
            buttonAddRandomRows.Click += buttonAddRandomRows_Click;
            // 
            // textBoxAddRowsCount
            // 
            textBoxAddRowsCount.Location = new Point(887, 19);
            textBoxAddRowsCount.Name = "textBoxAddRowsCount";
            textBoxAddRowsCount.Size = new Size(144, 23);
            textBoxAddRowsCount.TabIndex = 36;
            textBoxAddRowsCount.Text = "10";
            // 
            // textBoxAddRowsSeconds
            // 
            textBoxAddRowsSeconds.Location = new Point(887, 64);
            textBoxAddRowsSeconds.Name = "textBoxAddRowsSeconds";
            textBoxAddRowsSeconds.Size = new Size(144, 23);
            textBoxAddRowsSeconds.TabIndex = 37;
            textBoxAddRowsSeconds.Text = "2";
            // 
            // textBoxAddRowsStatus
            // 
            textBoxAddRowsStatus.Location = new Point(948, 115);
            textBoxAddRowsStatus.Multiline = true;
            textBoxAddRowsStatus.Name = "textBoxAddRowsStatus";
            textBoxAddRowsStatus.Size = new Size(185, 164);
            textBoxAddRowsStatus.TabIndex = 38;
            textBoxAddRowsStatus.TextChanged += textBoxAddRowsStatus_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(755, 22);
            label8.Name = "label8";
            label8.Size = new Size(113, 15);
            label8.TabIndex = 39;
            label8.Text = "Колличество строк";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(755, 67);
            label9.Name = "label9";
            label9.Size = new Size(109, 15);
            label9.TabIndex = 40;
            label9.Text = "Секунд / итераций";
            // 
            // buttonAddRandomRowsMeasures
            // 
            buttonAddRandomRowsMeasures.Enabled = false;
            buttonAddRandomRowsMeasures.Location = new Point(1048, 19);
            buttonAddRandomRowsMeasures.Name = "buttonAddRandomRowsMeasures";
            buttonAddRandomRowsMeasures.Size = new Size(119, 68);
            buttonAddRandomRowsMeasures.TabIndex = 41;
            buttonAddRandomRowsMeasures.Text = "Добавить в Measures";
            buttonAddRandomRowsMeasures.UseVisualStyleBackColor = true;
            buttonAddRandomRowsMeasures.Click += buttonAddRandomRowsMeasures_Click;
            // 
            // dateTimePickerStart
            // 
            dateTimePickerStart.Location = new Point(660, 100);
            dateTimePickerStart.Name = "dateTimePickerStart";
            dateTimePickerStart.Size = new Size(127, 23);
            dateTimePickerStart.TabIndex = 42;
            dateTimePickerStart.Value = new DateTime(2000, 9, 10, 15, 7, 0, 0);
            // 
            // dateTimePickerEnd
            // 
            dateTimePickerEnd.Location = new Point(803, 100);
            dateTimePickerEnd.Name = "dateTimePickerEnd";
            dateTimePickerEnd.Size = new Size(127, 23);
            dateTimePickerEnd.TabIndex = 43;
            // 
            // dateTimePickerStartRecords
            // 
            dateTimePickerStartRecords.Location = new Point(1168, 100);
            dateTimePickerStartRecords.Name = "dateTimePickerStartRecords";
            dateTimePickerStartRecords.Size = new Size(125, 23);
            dateTimePickerStartRecords.TabIndex = 44;
            dateTimePickerStartRecords.Value = new DateTime(2000, 9, 10, 15, 7, 0, 0);
            // 
            // dateTimePickerEndRecords
            // 
            dateTimePickerEndRecords.Location = new Point(1311, 100);
            dateTimePickerEndRecords.Name = "dateTimePickerEndRecords";
            dateTimePickerEndRecords.Size = new Size(124, 23);
            dateTimePickerEndRecords.TabIndex = 45;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1797, 645);
            Controls.Add(dateTimePickerEndRecords);
            Controls.Add(dateTimePickerStartRecords);
            Controls.Add(dateTimePickerEnd);
            Controls.Add(dateTimePickerStart);
            Controls.Add(buttonAddRandomRowsMeasures);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(textBoxAddRowsStatus);
            Controls.Add(textBoxAddRowsSeconds);
            Controls.Add(textBoxAddRowsCount);
            Controls.Add(buttonAddRandomRows);
            Controls.Add(buttonSaveToPDF);
            Controls.Add(label7);
            Controls.Add(textBoxTimeOut);
            Controls.Add(textBoxRecords);
            Controls.Add(dataGridViewRecords);
            Controls.Add(buttonRecordsAnalysis);
            Controls.Add(comboBoxSelectDB);
            Controls.Add(button4);
            Controls.Add(textBoxState);
            Controls.Add(button3);
            Controls.Add(textBox4);
            Controls.Add(button2);
            Controls.Add(label6);
            Controls.Add(textBox2);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(textBoxDB);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxUserId);
            Controls.Add(textBoxPort);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxServer);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRecords).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBoxServer;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBoxPort;
        private TextBox textBoxUserId;
        private TextBox textBoxPassword;
        private TextBox textBoxDB;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private TextBox textBox2;
        private Label label6;
        private Button button2;
        private TextBox textBox4;
        private Button button3;
        private TextBox textBoxState;
        private Button button4;
        private ComboBox comboBoxSelectDB;
        private Button buttonRecordsAnalysis;
        private DataGridView dataGridViewRecords;
        private TextBox textBoxRecords;
        private TextBox textBoxTimeOut;
        private Label label7;
        private Button buttonSaveToPDF;
        private Button buttonAddRandomRows;
        private TextBox textBoxAddRowsCount;
        private TextBox textBoxAddRowsSeconds;
        private TextBox textBoxAddRowsStatus;
        private Label label8;
        private Label label9;
        private Button buttonAddRandomRowsMeasures;
        private DateTimePicker dateTimePickerStart;
        private DateTimePicker dateTimePickerEnd;
        private DateTimePicker dateTimePickerStartRecords;
        private DateTimePicker dateTimePickerEndRecords;
    }
}
