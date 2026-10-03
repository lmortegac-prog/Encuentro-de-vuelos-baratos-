public class Vuelo
{
    public string Origen { get; set; }
    public string Destino { get; set; }
    public string Aerolinea { get; set; }
    public decimal Precio { get; set; }

    public Vuelo(string origen, string destino, string aerolinea, decimal precio)
    {
        Origen = origen;
        Destino = destino;
        Aerolinea = aerolinea;
        Precio = precio;
    }
}