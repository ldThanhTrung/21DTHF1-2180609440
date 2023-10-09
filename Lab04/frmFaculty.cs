using Lab04.Models;
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
    public partial class frmFaculty : Form
    {
        public frmFaculty()
        {
            InitializeComponent();
        }

        private void btFacultyExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BindGrid(List<Faculty> listFalcultys)
        {
            dgvFaculty.Rows.Clear();
            foreach
            (var item in listFalcultys)
            {
                int index = dgvFaculty.Rows.Add();
                dgvFaculty.Rows[index].Cells["FacultyID"].Value = item.FacultyID;
                dgvFaculty.Rows[index].Cells["FacultyName"].Value = item.FacultyName;
                dgvFaculty.Rows[index].Cells["TotalProfessor"].Value = item.TotalProfessor;
            }
        }

        private void frmFaculty_Load(object sender, EventArgs e)
        {
            try
            {
                StudentDBContext context = new StudentDBContext();
                List<Faculty> listFalcultys = context.Faculties.ToList();
                BindGrid(listFalcultys);
            }
            catch
            (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvFaculty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                txtFacultyID.Text = dgvFaculty.Rows[rowIndex].Cells["FacultyID"].Value.ToString();
                txtFacultyName.Text = dgvFaculty.Rows[rowIndex].Cells["FacultyName"].Value.ToString();
                if (dgvFaculty.Rows[rowIndex].Cells["TotalProfessor"].Value == null)
                {
                    txtTotalProfessor.Text = "0";
                }
                else
                {
                    txtTotalProfessor.Text = dgvFaculty.Rows[rowIndex].Cells["TotalProfessor"].Value.ToString();
                }
            }
        }

        private void btFacultyAdd_Click(object sender, EventArgs e)
        {
            var facultyID = txtFacultyID.Text;
            var facultyName = txtFacultyName.Text;
            var totalProfessor = txtTotalProfessor.Text;

            StudentDBContext context = new StudentDBContext();

            Faculty faculty = new Faculty()
            {
                FacultyName = facultyName,
                FacultyID = int.Parse(facultyID),
                TotalProfessor = int.Parse(totalProfessor)
            };

            context.Faculties.Add(faculty);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mã số khoa đã tồn tại !!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            BindGrid(context.Faculties.ToList());
        }

        private void btFacultyReplace_Click(object sender, EventArgs e)
        {
            StudentDBContext db = new StudentDBContext();
            int indexFacultyID = int.Parse(txtFacultyID.Text);
            var updateFaculty = db.Faculties.SingleOrDefault(c => c.FacultyID == indexFacultyID);
            if (updateFaculty == null)
            {
                MessageBox.Show("Không tồn tại sinh viên có MSSV {0}", txtFacultyID.Text);
                return;
            }
            updateFaculty.FacultyName = txtFacultyName.Text;
            updateFaculty.TotalProfessor = int.Parse(txtTotalProfessor.Text);

            db.SaveChanges();
            BindGrid(db.Faculties.ToList());
        }

        private void btFacultyDelete_Click(object sender, EventArgs e)
        {
            try
            {
                StudentDBContext faculty = new StudentDBContext();
                int facultyID = int.Parse(txtFacultyID.Text);
                Faculty selectedFaculty = faculty.Faculties.FirstOrDefault(s => s.FacultyID == facultyID);

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khoa này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {

                    faculty.Faculties.Remove(selectedFaculty);
                    faculty.SaveChanges();
                    MessageBox.Show("Xóa sinh viên thành công !!!");
                    List<Faculty> listFalcultys = faculty.Faculties.ToList();
                    BindGrid(listFalcultys);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
