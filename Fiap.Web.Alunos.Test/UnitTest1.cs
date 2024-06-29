namespace Fiap.Web.Alunos.Test
{
    public class UnitTest1
    {
        [Fact]
        public void VerificaMaioridade_DeveRetornarTrueSeMaior()
        {
            // Arrange
            var dataNascimento = new DateTime(2000, 1, 1); // Uma pessoa nascida em 01/01/2000
            var hoje = DateTime.Now;
            var maioridade = hoje.Year - dataNascimento.Year;
            if (dataNascimento > hoje.AddYears(-maioridade)) maioridade--;

            // Act
            var ehMaiorDeIdade = maioridade >= 18;

            //Assert
            Assert.True(ehMaiorDeIdade);
        }

        [Fact]
        public void VerificaMaioridade_DeveRetornarTrueSeMenor()
        {
            // Arrange
            var dataNascimento = new DateTime(2020, 1, 1); // Uma pessoa nascida em 01/01/2020
            var hoje = DateTime.Now;
            var maioridade = hoje.Year - dataNascimento.Year;
            if (dataNascimento > hoje.AddYears(-maioridade)) maioridade--;

            // Act
            var ehMenorIdade = maioridade < 18;

            //Assert
            Assert.True(ehMenorIdade);
        }
    }
}

