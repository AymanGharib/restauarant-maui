-- MySQL dump 10.13  Distrib 8.0.41, for Linux (x86_64)
--
-- Host: localhost    Database: restaurant
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Commandes`
--

DROP TABLE IF EXISTS `Commandes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Commandes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Total` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Commandes`
--

LOCK TABLES `Commandes` WRITE;
/*!40000 ALTER TABLE `Commandes` DISABLE KEYS */;
INSERT INTO `Commandes` VALUES (1,'2025-04-20 17:28:47',0.00),(2,'2025-04-20 17:29:34',0.00),(3,'2025-04-22 18:43:32',45.00),(4,'2025-04-22 19:13:09',45.00);
/*!40000 ALTER TABLE `Commandes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `LigneCommande`
--

DROP TABLE IF EXISTS `LigneCommande`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `LigneCommande` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CommandeId` int NOT NULL,
  `PlatId` int NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `CommandeId` (`CommandeId`),
  KEY `PlatId` (`PlatId`),
  CONSTRAINT `LigneCommande_ibfk_1` FOREIGN KEY (`CommandeId`) REFERENCES `Commandes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `LigneCommande_ibfk_2` FOREIGN KEY (`PlatId`) REFERENCES `Plats` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `LigneCommande`
--

LOCK TABLES `LigneCommande` WRITE;
/*!40000 ALTER TABLE `LigneCommande` DISABLE KEYS */;
INSERT INTO `LigneCommande` VALUES (1,3,1,1),(2,4,1,1);
/*!40000 ALTER TABLE `LigneCommande` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Plats`
--

DROP TABLE IF EXISTS `Plats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Plats` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text,
  `Price` decimal(10,2) NOT NULL,
  `Category` varchar(50) DEFAULT NULL,
  `ImageUrl` text,
  `TimesOrdered` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Plats`
--

LOCK TABLES `Plats` WRITE;
/*!40000 ALTER TABLE `Plats` DISABLE KEYS */;
INSERT INTO `Plats` VALUES (1,'Salade Csar','Salade frache avec poulet grill, crotons et sauce Csar.',45.00,'Entre','https://example.com/images/cesar.jpg',2),(2,'Soupe de lgumes','Soupe maison aux lgumes de saison.',35.00,'Entre','https://example.com/images/soupe.jpg',2),(3,'Spaghetti Bolognaise','Spaghetti avec sauce bolognaise maison.',75.00,'Plat principal','https://example.com/images/spaghetti.jpg',2),(4,'Poulet rti','Poulet fermier rti aux herbes avec pommes de terre.',90.00,'Plat principal','https://example.com/images/poulet.jpg',2),(5,'Tiramisu','Dessert italien classique au caf et mascarpone.',40.00,'Dessert','https://example.com/images/tiramisu.jpg',2),(6,'Fondant au chocolat','Gteau fondant au cur chocolat noir.',45.00,'Dessert','https://example.com/images/fondant.jpg',4),(7,'Coca-Cola','Boisson gazeuse rafrachissante 33cl.',15.00,'Boisson','https://example.com/images/coca.jpg',4),(8,'Eau minrale','Bouteille d\'eau minrale 50cl.',10.00,'Boisson','https://example.com/images/eau.jpg',4);
/*!40000 ALTER TABLE `Plats` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Reservations`
--

DROP TABLE IF EXISTS `Reservations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Reservations` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TableId` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Date` date NOT NULL,
  `Time` time NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `TableId` (`TableId`),
  CONSTRAINT `Reservations_ibfk_1` FOREIGN KEY (`TableId`) REFERENCES `Tables` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Reservations`
--

LOCK TABLES `Reservations` WRITE;
/*!40000 ALTER TABLE `Reservations` DISABLE KEYS */;
INSERT INTO `Reservations` VALUES (1,1,'ayman','2025-04-22','19:00:00'),(2,2,'ayman','2025-04-22','19:00:00'),(3,3,'5e','2025-04-22','19:00:00');
/*!40000 ALTER TABLE `Reservations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tables`
--

DROP TABLE IF EXISTS `Tables`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tables` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Number` int NOT NULL,
  `Seats` int NOT NULL,
  `TimesReserved` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Number` (`Number`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tables`
--

LOCK TABLES `Tables` WRITE;
/*!40000 ALTER TABLE `Tables` DISABLE KEYS */;
INSERT INTO `Tables` VALUES (1,1,2,0),(2,2,2,0),(3,3,2,1),(4,4,4,0),(5,5,4,0),(6,6,4,0),(7,7,4,0),(8,8,6,0),(9,9,6,0),(10,10,6,0),(11,11,8,0),(12,12,8,0);
/*!40000 ALTER TABLE `Tables` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `PasswordHash` varchar(255) DEFAULT NULL,
  `Role` enum('admin','customer') DEFAULT 'customer',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (1,'Admin User','admin@example.com','admin123','admin'),(2,'John Doe','john@example.com','john123','customer'),(3,'Jane Smith','jane@example.com','jane123','customer');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `id` int NOT NULL AUTO_INCREMENT,
  `customer_name` varchar(100) DEFAULT NULL,
  `dish_name` varchar(100) DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (1,'ayman','tajine',2);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-23 13:14:20
