using System;
using System.Collections.Generic;
using System.Text;
namespace AutoPeca.BE
{
    public class VeiculoBE : BaseBE
    {
        private VO.Veiculo vo;
        private DAO.VeiculoDAO dao;

        public VeiculoBE(VO.Veiculo vo)        {         
            this.vo = vo;        
        }

        public void incluir()
        {
            if(string.IsNullOrEmpty(this.vo.modelo))
            {
                throw new Exception("Modelo do veículo Obrigatorio");
            }

            dao = new DAO.VeiculoDAO(this.vo);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.VeiculoDAO(this.vo);
            dao.alterar();
        }
        public VO.Veiculo carregar(int id)
        {
            dao = new DAO.VeiculoDAO(this.vo);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.VeiculoDAO(this.vo);
            dao.remover(id);
        }

        public List<VO.Veiculo> listar()
        {
            dao = new DAO.VeiculoDAO(this.vo);
            return dao.listar();
        }
    }
}
