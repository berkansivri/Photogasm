using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Photogasm
{
    public class ExifClass
    {
        public static List<ExifDetails> getPhotosExif(string PID)
        {
            List<ExifDetails> _exifItems;
            _exifItems = new List<ExifDetails>();
            SqlTask.conn = new SqlConnection(SqlTask.connString);
            try
            {
                SqlTask.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Details WHERE PID=@_PID", SqlTask.conn);
                cmd.Parameters.AddWithValue("_PID", PID);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _exifItems.Add(new ExifDetails
                    {
                        Camera = dr[1].ToString(),
                        A_Value = dr[4].ToString(),
                        Focal_Rate = dr[3].ToString(),
                        ISO = dr[2].ToString(),
                        S_Value = dr[5].ToString(),
                        P_Date = dr[6].ToString(),
                        H_Resolution = dr[7].ToString(),
                        V_Resolution = dr[8].ToString(),
                        Color_Space = dr[9].ToString(),
                        Bits_Per_Pixel = dr[10].ToString(),
                        Image_Size = dr[11].ToString()
                    });
                }
                else
                {
                    _exifItems.Add(new ExifDetails
                    {
                        Camera = "No Value",
                        A_Value = "No Value",
                        Focal_Rate = "No Value",
                        ISO = "No Value",
                        S_Value = "No Value",
                        H_Resolution = "No Value",
                        V_Resolution = "No Value",
                        Color_Space = "No Value",
                        Bits_Per_Pixel = "No Value",
                        Image_Size = "No Value",
                        P_Date = "No Value"
                    });

                }
                return _exifItems;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                SqlTask.conn.Close();
            }
            
        }
    }
}