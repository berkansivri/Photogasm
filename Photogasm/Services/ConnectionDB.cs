using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Photogasm
{
    public class ConnectionDB
    {
        
            public static void OpenClose()
            {
                try
                {
                    switch (SqlTask.conn.State)
                    {

                        case ConnectionState.Closed:
                        SqlTask.conn.Open();
                            break;

                        case ConnectionState.Open:
                        SqlTask.conn.Close();
                        break;
                    }
                }
                catch { throw; }
            }

    }
}