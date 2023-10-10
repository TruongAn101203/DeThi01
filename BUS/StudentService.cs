using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class StudentService
    {
        public List<SinhVien> GetAll()
        {
            StudentModel context = new StudentModel();
            return context.SinhViens.ToList();
        }

        public SinhVien FindByID(string MaSV)
        {
            StudentModel context = new StudentModel();
            return context.SinhViens.FirstOrDefault(p => p.MaSV == MaSV);
        }
        public void InsertUpdate(List<SinhVien> list)
        {
            StudentModel context = new StudentModel();
            foreach (var item in list)
            {
                var ExistingStudent = context.SinhViens.Find(item.MaSV);
                if (ExistingStudent != null)
                {
                    ExistingStudent.HotenSV = item.HotenSV;
                    ExistingStudent.NgaySinh = item.NgaySinh;
                    ExistingStudent.MaLop = item.MaLop;
                    context.SinhViens.AddOrUpdate(ExistingStudent);
                }
                else
                {
                    context.SinhViens.Add(item);
                }
            }
            context.SaveChanges();
        }


    }
}
