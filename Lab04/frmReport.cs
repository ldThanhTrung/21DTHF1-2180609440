using Lab04.Models;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            StudentDBContext context = new StudentDBContext();
            List<Student> students = context.Students.ToList();
            List<StudentReports> studentReports = new List<StudentReports>();
            foreach (Student student in students)
            {
                StudentReports temp = new StudentReports();
                temp.StudentID = student.StudentID;
                temp.FullName = student.FullName;
                temp.AverageScore = student.AverageScore;
                temp.FacultyName = student.Faculty.FacultyName;
                studentReports.Add(temp);
            }

            this.reportViewer1.LocalReport.ReportPath = "C:\\Users\\thanh trung\\OneDrive\\Desktop\\Lab\\Lab04\\rptStudentReport.rdlc";
            var reportDataSource = new ReportDataSource("StudentDataSet", studentReports);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();
        }
    }
}
