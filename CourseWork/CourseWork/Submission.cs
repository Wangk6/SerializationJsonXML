//******************************************************
// File: Submission.cs
//
// Purpose: Contains the class definition for Submissions.
//          Submission will hold all submission information 
//          such as the name of the assignement, grade
//          and the category name associated with it
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
    [DataContract]
    public class Submission
    {
        #region Private Member Variables
        private string m_CategoryName;
        private string m_AssignmentName;
        private double m_Grade;
        #endregion

        #region Methods
        public Submission()
        {
            m_CategoryName = "Homework";
            m_AssignmentName = "Homework 4";
            m_Grade = 100;
        }

        public override string ToString()
        {
            return "Category Name: " + CategoryName + " Assignment Name:" + AssignmentName + " Grade:" + Grade;
        }
        #endregion

        #region Properties
        [DataMember(Name ="categoryName")]
        public string CategoryName
        {
            get
            {
                return m_CategoryName;
            }
            set
            {
                m_CategoryName = value;
            }
        }
        [DataMember(Name = "assignmentName")]
        public string AssignmentName
        {
            get
            {
                return m_AssignmentName;
            }
            set
            {
                m_AssignmentName = value;
            }
        }
        [DataMember(Name = "grade")]
        public double Grade
        {
            get
            {
                return m_Grade;
            }
            set
            {
                m_Grade = value;
            }
        }
        #endregion


    }
}
