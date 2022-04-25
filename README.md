## CR Medical Trip  ##

### Integrantes  ###

* Gerson Gonzalez Miranda
* Brainer Rojas Alfaro
* Natalia Hernandez Oreamuno


### Descripción del proyecto  ###

El proyecto CRMedical Ride consta de una plataforma web centrada en acercar y facilitar el contacto entre el cliente y los negocios de centros clínicos, con el objetivo de permitirle al usuario final realizar comparaciones, calendarización de citas, consultas de los servicios que ofrecen las clínicas, donde CRMedical Ride tiene un papel de intermediario. 
 
Para ello el sitio contará con un módulo de afiliación de parte de las diferentes clínicas que quieren a dar a conocer sus servicios dentro de la plataforma. De igual forma se contará con un módulo para que los usuarios finales inicien sesión en la aplicación web, sin embargo, no será obligatorio para los usuarios finales iniciar sesión para poder ver las diferentes clínicas y servicios, el inicio de sesión será voluntario, si el usuario decide iniciar sesión le permitirá recibir noticias con base a las búsquedas hechas por el usuario.  

Se mostrará un catálogo de resultados buscados por el usuario final, donde de acuerdo con los requerimientos y necesidades de este, ya sea por nombre, categoría, localización, y calificación de usuarios. 

Existirá un usuario administrador el cual podrá generar reportes basado desde cuales lugares se reciben más visitas, que tipo de clínicas se afilian, y cuáles son las más visitadas, todo esto con el fin adquirir más turista médico.  

De esta manera CRMedical Ride ofrece a sus usuarios finales una forma de obtener información y contactar con los distintos centros clínicos afiliados a la plataforma de manera fácil y rápida. 

### Cómo instalar el repositorio en el equipo para desarrollo  ###

# Prerequisitos

Para correr este proyecto, se necesita instalar ASP.NET CORE SDK. Lo pueden descargar desde [aqui.](https://dot.net/)
  
Para descargar este repositorio, se necesita Git instalado, lo pueden descargar desde [aqui.](https://git-scm.com/downloads)

## Desarrollo local
Utilizar el comando git clone que se encuentra en la parte superior de este repositorio. Luego, navegar dentro del nuevo directorio.

```bash
git clone <repository url> <OPTIONAL: new folder name>
cd <new folder name>
```
Usar el dotnet cli para descargar todas las dependencias del proyecto. Los comandos de dotnet deben ser corridos desde el directorio del proyecto.

```bash
dotnet build
```
## Ejecucion
El proyecto puede ser iniciado con cualquiera de los dos comandos de abajo. Se recomienda usar dotnet watch para hacer uso de la funcionalidad de hot reload.
```bash
dotnet run
dotnet watch
```
Uha vez el proyecto este corriendo, se puede ir en un buscador a localhost:<port>/ y ver la aplicacion web. dotnet watch abrira automaticamente una ventana del buscador al correr.

