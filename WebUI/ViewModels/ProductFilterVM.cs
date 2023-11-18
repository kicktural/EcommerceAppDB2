using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;

namespace WebUI.ViewModels
{
    public class ProductFilterVM
    {
        public IEnumerable<ProductFiltredDTO> ProductFilters { get; set; }
        public IEnumerable<CategoryFilterDTO> CategoryFilters { get; set; }

    }
}
