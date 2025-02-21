using Microsoft.AspNetCore.Mvc;

namespace projet_collab.Interfaces
{
    public interface IServiceController
    {
        IActionResult GetAll<T>() where T : class, new();
        IActionResult GetById<T>(int id) where T : class, new();
        IActionResult GetByField<T>(string field, string value) where T : class, new();
        IActionResult CreateNew<T>([FromBody] T entity) where T : class, new();
        IActionResult Put<T>(int id, [FromBody] T entity) where T : class, new();
        IActionResult Delete<T>(int id) where T : class, new();
    }
}
