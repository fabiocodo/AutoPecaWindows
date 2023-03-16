using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.DAO
{
    public class VeiculoDAO
    {
        public void incluir()
        {

        }
        public void alterar()
        {

        }
        public VO.Veiculo carregar()
        {
            VO.Veiculo vo = new VO.Veiculo();
            return vo;
        }
        public List<VO.Veiculo> listar()
        {
            if (DAO.listaVeiculo == null)
            {
                DAO.listaVeiculo = new List<VO.Veiculo>();
            }
            return DAO.listaVeiculo;
        }
    }
}
