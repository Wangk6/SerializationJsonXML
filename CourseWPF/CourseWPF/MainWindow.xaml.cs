//******************************************************
// File: MainWindow.xaml.cs
//
// Purpose: Frontend GUI for the students coursework.
//          The user picks a JSON file to read from.
//
// Written By: Kevin Wang
//
// Compiler: Visual Studio 2017
//
//******************************************************

using CourseWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace CourseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CourseWorkClass courseWork;

        public MainWindow()
        {
            InitializeComponent();
        }

        //****************************************************************
        //Name: OpenCourseWork_Click()
        //Purpose: When user clicks on button 'Open Course Work JSON File',
        //         opens a file dialog that allows user to select a file.
        //         The path is then displayed to textBoxFileName.
        //Input Type: None
        //Output Type: None
        //****************************************************************
        private void OpenCourseWork_Click(object sender, RoutedEventArgs e)
        {
            //Current working directory
            string exeDir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            //Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //Title of file dialog
            dlg.Title = "Open Course Work From JSON";

            if (Directory.Exists(exeDir))
            {
                dlg.InitialDirectory = exeDir;
            }
            else
            {
                dlg.InitialDirectory = @"C:\";
            }
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".json";
            dlg.Filter = "JSON Files (*.json)|*.json";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                //Clear previous boxes
                textBoxFileName.Text = "";
                textBoxFileName.Text = dlg.FileName;
                readCourseWorkJSON(dlg.FileName);
            }
        }

        //****************************************************************
        //Name: FindSubmission_Click()
        //Purpose: When user clicks on button 'Find Submission', finds the
        //         submission inputted in textBoxTargetAssignmentName.
        //Input Type: None
        //Output Type: None
        //****************************************************************
        private void FindSubmission_Click(object sender, RoutedEventArgs e)
        {
            clearSubmissionFields();

            Submission result = courseWork.Submissions.Find(x => x.AssignmentName == textBoxTargetAssignmentName.Text.Trim());
            if (result != null)
            {
                textBoxAssignmentName.Text = result.AssignmentName;
                textBoxCategoryName.Text = result.CategoryName;
                textBoxGrade.Text = result.Grade.ToString();
            }
            else
            {
                labelSubmissionResult.Content = "Assignment " + textBoxTargetAssignmentName.Text + " Not Found";
            }
        }

        //****************************************************************
        //Name: readCourseWorkJSON()
        //Purpose: Asks the user to input a file to read and will search for
        //         a JSON file to set to the object
        //Input Type: None - setFileName for file name
        //Output Type: None
        //****************************************************************
        private void readCourseWorkJSON(string fileName)
        {
            //Open a reader using the file name given by user to read the JSON/XML
            try
            {
                FileStream reader = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CourseWorkClass));
                courseWork = (CourseWorkClass)serializer.ReadObject(reader);
                reader.Close();
                Console.WriteLine("Reading Completed");
                showCourseDetails();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File Not Found");
            }
            catch (SerializationException)
            {
                Console.WriteLine("Wrong File Selected");
            }
        }

        //****************************************************************
        //Name: showCourseDetails()
        //Purpose: After user inputs JSON file, fill listView Categories,
        //         Assignments, Submissions after clearing fields
        //Input Type: None
        //Output Type: None
        //****************************************************************
        private void showCourseDetails()
        {
            //Clear ListViews then set items
            clearFields();

            //Category
            foreach(Category cw in courseWork.Categories)
            {
                listViewCategories.Items.Add(cw);
            }

            //Assignments
            foreach (Assignment asn in courseWork.Assignments)
            {
                listViewAssignments.Items.Add(asn);
            }

            //Assignments
            foreach (Submission sub in courseWork.Submissions)
            {
                listViewSubmissions.Items.Add(sub);
            }
        }

        //****************************************************************
        //Name: clearFields()
        //Purpose: Clears all fields including submissions
        //Input Type: None
        //Output Type: None
        //****************************************************************
        private void clearFields()
        {
            textBoxCourseName.Text = courseWork.CourseName;
            textBoxOverallGrade.Text = courseWork.CalculateGrade().ToString();
            listViewCategories.Items.Clear();
            listViewAssignments.Items.Clear();
            listViewSubmissions.Items.Clear();
            textBoxTargetAssignmentName.Clear();
            clearSubmissionFields();
        }

        //****************************************************************
        //Name: clearSubmissionFields()
        //Purpose: Clears all submissions fields besides target textBox
        //Input Type: None
        //Output Type: None
        //****************************************************************
        private void clearSubmissionFields()
        {
            textBoxAssignmentName.Clear();
            textBoxCategoryName.Clear();
            textBoxGrade.Clear();
            labelSubmissionResult.Content = "";
        }
    }
}
