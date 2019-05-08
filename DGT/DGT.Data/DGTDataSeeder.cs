using System;
using System.Collections.Generic;
using System.Linq;
using DGT.Data.Entities;

namespace DGT.Data
{
    class DGTDataSeeder
    {
        DGTContext _dgtContext;

        public DGTDataSeeder(DGTContext dgtContext)
        {
            _dgtContext = dgtContext;
        }

        public void Seed()
        {
            if (_dgtContext.Conductores.Any())
            {
                return;
            }

            try
            {
                for (int i = 0; i < _conductores.Count; i++)
                {
                    _dgtContext.Conductores.Add(_conductores[i]);
                }
                _dgtContext.SaveChanges();

                for (int y = 0; y < _vehiculos.Count; y++)
                {
                    _dgtContext.Vehiculos.Add(_vehiculos[y]);
                }
                _dgtContext.SaveChanges();
                for (int x = 0; x < _infracciones.Count; x++)
                {
                    _dgtContext.Infracciones.Add(_infracciones[x]);
                }
                _dgtContext.SaveChanges();
                for (int z = 0; z < _conductoresInfracciones.Count; z++)
                {
                    _dgtContext.ConductoresInfracciones.Add(_conductoresInfracciones[z]);
                }
                _dgtContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static IList<Infraccion> _infracciones = new List<Infraccion>()
        {
            new Infraccion { Descripcion = "Rebasar el límite de velocidad permitido", Puntos = 2 },
            new Infraccion { Descripcion = "Estacionamiento en zonas prohibidas", Puntos = 5 },
            new Infraccion { Descripcion = "Distracciones al volante por el uso del celular u otro tipo de dispositivos", Puntos = 3 },
            new Infraccion { Descripcion = "Limite de velocidad", Puntos = 6 },
            new Infraccion { Descripcion = "No usar el cinturón de seguridad", Puntos = 4 },
            new Infraccion { Descripcion = "Manejar sin poseer licencia destinada a este fin.", Puntos = 1 },
            new Infraccion { Descripcion = "No respetar las luces de los semáforos ignorando sus indicaciones", Puntos = 8 }
        };

        private static IList<Conductor> _conductores = new List<Conductor>()
        {
            new Conductor {DNI = "06543360K", Nombres = "Yair", Apellidos = "Lamb"},
            new Conductor {DNI = "X9981928C", Nombres = "Leonardo", Apellidos = "Payne"},
            new Conductor {DNI = "A02035574", Nombres = "Carl", Apellidos = "Henson"},
            new Conductor {DNI = "94267190B", Nombres = "Jensen", Apellidos = "Combs"},
            new Conductor {DNI = "Y0223406K", Nombres = "Amiah", Apellidos = "Burton"},
            new Conductor {DNI = "H36251163", Nombres = "Yaretzi", Apellidos = "Mayo"},
            new Conductor {DNI = "X3919923X", Nombres = "Kamren", Apellidos = "Huffman"}
        };

        private static IList<ConductorInfraccion> _conductoresInfracciones = new List<ConductorInfraccion>()
        {
            new ConductorInfraccion(){ Conductor = _conductores[0], Infraccion = _infracciones[0], Fecha = DateTime.Now },
            new ConductorInfraccion(){ Conductor = _conductores[0], Infraccion = _infracciones[1], Fecha = DateTime.Now.AddDays(1) },
            new ConductorInfraccion(){ Conductor = _conductores[1], Infraccion = _infracciones[2], Fecha = DateTime.Now.AddDays(2) },
            new ConductorInfraccion(){ Conductor = _conductores[1], Infraccion = _infracciones[3], Fecha = DateTime.Now.AddDays(3) },
            new ConductorInfraccion(){ Conductor = _conductores[2], Infraccion = _infracciones[4], Fecha = DateTime.Now.AddDays(4) },
            new ConductorInfraccion(){ Conductor = _conductores[3], Infraccion = _infracciones[5], Fecha = DateTime.Now.AddDays(5) },
            new ConductorInfraccion(){ Conductor = _conductores[4], Infraccion = _infracciones[6], Fecha = DateTime.Now.AddDays(6) }
        };

        private static IList<Vehiculo> _vehiculos = new List<Vehiculo>()
        {
            new Vehiculo { Matricula = "0035HHC", Marca = "Abarth", Modelo = "X", Conductor = _conductores[0]},
            new Vehiculo { Matricula = "7273DHB", Marca = "Acura", Modelo = "B", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "4925KCJ", Marca = "Alfa Romeo", Modelo = "F", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "6692JDH", Marca = "Aston Martin", Modelo = "R", Conductor = _conductores[3]},
            new Vehiculo { Matricula = "6560CDD", Marca = "Audi", Modelo = "E", Conductor = _conductores[4]},
            new Vehiculo { Matricula = "8239GDF", Marca = "BAIC", Modelo = "Q", Conductor = _conductores[6]},
            new Vehiculo { Matricula = "5343BKC", Marca = "Bentley", Modelo = "A", Conductor = _conductores[2]},
            new Vehiculo { Matricula = "4062HHD", Marca = "BMW", Modelo = "Z", Conductor = _conductores[5]},
            new Vehiculo { Matricula = "2325HDJ", Marca = "Buick", Modelo = "D", Conductor = _conductores[0]},
            new Vehiculo { Matricula = "6071DKC", Marca = "Cadillac", Modelo = "S", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "6381FBB", Marca = "Chang'an", Modelo = "T", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "8227GBJ", Marca = "Chevrolet", Modelo = "G", Conductor = _conductores[3]},
            new Vehiculo { Matricula = "0706KBC", Marca = "Chrysler", Modelo = "B", Conductor = _conductores[4]},
            new Vehiculo { Matricula = "3417KCC", Marca = "Dodge", Modelo = "N", Conductor = _conductores[6]},
            new Vehiculo { Matricula = "0145CBK", Marca = "DFSK", Modelo = "Y", Conductor = _conductores[2]},
            new Vehiculo { Matricula = "5894KGK", Marca = "FAW", Modelo = "U", Conductor = _conductores[5]},
            new Vehiculo { Matricula = "6148DCJ", Marca = "Ferrari", Modelo = "I", Conductor = _conductores[0]},
            new Vehiculo { Matricula = "3148CCJ", Marca = "Fiat", Modelo = "O", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "2414BJK", Marca = "Ford", Modelo = "U", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "6391HHB", Marca = "GMC", Modelo = "W", Conductor = _conductores[3]},
            new Vehiculo { Matricula = "0703BCK", Marca = "Honda", Modelo = "H", Conductor = _conductores[4]},
            new Vehiculo { Matricula = "4025JBF", Marca = "Hyundai", Modelo = "M", Conductor = _conductores[6]},
            new Vehiculo { Matricula = "0039KKH", Marca = "Infiniti", Modelo = "N", Conductor = _conductores[2]},
            new Vehiculo { Matricula = "7359HGF", Marca = "JAC", Modelo = "L", Conductor = _conductores[5]},
            new Vehiculo { Matricula = "9044CJK", Marca = "Jaguar", Modelo = "P", Conductor = _conductores[0]},
            new Vehiculo { Matricula = "9455DDK", Marca = "Jeep", Modelo = "34", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "6956JJD", Marca = "Kia", Modelo = "1", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "0809CKK", Marca = "Lamborghini", Modelo = "5", Conductor = _conductores[3]},
            new Vehiculo { Matricula = "8419KDG", Marca = "Land Rover", Modelo = "8", Conductor = _conductores[4]},
            new Vehiculo { Matricula = "2495KDD", Marca = "Lincoln", Modelo = "3", Conductor = _conductores[6]},
            new Vehiculo { Matricula = "9699HKJ", Marca = "Lotus", Modelo = "98", Conductor = _conductores[2]},
            new Vehiculo { Matricula = "8370BFG", Marca = "Maserati", Modelo = "45", Conductor = _conductores[5]},
            new Vehiculo { Matricula = "2957CBH", Marca = "Mazda", Modelo = "55", Conductor = _conductores[0]},
            new Vehiculo { Matricula = "3623GFC", Marca = "McLaren Automotive", Modelo = "6", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "5229BJJ", Marca = "Mercedes-Benz", Modelo = "77", Conductor = _conductores[1]},
            new Vehiculo { Matricula = "5916CGH", Marca = "MINI", Modelo = "22", Conductor = _conductores[3]},
            new Vehiculo { Matricula = "6843GKJ", Marca = "Mitsubishi Motors", Modelo = "8", Conductor = _conductores[4]},
            new Vehiculo { Matricula = "5070HBD", Marca = "Nissan", Modelo = "F1", Conductor = _conductores[6]},
            new Vehiculo { Matricula = "9824FGK", Marca = "Peugeot", Modelo = "S1", Conductor = _conductores[2]},
            new Vehiculo { Matricula = "7690GJJ", Marca = "Porsche", Modelo = "D3", Conductor = _conductores[5]},
            new Vehiculo { Matricula = "4198BFJ", Marca = "Ram", Modelo = "P2", Conductor = _conductores[0]},
            new Vehiculo { Matricula = "5836GHK", Marca = "Renault", Modelo = "BT", Conductor = _conductores[2]}
        };
    }
}
