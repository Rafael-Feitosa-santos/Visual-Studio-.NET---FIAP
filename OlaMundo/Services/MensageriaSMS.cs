using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlaMundo.Services
{
    public class MensageriaSMS
    {
        public bool EnviarMensagem(String texto)
        {
            Console.WriteLine("Enviando SMS: " + texto);
            return true;
        }
    }
}
