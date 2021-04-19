USE Practice10tSQLt

if not exists (select * from sysobjects where name='Marks_Table' and xtype='U')
	CREATE TABLE Marks_Table (
		ID int PRIMARY KEY IDENTITY(1,1),
		FirstName varchar(255),
		LastName varchar(255),
		FirstLabMark float,
		SecondLabMark float,
		ThirdLabMark float,
	)

INSERT INTO Marks_Table(FirstName, LastName, FirstLabMark, SecondLabMark, ThirdLabMark)
VALUES 
	 ('Svyatoslav', 'Fedyk', 5, 3, 5),
	 ('Arthas', 'Menetill', 5, 5, 5),
	 ('Soap',  'Mactavish', 4, 4, 5),
	 ('Sara',  'Conor', 4, 5, 4),
	 ('Martin',  'MacFly', 4, 3, 5),
	 ('Roronoa',  'Zoro', 3, 3, 3),
	 ('Izuki',  'Midoriya', 5, 5, 5),
	 ('Tony',  'Kark', 5, 5, 5),
	 ('Saimon',  'Railey', 4, 4, 4)

--SELECT * FROM Marks_Table
--DROP TABLE IF EXISTS Marks_Table
GO

CREATE OR ALTER PROCEDURE AvgMark @FirstName nvarchar(255), @Lastname nvarchar(255) 
AS
BEGIN
	DECLARE @ReturnValue float

	SELECT @ReturnValue = (FirstLabMark + SecondLabMark + ThirdLabMark) / 3 
							FROM Marks_Table 
							WHERE @FirstName = FirstName and @Lastname = LastName

	RETURN @ReturnValue
END
GO

EXEC tSQLt.NewTestClass 'testSQlPractice';
GO


CREATE OR ALTER PROCEDURE testSQlPractice.[test | чи не має в таблиці відємних оцінок]
AS
BEGIN
	
	SELECT *
	INTO actual
	FROM Marks_Table
	WHERE FirstLabMark < 0 or SecondLabMark < 0 or ThirdLabMark < 0

	EXEC tSQLt.AssertEmptyTable 'actual'

END
GO


CREATE OR ALTER PROCEDURE testSQlPractice.[test | чи правильно рахується середнє арифметичне для декількох студентів]
AS
BEGIN

	DECLARE @actual float
	DECLARE @expected float

	SET @actual = 4
	EXEC @expected = AvgMark @FirstName = 'Soap', @Lastname = 'Mactavish'
	EXEC tSQLt.AssertEquals @expected, @actual

	SET @actual = 4
	EXEC @expected = AvgMark @FirstName = 'Svyatoslav', @Lastname = 'Fedyk'
	EXEC tSQLt.AssertEquals @expected, @actual

	SET @actual = 3
	EXEC @expected = AvgMark @FirstName = 'Roronoa', @Lastname = 'Zoro'
	EXEC tSQLt.AssertEquals @expected, @actual

END
GO

CREATE OR ALTER PROCEDURE testSQlPractice.[test | чи містить таблиця запис з даними - цілий рядок ]
AS
BEGIN
	DECLARE @actual int

	SET @actual = (SELECT COUNT(FirstName) as count FROM Marks_Table WHERE 
	FirstName IS NOT NULL
	AND LastName IS NOT NULL
	AND FirstLabMark IS NOT NULL
	AND SecondLabMark IS NOT NULL
	AND ThirdLabMark IS NOT NULL)

    EXEC tSQLt.AssertNotEquals 0, @actual
	
END
GO


EXEC tSQLt.RunAll
GO

EXEC tSQLt.DropClass 'testSQlPractice';
GO

DROP TABLE IF EXISTS Marks_Table;
GO