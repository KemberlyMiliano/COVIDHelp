using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.Models
{
    public class Help
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("dirrecion")]
        public string Dirrecion { get; set; }

        [JsonProperty("posicion")]
        public string Posicion { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("cedula")]
        public Int64 Cedula { get; set; }

        [JsonProperty("descripcionProblema")]
        public string DescripcionProblema { get; set; }

        [JsonProperty("nombreVoluntario")]
        public string NombreVoluntario { get; set; }

        [JsonProperty("telefonoVoluntario")]
        public string TelefonoVoluntario { get; set; }

        [JsonProperty("emailVoluntario")]
        public string EmailVoluntario { get; set; }

        [JsonProperty("posicionVoluntario")]
        public string PosicionVoluntario { get; set; }

        [JsonProperty("cedulaVoluntario")]
        public Int64 CedulaVoluntario { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("gravedad")]
        public int Gravedad { get; set; }

        [JsonProperty("fechaEnviado")]
        public DateTime FechaEnviado { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonIgnore]
        public double State
        {
            get
            {
                switch (Status)
                {
                    case "Activo":
                        {
                            return 0;
                        }
                    case "Proceso":
                        {
                            return 0.5;

                        }
                    case "Completado":
                        {
                            return 1;

                        }
                    default:

                        return 0;
                }
            }
        }
    }
}
