using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Design.AxImporter;

namespace UMLClassDiagramTranslator
{
    public partial class LoadForm : Form
    {
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        SelectedFile sf = new SelectedFile();

        public LoadForm()
        {
            InitializeComponent();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.WindowsDefaultLocation;
            this.Top = 0;
            btnBrowse.Left = (this.ClientSize.Width - btnBrowse.Width) / 2;
            btnClose.Left = (this.ClientSize.Width - btnClose.Width) / 2;

            string path = GetDefaultPath();

        }

        private string GetDefaultPath()
        {
            mainMenu mm = new mainMenu();
            string path = Path
                .Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                mm.newFolder);

            return path;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = GetDefaultPath();
            openFileDialog1.Title = "Browse NCP Files";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "svg";
            openFileDialog1.Filter = "Nikon Custom Picture (*.NCP)|*.ncp";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            // Show the dialog box and check if the user selected a file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the file path of the selected SVG file
                string filePath = openFileDialog1.FileName;
                sf.selectedFile = filePath;



                // Load the SVG file into your application using a library or control that supports SVG
                // For example, you could use the SvgDocument class from the Svg.NET library:
                //SvgDocument svgDocument = SvgDocument.Open(filePath);

                // Display the SVG document in your application as needed
                lblFile.Text = String.Join("\n\n", filePath).TrimEnd();

            }

            lblFile.TextAlign = ContentAlignment.MiddleLeft;

            LoadFile();

        }

        private void LoadFile()
        {
            // Find the open instance of the TargetForm
            var mm = new mainMenu();

            foreach (var form in Application.OpenForms)
            {
                if (form is mainMenu)
                {
                    mm = (mainMenu)form;
                    break;
                }
            }

            // If the TargetForm is open, set its Data property
            if (mm != null)
            {
                mm.selectedFile = sf.selectedFile;
            }

            lblConfirm.Text = "File has been successfully loaded!";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
