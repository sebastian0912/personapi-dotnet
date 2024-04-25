# Bienvenido al Proyecto PersonAPI

Este proyecto es una API de gestión de personas construida con ASP.NET. A continuación, encontrarás las instrucciones para configurar y ejecutar el proyecto en tu entorno local.

## Prerrequisitos

Antes de comenzar, asegúrate de tener instalado lo siguiente:

- **Visual Studio 2022**
- **SQL Server 2019** (en modo básico)
- **SQL Server Management Studio (SSMS)**

## Configuración del entorno de desarrollo

Para configurar tu entorno de desarrollo en Visual Studio:

1. Instala **Visual Studio Community 2022** con los siguientes complementos:
   - Desarrollo ASP.NET y web
   - Almacenamiento y procesamiento de datos
   - Plantillas de proyecto y elementos de .NET Framework
   - Características avanzadas de ASP.NET

## Clonar el repositorio

Una vez configurado el entorno, puedes clonar el repositorio del proyecto a tu máquina local:

```bash
git clone https://github.com/sebastian0912/personapi-dotnet.git 

## Configuración de la Base de Datos
Inicia SQL Server Management Studio y crea una nueva base de datos llamada persona_db.
Abre el archivo CreacionTablas.sql encontrado dentro de la carpeta scripts del proyecto clonado y ejecuta el script para crear las tablas necesarias en tu base de datos.
Ejecuta el script InsercionDatos.sql para poblar las tablas con datos iniciales.
Estos scripts están diseñados para establecer la estructura necesaria y proporcionar un conjunto inicial de datos para que puedas comenzar a trabajar con la API de inmediato.

## Ejecutar el proyecto
Una vez que hayas configurado tu base de datos y clonado el código fuente, puedes abrir el proyecto en Visual Studio y ejecutarlo. Visual Studio debería manejar la restauración de los paquetes NuGet y la compilación del proyecto automáticamente.
