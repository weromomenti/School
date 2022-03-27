CREATE TABLE Teacher(
	name nvarchar(55) PRIMARY KEY
)

CREATE TABLE TeacherSubjectRelation(
	teacherName nvarchar(55) FOREIGN KEY REFERENCES Teacher(name) ON DELETE CASCADE,
	subjectName nvarchar(50)
	PRIMARY KEY (teacherName, subjectName)
)
CREATE TABLE PupilSubjectRelation(
	pupilName nvarchar(55),
	subjectName nvarchar(50)
	PRIMARY KEY (pupilName, subjectName)
)