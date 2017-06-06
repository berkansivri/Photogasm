using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Photogasm
{
    public class Comment
    {
        public string PID { get; set; }
        public string UID { get; set; }
        public string CText { get; set; }
        public DateTime CDate { get; set; }
        public string UserImg { get; set; }
        public string UserFName { get; set; }
        public string UserSName { get; set; }
        public string UserUrl { get; set; }

        public static bool sendComment(Comment comm)
        {
            SqlTask.conn = new SqlConnection(SqlTask.connString);
            try
            {
                SqlTask.conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Comments VALUES (@PID,@UID,@CText,@CDate)", SqlTask.conn);
                cmd.Parameters.AddWithValue("@PID", comm.PID);
                cmd.Parameters.AddWithValue("@UID", comm.UID);
                cmd.Parameters.AddWithValue("@CText", comm.CText);
                cmd.Parameters.AddWithValue("@CDate", comm.CDate);
                int result = cmd.ExecuteNonQuery();
                SqlTask.conn.Close();
                if (result > 0)

                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlTask.conn.Close();
            }
        }

    }
}