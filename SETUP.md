# Configuración y Despliegue de la Aplicación de Generación de Recibos

## Requisitos Previos
- Visual Studio 2022 o posterior con la carga de trabajo de desarrollo móvil con .NET
- .NET 7.0 SDK o posterior
- Emuladores de Android/iOS o dispositivos físicos para pruebas

## Configuración del Entorno de Desarrollo

1. Clonar el repositorio:

git clone https://github.com/tu-usuario/ReciboGeneratorApp.git

2. Abrir la solución `ReciboGeneratorApp.sln` en Visual Studio.

3. Restaurar los paquetes NuGet:
- Haz clic derecho en la solución en el Explorador de Soluciones.
- Selecciona "Restaurar paquetes NuGet".

4. Configura los emuladores o conecta dispositivos físicos para pruebas.

## Compilación y Ejecución

1. Selecciona la plataforma objetivo (Android, iOS, Windows) en la barra de herramientas de Visual Studio.

2. Presiona F5 o haz clic en el botón "Iniciar depuración" para compilar y ejecutar la aplicación.

## Despliegue

### Android
1. En Visual Studio, haz clic derecho en el proyecto Android.
2. Selecciona "Archivar" para crear un archivo APK firmado.
3. Sigue las instrucciones para firmar el APK con tu clave de firma.
4. Sube el APK firmado a Google Play Store.

### iOS
1. Asegúrate de tener una cuenta de desarrollador de Apple válida.
2. En Visual Studio for Mac o en un Mac conectado, archiva la aplicación iOS.
3. Usa Xcode para subir el archivo IPA a App Store Connect.

### Windows
1. En Visual Studio, haz clic derecho en el proyecto Windows.
2. Selecciona "Publicar" y sigue las instrucciones para crear un paquete MSIX.
3. Sube el paquete MSIX a la Microsoft Store o distribúyelo manualmente.

## Notas Adicionales
- Asegúrate de actualizar las claves de firma y los certificados según sea necesario.
- Revisa las políticas de las tiendas de aplicaciones antes de publicar.
