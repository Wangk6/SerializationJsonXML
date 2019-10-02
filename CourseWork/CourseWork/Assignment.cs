//******************************************************
// File: Assignment.cs
//
// Purpose: Contains the class definition for Assignments.
//          Assignment will hold all assignment information 
//          such as the name of the assignement, description
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
    public class Assignment
    {
        #region Private Member Variables
        private string m_Name;
        private string m_Description;
        private string m_CategoryName;
        #endregion

        #region Methods
        public Assignment()
        {
            m_Name = "Homework 2";
            m_Description = "Create the submission class. Add serialization to all classes.";
            m_CategoryName = "Homework";
        }

        public override string ToString()
        {
            return "Name: " + Name + " Description: " + Description + " Category Name:" + CategoryName;
        }
        #endregion

        #region Properties
        [DataMember(Name="name")]
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        [DataMember(Name = "description")]
        public string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        [DataMember(Name="categoryName")]
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
        #endregion


    }
}
