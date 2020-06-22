create database Account

create table Student(
StuID int primary key identity (1,1) not null,
StuName nvarchar(50) not null,
StuLoginName nvarchar(50) not null, 
StuLoginPwd nvarchar(50) not null,
StuSex int not null ,  
StuPhone nvarchar(50) not null,
StuEmail nvarchar(50) not null,
StuGrade nvarchar(50) not null                                                              
)

create table Teacher(
TeaID int primary key identity (1,1) not null,
TeaName nvarchar(50) not null,
TeaLoginName nvarchar(50) not null,
TeaLoginPwd nvarchar(50) not null,
TeaPhone nvarchar(50) not null,
TeaEmail nvarchar(50) not null
)
create table Paper(
PaperID int primary key identity (1,1) not null,
PaperNmae nvarchar(50) not null,
PaperExplain nvarchar(50) not null,
PaperTime int not null
)
create table Topic(
TopicID int primary key identity (1,1) not null,
TopicExplain nvarchar(200) not null,
TopicScore int not null,
TopicType int not null,
TopicA nvarchar(100) not NULL,
TopicB nvarchar(100) not NULL,
TopicC nvarchar(100) not NULL,
TopicD nvarchar(100) not NULL,
TopicSort int NOT NULL,
TopicAnswer nvarchar(200) NOT NULL,
)
create table Selects(
SelectID int primary key identity (1,1) not null,
PaperID int foreign key references Paper(PaperID),
TopicID int foreign key references Topic(TopicID)
)
create table Answer(
AnswerID int primary key identity (1,1) not null,
PaperID int foreign key references Paper(PaperID),
StuID int foreign key references Student(StuID),
TeaID int foreign key references Teacher(TeaID),
AnswerScore int not NULL,
AnswerTime datetime not NULL,
SubmitTime datetime not NULL,
BatchTime datetime not NULL,
AnswerState int NOT NULL
)
create table Detail(
DetailID int primary key identity (1,1) not null,
AnswerID int foreign key references Answer(AnswerID),
TopicID int foreign key references Topic(TopicID),
DetailAnswer nvarchar (200) not NULL
)
delete  from Detail