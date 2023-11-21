using Microsoft.AspNetCore.Diagnostics;
using Entities.ErrorModel;
using Polaris.Exceptions.Base;

namespace Polaris.Extensions;

public static class GlobalExceptionMiddlewareExtensions
{
	public static WebApplication ConfigureGlobalExceptionHandler(this WebApplication app)
	{
		app.UseExceptionHandler(appError =>
		{

			// appError is a Delegate i.e it is a callback function, that states a list of instructions to be performed after an event or someting
			appError.Run(async context =>
			{
				context.Response.ContentType = "application/json";

				// Get information about the Exception that occured
				var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                // Check if there is exception information available
                if (contextFeature != null)
				{
					context.Response.StatusCode = contextFeature.Error switch
					{
						NotFoundException => StatusCodes.Status404NotFound,
						_ => StatusCodes.Status500InternalServerError
					};

					await context.Response.WriteAsync(new ErrorDetails()
					{
						StatusCode = context.Response.StatusCode,
						Message = contextFeature.Error.Message,
					}.ToString());
				}
			});
		});

		return app;
	}
}
