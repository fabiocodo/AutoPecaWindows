using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.DAO
{
    public class BaseDAO
    {
        public DataAccess.DataAccessLayer db { get; }

        public BaseDAO() {
            db = new DataAccess.DataAccessLayer();
        }

    }
}
