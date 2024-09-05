# Clean Architecture Ejemplo

El siguiente es un ejemplo de un proyecto simple, en el cual se aplicó el patrón de arquitectura **CleanArchitecture**. Nos encontramos ante una **API Rest CRUD** (_Create-Read-Update-Delete_) de la entidad **Portfolio** (_Cartera_), con la utilización del ORM **EntityFramework** para la integración a una Base de Datos MS SQL Server. 
A su vez, se encuentran configurados los archivos para la utilización de **Docker**, facilitando la iniciación del proyecto.

Ante cualquier duda o inconveniente, podes contactarnos al siguiente correo:
* idtech@primary.com.ar

## Links de interés
* Sitio interno [I+D Tech](https://sites.google.com/matbarofex.com.ar/idtechprimary)
* Curso en Talent DDD [Estratégico](https://primary-lumina.talentlms.com/learner/courseinfo/id:285) y [Táctico](https://primary-lumina.talentlms.com/learner/courseinfo/id:289)

## Tabla de Contenido

- [Iniciando](#iniciando)
  - [Con Docker]()
  - [Sin Docker]()
- [Lista de características](#lista-de-caracter%C3%ADsticas)
  - [DDD (Domain Driven Design)](#large_blue_diamond-ddd)
  - [Docker](#large_blue_diamond-docker)
  - [Swagger Oas3 (OpenAPI Specification - Version 3)](#large_blue_diamond-swagger-oas3)
  - [Health Check](#large_blue_diamond-health-check)
  - [Unit Test](#large_blue_diamond-unit-test)
  - [Architectural Test](#large_blue_diamond-architectural-test)


## Iniciando
* Descargar este repositorio
* Recomendamos utilizar Visual Studio IDE :point_right: [download](https://visualstudio.microsoft.com/es/downloads)
* Es requerido SDK 8 :point_right: [download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Opcional] Docker Desktop :point_right: [download](https://www.docker.com/products/docker-desktop/)


### Paso a Paso 

#### Con Docker:

Para iniciar el entorno local, se deben ejecutar el siguiente comando en la carpeta raíz donde se encuentra el archivo ***"docker-compose-yml"***

```bash
docker-compose up -d
```

__Iniciar mis containers__
Podemos accionar el botón play en docker desktop (_dentro de la sección de containers_) o ejecutar los siguientes comandos:
```shell
docker start sqlserver
docker start CleanArchitectureAPI
```

#### Sin Docker:
Al no utilizar docker deberemos crear la base de datos y ajustar las configuraciones para que la API pueda acceder a la misma.

__Crear la base de datos__
Ejecutar el script "db-init.sql" que se encuentra en el raiz, para crear la base de datos en conjunto con el usuario de login. 

__Ajustar la configuración__

CleanArchitecture.App :arrow_right: Properties :arrow_right: launchSettings.json
```json
 "profiles": {
     "Local": {
         "environmentVariables": {
             "AppSettings__DataBaseConnectionString": "String de Conexión a la base de datos"
```

#### Por ultimo:
__Acceder al swagger y probar:__
* http://localhost:8098/swagger/index.html

A disfrutar!


## Lista de características

### :large_blue_diamond: DDD

El objetivo principal de aplicar DDD o Domain Driven Design en inglés, es poder aislar el código que pertenece al dominio, de los detalles técnicos de implementación y así centrarnos en la complejidad del negocio.

Recomendamos completar el curso DDD [Estratégico](https://primary-lumina.talentlms.com/learner/courseinfo/id:285) y [Táctico](https://primary-lumina.talentlms.com/learner/courseinfo/id:289) para mayor comprensión.


### :large_blue_diamond: Docker

Docker es un proyecto de código abierto que automatiza el despliegue de aplicaciones dentro de contenedores de software, proporcionando una capa adicional de abstracción y automatización de virtualización de aplicaciones en múltiples sistemas operativos.

Construido sobre las facilidades proporcionadas por el kernel Linux (_principalmente cgroups y namespaces_), un contenedor Docker, a diferencia de una máquina virtual, no requiere incluir un sistema operativo independiente. En su lugar, se basa en las funcionalidades del kernel y utiliza el aislamiento de recursos (_CPU, la memoria, E/S, red, etc._) y namespaces separados para aislar la vista de una aplicación del sistema operativo. Por lo tanto, los contenedores tienen una superficie significativamente menor que las imágenes de máquina virtual (_VM_).

Contenedores múltiples comparten el mismo núcleo, pero cada contenedor puede ser restringido a utilizar solo una cantidad definida de recursos como CPU, memoria y E/S.

Usar Docker para crear y gestionar contenedores puede simplificar la creación de sistemas altamente distribuidos. Esto permite que el despliegue de nodos se realice a medida que se dispone de recursos o cuando se necesiten más nodos, lo que permite una plataforma como servicio (_PaaS - Platform as a Service_) de estilo de despliegue.

> NOTA
> Docker es también una empresa que promueve e impulsa esta tecnología, en colaboración con proveedores de la nube, Linux y Windows, incluido Microsoft.

#### Comparando Contenedores de Docker con Virtual Machines

| Virtual Machines | Contenedores de Docker |
| :-------------: | :-------------: |
| ![vm](readme/virtual-machine-hardware-software.png?raw=true) | ![dc](readme/docker-container-hardware-software.png?raw=true) |
| Las máquinas virtuales incluyen la aplicación, las bibliotecas o los archivos binarios necesarios y un sistema operativo invitado completo. La virtualización completa requiere más recursos que la inclusión en contenedores. | Los contenedores incluyen la aplicación y todas sus dependencias. Sin embargo, comparten el kernel del sistema operativo con otros contenedores, que se ejecutan como procesos aislados en el espacio de usuario en el sistema operativo host. (_Excepto en los contenedores de Hyper-V, en que cada contenedor se ejecuta dentro de una máquina virtual especial por contenedor_). |

#### Analogía
Del mismo modo que los contenedores de mercancías permiten su transporte por barco, tren o camión independientemente de la carga de su interior, los contenedores de software actúan como una unidad estándar de implementación de software que puede contener diferentes dependencias y código. De esta manera, la inclusión del software en contenedor permite a los desarrolladores y los profesionales de TI implementarlo en entornos __con pocas modificaciones o ninguna en absoluto__.

#### Resumen
Los contenedores ofrecen las ventajas del aislamiento, la portabilidad, la agilidad, la escalabilidad y el control a lo largo de todo el flujo de trabajo del ciclo de vida de la aplicación. La ventaja más importante es el aislamiento del entorno que se proporciona entre el desarrollo y las operaciones.

#### Referencias :triangular_flag_on_post:
> * [Aprendiendo Microsoft - Container Docker Introducción](https://learn.microsoft.com/es-es/dotnet/architecture/microservices/container-docker-introduction/)
> * [Docker wiki](https://es.wikipedia.org/wiki/Docker_(software))


### :large_blue_diamond: Swagger Oas3

Para acceder a la documentación generada por esta herramienta, deberemos ingresar al siguiente endpoint:
* http://localhost:8098/swagger

![swagger_oas3_1](readme/swagger_oas3.png?raw=true)

#### Referencias :triangular_flag_on_post:
> * [Aprendiendo Microsoft - Swashbuckle](https://learn.microsoft.com/es-es/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio)
> * [Blog API versioning and integrate Swagger](https://blog.christian-schou.dk/how-to-use-api-versioning-in-net-core-web-api/)
  

### :large_blue_diamond: Health Check

Una aplicación se encarga de exponer las comprobaciones de estado como puntos de conexión HTTP, donde normalmente, las comprobaciones de estado se usan con un servicio de supervisión externa o un orquestador de contenedores para comprobar el estado de una aplicación. 

Antes de agregar comprobaciones de estado a una aplicación, debe decidir en qué sistema de supervisión se va a usar. El sistema de supervisión determina qué tipos de comprobaciones de estado se deben crear y cómo configurar sus puntos de conexión.

Para ello utilizamos la biblioteca:
  * Microsoft.AspNetCore.Diagnostics.HealthChecks

Dicha configuración se puede encontrar en:
  * __CleanArchitecture.App__ :arrow_right: Extensions   
    * ApplicationBuilder :arrow_right: HealthChecksApplicationBuilderExtensions    
    y en:
    * ServiceCollections :arrow_right: HealthChecksServiceCollectionExtensions
    
Y podemos ingresar al endpoint __"/health"__ para comprobar su funcionamiento.
  * http://localhost:5190/health

#### Referencias :triangular_flag_on_post:
> * [Aprendiendo Microsoft Health Checks](https://learn.microsoft.com/es-es/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-7.0)


### :large_blue_diamond: Unit Test
xUnit: Estos test están escritos mediante XUnit y utilizando las siguientes bibliotecas FluentAssertions y FakeItEasy.

#### Referencias :triangular_flag_on_post:
> * [Fluent Assertions Web](https://fluentassertions.com/)
> * [Fake It Easy Web](https://fakeiteasy.readthedocs.io/en/stable/)
> * [Blog NUnit vs xUnit vs MSTest](https://www.lambdatest.com/blog/nunit-vs-xunit-vs-mstest/)
> * [Aprendiendo Microsoft Unit Testing (mejores practicas)](https://learn.microsoft.com/es-es/dotnet/core/testing/unit-testing-best-practices)

### :large_blue_diamond: Architectural Test

¿Qué es una prueba de arquitectura? Una prueba de arquitectura es una prueba automatizada que permite a su aplicación verificar la estructura y el diseño de su código.

Imagine que un nuevo desarrollador se ha unido recientemente a su equipo y ampliará la aplicación actual. ¿Cómo puede hacer cumplir su arquitectura de software, reglas, etc.?

En este caso, se generaron unas "pruebas de dependencias" que reforzaran la solución para seguir el diseño de capas en Clean Architecture. Los invitamos a seguir investigando sobre estas pruebas para aprovechar al máximo sus ventajas.

#### Referencias :triangular_flag_on_post:
> * [Librería NetArchTest](https://github.com/BenMorris/NetArchTest)

