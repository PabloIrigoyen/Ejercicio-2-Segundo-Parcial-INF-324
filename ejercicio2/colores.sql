-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 11-06-2024 a las 23:37:42
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `colores`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `texturas`
--

CREATE TABLE `texturas` (
  `descripcion` varchar(50) NOT NULL,
  `cR` varchar(50) NOT NULL,
  `cG` varchar(50) NOT NULL,
  `cB` varchar(50) NOT NULL,
  `colorpintar` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `texturas`
--

INSERT INTO `texturas` (`descripcion`, `cR`, `cG`, `cB`, `colorpintar`) VALUES
('textura1', '7', '7', '9', 'Blue'),
('textura1', '58', '45', '35', 'AliceBlue'),
('textura2', '32', '31', '47', 'Khaki'),
('textura2', '71', '27', '41', 'SandyBrown'),
('textura2', '61', '22', '36', 'Salmon'),
('textura2', '243', '243', '238', 'SlateGray'),
('textura2', '239', '239', '235', 'SlateBlue'),
('textura2', '189', '171', '163', 'Tan'),
('textura2', '181', '128', '110', 'Snow'),
('textura2', '171', '170', '176', 'Crimson'),
('textura3', '228', '99', '127', 'Gray'),
('textura3', '166', '31', '72', 'Gray'),
('textura3', '188', '9', '6', 'Lime'),
('textura3', '131', '32', '102', 'CadetBlue'),
('textura3', '151', '53', '18', 'Purple'),
('textura3', '232', '208', '56', 'Plum'),
('textura3', '67', '97', '32', 'AliceBlue'),
('textura3', '31', '81', '6', 'AntiqueWhite'),
('textura3', '206', '166', '44', 'Coral');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
