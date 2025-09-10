# Práctica 1 - Programación de Software 

## 📂 Estructura del Proyecto
```text
ejercicio_1/
│
├── EspacioParqueo.cs   # Clase abstracta base
├── EspacioCarro.cs     # Clase concreta que hereda de EspacioParqueo
├── IPagable.cs         # Interface para pagos
├── IAuditable.cs       # Interface para auditoría
├── Ticket.cs           # Clase que representa tickets de entrada/salida
├── Parqueadero.cs      # Administra espacios y tickets
├── TipoEspacio.cs      # Enumeración con tipos de espacios
└── Program.cs          # Menú principal (consola)
```




## 🎯 Funcionalidades del Menú
1. Registrar entrada de vehículo.  
2. Registrar salida y cobro.  
3. Consultar disponibilidad por tipo de espacio.  
4. Mostrar información de todos los espacios.  
5. Mostrar ingresos totales del día.  
