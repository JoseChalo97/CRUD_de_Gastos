using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_de_Gastos
{
    public class Gasto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
        public decimal Monto { get; set; }
        public string Categoria { get; set; } = "";

        public override string ToString()
        {
            return $"{Id,-3} {Descripcion,-20} Q {Monto,7:N2}  {Categoria}";
        }
    }
}
