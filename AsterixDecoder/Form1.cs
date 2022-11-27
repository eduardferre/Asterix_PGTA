using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsterixDecoder
{
    public partial class Form1 : Form
    {
        DecodeFiles decodeFiles = new DecodeFiles();

        List<CAT10> listCAT10 = new List<CAT10>();
        List<CAT21> listCAT21 = new List<CAT21>();
        
        DataTable dataTableCAT10 = new DataTable();
        DataTable dataTableCAT21 = new DataTable();

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gridCAT10.Visible = false;
            gridCAT21.Visible = false;
        }

        private void loadFiles_Button_Click(object sender, EventArgs e)
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
                foreach (string path in openFileDialog.FileNames)
                {
                    int result = decodeFiles.Read(path);

                    if (result == 1)
                    {
                        decodeFiles.numFiles++;
                        decodeFiles.nameFiles.Add(path);
                    }
                }
            }

            this.listCAT10 = decodeFiles.GetListCAT10();
            this.listCAT21 = decodeFiles.GetListCAT21();

            this.dataTableCAT10 = decodeFiles.GetTableCAT10();
            this.dataTableCAT21 = decodeFiles.GetTableCAT21();

            gridCAT10.Location = new Point(10, 10);
            gridCAT21.Location = new Point(10, 320);
            
            gridCAT10.Size = new Size(800, 300);
            gridCAT21.Size = new Size(800, 300);

            this.Controls.Add(gridCAT10);
            this.Controls.Add(gridCAT21);

            gridCAT10.DataSource = dataTableCAT10;
            gridCAT21.DataSource = dataTableCAT21;

            gridCAT10.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            gridCAT21.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            gridCAT10.Visible = true;
            gridCAT21.Visible = true;
        }

        private void gridCAT10_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;
            
            if (row >= 0 && column >= 0)
            {
                CAT10 cat10 = this.listCAT10[row];
                string columnName = gridCAT10.Columns[column].Name;
                
                if (columnName == "Target Report")
                {
                    doubleClickInfo_label.Text = cat10.TYP + "\n" + cat10.DCR + "\n" + cat10.CHN +
                    "\n" + cat10.GBS + "\n" + cat10.CRT + "\n" + cat10.SIM + "\n" + cat10.TST + "\n"
                    + cat10.RAB + "\n" + cat10.LOP + "\n" + cat10.TOT + "\n" + cat10.SPI;
                }
                else if (columnName == "Track Status")
                {
                    doubleClickInfo_label.Text = cat10.CNF + "\n" + cat10.TRE + "\n" + cat10.CST + "\n"
                    + cat10.MAH + "\n" + cat10.TCC + "\n" + cat10.STH + "\n" + cat10.TOM + "\n" + cat10.DOU
                    + "\n" + cat10.MRS + "\n" + cat10.GHO;
                }
                else if (columnName == "Target Size and Orientation")
                {
                    doubleClickInfo_label.Text = cat10.targetSizeOrientation;
                }
                else if (columnName == "Flight Level")
                {
                    doubleClickInfo_label.Text = cat10.FlightLevelInfo;
                }
                else if (columnName == "System Status")
                {
                    doubleClickInfo_label.Text = cat10.NOGO + "\n" + cat10.OVL + "\n" + cat10.TSV + "\n"
                    + cat10.DIV + "\n" + cat10.TIF;
                }
                else if (columnName == "Mode S MB Data")
                {
                    //?doubleClickInfo_label.Text = cat10.modeSrep.ToString();
                }
                else if (columnName == "Standard Deviation of Position")
                {
                    doubleClickInfo_label.Text = cat10.StandardDevPos;
                }
                else if (columnName == "Presence")
                {
                    //?doubleClickInfo_label.Text = cat10.REPPresence.ToString();
                }
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

                if (columnName == "Target Report")
                {
                    doubleClickInfo_label.Text = cat21.ATP + "\n" + cat21.ARC + "\n" + cat21.RC + "\n" +
                    cat21.RAB + "\n" + cat21.DCR + "\n" + cat21.GBS + "\n" + cat21.SIM + "\n" + cat21.TST
                    + "\n" + cat21.SAA + "\n" + cat21.CL + "\n" + cat21.IPC + "\n" + cat21.NOGO + "\n" +
                    cat21.LDPJ + "\n" + cat21.RCF;
                }
                else if (columnName == "Quality Indicators")
                {
                    doubleClickInfo_label.Text = cat21.NUCr_NACv + "\n" + cat21.NUCp_NIC + "\n" + cat21.NICbaro
                    + "\n" + cat21.SILsupp + "\n" + cat21.NACp + "\n" + cat21.SDA + "\n" + cat21.GVA + "\n" +
                    cat21.PICsupp + "\n" + cat21.ICB + "\n" + cat21.NUCp + "\n" + cat21.NIC;
                }
                else if (columnName == "Target Status")
                {
                    doubleClickInfo_label.Text = cat21.ICF + "\n" + cat21.LNAV + "\n" + cat21.PS + "\n" + cat21.SS;
                }
                else if (columnName == "Met Information")
                {
                    doubleClickInfo_label.Text = cat21.WindSpeed + "\n" + cat21.WindDirection + "\n" + 
                    cat21.Temperature + "\n" + cat21.Turbulence;
                }
                else if (columnName == "Final State Selected Altitude")
                {
                    doubleClickInfo_label.Text = cat21.MV + "\n" + cat21.AH + "\n" + cat21.AM + "\n" + cat21.finalStateSelectedAltitude;
                }
                else if (columnName == "Trajectory Intent")
                {
                    doubleClickInfo_label.Text = cat21.NAV + "\n" + cat21.NVB;
                }
                else if (columnName == "Aircraft Operational Status")
                {
                    doubleClickInfo_label.Text = cat21.RA + "\n" + cat21.TC + "\n" + cat21.TS + "\n" + cat21.CDTIA + "\n" + cat21.NotTCAS + "\n" + cat21.SA;
                }
                else if (columnName == "Surface Capabilities and Characteristics") 
                {
                    doubleClickInfo_label.Text = cat21.POA + "\n" + cat21.CDTIS + "\n" + cat21.B2Low + "\n" + cat21.RAS + "\n" + cat21.IDENT + "\n" + cat21.Length_Width;
                }
                else if (columnName == "Mode S MB Data")
                {
                    //?doubleClickInfo_label.Text = cat21.REP_SMB.ToString();
                }
                else if (columnName == "ACAS Resolution Advisory Report")
                {
                    doubleClickInfo_label.Text = cat21.TYP + "\n" + cat21.STYP + "\n" + cat21.ARA + "\n" + cat21.RAC + "\n" + cat21.RAT + "\n" + cat21.MTE + "\n" + cat21.TTI + "\n" + cat21.TID;
                }
                else if (columnName == "Data Ages")
                {
                    doubleClickInfo_label.Text = cat21.AOS + "\n" + cat21.TRD + "\n" + cat21.M3A + "\n" + cat21.QI + "\n" + cat21.TI + "\n" + cat21.MAM + "\n" + cat21.GH + "\n" + cat21.FL + "\n" + cat21.ISA + "\n" + cat21.FSA + "\n" + cat21.AS + "\n" + cat21.TAS + "\n" + cat21.MH + "\n" + cat21.BVR + "\n" + cat21.GVR + "\n" + cat21.GV + "\n" + cat21.TAR + "\n" + cat21.TIDataAge + "\n" + cat21.TSDataAge + "\n" + cat21.MET + "\n" + cat21.ROA + "\n" + cat21.ARADataAge + "\n" + cat21.SCC;
                }
            }
        }

        private void doubleClickInfo_label_Click(object sender, EventArgs e)
        {

        }
    }
}
