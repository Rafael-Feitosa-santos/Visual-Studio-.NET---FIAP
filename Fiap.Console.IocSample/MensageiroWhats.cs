
using Fiap.Console.IocSample;

public class MensageiroWhats : IMensageiro
    {
        public void EnviarMensagem(string mensagem)
        {
            Console.WriteLine("Whats: " + mensagem);
        }
    }

