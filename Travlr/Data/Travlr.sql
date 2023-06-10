USE [master]
GO
IF db_id('Travlr') IS NULL
  CREATE DATABASE [Travlr]
GO
USE [Travlr]
GO
DROP TABLE IF EXISTS [UserTrip];
DROP TABLE IF EXISTS [Trip];
DROP TABLE IF EXISTS [Dine];
DROP TABLE IF EXISTS [Activity];
DROP TABLE IF EXISTS [Stay];
DROP TABLE IF EXISTS [StayOptions];
DROP TABLE IF EXISTS [DineOptions];
DROP TABLE IF EXISTS [ActivityOptions];
DROP TABLE IF EXISTS [User];
CREATE TABLE [User] (
  [id] int PRIMARY KEY identity,
  [name] nvarchar(255) not null,
  [dateCreated] datetime not null,
  [email] nvarchar(255) not null,
  [firebaseId] nvarchar(255)
)
GO
CREATE TABLE [UserTrip] (
  [id] int PRIMARY KEY identity,
  [userId] int not null,
  [tripId] int not null
)
GO
CREATE TABLE [Trip] (
  [id] int PRIMARY KEY identity,
  [userId] int not null,
  [tripName] nvarchar(255) not null,
  [pastTrip] bit not null,
  [description] nvarchar(255),
  [budget] int
)
GO
CREATE TABLE [Dine] (
  [id] int PRIMARY KEY identity,
  [dineName] nvarchar(255) not null,
  [dineAddress] nvarchar(255),
  [dineImage] nvarchar(255),
  [dineDescription] nvarchar(255),
  [dineNotes] nvarchar(255)
)
GO
CREATE TABLE [Activity] (
  [id] int PRIMARY KEY identity,
  [activityName] nvarchar(255) not null,
  [activityAddress] nvarchar(255),
  [activityImage] nvarchar(255),
  [activityDescription] nvarchar(255),
  [activityNotes] nvarchar(255)
)
GO
CREATE TABLE [Stay] (
  [id] int PRIMARY KEY identity,
  [stayName] nvarchar(255) not null,
  [stayAddress] nvarchar(255),
  [stayImage] nvarchar(255),
  [stayDescription] nvarchar(255),
  [stayNotes] nvarchar(255)
)
GO
CREATE TABLE [StayOptions] (
  [id] int PRIMARY KEY identity,
  [tripId] int not null,
  [stayId] int not null
)
GO
CREATE TABLE [DineOptions] (
  [id] int PRIMARY KEY identity,
  [tripId] int not null,
  [dineId] int not null
)
GO
CREATE TABLE [ActivityOptions] (
  [id] int PRIMARY KEY identity,
  [tripId] int not null,
  [activityId] int not null
)
GO
ALTER TABLE [dineOptions] ADD FOREIGN KEY ([dineId]) REFERENCES [dine] ([id])
GO
ALTER TABLE [dineOptions] ADD FOREIGN KEY ([tripId]) REFERENCES [trip] ([id])
GO
ALTER TABLE [stayOptions] ADD FOREIGN KEY ([stayId]) REFERENCES [stay] ([id])
GO
ALTER TABLE [stayOptions] ADD FOREIGN KEY ([tripId]) REFERENCES [trip] ([id])
GO
ALTER TABLE [activityOptions] ADD FOREIGN KEY ([activityId]) REFERENCES [activity] ([id])
GO
ALTER TABLE [activityOptions] ADD FOREIGN KEY ([tripId]) REFERENCES [trip] ([id])
GO
ALTER TABLE [userTrip] ADD FOREIGN KEY ([userId]) REFERENCES [user] ([id])
GO
ALTER TABLE [userTrip] ADD FOREIGN KEY ([tripId]) REFERENCES [trip] ([id])
GO
--STARTING DATA--
INSERT INTO [User] ([name], [dateCreated], [email], [firebaseId])
VALUES
    ('Desiree Burrier','2020-12-13T07:25:34.000Z', 'desiree@gmail.com', 'm4Kl9WLKeFTYTsLvw4Rpo2gQOXD3'),
    ('Erica Clayton','2020-11-13T07:25:34.000Z', 'erica@gmail.com', 'CUEzzSrMcPcKIsiHBQLAPkUhd4z1'),
    ('Wesley Baertsch','2022-10-03T09:44:58.000Z', 'wesley@gmail.com', 'NhQjw53JVkbxtRH7NAB9oVYR5jB3')
GO
INSERT INTO [UserTrip] ([userId], [tripId])
VALUES
    (1,1),
    (1,2),
    (2,1),
    (2,2),
    (3,3)
GO
INSERT INTO [Trip] ([userId], [tripName], [pastTrip], [description], [budget])
VALUES
    (1, 'Rome', '0', 'yay trip', 2000),
    (2, 'Greece', '0', 'yay summer trip', 1000),
    (3, 'Mexico', '0', 'birthday trip', 1500)
GO
INSERT INTO [Dine] ([dineName], [dineAddress], [dineImage], [dineDescription], [dineNotes])
VALUES
    ('Rome Restraunt', '100 Rome St', null, 'a place to eat in Rome', 'it was good'),
    ('Greece Restraunt', '100 Greece St', null, 'a place to eat in Greece', 'it was good'),
    ('Mexico Restraunt', '100 Mexico St', null, 'a place to eat in Mexico', 'it was good')
GO
INSERT INTO [Activity] ([activityName], [activityAddress], [activityImage], [activityDescription], [activityNotes])
VALUES
    ('Rome Activity', '100 Rome St', null, 'a place to do things in Rome', 'it was good'),
    ('Greece Activity', '100 Greece St', null, 'a place to do things in Greece', 'it was good'),
    ('Mexico Activity', '100 Mexico St', null, 'a place to do things in Mexico', 'it was good')
GO
INSERT INTO [Stay] ([stayName], [stayAddress], [stayImage], [stayDescription], [stayNotes])
VALUES
    ('Rome stay', '100 Rome St', null, 'a place stay in Rome', 'it was good'),
    ('Greece stay', '100 Greece St', null, 'a place to stay in Greece', 'it was good'),
    ('Mexico stay', '100 Mexico St', null, 'a place to stay in Mexico', 'it was good')
GO
INSERT INTO [StayOptions] ([tripId], [stayId])
VALUES
    (1,1),
    (2,2),
    (3,3)
GO
INSERT INTO [DineOptions] ([tripId], [dineId])
VALUES
    (1,1),
    (2,2),
    (3,3)
GO
INSERT INTO [ActivityOptions] ([tripId], [activityId])
VALUES
    (1,1),
    (2,2),
    (3,3)
GO