namespace Packing.Pedidos.Interfaces
{
    public interface IMailer
    {
        Task EnviarEmail(List<string> destinatarios, List<string> destinatariosOcultos, string asunto, string cuerpo);
    }
}
