using Fiap.Console.IocSample;

class Program
{
    static void Main()
    {
        IMensageiro mensageiro = new MensageiroWhats();
        EnviandoMensagem enviador = new EnviandoMensagem(mensageiro);
        enviador.Enviar("Olá, mundo!");
    }


}