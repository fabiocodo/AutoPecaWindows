using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    class FabricanteDAO : BaseDAO
    {
        private VO.Fabricante vo;
        public FabricanteDAO(VO.Fabricante vo)
        {

            if (DAO.listaVeiculo == null)
            {
                DAO.listaVeiculo = new List<VO.Veiculo>();
            }
            this.vo = vo;
        }

        public void incluir()
        {
            try
            {
                string sql = "insert into tb_fabricante (NM_NOME,NM_DESCRICAO) " +
                    "values (@Mod,@desc)";
                db.AddParameter("@Mod", vo.nome, ParameterDirection.Input);
                db.AddParameter("@desc", vo.descricao, ParameterDirection.Input);

                db.Execute(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
        public void alterar()
        {
            try
            {
                string sql = "update tb_fabricante set " +
                    "NM_NOME = @nome," +
                    "NM_DESCRICAO = @desc " +
                    "where ID = @id";
                db.AddParameter("@nome", vo.nome, ParameterDirection.Input);
                db.AddParameter("@desc", vo.descricao, ParameterDirection.Input);
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);

                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
        public void remover(int id)
        {
            try
            {
                string sql = $"delete from tb_fabricante where ID = @id";
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        public VO.Fabricante carregar(int id)
        {
            string sql = $"SELECT id,NM_NOME,NM_DESCRICAO from tb_fabricante where id=@id";
            db.AddParameter("@id", id, ParameterDirection.Input);
            try
            {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                        vo = LoadFabricante(dr);
                    }
                    return vo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Fabricante LoadFabricante(DbDataReader dr)
        {
            vo = new VO.Fabricante();
            vo.codigo = Convert.ToInt32(dr["ID"]);
            vo.nome = dr["NM_NOME"] != DBNull.Value ? dr["NM_NOME"].ToString() : "";
            vo.descricao = dr["NM_DESCRICAO"] != DBNull.Value ? dr["NM_DESCRICAO"].ToString():"";
            return vo;
        }

        public List<VO.Fabricante> listar()
        {
            try
            {
                string sql = "SELECT * FROM tb_fabricante;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Fabricante>();

                    while (dr.Read())
                    {
                        vo = LoadFabricante(dr);
                        objResultado.Add(vo);
                    }
                    return objResultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
    }
}