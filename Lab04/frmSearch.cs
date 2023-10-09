using Lab04.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            txtResult.Enabled = false;
            try
            {
                StudentDBContext context = new StudentDBContext();
                List<Faculty> listFalcultys = context.Faculties.ToList();
                List<Student> listStudent = context.Students.ToList();
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
                txtResult.Text = dgvStudent.RowCount.ToString();
            }
            catch
            (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cmbFaculty.DataSource = listFalcultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }

        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.Rows.Clear();
            foreach
            (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells["MSSV"].Value = item.StudentID;
                dgvStudent.Rows[index].Cells["FullName"].Value = item.FullName;
                dgvStudent.Rows[index].Cells["Faculty"].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells["AverageScore"].Value = item.AverageScore;
            }
            txtResult.Text = dgvStudent.RowCount.ToString();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            StudentDBContext context = new StudentDBContext();
            List<Student> students = context.Students.ToList();
            List<Student> student = new List<Student>();

            foreach (var item in students)
            {
                if(checkFacultyID(0) == item.FacultyID)
                {
                    if (txtMSSV.Text == item.StudentID || txtFullName.Text == item.FullName)
                    {
                        student.Add(item);
                    }
                    if(txtMSSV.Text == "" && txtFullName.Text == "")
                    {
                        student.Add(item);
                    }
                }
            }
            BindGrid(student);
        }

        private int checkFacultyID(int a)
        {
            StudentDBContext context = new StudentDBContext();
            List<Faculty> listFalcultys = context.Faculties.ToList();
            foreach (var f in listFalcultys)
            {
                if (cmbFaculty.Text == f.FacultyName)
                {
                    a = f.FacultyID;
                }
            }
            return a;
        }
    }
}
