using Generation.Soporte;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

string cadenaConexion = @"Server=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\CloudMonitor\WebApplicationAPI\ClassLibraryDAL\Persistencia.mdf;Integrated Security=True;Connect Timeout=30;";
string rutaDestinoClases = @"D:\CloudMonitor\WebApplicationAPI\ClassLibrary\Transporte\";
string rutaDestinoControladores = @"D:\CloudMonitor\WebApplicationAPI\WebApplicationAPI\Controllers\";
string rutaDestinoContexto = @"D:\CloudMonitor\WebApplicationAPI\ClassLibraryDAL\";

string namespaceAPI = @"WebApplicationAPI.Controllers";
string namespaceTransporte = @"ClassLibrary.Transporte";
string namespaceWrapperAPI = @"ClassLibrary.Solucion";
string namespaceAccesoDatos = @"ClassLibraryDAL";

string rutaAppAngular = @"D:\CloudMonitor\WebApplicationFront\src\app\";
string rutaModelosAngular = @"D:\CloudMonitor\WebApplicationFront\src\app\models\";
string rutaServiciosAngular = @"D:\CloudMonitor\WebApplicationFront\src\app\services\";
string rutaComponentesAngular = @"D:\CloudMonitor\WebApplicationFront\src\app\components\";
//string apiURL = @"https://localhost:7175/";
string apiURL = @"http://localhost:5000/";

string key = null;

Console.WriteLine("Conectando a la base de datos...");

Tabla[] tablas = new Tabla[0];

#region "Obtencion de tablas, columnas e indices de la base de datos"
using (DbContext cont = new DbContext(new DbContextOptionsBuilder<DbContext>().UseSqlServer(cadenaConexion).Options)) {
    bool conexion = cont.Database.CanConnect();
    Console.WriteLine("Conexion: " + (conexion ? "Hecha" : "Error"));
    if (!conexion) return;

    tablas = cont.Database.SqlQueryRaw<Tabla>(Tabla.sqlTablas()).ToArray();
    Console.WriteLine(tablas.Length + " tablas");

    for (int i = 0; i < tablas.Length; i++) {
        Tabla tabla = tablas[i];
        tabla.Namespace = namespaceTransporte;

        tabla.Columnas = cont.Database.SqlQueryRaw<Columna>(Columna.sqlColumnas(tabla.name)).ToArray();

        tabla.Indices = cont.Database.SqlQueryRaw<Indice>(Indice.sqlIndexes(tabla.name)).ToArray();

        foreach (Columna c in tabla.Columnas) c.Indices = Indice.getIndices(tabla.Indices, c);
    }
}
#endregion

Console.WriteLine("Se van a escribir " + tablas.Length + " clases para las tablas, deseas continuar Y/N");
key = "" + Console.ReadKey().KeyChar;
Console.WriteLine(""); if (key == null || !key.ToLower().Equals("y")) return;

#region "Exclusion de tablas"
Console.WriteLine("Deseas excluir tablas? Y/N");
key = "" + Console.ReadKey().KeyChar;
Console.WriteLine(""); if (key != null && key.ToLower().Equals("y")) {
    for(int i=0; i<tablas.Length; i++) Console.WriteLine("[" + i + "] " + tablas[i].name);
    Console.WriteLine("escribe los indices separados por comas eje: 0,2,4");
    string entrada = Console.ReadLine();
    string[] __indices = entrada.Split(',');
    int[] _indices = new int[0];
    List<int> ___indices = new List<int>();
    foreach (string ind in __indices) ___indices.Add(int.Parse(ind));
    _indices = ___indices.ToArray();

    Console.Write("Se excluirán: ");
    foreach (int ind in _indices) Console.Write(ind + ", ");
    Console.WriteLine();

    List<Tabla> restantes = new List<Tabla>();
    for (int i = 0; i < tablas.Length; i++)
    {
        if (!_indices.Contains(i)) restantes.Add(tablas[i]);
    }
    tablas = restantes.ToArray();

    Console.Write("Tablas restantes: ");
    foreach (Tabla t in tablas) Console.Write(t.name + ", ");
    Console.WriteLine();

    Console.WriteLine("Deseas continuar? Y/N");
    key = "" + Console.ReadKey().KeyChar;
    Console.WriteLine(""); if (key == null || !key.ToLower().Equals("y")) return;
}
#endregion

