create table Users(
	userName nvarchar(55) primary key,
	securitylevel int
)
create table Objects(
	dataName nvarchar(55) primary key,
	securitylevel int
)

select * from Users

select * from Objects

select dataName from Objects WHERE securitylevel <= (select securitylevel from Users where userName = 'input user name')

