using COSUMIR_API_TEST.Models;

namespace COSUMIR_API_TEST.Servicios
{
    public interface IServicio_API
    {
        Task<List<Book>> Lista();
        Task<Book> Obtener(int idProducto);

        Task<bool> Guardar(Book objeto);

        Task<bool> Editar(Book objeto);

        Task<bool> Eliminar(int idProducto);
    }
}
