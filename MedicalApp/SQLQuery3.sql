-- 1. Create DB
IF DB_ID('MedicalDB') IS NULL
    CREATE DATABASE MedicalDB;
GO
USE MedicalDB;
GO

-- 2. Tables
IF OBJECT_ID('dbo.Appointments', 'U') IS NOT NULL DROP TABLE dbo.Appointments;
IF OBJECT_ID('dbo.Doctors', 'U') IS NOT NULL DROP TABLE dbo.Doctors;
IF OBJECT_ID('dbo.Patients', 'U') IS NOT NULL DROP TABLE dbo.Patients;
GO

CREATE TABLE dbo.Doctors (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Specialty VARCHAR(100) NOT NULL,
    Availability BIT NOT NULL DEFAULT(1)
);

CREATE TABLE dbo.Patients (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE dbo.Appointments (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    DoctorID INT NOT NULL,
    PatientID INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Notes VARCHAR(500) NULL,
    CONSTRAINT FK_Appointments_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID) ON DELETE CASCADE,
    CONSTRAINT FK_Appointments_Patients FOREIGN KEY (PatientID) REFERENCES dbo.Patients(PatientID) ON DELETE CASCADE
);

-- Useful indexes
CREATE INDEX IX_Appointments_Doctor_Date ON dbo.Appointments(DoctorID, AppointmentDate);
CREATE INDEX IX_Appointments_Patient ON dbo.Appointments(PatientID);

-- 3. Seed data
INSERT INTO dbo.Doctors (FullName, Specialty, Availability) VALUES
('Dr. Ama Mensah', 'Cardiology', 1),
('Dr. Kojo Owusu', 'Dermatology', 1),
('Dr. Efua Adjei', 'Pediatrics', 0),
('Dr. Yaw Boateng', 'Orthopedics', 1),
('Dr. Nana Kwame', 'Neurology', 1),
('Dr. Abena Sarpong', 'Gynecology', 0);

INSERT INTO dbo.Patients (FullName, Email) VALUES
('Kofi Asare', 'kofi.asare@example.com'),
('Akosua Nyarko', 'akosua.nyarko@example.com'),
('Yaw Mensah', 'yaw.mensah@example.com'),
('Ama Serwaa', 'ama.serwaa@example.com'),
('Kwabena Osei', 'kwabena.osei@example.com');

-- Sample appointments
INSERT INTO dbo.Appointments (DoctorID, PatientID, AppointmentDate, Notes) VALUES
(1, 1, DATEADD(DAY, 1, GETDATE()), 'Routine heart checkup'),
(2, 2, DATEADD(DAY, 2, GETDATE()), 'Skin allergy consultation'),
(5, 3, DATEADD(DAY, 3, GETDATE()), 'Neurological examination'),
(1, 4, DATEADD(DAY, 4, GETDATE()), 'Follow-up appointment');

-- 4. Verification queries (optional)
SELECT 'Doctors:' AS TableName;
SELECT * FROM dbo.Doctors;

SELECT 'Patients:' AS TableName;
SELECT * FROM dbo.Patients;

SELECT 'Appointments:' AS TableName;
SELECT * FROM dbo.Appointments;