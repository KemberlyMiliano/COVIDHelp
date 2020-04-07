using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.Models
{
    public class NeedHelp
    {
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public int Dirrecion { get; set; }
        public string Posicion { get; set; }
        public string Email { get; set; }
        public Int64 Cedula { get; set; }
        public string DescripcionProblema { get; set; }
    
        public string NombreVoluntario { get; set; }
        public int TelefonoVoluntario { get; set; }
        public string EmailVoluntario { get; set; }
        public string PosicionVoluntario { get; set; }
        public Int64 CedulaVoluntario { get; set; }
        public bool IsActiveHelp { get; set; }
        public DateTime FechaEnviado { get; set; }
    }
}
