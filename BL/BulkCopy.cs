using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BulkCopy
    {
        public static ML.Result BulkCopySql(string path)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Leemos el archivo y creamos nuestro datatable
                DataTable DT = CSVtoDT(path);

                //Lo mandamos a la base de datos
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    context.Open();
                    using(SqlTransaction transaction = context.BeginTransaction())
                    {
                        using(SqlBulkCopy bulkCopy = new SqlBulkCopy(context, SqlBulkCopyOptions.Default, transaction))
                        {
                            try
                            {
                                bulkCopy.DestinationTableName = "Data";
                                bulkCopy.WriteToServer(DT);
                                transaction.Commit();
                                result.Correct = true;
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                context.Close();
                                result.Correct = false;
                                throw;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static DataTable CSVtoDT(string path)
        {
            //Se crea el DT
            DataTable dt = new DataTable(); 
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    //Creamos el header del DT
                    string[] encabezados = reader.ReadLine().Split(',');

                    //Agregamos la columnas al DT
                    foreach (string header in encabezados)
                    {
                        dt.Columns.Add(header.Trim());
                    }

                    //Se leen las demas lineas y las agrega al DT
                    while (!reader.EndOfStream)
                    {
                        string[] rows = reader.ReadLine().Split(',');
                        DataRow dataRow = dt.NewRow();

                        dataRow[0] = rows[0].ToString().Replace('\"', ' ').Trim();
                        dataRow[1] = rows[1].ToString().Replace('\"', ' ').Trim();
                        dataRow[2] = rows[2].ToString().Replace('\"', ' ').Trim();
                        dataRow[3] = rows[3].ToString().Replace('\"', ' ').Trim();

                        //Para la fecha
                        string[] Date = rows[4].Split(' ');
                        string data = Date[1].Replace('\"', ' ').Trim();
                        DateTime fechaConvertida = DateTime.ParseExact(data, "dd-MMM-yy", CultureInfo.InvariantCulture);
                        dataRow[4] = fechaConvertida.ToString("yyyy/MM/dd");

                        dataRow[5] = decimal.Parse(rows[5].Replace('\"', ' ').Trim());
                        dataRow[6] = int.Parse(rows[6].Replace('\"', ' ').Trim());
                        dataRow[7] = decimal.Parse(rows[7].Replace('\"', ' ').Trim());
                        dataRow[8] = int.Parse(rows[8].ToString().Replace('\"', ' ').Trim());
                        dataRow[9] = decimal.Parse(rows[9].Replace('\"', ' ').Trim());
                        dataRow[10] = rows[10].ToString().Replace('\"', ' ').Trim();
                        dataRow[11] = rows[11].ToString().Replace('\"', ' ').Trim();
                        dataRow[12] = rows[12].ToString().Replace('\"', ' ').Trim();
                        dataRow[13] = decimal.Parse(rows[13].Replace('\"', ' ').Trim());
                        dataRow[14] = rows[14].ToString().Replace('\"', ' ').Trim();
                        dataRow[15] = rows[15].ToString().Replace('\"', ' ').Trim();

                        dt.Rows.Add(dataRow);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el archivo CSV: " + ex.Message);
            }
            return dt;
        }
    }
}
