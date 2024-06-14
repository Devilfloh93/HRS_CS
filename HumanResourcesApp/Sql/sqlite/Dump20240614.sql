-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: human_resource_app
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `break_time`
--

DROP TABLE IF EXISTS `break_time`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `break_time` (
  `employee_id` int unsigned DEFAULT NULL,
  `break_time_in_sec` int unsigned DEFAULT '0',
  `break_date` datetime DEFAULT NULL,
  KEY `employee_id` (`employee_id`),
  CONSTRAINT `break_time_ibfk_1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `break_time`
--

LOCK TABLES `break_time` WRITE;
/*!40000 ALTER TABLE `break_time` DISABLE KEYS */;
/*!40000 ALTER TABLE `break_time` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `department`
--

DROP TABLE IF EXISTS `department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `department` (
  `department_id` int unsigned NOT NULL AUTO_INCREMENT,
  `department_name` varchar(255) DEFAULT NULL,
  `employee_id` int unsigned DEFAULT NULL,
  PRIMARY KEY (`department_id`),
  UNIQUE KEY `employee_id` (`employee_id`),
  CONSTRAINT `department_ibfk_1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `department`
--

LOCK TABLES `department` WRITE;
/*!40000 ALTER TABLE `department` DISABLE KEYS */;
INSERT INTO `department` VALUES (1,'Test',1);
/*!40000 ALTER TABLE `department` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `employee_id` int unsigned NOT NULL AUTO_INCREMENT,
  `salary` decimal(10,2) DEFAULT NULL,
  `max_holidays` int unsigned DEFAULT NULL,
  `job_title` varchar(255) DEFAULT NULL,
  `person_id` int unsigned DEFAULT NULL,
  PRIMARY KEY (`employee_id`),
  UNIQUE KEY `person_id` (`person_id`),
  CONSTRAINT `employee_ibfk_1` FOREIGN KEY (`person_id`) REFERENCES `personal_data` (`person_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES (1,2000.30,20,'Developer',1);
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `holiday`
--

DROP TABLE IF EXISTS `holiday`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `holiday` (
  `employee_id` int unsigned DEFAULT NULL,
  `holiday_date` datetime DEFAULT NULL,
  KEY `employee_id` (`employee_id`),
  CONSTRAINT `holiday_ibfk_1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `holiday`
--

LOCK TABLES `holiday` WRITE;
/*!40000 ALTER TABLE `holiday` DISABLE KEYS */;
INSERT INTO `holiday` VALUES (1,'2023-11-13 13:23:44');
/*!40000 ALTER TABLE `holiday` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `login`
--

DROP TABLE IF EXISTS `login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `login` (
  `employee_id` int unsigned DEFAULT NULL,
  `login_name` varchar(64) DEFAULT NULL,
  `login_password` varchar(64) DEFAULT NULL,
  UNIQUE KEY `login_name` (`login_name`),
  UNIQUE KEY `employee_id` (`employee_id`),
  CONSTRAINT `login_ibfk_1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `login`
--

LOCK TABLES `login` WRITE;
/*!40000 ALTER TABLE `login` DISABLE KEYS */;
INSERT INTO `login` VALUES (1,'test','9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08');
/*!40000 ALTER TABLE `login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `personal_data`
--

DROP TABLE IF EXISTS `personal_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `personal_data` (
  `person_id` int unsigned NOT NULL AUTO_INCREMENT,
  `firstname` varchar(255) DEFAULT NULL,
  `middlename` varchar(255) DEFAULT NULL,
  `lastname` varchar(255) DEFAULT NULL,
  `gender` varchar(120) DEFAULT NULL,
  `birth_date` datetime DEFAULT NULL,
  `street_adress` varchar(255) DEFAULT NULL,
  `postal_code` varchar(20) DEFAULT NULL,
  `city` varchar(255) DEFAULT NULL,
  `state` varchar(255) DEFAULT NULL,
  `country` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `phone` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`person_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `personal_data`
--

LOCK TABLES `personal_data` WRITE;
/*!40000 ALTER TABLE `personal_data` DISABLE KEYS */;
INSERT INTO `personal_data` VALUES (1,'Florian','','Pötzsch','männlich','1993-08-13 13:23:44','Boxberger Str. 13','01239','Dresden','Sachsen','Deutschland','poetzsch@gmx.de','00191393189');
/*!40000 ALTER TABLE `personal_data` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sick_day`
--

DROP TABLE IF EXISTS `sick_day`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sick_day` (
  `employee_id` int unsigned DEFAULT NULL,
  `sick_date` datetime DEFAULT NULL,
  KEY `employee_id` (`employee_id`),
  CONSTRAINT `sick_day_ibfk_1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sick_day`
--

LOCK TABLES `sick_day` WRITE;
/*!40000 ALTER TABLE `sick_day` DISABLE KEYS */;
INSERT INTO `sick_day` VALUES (1,'2022-08-06 13:23:44');
/*!40000 ALTER TABLE `sick_day` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `work_time`
--

DROP TABLE IF EXISTS `work_time`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `work_time` (
  `employee_id` int unsigned DEFAULT NULL,
  `work_time_in_sec` int unsigned DEFAULT '0',
  `work_date` datetime DEFAULT NULL,
  KEY `employee_id` (`employee_id`),
  CONSTRAINT `work_time_ibfk_1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `work_time`
--

LOCK TABLES `work_time` WRITE;
/*!40000 ALTER TABLE `work_time` DISABLE KEYS */;
/*!40000 ALTER TABLE `work_time` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-14 11:31:12
