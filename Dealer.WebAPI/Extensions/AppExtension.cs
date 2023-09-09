namespace Dealer.WebAPI.Extensions {
    public static class AppExtension {
        //Extender el configure

        public static void UseSwaggerExtension(this IApplicationBuilder app) {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dealer API");
            });


        }
    }
}
