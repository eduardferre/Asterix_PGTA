using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Net;


using GMap.NET;
using System.Reflection;
using GMap.NET.MapProviders;
using Cursors = System.Windows.Input.Cursors;
using Image = System.Drawing.Image;
using GMap.NET.WindowsPresentation;
using MessageBox = System.Windows.MessageBox;
using Microsoft.Ajax.Utilities;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
namespace AsterixDecoder
{
    public partial class AsterixDecoder : Form
    {
        DecodeFiles decodeFiles = new DecodeFiles();
        List<CAT10> listCAT10 = new List<CAT10>();
        List<CAT21> listCAT21 = new List<CAT21>();
        List<CATALL> listCATALL = new List<CATALL>();

        DataTable dataTableCAT10 = new DataTable();
        DataTable dataTableCAT21 = new DataTable();

        List<markerWithInfo> markers = new List<markerWithInfo>();
        int time = 0;

        public AsterixDecoder()
        {
            InitializeComponent();
        }

        

        private void AsterixDecoder_Load(object sender, EventArgs e)
        {
            process_label.Text = "Select a file to decode";
            clickInfo_label.Visible = false;
            gridCAT10.Visible = false;
            gridCAT21.Visible = false;
            info_label.Visible = false;
            process_label.Visible = true;
            msg_label.Visible = false;
            gMapControl1.Visible = false;
            timeLabel.Visible = false;
            startButton.Visible = false;
            resetButton.Visible = false;
            speedButton.Visible = false;
        }

        private void gridCAT10_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;

            if (row >= 0 && column >= 0)
            {
                CAT10 cat10 = this.listCAT10[row];
                string columnName = gridCAT10.Columns[column].Name;

                string info = "";

                if (columnName == "Target Report")
                {
                    info = cat10.TYP + "\n" + cat10.DCR + "\n" + cat10.CHN +
                    "\n" + cat10.GBS + "\n" + cat10.CRT + "\n" + cat10.SIM + "\n" + cat10.TST + "\n"
                    + cat10.RAB + "\n" + cat10.LOP + "\n" + cat10.TOT + "\n" + cat10.SPI;
                }
                else if (columnName == "Track Status")
                {
                    info = cat10.CNF + "\n" + cat10.TRE + "\n" + cat10.CST + "\n"
                    + cat10.MAH + "\n" + cat10.TCC + "\n" + cat10.STH + "\n" + cat10.TOM + "\n" + cat10.DOU
                    + "\n" + cat10.MRS + "\n" + cat10.GHO;
                }
                else if (columnName == "Target Size and Orientation")
                {
                    info = cat10.targetSizeOrientation;
                }
                else if (columnName == "Flight Level")
                {
                    info = cat10.FlightLevelInfo;
                }
                else if (columnName == "System Status")
                {
                    info = cat10.NOGO + "\n" + cat10.OVL + "\n" + cat10.TSV + "\n"
                    + cat10.DIV + "\n" + cat10.TIF;
                }
                else if (columnName == "Mode-3A Code")
                {
                    info = cat10.VMode3A + "\n" + cat10.GMode3A + "\n" + cat10.LMode3A + "\n" + cat10.Mode3A;
                }
                else if (columnName == "Mode S MB Data")
                {
                    info = "Repetitions: " + cat10.modeSrep.ToString();
                    for (int s = 0; s < cat10.modeSrep; s++)
                    {
                        info = info + "\n" + "Repetition: " + Convert.ToString(s) + "\n" + "Mode S Comm B message data: " + cat10.MBData[s] + "\n" + "Comm B Data Buffer Store 1 Address: " + cat10.BDS1[s] + "\n" + "Comm B Data Buffer Store 2 Address: " + cat10.BDS2[s];
                    }
                }
                else if (columnName == "Standard Deviation of Position")
                {
                    info = cat10.StandardDevPos;
                }
                else if (columnName == "Presence")
                {
                    info = "Repetitions: " + Convert.ToString(cat10.REPPresence);
                    for (int s = 0; s < cat10.REPPresence; s++)
                    {
                        info = info + "\n" + "Difference between the radial distance of the plot centre and that of the presence: " + cat10.DRHO[s] + "\n" + "Difference between the azimuth of the plot centre and that of the presence: " + cat10.DTHETA[s];
                    }
                }

                string[] infoSplit = info.Split("\n");
                info = "";

                foreach (string i in infoSplit)
                {
                    if (i != "") info = info + i + "\n";
                }

                clickInfo_label.Text = info;
            }

        }

