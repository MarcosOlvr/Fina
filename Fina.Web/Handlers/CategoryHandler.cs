using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using System.Net.Http.Json;

namespace Fina.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient();

        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("v1/categories", request);

            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao criar a categoria");
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            var result = await _httpClient.DeleteAsync($"v1/categories/{request.Id}");

            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao remover a categoria");
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
            => await _httpClient.GetFromJsonAsync<PagedResponse<List<Category>?>>("v1/categories")
            ?? new PagedResponse<List<Category>?>(null, 400, "Não foi possivel resgatar as categorias");

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
            => await _httpClient.GetFromJsonAsync<Response<Category?>>($"v1/categories/{request.Id}")
            ?? new Response<Category?>(null, 40, "Não foi possivel resgatar a categoria");

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"v1/categories/{request.Id}", request);

            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao atualizar a categoria");
        }
    }
}
