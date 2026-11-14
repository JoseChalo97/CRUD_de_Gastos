using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_de_Gastos
{
    public class curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Cuota { get; set; }
        public string Instructor { get; set; } = "";

        public override string ToString()
        {
            return $"{Id,-3} {Nombre,-20} ({Instructor}) - Q {Cuota,7:N2}";
        }
    }
}
