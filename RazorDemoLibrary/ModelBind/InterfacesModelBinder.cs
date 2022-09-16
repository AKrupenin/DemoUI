using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WSSC.V5.SYS.UICore
{
    public class InterfacesModelBinder : IModelBinder
    {
        private readonly ModelBinderProviderContext _Context;
        private IDictionary<ModelMetadata, IModelBinder> _propertyBinders;

        public InterfacesModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinder, ModelBinderProviderContext context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyBinders = propertyBinder ?? throw new ArgumentNullException(nameof(propertyBinder));
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }

            //if (!CanCreateModel(bindingContext))
            //{
            //    await Task.CompletedTask;
            //}

            await BindModelCoreAsync(bindingContext);
        }

        private async Task BindModelCoreAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.Model == null)
            {
                bindingContext.Model = await CreateModel(bindingContext);
            }

            if (bindingContext.Model == null)
            {
                return;
            }

                ModelMetadata modelMetadata = bindingContext.ModelMetadata;
            bool attemptedPropertyBinding = false;
            for (int i = 0; i < modelMetadata.Properties.Count; i++)
            {
                ModelMetadata property = modelMetadata.Properties[i];
                if (CanBindProperty(bindingContext, property))
                {
                    object model = null;
                    if (property.PropertyGetter != null && property.IsComplexType && !property.ModelType.IsArray)
                    {
                        model = property.PropertyGetter(bindingContext.Model);
                    }
                    else
                    {
                        model = bindingContext.Model;
                    }

                    string fieldName = property.BinderModelName ?? property.PropertyName;
                    string modelName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, fieldName);
                    ModelBindingResult result;
                    using (bindingContext.EnterNestedScope(property, fieldName, modelName, model))
                    {
                        await BindProperty(bindingContext);
                        result = bindingContext.Result;
                    }

                    if (result.IsModelSet)
                    {
                        attemptedPropertyBinding = true;
                        SetProperty(bindingContext, modelName, property, result);
                    }
                    else if (property.IsBindingRequired)
                    {
                        attemptedPropertyBinding = true;
                        string errorMessage = property.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(fieldName);
                        bindingContext.ModelState.TryAddModelError(modelName, errorMessage);
                    }
                }
            }

           

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            
        }

        protected virtual bool CanBindProperty(ModelBindingContext bindingContext, ModelMetadata propertyMetadata)
        {
            Func<ModelMetadata, bool> obj = bindingContext.ModelMetadata.PropertyFilterProvider?.PropertyFilter;
            if (obj != null && !obj(propertyMetadata))
            {
                return false;
            }

            Func<ModelMetadata, bool> propertyFilter = bindingContext.PropertyFilter;
            if (propertyFilter != null && !propertyFilter(propertyMetadata))
            {
                return false;
            }

            if (!propertyMetadata.IsBindingAllowed)
            {
                return false;
            }

            if (!CanUpdatePropertyInternal(propertyMetadata))
            {
                return false;
            }

            return true;
        }

        
        protected virtual Task BindProperty(ModelBindingContext bindingContext)
        {
            return _propertyBinders[bindingContext.ModelMetadata].BindModelAsync(bindingContext);
        }

        internal bool CanCreateModel(ModelBindingContext bindingContext)
        {
            bool isTopLevelObject = bindingContext.IsTopLevelObject;
            BindingSource bindingSource = bindingContext.BindingSource;
            if (!isTopLevelObject && bindingSource != null && bindingSource.IsGreedy)
            {
                return false;
            }

            if (isTopLevelObject)
            {
                return true;
            }

            if (CanBindAnyModelProperties(bindingContext))
            {
                return true;
            }

            return false;
        }

        private bool CanBindAnyModelProperties(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelMetadata.Properties.Count == 0)
            {
                
                return false;
            }

            for (int i = 0; i < bindingContext.ModelMetadata.Properties.Count; i++)
            {
                ModelMetadata modelMetadata = bindingContext.ModelMetadata.Properties[i];
                if (!CanBindProperty(bindingContext, modelMetadata))
                {
                    continue;
                }

                BindingSource bindingSource = modelMetadata.BindingSource;
                if (bindingSource != null && bindingSource.IsGreedy)
                {
                    return true;
                }

                string text = modelMetadata.BinderModelName ?? modelMetadata.PropertyName;
                string modelName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, text);
                using (bindingContext.EnterNestedScope(modelMetadata, text, modelName, null))
                {
                    if (bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal static bool CanUpdatePropertyInternal(ModelMetadata propertyMetadata)
        {
            if (propertyMetadata.IsReadOnly)
            {
                return CanUpdateReadOnlyProperty(propertyMetadata.ModelType);
            }

            return true;
        }

        private static bool CanUpdateReadOnlyProperty(Type propertyType)
        {
            if (propertyType.GetTypeInfo().IsValueType)
            {
                return false;
            }

            if (propertyType.IsArray)
            {
                return false;
            }

            if (propertyType == typeof(string))
            {
                return false;
            }

            return true;
        }



        //
        // Summary:
        //     Updates a property in the current Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Model.
        //
        // Parameters:
        //   bindingContext:
        //     The Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.
        //
        //   modelName:
        //     The model name.
        //
        //   propertyMetadata:
        //     The Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata for the property to set.
        //
        //   result:
        //     The Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult for the property's
        //     new value.
        protected virtual void SetProperty(ModelBindingContext bindingContext, string modelName, ModelMetadata propertyMetadata, ModelBindingResult result)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }

            if (modelName == null)
            {
                throw new ArgumentNullException("modelName");
            }

            if (propertyMetadata == null)
            {
                throw new ArgumentNullException("propertyMetadata");
            }

            if (result.IsModelSet && !propertyMetadata.IsReadOnly)
            {
                object model = result.Model;
                try
                {
                    propertyMetadata.PropertySetter(bindingContext.Model, model);
                }
                catch (Exception exception)
                {
                    AddModelError(exception, modelName, bindingContext);
                }
            }
        }

        private static void AddModelError(Exception exception, string modelName, ModelBindingContext bindingContext)
        {
            TargetInvocationException ex = exception as TargetInvocationException;
            if (ex?.InnerException != null)
            {
                exception = ex.InnerException;
            }

            ModelStateDictionary modelState = bindingContext.ModelState;
            if (modelState.GetFieldValidationState(modelName) == ModelValidationState.Unvalidated)
            {
                modelState.AddModelError(modelName, exception, bindingContext.ModelMetadata);
            }
        }


        protected async Task<object> CreateModel(ModelBindingContext bindingContext)
        {
           

            var modelName = bindingContext.ModelName + ".TypeName";

                var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
                if (valueProviderResult == ValueProviderResult.None)
                {
                    return null;
                }

                var modelType = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => !assembly.IsDynamic)
                        .SelectMany(x => x.GetExportedTypes())
                        .Where(type => !type.IsInterface)
                        .Where(type => !type.IsAbstract)
                        //.Where(bindingContext.ModelType.IsAssignableFrom)
                        .FirstOrDefault(type =>
                          string.Equals(type.FullName, valueProviderResult.FirstValue,
                            StringComparison.OrdinalIgnoreCase));



                var propertyBinders = new Dictionary<ModelMetadata, IModelBinder>();
                var metadata = _Context.MetadataProvider.GetMetadataForType(modelType);
                for (var i = 0; i < metadata.Properties.Count; i++)
                {
                    var property = metadata.Properties[i];
                    propertyBinders.Add(property, _Context.CreateBinder(property));
                }
                bindingContext.ModelMetadata = metadata;

            _propertyBinders = propertyBinders;
            return ActivatorUtilities.CreateInstance(bindingContext.HttpContext.RequestServices, modelType);




        }
    }
}
