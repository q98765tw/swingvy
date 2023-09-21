CREATE DATABASE swingvy;

USE swingvy;

CREATE TABLE member (
  member_id INT IDENTITY(1,1)  NOT NULL,
  account nvarchar(50) NOT NULL,
  password nvarchar(50) NOT NULL,
  PRIMARY KEY (member_id)
);
CREATE TABLE memberData (
  memberData_id INT IDENTITY(1,1)  NOT NULL,
  member_id INT NOT NULL,
  name nvarchar(30),
  email nvarchar(50),
  phone nvarchar(15),
  type INT NOT NULL,
  position INT NOT NULL,
  head INT NOT NULL,
  img_url nvarchar(300),
  PRIMARY KEY (memberData_id),
  FOREIGN KEY (member_id) REFERENCES member(member_id)
);
CREATE TABLE leaveOrder (
  leaveOrder_id INT IDENTITY(1,1)  NOT NULL,
  member_id INT NOT NULL,
  type INT NOT NULL,
  startTime datetime,
  endTime datetime,
  applyTime datetime,
  reason nvarchar(100),
  state INT NOT NULL,
  head INT NOT NULL,
  PRIMARY KEY (leaveOrder_id),
  FOREIGN KEY (member_id) REFERENCES member(member_id)
);
CREATE TABLE calendar (
  calendar_id INT IDENTITY(1,1)  NOT NULL,
  member_id INT NOT NULL,
  startTime datetime,
  endTime datetime,
  name nvarchar(100),
  PRIMARY KEY (calendar_id),
  FOREIGN KEY (member_id) REFERENCES member(member_id)
);
CREATE TABLE worktime (
  worktime_id INT IDENTITY(1,1)  NOT NULL,
  member_id INT NOT NULL,
  startTime datetime,
  endTime datetime,
  state INT NOT NULL,
  PRIMARY KEY (worktime_id),
  FOREIGN KEY (member_id) REFERENCES member(member_id)
);
INSERT INTO member (account, password) VALUES
('user1', 'password1'),
('user2', 'password2'),
('user3', 'password3');

INSERT INTO memberData (member_id, name, email, phone, type, position, head, img_url) VALUES
(1, 'John Doe', 'john.doe@example.com', '123-456-7890', 2, 1, 1, '~/img/0733ba760b29378474dea0fdbcb97107.jpg'),
(2, 'Jane Smith', 'jane.smith@example.com', '987-654-3210', 2, 0, 1, '~/img/2f578d07945132849b05fbdaf78cba38.jpg'),
(3, 'Bob Johnson', 'bob.johnson@example.com', '555-123-4567', 2, 0, 1, '~/img/33fd1f2798aba71937fc6318bc7f8a14.jpg');

INSERT INTO leaveOrder (member_id, type, startTime, endTime, applyTime, reason, state, head) VALUES
(1, 0, '2023-09-20 08:00:00', '2023-09-21 17:00:00', '2023-09-19 10:00:00', 'Vacation', 0, 1),
(2, 2, '2023-09-22 09:00:00', '2023-09-22 16:00:00', '2023-09-19 11:30:00', 'Sick Leave', 0, 1),
(3, 1, '2023-09-25 13:00:00', '2023-09-25 18:00:00', '2023-09-19 14:15:00', 'Personal Leave', 0, 1);

INSERT INTO calendar (member_id, startTime, endTime, name) VALUES
(1, '2022-09-20 10:00:00', '2022-09-20 12:00:00', '新到職'),
(2, '2023-09-01 14:00:00', '2023-09-01 16:00:00', '新到職'),
(3, '2023-09-02 09:30:00', '2023-09-02 11:30:00', '新到職');

INSERT INTO worktime (member_id, startTime, endTime, state) VALUES
(1, '2023-09-19 09:00:00', null, 1),
(2, null, null, 0),
(3, null, null, 0);