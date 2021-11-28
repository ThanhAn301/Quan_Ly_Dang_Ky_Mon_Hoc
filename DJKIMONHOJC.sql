USE master;
IF OBJECT_ID('[dbo].[QLDKMH]', 'U') IS NOT NULL
ALTER DATABASE [QLDKMH] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [QLDKMH] ;
GO


-- Create a new database called 'QLDKMH'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'QLDKMH'
)
CREATE DATABASE QLDKMH
GO

USE QLDKMH;

-- Drop a table called 'Participant' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Participant]', 'U') IS NOT NULL
DROP TABLE [dbo].[Participant]
GO
-- Drop a table called 'Teacher' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Teacher]', 'U') IS NOT NULL
DROP TABLE [dbo].[Teacher]
GO
-- Drop a table called 'Teacher' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Student]', 'U') IS NOT NULL
DROP TABLE [dbo].[Student]
GO
-- Drop a table called 'ParticipantPhoneNumber' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ParticipantPhoneNumber]', 'U') IS NOT NULL
DROP TABLE [dbo].[ParticipantPhoneNumber]
GO
-- Drop a table called 'ParticipantEmail' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ParticipantEmail]', 'U') IS NOT NULL
DROP TABLE [dbo].[ParticipantEmail]
GO

-- Drop a table called 'Dependent' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Dependent]', 'U') IS NOT NULL
DROP TABLE [dbo].[Dependent]
GO
-- Drop a table called 'Study' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Study]', 'U') IS NOT NULL
DROP TABLE [dbo].[Study]
GO

-- Drop a table called 'Department' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Department]', 'U') IS NOT NULL
DROP TABLE [dbo].[Department]
GO

-- Drop a table called 'DepartmentAdress' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[DepartmentAdress]', 'U') IS NOT NULL
DROP TABLE [dbo].[DepartmentAdress]
GO
-- Drop a table called 'Class' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Class]', 'U') IS NOT NULL
DROP TABLE [dbo].[Class]
GO
-- Drop a table called 'Subject' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Subject]', 'U') IS NOT NULL
DROP TABLE [dbo].[Subject]
GO


-- TẠO BẢNG NGƯỜI THAM GIA --
IF OBJECT_ID('dbo.Participant') is NULL
BEGIN
    CREATE TABLE Participant(
        ID VARCHAR(11) NOT NULL,
        ParticipantName VARCHAR(30) NOT NULL,
        Gender VARCHAR(6) NOT NULL,
        Username VARCHAR(20) NOT NULL,
        Password_Participant VARCHAR(30) NOT NULL CHECK(len(Password_Participant)>=8),
        Adress VARCHAR(50),
        DepartmentName VARCHAR(30) NOT NULL,
        PRIMARY KEY(ID)
    );
END

-- TẠO BẢNG GIẢNG VIÊN --
IF OBJECT_ID('dbo.Teacher') is NULL
BEGIN
    CREATE TABLE Teacher(
        TeacherID VARCHAR(11) NOT NULL,
        Degree VARCHAR(10) NOT NULL,
        PRIMARY KEY(TeacherID)
    );
END

-- TẠO BẢNG SINH VIÊN --
IF OBJECT_ID('dbo.Student') is NULL
BEGIN
    CREATE TABLE Student(
        StudentID VARCHAR(11) NOT NULL,
        Credits Int NOT NULL CHECK(Credits>=0),
        StudyStatus VARCHAR(20) NOT NULL,
        PRIMARY KEY(StudentID)
    );
END

-- TẠO BẢNG SDT NGƯỜI THAM GIA --
IF OBJECT_ID('dbo.ParticipantPhoneNumber') is NULL
BEGIN
    CREATE TABLE ParticipantPhoneNumber(
        PhoneNumber VARCHAR(11) NOT NULL,
        ID VARCHAR(11) NOT NULL,
        PRIMARY KEY(PhoneNumber, ID)
    )
END

-- TẠO BẢNG EMAIL NGƯỜI THAM GIA --
IF OBJECT_ID('dbo.ParticipantEmail') is NULL
BEGIN
    CREATE TABLE ParticipantEmail(
        Email VARCHAR(30) NOT NULL,
        ID VARCHAR(11) NOT NULL,
        PRIMARY KEY(Email, ID)
    );
END

