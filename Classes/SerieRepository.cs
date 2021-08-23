using System;
using System.Collections.Generic;
using DIO.Series.Interfaces;

namespace DIO.Series.Classes
{
    public class SerieRepository : IRepository<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();
        public void Atualizar(int id, Serie entidade)
        {
            listaSerie[id] = entidade;
        }

        public void Exclui(int id)
        {
            Serie serie = listaSerie.Find(s => s.RetornaId() == id);
            if (serie is null)
            {
                throw new Exception($"Série com o id '{id}' não encontrado!");
            }
            else if (serie.RetornaExcluido() == true)
            {
                throw new Exception($"Está série ja foi excluída!");
            }
            else
            {
                listaSerie[id].Excluir();
            }
        }

        public void Inserir(Serie entidade)
        {
            listaSerie.Add(entidade);
        }

        public List<Serie> Lista()
        {
            return listaSerie.FindAll(s => s.RetornaExcluido() != true);
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }

        public Serie RetornaPorId(int id)
        {
            Serie serie = listaSerie.Find(s => s.RetornaId() == id);
            if (serie is null)
            {
                throw new Exception($"Série com o id '{id}' não encontrado!");
            }
            else
            {
                return serie;
            }
        }
    }
}