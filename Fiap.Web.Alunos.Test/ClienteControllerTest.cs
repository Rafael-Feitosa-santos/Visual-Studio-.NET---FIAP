using Fiap.Web.Alunos.Data;
using Fiap.Web.Alunos.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Web.Alunos.Test
{
    public class ClienteControllerTest
    {

        private readonly Mock<DatabaseContext> _mockContext;

        private readonly ClienteControllerTest _controller;

        private readonly DbSet<ClienteModel> _mockSet;

        public ClienteControllerTest()
        { 

        }

      


    }
}