-- TẠO BẢNG NGƯỜI PHỤ THUỘC --
IF OBJECT_ID('dbo.Dependent') is NULL
BEGIN
    CREATE TABLE Dependent(
        StudentID VARCHAR(11) NOT NULL,
        DependentName VARCHAR(30) NOT NULL,
        PhoneNumber VARCHAR(11) NOT NULL,
        Relationship VARCHAR(30) NOT NULL,
        PRIMARY KEY(StudentID, DependentName)
    );
END

-- TẠO BẢNG CHO HỌC --
IF OBJECT_ID('dbo.Study') is NULL
BEGIN
    CREATE TABLE Study(
        StudentID VARCHAR(11) NOT NULL,
        ClassName VARCHAR(10) NOT NULL,
        PRIMARY KEY(StudentID, ClassName)
    );
END

-- TẠO BẢNG CHO LỚP HỌC --
IF OBJECT_ID('dbo.Class') is NULL
BEGIN
    CREATE TABLE Class(
        ClassName VARCHAR(10) NOT NULL,
        ClassRoom VARCHAR(10) NOT NULL,
        TeacherID VARCHAR(11) NOT NULL,
        SubjectID VARCHAR(10) NOT NULL,
        DayInWeek VARCHAR(10) NOT NULL,
		StartWeek SMALLINT NOT NULL,
		EndWeek	SMALLINT	NOT NULL,
		StartTimeInDay	TIME NOT NULL,
		EndTimeInDay	TIME NOT NULL,
        MaximumStudents INT NOT NULL CHECK(MaximumStudents>0),  
        
        PRIMARY KEY(ClassName),
		CONSTRAINT FormatTime CHECK(EndWeek >= StartWeek and StartWeek >= 0 and EndWeek <= 53 and StartTimeInDay <= EndTimeInDay)
    );
END

-- TẠO BẢNG CHO MÔN HỌC --
IF OBJECT_ID('dbo.Subject') is NULL
BEGIN
    CREATE TABLE Subject(
        SubjectName VARCHAR(40) NOT NULL,
        SubjectID VARCHAR(10) NOT NULL,
        Credits int NOT NULL CHECK(Credits>=0),
        Semester VARCHAR(40) NOT NULL,
        DepartmentName VARCHAR(30) NOT NULL,
        PreviousSubjectID VARCHAR(10) NULL,
        PRIMARY KEY(SubjectID)
    );
END

-- TẠO BẢNG CHO KHOA --
IF OBJECT_ID('dbo.Department') is NULL
BEGIN
    CREATE TABLE Department(
        PhoneNumber VARCHAR(20) NOT NULL,
        Email VARCHAR(40) NOT NULL,
        Fax VARCHAR(40) NOT NULL,
        DepartmentName VARCHAR(30) NOT NULL,
        PRIMARY KEY(DepartmentName)
    );
END

-- TẠO BẢNG CHO ĐỊA CHỈ KHOA --
IF OBJECT_ID('dbo.DepartmentAdress') is NULL
BEGIN
    CREATE TABLE DepartmentAdress(
        DepartmentName VARCHAR(30) NOT NULL,
        DepartmentAdress VARCHAR(40) NOT NULL,        
        PRIMARY KEY(DepartmentName, DepartmentAdress)
    );
END


-- Tạo foreign key cho Người tham gia --
ALTER TABLE Participant
    ADD CONSTRAINT FK_Participant_Department FOREIGN KEY (DepartmentName) REFERENCES Department(DepartmentName)
    ON DELETE CASCADE
    ON UPDATE CASCADE
;

-- Tạo foreign key cho Giảng Viên --
ALTER TABLE Teacher
    ADD CONSTRAINT FK_Teacher_Participant FOREIGN KEY (TeacherID) REFERENCES Participant(ID)
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
 
-- Tạo foreign key cho SĐT người tham gia --
ALTER TABLE ParticipantPhoneNumber
    ADD CONSTRAINT FK_ParticipantPhoneNumber_Participant FOREIGN KEY (ID) REFERENCES Participant(ID)
    ON DELETE CASCADE
    ON UPDATE CASCADE
;

-- Tạo foreign key cho Email người tham gia -- 
ALTER TABLE ParticipantEmail
    ADD CONSTRAINT FK_ParticipantEmail_Participant FOREIGN KEY (ID) REFERENCES Participant(ID)
    ON DELETE CASCADE
    ON UPDATE CASCADE
;

