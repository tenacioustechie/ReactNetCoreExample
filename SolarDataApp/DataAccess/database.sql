﻿
-- create database SolarDataApp;
-- consider removing this
use SolarDataApp;

-- grant all on SolarDataApp.* to 'SolarDataApp'@'%' identified by 'crappyPassword79';

/*
drop table if exists Readings;
drop table if exists Locations;
-- */

-- drop table Locations;
CREATE TABLE IF NOT EXISTS Locations (
  Id int NOT NULL AUTO_INCREMENT,
  Created datetime not null default( now()),
  Name varchar(100) not null,
  primary key (Id),
  index IxLocations_Created (Created)
);


-- drop table Readings;
CREATE TABLE IF NOT EXISTS Readings (
  Id int NOT NULL AUTO_INCREMENT,
  LocationId int not null,
  Day datetime not null,
  SolarGenerated decimal(5,2) not null default(0.0),
  PowerUsed decimal(5,2) not null default(0.0),
  primary key (Id),
  foreign key (LocationId) references Locations( Id),
  index IxReadings_Day (Day)
);


Insert Into Locations (Name) values ("Wilga") on duplicate key update Name = Name;
set @locationId = LAST_INSERT_ID();
Insert Into Readings (LocationId, Day, SolarGenerated, PowerUsed) values ( @locationId, DATE_ADD( NOW(), Interval -1 Day ), 34.1, 18.7);
Insert Into Readings (LocationId, Day, SolarGenerated, PowerUsed) values ( @locationId, DATE_ADD( NOW(), Interval -2 Day ), 37.5, 18.9);
Insert Into Readings (LocationId, Day, SolarGenerated, PowerUsed) values ( @locationId, DATE_ADD( NOW(), Interval -3 Day ), 22.3, 17.2);

