# 🌐 Taller 1: Introducción al desarrollo web/móvil

## 📌 Integrantes
* Jorge Nuñez Mori (21495752-3)
* Gustavo Miles Osorio (21444967-6)

## 📖 Descripción
Este repositorio contiene un proyecto escrito utilizando el lenguaje de programación C# con el entorno de .NET el cual posee un conjunto de controladores, entidades y un conjunto de endpoints para administrar un servicio de comercio electrónico. 

---
## Tecnologías
El proyecto utiliza las siguientes librerías y tecnologías:
- **C#**: Lenguaje de programación.  
- **.NET 8**: Framework para construir la API REST.  
- **SQLite**: Base de datos para almacenar los datos del proyecto.  
- **JWT**: Autenticación mediante tokens seguros.  
- **Postman**: Herramienta para pruebas y documentación de los endpoints.  

## ⚙️ Requisitos Previos

Asegúrate de tener instalado:
1. [.NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
2. [SQLite](https://www.sqlite.org/download.html)  
3. **Git** para clonar el repositorio.  
4. **Postman** (opcional, para probar los endpoints).  

---

## 🚀 Construcción

### 1️⃣ Clonar el Repositorio

Clonar el repositorio utilizando git
```bash
  git clone https://github.com/j0sash1/Taller_1_IDWM.git
```
### 2️⃣ Ir a la carpeta que contiene el proyecto
```bash
  cd Taller_1_IDWM 
```

### 3 Crear el archivo "appsettings.json" de la siguiente forma:
```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "ConnectionStrings": {
        "DefaultConnection": "Data Source=store.db"
    },
    "JWT": {
        "SignInKey": "random-string", // Cambiar por un string random propio
        "Issuer": "https://localhost:7064",
        "Audience": "https://localhost:7064"
    },
    "AllowedHosts": "*",
    "Serilog": {
        "MinimumLevel": {
            "Default": "Information",
            "Override": {
                "Microsoft": "Warning",
                "Microsoft.AspNetCore": "Warning",
                "Microsoft.AspNetCore.Hosting.Diagnostics": "Error",
                "Microsoft.Hosting.Lifetime": "Information",
                "System": "Error"
            }
        },
        "Enrich": [
            "FromLogContext",
            "WithMachineName",
            "WithThreadId"
        ],
        "WriteTo": [
            {
                "Name": "Console"
            },
            {
                "Name": "File",
                "Args": {
                    "path": "logs/log-.txt",
                    "rollingInterval": "Day",
                    "restrictedToMinimumLevel": "Information"
                }
            }
        ]
    }
}
```

Consideraciones:
- El JWT_SignInKey debe ser una clave random generada de largo de 128 bits.

---
### 3️⃣ Restaurar paquetes
```bash
    dotnet restore
```

### 4️⃣ Migraciones de Base de Datos

Para manejar la base de datos, debes aplicar las migraciones necesarias con los siguientes pasos:

1. **Generar las migraciones**:
   Ejecuta el siguiente comando para crear la migración inicial:
   ```bash
   dotnet ef migrations add InitialCreate
   ```
Este comando generará un archivo de migración que define la estructura de la base de datos.

2. **Aplicar las migraciones para crear la base de datos:**
   Ejecuta el siguiente comando para aplicar la migración y crear la base de datos:
   ```bash
   dotnet ef database update
   ```
---
### 5️⃣ Ejecutar el Proyecto
  Una vez completados los pasos anteriores, puedes iniciar el servidor localmente con el siguiente comando:
 
 ```bash
   dotnet run
  ```
  