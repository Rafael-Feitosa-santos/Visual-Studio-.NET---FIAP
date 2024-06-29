
using OlaMundo.Resources;
using OlaMundo.Services;

public class Program
{
    public static void Main(string[] args)
    {
        /*int numero = 10;

        if (numero > 5)
        {
            Console.WriteLine("O número é maior que 5.");
        }
        else
        {
            Console.WriteLine("O número é menor ou igual a 5.");
        }

        int diaDaSemana = 3;
        switch (diaDaSemana)
        {
            case 1:
                Console.WriteLine("Segunda-feira");
                break;
            case 2:
                Console.WriteLine("Terça-feira");
                break;
            case 3:
                Console.WriteLine("Quarta-feira");
                break;
            case 4:
                Console.WriteLine("Quinta-feira");
                break;
            case 5:
                Console.WriteLine("Sexta-feira");
                break;
            default:
                Console.WriteLine("Dia inválido");
                break;
        }

        Console.WriteLine("-----------------------------------");

        String[] nomes = { "Matilde", "Rafael", "Davi", "Gabriella" };
        foreach (string str in nomes)
        {
            Console.WriteLine(str);
        }

        Console.WriteLine("-----------------------------------");

        for (int i = 0; i < nomes.Length; i++)
        {
            Console.WriteLine(nomes[i]);
        }*/

        int valorUsuario = 10;
        int valorConta = 4564;

        try
        {
            var totalPorPessoa = valorConta / valorUsuario;
            Console.WriteLine(totalPorPessoa.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Não foi possível executar a operação!");
        }
        finally
        {
            Console.WriteLine("Execução encerrou!");
        }




    }
}