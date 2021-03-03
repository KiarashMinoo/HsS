using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HsS.Services
{
    public class EventsHub : Hub
    {
        private static readonly ConcurrentDictionary<string, List<string>> _connections;
        public static IReadOnlyDictionary<string, List<string>> Connections { get { return _connections; } }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<EventsHub> hubContext;

        static EventsHub()
        {
            _connections = new ConcurrentDictionary<string, List<string>>();
        }

        public EventsHub(IHttpContextAccessor httpContextAccessor, IHubContext<EventsHub> hubContext)
        {
            _httpContextAccessor = httpContextAccessor;
            this.hubContext = hubContext;
        }

        public override Task OnConnectedAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!_connections.TryGetValue(userId, out List<string> connectionIds))
                connectionIds = new List<string>();

            connectionIds.Add(Context.ConnectionId);

            _connections.TryAdd(userId, connectionIds);

            return base.OnConnectedAsync();
        }
    }
}
