# Toolkit-Suite
Un "proof of concept" WPF MVVM para una aplicación que solvente un problema a la hora de generar ficheros de configuración de motores en base a una serie de parámetros escogidos por el cliente.

La aplicación pretende solventar un problema que tiene mi sitio de trabajo, el cual es el siguiente:

Al venderse un motor, el cliente selecciona una serie de parámetros de configuración para que el producto se adapte a sus necesidades (por ejemplo, puede elegir entre distintos tipos de combustible para alimentar el motor).
Esos parámetros se recopilan y publican en un PDF, el cual mi compañero lee y a continuación selecciona, de memoria y de manera manual, una serie de variables que tiene que añadir a un fichero para cargar al motor.
Es decir, que si en el PDF pone que el motor va a usar Gas Natural, el sabe que hay que añadir la variable "CONTROL_AVANCE.W_GASTYPE_1_SEL" con el valor "Natural Gas".

Esto por supuesto plantea una serie de problemas. 
El primero, que consume mucho tiempo a mi compañero, en algo que con un poco de esfuerzo, podría automatizarse en gran medida. 
El segundo, que si mi compañero no está disponible, nadie está capacitado para hacer ese trabajo.

La dificultad que tiene este proyecto, es que existen más de 150 posibles variables que el cliente puede configurar, desembocando en un sin fin de posibles necesidades.
Es por ello que no es suficiente con tener una tabla que vincule unos valores a unas variables; se requiere tener una especie de entorno donde se puedan definir relaciones entre valores y variables de forma cómoda, rápida y visual.

Una vez definidas esas relaciones, el programa podrá recorrer un fichero PDF con las configuraciones del motor, y generar el fichero que contenga todas las variables pertinentes.

Cuando se han obtenido todas las variables necesarias, estas se podrán visualizar y editar en la pestaña view (siempre hay que hacer pequeños retoques).
