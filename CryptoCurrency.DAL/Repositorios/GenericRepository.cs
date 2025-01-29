using CryptoCurrency.DAL.DBContext;
using CryptoCurrency.DAL.Repositorios.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.DAL.Repositorios
{
    public class GenericRepository<TModelo> : IGenericRepository<TModelo> where TModelo : class
    {
        private readonly PruebaBgContext _dbpcontext;

        public GenericRepository(PruebaBgContext dbpcontext)
        {
            _dbpcontext = dbpcontext;
        }
        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                TModelo modelo = await _dbpcontext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                _dbpcontext.Set<TModelo>().Add(modelo);
                await _dbpcontext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                _dbpcontext.Set<TModelo>().Update(modelo);
                await _dbpcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> Eliminar(TModelo modelo)
        {
            try
            {
                _dbpcontext.Set<TModelo>().Remove(modelo);
                await _dbpcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModelo> queryModelo = filtro == null ? _dbpcontext.Set<TModelo>()
                                                                 : _dbpcontext.Set<TModelo>().Where(filtro);
                return queryModelo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
