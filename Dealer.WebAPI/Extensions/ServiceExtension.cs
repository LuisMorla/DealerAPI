using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Dealer.WebAPI.Extensions {
    public static class ServiceExtension {
        public static void AddSwaggerExtension(this IServiceCollection services) {
            services.AddSwaggerGen(c => {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));

                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Dealer API",
                    Version = "v1",
                    Description = "API para el manejo de la información de los vehículos",
                    Contact = new OpenApiContact {
                        Name = "Luis Junior Morla Vásquez",
                        Email = "Luisjrmorla@gmail.com"
                    }
                });

                c.DescribeAllParametersInCamelCase();
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection services) {
            services.AddApiVersioning(c => {
                c.DefaultApiVersion = new ApiVersion(1, 0);
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.ReportApiVersions = true;
            });
        }
    }
}
