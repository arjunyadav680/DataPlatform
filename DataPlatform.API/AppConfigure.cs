using Microsoft.OpenApi.Models;
using System.ComponentModel;


namespace DataPlatform.API
{
    public static class AppConfigure
    {
        public static void AddHttpClient(IServiceCollection services)
        {
            //Register all the HttpClients here to avoid port exhaustion issues.

            var httpCallTimeout = TimeSpan.FromSeconds(30);

            services.AddHttpClient("MICROSOFT_GRAPH", client =>
            {
                client.BaseAddress = new Uri("https://graph.microsoft.com/beta/");
                client.Timeout = httpCallTimeout;
            });

            services.AddHttpClient("MICROSOFT_LOGIN", client =>
            {
                client.BaseAddress = new Uri("https://login.microsoftonline.com/");
                client.Timeout = httpCallTimeout;
            });

            services.AddHttpClient("APPLE", client =>
            {
                client.BaseAddress = new Uri("https://appleid.apple.com/");
                client.Timeout = httpCallTimeout;
            });

            services.AddHttpClient("GOOGLE_ACCOUNTS", client =>
            {
                client.BaseAddress = new Uri("https://accounts.google.com/");
                client.Timeout = httpCallTimeout;
            });

            services.AddHttpClient("GOOGLE_API", client =>
            {
                client.BaseAddress = new Uri("https://www.googleapis.com/");
                client.Timeout = httpCallTimeout;
            });

            services.AddHttpClient("METWEATHER", client =>
            {
                client.BaseAddress = new Uri("https://api.met.no/");
                client.DefaultRequestHeaders.Add("User-Agent", "rnd");
            });
        }

        public static void AddCorsSettings(IServiceCollection services, string[] allowedOrigins, string myAllowSpecificOrigins)
        {
          
           services.AddCors(options =>
        {
            options.AddPolicy(name: myAllowSpecificOrigins, builder =>
            {
                builder.SetIsOriginAllowed(origin =>
                {
                    if (string.IsNullOrEmpty(origin))
                        return true;

                    // Exact matches from config
                    if (allowedOrigins.Any(o =>
                        !o.StartsWith("*.") &&
                        origin.StartsWith(o, StringComparison.OrdinalIgnoreCase)))
                        return true;

                    if (allowedOrigins.Any(o =>
                        o.StartsWith("*.") &&
                        origin.EndsWith(o.Substring(1), StringComparison.OrdinalIgnoreCase)))
                        return true;

                    return false;
                })
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // needed for SignalR
            });
        });
        }

        public static void AddHangfire(IServiceCollection services)
        {
            ////if (!Utils.IsDebugMode)
            ////{
            ////    services.AddHangfire(configuration =>
            ////    {
            ////        configuration.UseRedisStorage(RedisDataHelper.RedisConnection, new RedisStorageOptions()
            ////        {
            ////            Db = RedisDataHelper.RedisDB.Database,
            ////            Prefix = "udf_hf_",
            ////            SucceededListSize = 500, //Maximum visible background jobs in the succeed list to prevent it from growing indefinitely.
            ////            DeletedListSize = 200, //Maximum visible background jobs in the deleted list to prevent it from growing indefinitely.
            ////        }).WithJobExpirationTimeout(TimeSpan.FromDays(7));//Expire and remove jobs after 7 days
            ////    });

            ////    //The server has the task of picking up job definitions from the storage and executing them.
            ////    services.AddHangfireServer();
            ////}

            //if (!Utils.IsDebugMode)
            //{
            //    services.AddHangfire(configuration =>
            //        configuration.UseSqlServerStorage(
            //            DBConnectionHelper.Configuration.GetConnectionString("DefaultConnection"),
            //            new SqlServerStorageOptions
            //            {
            //                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //                QueuePollInterval = TimeSpan.FromSeconds(15),
            //                UseRecommendedIsolationLevel = true,
            //                DisableGlobalLocks = true,
            //                SchemaName = "HF_"
            //            })
            //        .WithJobExpirationTimeout(TimeSpan.FromDays(7)) // Expire job logs after 7 days
            //    );

            //    //Enables cleanup of expired job data
            //    services.AddHangfireServer(options =>
            //    {
            //        options.SchedulePollingInterval = TimeSpan.FromSeconds(15);
            //    });

            //    //The server has the task of picking up job definitions from the storage and executing them.
            //    //services.AddHangfireServer();
            //}
        }

        public static void ScheduleTask(WebApplication app, IConfiguration configuration)
        {
            //if (!Utils.IsDebugMode)
            //{
            //    app.UseHangfireDashboard("/jobs", new DashboardOptions()
            //    {
            //        DashboardTitle = "Jobs",
            //        Authorization = new[]
            //        {
            //        new HangfireCustomBasicAuthenticationFilter{
            //            User = configuration.GetSection("HangfireSettings:UserName").Value,
            //            Pass = configuration.GetSection("HangfireSettings:Password").Value
            //        }
            //    }
            //    });

            //    CronService.RunApiTrackCronJob();

            //}
        }

    }

}
