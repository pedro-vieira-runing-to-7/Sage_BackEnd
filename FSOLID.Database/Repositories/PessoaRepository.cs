﻿using FSOLID.Commom.Pagging;
using FSOLID.Database.Context;
using FSOLID.Domain.Entities;
using FSOLID.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FSOLID.Database.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly FSolidContext _context;
        public PessoaRepository(FSolidContext context)
        {
            _context = context;
        }
        public Guid Add(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            return pessoa.Id;
        }

        public void Delete(Guid id)
        {
            var pessoa = _context.Pessoas.Where(w => w.Id == id).FirstOrDefault();

            if (pessoa != null && pessoa.Endereco != null)
            {
                foreach (var item in pessoa.Endereco)
                {
                    _context.Enderecos.Remove(item);
                }
            }

            _context.Pessoas.Remove(pessoa);
        }

        public Pessoa Get(Guid id)
        {
            var pessoa = _context.Pessoas
                         .Include(e => e.Endereco)
                         .Where(w => w.Id == id).FirstOrDefault();

            if (pessoa != null && pessoa.Endereco != null)
            {
                foreach (var item in pessoa.Endereco)
                {
                    item.IdPessoaNavigation = null;
                    item.IdEstadoNavigation = _context.Estados.Where(w => w.Id == item.IdEstado).FirstOrDefault();
                    item.IdEstadoNavigation.Endereco = null;
                }
            }

            return pessoa;
        }

        public PagedResult<Pessoa> Get(int actualPage, int pageSize)
        {
            return _context.Pessoas.GetPaged(actualPage, pageSize);
        }


        public void Update(Pessoa pessoa)
        {
            _context.Pessoas.Update(pessoa);
        }
    }
}
