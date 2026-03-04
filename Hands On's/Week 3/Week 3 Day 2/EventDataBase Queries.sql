CREATE DATABASE EventDb;

USE EventDb;

CREATE TABLE UserInfo (
    EmailId VARCHAR(100) PRIMARY KEY,
    UserName VARCHAR(50) NOT NULL,
    Role VARCHAR(20) NOT NULL,
    Password VARCHAR(20) NOT NULL,

    CONSTRAINT CHK_Role CHECK (Role IN ('Admin','Participant'))
);

CREATE TABLE EventDetails (
    EventId INT PRIMARY KEY,
    EventName VARCHAR(50) NOT NULL,
    EventCategory VARCHAR(50) NOT NULL,
    EventDate DATETIME NOT NULL,
    Description VARCHAR(255) NULL,
    Status VARCHAR(20) NOT NULL 
        CHECK (Status IN ('Active','In-Active'))
);

CREATE TABLE SpeakersDetails (
    SpeakerId INT PRIMARY KEY,
    SpeakerName VARCHAR(50) NOT NULL
);

CREATE TABLE SessionInfo (
    SessionId INT PRIMARY KEY,
    EventId INT NOT NULL,
    SessionTitle VARCHAR(50) NOT NULL,
    SpeakerId INT NOT NULL,
    Description VARCHAR(255) NULL,
    SessionStart DATETIME NOT NULL,
    SessionEnd DATETIME NOT NULL,
    SessionUrl VARCHAR(255) NULL,

    CONSTRAINT FK_Session_Event
        FOREIGN KEY (EventId) 
        REFERENCES EventDetails(EventId),

    CONSTRAINT FK_Session_Speaker
        FOREIGN KEY (SpeakerId) 
        REFERENCES SpeakersDetails(SpeakerId),

    CONSTRAINT CHK_Session_Time 
        CHECK (SessionEnd > SessionStart)
);

CREATE TABLE ParticipantEventDetails (
    Id INT PRIMARY KEY,
    ParticipantEmailId VARCHAR(100) NOT NULL,
    EventId INT NOT NULL,
    SessionId INT NOT NULL,
    IsAttended BIT NOT NULL 
        CHECK (IsAttended IN (0,1)),

    CONSTRAINT FK_Participant_User
        FOREIGN KEY (ParticipantEmailId) 	
        REFERENCES UserInfo(EmailId),

    CONSTRAINT FK_Participant_Event
        FOREIGN KEY (EventId) 
        REFERENCES EventDetails(EventId),

    CONSTRAINT FK_Participant_Session
        FOREIGN KEY (SessionId) 
        REFERENCES SessionInfo(SessionId)
);

-- Insert Users
INSERT INTO UserInfo VALUES
('admin@gmail.com','AdminUser','Admin','admin123'),
('user1@gmail.com','Rahul','Participant','pass123');

-- Insert Event
INSERT INTO EventDetails VALUES
(1,'Tech Conference','Technology','2026-04-10','Annual Tech Meet','Active');

-- Insert Speaker
INSERT INTO SpeakersDetails VALUES
(101,'Mr. John');

-- Insert Session
INSERT INTO SessionInfo VALUES
(1001,1,'AI Trends',101,'AI Discussion',
 '2026-04-10 10:00:00',
 '2026-04-10 11:00:00',
 'https://meetlink.com/ai');

-- Insert Participant Event Record
INSERT INTO ParticipantEventDetails VALUES
(1,'user1@gmail.com',1,1001,1);

SELECT * FROM EventDetails;

SELECT s.SessionTitle,
       e.EventName,
       sp.SpeakerName,
       s.SessionStart,
       s.SessionEnd
FROM SessionInfo s
JOIN EventDetails e ON s.EventId = e.EventId
JOIN SpeakersDetails sp ON s.SpeakerId = sp.SpeakerId;

SELECT u.UserName,
       e.EventName,
       s.SessionTitle,
       p.IsAttended
FROM ParticipantEventDetails p
JOIN UserInfo u ON p.ParticipantEmailId = u.EmailId
JOIN EventDetails e ON p.EventId = e.EventId
JOIN SessionInfo s ON p.SessionId = s.SessionId;