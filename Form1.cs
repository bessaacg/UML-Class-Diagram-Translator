using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using Svg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;


namespace UMLClassDiagramTranslator
{
    public partial class mainMenu : Form
    {
        public string newFolder = "umlClassDiagrams";

        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        public string selectedFile { get; set; } = String.Empty;

        public mainMenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            btnCreate.Left = (this.ClientSize.Width - btnCreate.Width) / 2;
            btnLoad.Left = (this.ClientSize.Width - btnLoad.Width) / 2;
            btnLitTrans.Left = (this.ClientSize.Width - btnLoad.Width) / 2;
            btnModTrans.Left = (this.ClientSize.Width - btnLoad.Width) / 2;
            btnClose.Left = (this.ClientSize.Width - btnClose.Width) / 2;

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Process ExternalProcess = new Process();
            ExternalProcess.StartInfo.FileName = @"NClass.exe";
            ExternalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            ExternalProcess.Start();
            ExternalProcess.WaitForExit();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadForm frm = new LoadForm();
            frm.ShowDialog();
        }

        ////Finding the number of boxes/classes from an svg class diagram
        //public static int FindingBoxesOrClasses(string path)
        //{
        //    XDocument doc = XDocument.Load(path);

        //    // Find all rectangles where fill="none"

        //    var rects = doc.Descendants()
        //        .Where(e => e.Name.LocalName == "rect")
        //        .Where(r => r.Attribute("fill") != null &&
        //    r.Attribute("fill")?.Value == "none");
        //    return rects.Count();
        //}

        ////Finding classes and their members from a uml class diagram
        //public static List<string> FindingClassElements(string path)
        //{
        //    XDocument doc = XDocument.Load(path);

        //    //classes will store a list of classes and their members
        //    List<string> classes = new List<string>();

        //    //string builder will store text and path elements
        //    StringBuilder sb = new StringBuilder();

        //    //Find all the 'g' elements in the document
        //    var gElements = doc.Descendants().Where(e => e.Name.LocalName == "g" &&
        //      e.Attribute("transform") != null);

        //    foreach (var gElement in gElements)
        //    {
        //        // Find all the 'rect' elements with 'fill="none"'
        //        var rectElements = gElement.Descendants()
        //       .Where(e => e.Name.LocalName == "rect" &&
        //       e.Attribute("fill")?.Value == "none");

        //        foreach (var rectElement in rectElements)
        //        {
        //            // Extract the relevant attributes for the 'rect' element
        //            string className = rectElement.Attribute("id")?.Value;
        //            string x = rectElement.Attribute("x")?.Value;
        //            string y = rectElement.Attribute("y")?.Value;
        //            string width = rectElement.Attribute("width")?.Value;
        //            string height = rectElement.Attribute("height")?.Value;

        //            // Find all the 'text' and 'path' elements inside the 'g' Element
        //            var textAndPathElements = gElement.Descendants()
        //                            .Where(e => e.Name.LocalName == "text" ||
        //                                e.Name.LocalName == "path");

        //            foreach (var element in textAndPathElements)
        //            {
        //                if (element.Name.LocalName == "path")
        //                {
        //                    sb.Append(Environment.NewLine);
        //                }
        //                else
        //                    sb.AppendLine(element.Value);
        //            }

        //            classes.Add(sb.ToString().Trim());
        //            sb.Clear();
        //        }
        //    }

        //    return classes;
        //}

        //public static string FindTypeOfShape(string path, int i)
        //{
        //    XDocument doc = XDocument.Load(path);

        //    List<string> shapeType = new List<string>();

        //    //Find all the 'g' elements in the document
        //    var gElements = doc.Descendants().Where(e => e.Name.LocalName == "g" &&
        //        e.Attribute("transform") != null);

        //    foreach (var gElement in gElements)
        //    {
        //        // Find all the 'rect' elements with 'fill="none"'
        //        var rectElements = gElement.Descendants()
        //       .Where(e => e.Name.LocalName == "rect" &&
        //       e.Attribute("fill")?.Value == "none");

        //        foreach (var rectElement in rectElements)
        //        {
        //            // Extract the relevant attributes for the 'rect' element
        //            string width = rectElement.Attribute("width")?.Value;
        //            string height = rectElement.Attribute("height")?.Value;

        //            string shape = (width == height) ? "square" : "rectangle";

        //            shapeType.Add(shape);
        //        }
        //    }

        //    return shapeType[i];
        //}

        ////Finding commpartments of each class box
        //public static int FindRectangleCompartments(string str)
        //{
        //    //spliting string based on an empty line
        //    string[] compartments = str.ToString().Split(new string[] { Environment.NewLine + Environment.NewLine },
        //        StringSplitOptions.RemoveEmptyEntries);

        //    int numberOfCompartments = compartments.Count();

        //    return numberOfCompartments;
        //}

        ////Finding horizontal lines separating compartments
        //public static int HorizontalLines(string cclass)
        //{
        //    int hLines = cclass.ToString().Split(new string[] { Environment.NewLine + Environment.NewLine },
        //        StringSplitOptions.RemoveEmptyEntries).Count() - 1;

        //    return hLines;
        //}


        private void btnLitTrans_Click(object sender, EventArgs e)
        {


            string litTranslation = TranslateLiteral.Translate(selectedFile);

            DisplayTranslation(litTranslation);

        }

        private static void DisplayTranslation(string translation)
        {
            // Create a temporary text file
            string filePath = Path.Combine(Path.GetTempPath(), "temp.txt");
            File.WriteAllText(filePath, translation);

            // Start Notepad and open the temporary file
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "notepad.exe",
                Arguments = filePath,
                WindowStyle = ProcessWindowStyle.Maximized
            };
            Process.Start(startInfo);
        }

        
        private void btnModTrans_Click(object sender, EventArgs e)
        {
            string modTranslation = TranslateModel.TranslateToModel(selectedFile);
            DisplayTranslation(modTranslation);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

    














































    
