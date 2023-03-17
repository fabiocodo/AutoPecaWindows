using System;
using System.Collections.Generic;
using System.Text;
namespace AutoPeca.DAO
{
    public class VeiculoDAO : BaseDAO
    {
        private VO.Veiculo vo;
        public VeiculoDAO(VO.Veiculo vo) {

            if (DAO.listaVeiculo == null)
            {
                DAO.listaVeiculo = new List<VO.Veiculo>();
            }
            this.vo = vo;
        }
        public void incluir()
        {
            DAO.listaVeiculo.Add(vo);
        }
        public void alterar()
        {
            
        }
        public void remover(int id)
        {
            DAO.listaVeiculo.RemoveAt(id);
        }
       
        public VO.Veiculo carregar(int id)
        {
            return DAO.listaVeiculo[id];
        }
        public List<VO.Veiculo> listar()
        {           
            return DAO.listaVeiculo;
        }
    }
}
