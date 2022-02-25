# Tuya Prueba German

Realizacion de proyecto con Clean / Architectrure Clean y **Arquitectura Onion**
![onion arquitec](https://user-images.githubusercontent.com/3777303/155810881-98d94568-845b-474a-9338-106efaf9de77.PNG)

Patrones de Diseño implementados:

•	Repositorio

•	Injección de Dependencias

•	Unit of work

•	MVC etc.

Se desarrollo en: 

•	 C# en .NET Core version 5

•	 EF en .NET Core 5

•	Versionamiento de código (Git) https://github.com/gregdorian/TuyaPruebaGerman

•	Manejo de Base de Datos 

Se Realizaron los siguientes entidades/Tablas

![Tuya](https://user-images.githubusercontent.com/3777303/155810877-4b296341-ffe1-4139-9034-f4aeb7e4dcc7.PNG)

Producto

OrdenCompra

Cliente

Logistica

Para la Base de datos se utilizo SQL server 2016 Utilizando Entity Framework Core 6.0 y Code First .Net 5.0

La estructura de los proyectos de clases y UI son las siguientes:


**Core**

Domain.Core

Domain.Aplication

Domain.Entities

**Infrastructure**

TuyaPruebaGerman.Infrastructure.Persistence

TuyaPruebaGerman.Infrastructure.Shared

**Presentacion**

Controllers

para ejecutar la solucion despues de descargarla/Clonar del repeositorio de github:

add-migration Initial

update-database

**Para usar Autenticacion**

Los roles predeterminados son los siguientes.

superadministrador

Administración

Moderador

Básico

Aquí están las credenciales para los usuarios predeterminados.

Correo electrónico - superadmin@gmail.com / Contraseña - 123Pa$$word!

Correo electrónico - basic@gmail.com / Contraseña - 123Pa$$word!

Por temas del tiempo no se realizaron los proyectos de TEST!!
