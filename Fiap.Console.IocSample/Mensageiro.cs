﻿using Fiap.Console.IocSample;

public class Mensageiro : IMensageiro
{
    public void EnviarMensagem(string mensagem)
    {
        Console.WriteLine(mensagem);
    }

}