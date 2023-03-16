using System;
using System.Collections.Generic;
using System.Text;
namespace AutoPeca.BE
{
    public class VeiculoBE : BaseBE
    {
        public void incluir()
        {

        }
        public void alterar()
        {

        }
        public VO.Veiculo carregar()
        {
            VO.Veiculo  vo = new VO.Veiculo();
            return vo;
        }
        public List<VO.Veiculo> listar()
        {
            if (DAO.DAO.listaVeiculo == null)
            {
                DAO.DAO.listaVeiculo = new List<VO.Veiculo>();
            }
            return DAO.DAO.listaVeiculo;
        }
    }
}
