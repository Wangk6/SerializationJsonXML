//******************************************************
// File: CourseWork.cs
//
// Purpose: Contains the class definition for CourseWork.
//          CourseWork will hold all CourseWork information 
//          including course name, category, assignment and 
//          submission
//
// Written By: Kevin Wang
//
// Compiler: Visual Studio 2017
//
//******************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CourseWork
{
    [DataContract(Name ="coursename")]
    class CourseWork
    {
        #region Member Variables
        private string m_CourseName;
        private List<Category> m_Categories;
        private List<Assignment> m_Assignments;
        private List<Submission> m_Submissions;
        #endregion

        #region Methods
        //****************************************************************
        //Name: CourseWork()
        //Purpose: Default constructor that sets the name and percentage
        //         variables
        //Input Type: None
        //Output Type: None
        //****************************************************************
        public CourseWork()
        {
            m_CourseName = "Test Course Name";
            m_Categories.Add(new Category { Name = "Test Category Name", Percentage = 50.0 });
            m_Assignments.Add(new Assignment { Name = "Test Assignment Name", Description = "Test Description", CategoryName="Test Category Name" });
            m_Submissions.Add(new Submission { AssignmentName = "Test Assignment Name", CategoryName = "Test Category Name", Grade = 50.0 });
        }

        //***************************************************************
        //Purpose: This method takes the assignment name as a parameter.It should 
        //         return the Submission with the given assignment name.If it is 
        //         not found then return null.
        //Input Type: string asn - assignment name
        //Output Type: Submission - returns submission object or null
        //***************************************************************
        public Submission FindSubmission(string asn)
        {
            Submission result = Submissions.Find(x => x.AssignmentName == asn);
            return result;
        }

        //Used for grades, count, and weight
        struct TotalGrades
        {
            public double grade;
            public int numOfGrades;
            public double gradeWeight;
        }

        public double CalculateGrade()
        {
            //Final grade score
            double overallGrade = 0;

            TotalGrades exm = new TotalGrades(), hwk = new TotalGrades(), qz = new TotalGrades(), lb = new TotalGrades();

            //There are several ways of obtaining the grade information. 
            //One way is looping each category and a nested loop finding submissions that are in
            //category name then when submissions loop is complete add the weight to a list.
            //Downside is having to loop through the entire submissions list O(n) times with category
            //Is that the resulting runtime would be O(n^2)

            //First method will not require a sorted Json file, but if the json were to be sorted alphabetically,
            //we would be able to run each category then nest loop through the submissions and checking
            //to see if the next element is a different category. If the next elements category is different, store the index, break from the submissions loop. 
            //We then move to the next category assuming it's in order and start the index from the next element variable.
            //Runtime should be O(n)

            //Another way is not relying on sorted Json but the category names.
            //We must depend on the number of category and category names stay the same.
            //This method is really hardcoded but is good if category almost never changes.
            //We would simply just run through the submissions list and for each element
            //check the category name and add it to the correct coresponding category grade.
            //Runtime should be O(n)

            //In our program we assume that the number of category will stay the same so we use four double 
            //variables to store our different grades instead of a
            //list or dynamic array

            //Check to see if our harded category can be used
            if (Categories.Count == 4 && Categories.Find(x=> x.Name == "Exams").ToString() == "Exams"
                && Categories.Find(x => x.Name == "Homework").ToString() == "Homework"
                && Categories.Find(x => x.Name == "Quizzes").ToString() == "Quizzes"
                && Categories.Find(x => x.Name == "Labs").ToString() == "Labs")
            {
                //Run through the assignments
                foreach (Submission j in Submissions)
                {
                    //Exam Grade
                    if (j.CategoryName == "Exams")
                    {
                        exm.grade += j.Grade;
                        exm.numOfGrades++;
                    }
                    else if (j.CategoryName == "Homework")//Homework Grade
                    {
                        hwk.grade += j.Grade;
                        hwk.numOfGrades++;
                    }
                    else if (j.CategoryName == "Quizzes")//Quiz Grade
                    {
                        qz.grade += j.Grade;
                        qz.numOfGrades++;
                    }
                    else if (j.CategoryName == "Labs")//Lab Grade
                    {
                        lb.grade += j.Grade;
                        lb.numOfGrades++;
                    }
                }

                //Find the weight of each category
                foreach(Category c in Categories)
                {
                    if(c.Name == "Exams")
                    {
                        exm.gradeWeight = c.Percentage;
                    }
                    else if(c.Name == "Homework")
                    {
                        hwk.gradeWeight = c.Percentage;
                    }
                    else if(c.Name == "Quizzes")
                    {
                        qz.gradeWeight = c.Percentage;
                    }
                    else if(c.Name == "Labs")
                    {
                        lb.gradeWeight = c.Percentage;
                    }
                }

                //Calculate the grades with the weight
                //Exam
                exm.grade = (exm.grade/exm.numOfGrades) * exm.gradeWeight;
                //Homework
                hwk.grade = (hwk.grade/ hwk.numOfGrades) * hwk.gradeWeight;
                //Quiz
                qz.grade = (qz.grade/exm.numOfGrades) * qz.gradeWeight;
                //Labs
                lb.grade = (lb.grade / lb.numOfGrades) * lb.gradeWeight;

                //Add to overall grade
                overallGrade = exm.grade + hwk.grade + qz.grade + lb.grade;
            }
            else //Can't be used
            {
                overallGrade = -1;
            }

            return overallGrade;
        }

        //****************************************************************
        //Name: ToString()
        //Purpose: Displays descriptive text of course name, assignment, category, submission
        //Input Type: None
        //Output Type: None
        //****************************************************************
        public override string ToString()
        {
            return "CourseName: " + CourseName + " Assignment: " + Assignments + " Category: " + Categories + " Submission: " + Submissions;
        }
        #endregion

        #region Properties
        [DataMember]
        public string CourseName { get { return m_CourseName; } set { m_CourseName = value; } }

        public List<Category> Categories { get { return m_Categories; } set { m_Categories = value; } }

        public List<Assignment> Assignments { get { return m_Assignments; } set { m_Assignments = value; } }

        public List<Submission> Submissions { get { return m_Submissions; } set { m_Submissions = value; } }
        #endregion
    }
}
