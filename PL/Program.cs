using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Presiona una tecla para iniciar el BulkCopy");
            Console.ReadKey();

            string path = "C:/Users/digis/Downloads/DATA TEST (1).CSV";

            ML.Result result = BL.BulkCopy.BulkCopySql(path);
            if(result.Correct)
            {
                Console.WriteLine("Se completo correctamente el BulkCopy");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Hubo un error al realizar el BulkCopy");
                Console.ReadKey();
            }
        }
    }
}