for (int i = 0; i < tablas.Length; i++) {
    Tabla tabla = tablas[i];

    Console.WriteLine("Escribiendo entidad de transporte tabla: " + tabla.name);

    System.IO.File.WriteAllText(rutaDestinoClases + tabla.name + ".cs", tabla.ToString());

    Console.WriteLine("Escribiendo controlador de transporte tabla: " + tabla.name);
    Controlador cont = new Controlador(tabla, namespaceAPI, namespaceTransporte, namespaceWrapperAPI, namespaceAccesoDatos);
    System.IO.File.WriteAllText(rutaDestinoControladores + tabla.name + "Controller.cs", cont.ToString());
}

Console.WriteLine("Escribiendo contexto con " + tablas.Length + " tablas");

GeneradorContexto gcon = new GeneradorContexto(namespaceTransporte, namespaceAccesoDatos);
foreach (Tabla tabla in tablas) gcon.tablas.Add(tabla);
System.IO.File.WriteAllText(rutaDestinoContexto + "Contexto.cs", gcon.ToString());

Console.WriteLine("Escribiendo " + tablas.Length + " modelos de angular");

for (int i = 0; i < tablas.Length; i++)
{
    System.IO.File.WriteAllText(rutaModelosAngular + tablas[i].name + ".ts", tablas[i].ToStringAngular());
}

Console.WriteLine("Escribiendo " + tablas.Length + " servicios de angular");

for (int i = 0; i < tablas.Length; i++)
{
    ServicioAngular sa = new ServicioAngular(tablas[i], rutaModelosAngular, rutaAppAngular, rutaServiciosAngular, apiURL);
    System.IO.File.WriteAllText(rutaServiciosAngular + tablas[i].name.ToLower() + ".service.ts", sa.ToString());
}

Console.WriteLine("Escribiendo " + tablas.Length + " componentes de angular");

for (int i = 0; i < tablas.Length; i++)
{
    System.IO.DirectoryInfo tdi = new System.IO.DirectoryInfo(rutaComponentesAngular + tablas[i].name.ToLower());
    if (!tdi.Exists) tdi.Create();
    System.IO.File.WriteAllText(rutaComponentesAngular + tablas[i].name.ToLower() + "\\" + tablas[i].name.ToLower() + ".component.ts", new ComponenteAngularTS(tablas[i]).ToString());
    System.IO.File.WriteAllText(rutaComponentesAngular + tablas[i].name.ToLower() + "\\" + tablas[i].name.ToLower() + ".component.spec.ts", new ComponenteAngularSPEC(tablas[i]).ToString());
    System.IO.File.WriteAllText(rutaComponentesAngular + tablas[i].name.ToLower() + "\\" + tablas[i].name.ToLower() + ".component.scss", new ComponenteAngularSCSS(tablas[i]).ToString());
    System.IO.File.WriteAllText(rutaComponentesAngular + tablas[i].name.ToLower() + "\\" + tablas[i].name.ToLower() + ".component.html", new ComponenteAngularHTML(tablas[i]).ToString());
}

Console.WriteLine("Deseas esrcibir el modulo de ruteo de angular? Y/N");
key = "" + Console.ReadKey().KeyChar;
Console.WriteLine("");
if (key != null && key.ToLower().Equals("y")) {
    Console.WriteLine("Escribiendo router de angular");
    System.IO.File.WriteAllText(rutaAppAngular + "app.routes.ts", new RouterAngular(tablas).ToString());
}

Console.WriteLine("Proceso terminado");