-- Tạo foreign key cho Sinh viên --
ALTER TABLE Student
    ADD CONSTRAINT FK_Student_Participant FOREIGN KEY (StudentID) REFERENCES Participant(ID)
    ON DELETE CASCADE
    ON UPDATE CASCADE
;

-- Tạo foreign key cho Địa chỉ khoa --
ALTER TABLE DepartmentAdress
    ADD CONSTRAINT FK_DepartmentAddress_Department FOREIGN KEY (DepartmentName) REFERENCES Department(DepartmentName)
    ON DELETE CASCADE
    ON UPDATE CASCADE
;

-- Tạo foreign key cho Lớp học --
ALTER TABLE Class
    ADD 
        CONSTRAINT FK_Class_Teacher FOREIGN KEY (TeacherID) REFERENCES Teacher(TeacherID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
        CONSTRAINT FK_Class_Subject FOREIGN KEY (SubjectID) REFERENCES Subject(SubjectID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
;

-- Tạo foreign key cho Học --
ALTER TABLE Study
    ADD
        CONSTRAINT FK_Study_Student FOREIGN KEY (StudentID) REFERENCES Student(StudentID)
        on delete no ACTION
        on update no ACTION,
        CONSTRAINT FK_Study_Class FOREIGN KEY (ClassName) REFERENCES Class(ClassName)
        on DELETE CASCADE
        on UPDATE CASCADE
;

-- Tạo foreign key cho Người phụ thuộc --
ALTER TABLE Dependent
    ADD
        CONSTRAINT FK_Dependent_Student FOREIGN KEY (StudentID) REFERENCES Student(StudentID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
;

-- Tạo foreign key cho Môn Học --
ALTER TABLE Subject
    ADD
        CONSTRAINT FK_Subject_Department FOREIGN KEY (DepartmentName) REFERENCES Department(DepartmentName)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
        CONSTRAINT FK_PreviousSubject_Subject FOREIGN KEY (PreviousSubjectID) REFERENCES Subject(SubjectID)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
;

GO
-- Tạo trigger ràng buộc toàn phần của Participant
CREATE TRIGGER trigger_TotalPartipation_Participant
ON Participant
FOR DELETE, INSERT, UPDATE
AS
BEGIN
    IF exists((SELECT Distinct Participant.DepartmentName From Participant) 
        EXCEPT (SELECT Department.DepartmentName FROM Department))
    BEGIN
		PRINT N'Mỗi nhân viên phải thuộc vào một phòng ban!!!'
		ROLLBACK TRAN
	END
END
-- Tạo trigger ràng buộc toàn phần của Sụbject 

GO
CREATE TRIGGER trigger_TotalPartipation_Subject
ON Subject
FOR DELETE, INSERT, UPDATE
AS
BEGIN
    IF exists((SELECT Distinct Subject.DepartmentName From Subject) 
        EXCEPT (SELECT Department.DepartmentName FROM Department))
    BEGIN
		PRINT N'Môi môn học phải nằm trong một khoa nằm đó!!!'
		ROLLBACK TRAN
	END
END
GO

-- Tạo trigger ràng buộc toàn phần của Class

CREATE TRIGGER trigger_TotalPartipation_Class
ON Class
FOR DELETE, INSERT, UPDATE
AS
BEGIN
    IF exists(
        (SELECT Distinct Class.TeacherID From Class) 
        EXCEPT 
        (SELECT Teacher.TeacherID FROM Teacher)
        ) 
        OR (
            exists(
                (SELECT Distinct Class.SubjectID From Class) 
                EXCEPT 
                (SELECT Subject.SubjectID FROM Subject)
            )
        )
    BEGIN
		PRINT N'Mỗi lớp học phải thuộc vào một môn học, mỗi lớp học cũng phải có giảng viên dạy!!!'
		ROLLBACK TRAN
	END
END
GO

-- Thêm dữ liệu vào
insert into Department(PhoneNumber,Email,Fax,DepartmentName)
values
('0123456789','khmt@gmail.com','1111','KHMT'),
('0123456789','xd@gmail.com','2222','XD')

GO


INSERT INTO Participant
(ID, ParticipantName, Gender, Username, Password_Participant, Adress, DepartmentName)
VALUES
('230095', 'Le Duong Anh', 'Female', 'DuongAnh', 'Zxcvbnm1234', 'Dong Nai', 'KHMT'),
('232144', 'Do Van Anh', 'Male', 'Anh123', 'Qwerty123', 'Ho Chi Minh City', 'KHMT'),
('265907', 'Tran Thanh Binh', 'Male', 'BinhKTMT', 'Binhdeptrai260', 'Ho Chi Minh City', 'KHMT'),
('292103', 'Nguyen Hung Chi', 'Male', 'Chi192', '12345678', 'Tien Giang', 'XD'),
('299778', 'Tran Van Cuong', 'Male', 'CuongBK', 'Cuong2711', 'Nghe An', 'XD'),
('300405', 'Tran Thi Kieu Dung', 'Female', 'Dungxinh', '13101987', 'Binh Phuoc', 'XD'),
('300877', 'Nguyen Thuy Duong', 'Female', 'Duong275', 'IloveTARA', 'Ho Chi Minh City', 'KHMT'),
('301122', 'Le Trang Duong', 'Female', 'Cuteabcd', 'TrangDuongxinhdep', 'Ho Chi Minh City', 'XD'),
('302045', 'Tran Thi Em', 'Female', 'Embe1997', 'Embedethuong', 'Ho Chi Minh City', 'KHMT'),
('309876', 'Nguyen Van Giau', 'Male', 'Giausang', '19091967', 'Dong Nai', 'XD'),
('311568', 'Le Thanh Hai', 'Male', 'HaiThanh', 'Haiquayxe', 'Ho Chi Minh City', 'KHMT'),
('331094', 'Dao Thi Thuy Tien', 'Female', 'Tiendao', 'Toctienidol', 'Ho Chi Minh City', 'XD'),
('1912533', 'Le Thanh An', 'Male', 'ThanhAn', 'Abc12345', 'Ho Chi Minh City', 'KHMT'),
('1912887', 'Tran Hong Anh', 'Female', 'Anhcute9', '1s2s2p3s', 'Ho Chi Minh City', 'XD'),
('1912999', 'Phan Hoang Bao', 'Male', 'Baobig', 'BlackaForever', 'Da Nang', 'XD'),
('1913012', 'Nguyen Thi Binh', 'Female', 'Cutexyzw', 'BigBangidol', 'Dong Nai', 'KHMT');

GO

INSERT INTO ParticipantEmail (Email, ID)
VALUES
('Anh.le@gmail.com', '230095'),
('Anh.do@gmail.com', '232144'),
('Binh.tran@gmail.com', '265907'),
('TruongKhoaKHMT@gmail.com', '265907'),
('Chi.nguyen@gmail.com', '292103'),
('TruongKhoaXD@gmail.com', '292103'),
('Cuong.tran@gmail.com', '299778'),
('Dung.tran@gmail.com', '300405'),
('Duong.nguyen@gmail.com', '300877'),
('Duong.le@gmail.com', '301122'),
('Em.tran@gmail.com', '302045'),
('Giau.nguyen@gmail.com', '309876'),
('Hai.le@gmail.com', '311568'),
('Tien.dao@gmail.com', '331094'),
('An.le@gmail.com', '1912533'),
('Anh.tran@gmail.com', '1912887'),
('Bao.phan@gmail.com', '1912999'),
('Binh.nguyen@gmail.com', '1913012');
GO
INSERT INTO ParticipantPhoneNumber(PhoneNumber, ID)
VALUES
('0123456789', '230095'),
('0917348562', '230095'),
('0987564213', '232144'),
('0965732418', '265907'),
('0239685417', '292103'),
('0124356987', '299778'),
('0368954712', '299778'),
('0924381576', '300405'),
('0285947316', '300877'),
('0286974315', '301122'),
('0978642513', '302045'),
('0134596872', '302045'),
('0913547862', '309876'),
('0914327865', '311568'),
('0915826437', '331094'),
('0135426789', '1912533'),
('0916852473', '1912887'),
('0926381574', '1912999'),
('0983124765', '1913012');

GO

INSERT INTO Teacher(TeacherID, Degree)
VALUES
('230095', 'Ph.D'),
('232144', 'Professor'),
('265907', 'Master'),
('292103', 'Ph.D'),
('299778', 'Master'),
('300405', 'Professor'),
('300877', 'Ph.D'),
('301122', 'Ph.D'),
('302045', 'Professor'),
('309876', 'Master'),('311568', 'Professor'),
('331094', 'Ph.D');

GO

INSERT INTO Student
(StudentID, Credits, StudyStatus)
VALUES
('1912533', 72, 'Second Year'),
('1912887', 75, 'Second Year'),
('1912999', 60, 'First Year'),
('1913012', 80, 'Second Year');
GO

insert into Subject(SubjectName,SubjectID,Credits,Semester,DepartmentName,PreviousSubjectID)
values
('Giai tich 1','MT1003',4,1,'KHMT',null),
('Vat ly 1','PH1003',4,1,'KHMT',null),
('Nhap mon dien toan','CO1005',4,1,'KHMT',null),
('He thong so','CO1023',3,1,'KHMT','CO1005'),
('Giai tich 2','MT1005',4,1,'KHMT','MT1003'),
('Ky thuat lap trinh','CO1027',4,1,'KHMT',null),
('Kien truc may tinh','CO2007',4,1,'XD','CO1023'),
('Cau truc du lieu va giai thuat','CO2003',4,1,'XD',null)

GO

insert into Class(ClassName,ClassRoom,TeacherID,SubjectID,DayInWeek,StartWeek,EndWeek,StartTimeInDay,EndTimeInDay,MaximumStudents)
values
('L01-GT1','101','230095','MT1003','2',3,9,'7:00','10:00',3),
('L02-GT1','201','232144','MT1003','3',3,9,'14:00','16:00',5),
('L03-GT1','301','265907','MT1003','2',3,9,'12:00','15:00',4),
('L01-VL1','402','292103','PH1003','3',2,8,'7:00','9:00',2),
('L02-VL1','504','299778','PH1003','2',2,8,'9:00','12:00',5),
('L01-GT2','204','300405','MT1005','4',2,8,'8:00','10:00',1),
('L01-NMDT','102','300877','CO1005','5',3,7,'15:00','17:00',1),
('L01-HTS','402','301122','CO1023','6',3,7,'16:00','18:00',2),
('L01-KTLT','304','302045','CO1027','4',3,7,'9:00','11:00',5),
('L02-KTLT','304','302045','CO1027','5',3,7,'9:00','11:00',5),
('L01-KTMT','304','302045','CO2007','2',3,7,'9:00','11:00',5),
('L01-CTDL','304','302045','CO2003','6',3,7,'7:00','9:00',5),
('L02-CTDL','304','302045','CO2003','5',3,7,'12:00','14:00',5)
GO

SELECT *
FROM Class




SELECT *
FROM Study
SELECT *
FROM Student
GO
-- 1.Hệ thống cho phép sinh viên chọn môn học muốn đăng ký (thêm vào danh sách đăng ký tạm thời).

CREATE PROC DKYHOC
@studentID varchar(11),
@classname varchar(10)
AS
BEGIN
    INSERT INTO STUDY(StudentID,ClassName) 
    VALUES (@studentID,@classname)
END
GO

-- Đếm tỉ lệ sinh viên đã đăng ký trên tổng số sinh viên có thể ở mỗi lớp học
create function [dbo].[numStudent]
( @classname varchar(10) )
returns varchar(10)
AS
BEGIN
    declare @num int
    select @num = count(*) 
    from Study
    where ClassName = @classname

    declare @max int
    select @max = MaximumStudents from Class where ClassName = @classname
    return (CAST(@num as varchar) + ' / ' + CAST(@max as varchar))
END
GO


--DECLARE @NumberStudentInClass table(
--        NumberStudent VARCHAR(10)
--    )   

--insert into @NumberStudentInClass
--SELECT dbo.numStudent('L01a') 

--select* from @NumberStudentInClass




-- view Class, xem thông tin lớp học
CREATE FUNCTION [dbo].[VIEWCLASS]
(@subjectID varchar(40))
RETURNS TABLE
AS
RETURN(
    SELECT ClassName,ClassRoom,ParticipantName AS TeacherName,SubjectID,DayInWeek,StartWeek,EndWeek,StartTimeInDay,EndTimeInDay
    FROM Class, Participant where SubjectID = @subjectID AND TeacherID = ID
	)
GO


-- 5.Hệ thống cho phép tìm kiếm môn học thông qua tên hoặc ID. (đến từ sinh viên)
CREATE FUNCTION [dbo].[SearchSubject] 
(@Name varchar(40))
RETURNS TABLE
AS
RETURN(
	SELECT * FROM Subject where SubjectName like '%'+@Name+'%' or SubjectID like '%'+@Name+'%'
)
GO
--6. Hệ thống cho phép hủy lớp học của môn học đã đăng ký nếu vẫn còn trong thời gian đăng ký. (đến từ sinh viên) 
CREATE PROCEDURE DeleteSuject @SelectStudentId VARCHAR(11), @SelectClassName nvarchar(30)
AS
delete from Study	
where Study.ClassName = @selectClassName and Study.StudentID = @SelectStudentId
GO

--7. Hệ thống cho phép sinh viên đổi lớp học của môn học đã đăng ký (nếu môn học có nhiều hơn 1 lớp). (đến từ sinh viên) 
CREATE PROCEDURE ChangeClassName @StudentId varchar(11), @ClassNameBefore varchar(10), @ClassNameAfter varchar(10)
AS
update Study		
set ClassName = @ClassNameAfter
where Study.ClassName = @ClassNameBefore AND Study.StudentID = @StudentId
GO

drop proc ChangeClassName

--Hủy bỏ môn học khi sĩ số ít hơn hoặc bằng một nửa số lượng tối đa mà lớp học có thể chứa sau khi kết thúc thời gian đăng ký

create PROCEDURE CancleTheSubjectAfterTimeOut
AS
    DECLARE @NumberStudentInClass table(
        ClassName VARCHAR(10) not null,
        NumberStudent INT
    ) 

    insert into @NumberStudentInClass
    select ClassName, COUNT(StudentID) as NumberStudent
    from Study
    GROUP BY ClassName

    delete from Class
    WHERE Class.ClassName IN (
                            select A.ClassName
                            from @NumberStudentInClass A
                            WHERE A.NumberStudent <= ((select B.MaximumStudents from Class B WHERE B.ClassName = A.ClassName)/2)
    )
GO

--- hàm kiểm tra xem thử 2 lớp có trùng lịch hay không?
create FUNCTION FalseDayTimeStudy(
	@SelectClassName1 varchar(10)
	,@SelectClassName2 varchar(10)
)
RETURNS BIT
AS
BEGIN
    declare @valuesBIT BIT; -- trả về kết quả trùng lịch hay không?
    declare @DayInWeek1 varchar(10), @StartWeek1 SMALLINT, @EndWeek1 SMALLINT, @StartTimeInDay1 time, @EndTimeInDay1 time;
    declare @DayInWeek2 varchar(10), @StartWeek2 SMALLINT, @EndWeek2 SMALLINT, @StartTimeInDay2 time, @EndTimeInDay2 time;

    -- Lấy ra ngày trong tuần, tuần bắt đầu, tuần kết thúc, giờ bắt đầu trong ngày, giờ kết thúc trong ngày
    set @DayInWeek1 = (select Class.DayInWeek from Class where Class.ClassName = @SelectClassName1)
    set @DayInWeek2 = (select Class.DayInWeek from Class where Class.ClassName = @SelectClassName2)
        set @StartWeek1 = (select Class.StartWeek from Class where Class.ClassName = @SelectClassName1)
        set @StartWeek2 = (select Class.StartWeek from Class where Class.ClassName = @SelectClassName2)
        set @EndWeek1 = (select Class.EndWeek from Class where Class.ClassName = @SelectClassName1)
        set @EndWeek2 = (select Class.EndWeek from Class where Class.ClassName = @SelectClassName2)
            set @StartTimeInDay1 = (select Class.StartTimeInDay from Class where Class.ClassName = @SelectClassName1)
            set @StartTimeInDay2 = (select Class.StartTimeInDay from Class where Class.ClassName = @SelectClassName2)
            set @EndTimeInDay1 = (select Class.EndTimeInDay from Class where Class.ClassName = @SelectClassName1)
            set @EndTimeInDay2 = (select Class.EndTimeInDay from Class where Class.ClassName = @SelectClassName2)
    -- Kiểm tra xem có bị đụng lịch hay không
    IF (@DayInWeek1 = @DayInWeek2 
        and ((@StartWeek1 >= @StartWeek2 and @StartWeek1 <= @EndWeek2) 
            OR (@StartWeek2 >= @StartWeek1 and @StartWeek2 <= @EndWeek1))
        and ((@StartTimeInDay1 >= @StartTimeInDay2 and @StartTimeInDay1 <= @EndTimeInDay2)
            OR (@StartTimeInDay2 >= @StartTimeInDay1 and @StartTimeInDay2 <= @EndTimeInDay1))
        )
    BEGIN
        SET @valuesBIT = 1; --trùng lịch học
    END
    ELSE BEGIN
            SET @valuesBIT = NULL; --KO trùng lịch học
        END
    RETURN @valuesBIT;

END;

GO


--- Cài đặt trigger cho Study gồm các trigger con sau đây.
--- Hệ thống không cho phép đăng ký 2 môn học trùng lịch học
-- Sinh viên chỉ được đăng ký tối đa 24 tín chỉ trong 1 kỳ --
-- Sinh viên không được đăng ký nhiều lớp học trong cùng môn học.
-- Sinh viên chỉ được đăng ký tối đa 24 tín chỉ trong 1 kỳ --
--- Khi đăng ký quá số lượng sinh viên tối đa của mỗi lớp thì báo lỗi
CREATE TRIGGER CheckCreditsForEachStudent
ON Study
FOR INSERT, UPDATE
AS
BEGIN
    --- Hệ thống không cho phép đăng ký 2 môn học trùng lịch học

    -- Tạo bảng copy của inserted thêm trường identity rồi ghép lại với Study 
    -- để lấy ra từng cặp tên lớp học có môn lớp giống với tên môn học của 
    --  inserted classname để so sánh xem thử có bị trùng lịch học hay không
    DECLARE @copyStudyinserted table(
        id Int IDENTITY,
        StudentId varchar(11),
        ClassName varchar(10)
    )
    DECLARE @ClassTableToCheck table(
        id Int IDENTITY,
        Class1 varchar(10),
        Class2 varchar(10)

    )
    insert into @copyStudyinserted
    select *
    from inserted

    insert into @ClassTableToCheck(Class1, Class2) 
    SELECT Study.ClassName as Class1, C.ClassName as Class2
    from Study, @copyStudyinserted C
    WHERE Study.StudentID = C.StudentId AND Study.ClassName != C.ClassName


    --- Lấy ra từng cặp tên lớp học
    DECLARE
    @counter INT = 1,
    @max INT = 0,
    @class1 varchar(10),
    @class2 varchar(10)

    -- loop qua các cặp
    SELECT @max = COUNT(id) FROM @ClassTableToCheck
    WHILE @counter <= @max
    BEGIN
        SET @class1 = (SELECT Class1 FROM @ClassTableToCheck C WHERE C.id = @counter)
        SET @class2 = (SELECT Class2 FROM @ClassTableToCheck C WHERE C.id = @counter)
        if dbo.FalseDayTimeStudy(@class1, @class2) = 1
        BEGIN
            print N'Có 2 lớp bị đụng lịch rồi!!!!'
            ROLLBACK TRAN
        END
        SET @counter = @counter + 1
    END

    -- Sinh viên không được đăng ký nhiều lớp học trong cùng môn học.
    IF (
        SELECT COUNT(Study.ClassName)
        FROM Study, Class
        WHERE Study.ClassName = Class.ClassName 
                AND Class.SubjectID = (SELECT SubjectID FROM inserted A, Class B WHERE A.ClassName = B.ClassName)
                AND Study.StudentID = (SELECT StudentID FROM inserted)
        GROUP BY StudentID, SubjectID
    )>1
    BEGIN
        print N'Môi sinh viên chỉ đăng ký được một lớp học ở mỗi môn học'
        ROLLBACK TRAN
    END
    -- Sinh viên chỉ được đăng ký tối đa 24 tín chỉ trong 1 kỳ --
    IF (
        SELECT SUM(Subject.Credits) as NumberCredits
        FROM Study, Class, Subject
        WHERE Study.ClassName = Class.ClassName AND Class.SubjectID = Subject.SubjectID AND  StudentID = (SELECT StudentID FROM inserted)
        GROUP BY Study.StudentID)>24
    BEGIN
        print N'Mỗi sinh viên chỉ đăng ký tối đa 24 tín chỉ'
        ROLLBACK TRAN
    END

    --- Khi đăng ký quá số lượng sinh viên tối đa của mỗi lớp thì báo lỗi
    IF (
        SELECT COUNT(*)
        FROM Study
        WHERE ClassName = (SELECT ClassName FROM inserted)
    ) > (
        SELECT MaximumStudents
        FROM Class
        WHERE ClassName = (SELECT ClassName FROM inserted)
    )
    BEGIN
        print N'Đã đăng ký quá số lượng sinh tối đa!!'
        ROLLBACK TRAN
    END
END