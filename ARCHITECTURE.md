# Arquitectura de la Aplicación de Generación de Recibos

## Visión General
Esta aplicación sigue el patrón de arquitectura MVVM (Model-View-ViewModel) y está construida utilizando .NET MAUI.

## Componentes Principales

### Modelos
Los modelos representan los datos y la lógica de negocio. Se encuentran en la carpeta `Models`.

- `Recibo`: Representa un recibo completo.
- `TallerInfo`: Contiene información sobre el taller.
- `ClienteInfo`: Almacena datos del cliente.
- `DetalleServicio`: Representa un ítem individual en el recibo.

### Vistas
Las vistas son las interfaces de usuario y se encuentran en la carpeta `Views`.

- `MainPage`: Página principal que muestra la lista de recibos.
- `CrearEditarReciboPage`: Formulario para crear o editar recibos.
- `PrevisualizarReciboPage`: Muestra una vista previa del recibo.

### ViewModels
Los ViewModels actúan como intermediarios entre los Modelos y las Vistas. Están en la carpeta `ViewModels`.

- `MainViewModel`: Maneja la lógica de la página principal.
- `CrearEditarReciboViewModel`: Gestiona la creación y edición de recibos.
- `PrevisualizarReciboViewModel`: Controla la vista previa del recibo.

### Servicios
Los servicios manejan operaciones de datos y lógica de negocio compleja. Se encuentran en la carpeta `Services`.

- `LiteDBService`: Maneja todas las operaciones de base de datos.
- `PDFService`: Se encarga de la generación de PDFs.
- `ValidacionService`: Proporciona validación de datos.

## Flujo de Datos
1. El usuario interactúa con una Vista.
2. La Vista notifica al ViewModel sobre la acción del usuario.
3. El ViewModel procesa la acción, interactuando con los Servicios si es necesario.
4. Los Servicios realizan operaciones en los Modelos o en recursos externos (como la base de datos).
5. El ViewModel actualiza sus propiedades con los nuevos datos.
6. La Vista se actualiza automáticamente debido al enlace de datos (data binding).

## Patrones y Prácticas
- Inyección de Dependencias: Utilizada para la gestión de servicios y ViewModels.
- Comandos: Implementados usando `RelayCommand` para manejar acciones del usuario.
- Observables: Utilizados para la notificación de cambios en propiedades.
