using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Serialization;
using Npgsql.Internal;
using System.Collections;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System;
using static System.Windows.Forms.LinkLabel;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Runtime.InteropServices;
using System.Text.Unicode;
using System.Text;
using System.Text;
using Aspose.Pdf;
using System.Threading.Channels;
using System.Diagnostics;
using Aspose.Pdf.Operators;


namespace DBChecher
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            try
            {
                DeserializedConInfo();
            }
            catch (Exception ex)
            when (ex is System.InvalidOperationException || ex is System.IO.FileNotFoundException)
            {
                MessageBox.Show("Не получилось загрузить данные из локального хранилища");
            }

            DeserializedEventConsts();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Form1_Closing);
        }

        private Dictionary<string, ConnectionInfo> _connections = new Dictionary<string, ConnectionInfo>();
        public bool button2PumpFlag = false;
        public bool isChangesSaved = true;
        Thread threadForButtonExecute;
        Thread threadForButtonRecords;
        CancellationTokenSource tokenSource = new();
        private string CurrentConnectionInfoID = "";
        private Dictionary<int, string> eventConsts;


        private void button3_Click(object sender, EventArgs e)
        {
            isChangesSaved = false;
            bool isUnique = true;
            ConnectionString con = new ConnectionString(
                textBoxServer.Text,
                textBoxPort.Text,
                textBoxUserId.Text,
                textBoxPassword.Text,
                textBoxDB.Text);

            foreach (string connection in _connections.Keys)
            {
                var connectionInfo = _connections[connection];
                if (con.GetConnectionString() == connectionInfo.Manager.connectionString.GetConnectionString())
                {
                    textBoxState.Text = "Подключение уже существует";
                    isUnique = false;
                }
            }

            if (isUnique)
            {
                //check if DB are exist

                ConnectionMenager connectionMenager = new ConnectionMenager(con);
                bool isDbExist = true;
                try
                {
                    connectionMenager.OpenConnection();
                    connectionMenager.CloseConnection();
                }
                catch (Npgsql.PostgresException ex)
                {
                    textBoxState.Text = "Невозможно установить подключение";
                    isDbExist = false;
                }

                if (isDbExist)
                {
                    _connections.Add(con.DataBase, new ConnectionInfo(
                        new ConnectionMenager(con),
                        new DataTable { TableName = con.DataBase + ".AllTables" },
                        new DataTable { TableName = con.DataBase + ".MeasuresTable" },
                        new DataTable { TableName = con.DataBase + ".RecordsTable" },
                        " "));

                    textBoxState.Text = "Подключение добавлено";
                    comboBoxSelectDB.Items.Add(con.DataBase);
                    comboBoxSelectDB.SelectedItem = con.DataBase;
                    CurrentConnectionInfoID = con.DataBase;

                    buttonRecordsAnalysis.Enabled = false;
                    button2.Enabled = false;
                }
            }
        }

        private void WriteConnectionInfo(ConnectionInfo info)
        {
            textBox2.Text = info.FullDBSize;
            dataGridView1.DataSource = info.AllTables;
            dataGridView2.DataSource = info.MeasuresTable;
            dataGridViewRecords.DataSource = info.RecordsTable;
            textBoxServer.Text = info.Manager.connectionString.Server;
            textBoxPort.Text = info.Manager.connectionString.Port;
            textBoxUserId.Text = info.Manager.connectionString.UserID;
            textBoxPassword.Text = info.Manager.connectionString.Password;
            textBoxDB.Text = info.Manager.connectionString.DataBase;
            textBoxState.Text = "";
            textBox4.Text = " ";
            textBoxRecords.Text = " ";
        }


        private void button1_logic()
        {
            Invoke((MethodInvoker)delegate
            {
                textBoxState.Text = "Поиск начат";

                isChangesSaved = false;
                button3.Enabled = false;
                button2.Enabled = false;
                buttonRecordsAnalysis.Enabled = false;
                comboBoxSelectDB.Enabled = false;
                button4.Enabled = false;

            });

            _connections[CurrentConnectionInfoID].Manager.OpenConnection();

            var sqlCmdList = "SELECT nspname AS Схема,\r\n" +
                "relname AS \"Таблица\",\r\n" +
                "pg_size_pretty (pg_total_relation_size (C .oid))\r\n" +
                "AS \"Размер\",\r\n" +
                "ROUND((pg_total_relation_size (C .oid)::numeric  / pg_database_size(current_database())::numeric) *100, 1)  \r\n" +
                "AS \"Относительный размер\",\r\n" +
                "reltuples  AS \"Активные строки\" \r\n" +
                "FROM pg_class C\r\n" +
                "LEFT JOIN pg_namespace N ON (N.oid = C .relnamespace)\r\n" +
                "WHERE nspname NOT IN ('pg_catalog','information_schema')\r\n" +
                "AND C .relkind <> 'i' AND nspname !~ '^pg_toast'\r\n" +
                "ORDER BY pg_total_relation_size (C .oid) DESC\r\n" +
                "limit 10";

            var sqlCmdFullSize = "SELECT pg_size_pretty(pg_database_size(current_database())) AS \"fullSize\";";
            int timeOut;
            try
            {
                timeOut = Convert.ToInt32(textBoxTimeOut.Text);
            }
            catch (FormatException ex)
            {
                timeOut = 1800;
            }
            var allTables = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdList, timeOut);
            var FullSizeInfo = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdFullSize, timeOut);


            var fullDBSize = FullSizeInfo.Rows[0]["fullSize"];

            allTables.TableName = _connections[CurrentConnectionInfoID].Manager.connectionString.DataBase + ".allTables";

            Invoke((MethodInvoker)delegate
            {
                textBox2.Text = fullDBSize.ToString();
                _connections[CurrentConnectionInfoID].FullDBSize = fullDBSize.ToString();

                dataGridView1.DataSource = allTables;

                _connections[CurrentConnectionInfoID].AllTables = allTables;

                textBoxState.Text = "Поиск окончен";

                button3.Enabled = true;
                button2.Enabled = true;
                buttonRecordsAnalysis.Enabled = true;
                comboBoxSelectDB.Enabled = true;
                button4.Enabled = true;

            });

            _connections[CurrentConnectionInfoID].Manager.CloseConnection();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            threadForButtonExecute = new Thread(() => button1_logic());
            threadForButtonExecute.Start();
        }


        private void SearchInMeasure_logic(CancellationToken token, string startDate, string endDate)
        {

            Invoke((MethodInvoker)delegate
            {
                isChangesSaved = false;
                button2PumpFlag = true;
                button3.Enabled = false;
                button1.Enabled = false;
                comboBoxSelectDB.Enabled = false;
                button4.Enabled = false;
            });

            var isInDBFlag = false;
            foreach (DataRow row in _connections[CurrentConnectionInfoID].AllTables.Rows)
            {
                if (row["Таблица"].ToString() == "Measures")
                {
                    isInDBFlag = true;
                }
            }

            if (isInDBFlag)
            {
                _connections[CurrentConnectionInfoID].Manager.OpenConnection();

                var sqlCmdValueType = "SELECT DISTINCT \"MeasureChannels\".\"ValueType\" FROM \"em_measures\".\"MeasureChannels\";";
                var sqlCmdIntervalType = "SELECT DISTINCT \"MeasureChannels\".\"IntervalType\" FROM \"em_measures\".\"MeasureChannels\";";
                var sqlCmdType = "SELECT DISTINCT \"MeasureChannels\".\"Type\" FROM \"em_measures\".\"MeasureChannels\";";

                int timeOut = Convert.ToInt32(textBoxTimeOut.Text);

                var ValueTypeTable = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdValueType, timeOut);
                var IntervalTypeTable = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdIntervalType, timeOut);
                var TypeTable = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdType, timeOut);

                var list = new Dictionary<(int, int, int), int>();

                if (ValueTypeTable.Rows.Count > 0 && IntervalTypeTable.Rows.Count > 0 && TypeTable.Rows.Count > 0)
                {
                    foreach (DataRow type in TypeTable.Rows)
                    {
                        int typeInt = Convert.ToInt32(type["Type"]);
                        foreach (DataRow interval in IntervalTypeTable.Rows)
                        {
                            int intervalInt = Convert.ToInt32(interval["IntervalType"]);
                            foreach (DataRow value in ValueTypeTable.Rows)
                            {
                                int valueInt = Convert.ToInt32(value["ValueType"]);

                                var sqlCmdGetIDsForKey = "SELECT \"Id\"\r\n\t" +
                                    "FROM em_measures.\"MeasureChannels\"\r\n\t" +
                                    "WHERE \"MeasureChannels\".\"Type\"=" + typeInt + "\r\n\t" +
                                    "AND \"MeasureChannels\".\"IntervalType\" = " + intervalInt + " \r\n\t" +
                                    "AND \"MeasureChannels\".\"ValueType\"=" + valueInt + ";";
                                var IDsForKey = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdGetIDsForKey, timeOut);
                                var countForKey = 0;
                                foreach (DataRow idRow in IDsForKey.Rows)
                                {
                                    var Id = Convert.ToInt32(idRow["Id"]);

                                    var sqlCmdCountForId = "SELECT \"MeasureChannelId\", COUNT(*) as sss\r\n\t" +
                                        "FROM em_measures.\"Measures\"\r\n\t" +
                                        "WHERE \"Measures\".\"MeasureChannelId\" = " + Id + "\r\n\t" +
                                        "AND \"Measures\".\"TimeBegin\" > date '"+ startDate + "'\r\n\t" +
                                        "AND \"Measures\".\"TimeEnd\" < date '"+ endDate + "' + interval '24 hour'\r\n\t" +
                                        "GROUP BY \"Measures\".\"MeasureChannelId\";";

                                    var countOfCurrentId = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdCountForId, timeOut);
                                    if (countOfCurrentId.Rows.Count > 0)
                                    {
                                        countForKey += Convert.ToInt32(countOfCurrentId.Rows[0]["sss"].ToString());
                                    }
                                }

                                list.Add((typeInt, intervalInt, valueInt), countForKey);
                            }
                        }
                    }
                    var rowsCountSqlRequest = "SELECT COUNT(*)\r\n\tFROM em_measures.\"Measures\"";
                    var rowCount = Convert.ToDouble(_connections[CurrentConnectionInfoID].Manager.sendRequest(rowsCountSqlRequest, timeOut).Rows[0]["count"].ToString());


                    var mostCommonValsCount = new DataTable();


                    var columnMeasure = new DataColumn();
                    columnMeasure.DataType = typeof(string);
                    columnMeasure.ColumnName = "Имя типа";
                    mostCommonValsCount.Columns.Add(columnMeasure);

                    var columnInterval = new DataColumn();
                    columnInterval.DataType = typeof(string);
                    columnInterval.ColumnName = "Имя типа интервала";
                    mostCommonValsCount.Columns.Add(columnInterval);

                    var columnValue = new DataColumn();
                    columnValue.DataType = typeof(string);
                    columnValue.ColumnName = "Имя типа значения";
                    mostCommonValsCount.Columns.Add(columnValue);

                    var columnCount = new DataColumn();
                    columnCount.DataType = typeof(int);
                    columnCount.ColumnName = "Количество вхождений";
                    mostCommonValsCount.Columns.Add(columnCount);

                    var columnPercentCount = new DataColumn();
                    columnPercentCount.DataType = typeof(double);
                    columnPercentCount.ColumnName = "Относительный размер";
                    mostCommonValsCount.Columns.Add(columnPercentCount);

                    list = list.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

                    foreach ((int, int, int) key in list.Keys.Reverse())
                    {
                        var row = mostCommonValsCount.NewRow();
                        var infoForID = SearchMeasureNamesForId(key.Item1, key.Item2, key.Item3);
                        row["Количество вхождений"] = Convert.ToString(list[key]);
                        row["Относительный размер"] = Math.Round(Convert.ToDouble(list[key]) * 100 / rowCount, 2);

                        row["Имя типа"] = infoForID.Item1;
                        row["Имя типа интервала"] = infoForID.Item2;
                        row["Имя типа значения"] = infoForID.Item3;

                        mostCommonValsCount.Rows.Add(row);
                    }

                    mostCommonValsCount.TableName = _connections[CurrentConnectionInfoID].Manager.connectionString.DataBase + ".MeasuresTable";

                    Invoke((MethodInvoker)delegate
                    {

                        _connections[CurrentConnectionInfoID].MeasuresTable = mostCommonValsCount;

                        dataGridView2.DataSource = mostCommonValsCount;
                        textBox4.Text = "Анализ остановлен";
                        SerializedConInfo();

                    });
                    _connections[CurrentConnectionInfoID].Manager.CloseConnection();
                }
                else
                {
                    Invoke((MethodInvoker)delegate
                    {
                        textBox4.Text = "Таблица пустая";
                    });
                }
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    textBox4.Text = "Неверный тип бд";
                });
            }
            Invoke((MethodInvoker)delegate
            {
                button2PumpFlag = false;
                button3.Enabled = true;
                button1.Enabled = true;
                comboBoxSelectDB.Enabled = true;
                button4.Enabled = true;

                _connections[CurrentConnectionInfoID].Manager.CloseConnection();
            });
        }
        private void SearchInRecords_logic(CancellationToken token, string eventDate, string insertDate)
        {
            Invoke((MethodInvoker)delegate
            {
                isChangesSaved = false;
                button2PumpFlag = true;
                button3.Enabled = false;
                button1.Enabled = false;
                comboBoxSelectDB.Enabled = false;
                button4.Enabled = false;
            });

            var isInDBFlag = false;
            foreach (DataRow row in _connections[CurrentConnectionInfoID].AllTables.Rows)
            {
                if (row["Таблица"].ToString() == "Records")
                {
                    isInDBFlag = true;
                }
            }

            if (isInDBFlag)
            {
                _connections[CurrentConnectionInfoID].Manager.OpenConnection();

                var sqlCmdGroupBy = "SELECT \"EventCode\", COUNT(*) as count\r\n\t" +
                    "FROM em_protocol.\"Records\"\r\n\t" +
                    "INNER JOIN em_protocol.\"Channels\"\r\n\t\t\t" +
                    "ON \"Records\".\"ChannelId\" = \"Channels\".\"Id\"\r\n\t" +
                    "WHERE \"Records\".\"EventTime\" > date '" + eventDate + "'\r\n\t" +
                                        "AND \"Records\".\"InsertTime\" < date '" + insertDate + "' + interval '24 hour'\r\n\t" +
                    "GROUP BY \"Channels\".\"EventCode\";";

                var rowsCountSqlRequest = "SELECT COUNT(*) as count\r\n\tFROM em_protocol.\"Records\"";

                int timeOut = Convert.ToInt32(textBoxTimeOut.Text);
                var rowCount = Convert.ToDouble(_connections[CurrentConnectionInfoID].Manager.sendRequest(rowsCountSqlRequest, timeOut).Rows[0]["count"].ToString());

                var mostCommonVals = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdGroupBy, timeOut);

                if (mostCommonVals.Rows.Count > 0)
                {

                    var list = new Dictionary<int, int>();
                    foreach (DataRow row in mostCommonVals.Rows)
                    {

                        list.Add(Convert.ToInt32(row["EventCode"].ToString()),
                                Convert.ToInt32(row["count"].ToString()));
                    }

                    var mostCommonValsCount = new DataTable();

                    var columnKey = new DataColumn();
                    columnKey.DataType = typeof(int);
                    columnKey.ColumnName = "EventCode";
                    mostCommonValsCount.Columns.Add(columnKey);

                    var columnValue = new DataColumn();
                    columnValue.DataType = typeof(int);
                    columnValue.ColumnName = "Количество вхождений";
                    mostCommonValsCount.Columns.Add(columnValue);

                    var columnPercentVolume = new DataColumn();
                    columnPercentVolume.DataType = typeof(double);
                    columnPercentVolume.ColumnName = "Относительный размер";
                    mostCommonValsCount.Columns.Add(columnPercentVolume);

                    var columnDescription = new DataColumn();
                    columnDescription.DataType = typeof(string);
                    columnDescription.ColumnName = "Описание";
                    mostCommonValsCount.Columns.Add(columnDescription);


                    list = list.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

                    foreach (int key in list.Keys.Reverse())
                    {
                        var row = mostCommonValsCount.NewRow();
                        row["Количество вхождений"] = Convert.ToString(list[key]);
                        row["EventCode"] = Convert.ToString(key);
                        row["Относительный размер"] = Math.Round(Convert.ToDouble(list[key]) * 100.0 / rowCount, 1);
                        if (eventConsts.Keys.Contains(key))
                        {
                            row["Описание"] = eventConsts[key];

                        }
                        else
                        {
                            row["Описание"] = "---";

                        }

                        mostCommonValsCount.Rows.Add(row);
                    }

                    mostCommonValsCount.TableName = _connections[CurrentConnectionInfoID].Manager.connectionString.DataBase + ".RecordsTable";

                    Invoke((MethodInvoker)delegate
                    {

                        _connections[CurrentConnectionInfoID].RecordsTable = mostCommonValsCount;
                        dataGridViewRecords.DataSource = mostCommonValsCount;
                        textBoxRecords.Text = "Анализ остановлен";
                    });
                    _connections[CurrentConnectionInfoID].Manager.CloseConnection();
                }
                else
                {
                    Invoke((MethodInvoker)delegate
                    {
                        textBoxRecords.Text = "Таблица пустая";
                    });
                }
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    textBoxRecords.Text = "Неверный тип бд";
                });
            }
            Invoke((MethodInvoker)delegate
            {
                button2PumpFlag = false;
                button3.Enabled = true;
                button1.Enabled = true;
                comboBoxSelectDB.Enabled = true;
                button4.Enabled = true;

                _connections[CurrentConnectionInfoID].Manager.CloseConnection();
            });
        }

        private (string, string, string) SearchMeasureNamesForId(int type, int intervalType, int valueType)
        {
            var connection = _connections[CurrentConnectionInfoID].Manager;

            int timeOut = Convert.ToInt32(textBoxTimeOut.Text);

            var requestMeasureType = "SELECT \"Name\"\r\n\t" +
                "FROM em_measures.\"DicMeasureTypes\"\r\n\t" +
                "WHERE \"DicMeasureTypes\".\"Id\" = " + type + ";";

            var requestIntervalType = "SELECT \"Name\"\r\n\t" +
                "FROM em_measures.\"DicIntervalTypes\"\r\n\t" +
                "WHERE \"DicIntervalTypes\".\"Id\" = " + intervalType + ";";

            var requestValueType = "SELECT \"Name\"\r\n\t" +
                "FROM em_measures.\"DicValueTypes\"\r\n\t" +
                "WHERE \"DicValueTypes\".\"Id\" = " + valueType + ";";


            var measure = connection.sendRequest(requestMeasureType, timeOut);
            var measureString = "---";
            if (measure.Rows.Count > 0)
            {
                measureString = measure.Rows[0]["Name"].ToString();
            }
            var interval = connection.sendRequest(requestIntervalType, timeOut);
            var intervalString = "---";
            if (interval.Rows.Count > 0)
            {
                intervalString = interval.Rows[0]["Name"].ToString();
            }
            var value = connection.sendRequest(requestValueType, timeOut);
            var valueString = "---";
            if (value.Rows.Count > 0)
            {
                valueString = value.Rows[0]["Name"].ToString();
            }

            return (measureString, intervalString, valueString);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (button2PumpFlag == false)
            {
                tokenSource = new CancellationTokenSource();
                textBox4.Text = "Анализ запущен";
                var startDate = dateTimePickerStart.Value.Date.ToString();
                var endDate = dateTimePickerEnd.Value.Date.ToString();
                threadForButtonExecute = new Thread(() =>
                SearchInMeasure_logic(tokenSource.Token, startDate, endDate)
                );
                threadForButtonExecute.Start();

            }
            else if (button2PumpFlag == true)
            {

                tokenSource.Cancel();
                textBox4.Text = "Анализ останавливается";
            }
        }
        private void buttonRecordsAnalysis_Click(object sender, EventArgs e)
        {

            if (button2PumpFlag == false)
            {
                tokenSource = new CancellationTokenSource();
                var eventDate = dateTimePickerStartRecords.Value.Date.ToString();
                var insertDate = dateTimePickerEndRecords.Value.Date.ToString();
                textBoxRecords.Text = "Анализ запущен";
                threadForButtonRecords = new Thread(() =>
                SearchInRecords_logic(tokenSource.Token, eventDate, insertDate)
                );
                threadForButtonRecords.Start();

            }
            else if (button2PumpFlag == true)
            {

                tokenSource.Cancel();
                textBoxRecords.Text = "Анализ останавливается";
            }
        }
        private void DeserializedEventConsts()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<EntryForEvents>));

            List<EntryForEvents> list = new List<EntryForEvents>();

            using (TextReader reader = new StreamReader("../../../EventConsts.xml"))
            {
                list = (List<EntryForEvents>)serializer.Deserialize(reader);
            }

            var dict = new Dictionary<int, string>();

            foreach (EntryForEvents entry in list)
            {
                dict.Add(entry.Key, entry.Value);
            }

            eventConsts = dict;

        }
        private void SerializedConInfo()
        {

            List<Entry> entries = new List<Entry>(_connections.Count);
            foreach (string key in _connections.Keys)
            {
                if (_connections[key].AllTables.Columns.Count <= 0)
                {
                    _connections[key].AllTables.Columns.Add("Схема", typeof(string));
                    _connections[key].AllTables.Columns.Add("Таблица", typeof(string));
                    _connections[key].AllTables.Columns.Add("Размер", typeof(string));
                    _connections[key].AllTables.Columns.Add("Относительный_размер", typeof(decimal));
                    _connections[key].AllTables.Columns.Add("Активные_строки", typeof(float));
                }

                if (_connections[key].MeasuresTable.Columns.Count <= 0)
                {
                    _connections[key].MeasuresTable.Columns.Add("ID", typeof(Int32));
                    _connections[key].MeasuresTable.Columns.Add("Количество вхождений", typeof(Int32));
                }
                if (_connections[key].RecordsTable.Columns.Count <= 0)
                {
                    _connections[key].RecordsTable.Columns.Add("ID", typeof(Int32));
                    _connections[key].RecordsTable.Columns.Add("Количество вхождений", typeof(Int32));
                }

                entries.Add(new Entry(key, _connections[key]));
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Entry>));
            using (TextWriter writer = new StreamWriter("../../../SerializedDBInfo.xml"))
            {
                xmlSerializer.Serialize(writer, entries);
            }
            isChangesSaved = true;
            textBoxState.Text = ("Данные сохранены");

        }
        private void DeserializedConInfo()
        {

            _connections.Clear();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Entry>));

            List<Entry> list = new List<Entry>();

            using (TextReader reader = new StreamReader("../../../SerializedDBInfo.xml"))
            {
                list = (List<Entry>)serializer.Deserialize(reader);
            }

            foreach (Entry entry in list)
            {
                _connections[entry.Key] = entry.Value;
                _connections[entry.Key].Manager.CreateConnection();
                _connections[entry.Key].AllTables.TableName = entry.Value.Manager.connectionString.DataBase + ".AllTables";
                _connections[entry.Key].MeasuresTable.TableName = entry.Value.Manager.connectionString.DataBase + ".MeasuresTable";
                _connections[entry.Key].RecordsTable.TableName = entry.Value.Manager.connectionString.DataBase + ".RecordsTable";

                comboBoxSelectDB.Items.Add(entry.Key);
            }
            //textBox2.Text = Convert.ToString(list.Count());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SerializedConInfo();
        }

        private void comboBoxSelectDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentConnectionInfoID = Convert.ToString(comboBoxSelectDB.SelectedItem);

            if (button1.Enabled == false)
            {
                button1.Enabled = true;
                if (_connections[CurrentConnectionInfoID].AllTables.Rows.Count > 0)
                {
                    button2.Enabled = true;
                    buttonRecordsAnalysis.Enabled = true;
                    buttonAddRandomRows.Enabled = true;
                    buttonAddRandomRowsMeasures.Enabled = true;
                    buttonSaveToPDF.Enabled = true;
                }
            }
            WriteConnectionInfo(_connections[CurrentConnectionInfoID]);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (!isChangesSaved)
            {
                DialogResult dialogResult =
                    MessageBox.Show("У вас есть данные, не сохранённые в локальном хранилище. Сохранить их?", "Несохранённые изменения", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SerializedConInfo();
                }
            }
        }

        private void buttonSaveToPDF_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            PdfWriter.GetInstance(doc, new FileStream("../../../Data/pdfInfo_" +
                _connections[CurrentConnectionInfoID].Manager.connectionString.DataBase +
                ".pdf", FileMode.Create));

            doc.Open();

            BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            doc.Add(new Phrase("   База данных:   " +
                _connections[CurrentConnectionInfoID].Manager.connectionString.DataBase +
                "\n", font));

            doc.Add(new Phrase("   Общий вес БД:   " +
                _connections[CurrentConnectionInfoID].FullDBSize +
                "\n", font));
            var tables = new List<(DataTable, string)>{(
                _connections[CurrentConnectionInfoID].AllTables, "Информация о всех таблицах"),
                (_connections[CurrentConnectionInfoID].MeasuresTable,"Таблица Показания"),
                (_connections[CurrentConnectionInfoID].RecordsTable, "Таблица События")};

            foreach (var datatableInfo in tables)
            {
                DataTable datatable = datatableInfo.Item1;
                string name = datatableInfo.Item2;
                PdfPTable table = new PdfPTable(datatable.Columns.Count);

                PdfPCell cell = new PdfPCell(new Phrase(name, font));

                cell.Colspan = datatable.Columns.Count;
                cell.HorizontalAlignment = 1;
                //Убираем границу первой ячейки, чтобы балы как заголовок
                cell.Border = 0;
                table.AddCell(cell);

                //Сначала добавляем заголовки таблицы
                for (int j = 0; j < datatable.Columns.Count; j++)
                {
                    cell = new PdfPCell(new Phrase(new Phrase(datatable.Columns[j].ColumnName, font)));
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }

                for (int j = 0; j < datatable.Rows.Count; j++)
                {
                    for (int k = 0; k < datatable.Columns.Count; k++)
                    {
                        table.AddCell(new Phrase(datatable.Rows[j][k].ToString(), font));
                    }
                }
                //Добавляем таблицу в документ
                doc.Add(table);
                doc.Add(new Phrase("\n\n", font));

            }

            //Закрываем документ
            doc.Close();
        }

        private void buttonAddRandomRows_Click(object sender, EventArgs e)
        {
            var isInDBFlag = false;
            foreach (DataRow row in _connections[CurrentConnectionInfoID].AllTables.Rows)
            {
                if (row["Таблица"].ToString() == "Records")
                {
                    isInDBFlag = true;
                }
            }

            if (isInDBFlag)
            {
                _connections[CurrentConnectionInfoID].Manager.OpenConnection();
                var rowCount = Convert.ToInt32(textBoxAddRowsCount.Text);
                var seconds = Convert.ToInt32(textBoxAddRowsSeconds.Text);

                var sqlCmdChannels = "SELECT DISTINCT \"Records\".\"ChannelId\" FROM \"em_protocol\".\"Records\"\r\n\t" +
                    "LIMIT " + Convert.ToString(rowCount * 10) + ";";
                var sqlCmdObjectFieldId = "SELECT DISTINCT \"Records\".\"ObjectFieldId\" FROM \"em_protocol\".\"Records\"\r\n\t" +
                    "LIMIT 10 ;";
                var sqlCmdCommunicationObjectFieldId = "SELECT DISTINCT \"Records\".\"CommunicationObjectFieldId\" FROM \"em_protocol\".\"Records\"\r\n\t" +
                    "LIMIT 10 ;";

                int timeOut = Convert.ToInt32(textBoxTimeOut.Text);

                var tableChannels = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdChannels, timeOut);
                var tableObjectFieldId = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdObjectFieldId, timeOut);
                var tableCommunicationObjectFieldId = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdCommunicationObjectFieldId, timeOut);
                if (tableChannels.Rows.Count > 0)
                {
                    Random rnd = new Random();
                    Stopwatch stopwatch = new Stopwatch();

                    for (int i = 0; i < seconds; i++)
                    {
                        var sqlCmdInsertRows = "INSERT INTO em_protocol.\"Records\"(\r\n\t" +
                            "\"ChannelId\", \"EventTime\", \"ReceiptTime\", \"InsertTime\", \"NumberValue\", \"ObjectFieldId\", \"CommunicationObjectFieldId\") VALUES\r\n\t";
                        for (int j = 0; j < rowCount; j++)
                        {
                            int rndChannel = rnd.Next(0, tableChannels.Rows.Count);
                            int rndObjectFieldId = rnd.Next(0, tableObjectFieldId.Rows.Count);
                            int rndCommunicationObjectFieldId = rnd.Next(0, tableCommunicationObjectFieldId.Rows.Count);
                            sqlCmdInsertRows += "(" +
                                tableChannels.Rows[rndChannel]["ChannelId"].ToString() +
                                ", CURRENT_TIMESTAMP - interval '" + Convert.ToString(j) + " seconds'" +
                                ", CURRENT_TIMESTAMP , CURRENT_TIMESTAMP, 0" +
                                ", " + tableObjectFieldId.Rows[rndObjectFieldId]["ObjectFieldId"].ToString() +
                                ", " + tableCommunicationObjectFieldId.Rows[rndCommunicationObjectFieldId]["CommunicationObjectFieldId"].ToString() + ")";
                            if (rowCount - j > 1)
                            {
                                sqlCmdInsertRows += ",\r\n\t";
                            }
                            else
                            {
                                sqlCmdInsertRows += ";";
                            }
                        }
                        stopwatch.Start();

                        //_connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdInsertRows, timeOut);

                        stopwatch.Stop();
                        if (stopwatch.ElapsedMilliseconds < 1000)
                        {
                            Thread.Sleep(1000 - Convert.ToInt32(stopwatch.ElapsedMilliseconds));
                        }
                        textBoxAddRowsStatus.Text += sqlCmdInsertRows;
                        stopwatch.Reset();
                    }
                }
                else
                {
                    textBoxAddRowsStatus.Text = "Таблица пустая";
                }
                _connections[CurrentConnectionInfoID].Manager.CloseConnection();

            }
            else
            {
                textBoxAddRowsStatus.Text = "Неверный формат БД";
            }
        }

        private void textBoxAddRowsStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddRandomRowsMeasures_Click(object sender, EventArgs e)
        {
            var isInDBFlag = false;
            foreach (DataRow row in _connections[CurrentConnectionInfoID].AllTables.Rows)
            {
                if (row["Таблица"].ToString() == "Measures")
                {
                    isInDBFlag = true;
                }
            }

            if (isInDBFlag)
            {
                _connections[CurrentConnectionInfoID].Manager.OpenConnection();
                var rowCount = Convert.ToInt32(textBoxAddRowsCount.Text);
                var seconds = Convert.ToInt32(textBoxAddRowsSeconds.Text);

                //var sqlCmdMeasureChannels = "SELECT DISTINCT \"Measures\".\"MeasureChannelId\" FROM \"em_measures\".\"Measures\"\r\n\t" +
                //    "LIMIT " + Convert.ToString(rowCount * 10) + ";";
               

                int timeOut = Convert.ToInt32(textBoxTimeOut.Text);
                //var tableMeasureChannels = _connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdMeasureChannels, timeOut);

                //if (tableMeasureChannels.Rows.Count > 0)
                //{
                    Random rnd = new Random();
                    Stopwatch stopwatch = new Stopwatch();
                    for (int i = 0; i < seconds; i++)
                    {
                        var sqlCmdInsertRows = "INSERT INTO em_measures.\"Measures\"(\r\n\t" +
                            "\"MeasureChannelId\", \"TimeBegin\", \"TimeEnd\", \"Quality\", \"QualitySource\", \"Source\", \"Value\", \"InsertTime\") VALUES\r\n\t";
                        for (int j = 0; j < rowCount; j++)
                        {
                            //int rndMeasureChannel = rnd.Next(0, tableMeasureChannels.Rows.Count);

                            sqlCmdInsertRows += "(" +
                                Convert.ToString(j + 1) +
                                ", CURRENT_TIMESTAMP - interval '" + Convert.ToString(j) + " seconds'" +
                                ", CURRENT_TIMESTAMP - interval '" + Convert.ToString(j) + " seconds'" +
                                ", 0, 0, 0, 0,  CURRENT_TIMESTAMP)";

                            if (rowCount - j > 1)
                            {
                                sqlCmdInsertRows += ",\r\n\t";
                            }
                            else
                            {
                                sqlCmdInsertRows += ";";
                            }
                        }
                        stopwatch.Start();

                        //_connections[CurrentConnectionInfoID].Manager.sendRequest(sqlCmdInsertRows, timeOut);

                        stopwatch.Stop();

                        if (stopwatch.ElapsedMilliseconds < 1000)
                        {
                            Thread.Sleep(1000 - Convert.ToInt32(stopwatch.ElapsedMilliseconds));
                        }
                        textBoxAddRowsStatus.Text += sqlCmdInsertRows;
                        stopwatch.Reset();
                    }
                //}
                //else
                //{
                //    textBoxAddRowsStatus.Text = "Таблица пустая";
                //}
                _connections[CurrentConnectionInfoID].Manager.CloseConnection();

            }
            else
            {
                textBoxAddRowsStatus.Text = "Неверный формат БД";
            }
        }
    }
}
