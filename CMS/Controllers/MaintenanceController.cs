using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaintenanceController : ControllerBase
    {
        IMemoryCache _memoryCache;
        public MaintenanceController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpPost("restart")]
        public IActionResult Restart()
        {
            // Log the restart request if necessary
            // Log("Restart requested");

            // Ensure this method is secured properly
            // For example, check if the request is from an authorized user

            // Perform the restart asynchronously to allow the response to be sent before restarting
            try
            {
                Task.Run(() => RestartApplication());

                return Ok("Application is restarting...");
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        private void RestartApplication()
        {
            try
            {
                ClearMemoryCache();
                // Get the path of the currently running executable
                string applicationPath = Process.GetCurrentProcess().MainModule.FileName;
                string applicationDirectory = Path.GetDirectoryName(applicationPath);
                string applicationName = Path.GetFileName(applicationPath);

                // Create a new process to start the application after the current one stops
                var startInfo = new ProcessStartInfo
                {
                    FileName = applicationPath,
                    WorkingDirectory = applicationDirectory
                };

                // Start the new instance with a delay
                Task.Run(async () =>
                {
                    // Wait for a short period to ensure the current process has time to shut down
                    await Task.Delay(1000);
                    Process.Start(startInfo);
                });

                // Exit the current application gracefully
                // You might want to perform additional cleanup tasks here

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
            }

        }
        private void ClearMemoryCache()
        {
            // Assuming _memoryCache is an instance of IMemoryCache injected into the controller
            // Here we use reflection to clear all entries in the cache

            try
            {
                if (_memoryCache is MemoryCache cache)
                {
                    // Use reflection to access the internal EntriesCollection of the MemoryCache
                    var entries = cache.GetType()
                        .GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        .GetValue(cache) as dynamic;

                    if (entries != null)
                    {
                        // Create a list to hold the keys of all cache entries
                        var keys = new List<object>();

                        // Iterate through the entries and collect the keys
                        foreach (var entry in entries)
                        {
                            var key = entry.GetType().GetProperty("Key").GetValue(entry, null);
                            keys.Add(key);
                        }

                        // Remove each key from the cache
                        foreach (var key in keys)
                        {
                            _memoryCache.Remove(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
            }

            // Alternatively, if you have a more straightforward way to access all cache keys,
            // you can use that method to clear the cache.
        }
    }
}
