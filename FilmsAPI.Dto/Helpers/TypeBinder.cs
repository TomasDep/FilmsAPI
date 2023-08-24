using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace FilmsAPI.Dto.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var property = bindingContext.ModelName;
            var provider = bindingContext.ValueProvider.GetValue(property);

            if (provider == ValueProviderResult.None)
                return Task.CompletedTask;

            try
            {
                var deserializedValue = JsonConvert.DeserializeObject<List<T>>(provider.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializedValue);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(property, "Invalid value for type List<int>");
            }
            return Task.CompletedTask;
        }
    }
}