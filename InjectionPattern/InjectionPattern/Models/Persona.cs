using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjectionPattern.Models
{
    public class Persona
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public DateTime HoyDia { get; set; }
        public string DescripcionLarga { get; set; }
        public bool WithInjection { get; set; }
    }
}
