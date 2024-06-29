
namespace Fiap.Console.IocSample
{
    public class EnviandoMensagem
    {
        private readonly IMensageiro _mensageiro;
        
        public EnviandoMensagem(IMensageiro mensageiro)
        {
            _mensageiro = mensageiro;
        }

        public void Enviar(string mensagem)
        {
            _mensageiro.EnviarMensagem(mensagem);
        }

    }
}
