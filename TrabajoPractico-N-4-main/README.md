Trabajo Práctico: Patrones de Diseño en Unity

Una empresa de desarrollo de videojuegos llamada UAI Games ha contratado nuestros servicios para llevar a cabo las siguientes tareas:

En primer lugar, están en proceso de desarrollo de un juego tipo tower defense y desean que implementemos el patrón Object Pool para gestionar la invocación de proyectiles. Nuestra labor principal consistirá en adaptar el código existente, denominado "TurrenAI", para que sea compatible con esta nueva funcionalidad. Cabe destacar que, además de la implementación del patrón, la empresa valora cualquier optimización de código y la aplicación de buenas prácticas para garantizar un código limpio y eficiente.

Por otro lado, en su segundo proyecto, UAI Games se encuentra trabajando en la creación de niveles para su juego. Los niveles ya están construidos, pero están indecisos acerca de la última decoración que debe ubicarse junto a la casa en la escena. Por esta razón, nos han solicitado que apliquemos el patrón Factory, permitiendo a los jugadores invocar entre tres objetos diferentes: un arbusto, un árbol o una roca. Estos objetos deberán ser invocados en la ubicación de un objeto vacío denominado "Factory," y estarán disponibles para su selección a través de botones visibles en pantalla.


Para la Escena "PoolObject":
Objetivo: Implementar el patrón Pool Object para administrar las balas de una ametralladora.
Tareas:
En la escena "PoolObject", hay un objeto de tipo ametralladora. Deben aplicar el patrón Pool Object para gestionar las balas de este objeto.
Deben asegurarse de que los objetos(balas) estén correctamente instanciados pero deshabilitados en la pantalla.
La activación de cada objeto debe manejarse adecuadamente.
Deben implementar el patrón Singleton junto con el patrón Pool Object.
Las balas deben ser llamadas cuando se haga clic con el mouse.
Extra: Si el número de balas supera la cantidad máxima permitida, se deben instanciar balas adicionales.
Para la Escena del Patrón "Factory":
Objetivo: Implementar el patrón Factory para crear objetos 3D en Unity.
Tareas:
Crear tres objetos 3D: Arbusto, Roca y Arbol.
Crear una clase padre llamada "Factory" para la creación de objetos.
Crear tres clases hijas que heredan de la clase "Factory" .Uno para gestionar la creación de Arbustos, otro para gestionar la creación de rocas y para gestionar la creación de árboles.
Deben implementar un botón en la pantalla que llame a las fábricas para crear los objetos correspondientes.
Criterios de Evaluación:
Correcta instanciación de objetos deshabilitados en la pantalla.
Gestión adecuada de la activación y desactivación de objetos.
Uso correcto del patrón Pool Object implementado con un diccionario.
Implementación adecuada del patrón Singleton junto con el patrón Pool Object.
Correcta llamada de balas al hacer clic con el mouse.
Implementación de la creación de objetos 3D utilizando el patrón Factory.
Correcta herencia y uso de las clases para gestionar la creación de esferas y cubos.
Implementación funcional del botón para llamar a las fábricas de objetos.
Recuerden que la creatividad y la claridad en el código también serán tenidas en cuenta en la evaluación. ¡Buena suerte!
