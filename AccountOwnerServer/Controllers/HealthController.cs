using System;
using System.Threading.Tasks;

using AccountOwnerServer.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountOwnerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HealthController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Basic health check endpoint for load balancers and deployment slots
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }

        /// <summary>
        /// Warm-up endpoint that initializes database connection and ensures application is ready
        /// Used by Azure slot swapping to prevent cold starts
        /// </summary>
        [HttpGet("warmup")]
        public async Task<IActionResult> Warmup()
        {
            try
            {
                // Test database connectivity
                await _context.Database.CanConnectAsync();

                return Ok(new
                {
                    status = "warmed-up",
                    timestamp = DateTime.UtcNow,
                    database = "connected"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(503, new
                {
                    status = "unhealthy",
                    timestamp = DateTime.UtcNow,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Readiness probe - checks if application is ready to accept traffic
        /// </summary>
        [HttpGet("ready")]
        public async Task<IActionResult> Ready()
        {
            try
            {
                // Verify database is accessible
                await _context.Database.CanConnectAsync();

                return Ok(new
                {
                    status = "ready",
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(503, new
                {
                    status = "not-ready",
                    timestamp = DateTime.UtcNow,
                    error = ex.Message
                });
            }
        }
    }
}


