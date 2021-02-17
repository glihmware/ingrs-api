DROP DATABASE IF EXISTS `ingrs`;

CREATE DATABASE `ingrs` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `ingrs`;

SET NAMES utf8mb4;

------
--
-- Domain.
--
-- A domain must register it's public keys to operate with ingrs.
--
-----
DROP TABLE IF EXISTS `Domain`;

CREATE TABLE `Domain` (
       `Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
       `Name` VARCHAR(256) NOT NULL UNIQUE,
       `IID` VARCHAR(512) NOT NULL UNIQUE,
       `RsaPub` VARCHAR(1024) DEFAULT NULL,
       `EcdsaPub` VARCHAR(512) DEFAULT NULL
) ENGINE=INNODB;


------
--
-- Operation.
--
-- An operation is created at each domain request to manipulate a user.
--
-----
DROP TABLE IF EXISTS `Op`;

CREATE TABLE `Op` (
       `Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,

       `TIID` VARCHAR(512) NOT NULL,
       `RandNum` BIGINT UNSIGNED NOT NULL,

       `GenerateTs` BIGINT UNSIGNED DEFAULT 0,
       -- User register, verify credential, update credentials, etc...
       `Type` TINYINT UNSIGNED,

       -- Domain ID to be sure the key is used by the same domain.
       `DomainId` INT UNSIGNED NOT NULL

) ENGINE=INNODB;


------
--
-- User.
--
-- End user of domains.
--
-----
DROP TABLE IF EXISTS `User`;

CREATE TABLE `User` (
       `Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,

       -- Unique ingrs identifier given to the domain to identify the user.
       `IID` VARCHAR(512) NOT NULL,

       -- Random salt associated to the user credentials.
       `Salt` VARCHAR(512) NOT NULL,
       -- The argon2 hash of the Argon2(Username | Passwd | DomainIID) coming from front-end.
       `Argon22` VARCHAR(512) NOT NULL UNIQUE,

       `RegisterTs` BIGINT UNSIGNED DEFAULT 0,
       `FailAttempts` INT UNSIGNED DEFAULT 0,
       `LastSuccessCon` BIGINT UNSIGNED DEFAULT 0,

       `DomainId` INT UNSIGNED NOT NULL

) ENGINE=INNODB;