        private void gridCAT21_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;

            if (row >= 0 && column >= 0)
            {
                CAT21 cat21 = this.listCAT21[row];
                string columnName = gridCAT21.Columns[column].Name;

                string info = "";

                if (columnName == "Target Report")
                {
                    info = cat21.ATP + "\n" + cat21.ARC + "\n" + cat21.RC + "\n" +
                    cat21.RAB + "\n" + cat21.DCR + "\n" + cat21.GBS + "\n" + cat21.SIM + "\n" + cat21.TST
                    + "\n" + cat21.SAA + "\n" + cat21.CL + "\n" + cat21.IPC + "\n" + cat21.NOGO + "\n" +
                    cat21.LDPJ + "\n" + cat21.RCF;
                }
                else if (columnName == "Quality Indicators")
                {
                    info = cat21.NUCr_NACv + "\n" + cat21.NUCp_NIC + "\n" + cat21.NICbaro
                    + "\n" + cat21.SILsupp + "\n" + cat21.NACp + "\n" + cat21.SDA + "\n" + cat21.GVA + "\n" +
                    cat21.PICsupp + "\n" + cat21.ICB + "\n" + cat21.NUCp + "\n" + cat21.NIC;
                }
                else if (columnName == "Target Status")
                {
                    info = cat21.ICF + "\n" + cat21.LNAV + "\n" + cat21.PS + "\n" + cat21.SS;
                }
                else if (columnName == "Met Information")
                {
                    info = cat21.WindSpeed + "\n" + cat21.WindDirection + "\n" +
                    cat21.Temperature + "\n" + cat21.Turbulence;
                }
                else if (columnName == "Selected Altitude")
                {
                    info = cat21.SAS + "\n" + cat21.Source + "\n" + cat21.SA;
                }
                else if (columnName == "Final State Selected Altitude")
                {
                    info = cat21.MV + "\n" + cat21.AH + "\n" + cat21.AM + "\n" + cat21.finalStateSelectedAltitude;
                }
                else if (columnName == "Trajectory Intent")
                {
                    info = cat21.NAV + "\n" + cat21.NVB;
                }
                else if (columnName == "Aircraft Operational Status")
                {
                    info = cat21.RA + "\n" + cat21.TC + "\n" + cat21.TS + "\n" + cat21.CDTIA + "\n" +
                    cat21.NotTCAS + "\n" + cat21.SA;
                }
                else if (columnName == "Surface Capabilities and Characteristics")
                {
                    info = cat21.POA + "\n" + cat21.CDTIS + "\n" + cat21.B2Low + "\n" + cat21.RAS + "\n" +
                    cat21.IDENT + "\n" + cat21.Length_Width;
                }
                else if (columnName == "Mode S MB Data")
                {
                    info = "Repetitions: " + cat21.REP_SMB.ToString();
                    for (int s = 0; s < cat21.REP_SMB; s++)
                    {
                        info = info + "\n" + "Repetition: " + Convert.ToString(s) + "\n" + "Mode S Comm B message data: " + cat21.MBData[s] + "\n" + "Comm B Data Buffer Store 1 Address: " + cat21.BDS1[s] + "\n" + "Comm B Data Buffer Store 2 Address: " + cat21.BDS2[s];
                    }
                }
                else if (columnName == "ACAS Resolution Advisory Report")
                {
                    info = cat21.TYP + "\n" + cat21.STYP + "\n" + cat21.ARA + "\n" + cat21.RAC + "\n" + cat21.RAT
                    + "\n" + cat21.MTE + "\n" + cat21.TTI + "\n" + cat21.TID;
                }
                else if (columnName == "Data Ages")
                {
                    info = cat21.AOS + "\n" + cat21.TRD + "\n" + cat21.M3A + "\n" + cat21.QI + "\n" + cat21.TI +
                    "\n" + cat21.MAM + "\n" + cat21.GH + "\n" + cat21.FL + "\n" + cat21.ISA + "\n" + cat21.FSA + "\n" + cat21.AS + "\n"
                    + cat21.TAS + "\n" + cat21.MH + "\n" + cat21.BVR + "\n" + cat21.GVR + "\n" + cat21.GV + "\n" + cat21.TAR + "\n" +
                    cat21.TIDataAge + "\n" + cat21.TSDataAge + "\n" + cat21.MET + "\n" + cat21.ROA + "\n" + cat21.ARADataAge + "\n" + cat21.SCC;
                }

                string[] infoSplit = info.Split("\n");
                info = "";

                foreach (string i in infoSplit)
                {
                    if (i != "") info = info + i + "\n";
                }

                clickInfo_label.Text = info;
            }
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt) | *.txt* | ast files (*.ast) | *.ast*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Multiselect = true;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();

            if (openFileDialog.SafeFileName != null)
            {
                decodeFiles.ClearStoredData();

                string[] fileNames = openFileDialog.FileNames;

                if (fileNames.Length == 1) process_label.Text = "Selected file: \n                        " + fileNames[0];
                else
                {
                    string process = "Selected files: \n                        ";
                    foreach (string p in fileNames) { process = process + p + "\n                        "; }

                    process_label.Text = process;
                }

                foreach (string path in fileNames)
                {
                    int result = decodeFiles.Read(path);

                    if (result == 1)
                    {
                        decodeFiles.numFiles++;
                        decodeFiles.nameFiles.Add(path);
                    }
                    else MessageBox.Show(path + " file is nor readable or an error ocurred while decoding");
                }

                msg_label.Text = "There are a total of " + decodeFiles.numMsgs.ToString() + " messages\n" +
                                 "CAT10: " + decodeFiles.numCAT10Msgs.ToString() + " messages\n" +
                                 "        SMR: " + decodeFiles.numCAT10SMRMsgs + " messages\n" +
                                 "        MLAT: " + decodeFiles.numCAT10MLATMsgs + " messages\n" +
                                 "CAT21: " + decodeFiles.numCAT21Msgs.ToString() + " messages";
            }

            msg_label.Visible = true;

            this.listCAT10 = decodeFiles.GetListCAT10();
            this.listCAT21 = decodeFiles.GetListCAT21();
            this.listCATALL = decodeFiles.GetListCATALL();

            this.dataTableCAT10 = decodeFiles.GetTableCAT10();
            this.dataTableCAT21 = decodeFiles.GetTableCAT21();
        }

        private void tableCAT10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickInfo_label.Visible = true;
            gridCAT10.Visible = true;
            gridCAT21.Visible = false;
            info_label.Visible = true;
            process_label.Visible = false;
            msg_label.Visible = false;
            gMapControl1.Visible = false;
            timeLabel.Visible = false;
            startButton.Visible = false;
            resetButton.Visible = false;
            speedButton.Visible = false;

            if (listCAT10.Count != 0)
            {

                gridCAT10.Location = new Point(10, 40);
                clickInfo_label.Location = new Point(1075, 80);
                info_label.Location = new Point(1100, 40);

                gridCAT10.Size = new Size(1050, 600);
                clickInfo_label.Size = new Size(210, 250);

                this.Controls.Add(gridCAT10);

                gridCAT10.DataSource = dataTableCAT10;

                gridCAT10.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                info_label.Visible = true;
                clickInfo_label.Visible = true;
                gridCAT10.Visible = true;
            }
            else { MessageBox.Show("No data for CAT10, please upload a file with CAT10 messages"); }
        }

        private void tableCAT21ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickInfo_label.Visible = true;
            gridCAT10.Visible = false;
            gridCAT21.Visible = true;
            info_label.Visible = true;
            process_label.Visible = false;
            msg_label.Visible = false;
            gMapControl1.Visible = false;
            timeLabel.Visible = false;
            startButton.Visible = false;
            resetButton.Visible = false;
            speedButton.Visible = false;
            if (listCAT21.Count != 0)
            {

                gridCAT21.Location = new Point(10, 40);
                clickInfo_label.Location = new Point(1075, 80);
                info_label.Location = new Point(1100, 40);

                gridCAT21.Size = new Size(1050, 600);
                clickInfo_label.Size = new Size(210, 250);

                this.Controls.Add(gridCAT21);

                gridCAT21.DataSource = dataTableCAT21;

                gridCAT21.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                info_label.Visible = true;
                clickInfo_label.Visible = true;
                gridCAT21.Visible = true;
            }
            else { MessageBox.Show("No data for CAT21, please upload a file with CAT21 messages"); }
        }

        private void mapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickInfo_label.Visible = false;
            gridCAT10.Visible = false;
            gridCAT21.Visible = false;
            info_label.Visible = false;
            process_label.Visible = false;
            msg_label.Visible = false;
            gMapControl1.Visible = true;
            timeLabel.Visible = true;
            startButton.Visible = true;
            resetButton.Visible = true;
            speedButton.Visible = true;

            timer1.Stop();
            timer1.Interval = 1000;
            time = 86400;
            foreach (CATALL message in listCATALL) 
            { 
                if (message.timeOfDay < time) 
                {
                    time = message.timeOfDay;
                }
            }
            timeLabel.Text = Convert.ToString((time / (60 * 60)) % 24) + " : " + Convert.ToString((time / 60) % 60) + " : " + Convert.ToString((time % 60));

        }

        private void exportCSV_CAT10_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv*)|*.csv*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != null)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                string path = saveFileDialog.FileName + ".csv";
                StringBuilder sb = new StringBuilder();
                if (File.Exists(path)) { File.Delete(path); }

                StringBuilder ColumnsNames = new StringBuilder();

                foreach (DataColumn col in dataTableCAT10.Columns)
                {
                    ColumnsNames.Append(col.ColumnName + ';');
                }

                string ColNames = ColumnsNames.ToString();
                ColNames = ColNames.TrimEnd(';');
                sb.AppendLine(ColNames);

                foreach (DataRow row in dataTableCAT10.Rows) //cat10
                {
                    string space = ", ";
                    StringBuilder RowData = new StringBuilder();
                    int number = Convert.ToInt32(row[1].ToString());
                    CAT10 message = listCAT10[number - 1];

                    foreach (DataColumn column in dataTableCAT10.Columns)
                    {
                        string cellString = "";

                        string data = row[column].ToString();

                        if (data == "Click for more data")
                        {
                            if (column.ColumnName == "Target Report")
                            {
                                cellString = message.TYP + space + message.DCR + space + message.CHN + space + message.GBS + space + message.CRT;
                                if (message.SIM != null) { cellString = cellString + space + message.SIM + space + message.TST + space + message.RAB + space + message.LOP + space + message.TOT; }
                                if (message.SPI != null) { cellString = cellString + space + message.SPI; }
                            }
                            if (column.ColumnName == "Flight Level")
                            {
                                cellString = message.VFlightLevel + space + message.GFlightLevel + space + message.FlightLevel;
                            }
                            if (column.ColumnName == "Track Status")
                            {
                                cellString = cellString + message.CNF + space + message.TRE + space + message.CST + space + message.MAH + space + message.TCC + space + message.STH;
                                if (message.TOM != null) { cellString = cellString + space + message.TOM + space + message.DOU + space + message.MRS; }
                                if (message.GHO != null) { cellString = cellString + space + message.GHO; }
                            }
                            if (column.ColumnName == "System Status")
                            {
                                cellString = message.NOGO + space + message.OVL + space + message.TSV + space + message.DIV + space + message.TIF;
                            }
                            if (column.ColumnName == "Target Size and Orientation")
                            {
                                cellString = message.targetLength + space + message.targetWidth + space + message.targetOrientation;
                            }
                            if (column.ColumnName == "Mode-3A Code")
                            {
                                cellString = message.VMode3A + space + message.GMode3A + space + message.LMode3A + space + message.Mode3A;
                            }
                            if (column.ColumnName == "Mode S MB Data")
                            {
                                cellString = "Repetitions: " + Convert.ToString(message.modeSrep);
                                for (int s = 0; s < message.modeSrep; s++)
                                {
                                    cellString = cellString + space + "Repetition: " + Convert.ToString(s) + space + "Mode S Comm B message data: " + message.MBData[s] + space + "Comm B Data Buffer Store 1 Address: " + message.BDS1[s] + space + "Comm B Data Buffer Store 2 Address: " + message.BDS2[s];
                                }
                            }

                            if (column.ColumnName == "Standard Deviation of Position")
                            {
                                cellString = message.DeviationX + space + message.DeviationY + space + message.CovarianceXY;
                            }
                            if (column.ColumnName == "Presence")
                            {
                                cellString = "Repetitions: " + Convert.ToString(message.REPPresence);
                                for (int s = 0; s < message.REPPresence; s++)
                                {
                                    cellString = cellString + space + "Difference between the radial distance of the plot centre and that of the presence: " + message.DRHO[s] + space + "Difference between the azimuth of the plot centre and that of the presence: " + message.DTHETA[s];
                                }
                            }

                            data = cellString;
                        }

                        data = data.Replace(",", ".");
                        RowData.Append(data);
                        RowData.Append(";");
                    }

                    string RowDat = RowData.ToString();
                    RowDat = RowDat.TrimEnd(';');
                    sb.AppendLine(RowDat);
                }

                File.WriteAllText(path, sb.ToString());
                Mouse.OverrideCursor = null;
            }
        }

        private void exportCSV_CAT21_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv*)|*.csv*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != null)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                string path = saveFileDialog.FileName + ".csv";
                StringBuilder sb = new StringBuilder();
                if (File.Exists(path)) { File.Delete(path); }

                StringBuilder ColumnsNames = new StringBuilder();

                foreach (DataColumn col in dataTableCAT21.Columns)
                {
                    ColumnsNames.Append(col.ColumnName + ";");
                }

                string ColNames = ColumnsNames.ToString();
                ColNames = ColNames.TrimEnd(';');
                sb.AppendLine(ColNames);

                foreach (DataRow row in dataTableCAT21.Rows) //cat10
                {
                    string space = ", ";

                    StringBuilder RowData = new StringBuilder();
                    int number = Convert.ToInt32(row[1].ToString());
                    CAT21 message = listCAT21[number - 1];

                    foreach (DataColumn column in dataTableCAT21.Columns)
                    {

                        string cellString = "";

                        string data = row[column].ToString();

                        if (data == "Click for more data")
                        {
                            if (column.ColumnName == "Target Report")
                            {
                                cellString = message.ATP + space + message.ARC + space + message.RC + space + message.RAB;
                                if (message.DCR != null)
                                {
                                    cellString = cellString + space + message.DCR + space + message.GBS + space + message.SIM + space + message.TST + space + message.SAA + space + message.CL;
                                    if (message.IPC != null)
                                    {
                                        cellString = cellString + space + message.IPC + space + message.NOGO + space + message.CPR + space + message.LDPJ + space + message.RCF;
                                    }
                                }
                            }
                            if (column.ColumnName == "Quality Indicators")
                            {
                                cellString = message.NUCr_NACv + space + message.NUCp_NIC;
                                if (message.NICbaro != null)
                                {
                                    cellString = cellString + space + message.NICbaro + space + message.SIL + space + message.NACp;
                                    if (message.SILsupp != null)
                                    {
                                        cellString = cellString + space + message.SILsupp + space + message.SDA + space + message.GVA;
                                        if (message.ICB != null) { cellString = cellString + space + message.PICsupp + space + message.ICB + space + message.NUCp + space  + message.NIC; }
                                    }
                                }
                            }
                            if (column.ColumnName == "Target Status")
                            {
                                cellString = message.ICF + space + message.LNAV + space + message.PS + space + message.SS;
                            }
                            if (column.ColumnName == "Met Information")
                            {
                                if (message.WindSpeed != null) { cellString = cellString + message.WindSpeed; }
                                if (message.WindDirection != null) { cellString = cellString + space + message.WindDirection; }
                                if (message.Temperature != null) { cellString = cellString + space + message.Temperature; }
                                if (message.Turbulence != null) { cellString = cellString + space + message.Turbulence; }
                            }
                            if (column.ColumnName == "Selected Altitude")
                            {
                                cellString = cellString + message.SAS + space + message.Source + space + message.SA;
                            }
                            if (column.ColumnName == "Final State Selected Altitude")
                            {
                                cellString = cellString + message.MV + space + message.AH + message.AM + space + message.finalStateSelectedAltitude;
                            }
                            if (column.ColumnName == "Trajectory Intent")
                            {
                                if (message.NAV != null)
                                {
                                    cellString = cellString + message.NAV + space + message.NVB;
                                }
                                if (message.REP_TI != 0)
                                {
                                    cellString = cellString + space + "Repetitions: " + Convert.ToString(message.REP_TI);
                                    for (int s = 0; s < message.REP_TI; s++)
                                    {
                                        cellString = cellString + space + "Repetition: " + Convert.ToString(s) + space + message.TCA[s] + space + message.NC[s] + space + "Trajectory Change Point number: " + Convert.ToString(message.TCP[s]) + space + "Altitude: " + message.Altitude[s] + space + "Latitude: " + message.Latitude[s] + space + "Longitude: " + message.Longitude[s] + space + "PT: " + message.PointType[s] + space;
                                        cellString = cellString + space + "TD: " + message.TD[s] + space + "Turn Radius Availabilty" + message.TRA[s] + space + message.TOA[s] + space + "Time Over Point: " + message.TOV[s] + space + "TCP Turn radius: " + message.TTR[s];
                                    }
                                }
                            }
                            if (column.ColumnName == "Aircraft Operational Status")
                            {
                                cellString = cellString + message.RA + space + message.TC + space + message.TS + space + message.ARV + space + message.CDTIA + space + message.NotTCAS + space + message.SA;
                            }
                            if (column.ColumnName == "Surface Capabilities and Characteristics")
                            {
                                cellString = cellString + message.POA + space + message.CDTIS + space + message.B2Low + space + message.RAS + space + message.IDENT;
                                if (message.Length_Width != null) { cellString = cellString + space + message.Length_Width; }
                            }
                            if (column.ColumnName == "Mode S MB Data")
                            {
                                cellString = cellString + "Repetitions: " + message.REP_SMB;
                                for (int s = 0; s < message.REP_SMB; s++)
                                {
                                    cellString = cellString + space + "Repetition: " + Convert.ToString(s) + space + "Mode S Comm B message data: " + message.MBData[s] + space + "Comm B Data Buffer Store 1 Address: " + message.BDS1[s] + space + "Comm B Data Buffer Store 2 Address: " + message.BDS2[s];
                                }
                            }
                            if (column.ColumnName == "ACAS Resolution Advisory Report")
                            {
                                cellString = cellString + message.TYP + space + message.STYP + space + message.ARA + space + message.RAC + space + message.RAT + space + message.MTE + space + message.TTI + space + message.TID;
                            }
                            if (column.ColumnName == "Data Ages")
                            {
                                if (message.AOS != null) { cellString = cellString + message.AOS; }
                                if (message.TRD != null) { cellString = cellString + space + message.TRD; }
                                if (message.M3A != null) { cellString = cellString + space + message.M3A; }
                                if (message.QI != null) { cellString = cellString + space + message.QI; }
                                if (message.TI != null) { cellString = cellString + space + message.TI; }
                                if (message.MAM != null) { cellString = cellString + space + message.MAM; }
                                if (message.GH != null) { cellString = cellString + space + message.GH; }
                                if (message.FL != null) { cellString = cellString + space + message.FL; }
                                if (message.ISA != null) { cellString = cellString + space + message.ISA; }
                                if (message.FSA != null) { cellString = cellString + space + message.FSA; }
                                if (message.AS != null) { cellString = cellString + space + message.AS; }
                                if (message.TAS != null) { cellString = cellString + space + message.TAS; }
                                if (message.MH != null) { cellString = cellString + space + message.MH; }
                                if (message.BVR != null) { cellString = cellString + space + message.BVR; }
                                if (message.GVR != null) { cellString = cellString + space + message.GVR; }
                                if (message.GV != null) { cellString = cellString + space + message.GV; }
                                if (message.TAR != null) { cellString = cellString + space + message.TAR; }
                                if (message.TIDataAge != null) { cellString = cellString + space + message.TIDataAge; }
                                if (message.TSDataAge != null) { cellString = cellString + space + message.TSDataAge; }
                                if (message.MET != null) { cellString = cellString + space + message.MET; }
                                if (message.ROA != null) { cellString = cellString + space + message.ROA; }
                                if (message.ARADataAge != null) { cellString = cellString + space + message.ARADataAge; }
                                if (message.SCC != null) { cellString = cellString + space + message.SCC; }
                            }

                            data = cellString;
                        }

                        data = data.Replace(",", ".");
                        RowData.Append(data);
                        RowData.Append(";");
                    }

                    string RowDat = RowData.ToString();
                    RowDat = RowDat.TrimEnd(';');
                    sb.AppendLine(RowDat);
                }

                File.WriteAllText(path, sb.ToString());
                Mouse.OverrideCursor = null;
            }
        }

        private void exportKMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        int zoom = 12;
        private void gMapControl1_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            gMapControl1.RoutesEnabled = true;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.MarkersEnabled = true;
            gMapControl1.NegativeMode = false;
            gMapControl1.RetryLoadTile = 0;
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.AllowDrop = true;
            gMapControl1.IgnoreMarkerOnMouseWheel = true;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.DisableFocusOnMouseEnter = true;
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = zoom;
            gMapControl1.Position = new PointLatLng(41.295855, 2.08442);
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.ShowCenter = false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Start")
            {
                timer1.Start();
                startButton.Text = "Pause";
            }
            else
            {
                timer1.Stop();
                startButton.Text = "Start";
            }
        }

        private void AddActualMarker(double X, double Y, string Callsign, int time, int num, string emmiter, string TargetAdd, string detectionmode, string CAT, string SIC, string SAC, string Flight_level, string Track_number, int direction, int refreshratio)
        {
            PointLatLng coordinates = new PointLatLng(X, Y);
            markerWithInfo marker = new markerWithInfo(coordinates, Callsign, time, num, emmiter, TargetAdd, detectionmode, CAT, SIC, SAC, Flight_level, Track_number, direction, refreshratio);
            markers.Add(marker);
           // SetMarkerShape(marker);
        }
        

        public GMapOverlay OverlayMarkers = new GMapOverlay("Markers");

        public class MarkerIMG : GMarkerGoogle
        {
            public markerWithInfo marker;
            Bitmap bitmap;
            PointLatLng p;
            public MarkerIMG(PointLatLng p, Bitmap bitmap, markerWithInfo mk)
            :base(p,bitmap)
            {
                this.marker = mk;
            }
            public MarkerIMG(PointLatLng p, GMarkerGoogleType type, markerWithInfo mk)
            : base(p,type)
            {
                this.marker = mk;
            }
        }

        private void ShowMarkers()
        {
            OverlayMarkers.Markers.Clear();
            foreach (markerWithInfo marker in markers)
            {
                if (smrCheck.Checked)
                {
                    if (marker.DetectionMode == "SMR")
                    {
                        MarkerIMG mk = new MarkerIMG(marker.p, GMarkerGoogleType.blue_dot, marker);

                        OverlayMarkers.Markers.Add(mk);
                    }
                }
                if (mlatCheck.Checked)
                {
                    if (marker.DetectionMode == "MLAT")
                    {
                        MarkerIMG mk = new MarkerIMG(marker.p, GMarkerGoogleType.pink, marker);
                        OverlayMarkers.Markers.Add(mk);
                    }
                }
                if (adsbCheck.Checked)
                {
                    if (marker.DetectionMode == "ADSB")
                    {
                        MarkerIMG mk = new MarkerIMG(marker.p, GMarkerGoogleType.green, marker);
                        OverlayMarkers.Markers.Add(mk);
                    }
                }
            }
            gMapControl1.Overlays.Add(OverlayMarkers);
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            time++;
            timeLabel.Text = Convert.ToString((time / (60 * 60))%24) + " : " + Convert.ToString((time / 60)%60) + " : " + Convert.ToString((time%60));
            timeIncrease();
        }

        private void timeIncrease()
        {
            bool first_found = false;
            int s = 0;

            for (int i = 0; first_found == false && i<listCATALL.Count(); i++) { if (listCATALL[i].timeOfDay == time) { first_found = true; s = i; }; }

            while (listCATALL[s].timeOfDay == time)
            {
                CATALL message = listCATALL[s];
                if (message.latitudeInWGS84 != -200 && message.longitudeInWGS84 != -200)
                {
                    bool DuplicatedTarget = false;
                    bool DuplicatedTrackNumber = false;
                    if (message.targetAddress != null) { DuplicatedTarget = markers.Any(x => x.TargetAddress == message.targetAddress && x.TargetAddress != null && x.Time == message.timeOfDay && message.detectionMode == x.DetectionMode); }
                    else { DuplicatedTrackNumber = markers.Any(x => x.Track_number == message.trackNumber && x.Track_number != null && x.Time == message.timeOfDay && message.detectionMode == x.DetectionMode); }

                    if (DuplicatedTarget == false && DuplicatedTrackNumber == false)
                    {
                        markers.RemoveAll(item => (((item.TargetAddress == message.targetAddress && item.TargetAddress != null) || (item.Track_number == message.trackNumber && item.Track_number != null) || (item.Callsign == message.targetIdentification && item.Callsign != null)) && item.DetectionMode == message.detectionMode));
                        AddActualMarker(Convert.ToDouble(message.latitudeInWGS84), Convert.ToDouble(message.longitudeInWGS84), message.targetIdentification, time, message.msgNum, message.type, message.targetAddress, message.detectionMode, message.CAT, message.SIC, message.SAC, message.flightLevel, message.trackNumber, message.direction, message.refreshratio);
                    }
                }
                s++;
            }
            ShowMarkers();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            time = 86400;
            foreach (CATALL message in listCATALL)
            {
                if (message.timeOfDay < time)
                {
                    time = message.timeOfDay;
                }
            }
            markers.Clear();
            OverlayMarkers.Markers.Clear();
            timeLabel.Text = Convert.ToString((time / (60 * 60)) % 24) + " : " + Convert.ToString((time / 60) % 60) + " : " + Convert.ToString((time % 60));
            startButton.Text = "Start";
            
        }

        private void speedButton_Click(object sender, EventArgs e)
        {
            if (speedButton.Text=="Speed (x1)")
            {
                timer1.Interval = 100;
                speedButton.Text = "Speed (x10)";
            }
            else if (speedButton.Text == "Speed (x10)")
            {
                timer1.Interval = 20;
                speedButton.Text = "Speed (x50)";
            }
            else if (speedButton.Text == "Speed (x50)")
            {
                timer1.Interval = 1000;
                speedButton.Text = "Speed (x1)";
            }
        }

        private void timeButton_Click(object sender, EventArgs e)
        {
            Form2 cv = new Form2();
            cv.ShowDialog();
            time = cv.getTime();
        }


        void gMapControl1_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, System.Windows.Forms.MouseEventArgs e)
        {
        }
    }
}
