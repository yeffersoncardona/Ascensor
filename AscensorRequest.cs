using System.Net.NetworkInformation;

namespace AscensorApi
{
    public class AscensorRequest
    {
        public int Floor { get; set; }
        public string Direction { get; set; } // "up" or "down"

        private AscensorEstado ascensorEstado = new AscensorEstado();
        private static Queue<AscensorRequest> _requests = new Queue<AscensorRequest>();
        public AscensorEstado ProcessRequests(AscensorRequest Asensorrequest)
        {
            _requests.Enqueue(Asensorrequest);

            if (ascensorEstado.CurrentFloor == Asensorrequest.Floor )
            { //Direction.Equals("UP") && 
                ascensorEstado.description = ascensorEstado.description + " \n " + " el ascensor se encuentra en el mismo piso por lo tanto no se movera";
                return ascensorEstado;
            }
            while (_requests.Count > 0)
                  {
                      var request = _requests.Dequeue(); // Mover el ascensor al piso solicitado
                      MoveElevator(request.Floor); // Abrir y cerrar las puertas al llegar al piso solicitado
                      OpenDoors();
                      CloseDoors();
                  }
           
            return ascensorEstado;
        }
        public void MoveElevator(int floor)
        { // Lógica para mover el ascensor // Simulación del movimiento del ascensor entre pisos
            while (ascensorEstado.CurrentFloor != floor)
            {
                if (ascensorEstado.CurrentFloor < floor)
                { 
                    ascensorEstado.CurrentFloor++;
                    ascensorEstado.description = ascensorEstado.description + " \n Subiendo... piso actual " + ascensorEstado.CurrentFloor;
                }
                else
                {
                    ascensorEstado.CurrentFloor--;
                    ascensorEstado.description = ascensorEstado.description + " \n Descendiendo... piso actual " + ascensorEstado.CurrentFloor;
                }
                // Simulación del tiempo de movimiento entre pisos
                Thread.Sleep(20000); // Simula 20 segundo por piso // Verificar si hay solicitudes para detenerse en el piso actual
                
                if (_requests.Any(r => r.Floor == ascensorEstado.CurrentFloor))
                {
                    ascensorEstado.description = ascensorEstado.description + " \n deteniendose en el ... piso  " + ascensorEstado.CurrentFloor +" parada por llamada";
                    break;
                }
            }
        }
        public void OpenDoors()
        {
            ascensorEstado.description = ascensorEstado.description + " \n abriendo puertas";
            ascensorEstado.DoorsOpen = true; // Simulación del tiempo de apertura de puertas
            Thread.Sleep(2000); // Simula 2 segundos para abrir las puertas
            ascensorEstado.description = ascensorEstado.description + " \n  puertas abiertas";
        }
        public void CloseDoors()
        {
            ascensorEstado.description = ascensorEstado.description + " \n cerrando puertas";
            ascensorEstado.DoorsOpen = false; // Simulación del tiempo de cierre de puertas
            Thread.Sleep(2000); // Simula 2 segundos para cerrar las puertas
            ascensorEstado.description = ascensorEstado.description + " \n  puertas cerradas"; 
        }

    }
}
