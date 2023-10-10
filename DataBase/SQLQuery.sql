USE MASTER
GO
IF EXISTS ( SELECT * FROM SYS.DATABASES WHERE NAME = 'QuanlySV')
    DROP DATABASE QuanlySV
GO

CREATE DATABASE QuanlySV
GO

USE QuanlySV
GO
IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE NAME = 'Lop')
    DROP TABLE Lop
GO
CREATE TABLE Lop
(
    MaLop char(3) not null primary key,
    TenLop nvarchar(30) not null
)

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE NAME = 'SinhVien')
    DROP TABLE SinhVien
GO
CREATE TABLE SinhVien
(
    MaSV varchar(6) not null primary key,
    HotenSV nvarchar(40),
    NgaySinh datetime,
    MaLop char(3)
)

ALTER TABLE SinhVien
    ADD CONSTRAINT FK_SinhVien_Lop FOREIGN KEY(MaLop) REFERENCES Lop(MaLop)

SET DATEFORMAT DMY

INSERT INTO Lop(MaLop,TenLop) VALUES('IT','Cong Nghe Thong Tin')
INSERT INTO Lop(MaLop,TenLop) VALUES('KT','Ke Toan')

INSERT INTO SinhVien(MaSV,HotenSV,NgaySinh,MaLop) VALUES('SV0001','Bui Van Toan','23/09/2003','IT')
INSERT INTO SinhVien(MaSV,HotenSV,NgaySinh,MaLop) VALUES('SV0002','Van Si Thinh','17/09/2003','KT')
INSERT INTO SinhVien(MaSV,HotenSV,NgaySinh,MaLop) VALUES('SV0003','Le Hoan Khang','04/4/2003','IT')
INSERT INTO SinhVien(MaSV,HotenSV,NgaySinh,MaLop) VALUES('SV0004','Le Van Tai','11/8/2003','KT')

select * from Lop
select * from SinhVien