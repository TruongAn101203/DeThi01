using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class frmSinhVien : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly ClassService classService = new ClassService();
        StudentService db = new StudentService();
        int CountRow;
        int OriginalRow;

        public frmSinhVien()
        {
            InitializeComponent();
        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            try
            {

                var listClasss = classService.GetAll();
                var listStudents = studentService.GetAll();
                FillClassCombobox(listClasss);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        private void FillClassCombobox(List<Lop> listclass)
        {
            this.cboLop.DataSource = listclass;
            this.cboLop.DisplayMember = "TenLop";
            this.cboLop.ValueMember = "MaLop";
        }

        public void BindGrid(List<SinhVien> listsv)
        {
            lvSinhVien.Items.Clear();
            foreach (SinhVien student in listsv)
            {
                ListViewItem item = new ListViewItem();
                item.Text = student.MaSV;
                item.SubItems.Add(new ListViewSubItem(item, student.HotenSV));
                item.SubItems.Add(new ListViewSubItem(item, student.NgaySinh.ToString()));
                item.SubItems.Add(new ListViewSubItem(item, student.Lop?.TenLop));
                lvSinhVien.Items.Add(item);
            }
        }
        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem item = lvSinhVien.SelectedItems[0];
                string maSV = item.Text;
                string hoTenSV = item.SubItems[1].Text;
                DateTime ngaySinh;
                string tenLop = item.SubItems[3].Text;
                txtMaSV.Text = maSV;
                txtHoTen.Text = hoTenSV;
                if (DateTime.TryParse(item.SubItems[2].Text.ToString(), out ngaySinh))
                {
                    dtNgaySinh.Value = ngaySinh;
                }
                cboLop.Text = tenLop;
            }
        }


        private void UpdateSaveandUnsaveButtonState()
        {
              btnLuu.Enabled = lvSinhVien.Items.Count >= OriginalRow || lvSinhVien.Items.Count < OriginalRow;
              btnKhongLuu.Enabled = lvSinhVien.Items.Count >= OriginalRow ||  lvSinhVien.Items.Count < OriginalRow;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem(txtMaSV.Text);
            item.SubItems.Add(txtHoTen.Text);
            item.SubItems.Add(dtNgaySinh.Value.ToString());
            item.SubItems.Add(cboLop.Text);
            lvSinhVien.Items.Add(item);
            txtMaSV.Text = null;
            txtHoTen.Text = null;
            dtNgaySinh.Value = DateTime.Now;
            cboLop.SelectedIndex = 0;
            CountRow = lvSinhVien.Items.Count;

            UpdateSaveandUnsaveButtonState();
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
           
            while (lvSinhVien.SelectedItems.Count > 0)
            {
                Console.WriteLine(lvSinhVien.SelectedItems[0].Index);
                lvSinhVien.Items.RemoveAt(lvSinhVien.SelectedItems[0].Index);
                txtMaSV.Text = null;
                txtHoTen.Text = null;
                dtNgaySinh.Value = DateTime.Now;
                cboLop.SelectedIndex = 0;
                CountRow = lvSinhVien.Items.Count;
                UpdateSaveandUnsaveButtonState();

            }
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
           
            List<SinhVien> newStudents = new List<SinhVien>();
            // Save the data from the ListView control to the database.
            foreach (ListViewItem item in lvSinhVien.Items)
            {
                SinhVien student = new SinhVien();
                student.MaSV = item.Text;
                student.HotenSV = item.SubItems[1].Text;
                DateTime ngaySinh;
                if (DateTime.TryParse(item.SubItems[2].Text.ToString(), out ngaySinh))
                {
                    dtNgaySinh.Value = ngaySinh;
                }
                student.NgaySinh = dtNgaySinh.Value;
                string maSV = item.Text.ToString();
                var maLop = studentService.FindByID(maSV)?.MaLop?.ToString();

                if (maLop != null)
                {
                    student.MaLop = maLop;
                }
             
                var tenLop = item.SubItems[3].Text;
                if (cboLop.Text == tenLop && tenLop == "Cong Nghe Thong Tin")
                {
                    student.MaLop = Convert.ToString("IT");
                }
                else
                {
                    student.MaLop = Convert.ToString("KT");
                }

                
                    newStudents.Add(student);
                
            }
            studentService.InsertUpdate(newStudents);
        }
    

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem lv = lvSinhVien.SelectedItems[0];
                lv.SubItems[1].Text = txtHoTen.Text;
                lv.SubItems[2].Text = dtNgaySinh.Value.ToString();
                lv.SubItems[3].Text = cboLop.Text;
                txtMaSV.Text = null;
                txtHoTen.Text = null;
                dtNgaySinh.Value = DateTime.Now;
                cboLop.SelectedIndex = 0;
                UpdateSaveandUnsaveButtonState();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
               "Ban co muon thoat ?",
               "Title",
            MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            
        }
    }
}
