using Microsoft.AspNetCore.Mvc.Rendering;

namespace Product_Catalog.BLL.Services
{
    public interface ISelectList<T> where T : class
    {
        SelectList GetSelectList();
    }
}
