using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PisDataAccess;

namespace CargarPermiso
{
    class Program
    {
        public static String PLATAFORMA = "android_test";
        public static String PATH_PERMISO = "C:\\Users\\marcelo\\Downloads\\WMF.p12";
        static void Main(string[] args)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(PATH_PERMISO);
                byte[] contenido = new byte[fs.Length];
                fs.Read(contenido, 0, Convert.ToInt32(fs.Length));
                using (var context = new DevelopmentPISEntities())
                {
                    context.PermissionSet.Add(new Permission()
                    {
                        Platform = PLATAFORMA,
                        Content = contenido
                    });
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
    }
}
