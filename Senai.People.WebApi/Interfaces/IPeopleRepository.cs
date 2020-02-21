using Senai.People.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.People.WebApi.Interfaces
{
    interface IPeopleRepository
    {
        List<PeopleDomain> Listar();

        void Cadastrar(PeopleDomain people);

        void AtualizarIdCorpo(PeopleDomain people);

        void Deletar(PeopleDomain people);

        PeopleDomain BuscarPorId(int id);
    }
}
