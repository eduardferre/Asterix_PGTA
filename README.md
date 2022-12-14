# AsterixDecoder (PGTA)

El lenguaje usado para el proyecto ha sido C# (en Visual Studio C# 2019) haciendo uso de WindowForms y de la extensión GMAP.NET para la parte gràfica del trabajo.

El programa desarrollado, AsterixDecoder, es un decodificador de ficheros ASTERIX (.ast) CAT10 y CAT21. El programa a la entrada de un fichero .ast lee los datos, los decodifica en hexadecimal y los interpreta según cada DataItem. La información que obtenemos de dicha decodificación es guardada en Tablas de Datos (DataTable) para su futuro uso mediante listados en un DataGridView. Una vez se han realizado todos los cálculos sobre dichos DataItems la información puede ser mostrada en tablas individuales según la categoria: CAT10 y CAT21.

Cada tabla permite filtrar por Track Number, TargetID, Target Address y Mode 3/A, exceptuando CAT10 que también permite filtrar entre tráfico SMR y MLAT. En dichas tablas de datos también es posible hacer click en una celda para ver la información de forma más detallada (sólo en aquellas celdas cuyo contenido sea "Click for more data"). Ambas tablas tienen la funcionalidad de exportación a CSV, para un uso de los datos fuera del programa.

Por lo que hace a la pestaña del mapa consiste en un mapa interactivo (GMAP.NET) en el que la información de las trayectorias de cada vehículo es mostrada gráficamente. En dicho mapa se simulan dichas trayectorias pudiendo empezar, pausar, reiniciar, adelantar a una hora en concreto, cambiar la velocidad de la simulación, seleccionar tipo de tráfico, ver la información del vehículo... También en dicha pestaña es posible hacer una exportación a formato KML.

# PLANIFICACIÓN

El proyecto ha consistido en la programación de un programa capaz de leer ficheros Asterix y decodificarlos según los criterios indicados en los documentos pertinentes. Una vez hecha dicha decodificación se ha procedido con la verificación de los datos y finalmente se han procesado para listar cada DataItem a su mensaje correspondiente. Una vez los datos ya han sido procesados se empieza con la sintetización de los DataItems más relevantes, y así mostrar las trayectorias en el mapa. 

La planificación del proyecto se ha llevado a cabo semanalmente, por lo que cada semana se marcó un objetivo a presentar en las sesiones con el profesor. 

PLANIFICACIÓN (12 semanas):

1. Lectura fichero SMR CAT10
2. Verificación SMR CAT10
3. Lectura fichero MLAT CAT10
4. Verificación MLAT CAT10
5. Lectura fichero ADSB CAT21
6. Verificación ADSB CAT21
7. Listado CAT10 y CAT21
8. Optimización de código
9. Cálculo de trayectorias
10. Graficado de los mapas
11. Filtraje CAT10 y CAT21
12. Filtraje SMR y MLAT
13. Simulación (+ funcionalidades extra)
14. Funcionalidades extra
15. Memória escrita

En la siguiente gráfica se puede ver la evolución:

<img width="509" alt="image" src="https://user-images.githubusercontent.com/73181261/206764583-4d294939-c126-4888-a839-7d75ec52b9c5.png">
<img width="770" alt="image" src="https://user-images.githubusercontent.com/73181261/206764272-a2856496-0bbd-4638-8b7e-fbbc65a8d6ec.png">

Las primeras 3 semanas se invirtió el tiempo en la definicion de cada DataItem y de las funciones necesarias para CAT10. Una vez CAT10 estuvo definida, se procedió a su decodificación y lectura. 
Las próximas 3 semanas se definieron los DataItem y las funciones para CAT21. La decodificación de CAT21 culminó con el listado de la misma. Se verificaron los datos de ambas categorias con el programa AsterixInspector. 
Las semanas 7 y 8 se reservaron para la optimización del código y para resolver un par de conflictos. En el momento en que el código ya fue suficientemente robusto se empezó con la segunda parte del proyecto: trayectorias y mapas.
El cálculo de trayectorias y el graficado de los mapas se llevaron a cabo durante las semanas 9, 10 y 11, en paralelo se fueron añadiendo funcionalidades extra al proyecto. Una vez el graficado estuvo disponible y verificado, se programaron las funcionalidades extra de la simulación.
La última semana se dedicó a acabar dichas funcionalidades extra y a resolver posibles errores de robustez.

Dicho proyecto ha acabado con un total de 159 horas, repartido entre los 3 miembros del grupo de la siguiente forma (teniendo en cuenta que la compañera Catalina Mosteiro se reubicó del proyecto 1 a los proyectos 2 y 3 en la octava semana):

<img width="622" alt="image" src="https://user-images.githubusercontent.com/73181261/206767056-e2ffd50f-bc2f-4ee3-9922-6d46a38c3778.png">
